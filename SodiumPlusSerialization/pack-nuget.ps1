function PackSodiumPlusSerialization($version) {
	nuget pack "SodiumPlusSerialization.nuspec" -OutputDirectory bin\release -Version $version
}