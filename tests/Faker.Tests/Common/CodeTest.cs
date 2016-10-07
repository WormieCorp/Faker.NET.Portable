using NUnit.Framework;
using System;

namespace Faker.Tests.Common
{
    [TestFixture]
    public class CodeTests
    {
        [Test]
        [Repeat(1000)]
        public void Should_generate_ISBN10_10_chars_long()
        {
            var result = Code.ISBN10();
            Assert.That(result.Length, Is.EqualTo(10));
        }

        [Test]
        [Repeat(1000)]
        public void Should_generate_ISBN10_as_string_of_numbers_and_a_checksum()
        {
            var result = Code.ISBN10();
            Assert.That(result, Is.StringMatching(@"[0-9]{9}[0-9,X]"));
        }

        [Test]
        [Repeat(100)]
        public void Should_generate_ISBN10_with_valid_checksum()
        {
            var result = Code.ISBN10();
            Assert.IsTrue(isIsbn10ChecksumValid(result));
        }

        /// <summary>
        /// Computes checksum validity on a ISBN10
        /// </summary>
        /// <param name="isbn">The ISBN to be checked</param>
        /// <returns>Checksum is valid</returns>
        /// <see cref="http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js"/>
        private bool isIsbn10ChecksumValid(string isbn)
        {
            var v = new int[10];
            for (int i = 0; i < 9; i++)
                v[i] = Convert.ToInt32(isbn[i].ToString());

            v[9] = (isbn[9] == 'X' ? 10 : Convert.ToInt32(isbn[9].ToString()));

            var sum = 0;
            for (var i = 0; i < 10; i++)
            {
                sum += (10 - i) * v[i];
            }

            return (sum % 11) == 0;
        }
    }
}