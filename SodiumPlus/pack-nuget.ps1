function PackSodiumPlus($version) {
	nuget pack "SodiumPlus.nuspec" -OutputDirectory bin\release -Version $version
}