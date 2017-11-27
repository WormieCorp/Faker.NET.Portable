using System;

namespace Faker
{
	/// <summary>
	///   A collection of Boolean related resources.
	/// </summary>
	/// <threadsafety static="true" />
	public static class Boolean
	{
		/// <summary>
		///   Generates a random Boolean value
		/// </summary>
		/// <returns></returns>
		public static bool Next()
		{
			return RandomNumber.Next(0, 2) == 0;
		}
	}
}
