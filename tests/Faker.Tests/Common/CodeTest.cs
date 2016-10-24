using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests.Common
{
	public class CodeTests
	{
		#region Fiscal Code tests

		[Test]
		[Repeat(1000)]
		public void Should_encode_date()
		{
			char[] monthChars = { 'A', 'B', 'C', 'D', 'E', 'H', 'L', 'M', 'P', 'R', 'S', 'T' };

			var date = Date.Birthday();
			var fiscalCode = Code.FiscalCode("Macchi", "Michi", date, true);
			var regex = string.Format("^.{{6}}{0}{1}({2}|{3})",
				(date.Year % 100).ToString("00", CultureInfo.InvariantCulture),
				monthChars[date.Month - 1],
				date.Day.ToString("00", CultureInfo.InvariantCulture),
				(date.Day + 40).ToString("00", CultureInfo.InvariantCulture));
			Assert.That(fiscalCode, Does.Match(regex));
		}

		[Test]
		public void Should_fiscalCode_be_case_insensitive()
		{
			var fiscalCode1 = Code.FiscalCode("GRACCHI", "ANTONIO", System.DateTime.Today, true);
			var fiscalCode2 = Code.FiscalCode("gracchi", "antonio", System.DateTime.Today, true);
			Assert.That(fiscalCode1.Substring(0, 6), Is.EqualTo(fiscalCode2.Substring(0, 6)));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_FiscalCode_with_invalid_checksum()
		{
			var fiscalCode = Code.FiscalCode(false);
			Assert.That(IsFiscalCodeOk(fiscalCode), Is.EqualTo(FiscalCodeValidationResult.ChecksumError));
		}

		[Test]
		public void Should_generate_invalid_FiscalCode_with_less_than_16_chars()
		{
			var fiscalCode = "MRRTRR55E12A343";
			Assert.That(IsFiscalCodeOk(fiscalCode), Is.EqualTo(FiscalCodeValidationResult.FormallyInvalid));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_FiscalCode_with_valid_checksum()
		{
			var fiscalCode = Code.FiscalCode();
			Assert.That(IsFiscalCodeOk(fiscalCode), Is.EqualTo(FiscalCodeValidationResult.Ok));
		}

		[Test]
		public void Should_squeeze_double_names()
		{
			var fiscalCode = Code.FiscalCode("Dado Amico", "Rino Andrea", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("DDMRND"));
		}

		[Test]
		public void Should_squeeze_more_than_three_consonants_in_firstname()
		{
			//the special does apply to firstnames
			var fiscalCode = Code.FiscalCode("Masi", "Annamaria", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.Match("^.{3}NMR"));
		}

		[Test]
		public void Should_squeeze_more_than_three_consonants_in_lastname()
		{
			//the special case does not apply to lastnames
			var fiscalCode = Code.FiscalCode("Astratto", "Michela", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("STR"));
		}

		[Test]
		public void Should_squeeze_names_starting_with_vowel()
		{
			var fiscalCode = Code.FiscalCode("AIRONE", "EULA", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("RNALEU"));
		}

		[Test]
		public void Should_squeeze_names_with_apostrophe()
		{
			var fiscalCode = Code.FiscalCode("D'Urzo", "Morena", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("DRZMRN"));
		}

		[Test]
		public void Should_squeeze_names_with_symbols()
		{
			var fiscalCode = Code.FiscalCode("D'Ama-Deidda", "Pasquale", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("DMDPQL"));
		}

		[Test]
		public void Should_squeeze_one_consonant_in_names()
		{
			var fiscalCode = Code.FiscalCode("Aria", "Maia", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("RAIMAI"));
		}

		[Test]
		public void Should_squeeze_three_consonants_in_names()
		{
			//special case does not apply with 3 consonants in firstname
			var fiscalCode = Code.FiscalCode("Macchi", "Michi", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("MCCMCH"));
		}

		[Test]
		public void Should_squeeze_two_consonants_in_names()
		{
			var fiscalCode = Code.FiscalCode("Masi", "Nico", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("MSANCI"));
		}

		[Test]
		public void Should_squeeze_two_letter_names()
		{
			var fiscalCode = Code.FiscalCode("Ro", "Ka", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("ROXKAX"));
		}

		[Test]
		public void Should_squeeze_uppercase_names()
		{
			var fiscalCode = Code.FiscalCode("MORI", "ANTONIO", System.DateTime.Today, true);
			Assert.That(fiscalCode, Does.StartWith("MRONTN"));
		}

		[Test]
		public void Should_take_chkValidity_with_lastName_firstName_birthday_parameters()
		{
			var fiscalCode = Code.FiscalCode("AAA", "BBB", System.DateTime.Today, false);
			Assert.That(IsFiscalCodeOk(fiscalCode), Is.EqualTo(FiscalCodeValidationResult.ChecksumError));
		}

		[Test]
		[Repeat(1000)]
		public void Should_take_chkValidity_with_minAge_maxAge_parameters()
		{
			var fiscalCode = Code.FiscalCode(false, 0, 0);
			Assert.That(IsFiscalCodeOk(fiscalCode), Is.EqualTo(FiscalCodeValidationResult.ChecksumError));
		}

		[Test]
		[Repeat(1000)]
		public void Should_take_minAge_maxAge_parameters()
		{
			var fiscalCode = Code.FiscalCode(0, 0);
			var regex = "^.{6}" + (System.DateTime.Today.Year % 100).ToString("00", CultureInfo.InvariantCulture);
			Assert.That(fiscalCode, Does.Match(regex));
		}

		[Test]
		[Repeat(1000)]
		public void Should_town_code_part_be_a_valid_one()
		{
			var fiscalCode = Code.FiscalCode();
			var trailingPart = fiscalCode.Substring(11, 4);
			var townCodes = Resources.FiscalCode_TownCodes.Codes.Split(Config.SEPARATOR);
			Assert.That(townCodes, Does.Contain(trailingPart));
		}

		#endregion Fiscal Code tests

		#region ISBN10 tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_ISBN10_with_invalid_checksum()
		{
			var result = Code.ISBN10(false);
			Assert.IsFalse(IsIsbn10ChecksumValid(result), result + " has valid checksum");
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
			Assert.IsTrue(IsIsbn10ChecksumValid(result));
		}

		#endregion ISBN10 tests

		#region ISBN13 tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_ISBN13_with_invalid_checksum()
		{
			var result = Code.ISBN13(false);
			Assert.IsFalse(IsEanChecksumValid(result), result.ToString() + " has valid checksum");
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
			Assert.IsTrue(IsEanChecksumValid(result));
		}

		#endregion ISBN13 tests

		#region EAN tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_EAN_with_invalid_checksum()
		{
			var result = Code.EAN(false);
			Assert.IsFalse(IsEanChecksumValid(result), result.ToString() + " has valid checksum");
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
			Assert.IsTrue(IsEanChecksumValid(result));
		}

		#endregion EAN tests

		#region RUT tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_invalid_RUT_with_invalid_checksum()
		{
			var result = Code.RUT(false);
			Assert.IsFalse(IsRutOk(result), result + " has valid checksum");
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_RUT_as_string_of_8_numbers_and_a_checksum()
		{
			var result = Code.RUT();
			Assert.That(result, Does.Match(@"[0-9]{8}[0-9,K]"));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_RUT_no_longer_than_9_chars()
		{
			var result = Code.RUT();
			Assert.That(result.Length, Is.LessThanOrEqualTo(9));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_RUT_with_valid_checksum()
		{
			var result = Code.RUT();
			Assert.IsTrue(IsRutOk(result), "RUT {0} has invalid checksum", result);
		}

		#endregion RUT tests

		#region NPI tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_NPI_as_10_digits_number()
		{
			var result = Code.NPI();
			Assert.That(result, Does.Match(@"[0-9]{10}"));
		}

		#endregion NPI tests

		#region NRIC tests

		[Test]
		[Repeat(1000)]
		public void Should_generate_NRIC_with_invalid_checksum()
		{
			var result = Code.NRIC(false, 10, 40);
			Assert.IsFalse(IsNricOk(result));
		}

		[Test]
		[Repeat(1000)]
		public void Should_generate_valid_NRIC_with_valid_checksum()
		{
			var result = Code.NRIC();
			Assert.IsTrue(IsNricOk(result));
		}

		#endregion NRIC tests

		internal enum FiscalCodeValidationResult { Ok, FormallyInvalid, ChecksumError };

		/// <summary>
		///   Computes checksum validity on a EAN
		/// </summary>
		/// <param name="ean">The EAN to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js</remarks>
		private bool IsEanChecksumValid(string ean)
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
		///   Computes checksum validity on a Fiscal Code
		/// </summary>
		/// <param name="ean">The Fiscal Code to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>Checksum routines are at https://en.wikipedia.org/wiki/Italian_fiscal_code_card</remarks>
		private FiscalCodeValidationResult IsFiscalCodeOk(string fiscalCode)
		{
			if (!Regex.IsMatch(fiscalCode, @"^[A-Z]{6}[0-9]{2}[A-Z][0-9]{2}[A-Z][0-9]{3}[A-Z]$"))
				return FiscalCodeValidationResult.FormallyInvalid;

			#region static maps

			var oddMap = new Dictionary<char, int>() {
				{ '0', 1 },
				{ '1', 0 },
				{ '2', 5 },
				{ '3', 7 },
				{ '4', 9 },
				{ '5', 13 },
				{ '6', 15 },
				{ '7', 17 },
				{ '8', 19 },
				{ '9', 21 },
				{ 'A', 1 },
				{ 'B', 0 },
				{ 'C', 5 },
				{ 'D', 7 },
				{ 'E', 9 },
				{ 'F', 13 },
				{ 'G', 15 },
				{ 'H', 17 },
				{ 'I', 19 },
				{ 'J', 21 },
				{ 'K', 2 },
				{ 'L', 4 },
				{ 'M', 18 },
				{ 'N', 20 },
				{ 'O', 11 },
				{ 'P', 3 },
				{ 'Q', 6 },
				{ 'R', 8 },
				{ 'S', 12 },
				{ 'T', 14 },
				{ 'U', 16 },
				{ 'V', 10 },
				{ 'W', 22 },
				{ 'X', 25 },
				{ 'Y', 24 },
				{ 'Z', 23 }
			};

			var evenMap = new Dictionary<char, int>() {
				{ '0', 0 },
				{ '1', 1 },
				{ '2', 2 },
				{ '3', 3 },
				{ '4', 4 },
				{ '5', 5 },
				{ '6', 6 },
				{ '7', 7 },
				{ '8', 8 },
				{ '9', 9 },
				{ 'A', 0 },
				{ 'B', 1 },
				{ 'C', 2 },
				{ 'D', 3 },
				{ 'E', 4 },
				{ 'F', 5 },
				{ 'G', 6 },
				{ 'H', 7 },
				{ 'I', 8 },
				{ 'J', 9 },
				{ 'K', 10 },
				{ 'L', 11 },
				{ 'M', 12 },
				{ 'N', 13 },
				{ 'O', 14 },
				{ 'P', 15 },
				{ 'Q', 16 },
				{ 'R', 17 },
				{ 'S', 18 },
				{ 'T', 19 },
				{ 'U', 20 },
				{ 'V', 21 },
				{ 'W', 22 },
				{ 'X', 23 },
				{ 'Y', 24 },
				{ 'Z', 25 }
			};

			#endregion static maps

			int total = 0;
			for (int i = 0; i < 15; i += 2)
				total += oddMap[fiscalCode[i]];
			for (int i = 1; i < 15; i += 2)
				total += evenMap[fiscalCode[i]];

			if (fiscalCode[15] != (char)('A' + total % 26))
				return FiscalCodeValidationResult.ChecksumError;

			return FiscalCodeValidationResult.Ok;
		}

		/// <summary>
		///   Computes checksum validity on a ISBN10
		/// </summary>
		/// <param name="isbn">The ISBN to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js</remarks>
		private bool IsIsbn10ChecksumValid(string isbn)
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

		/// <summary>
		///   Computes checksum validity on a NRIC
		/// </summary>
		/// <param name="nric">The NRIC to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>Checksum routines are at https://github.com/stympy/faker/blob/master/lib/faker/code.rb#L32</remarks>
		private bool IsNricOk(string nric)
		{
			if (!Regex.IsMatch(nric, @"[ST][0-9]{7}[A-Z]"))
				return false;

			int[] weights = { 2, 7, 6, 5, 4, 3, 2 };
			var total = 0;
			for (int i = 0; i < 7; i++)
				total += (nric[i + 1] - '0') * weights[i];
			if (nric[0] == 'T')
				total += 4;

			char[] checksumChars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'Z', 'J' };

			return checksumChars[10 - total % 11] == nric[8];
		}

		/// <summary>
		///   Computes checksum validity on a RUT
		/// </summary>
		/// <param name="isbn">The RUT to be checked</param>
		/// <returns>Checksum is valid</returns>
		/// <remarks>Checksum routines are at http://www.vesic.org/english/blog-eng/net/verifying-chilean-rut-code-tax-number/</remarks>
		private bool IsRutOk(string rut)
		{
			if (!Regex.IsMatch(rut, @"[0-9]{8}[0-9,K]"))
				return false;

			int total = 0;
			int[] coefs = { 3, 2, 7, 6, 5, 4, 3, 2 };

			for (int i = 0; i < 8; i++)
				total += coefs[i] * (rut[i] - '0');

			int rest = (11 - (total % 11)) % 11;

			if ((rest == 10) && rut.EndsWith("K"))
				return true;

			if (rut[rut.Length - 1] - '0' == rest)
				return true;

			return false;
		}
	}
}
