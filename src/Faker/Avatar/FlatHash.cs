using Faker.Extensions;

namespace Faker
{
	/// <summary>
	///   What kind of image will be generated.
	/// </summary>
	/// <remarks>When version 3.0 hits, this will be in Faker.Avatar namespace.</remarks>
	public enum FlatHashImageFormat
	{
		/// <summary>
		///   A PNG image
		/// </summary>
		png,

		/// <summary>
		///   A JPG image
		/// </summary>
		jpg,

		/// <summary>
		///   A BMP image
		/// </summary>
		bmp,

		/// <summary>
		///   A GIF image
		/// </summary>
		gif
	}

	/// <summary>
	///   A collection of FlatHash.com avatar related resources.
	/// </summary>
	/// <threadsafety static="true" />
	/// <remarks>When version 3.0 hits, this will be in Faker.Avatar namespace.</remarks>
	/// <seealso cref="RoboHash" />
	public static class FlatHash
	{
		/// <summary>
		///   Gets a random FlatHash.com image URL.
		/// </summary>
		/// <param name="slug">The optional slug to use instead of generating a new random slug.</param>
		/// <param name="format">The image format to use.</param>
		/// <returns>The random image URL.</returns>
		/// <seealso cref="RoboHash.Image(string, string, RoboHashImageFormat, string)" />
		public static string Image(string slug = null, FlatHashImageFormat format = FlatHashImageFormat.png)
		{
			slug = slug ?? string.Join(string.Empty, Lorem.Words(3));

			return "http://flathash.com/{0}.{1}".FormatCulture(slug, format);
		}
	}
}
