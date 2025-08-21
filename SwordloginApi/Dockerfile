# 1. Runtime ortamı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app


# 2. Build ortamı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./SwordloginApi.csproj"
RUN dotnet publish "./SwordloginApi.csproj" -c Release -o /app/publish
RUN dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget

# 3. Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SwordloginApi.dll"]
