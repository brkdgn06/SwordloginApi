# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore ./SwordloginApi.csproj

RUN dotnet publish -c Release -o out
RUN dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget
# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "SwordloginApi.dll"]