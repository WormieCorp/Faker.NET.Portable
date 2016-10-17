#load "./credentials.cake"
#load "./packages.cake"
#load "./paths.cake"
#load "./version.cake"

public class BuildParameters
{
	public string Configuration { get; private set; }
	public string Target { get; private set; }
	public Verbosity Verbosity { get; private set; }
	public string CurrentBranch { get; private set; }

	public bool IsLocalBuild { get; private set; }
  public bool IsMainBranch { get; private set; }
	public bool IsMainRepo { get; private set; }
	public bool IsPublishBuild { get; private set; }
	public bool IsPullRequest { get; private set; }
	public bool IsReleaseBuild { get; private set; }
	public bool IsRunningOnAppVeyor { get; private set; }
	public bool IsRunningOnTravisCI { get; private set; }
  public bool IsRunningOnWindows { get; private set; }
	public bool IsTagged { get; private set; }

	public bool ShouldPublishToMyGet
	{
		get
		{
			return IsRunningOnAppVeyor && !IsPullRequest &&
				IsMainRepo && (IsTagged || BranchMatches(CurrentBranch, "develop","release/.+", "feature/.*", "hotfix/.*"));
		}
	}

	public bool ShouldPublish
	{
		get
		{
			return IsRunningOnAppVeyor && !IsPullRequest &&
				IsMainRepo && IsTagged;
		}
	}

	public bool ShouldCreateReleaseNotes
	{
		get
		{
			return IsRunningOnAppVeyor && !IsPullRequest &&
				IsMainRepo && IsMainBranch && !IsTagged;
		}
	}

	public BuildCredentials GitHub { get; private set; }
	public BuildPackages Packages { get; private set; }
	public BuildPaths Paths { get; private set; }
	public BuildVersion Version { get; private set; }

	public void Initialize(ICakeContext context)
	{
		Version = BuildVersion.Calculate(context, this);

		Paths = BuildPaths.GetPaths(context, Configuration, Version.SemVersion);

		Packages = BuildPackages.GetPackages(
			Paths.Directories.NugetRoot,
			Version.NuspecVersion,
			new [] { "Faker.Net.Portable" }
		);
	}

	public static BuildParameters GetParameters(ICakeContext context)
	{
		if (context == null)
		{
			throw new ArgumentNullException("context");
		}

		var target = context.Argument("target", "Default");
		var buildSystem = context.BuildSystem();

		return new BuildParameters
		{
			Target = target,
			Configuration = context.Argument("configuration", "Release"),
			CurrentBranch = GetCurrentBranch(buildSystem),

			IsLocalBuild = buildSystem.IsLocalBuild,
			IsMainBranch = IsOnBranch(buildSystem, "master"),
			IsMainRepo = IsOnRepo(buildSystem, "AdmiringWorm/Faker.Net.Portable"),
			IsPublishBuild = IsPublishing(target),
			IsPullRequest = CheckIsPullRequest(buildSystem),
			IsReleaseBuild = IsReleasing(target),
			IsRunningOnAppVeyor = buildSystem.IsRunningOnAppVeyor,
			IsRunningOnTravisCI = buildSystem.IsRunningOnTravisCI,
			IsRunningOnWindows = context.IsRunningOnWindows(),
			IsTagged = IsBuildTagged(buildSystem),

			GitHub = new BuildCredentials(
				username: context.EnvironmentVariable("GITHUB_USERNAME"),
				password: context.EnvironmentVariable("GITHUB_PASSWORD")
			),
		};
	}

	private static bool BranchMatches(string currentBranch, params string[] branchMatches)
	{
		var regexString = "^" + string.Join("|", branchMatches) + "$";
		return System.Text.RegularExpressions.Regex.IsMatch(currentBranch, regexString);
	}

	private static string GetCurrentBranch(BuildSystem buildSystem)
	{
		if (buildSystem.IsRunningOnAppVeyor) {
			return buildSystem.AppVeyor.Environment.Repository.Branch;
		}
		else if(buildSystem.IsRunningOnTravisCI) {
			return buildSystem.TravisCI.Environment.Build.Branch;
		}

		return "unknown";
	}

	private static bool IsBuildTagged(BuildSystem buildSystem)
	{
		return buildSystem.AppVeyor.Environment.Repository.Tag.IsTag
			&& !string.IsNullOrWhiteSpace(buildSystem.AppVeyor.Environment.Repository.Tag.Name);
	}

	private static bool IsOnBranch(BuildSystem buildSystem, string branch)
	{
		var actualBranch = GetCurrentBranch(buildSystem);
		return StringComparer.OrdinalIgnoreCase.Equals(branch, actualBranch);
	}

	private static bool IsOnRepo(BuildSystem buildSystem, string repository)
	{
		string actualRepo = "unknown";
		if (buildSystem.IsRunningOnAppVeyor)
		{
			actualRepo = buildSystem.AppVeyor.Environment.Repository.Name;
		}

		return StringComparer.OrdinalIgnoreCase.Equals(repository, actualRepo);
	}

	private static bool CheckIsPullRequest(BuildSystem buildSystem)
	{
		if (buildSystem.IsRunningOnAppVeyor)
		{
			return buildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
		}
		if (buildSystem.IsRunningOnTravisCI)
		{
			return !string.IsNullOrWhiteSpace(buildSystem.TravisCI.Environment.Repository.PullRequest);
		}

		return false;
	}

	private static bool IsPublishing(string target)
	{
		var targets = new [] { "Create-Release-Notes", "Export-Release-Notes" };
		return targets.Any(t => StringComparer.OrdinalIgnoreCase.Equals(t, target));
	}

	private static bool IsReleasing(string target)
	{
		var targets = new [] { "Publish", "Publis-NuGet", "Publish-GitHub-Release" };
		return targets.Any(t => StringComparer.OrdinalIgnoreCase.Equals(t, target));
	}
}
