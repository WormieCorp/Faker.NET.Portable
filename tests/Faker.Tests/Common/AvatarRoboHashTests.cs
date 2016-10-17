using System;
using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class AvatarRoboHashTests
	{
		private const string IMAGE_FORMAT = @"{0}\.{1}\?size={2}&set={3}$";
		private const string URL_STARTS_WITH = "http://robohash.org/";

		[Test]
		public static void Should_Get_Avatar_RoboHash_Image_With_Custom_Format()
		{
			string avatar = RoboHash.Image(format: RoboHashImageFormat.jpg);

			string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "jpg", "300x300", "set1");

			Assert.That(avatar,
						Does.StartWith(URL_STARTS_WITH)
						  .And.Match(expectedFormat));
		}

		[Test]
		public static void Should_Get_Avatar_RoboHash_Image_With_Custom_Set()
		{
			string avatar = RoboHash.Image(set: "MySet");

			string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "png", "300x300", "MySet");

			Assert.That(avatar,
						Does.StartWith(URL_STARTS_WITH)
						  .And.Match(expectedFormat));
		}

		[Test]
		public static void Should_Get_Avatar_RoboHash_Image_With_Custom_Size()
		{
			string avatar = RoboHash.Image(size: "200x140");

			string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "png", "200x140", "set1");

			Assert.That(avatar,
						Does.StartWith(URL_STARTS_WITH)
						  .And.Match(expectedFormat));
		}

		[Test]
		public static void Should_Get_Avatar_RoboHash_Image_With_Custom_Slug()
		{
			string avatar = RoboHash.Image("YOOOOOOOO");

			string expectedFormat = string.Format(IMAGE_FORMAT, "YOOOOOOOO", "png", "300x300", "set1");

			Assert.That(avatar,
						Does.StartWith(URL_STARTS_WITH)
						  .And.Match(expectedFormat));
		}

		[Test]
		public static void Should_Get_Avatar_RoboHash_Image_With_Default_Values()
		{
			string avatar = RoboHash.Image();

			string expectedFormat = string.Format(IMAGE_FORMAT, "[a-z]+", "png", "300x300", "set1");

			Assert.That(avatar,
						Does.StartWith(URL_STARTS_WITH)
						  .And.Match(expectedFormat));
		}

		[Test]
		public static void Should_Throw_Argument_Exception_If_Avatar_RoboHash_Size_Not_Valid()
		{
			var ex = Assert.Throws<ArgumentException>(() => RoboHash.Image(size: "NotValid"));
			Assert.That(ex.Message, Does.StartWith("Size should be specified in format 300x300"));
			Assert.That(ex.ParamName, Is.EqualTo("size"));
		}

		[Test]
		public static void Should_Throw_Argument_Null_Exception_If_Avatar_RoboHash_Set_Is_Null()
		{
			var ex = Assert.Throws<ArgumentNullException>(() => RoboHash.Image(set: null));
			Assert.That(ex.ParamName, Is.EqualTo("set"));
		}
	}
}
