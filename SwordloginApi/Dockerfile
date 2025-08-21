# 1. SDK imajı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# 2. Çalışma dizini
WORKDIR /app
# 3. Tüm proje dosyalarını kopyala
COPY . .
# 4. Restore işlemi

RUN dotnet restore SwordloginApi.csproj
# 5. Publish işlemi

RUN dotnet publish SwordloginApi.csproj -c Release -o /app/out
RUN dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget
# 6. Runtime imajı

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out 

# 7. Uygulamayı başlat
ENTRYPOINT ["dotnet", "SwordloginApi.dll"]

