msbuild /t:pack /p:Configuration=Release

nuget push .\bin\Release\Helloserve.RandomOrg.1.0.2.nupkg