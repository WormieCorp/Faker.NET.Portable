using System;
using Faker.Extensions;

namespace Faker
{
    /// <summary>
    ///     A collection of Code related resources.
    /// </summary>
    /// <threadsafety static="true" />
    public static class Code
    {
        /// <summary>
        ///     Generates an ISBN-10 Code
        /// </summary>
        /// <param name="formallyValid">Indicates whether the generated ISBN must be formally valid or not</param>
        /// <returns>The generated ISBN-10</returns>
        /// <see cref="https://en.wikipedia.org/wiki/International_Standard_Book_Number"/>>
        /// <seealso cref="http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js"/>
        public static string ISBN10(bool formallyValid = true)
        {
            int[] v = new int[9];
            for (int i = 0; i < 9; i++)
                v[i] = RandomNumber.Next(0, 10);
            var checksum = computeChecksumIsbn10(v);

            if (!formallyValid)
                checksum++; //set wrong checksum 

            var prefix = string.Join(string.Empty, v);

            return prefix + (checksum < 10 ? checksum.ToString() : "X");
        }

        private static int computeChecksumIsbn10(int[] digits)
        {
            int sum = 0;
            
            for (var i = 0; i < 9; i++)
                sum += (10 - i) * digits[i];

            sum *= 10;

            return (sum % 11);
        }
    }
}