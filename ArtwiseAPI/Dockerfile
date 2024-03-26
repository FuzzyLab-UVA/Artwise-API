FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5097

ENV ASPNETCORE_URLS=http://+:5097

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ArtwiseAPI/ArtwiseAPI.csproj", "ArtwiseAPI/"]
RUN dotnet restore "ArtwiseAPI/ArtwiseAPI.csproj"
COPY . .
WORKDIR "/src/ArtwiseAPI"
RUN dotnet build "ArtwiseAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ArtwiseAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtwiseAPI.dll"]