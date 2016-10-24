using Faker.Caching;
using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   A collection of Company related resources.
	/// </summary>
	/// <include file="Docs/CustomRemarks.xml" path="Comments/SatelliteResource/*" />
	/// <threadsafety static="true" />
	public static class Company
	{
		/// <summary>
		///   When a straight answer won't do, Bullshit to the rescue!
		/// </summary>
		/// <returns>Some <c>bullshit</c>.</returns>
		/// <remarks>Wordlist originates from <see href="http://dack.com/web/bullshit.html" /></remarks>
		public static string Bullshit()
		{
			return string.Join(
				" ",
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.BS1)).Random(),
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.BS2)).Random(),
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.BS3)).Random());
		}

		/// <summary>
		///   Generates a buzzword-laden catch phrase
		/// </summary>
		/// <returns>The buzzword-laden catch phrase.</returns>
		/// <remarks>Wordlist originates from <see href="http://www.1728.com/buzzword.htm" /></remarks>
		public static string CatchPhrase()
		{
			return string.Join(
				" ",
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.Buzzwords1)).Random(),
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.Buzzwords2)).Random(),
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.Buzzwords3)).Random());
		}

		/// <summary>
		///   Gets a random logo URL.
		/// </summary>
		/// <returns>A random logo URL from</returns>
		public static string Logo()
		{
			int randNum = RandomNumber.Next(13) + 1;
			return "http://pigment.github.io/fake-logos/logos/medium/color/{0}.png".FormatCulture(randNum);
		}

		/// <summary>
		///   Generates a random Company name
		/// </summary>
		/// <returns>The random company name.</returns>
		public static string Name()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.NameFormats)).Random().Transform(true);
		}

		/// <summary>
		///   Generates a random Company suffix.
		/// </summary>
		/// <returns>The random company suffix.</returns>
		public static string Suffix()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Company.Suffix)).Random();
		}
	}
}
