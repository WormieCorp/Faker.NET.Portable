public static class Publishing
{
	public static void NuGet(ICakeContext context, FilePath packagePath)
	{
		var apiKey = context.EnvironmentVariable("NUGET_API_KEY");
		PublishNugetPackage(context, apiKey, null, packagePath);
	}

	public static void MyGet(ICakeContext context, FilePath packagePath)
	{
		var apiKey = context.EnvironmentVariable("MYGET_API_KEY");
		var apiUrl = context.EnvironmentVariable("MYGET_API_URL");
		PublishNugetPackage(context, apiKey, apiUrl, packagePath);
	}

	public static void MyGetSymbols(ICakeContext context, FilePath packagePath)
	{
		var apiKey = context.EnvironmentVariable("MYGET_API_KEY");
		var symbolsUrl = context.EnvironmentVariable("MYGET_SYMBOLS_API_URL");
		PublishNugetPackage(context, apiKey, symbolsUrl, packagePath);
	}

	private static void PublishNugetPackage(ICakeContext context, string apiKey, string apiUrl, FilePath packagePath)
	{
		if (string.IsNullOrEmpty(apiKey))
		{
			throw new ArgumentNullException("apiKey");
		}

		var settings = new NuGetPushSettings
		{
			ApiKey = apiKey
		};
		if (!string.IsNullOrEmpty(apiUrl))
		{
			settings.Source = apiUrl;
		}

		context.NuGetPush(packagePath, settings);
	}
}
