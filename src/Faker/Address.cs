using Faker.Caching;
using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   A collection of address related resources.
	/// </summary>
	/// <include file="Docs/CustomRemarks.xml" path="Comments/SatelliteResource/*" />
	/// <threadsafety static="true" />
	public static class Address
	{
		/// <summary>
		///   Gets a random building number.
		/// </summary>
		/// <returns>The building number.</returns>
		public static string BuildingNumber()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.BuildingNumber)).Random().Numerify();
		}

		#region Random Example

		/// <summary>
		///   Gets a random city.
		/// </summary>
		/// <returns>The random city.</returns>
		public static string City()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.CityFormat)).Random().Transform(true);
		}

		#endregion Random Example

		/// <summary>
		///   Gets a random city prefix.
		/// </summary>
		/// <returns>The random city prefix.</returns>
		public static string CityPrefix()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.CityPrefix)).Random();
		}

		/// <summary>
		///   Gets a random city suffix.
		/// </summary>
		/// <returns>The city suffix.</returns>
		public static string CitySuffix()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.CitySuffix)).Random();
		}

		/// <summary>
		///   Gets a random country.
		/// </summary>
		/// <returns>The random country.</returns>
		public static string Country()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.Country)).Random();
		}

		/// <summary>
		///   Gets a random country code.
		/// </summary>
		/// <returns>The random country code.</returns>
		public static string CountryCode()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.CountryCode)).Random();
		}

		/// <summary>
		///   Gets the default country.
		/// </summary>
		/// <returns>The default country.</returns>
		public static string DefaultCountry()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.DefaultCountry)).Random();
		}

		/// <summary>
		///   Gets a random latitude coordinate.
		/// </summary>
		/// <returns>The latitude.</returns>
		public static double Latitude()
		{
			return (RandomNumber.NextDouble() * 180) - 90;
		}

		/// <summary>
		///   Gets a random longitude coordinate.
		/// </summary>
		/// <returns>The longitude.</returns>
		public static double Longitude()
		{
			return (RandomNumber.NextDouble() * 360) - 180;
		}

		/// <summary>
		///   Gets a random secondary address.
		/// </summary>
		/// <returns>A random secondary address.</returns>
		public static string SecondaryAddress()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.SecondaryAddress)).Random().Numerify();
		}

		/// <summary>
		///   Gets a random state.
		/// </summary>
		/// <returns>The random state.</returns>
		/// <remarks>
		///   <note>If the selected Culture doesn't have states, this will output a Random Country instead.</note>
		/// </remarks>
		public static string State()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.State)).Random();
		}

		/// <summary>
		///   Gets a random state abbreviation.
		/// </summary>
		/// <returns>The state abbreviation.</returns>
		/// <remarks>
		///   <note>If the selected Culture doesn't have states, this will output a Random Country
		///   Abbreviation instead.</note>
		/// </remarks>
		public static string StateAbbreviation()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StateAbbr)).Random();
		}

		/// <summary>
		///   Gets a random street address, without a Secondary address.
		/// </summary>
		/// <returns>A random street address, without a secondary address.</returns>
		public static string StreetAddress()
		{
			return StreetAddress(false);
		}

		/// <summary>
		///   Gets a random street address.
		/// </summary>
		/// <param name="includeSecondaryAddress">
		///   if set to <see langword="true" /> include secondary address.
		/// </param>
		/// <returns>A random street address.</returns>
		public static string StreetAddress(bool includeSecondaryAddress)
		{
			return includeSecondaryAddress
					   ? ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StreetAddressSecondaryFormat)).Random().Transform(true)
					   : ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StreetAddressFormat)).Random().Transform(true);
		}

		/// <summary>
		///   Gets the name of a random street.
		/// </summary>
		/// <returns>The name of a random street.</returns>
		public static string StreetName()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StreetNameFormat)).Random().Transform(true);
		}

		/// <summary>
		///   Gets a random street suffix.
		/// </summary>
		/// <returns>The random street suffix.</returns>
		public static string StreetSuffix()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StreetSuffix)).Random();
		}

		/// <summary>
		///   Gets a random time zone.
		/// </summary>
		/// <returns>The time zone.</returns>
		public static string TimeZone()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.TimeZone)).Random();
		}

		/// <summary>
		///   Gets a random zip code.
		/// </summary>
		/// <returns>The random zip code.</returns>
		public static string ZipCode()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.ZipCode)).Random().Numerify();
		}
	}
}
