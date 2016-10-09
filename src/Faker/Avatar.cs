using System;

namespace Faker
{
	/// <summary>
	///    What kind of image will be generated.
	/// </summary>
	[Obsolete("MESSAGE. Please use 'RoboHashImageFormat' instead. Will be removed in version 3.0.")]
	public enum ImageFormat
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
		bmp

		// ReSharper restore InconsistentNaming
	}

	/// <summary>
	/// A collection of Avatar related resources.
	/// </summary>
	/// <threadsafety static="true" />
	[Obsolete("MESSAGE. Please use 'RoboHash' class instead. Will be removed in version 3.0.")]
	public static class Avatar
	{
		/// <summary>
		///    Gets a random image URL
		/// </summary>
		/// <exception cref="ArgumentNullException"><paramref name="set" /> is <see langword="null" />.</exception>
		// <exception cref="ArgumentException">Size should be specified in format 300x300</exception>
		public static string Image(string slug = null, string size = "300x300", ImageFormat format = ImageFormat.png,
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
