---
Title: Name Examples
Description: Examples of using Name related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Name) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## First Name
Will generate a random first name, can be both male and female.
```
Faker.Name.First();
```
**OUTPUTS:** `"Kaylie"`

- - -

## Full Name
Will generate a random name, combined both First and Last Name, with randomly using either Prefix or Suffix (or both).
```
Faker.Name.FullName();
```
**OUTPUTS:** `"Ms. Sallie Murphy III"`

- - -

## Last Name (Surname)
Will generate a random last name.
```
Faker.Name.Last();
```
**OUTPUTS:** `"Braun"`

- - -

## Prefix
Will generate a random prefix.
```
Faker.Name.Prefix();
```
**OUTPUTS:** `"Ms."`

- - -

## Suffix
Will generate a random suffix.
```
Faker.Name.Suffix();
```
**OUTPUTS:** `"Jr."`
