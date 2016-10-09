using System.Globalization;

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
        /// <param name="validChecksum">Indicates whether the generated ISBN has a valid checksum or not</param>
        /// <returns>The generated ISBN-10</returns>
        /// <remarks>
        ///     Description of the ISBN standard is at https://en.wikipedia.org/wiki/International_Standard_Book_Number
        ///     Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js
        /// </remarks>
        public static string ISBN10(bool validChecksum = true)
        {
            int[] v = new int[9];
            for (int i = 0; i < 9; i++)
                v[i] = RandomNumber.Next(0, 10);
            var checksum = computeChecksumIsbn10(v);

            if (!validChecksum)
                checksum = (checksum + 1) % 11; //set wrong checksum

            var prefix = string.Join(string.Empty, v);

            return prefix + (checksum < 10 ? checksum.ToString(CultureInfo.InvariantCulture) : "X");
        }

        /// <summary>
        ///     Generates an ISBN-13 Code
        /// </summary>
        /// <param name="validChecksum">Indicates whether the generated ISBN has a valid checksum or not</param>
        /// <returns>The generated ISBN-13</returns>
        /// <remarks>
        ///     Description of the ISBN standard is at https://en.wikipedia.org/wiki/International_Standard_Book_Number
        ///     Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js
        /// </remarks>
        public static string ISBN13(bool validChecksum = true)
        {
            int[] v = new int[12];
            //ISBN13 starts with "978" or "979"
            v[0] = 9;
            v[1] = 7;
            v[2] = RandomNumber.Next(8, 10);
            for (int i = 3; i < 12; i++)
                v[i] = RandomNumber.Next(0, 10);

            var checksum = computeChecksumEan(v); //ISBN13 is an EAN

            if (!validChecksum)
                checksum = (checksum + 1) % 10; //set wrong checksum

            var prefix = string.Join(string.Empty, v);

            return prefix + checksum;
        }

        /// <summary>
        ///     Generates a EAN Code
        /// </summary>
        /// <param name="validChecksum">Indicates whether the generated EAN has a valid checksum or not</param>
        /// <returns>The generated EAN</returns>
        /// <remarks>
        ///     Description of the EAN standard is at https://en.wikipedia.org/wiki/International_Article_Number
        ///     Checksum routines are at http://www.isbn-check.de/servejs.pl?src=isbnfront.perlserved.js
        /// </remarks>
        public static string EAN(bool validChecksum = true)
        {
            int[] v = new int[12];
            for (int i = 0; i < 12; i++)
                v[i] = RandomNumber.Next(0, 10);

            var checksum = computeChecksumEan(v);

            if (!validChecksum)
                checksum = (checksum + 1) % 10; //set wrong checksum

            var prefix = string.Join(string.Empty, v);

            return prefix + checksum;
        }

        /// <summary>
        ///     Generates a RUT Code
        /// </summary>
        /// <param name="validChecksum">Indicates whether the generated RUT has a valid checksum or not</param>
        /// <returns>The generated RUT</returns>
        /// <remarks>
        ///     Description of the RUT standard is at https://en.wikipedia.org/wiki/National_identification_number#Chile
        ///     Checksum routines are at http://www.vesic.org/english/blog-eng/net/verifying-chilean-rut-code-tax-number/
        /// </remarks>
        public static string RUT(bool validChecksum = true)
        {
            int[] v = new int[8];

            for (int i = 0; i < 8; i++)
                v[i] = RandomNumber.Next(0, 10);

            var checksum = computeRutChecksum(v);

            if (!validChecksum)
                checksum = (checksum + 1) % 11; //set wrong checksum

            var prefix = string.Join(string.Empty, v);

            var result = prefix + (checksum < 10 ? checksum.ToString(CultureInfo.InvariantCulture) : "K");
            return result;
        }

        private static int computeChecksumIsbn10(int[] digits)
        {
            int sum = 0;

            for (var i = 0; i < 9; i++)
                sum += (10 - i) * digits[i];

            sum *= 10;

            return sum % 11;
        }

        private static int computeChecksumEan(int[] digits)
        {
            var sum = 0;
            for (int i = 0; i < 12; i += 2)
                sum += digits[i];
            for (int i = 1; i < 12; i += 2)
                sum += 3 * digits[i];

            return (10 - (sum % 10)) % 10;
        }

        private static int computeRutChecksum(int[] digits)
        {
            int[] coefs = { 3, 2, 7, 6, 5, 4, 3, 2 };

            int sum = 0;

            for (int i = 0; i < 8; i++)
                sum += coefs[i] * digits[i];

            return (11 - (sum % 11)) % 11;
        }
    }
}