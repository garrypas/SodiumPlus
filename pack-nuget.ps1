param([string]$version = "1.0.0")
. SodiumPlus\pack-nuget.ps1
. SodiumPlusTraining\pack-nuget.ps1
. SodiumPlusSerialization\pack-nuget.ps1

cd SodiumPlus
PackSodiumPlus $version
cd ..

cd SodiumPlusTraining
PackSodiumPlusTraining $version
cd ..

cd SodiumPlusSerialization
PackSodiumPlusSerialization $version
cd ..