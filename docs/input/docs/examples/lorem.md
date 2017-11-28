---
Title: Lorem Ipsum Examples
Description: Examples of using Lorem Ipsum related methods
---

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Internet) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## Characters
Will generate random characters with the exact length of the specified `charCount` parameter (defaults to 255 characters).
```cs
Faker.Lorem.Characters(charCount: 255);
```
:::{.alert .alert-info}
Output have been abbreviated, actual output would be the same length
as the specified `charCount` parameter.
:::
**OUTPUTS:** `"PIXQAe"`

- - -

## Paragraph
Will generate a random paragraph using Lorem Ipsum paragraph (the paragraph will be a minimum of 3 sentences).
An overload exist to allow a selection of different minimum sentences.
```
Faker.Lorem.Paragraph();
```
**OUTPUTS:** an array with the following sentences (split to multiple lines for viewing purposes only):
```
"Necessitatibus est soluta est modi."
"Ut debitis iste provident est eum voluptas ut."
"Unde aliquid quo excepturi omnis hic fuga consectetur dolores."
"Provident neque beatae omnis illo eos."
```

- - -

## Sentence
Will generate a random Lorem Ipsum sentence.
```
Faker.Lorem.Paragraph();
```
**OUTPUTS:** `"Consectetur beatae et doloremque amet."`
