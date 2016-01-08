using System;
using Faker;
using NUnit.Framework;

namespace Faker.Tests.Common
{
    [TestFixture]
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
                        Is.StringStarting(URL_STARTS_WITH)
                          .And.StringMatching(expectedFormat));
        }

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_Custom_Slug()
        {
            string avatar = FlatHash.Image("YOOOOOOOO");

            string expectedFormat = string.Format(IMAGE_FORMAT, "YOOOOOOOO", "png");

            Assert.That(avatar,
                        Is.StringStarting(URL_STARTS_WITH)
                          .And.StringMatching(expectedFormat));
        }

        [Test]
        public static void Should_Get_Avatar_FlatHash_Image_With_Default_Values()
        {
            string avatar = FlatHash.Image();

            string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "png");

            Assert.That(avatar,
                        Is.StringStarting(URL_STARTS_WITH)
                          .And.StringMatching(expectedFormat));
        }
    }
}
