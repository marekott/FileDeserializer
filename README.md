# FileDeserializer
FileDeserializer is a library for .NET applications which will simplify reading and writing to files. Written with .NET Standard 2.0, can be used both with .NET Framework and .NET Core.

- Build: ![Build Status](
https://dev.azure.com/marekott94/FileDeserializer/_apis/build/status/FileDeserializerBuild?branchName=master "Build Status")
- Release: ![Release Status](
https://vsrm.dev.azure.com/marekott94/_apis/public/Release/badge/43ae9ff3-8a8a-4bdc-9bbf-7b3beb3380c3/1/1 "Release Status")

# Current version
Current version supports:
- Csv deserialization

# Getting started
FileDeserializer is installed from NuGet.
```
Install-Package FileDeserializer
```
To start using FileDeserialized add using statement.
```
using FileDeserializer;
```
Create object of type ICsvConverter by calling:
```csharp
ICsvConverter csvConverter = new CsvConverter();
```
You can now start using deserialization in two modes on ICsvConverter object:
```csharp
/// <summary>
/// Generic method which deserialize specific Csv for one-dimensional array. Use when file has only one row or you don't need data split by columns.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="path">Absolute path to file</param>
/// <param name="separator">Separator by which file will be spitted</param>
/// <returns></returns>
T[] Deserialize<T>(string path, char separator);
```
```csharp
/// <summary>
/// Generic method which deserialize specific Csv for two-dimensional array. Deserialize to string if you would like to keep headers which are different than numeric.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="path">Absolute path to file</param>
/// <param name="separator">Separator by which file will be spitted</param>
/// <param name="skipHeaders">Choose if headers should be omitted.</param>
/// <returns></returns>
T[,] DeserializeByRows<T>(string path, char separator, bool skipHeaders = false);
```
