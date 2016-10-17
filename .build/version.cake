public class BuildVersion
{
	public string Version { get; private set; }
	public string SemVersion { get; private set; }
	public string NuspecVersion { get; private set; }
	public string Milestone { get; private set; }
	public string CakeVersion { get; private set; }

	public static BuildVersion Calculate(ICakeContext context, BuildParameters parameters)
	{
		if (context == null)
		{
			throw new ArgumentNullException("context");
		}

		string version = null;
		string semVersion = null;
		string milestone = null;
		string nuspecVersion = null;
		string legacySemVersion = null;

		if (context.IsRunningOnWindows() || parameters.IsRunningOnTravisCI)
		{
			context.Information("Calculating Semantic Version");
			if (!parameters.IsLocalBuild || parameters.IsPublishBuild || parameters.IsReleaseBuild)
			{
				context.GitVersion(new GitVersionSettings
				{
					UpdateAssemblyInfoFilePath = "./src/SolutionInfo.cs",
					UpdateAssemblyInfo = true,
					OutputType = GitVersionOutput.BuildServer
				});

				version = context.EnvironmentVariable("GitVersion_MajorMinorPatch");
				semVersion = context.EnvironmentVariable("GitVersion_SemVer");
				nuspecVersion = context.EnvironmentVariable("GitVersion_NuGetVersion");
				milestone = string.Concat("v", version);
			}

			GitVersion assertedVersions = context.GitVersion(new GitVersionSettings
			{
				OutputType = GitVersionOutput.Json
			});

			version = assertedVersions.MajorMinorPatch;
			semVersion = assertedVersions.SemVer;
			nuspecVersion = assertedVersions.NuGetVersion;
			milestone = string.Concat("v", version);

			context.Information("Calculated Semantic Version: {0}", semVersion);
		}

		if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(semVersion))
		{
			context.Information("Fetching version from AssemblyInfo");
			var assemblyInfo = context.ParseAssemblyInfo("./src/SolutionInfo.cs");
			version = assemblyInfo.AssemblyVersion;
			semVersion = assemblyInfo.AssemblyInformationalVersion;
		  int index = semVersion.IndexOf("+");
			if (index  > 0)
			{
				semVersion = semVersion.Substring(0, index);
			}

			var match = System.Text.RegularExpressions.Regex.Match(semVersion, @"\.([0-9]+)$");
			if (match.Success)
			{
				index = match.Index;
				nuspecVersion = semVersion.Substring(0, index);
				index = match.Groups[0].Index;
				nuspecVersion += semVersion.Substring(index + 1).PadLeft(4, '0');
			}

			milestone = string.Concat("v", version);
		}

		var cakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();

		return new BuildVersion
		{
			Version = version,
			SemVersion = semVersion,
			NuspecVersion = nuspecVersion,
			Milestone = milestone,
			CakeVersion = cakeVersion
		};
	}
}
