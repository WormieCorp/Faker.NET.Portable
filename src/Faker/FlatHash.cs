using Faker.Extensions;

namespace Faker.Avatar
{
    /// <summary>
    ///     What kind of image will be generated.
    /// </summary>
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
    /// <include file='Docs/CustomRemarks.xml' path='Comments/SatelliteResource/*' />
    /// <threadsafety static="true" />
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
