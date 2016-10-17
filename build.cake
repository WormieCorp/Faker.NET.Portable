#tool "nuget:?package=gitreleasemanager&version=0.6.0"
#tool "nuget:?package=GitVersion.CommandLine&version=3.6.4"
#tool "nuget:?package=OpenCover&version=4.6.519"
#tool "nuget:?package=NUnit.ConsoleRunner&version=3.4.1"

#load "./.build/parameters.cake"
#load "./.build/resolution.cake"
#load "./.build/publishing.cake"

var parameters = BuildParameters.GetParameters(Context);;
bool publishingError = false;

Setup(context =>
{
	parameters.Initialize(context);

	Information("Building version {0} of Faker.Net.Portable ({1}, {2}) using version {3} of Cake. (IsTagged: {4})",
		parameters.Version.SemVersion,
		parameters.Configuration,
		parameters.Target,
		parameters.Version.CakeVersion,
		parameters.IsTagged);
});

Task("Clean")
	.Does(() =>
	{
		CleanDirectories(parameters.Paths.Directories.ToClean);
	});

Task("Restore-NuGet-Packages")
	.IsDependentOn("Clean")
	.Does(() =>
	{
		NuGetRestore("./Faker.sln");
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
	{
		DotNetBuild("./Faker.sln", settings =>
			settings.SetVerbosity(parameters.Verbosity)
				.SetConfiguration(parameters.Configuration));
	});

Task("Run-Unit-Tests")
	.IsDependentOn("Build")
	.Does(() =>
{
	var settings = new NUnit3Settings
	{
		Verbose = parameters.Verbosity == Verbosity.Verbose || parameters.Verbosity == Verbosity.Diagnostic,
		Configuration = parameters.Configuration,
		OutputFile = parameters.Paths.Directories.TestResults.CombineWithFilePath("faker-testresults.xml")
	};

	var path = PathResolution.Resolve(Context, "nunit3-console.exe");
	if (path != null)
	{
		settings.ToolPath = path;
	}

	Action<ICakeContext> testAction = tool =>
	{
		tool.NUnit3("./tests/**/bin/" + parameters.Configuration + "/*.Tests.dll", settings);
	};

	if (!parameters.IsRunningOnWindows)
	{
		testAction(Context);
	}
	else
	{
		OpenCover(testAction,
			parameters.Paths.Files.TestCoverageOutputFilePath,
			new OpenCoverSettings
			{
				ReturnTargetCodeOffset = 0,
				MergeOutput = true,
				SkipAutoProps = true,
				Register = "user"
			}
			.WithFilter("+[Faker*]Faker* -[*.Tests]* -[Faker*]Faker.Resources.*")
			.ExcludeByAttribute("*.ExcludeFromCodeCoverage*")
			.ExcludeByFile("**/*Designer.cs;*/*.g.cs;*/*.g.i.cs")
		);
	}
});

/*Task("Generate-Coverage-Report")
	.WithCriteria(() => parameters.IsRunningOnWindows)
	.IsDependentOn("Run-Unit-Tests")
	.Does(() =>
	{
		var historyDir = parameters.Paths.Directories.ReportsDirectory.Combine("history");
		ReportGenerator(
			parameters.Paths.Files.TestCoverageOutputFilePath,
			parameters.Paths.Directories.ReportsDirectory,
			new ReportGeneratorSettings
			{
				HistoryDirectory = historyDir,
				Verbosity = (parameters.Verbosity == Verbosity.Minimal
				  ? ReportGeneratorVerbosity.Error :
					(parameters.Verbosity == Verbosity.Normal
						  ? ReportGeneratorVerbosity.Info : ReportGeneratorVerbosity.Verbose
					)
				)
			}
		);
	});*/

Task("Copy-Files")
	.IsDependentOn("Build")
	.Does(() =>
	{
		CopyFiles(
			parameters.Paths.Files.ArtifactsSourcePaths,
			parameters.Paths.Directories.ArtifactsBin
		);

		foreach (var directory in parameters.Paths.Directories.ResourcePaths)
		{
			var directoryName = directory.GetDirectoryName();
			CopyDirectory(directory, parameters.Paths.Directories.ArtifactsBin + "/" + directoryName);
		}
	});

Task("Zip-Files")
	.IsDependentOn("Copy-Files")
	.IsDependentOn("Export-Release-Notes")
	.Does(() =>
	{
		var files = GetFiles(parameters.Paths.Directories.ArtifactsBin.FullPath + "/**/*")
			- GetFiles(parameters.Paths.Directories.ArtifactsBin.FullPath + "/*.Tests.*");
		Zip(parameters.Paths.Directories.ArtifactsBin, parameters.Paths.Files.ZipArtifactPath, files);
	});

Task("Create-NuGet-Packages")
	.IsDependentOn("Copy-Files")
	.IsDependentOn("Export-Release-Notes")
	.Does(() =>
	{
		//var releaseNotes = ParseAllReleaseNotes("./CHANGELOG.md").First().Notes.ToArray();
		var changelogFile = parameters.Paths.Directories.ArtifactsBin.CombineWithFilePath("CHANGELOG.md");

		foreach (var package in parameters.Packages.Nuget)
		{
			NuGetPack(package.NuspecPath, new NuGetPackSettings
			{
				Version = parameters.Version.NuspecVersion,
				ReleaseNotes = ParseReleaseNotes(changelogFile).Notes.ToArray(),
				BasePath = parameters.Paths.Directories.ArtifactsBin,
				OutputDirectory = parameters.Paths.Directories.NugetRoot,
				Symbols = true
			});
		}
	});

Task("Upload-AppVeyor-Artifacts")
	.WithCriteria(() => parameters.IsRunningOnAppVeyor)
	.IsDependentOn("Run-Unit-Tests") // Not Really, but we don't want to upload artifacts if the tests didn't pass
	.IsDependentOn("Create-NuGet-Packages")
	.IsDependentOn("Zip-Files")
	.Does(() =>
	{
		AppVeyor.UploadArtifact(parameters.Paths.Files.ZipArtifactPath);
		foreach (var package in GetFiles(parameters.Paths.Directories.NugetRoot + "/*"))
		{
			AppVeyor.UploadArtifact(package);
		}
	});

/*Task("Upload-Test-Results")
	.WithCriteria(() => parameters.IsRunningOnAppVeyor)
	.IsDependentOn("Run-Unit-Tests")
	.Does(() =>
	{
		AppVeyor.UploadTestResults(parameters.Paths.Directories.TestResults.CombineWithFilePath("faker-testresults.xml"),
			AppVeyorTestResultsType.NUnit3);
	});*/

/*Task("Upload-Coverage-Report")
	.WithCriteria(() => parameters.IsRunningOnAppVeyor)
	.WithCriteria(() => PathResolution.Resolve(Context, "codecov.exe") != null)
	.IsDependentOn("Run-Unit-Tests")
	.Does(() =>
	{
		var token = EnvironmentVariable("CODECOV_TOKEN");
		var settings = new ProcessSettings()
			.WithArguments((args) => args
				.Append("-f").Append(parameters.Paths.Files.TestCoverageOutputFilePath.FullPath)
				.Append("-X").Append("gcov"));
		if (!string.IsNullOrEmpty(token))
		{
			settings.WithArguments((args) => args.Append("-t").Append(token));
		}
		var file = PathResolution.Resolve(Context, "codecov.exe");
		StartProcess(file, settings);
	});*/

Task("Create-Release-Notes")
	.WithCriteria(() => parameters.GitHub.HasCredentials)
	.WithCriteria(() => parameters.ShouldPublish)
	.Does(() =>
	{
		GitReleaseManagerCreate(
			parameters.GitHub.Username,
			parameters.GitHub.Password,
			"AdmiringWorm",
			"Faker.Net.Portable",
			new GitReleaseManagerCreateSettings
			{
				Milestone = parameters.Version.Milestone,
				Prerelease = parameters.IsMainBranch
			}
		);
	});

Task("Publish-Git-Release")
	.WithCriteria(() => parameters.GitHub.HasCredentials)
	.Does(() =>
	{
		GitReleaseManagerPulblish(
			parameters.GitHub.Username,
			parameters.GitHub.Password,
			"AdmiringWorm",
			"Faker.Net.Portable",
			parameters.Version.Milestone
		);
	});

Task("Export-Release-Notes")
  .WithCriteria(() => parameters.GitHub.HasCredentials)
	.IsDependentOn("Copy-Files")
	.IsDependentOn("Create-Release-Notes")
	.Does(() =>
{
	GitReleaseManagerExport(
		parameters.GitHub.Username,
		parameters.GitHub.Password,
		"AdmiringWorm",
		"Faker.Net.Portable",
		parameters.Paths.Directories.ArtifactsBin.CombineWithFilePath("CHANGELOG.md")
	);
});

Task("Publish-MyGet")
	.IsDependentOn("Create-NuGet-Packages")
	.WithCriteria(() => parameters.ShouldPublishToMyGet)
	.Does(() =>
	{
		foreach (var package in parameters.Packages.Nuget)
		{
			Publishing.MyGet(Context, package.PackagePath);
			Publishing.MyGetSymbols(Context, package.PackageSymbolsPath);
		}
	})
	.OnError(exception =>
	{
		Information("Publish-MyGet Task failed, but continuing with next Task...");
		publishingError = true;
	});

Task("Publish-NuGet")
	.IsDependentOn("Create-NuGet-Packages")
	.WithCriteria(() => parameters.ShouldPublish)
	.Does(() =>
	{
		foreach (var package in parameters.Packages.Nuget)
		{
			Publishing.NuGet(Context, package.PackagePath);
		}
	});

Task("Publish-GitHub-Release")
	.IsDependentOn("Zip-Files")
	.WithCriteria(() => parameters.ShouldPublish)
	.Does(() =>
	{
		GitReleaseManagerAddAssets(parameters.GitHub.Username, parameters.GitHub.Password, "AdmiringWorm", "Faker.Net.Portable", parameters.Version.Milestone, parameters.Paths.Files.ZipArtifactPath.ToString());
		GitReleaseManagerClose(parameters.GitHub.Username, parameters.GitHub.Password, "AdmiringWorm", "Faker.Net.Portable", parameters.Version.Milestone);
	})
	.OnError(exception =>
	{
		Information("Publish-GitHub-Release Task failed, but continuing with next Task...");
		publishingError = true;
	});

Task("Package")
	.IsDependentOn("Zip-Files")
	.IsDependentOn("Create-NuGet-Packages");

Task("Default")
	.IsDependentOn("Run-Unit-Tests");

Task("Travis")
	.IsDependentOn("Run-Unit-Tests");

Task("AppVeyor")
	.IsDependentOn("Upload-AppVeyor-Artifacts")
	.IsDependentOn("Package")
	.IsDependentOn("Publish-MyGet")
	.IsDependentOn("Publish-NuGet")
	.IsDependentOn("Publish-GitHub-Release");

RunTarget(parameters.Target);
