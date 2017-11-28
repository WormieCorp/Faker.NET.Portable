using System;
using System.Linq;
using NUnit.Framework;

namespace Faker.Tests.Common
{
    public class BooleanTests
    {
        [Test]
        [Repeat(1000)]
        public void Should_Generate_True_Or_False()
        {
            var booleans = Enumerable.Range(1, 10000)
                .Select(idx => Boolean.Next());

            int trueCount = booleans.Count(b => b);
            int falseCount = booleans.Count(b => !b);

            Assert.That(trueCount, Is.GreaterThan(1000));
            Assert.That(falseCount, Is.GreaterThan(1000));
        }

        [Test]
        [Repeat(100)]
        public void Should_Not_Generate_True_With_Zero_Probability()
        {
            var booleans = Enumerable.Range(1, 1000)
                .Select(idx => Boolean.Next(0));
            var trueCount = booleans.Count(b => b);

            Assert.That(trueCount, Is.Zero);
        }

        [Test]
        [Repeat(100)]
        public void Should_Always_Generate_True_With_One_Probability()
        {
            var booleans = Enumerable.Range(1, 1000)
                .Select(idx => Boolean.Next(1));
            var trueCount = booleans.Count(b => b);

            Assert.That(trueCount, Is.EqualTo(1000));
        }

        [Test]
        [Repeat(100)]
        public void Should_Generate_Little_True_Values_According_To_Large_Numbers_Theory()
        {
            var runs = 10000;
            var trueProbability = 0.1d;
            var guardThreshold = 1.25f;

            var booleans = Enumerable.Range(1, runs)
                .Select(idx => Boolean.Next(trueProbability));
            var trueCount = booleans.Count(b => b);

            Assert.That(trueCount, Is.LessThan(runs * trueProbability * guardThreshold));
        }

        [Test]
        [Repeat(100)]
        public void Should_Generate_Many_True_Values_According_To_Large_Numbers_Theory()
        {
            var runs = 10000;
            var trueProbability = 0.9d;
            var guardThreshold = 1.25f;

            var booleans = Enumerable.Range(1, runs)
                .Select(idx => Boolean.Next(trueProbability));
            var trueCount = booleans.Count(b => b);

            Assert.That(trueCount, Is.LessThan(runs * trueProbability * guardThreshold));
        }
    }
}
