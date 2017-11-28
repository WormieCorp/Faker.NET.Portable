---
Title: Avatar Examples
Description: Examples of using Avatar related methods
---

:::{.alert .alert-warning}
<i class="fa fa-warning" aria-hidden="true"></i>
<span class="sr-only">Warning:</span>
This class have been marked as obsolete, and is expected to be removed in the next major release.
Please use the [RoboHash](robohash) or [FlatHash](flathash) classes instead.
:::

Examples of available methods and a possible output from a call.
See the [API documentation](../../api/Faker/Avatar) for the syntax of the methods.

**NOTES:**
By adding `using Faker;`, all `Faker.` prefixes in the examples can be removed.

- - -

## Image
Generates an image from the great RoboHash website,
recommended to use [`RoboHash.Image`](robohash#Image) instead.
```
Faker.Avatar.Image(
	slug = null,
	size = "300x300",
	ImageFormat format = ImageFormat.png,
	string set = "set1
)
```
**OUTPUTS:** `"http://robohash.org/dictasitsit.png?size=300x300&set=set1"`
![RoboHash Example Image](http://robohash.org/dictasitsit.png?size=300x300&set=set1)
