FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY . .
WORKDIR /Gdal.Console
RUN dotnet publish -c Release -o /app
WORKDIR /app
ENV PROJ_LIB=/app/runtimes/linux-x64/native/maxrev.gdal.core.libshared/
ENTRYPOINT ["dotnet", "Gdal.Console.dll"]
