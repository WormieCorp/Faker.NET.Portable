---
Title: Internet Examples
Description: Examples of using Internet related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Internet) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## Domain Name
Will generate a random internet domain name.
```
Faker.Internet.DomainName();
```
**OUTPUTS:** `"roberts.us"`

- - -

## Domain Suffix
Will generate a random internet domain suffix (like `com`, `net`, `info`, etc..).
```
Faker.Internet.DomainSuffix();
```
**OUTPUTS:** `"info"`

- - -

## Domain Word
Will generate a random internet domain word
```
Faker.Internet.DomainWord();
```
**OUTPUTS:** `"mrazmedhurst"`

- - -

## Email Address
Will generate a random email address.
```
Faker.Internet.Email();
```
**OUTPUTS:** `"felicia_reinger@brownstroman.info"`

- - -

## Free Email Address
Will generate a random email address, using an existing free email provider.
```
Faker.Internet.FreeEmail();
```
**OUTPUTS:** `"adela_murphy@yahoo.com"`

- - -

## IPv4 Address
Will generate a random IPv4 address.
```
Faker.Internet.IPv4Address();
```
**OUTPUTS:** `"107.40.61.129"`

- - -

## IPv6 Address
Will generate a random IPv6 address.
```
Faker.Internet.IPv6Address();
```
**OUTPUTS:** `"5a84:cd5:5e97:b368:6266:30fe:f0e5:5eff"`

- - -

## MAC Address
Will generate a random computer MAC Address
```
Faker.Internet.MacAddress(
	prefix: null,
	groupSplit: ':'
);
```
**OUTPUTS:** `"A8:D9:AB:F2:D5:A2"`

## Password
Will generate a random password, between the specified minimum and maximum length.
```
Faker.Internet.Password(
	minLength: 6,
	maxLength: 16
);
```
**OUTPUTS:** `"2e@0SEu'!&ao:+("`

## Slugs [^1]
Will generate a random slug[^1].
```
Faker.Internet.Slug(
	words: null,
	glue: null
);
```
**OUTPUTS:** `"inventore-excepturi"`

- - -

## Url
Will generate a random url.
```
Faker.Internet.Url();
```
**OUTPUTS:** `"http://www2.dibbert.net/ernesto"`

- - -

## UserName
Will generate a random user name (based on real names).
```
Faker.Internet.UserName();
```
**OUTPUTS:** `"eino_olson"`

[^1]: A slug is defined as parto of a URL that identifies a page in human-readable keywords.  
    [Source](https://en.wikipedia.org/wiki/Semantic_URL#Slug "Wikipedia") \|

*[IPv4]: Internet Protocol version 4
*[IPv6]: Internet Protocol version 6
*[MAC]: Media Access Control
