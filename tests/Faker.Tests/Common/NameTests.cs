using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class NameTests
	{
		[Test]
		[Repeat(1000)]
		public void Should_Get_Prefix()
		{
			string[] possiblePrefixes = Resources.Name.Prefix.Split(Config.SEPARATOR);

			string prefix = Name.Prefix();

			Assert.That(new[] { prefix }, Is.SubsetOf(possiblePrefixes));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Get_Suffix()
		{
			string[] possibleSuffixes = Resources.Name.Suffix.Split(Config.SEPARATOR);

			string suffix = Name.Suffix();

			Assert.That(new[] { suffix }, Is.SubsetOf(possibleSuffixes));
		}

		[Test]
		[Repeat(100000)]
		public void Compute_Time_Elapsed_By_The_First_Method()
		{
			Name.First();
		}

		[Test]
		[Repeat(100000)]
		public void Compute_Time_Elapsed_By_The_Last_Method()
		{
			Name.Last();
		}
	}
}
