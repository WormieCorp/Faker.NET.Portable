using System;
using Faker.Caching;
using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   A collection of Business related resources.
	/// </summary>
	/// <include file="Docs/CustomRemarks.xml" path="Comments/SatelliteResource/*" />
	/// <threadsafety static="true" />
	public static class Business
	{
		/// <summary>
		///   Generates a random Credit card number.
		/// </summary>
		/// <returns>A random Credit card number</returns>
		public static string CreditCardNumber()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Business.CreditCardNumbers)).Random();
		}

		/// <summary>
		///   Generates a random credit card expiration date.
		/// </summary>
		/// <returns>A random credit card expiration date.</returns>
		public static DateTime CreditCardExpiryDate()
		{
			return DateTime.Now.AddDays(365 * (RandomNumber.Next(4) + 1));
		}

		/// <summary>
		///   Generates a random credit card type.
		/// </summary>
		/// <returns>A random credit card type.</returns>
		public static string CreditCardType()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Business.CreditCardTypes)).Random();
		}

		/// <summary>
		///   Generates a random National insurance number.
		/// </summary>
		/// <returns>A random National insurance number</returns>
		public static string NationalInsuranceNumber()
		{
			return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Business.NationalInsuranceNumbers)).Random();
		}
	}
}
