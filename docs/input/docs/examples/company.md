---
Title: Company Examples
Description: Examples of using Company related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Company) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## Bullshit
Generates some random company bullshit.
```
Faker.Company.Bullshit();
```
**OUTPUTS:** `"syndicate innovative architectures"`

- - -

## Catch Phrase
Generates a random company catch phrase.
```
Faker.Company.CatchPhrase();
```
**OUTPUTS:** `"Fundamental stable workforce"`

- - -

## Logo
Will generate a random logo by using a remote url and the http protocol.
```
Faker.Company.Logo();
```
**OUTPUTS:** `"http://pigment.github.io/fake-logos/logos/medium/color/13.png"`  
![Company Logo Example](http://pigment.github.io/fake-logos/logos/medium/color/13.png){.img-responsive}

Will generate a random logo by using a remote url and the https protocol.
```
Faker.Company.Logo(useSsl: true);
```
**OUTPUTS:** `"https://pigment.github.io/fake-logos/logos/medium/color/4.png"`
![Company Https Logo Example](https://pigment.github.io/fake-logos/logos/medium/color/4.png){.img-responsive}

- - -

## Name
Will generate a random company name.
```
Faker.Company.Name();
```
**OUTPUTS:** `"Parisian-Witting"`

- - -

## Suffix
Will generate a random company suffix.
```
Faker.Company.Suffix();
```
**OUTPUTS:** `"and Sons"`
