---
Title: Is it possible to Change the Language?
---

## Short answer
Yes it is possible, but not directly.

## Long answer
While it is possible to change the language, there isn't really any code in the library
to change it directly.
However, the library makes use of the current ui culture, and as such Changing the `CurrentUICulture`
on the current thread will also force `Faker.NET.Portable` to change the language.
See [Thread.CurrentUICulture Property](https://msdn.microsoft.com/en-us/library/system.threading.thread.currentuiculture(v=vs.110).aspx).

### Example
```
using System;
using System.Globalization;
using System.Threading;

// Force English (United States)
Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
string suffix = Faker.Name.Suffix();
Console.WriteLine(suffix); // Would output something like 'and Sons'

// Change current culture to be Norwegian
Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("nb-NO");
suffix = Faker.Name.Suffix();
Console.WriteLine(suffix); // Would output something like 'og Sønner'
```
