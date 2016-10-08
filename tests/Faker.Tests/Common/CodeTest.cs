using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class CodeTests
	{
		#region ISBN10 tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_ISBN10_with_invalid_checksum()
		{
			var result = Code.ISBN10(false);
			Assert.IsFalse(isIsbn10ChecksumValid(result), result + " has valid checksum");
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN10_10_chars_long()
		{
			var result = Code.ISBN10();
			Assert.That(result.Length, Is.EqualTo(10));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN10_as_string_of_numbers_and_a_checksum()
		{
			var result = Code.ISBN10();
			Assert.That(result, Does.Match(@"[0-9]{9}[0-9,X]"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN10_with_valid_checksum()
		{
			var result = Code.ISBN10();
			Assert.IsTrue(isIsbn10ChecksumValid(result));
		}

		#endregion ISBN10 tests

		#region ISBN13 tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_ISBN13_with_invalid_checksum()
		{
			var result = Code.ISBN13(false);
			Assert.IsFalse(isEanChecksumValid(result), result.ToString() + " has valid checksum");
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN13_13_chars_long()
		{
			var result = Code.ISBN13();
			Assert.That(result.Length, Is.EqualTo(13), result + " is not 13 chars long");
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN13_as_string_of_numbers_and_a_checksum()
		{
			var result = Code.ISBN13();
			Assert.That(result, Does.Match(@"[0-9]{13}"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN13_starting_with_978_or_979()
		{
			var result = Code.ISBN13();
			Assert.That(result, Does.Match(@"^97[89]"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_ISBN13_with_valid_checksum()
		{
			var result = Code.ISBN13();
			Assert.IsTrue(isEanChecksumValid(result));
		}

		#endregion ISBN13 tests

		#region EAN tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_EAN_with_invalid_checksum()
		{
			var result = Code.EAN(false);
			Assert.IsFalse(isEanChecksumValid(result), result.ToString() + " has valid checksum");
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_EAN_13_chars_long()
		{
			var result = Code.EAN();
			Assert.That(result.Length, Is.EqualTo(13), result + " is not 13 chars long");
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_EAN_as_string_of_numbers_and_a_checksum()
		{
			var result = Code.EAN();
			Assert.That(result, Does.Match(@"[0-9]{13}"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_EAN_with_valid_checksum()
		{
			var result = Code.EAN();
			Assert.IsTrue(isEanChecksumValid(result));
		}

		#endregion EAN tests

		/// <summary>
		/// Computes checksum validity on a EAN
		/// </summary>
		/// <param name="ean">The EAN to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>
		///     Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js
		/// </remarks>
		private bool isEanChecksumValid(string ean)
		{
			var v = new int[13];
			for (int i = 0; i < 13; i++)
				v[i] = (byte)ean[i] - (byte)'0';

			var sum = 0;
			for (int i = 0; i < 13; i += 2)
				sum += v[i];
			for (int i = 1; i < 13; i += 2)
				sum += 3 * v[i];

			return sum % 10 == 0;
		}

		/// <summary>
		/// Computes checksum validity on a ISBN10
		/// </summary>
		/// <param name="isbn">The ISBN to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>
		///     Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js
		/// </remarks>
		private bool isIsbn10ChecksumValid(string isbn)
		{
			var v = new int[10];
			for (int i = 0; i < 9; i++)
				v[i] = (byte)isbn[i] - (byte)'0';

			v[9] = (isbn[9] == 'X' ? 10 : (byte)isbn[9] - (byte)'0');

			var sum = 0;
			for (var i = 0; i < 10; i++)
				sum += (10 - i) * v[i];

			return (sum % 11) == 0;
		}
	}
}
