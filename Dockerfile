FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /src
COPY . .
RUN dotnet build "GdalNetCore.csproj" -c Release
RUN dotnet publish "GdalNetCore.csproj" -c Release -o /app
RUN apt-get update && apt-get install -y gdal-bin 
ENV GDAL_DATA=/usr/share/gdal/2.1/
WORKDIR /app
