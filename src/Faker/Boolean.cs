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
        /// <returns>The random boolean value</returns>
        public static bool Next()
        {
            return RandomNumber.Next(0, 2) == 0;
        }

        /// <summary>
        ///   Generates a weighted Boolean value
        /// </summary>
        /// <param name="trueProbability">
        ///   The probability to have a true value. Ranges from 0.0 to 1.0.
        /// </param>
        /// <returns>The random boolean value</returns>
        public static bool Next(double trueProbability)
        {
            return RandomNumber.NextDouble() < trueProbability;
        }
    }
}
