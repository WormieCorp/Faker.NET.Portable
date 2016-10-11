using NUnit.Framework;
using System;
using System.Diagnostics;

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
		[Repeat(1)]
		public void Compute_Time_Elapsed_By_The_First_Method()
		{
			var sw = new Stopwatch();
			sw.Start();
			for (int i = 0; i < 100000; i++)
			{
				Name.First();
			}
			sw.Stop();
			Console.WriteLine("Elapsed time without caching {0}msec", sw.ElapsedMilliseconds);
			sw.Reset();

			sw.Start();
			for (int i = 0; i < 100000; i++)
			{
				Name.FirstCached();
			}
			sw.Stop();
			Console.WriteLine("Elapsed time with caching {0}msec", sw.ElapsedMilliseconds);
		}

		[Test]
		[Repeat(1)]
		public void Compute_Time_Elapsed_By_The_Last_Method()
		{
			Name.Last();
		}
	}
}
