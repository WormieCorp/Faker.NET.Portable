using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Faker.NET (Portable Edition)")]
[assembly: NeutralResourcesLanguage("en")]
#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else

[assembly: AssemblyConfiguration("Release")]
#endif

[assembly:
	InternalsVisibleTo(
		"Faker.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001009701c8f81e0a5095620fcce99dc8321b15e956cb27bb97cb669d87c7140c937c5b8049b2c522ae85929fb69602e2c9d05fd237d99f33cea07f6fd52e8e0af52866f75381efe35623dbf32e1707a3654f09e63383fe004f13b66e49af62b82213b8d0d00c48c0b4ad851baaed427ceb9eaf63df6cf89c795e243fea91581946c4"
		)]
