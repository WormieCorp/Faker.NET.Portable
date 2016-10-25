using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Faker.Caching;
using JetBrains.Annotations;

namespace Faker.Extensions
{
	/// <summary>
	///   A collection of string helper extensions.
	/// </summary>
	/// <threadsafety static="true" />
	public static class StringExtensions
	{
		private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
		private static readonly object DictionaryLock = new object();
		private static readonly IDictionary<string, Func<string>> ValidVariables;

		static StringExtensions()
		{
			lock (DictionaryLock)
			{
				ValidVariables = new Dictionary<string, Func<string>>();

				AddVariables(typeof(Address), ValidVariables);
				AddVariables(typeof(App), ValidVariables);
#pragma warning disable CS0618 // Type or member is obsolete
				AddVariables(typeof(Avatar), ValidVariables);  // Will be removed in v.3.0
#pragma warning restore CS0618 // Type or member is obsolete
				AddVariables(typeof(RoboHash), ValidVariables);
				AddVariables(typeof(FlatHash), ValidVariables);
				AddVariables(typeof(Business), ValidVariables);
				AddVariables(typeof(Company), ValidVariables);
				AddVariables(typeof(Internet), ValidVariables);
				AddVariables(typeof(Lorem), ValidVariables);
				AddVariables(typeof(Name), ValidVariables);
				AddVariables(typeof(Phone), ValidVariables);
				ValidVariables.Add("StreetRoot", () => ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StreetRoot)).Random());
				ValidVariables.Add("CityRoot", () => ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.CityRoot)).Random());
				ValidVariables.Add("CommonStreetSuffix", () => ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.CommonStreetSuffixes)).Random());
				ValidVariables.Add("AreaCode", () => ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Phone.AreaCode)).Random());
				ValidVariables.Add("ExchangeCode", () => ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Phone.ExchangeCode)).Random());
				ValidVariables.Add("Address.StreetPrefix", () => ResourceCollectionCacher.GetArray(PropertyHelper.GetProperty(() => Resources.Address.StreetPrefix)).Random());
			}
		}

		/// <summary>
		///   Removes all characters which is not Alphanumeric from the specified <paramref name="s">source</paramref>
		/// </summary>
		/// <param name="s">The source string.</param>
		/// <returns>The transformed string.</returns>
		public static string AlphanumericOnly(this string s)
		{
			IEnumerable<char> result =
				RemoveAccent(s)
					.ToCharArray()
					.Where(x => (x >= 'A' && x <= 'Z') || (x >= 'a' && x <= 'z') || char.IsDigit(x));

			return new string(result.ToArray());
		}

		/// <summary>
		///   Capitalizes the first letter of the given string.
		/// </summary>
		/// <param name="s">The source string.</param>
		/// <returns>The source string with it's first letter capitalized.</returns>
		public static string Capitalise(this string s)
		{
			return Regex.Replace(s, "^[a-z]", x => x.Value.ToUpperInvariant());
		}

		/// <summary>
		///   Helper function for formatting a string with the current <see cref="CultureInfo" />
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		/// <returns>The formatted string.</returns>
		[NotNull]
		[Pure]
		[StringFormatMethod("format")]
		public static string FormatCulture([NotNull] this string format, [NotNull] params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		/// <summary>
		///   Helper function for formatting string with an invariant <see cref="CultureInfo" />.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		/// <returns>The formatted string.</returns>
		[NotNull]
		[Pure]
		[StringFormatMethod("format")]
		public static string FormatInvariant([NotNull] this string format, [NotNull] params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		/// <summary>
		///   Get a string with every '?' replaced with a random character from the english alphabet.
		/// </summary>
		/// <param name="s">The source format</param>
		/// <returns>The formatted string.</returns>
		public static string Letterify(this string s)
		{
			return new string(Transform(s, true, false, false).ToArray());
		}

		/// <summary>
		///   Get a string with every occurrence of '#' replaced with a random number.
		/// </summary>
		/// <param name="s">The source format.</param>
		/// <returns>The formatted string.</returns>
		public static string Numerify(this string s)
		{
			return Numerify(s, false);
		}

		/// <summary>
		///   Transforms the specified source and conditionally replaces variables.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <param name="replaceVariables">
		///   if set to <see langword="true" /> replace variables in the string.
		/// </param>
		/// <returns>The transformed string.</returns>
		public static string Transform(this string s, bool replaceVariables = false)
		{
			char[] currentChars = Transform(s, true, true, replaceVariables).ToArray();

			return new string(currentChars.ToArray());
		}

		/// <summary>
		///   Transform the <paramref name="s">string</paramref> replacing <c>#</c> placeholders into
		///   a number. Also replaces variables if <paramref name="replaceVariables" /> is set to <see langword="true" />.
		/// </summary>
		/// <param name="s">The string containing the format to numerify.</param>
		/// <param name="replaceVariables">if set to <see langword="true" /> also replace variables.</param>
		/// <returns>The numerified string.</returns>
		internal static string Numerify(this string s, bool replaceVariables)
		{
			return new string(Transform(s, false, true, replaceVariables).ToArray());
		}

		private static void AddVariables(Type classType, IDictionary<string, Func<string>> validVariables)
		{
			MethodInfo[] methods = classType.GetMethods(BindingFlags.Public | BindingFlags.Static);

			foreach (MethodInfo methodInfo in
				methods
					.Where(
						   methodInfo =>
						   methodInfo.ReturnType == typeof(string)
						   && !methodInfo.GetParameters().Any()))
			{
				validVariables.Add(
					classType.Name + "." + methodInfo.Name,
					() => (string)methodInfo.Invoke(null, null));
			}
		}

		private static string GetVariable(string chars, ref int index)
		{
			var substring = new StringBuilder();

			while (chars.Length >= index && chars[index] != '}')
			{
				substring.Append(chars[index++]);
			}

			return substring.ToString();
		}

		private static string GetVariableValue(string chars, ref int index)
		{
			string variable = GetVariable(chars, ref index);

			lock (DictionaryLock)
			{
				return ValidVariables.ContainsKey(variable) ? ValidVariables[variable].Invoke() : string.Empty;
			}
		}

		private static string RemoveAccent(string source)
		{
			byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(source);

			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}

		private static IEnumerable<char> Transform(string s, bool letterify, bool numerify, bool replaceVariables)
		{
			for (var index = 0; index < s.Length; index++)
			{
				char c = s[index];
				if (numerify && c == '#')
				{
					yield return RandomNumber.Next(0, 10).ToString()[0];
				}
				else if (letterify && c == '?')
				{
					yield return ALPHABET.Random();
				}
				else if (replaceVariables && c == '{' && char.IsLetter(s[++index]))
				{
					string value = GetVariableValue(s, ref index);
					foreach (char ch in value)
					{
						yield return ch;
					}
				}
				else
				{
					yield return c;
				}
			}
		}
	}
}
