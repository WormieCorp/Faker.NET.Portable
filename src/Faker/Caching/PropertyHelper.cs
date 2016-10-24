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
