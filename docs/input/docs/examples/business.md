---
Title: Business Examples
Description: Examples of using Business related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Business) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## Credit Card Number
Will generate a random credit card number.
```
Faker.Business.CreditCardNumber();
```
**OUTPUTS:** `"1211-1221-1234-2201"`

- - -

## Credit Card Expiry Date
Will generate a random credit card expiration date and time.
:::{.alert .alert-info}
The date will be between the next 365 and 1460 days (1 to 4 years),
and will use the current month, day, and time.
:::
```
Faker.Business.CreditCardExpiryDate();
```
**OUTPUTS:** `2019-07-17 15:35:35.731`

- - -

## National Insurance Number
Will generate a random national insurance number
```
Faker.Business.NationalInsuranceNumber();
```
**OUTPUTS:** `"AA123456A"`
