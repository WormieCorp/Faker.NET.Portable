---
Title: App Examples
Description: Examples of using App related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/App) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## Author
Generate a random application author, this can both be a name or a company.
The same process can be aquired by either using [`Name.FullName`](name#full-name)
or by using [`Company.Name`](company#name).

```
Faker.App.Author();
```
**OUTPUTS:** `"Hartmann LLC"`

- - -

## Name
Generates a randomly selected application name from a static culture list of names.

```
Faker.App.Name();
```
**OUTPUTS:** `"Flixflex"`

- - -

## Version
Generates a random valid version.
```
Faker.App.Version();
```
**OUTPUTS:** `"0.8.6"`
