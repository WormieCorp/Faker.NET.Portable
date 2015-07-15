Faker.NET Portable Edition
=====
AppVeyor = [![Build status](https://ci.appveyor.com/api/projects/status/iggsqi972k9dqvoo/branch/release%2Fv.2.0.0?svg=true)](https://ci.appveyor.com/project/AdmiringWorm/faker-cs)
Travis   = [![Build Status](https://travis-ci.org/AdmiringWorm/Faker.NET.Portable.svg?branch=release%2Fv.2.0.0)](https://travis-ci.org/AdmiringWorm/Faker.NET.Portable)


C# port of the Ruby Faker gem (http://faker.rubyforge.org/) and is used to easily generate fake data: 
names, addresses, phone numbers, etc.

Will be available as a NuGet package when it's ready.

Get the code via git:

    git clone https://github.com/AdmiringWorm/Faker.NET.Portable.git

Supported version:


	.NET framework 4.0,
	Silverlight 5.0,
    Windows 8,
    Windows Phone 8.1,
    Windows Phone Silverlight 8,
    Xamarin.Android,
    Xamarin.iOS,
    Xamarin.iOS (Classic),
    Mono 3.2.8

## Usage

Add a reference to `Faker.dll` in you project within Visual Studio.

Start using the Faker methods to generate your random test data.

### Names, phone numbers and emails

	var name = Faker.Name.FullName();  // "Alene Hayes"
	Faker.Internet.Email(name);  // "alene_hayes@hartmann.co.uk"
	Faker.Internet.UserName(name);  // "alene.hayes"

	Faker.Internet.Email();  // "morris@friesen.us"
	Faker.Internet.FreeEmail();  // "houston_purdy@yahoo.com"

	Faker.Internet.DomainName();  // "larkinhirthe.com"

	Faker.Phone.Number();  // "(033)216-0058 x0344"

### Addresses

	Faker.Address.StreetAddress();  // "52613 Turcotte Lock"
	Faker.Address.SecondaryAddress();  // "Suite 656"
	Faker.Address.City();  // "South Wavaside"

	Faker.Address.UkCounty();  // "West Glamorgan"
	Faker.Address.UkPostCode().ToUpper();  // "BQ7 3AM"

	Faker.Address.UsState();  // "Tennessee"
	Faker.Address.ZipCode();  // "66363-7828"

### Lorem Ipsum sentences and paragraphs

	Faker.Lorem.Sentence();  // "Voluptatem repudiandae necessitatibus assumenda dolor illo maiores in."
	Faker.Lorem.Paragraph();  /* "Rerum dolor cumque cum animi consequatur praesentium. Enim quia quia modi est ut. Dolores qui debitis qui perspiciatis autem quas. Expedita distinctio earum aut. Delectus assumenda rerum quibusdam harum iusto." */

### Buzzword bingo

Last, but not least, you can generate company names, catchphrases and bs!

	Faker.Company.Name();  // "Dickens Group"
	Faker.Company.CatchPhrase();  // "User-centric neutral internet solution"
	Faker.Company.BS();  // "transition proactive solutio
