﻿using System;
using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   A collection of Color related resources.
	/// </summary>
	/// <threadsafety static="true" />
	public static class Color
	{
		/// <summary>
		///   Generates Hex Color
		/// </summary>
		/// <returns>The generated hex color</returns>
		public static string HexColor()
		{
			var hexColor = "#{0:X6}".FormatCulture(RandomNumber.Next(0x1000000));

			return hexColor;
		}

		/// <summary>
		///   Generates a byte array of RGB values.
		/// </summary>
		/// <returns>a byte array of RGB values.</returns>
		/// <remarks>The generated byte array is an array of a fixed length of 3.</remarks>
		public static byte[] RGB()
		{
			var result = new byte[3];
			for (var i = 0; i < result.Length; i++)
			{
				result[i] = SingleRGB();
			}

			return result;
		}

		/// <summary>
		///   Generates a single rgb value.
		/// </summary>
		/// <returns>A single rgb value</returns>
		public static byte SingleRGB()
		{
			return (byte)RandomNumber.Next(256);
		}

		/// <summary>
		///   Generates a single hsl value. (Random <see cref="double" /> between 0 and 360.
		/// </summary>
		/// <returns>A single hsl value</returns>
		public static double SingleHSL()
		{
			var value = RandomNumber.NextDouble() * 360.00;

			return Math.Round(value, 2);
		}

		/// <summary>
		///   Generates an array of HSL values.
		/// </summary>
		/// <returns>An array of HSL values.</returns>
		/// <remarks>The generated <see cref="double" /> is an array of fixed length of 3.</remarks>
		public static double[] HSL()
		{
			var values = new double[3];

			for (var i = 0; i < values.Length; i++)
			{
				values[i] = SingleHSL();
			}

			return values;
		}

		/// <summary>
		///   Generates an array of HSL values with alpha channel.
		/// </summary>
		/// <returns>An array of HSL values with alpha channel.</returns>
		/// <remarks>
		///   <para>The generated <see cref="double" /> is an array of fixed length of 4.</para>
		///   <para>The first 3 items is the HSL values.</para>
		///   <para>The last item is the alpha channel.</para>
		/// </remarks>
		public static double[] HSLA()
		{
			var values = new double[4];

			for (var i = 0; i < 3; i++)
			{
				values[i] = SingleHSL();
			}

			values[3] = AlphaChannel();

			return values;
		}

		/// <summary>
		///   Generates a random alpha value. (Random <see cref="double" /> between 0 and 1).
		/// </summary>
		/// <returns>A random alpha value.</returns>
		public static double AlphaChannel()
		{
			return RandomNumber.NextDouble();
		}
	}
}
