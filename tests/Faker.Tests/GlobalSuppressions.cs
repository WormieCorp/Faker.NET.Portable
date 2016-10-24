[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"Reliability",
	"S2201:Return values should not be ignored when function calls don't have any side effects",
	Justification = "This test have no need for the return value",
	Scope = "member",
	Target = "~M:Faker.Tests.Common.ResourceCollectionCachingTests.Cache_At_Least_Doubles_Performance")]
