---
Title: Code Examples
Description: Examples of using Code related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Code) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## European  Article Number (EAN) / Global Trade Item Number (GTIN)

Will generate a random EAN / GTIN number with a valid checksum.
```
Faker.Code.EAN(validChecksum: true);
```
**OUTPUTS:** `8893491626338`

- - -

## Italian Fiscal Code
:::{.alert .alert-info}
Example is missing information, please help out with providing a description.
:::
```
Faker.Code.FiscalCode(
	validChecksum: true,
	minAge: 18,
	maxAge: 65
);
```
**OUTPUTS:** `"GQAMTZ66E42C248W"`

- - -

## International Standard Book Number, 10 digits (ISBN10)
Will generate a random ISBN10 number.
```
Faker.Code.ISBN10(validChecksum: true);
```
**OUTPUTS:** `"959472336X"`

- - -

## International Standard Book Number, 13 digits (ISBN13)
Will generate a random ISBN13 number.
```
Faker.Code.ISBN13(validChecksum: true);
```
**OUTPUTS:** `"9798346018735"`

- - -

## National Provider Identifier (NPI)
Will generate a random NPI.
```
Faker.Code.NPI();
```
**OUTPUTS:** `"0929091271"`

- - -

## National Registration Identity Card (NRIC)
Will generate a random NRIC.
```
Faker.Code.NRIC(
	validChecksum: true,
	minAge: 18,
	maxage: 65
);
```
**OUTPUTS:** "59705128C"

- - -

## RUT
:::{.alert .alert-info}
Example is missing information, please help out with providing a description.
:::
```
Faker.Code.RUT(validChecksum: true);
```
**OUTPUTS:** `"16026599K"`

*[EAN]: European Article Number
*[GTIN]: Global Trade Item Number
*[ISBN10]: International Standard Book Number, 10 digits
*[ISBN13]: International Standard Book Number, 13 digits
*[ISBN]: International Standard Book Number
*[NPI]: National Provider Identifier
*[NRIC]: National Registration Identity Card
