#load "nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease"

Environment.SetVariableNames();

BuildParameters.SetParameters(
	context: Context,
	buildSystem: BuildSystem,
	sourceDirectoryPath: "./src",
	testDirectoryPath: "./tests",
	title: "Faker.NET.Portable",
	repositoryOwner: "WormieCorp",
	repositoryName: "Faker.NET.Portable",
	appVeyorAccountName: "AdmiringWorm",
	appVeyorProjectSlug: "faker-cs",
	shouldPublishChocolatey: false,
	solutionFilePath: "./Faker.sln",
	shouldRunInspectCode: false
);

ToolSettings.SetToolSettings(
	context: Context,
    dupFinderExcludePattern: new string[] {
        BuildParameters.RootDirectoryPath + "/tests/**/*.cs"
    },
     dupFinderExcludeFilesByStartingCommentSubstring: new string[] {
         "<auto-generated>"
     },
     testCoverageFilter: "+[Faker.*]* -[*.Tests]*",
     testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
     testCoverageExcludeByFile: "*Designer.cs;*.g.cs;*.g.i.cs"
);

BuildParameters.PrintParameters(Context);

Build.Run();
