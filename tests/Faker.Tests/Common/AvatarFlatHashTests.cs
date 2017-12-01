using NUnit.Framework;

namespace Faker.Tests.Common
{
    public class AvatarFlatHashTests
    {
        private const string IMAGE_FORMAT = @"{0}\.{1}$";
        private const string URL_STARTS_WITH = "http://flathash.com/";

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_Custom_Format()
        {
            string avatar = FlatHash.Image(format: FlatHashImageFormat.jpg);

            string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "jpg");

            Assert.That(avatar,
                        Does.StartWith(URL_STARTS_WITH)
                          .And.Match(expectedFormat));
        }

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_Custom_Slug()
        {
            string avatar = FlatHash.Image("YOOOOOOOO");

            string expectedFormat = string.Format(IMAGE_FORMAT, "YOOOOOOOO", "png");

            Assert.That(avatar,
                        Does.StartWith(URL_STARTS_WITH)
                          .And.Match(expectedFormat));
        }

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_Default_Values()
        {
            string avatar = FlatHash.Image();

            string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "png");

            Assert.That(avatar,
                        Does.StartWith(URL_STARTS_WITH)
                          .And.Match(expectedFormat));
        }

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_HTTPS_When_UseSSL_Is_True()
        {
            string avatar = FlatHash.Image(true);

            Assert.That(avatar,
                Does.StartWith("https:"));
        }

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_HTTP_When_UseSSL_Is_False()
        {
            string avatar = FlatHash.Image(false);

            Assert.That(avatar,
                Does.StartWith("http:"));
        }
    }
}
