using System;

#pragma warning disable 0618 // Type or member is obsolete
namespace Faker
{
    /// <summary>
    ///   What kind of image will be generated.
    /// </summary>
    [Obsolete("MESSAGE. Please use 'RoboHashImageFormat' instead. Will be removed in version 3.0.")]
    public enum ImageFormat
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
        bmp
    }

    /// <summary>
    ///   A collection of Avatar related resources.
    /// </summary>
    /// <threadsafety static="true" />
    /// <seealso cref="RoboHash" />
    /// <seealso cref="FlatHash" />
    [Obsolete("MESSAGE. Please use 'RoboHash' class instead. Will be removed in version 3.0.")]
    public static class Avatar
    {
        /// <summary>
        ///   Gets a random RoboHash.org image URL.
        /// </summary>
        /// <param name="slug">The optional slug to set instead of generating a new random slug.</param>
        /// <param name="size">The image size</param>
        /// <param name="format">The image format</param>
        /// <param name="set">The image set to use in url generation</param>
        /// <returns>The randomly generated Robo hash image URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="set" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">Size should be specified in format 300x300</exception>
        /// <seealso cref="RoboHash.Image(string, string, RoboHashImageFormat, string)" />
        /// <seealso cref="FlatHash.Image(string, FlatHashImageFormat)" />
        public static string Image(
            string slug = null,
            string size = "300x300",
            ImageFormat format = ImageFormat.png,
            string set = "set1")
        {
            RoboHashImageFormat roboFormat = RoboHashImageFormat.png;
            switch (format)
            {
                case ImageFormat.png:
                    roboFormat = RoboHashImageFormat.png;
                    break;

                case ImageFormat.jpg:
                    roboFormat = RoboHashImageFormat.jpg;
                    break;

                case ImageFormat.bmp:
                    roboFormat = RoboHashImageFormat.bmp;
                    break;
            }

            return RoboHash.Image(slug, size, roboFormat, set);
        }
    }
}
#pragma warning restore 0618 // Type or member is obsolete
