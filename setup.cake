#load "nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease"

Environment.SetVariableNames();

BuildParameters.SetParameters(
	context: Context,
	buildSystem: BuildSystem,
	sourceDirectoryPath: "./src",
	title: "Faker.NET.Portable",
	repositoryOwner: "WormieCorp",
	repositoryName: "Faker.NET.Portable",
	appVeyorAccountName: "AdmiringWorm",
	shouldDownloadFullReleaseNotes: true,
	shouldDownloadMilestoneReleaseNotes: true,
	shouldPublishChocolatey: false,
	shouldPublishNuGet: false,
	shouldPublishGitHub: false,
	shouldExecuteGitLink: false,
	solutionFilePath: "./Faker.sln"
);

ToolSettings.SetToolSettings(context: Context);

Build.Run();
