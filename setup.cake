#load nuget:?package=Cake.Recipe

Environment.SetVariableNames();

BuildParameters.SetParameters(
	context: Context,
	buildSystem: BuildSystem,
	sourceDirectoryPath: "./src",
	title: "Faker.NET Portable Edition",
	repositoryOwner: "AdmiringWorm",
	repositoryName: "Faker.NET.Portable",
	appVeyorAccountName: "AdmiringWorm",
	shouldDownloadFullReleaseNotes: true,
	shouldDownloadMilestoneReleaseNotes: true,
	shouldPublishChocolatey: false,
	shouldPublishNuGet: false,
	shouldPublishGitHub: false,
	shouldExecuteGitLink: false
);

ToolSettings.SetToolSettings(context: Context);

Build.Run();
