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
		/// <seealso cref="Image(bool,string,FlatHashImageFormat)" />
        public static string Image(string slug = null, FlatHashImageFormat format = FlatHashImageFormat.png)
        {
            return Image(false, slug: slug, format: format);
        }

        /// <summary>
        ///   Gets a random FlatHash.com image URL.
        /// </summary>
        /// <param name="useSsl">Set to <c>true</c> if https should be used, otherwise <c>false</c></param>
        /// <param name="slug">The optional slug to use instead of generating a new random slug.</param>
        /// <param name="format">The image format to use.</param>
        /// <returns>The random image URL.</returns>
        /// <seealso cref="RoboHash.Image(string, string, RoboHashImageFormat, string)" />
        /// <seealso cref="Image(string,FlatHashImageFormat)" />
        public static string Image(bool useSsl, string slug = null, FlatHashImageFormat format = FlatHashImageFormat.png)
        {
            slug = slug ?? string.Join(string.Empty, Lorem.Words(3));

            return "{0}://flathash.com/{1}.{2}".FormatCulture(useSsl ? "https" : "http", slug, format);
        }
    }
}
