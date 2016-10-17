using System.Globalization;
using Faker.Caching;
using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   A collection of Beer related generator tasks.
	/// </summary>
	public static class Beer
	{
		/// <summary>
		///   Generates a random alcohol percentage between 2.0 and 10.0
		/// </summary>
		/// <returns>The generated Alcohol percentage.</returns>
		public static string Alcohol()
		{
			var num = RandomNumber.NextDouble() * (10.0 - 2.0) + 2.0;

			return num.ToString("N1", CultureInfo.CurrentCulture) + CultureInfo.CurrentCulture.NumberFormat.PercentSymbol;
		}

		/// <summary>
		///   Generates a random hop type
		/// </summary>
		/// <returns>The random hop type.</returns>
		public static string Hop()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Beer.Hop)).Random();
		}

		/// <summary>
		///   Generates a random IBU (Bitterness of the beer)
		/// </summary>
		/// <returns>The generated IBU.</returns>
		/// <remarks>Description of IBU is at (https://en.wikipedia.org/wiki/Beer_measurement#Bitterness)</remarks>
		public static string IBU()
		{
			return RandomNumber.Next(10, 100).ToString(CultureInfo.CurrentCulture) + " IBU";
		}

		/// <summary>
		///   Generates a random beer malt type
		/// </summary>
		/// <returns>The random beer malt type.</returns>
		public static string Malt()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Beer.Malt)).Random();
		}

		/// <summary>
		///   Generates a random Beer Name
		/// </summary>
		/// <returns>Random Beer Name.</returns>
		public static string Name()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Beer.Name)).Random();
		}

		/// <summary>
		///   Generates a random Beer Style
		/// </summary>
		/// <returns>The random beer style.</returns>
		public static string Style()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Beer.Style)).Random();
		}

		/// <summary>
		///   Generates a random yeast type
		/// </summary>
		/// <returns>The random yeast type.</returns>
		public static string Yeast()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Beer.Yeast)).Random();
		}
	}
}
