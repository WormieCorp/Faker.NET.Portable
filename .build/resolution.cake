public static class PathResolution
{
	private static List<DirectoryPath> _paths;
	private static readonly object _lock = new object();

	public static FilePath Resolve(ICakeContext context, string toolName)
	{
		if (string.IsNullOrEmpty(toolName))
		{
			throw new ArgumentNullException("Tool name cannot be empty.", toolName);
		}

		lock (_lock)
		{
			if (_paths == null)
			{
				_paths = GetPathDirectories(context);
			}

			foreach (var pathDir in _paths)
			{
				var file = pathDir.CombineWithFilePath(toolName);
				if (context.FileExists(file))
				{
					return file;
				}
			}
		}
		return null;
	}

	private static List<DirectoryPath> GetPathDirectories(ICakeContext context)
	{
		var result = new List<DirectoryPath>();
		var path = context.EnvironmentVariable("PATH");
		if (!string.IsNullOrEmpty(path))
		{
			var separator = new [] { context.IsRunningOnUnix() ? ':' : ';' };
			var paths = path.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			result.AddRange(paths.Select(p => new DirectoryPath(p)));
		}
		return result;
	}
}
