using System;
using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class BooleanTests
	{
		[Test]
		[Repeat(1000)]
		public void Should_Generate_True_Or_False()
		{
			int trueCount = 0;
			int falseCount = 0;

			// Check that roughly 50% of values return true/false
			for (int i = 0; i < 10000; i++)
			{
				var result = Boolean.Next();
				if (result)
					trueCount++;
				else
					falseCount++;
			}

			Assert.That(trueCount, Is.GreaterThan(1000));
			Assert.That(falseCount, Is.GreaterThan(1000));
		}
	}
}
