FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
WORKDIR /src
COPY . .
RUN dotnet build "GdalNetCore.csproj" -c Release
RUN dotnet publish "GdalNetCore.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/runtime:2.2
WORKDIR /app
COPY --from=build /app /app/
RUN apt-get update && apt-get install -y gdal-bin 
ENTRYPOINT ["dotnet", "GdalNetCore.dll"]