msbuild /t:pack /p:Configuration=Release

dotnet nuget push .\bin\Release\Helloserve.RandomOrg.2.0.0.nupkg