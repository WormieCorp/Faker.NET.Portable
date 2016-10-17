using NUnit.Framework;

namespace Faker.Tests.it_IT
{
	[SetUICulture("it-IT")]
	[SetCulture("it-IT")]
	[Category("Culture 'it_IT'")]
	public class AddressItalianTests
	{
		[Test]
		[Repeat(1000)]
		public void Should_Get_Building_Number()
		{
			string buildingNum = Address.BuildingNumber();

			Assert.That(buildingNum, Has.Length.GreaterThanOrEqualTo(3)
										.Or.Length.LessThanOrEqualTo(5));

			buildingNum.AssertFormats(Resources.Address.BuildingNumber.ToFormat());
		}

		[Test]
		[Repeat(1000)]
		public virtual void Should_Get_City()
		{
			string cityPrefixFormat = Resources.Address.CityPrefix.ToFormat();
			string firstNameFormat = Resources.Name.First.ToFormat();
			string citySuffixFormat = Resources.Address.CitySuffix.ToFormat();

			string city = Address.City();

			city.AssertFormats(cityPrefixFormat + " " + firstNameFormat + " " + citySuffixFormat,
							   cityPrefixFormat + " " + firstNameFormat,
							   firstNameFormat + " " + citySuffixFormat);
		}

		[Test]
		[Repeat(1000)]
		public virtual void Should_Get_Street_Address()
		{
			string addressStreetPrefixFormat = Resources.Address.StreetPrefix.ToFormat();
			string firstNameFormat = Resources.Name.First.ToFormat();
			string lastNameFormat = Resources.Name.Last.ToFormat();
			string buildingNumberFormat = Resources.Address.BuildingNumber.ToFormat();

			string address = Address.StreetAddress();

			address.AssertFormats(addressStreetPrefixFormat + " " + firstNameFormat + " " + lastNameFormat + ", " + buildingNumberFormat);
		}

		[Test]
		[Repeat(1000)]
		public virtual void Should_Get_Street_Address_With_Secondary_Address()
		{
			string addressStreetPrefixFormat = Resources.Address.StreetPrefix.ToFormat();
			string firstNameFormat = Resources.Name.First.ToFormat();
			string lastNameFormat = Resources.Name.Last.ToFormat();
			string buildingNumberFormat = Resources.Address.BuildingNumber.ToFormat();
			string addressSecondaryAddressFormat = Resources.Address.SecondaryAddress.ToFormat();

			string address = Address.StreetAddress(true);

			address.AssertFormats(addressStreetPrefixFormat + " " + firstNameFormat + " " + lastNameFormat + ", " + buildingNumberFormat + " " + addressSecondaryAddressFormat);
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_Zip_Code()
		{
			string zipcodeFormat = Resources.Address.ZipCode.ToFormat(true);

			string zipcode = Address.ZipCode();

			zipcode.AssertFormats(zipcodeFormat);
		}
	}
}
