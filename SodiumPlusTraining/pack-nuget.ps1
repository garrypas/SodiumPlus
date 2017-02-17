function PackSodiumPlusTraining($version) {
	nuget pack "SodiumPlusTraining.nuspec" -OutputDirectory bin\release -Version $version
}