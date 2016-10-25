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

		/// <summary>
		///   Gets the cached array (with O(1) complexity) in cache-hit case. In cache-miss case,
		///   invokes the property, splits, caches, and returns the array (with O(n) complexity).
		/// </summary>
		/// <param name="p">The property returning the array</param>
		/// <returns>The (possibly cached) array</returns>
		/// <remarks>
		///   The cache is indexed by the class holding the property, the property name and the
		///   current UI culture name.
		/// </remarks>
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
