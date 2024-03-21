[![.NET Unit Tests][.NET-Badge]][.NET-Url]
[![CodeQL][CodeQL-Badge]][CodeQL-Url]
[![CodeFactor][CodeFactor-Badge]][CodeFactor-Url]

# Artwise-API

## Requirements

- .NET 8 SDK
- ASP .NET Core Runtime and Targeting Pack (usually bundled with the SDK)
    - To check, you can execute `dotnet new list`. If you see `ASP.NET Core` listed, you have it installed.

## How to use

To execute the API, run:

```
dotnet run
```

To execute the API with hot-reloading, run:

```
dotnet watch
```

To execute the unit tests, run:

```
dotnet test
```

To build for production, run:

```
dotnet publish -c Release -o "path/to/build/folder"
```

[CodeFactor-Url]: https://www.codefactor.io/repository/github/fuzzylab-uva/artwise-api/overview/main
[CodeFactor-Badge]: https://www.codefactor.io/repository/github/fuzzylab-uva/artwise-api/badge/main
[.NET-Url]: ../../actions/workflows/dotnet.yml
[.NET-Badge]: ../../actions/workflows/dotnet.yml/badge.svg
[CodeQL-Url]: ../../actions/workflows/codeql-analysis.yml
[CodeQL-Badge]: ../../actions/workflows/codeql-analysis.yml/badge.svg