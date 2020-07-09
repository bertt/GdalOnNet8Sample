FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /src
COPY . .
RUN dotnet build "GdalNetCore.csproj" -c Release
RUN dotnet publish "GdalNetCore.csproj" -c Release -o /app
RUN apt-get update && apt-get install gdal-bin -y && apt-get install libproj-dev -y
ENV GDAL_DATA=/usr/share/gdal
WORKDIR /app
ENTRYPOINT ["dotnet", "GdalNetCore.dll"]
