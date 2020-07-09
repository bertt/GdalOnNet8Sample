FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /src
COPY . .
RUN dotnet build "GdalNetCore.csproj" -c Release
RUN dotnet publish "GdalNetCore.csproj" -c Release -o /app
RUN apt-get update && apt-get install -y gdal-bin && apt-get install libproj-dev
ENV GDAL_DATA=/usr/share/gdal/2.1/
WORKDIR /app
ENTRYPOINT ["dotnet", "GdalNetCore.dll"]
