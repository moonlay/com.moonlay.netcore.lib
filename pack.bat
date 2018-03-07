echo CREATE NUGET PACKAGE
dotnet restore 
dotnet build
dotnet pack -c Release -o ..\.nupkg
pause