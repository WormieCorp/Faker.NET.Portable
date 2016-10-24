using System;
using System.Diagnostics.CodeAnalysis;

namespace Faker
{
	/// <summary>
	///   Provides access to random number generator.
	/// </summary>
	/// <seealso cref="Random" />
	/// <threadsafety static="true" />
	/// <remarks>This is just a convenience class and is just wrapping <see cref="Random" />.</remarks>
	public static class RandomNumber
	{
		private static readonly object LockObject = new object();
		private static Random random = new Random();

		/// <inheritdoc cref="Random.Next()" />
		/// <seealso cref="Random.Next()" />
		public static int Next()
		{
			lock (LockObject)
			{
				return random.Next();
			}
		}

		/// <inheritdoc cref="Random.NextBytes(byte[])" />
		public static void NextBytes(byte[] buffer)
		{
			lock (LockObject)
			{
				random.NextBytes(buffer);
			}
		}

		/// <inheritdoc cref="Random.Next(int)" />
		/// <seealso cref="Random.Next(int)" />
		/// <include file="Docs/RevisionHistory.xml" path="Revisions/RandomNumber[@id=&quot;NextMaxValue&quot;]/revisionHistory" />
		public static int Next(int maxValue)
		{
			lock (LockObject)
			{
				return random.Next(maxValue);
			}
		}

		/// <inheritdoc cref="Random.Next(int,int)" />
		/// <seealso cref="Random.Next(int,int)" />
		/// <include file="Docs/RevisionHistory.xml" path="Revisions/RandomNumber[@id=&quot;NextMinValueMaxValue&quot;]/revisionHistory" />
		public static int Next(int minValue, int maxValue)
		{
			lock (LockObject)
			{
				return random.Next(minValue, maxValue);
			}
		}

		/// <inheritdoc cref="Next(int,int)" />
		public static long Next(long min, long max)
		{
			var buf = new byte[8];
			NextBytes(buf);
			var longRand = BitConverter.ToInt64(buf, 0);

			return Math.Abs(longRand % (max - min)) + min;
		}

		/// <inheritdoc cref="Random.NextDouble()" />
		public static double NextDouble()
		{
			lock (LockObject)
			{
				return random.NextDouble();
			}
		}

		/// <inheritdoc cref="System.Random(int)" />
		/// <seealso cref="Random" />
		/// <include file="Docs/RevisionHistory.xml" path="Revisions/RandomNumber[@id=&quot;ResetSeed&quot;]/revisionHistory" />
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "We don't want to change existing api")]
		public static void ResetSeed(int Seed)
		{
			lock (LockObject)
			{
				random = new Random(Seed);
			}
		}
	}
}
