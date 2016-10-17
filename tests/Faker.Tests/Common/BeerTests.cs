using System.Globalization;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class BeerTests
	{
		[Test]
		[Repeat(1000)]
		public void Should_Generate_Alcohol_Between_2_And_10()
		{
			var decimalSeparator = Regex.Escape(CultureInfo.CurrentCulture.NumberFormat.PercentDecimalSeparator);
			var symbol = Regex.Escape(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol);

			var result = Beer.Alcohol();

			Assert.That(result, Does.Match("^([2-9]" + decimalSeparator + "[0-9]|10" + decimalSeparator + "0)" + symbol + "$"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Hop()
		{
			var possibleValues = Resources.Beer.Hop.Split(';');

			var actualValue = new[] { Beer.Hop() };

			Assert.That(actualValue, Is.SubsetOf(possibleValues));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Malt()
		{
			var possibleValues = Resources.Beer.Malt.Split(';');

			var actualValue = new[] { Beer.Malt() };

			Assert.That(actualValue, Is.SubsetOf(possibleValues));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Name()
		{
			var possibleValues = Resources.Beer.Name.Split(';');

			var actualValue = new[] { Beer.Name() };

			Assert.That(actualValue, Is.SubsetOf(possibleValues));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Style()
		{
			var possibleValues = Resources.Beer.Style.Split(';');

			var actualValue = new[] { Beer.Style() };

			Assert.That(actualValue, Is.SubsetOf(possibleValues));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generate_Yeast()
		{
			var possibleValues = Resources.Beer.Yeast.Split(';');
			var actualValue = new[] { Beer.Yeast() };

			Assert.That(actualValue, Is.SubsetOf(possibleValues));
		}

		[Test]
		[Repeat(1000)]
		public void Should_Generated_IBU_Between_10_And_99()
		{
			var result = Beer.IBU();

			Assert.That(result, Does.Match("^[1-9][0-9] IBU$"));
		}
	}
}
