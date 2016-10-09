using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///     What kind of image will be generated.
	/// </summary>
	/// <remarks>When version 3.0 hits, this will be in Faker.Avatar namespace.</remarks>
	public enum FlatHashImageFormat
	{
		// ReSharper disable InconsistentNaming
		/// <summary>
		///     A PNG image
		/// </summary>
		png,

		/// <summary>
		///     A JPG image
		/// </summary>
		jpg,

		/// <summary>
		///     A BMP image
		/// </summary>
		bmp,

		/// <summary>
		///     A GIF image
		/// </summary>
		gif

		// ReSharper restore InconsistentNaming
	}

	/// <summary>
	///     A collection of FlatHash.com avatar related resources.
	/// </summary>
	/// <threadsafety static="true" />
	/// <remarks>When version 3.0 hits, this will be in Faker.Avatar namespace.</remarks>
	public static class FlatHash
	{
		/// <summary>
		///     Gets a random RoboHash.com image URL.
		/// </summary>
		/// <returns>The random image URL.</returns>
		public static string Image(string slug = null, FlatHashImageFormat format = FlatHashImageFormat.png)
		{
			slug = slug ?? string.Join(string.Empty, Lorem.Words(3));

			return "http://flathash.com/{0}.{1}".FormatCulture(slug, format);
		}
	}
}
