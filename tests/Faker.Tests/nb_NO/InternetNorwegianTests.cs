using Faker.Tests.Common;
using NUnit.Framework;

namespace Faker.Tests.nb_NO
{
	[SetUICulture("nb-NO")]
	[SetCulture("nb-NO")]
	[Category("Culture 'nb_NO'")]
	public class InternetNorwegianTests
	{
		[Test]
		[Repeat(1000)]
		public void Should_Create_Free_Email()
		{
			string freeEmailsFormat = Resources.Internet.FreeMail.ToFormat();

			string email = Internet.FreeEmail();

			Assert.That(email, Does.Match(string.Format("@({0})$", freeEmailsFormat))
								 .And.Match(InternetTests.EMAIL_REGEX)
								 .And.Not.Contains("www"));
		}
	}
}
