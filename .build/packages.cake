public class BuildPackages
{
	public ICollection<BuildPackage> Nuget { get; private set; }

	public static BuildPackages GetPackages(
		DirectoryPath nugetRootPath,
		string semVersion,
		string[] packageIds
	)
	{
		var toNugetPackage = BuildPackage(nugetRootPath, semVersion);
		var nugetPackages = packageIds.Select(toNugetPackage).ToArray();

		return new BuildPackages
		{
			Nuget = nugetPackages
		};
	}

	private static Func<string, BuildPackage> BuildPackage(
		DirectoryPath nugetRootPath,
		string semVersion
	)
	{
		return package => new BuildPackage(
			id: package,
			nuspecPath: string.Concat("./nuspec/", package, ".nuspec"),
			packagePath: nugetRootPath.CombineWithFilePath(string.Concat(package, ".", semVersion, ".nupkg"))
		);
	}
}

public class BuildPackage
{
	public string Id { get; private set; }
	public FilePath NuspecPath { get; private set; }
	public FilePath PackagePath { get; private set; }
	public FilePath PackageSymbolsPath { get; private set; }

	public BuildPackage (
		string id,
		FilePath nuspecPath,
		FilePath packagePath
	)
	{
		Id = id;
		NuspecPath = nuspecPath;
		PackagePath = packagePath;
		PackageSymbolsPath = packagePath.ChangeExtension("symbols.nupkg");
	}
}
