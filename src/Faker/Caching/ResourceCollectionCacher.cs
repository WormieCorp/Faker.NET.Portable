using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Faker.Caching
{
	/// <summary>
	///   This class caches resource items containing collections of strings, separated by a separator.
	/// </summary>
	/// <remarks>
	///   It returns the split array reducing complexity from O(n) to O(1). The class is thread-safe.
	/// </remarks>
	/// <threadsafety static="true" />
	internal static class ResourceCollectionCacher
	{
		private static object lockObj = new object();
		private static Dictionary<string, string[]> cache = new Dictionary<string, string[]>();

		internal static string[] GetArray(PropertyInfo p)
		{
			var invokingClassName = p.DeclaringType.FullName;
			var invokedPropertyName = p.Name;
			var currentUICultureName = CultureInfo.CurrentUICulture.Name;
			var cacheKey = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", invokingClassName, invokedPropertyName, currentUICultureName);

			lock (lockObj)
			{
				if (!cache.ContainsKey(cacheKey))
				{
					var get = p.GetGetMethod(true);
					var collection = (string)get.Invoke(null, null);
					var splittedArray = collection.Split(Config.SEPARATOR).Select(s => s.Trim()).ToArray();
					cache[cacheKey] = splittedArray;
				}
			}

			return cache[cacheKey];
		}

		/// <summary>
		///   Used just for testing purposes
		/// </summary>
		internal static void Clear()
		{
			cache.Clear();
		}
	}
}
