# 1. SDK imajı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
# 2. Çalışma dizini
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 3. Tüm proje dosyalarını kopyala
COPY . .
# 4. Restore işlemi

RUN dotnet restore "./SwordloginApi.csproj"

# 5. Publish işlemi

RUN dotnet publish "./SwordloginApi.csproj" -c Release -o /app/publish
RUN dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget
# 6. Runtime imajı

FROM base AS final

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SwordloginApi.dll"]
# 7. Uygulamayı başlat
ENTRYPOINT ["dotnet", "SwordloginApi.dll"]

