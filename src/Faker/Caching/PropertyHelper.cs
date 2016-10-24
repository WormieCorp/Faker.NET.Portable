using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Faker.Caching
{
	/// <summary>
	///   This class helps to shorten code to return method PropertyInfo.
	/// </summary>
	/// <remarks>
	///   The code is inspired by <see href="http://stackoverflow.com/a/491486/1045789" /> and edited
	///   to address the static class with static method case.
	/// </remarks>
	internal static class PropertyHelper
	{
		/// <summary>
		///   Extracts the Property Information for the member selector
		/// </summary>
		/// <typeparam name="TValue">Type returned from the selector</typeparam>
		/// <param name="selector">The lambda expression to extract property from</param>
		/// <returns>The property information</returns>
		public static PropertyInfo GetProperty<TValue>(
			Expression<Func<TValue>> selector)
		{
			Expression body = selector;
			if (body is LambdaExpression)
			{
				body = ((LambdaExpression)body).Body;
			}

			switch (body.NodeType)
			{
				case ExpressionType.MemberAccess:
					return (PropertyInfo)((MemberExpression)body).Member;

				default:
					throw new InvalidOperationException();
			}
		}
	}
}
