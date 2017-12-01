using Faker.Caching;
using Faker.Extensions;

namespace Faker
{
    /// <summary>
    ///   A collection of App related resources.
    /// </summary>
    /// <remarks>
    ///   The resources are acquired by satellite assemblies matching the current thread's
    ///   <see cref="System.Globalization.CultureInfo" />.
    ///	  If no matching satellite  assembly is found, the english version is used.
    ///	</remarks>
    /// <threadsafety static="true" />
    public static class App
    {
        /// <summary>
        ///   Gets a random application author.
        /// </summary>
        /// <returns>The Author.</returns>
        public static string Author()
        {
            return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.App.Author)).Random().Transform(true);
        }

        /// <summary>
        ///   Gets the name of a random application.
        /// </summary>
        /// <returns>The name.</returns>
        public static string Name()
        {
            return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.App.Name)).Random();
        }

        /// <summary>
        ///   Gets a random application version.
        /// </summary>
        /// <returns>The version.</returns>
        public static string Version()
        {
            return ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.App.VersionFormat)).Random().Numerify();
        }
    }
}
