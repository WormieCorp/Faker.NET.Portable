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

		/// <summary>
		///   Returns a non-negative random integer
		/// </summary>
		/// <returns>
		///   A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="int.MaxValue" />.
		/// </returns>
		/// <seealso cref="Random.Next()" />
		public static int Next()
		{
			lock (LockObject)
			{
				return random.Next();
			}
		}

		/// <summary>
		///   Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <seealso cref="Random.NextBytes(byte[])" />
		/// <exception cref="ArgumentNullException"><paramref name="buffer" /> is <see langword="null" />.</exception>
		public static void NextBytes(byte[] buffer)
		{
			lock (LockObject)
			{
				random.NextBytes(buffer);
			}
		}

		/// <summary>
		///   Returns a non-negative random integer that is less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">
		///   The exclusive upper bound of the random number to be generated.
		///   <paramref name="maxValue" /> must be greater than or equal to 0.
		/// </param>
		/// <returns>
		///   A 32-bit signed integer that is greater than or equal to 0, and less than
		///   <paramref name="maxValue" />; that is, the range of return values ordinarily includes 0
		///   but not <paramref name="maxValue" />. However if <paramref name="maxValue" /> equals 0,
		///   <paramref name="maxValue" /> is returned.
		/// </returns>
		/// <seealso cref="Random.Next(int)" />
		/// <include file="Docs/RevisionHistory.xml" path="Revisions/RandomNumber[@id=&quot;NextMaxValue&quot;]/revisionHistory" />
		/// <exception cref="ArgumentOutOfRangeException">
		///   <paramref name="maxValue" /> is less than 0.
		/// </exception>
		public static int Next(int maxValue)
		{
			lock (LockObject)
			{
				return random.Next(maxValue);
			}
		}

		/// <summary>
		///   Returns a random integer that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">
		///   The exclusive upper bound of the random number returned. <paramref name="maxValue" />
		///   must be greater than or equal to <paramref name="minValue" />.
		/// </param>
		/// <returns>
		///   A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less
		///   than <paramref name="maxValue" />; that is, the range of return values includes
		///   <paramref name="minValue" /> but not <paramref name="maxValue" />. If
		///   <paramref name="minValue" /> equals <paramref name="maxValue" />,
		///   <paramref name="minValue" /> is returned.
		/// </returns>
		/// <seealso cref="Random.Next(int,int)" />
		/// <include file="Docs/RevisionHistory.xml" path="Revisions/RandomNumber[@id=&quot;NextMinValueMaxValue&quot;]/revisionHistory" />
		public static int Next(int minValue, int maxValue)
		{
			lock (LockObject)
			{
				return random.Next(minValue, maxValue);
			}
		}

		/// <summary>
		///   Returns a random integer that is within a specified range.
		/// </summary>
		/// <param name="min">The inclusive lower bound of the random number returned.</param>
		/// <param name="max">
		///   The exclusive upper bound of the random number returned. <paramref name="max" /> must
		///   be greater than or equal to <paramref name="min" />.
		/// </param>
		/// <returns>
		///   A 64-bit signed integer greater than or equal to <paramref name="min" /> and less than
		///   <paramref name="max" />; that is, the range of return values includes
		///   <paramref name="min" /> but not <paramref name="max" />. If <paramref name="min" />
		///   equals <paramref name="max" />, <paramref name="min" /> is returned.
		/// </returns>
		/// <seealso cref="Next(int, int)" />
		public static long Next(long min, long max)
		{
			var buf = new byte[8];
			NextBytes(buf);
			var longRand = BitConverter.ToInt64(buf, 0);

			return Math.Abs(longRand % (max - min)) + min;
		}

		/// <summary>
		///   Returns a random floating-point number that is greater than or equal to 0.0, and less
		///   than 1.0.
		/// </summary>
		/// <returns>
		///   A double-precision floating point number that is greater than or equal to 0.0, and less
		///   than 1.0.
		/// </returns>
		/// <seealso cref="Random.NextDouble()" />
		public static double NextDouble()
		{
			lock (LockObject)
			{
				return random.NextDouble();
			}
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Random" /> class, using the specified seed value.
		/// </summary>
		/// <param name="Seed">
		///   A number used to calculate a starting value for the pseudo-random number sequence. If a
		///   negative number is specified, the absolute value of the number is used.
		/// </param>
		/// <seealso cref="Random.Random(int)" />
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
