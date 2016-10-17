public class BuildPaths
{
	public BuildDirectories Directories { get; private set; }
	public BuildFiles Files { get; private set; }

	public static BuildPaths GetPaths(
		ICakeContext context,
		string configuration,
		string semVersion
	)
	{
		if (context == null)
		{
			context.Error("No context was passed to BuildPaths.GetPaths");
			throw new ArgumentNullException("context");
		}

		if (string.IsNullOrEmpty(configuration))
		{
			context.Error("No configuration was passed to BuildPaths.GetPaths");
			throw	new ArgumentNullException("configuration");
		}

		if (string.IsNullOrEmpty(semVersion))
		{
			context.Error("No semVersion was passed to BuildPaths.GetPaths");
			throw new ArgumentNullException("semVersion");
		}

		var buildDir = (DirectoryPath)(context.Directory("./src/Faker/bin") + context.Directory(configuration));
		var artifactsDir = (DirectoryPath)(context.Directory("./artifacts") + context.Directory("v" + semVersion));
		var testResultsDir = artifactsDir.Combine("test-results");
		var testingDir = context.Directory("./tests/Faker.Tests/bin") + context.Directory(configuration);
		var resourcePaths = new DirectoryPath[]
		{
			"de-DE",
			"en-US",
			"it-IT",
			"nb-NO",
			"pt-BR"
		}.Select(d => buildDir.Combine(d)).ToArray();

		var fakerFiles = new FilePath[]
		{
			context.File("Faker.Portable.dll"),
			context.File("Faker.Portable.pdb"),
			context.File("Faker.Portable.xmL")
		};

		var fakerAssemblyPaths =
			fakerFiles.Select(file => buildDir.CombineWithFilePath(file))
			.ToArray();

		var testingAssemblyPaths = new FilePath[]
		{
			testingDir + context.File("Faker.Tests.dll"),
			testingDir + context.File("Faker.Tests.pdb")
		};

		var repoFilePaths = new FilePath[]
		{
			"LICENSE",
			"README.md",
			"CHANGELOG.md",
			"Contributors.md"
		};

		var artifactSourcePaths = fakerAssemblyPaths.Concat(testingAssemblyPaths.Concat(repoFilePaths)).ToArray();

		var zipArtifactPath = artifactsDir.CombineWithFilePath("Faker-bin-v" + semVersion + ".zip");

		var testCoverageOutputFilePath = testResultsDir.CombineWithFilePath("OpenCover.xml");

		var buildDirectories = new BuildDirectories(
			artifactsDir,
			testResultsDir,
			artifactsDir.Combine("nuget"),
			artifactsDir.Combine("bin"),
			resourcePaths
		);

		var buildFiles = new BuildFiles(
			context,
			fakerAssemblyPaths,
			testingAssemblyPaths,
			repoFilePaths,
			artifactSourcePaths,
			zipArtifactPath,
			testCoverageOutputFilePath
		);

		return new BuildPaths
		{
			Files = buildFiles,
			Directories = buildDirectories
		};
	}
}

public class BuildFiles
{
	public ICollection<FilePath> FakerAssemblyPaths { get; private set; }
	public ICollection<FilePath> TestingAssemblyPaths { get; private set; }
	public ICollection<FilePath> RepoFilePaths { get; private set; }
	public ICollection<FilePath> ArtifactsSourcePaths { get; private set; }

	public FilePath ZipArtifactPath { get; private set; }
	public FilePath TestCoverageOutputFilePath { get; private set; }

	public BuildFiles(
		ICakeContext context,
		FilePath[] fakerAssemblyPaths,
		FilePath[] testingAssemblyPaths,
		FilePath[] repoFilePaths,
		FilePath[] artifactsSourcePaths,
		FilePath zipArtifactPath,
		FilePath testCoverageOutputFilePath
	)
	{
		FakerAssemblyPaths = Filter(context, fakerAssemblyPaths);
		TestingAssemblyPaths = Filter(context, testingAssemblyPaths);
		RepoFilePaths = Filter(context, repoFilePaths);
		ArtifactsSourcePaths = Filter(context, artifactsSourcePaths);
		ZipArtifactPath = zipArtifactPath;
		TestCoverageOutputFilePath = testCoverageOutputFilePath;
	}

	private static FilePath[] Filter(ICakeContext context, FilePath[] files)
	{
		if (!context.IsRunningOnWindows())
		{
			return files.Where(f => !f.FullPath.EndsWith("pdb")).ToArray();
		}
		return files;
	}
}

public class BuildDirectories
{
	public DirectoryPath Artifacts { get; private set; }
	public DirectoryPath TestResults { get; private set; }
	public DirectoryPath NugetRoot { get; private set; }
	public DirectoryPath ArtifactsBin { get; private set; }

	public ICollection<DirectoryPath> ResourcePaths { get; private set; }
	public ICollection<DirectoryPath> ToClean { get; private set; }

	public BuildDirectories(
		DirectoryPath artifactsDir,
		DirectoryPath testResultsDir,
		DirectoryPath nugetRoot,
		DirectoryPath artifactsBinDir,
		DirectoryPath[] resourceDirs
	)
	{
		Artifacts = artifactsDir;
		TestResults = testResultsDir;
		NugetRoot = nugetRoot;
		ArtifactsBin = artifactsBinDir;
		ResourcePaths = resourceDirs;
		ToClean = new[]
		{
			Artifacts,
			TestResults,
			NugetRoot,
			ArtifactsBin
		}.Concat(ResourcePaths).ToArray();
	}
}
