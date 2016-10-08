using System.Net;
using System.Net.Sockets;
using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class InternetTests
	{
		public const string EMAIL_REGEX =
			"^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$";

		[Test]
		[Repeat(1000)]
		public void Should_Create_Email_Address()
		{
			string email = Internet.Email();

			Assert.That(email, Does.StartWith(EMAIL_REGEX)
								 .And.Not.Contains("www"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Create_Email_Address_From_Given_Name()
		{
			string email = Internet.Email("Bob Smith");

			Assert.That(email, Does.Match(@"^bob[_\.]smith@")
								 .And.Match(EMAIL_REGEX));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Create_User_Name()
		{
			string username = Internet.UserName();

			Assert.That(username, Does.Match(@"^[a-z]+((_|\.)[a-z]+)?$"));
		}

		[Test]
		public void Should_Create_User_Name_From_Given_Name()
		{
			string username = Internet.UserName("Bob Smith");

			Assert.That(username, Does.Match(@"^bob[_\.]smith$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Mac_Address()
		{
			string mac = Internet.MacAddress();

			Assert.That(mac,
						Does.Match(@"^([0-9A-F]{2}:){5}([0-9A-F]{2})$"));
		}

		[Test]
		public void Should_Generate_Mac_Address_With_Custom_Group_Split()
		{
			string mac = Internet.MacAddress(groupSplit: '-');

			Assert.That(mac,
						Does.Match(@"^([0-9A-F]{2}-){5}([0-9A-F]{2})$"));
		}

		[Test]
		public void Should_Generate_Mac_Address_With_Existing_Prefix()
		{
			string mac = Internet.MacAddress("0F:3A");

			Assert.That(mac,
						Does.Match(@"^0F:3A:([0-9A-F]{2}:){3}([0-9A-F]{2})$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Password()
		{
			string password = Internet.Password(8, 16);

			Assert.That(password, Has.Length.GreaterThanOrEqualTo(8)
									 .And.Length.LessThanOrEqualTo(16));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Slug()
		{
			string slug = Internet.Slug();

			Assert.That(slug,
						Does.Match("^[A-Za-z_\\.\\-]+$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_A_Random_Url()
		{
			string url = Internet.Url();

			Assert.That(url, Does.Match(
											   @"https?\:\/\/\w+(\.\w+){1,2}\/[a-z]+((_|\.)[a-z]+)?$")
							   .Or.Match(
												  @"https?\:\/\/www2?\.\w+(\.\w+){1,2}\/[a-z]+((_|\.)[a-z]+)?$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_Domain_Name()
		{
			string domain = Internet.DomainName();

			Assert.That(domain, Does.Match(@"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_Domain_Suffix()
		{
			string[] suffixFormat = Resources.Internet.DomainSuffix.Split(Config.SEPARATOR);

			string suffix = Internet.DomainSuffix();

			Assert.That(new[] { suffix }, Is.SubsetOf(suffixFormat));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_Domain_Word()
		{
			string word = Internet.DomainWord();

			Assert.That(word, Does.Match(@"^\w+$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_IP_Version_4_Address()
		{
			string ipAddressString = Internet.IPv4Address();

			IPAddress ipAddress = IPAddress.Parse(ipAddressString);

			Assert.That(ipAddress.AddressFamily, Is.EqualTo(AddressFamily.InterNetwork));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_IP_Version_6_Address()
		{
			string ipAddressString = Internet.IPv6Address();

			IPAddress ipAddress = IPAddress.Parse(ipAddressString);

			Assert.That(ipAddress.AddressFamily, Is.EqualTo(AddressFamily.InterNetworkV6));
		}
	}
}
