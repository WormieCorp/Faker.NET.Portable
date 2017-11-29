using NUnit.Framework;

namespace Faker.Tests.Common
{
    public class CompanyTests
    {
        [Test]
        [Repeat(10)]
        public void Should_Generate_Logo_Url()
        {
            string url = Company.Logo();

            Assert.That(url, Does.StartWith("http://pigment.github.io/fake-logos/logos/medium/color/")
                               .And.Match(@"[0-9]+\.png$"));
        }

        [Test]
        [Repeat(10)]
        public void Should_Generate_Logo_Url_With_Https()
        {
            string url = Company.Logo(true);

            Assert.That(url, Does.StartWith("https://pigment.github.io/fake-logos/logos/medium/color/")
                               .And.Match(@"[0-9]+\.png$"));
        }
    }
}
