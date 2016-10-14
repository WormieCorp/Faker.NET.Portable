using Faker.Caching;
using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
using System.Resources;

namespace Faker.Tests.Common
{
	public class ResourceCollectionCachingTests
	{
		private System.Globalization.CultureInfo defaultUICulture;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			defaultUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
		}

		[SetUp]
		public void SetUp()
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture = defaultUICulture;
			ResourceCollectionCacher.Clear();
		}

		[Test]
		public void Cache_At_Least_Doubles_Performance()
		{
			var sw = new Stopwatch();
			sw.Start();
			for (int i = 0; i < 10000; i++)
				Resources.Name.First.Split(Config.SEPARATOR);
			sw.Stop();
			var uncachedElapsed = sw.ElapsedMilliseconds;
			sw.Reset();

			sw.Start();
			for (int i = 0; i < 10000; i++)
				ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
			sw.Stop();
			var cachedElapsed = sw.ElapsedMilliseconds;

			Assert.That(uncachedElapsed, Is.GreaterThan(2 * cachedElapsed));
		}

		[Test]
		public void Different_Methods_Of_Same_Class_Give_Not_Equal_Arrays()
		{
			// Cache miss case
			{
				var firstNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				var lastNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.Last));

				Assert.IsFalse(firstNames.SequenceEqual(lastNames));
			}

			// Cache hit case
			{
				var firstNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				var lastNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.Last));

				Assert.IsFalse(firstNames.SequenceEqual(lastNames));
			}
		}

		[Test]
		public void Switch_To_Foreign_Culture_Returns_Not_Equal_Arrays()
		{
			// Cache miss case
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = defaultUICulture;
				var array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
				var de_DE_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsFalse(array.SequenceEqual(de_DE_array));
			}

			// Cache hit case
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = defaultUICulture;
				var array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
				var de_DE_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsFalse(array.SequenceEqual(de_DE_array));
			}
		}

		[Test]
		public void Switch_Between_Foreign_Cultures_Returns_Not_Equal_Arrays()
		{
			// Cache miss case
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");
				var en_US_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
				var de_DE_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsFalse(en_US_array.SequenceEqual(de_DE_array));
			}

			// Cache hit case
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");
				var en_US_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
				var de_DE_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsFalse(en_US_array.SequenceEqual(de_DE_array));
			}
		}

		[Test]
		public void Switch_To_Unsupported_Culture_Returns_Equal_Arrays()
		{
			// Cache miss case
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = defaultUICulture;
				var array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("br-BR");
				var br_BR_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsTrue(array.SequenceEqual(br_BR_array));
			}

			// Cache hit case
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = defaultUICulture;
				var array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("br-BR");
				var br_BR_array = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsTrue(array.SequenceEqual(br_BR_array));
			}
		}

		[Test]
		public void Resource_And_Cache_Give_Equal_Arrays()
		{
			// Cache miss case
			{
				var firstNames = Resources.Name.First.Split(Config.SEPARATOR);
				var cachedFirtsNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsTrue(firstNames.SequenceEqual(cachedFirtsNames));
			}

			// Cache hit case
			{
				var firstNames = Resources.Name.First.Split(Config.SEPARATOR);
				var cachedFirtsNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsTrue(firstNames.SequenceEqual(cachedFirtsNames));
			}
		}

		[Test]
		public void Resource_And_Cache_Give_Equal_Arrays_With_Foreign_Culture()
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");

			// Cache miss case
			{
				var firstNames = Resources.Name.First.Split(Config.SEPARATOR);
				var cachedFirtsNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsTrue(firstNames.SequenceEqual(cachedFirtsNames));
			}

			// Cache hit case
			{
				var firstNames = Resources.Name.First.Split(Config.SEPARATOR);
				var cachedFirtsNames = ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Name.First));

				Assert.IsTrue(firstNames.SequenceEqual(cachedFirtsNames));
			}
		}
	}
}
