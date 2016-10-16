using NUnit.Framework;

namespace Faker.Tests.it_IT
{
	[SetUICulture("it-IT")]
	[SetCulture("it-IT")]
	[Category("Culture 'it_IT'")]
	public class AppItalianTests
	{
		[Test]
		[Repeat(1000)]
		public virtual void Should_Generate_Author()
		{
			string firstNameFormat = Resources.Name.First.ToFormat();
			string lastNameFormat = Resources.Name.Last.ToFormat();
			string prefixNameFormat = Resources.Name.Prefix.ToFormat();
			string suffixNameFormat = Resources.Name.Suffix.ToFormat();
			string suffixCompanyFormat = Resources.Company.Suffix.ToFormat();

			string author = App.Author();

			author.AssertFormats(firstNameFormat.Combine(lastNameFormat),
								 prefixNameFormat.Combine(firstNameFormat, lastNameFormat),
								 firstNameFormat.Combine(lastNameFormat, suffixNameFormat),
								 prefixNameFormat.Combine(firstNameFormat, lastNameFormat, suffixNameFormat),
<<<<<<< HEAD
								 firstNameFormat.Combine(lastNameFormat, suffixCompanyFormat),
							   lastNameFormat + "-" + lastNameFormat + " " + suffixCompanyFormat,
							   lastNameFormat + " e " + lastNameFormat + " " + suffixCompanyFormat,
							   lastNameFormat + ", " + lastNameFormat + " e " + lastNameFormat + " " + suffixCompanyFormat);
=======
								 lastNameFormat.Combine(suffixCompanyFormat),
								 lastNameFormat + "-" + lastNameFormat,
								 (lastNameFormat + ",").Combine(lastNameFormat, "and", lastNameFormat));
>>>>>>> 97445966a8b77bff8e3b86174cf827793e0ebe46
		}
	}
}
