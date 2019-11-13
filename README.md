# GdalOnNetCoreSample

Sample of running GDAL on .NET Core. Works on Linux and Windows (not Mac).

Sample contains 2 parts:

- Read GeoJSON file

- Transform coordinate from espg:28992 to epsg:4326

## Running

```
$ git clone https://github.com/bertt/GdalOnNetCoreSample.git
$ cd GdalOnNetCoreSample
$ dotnet build
$ dotnet run
```

## Docker

```
$ docker build -t gdalnetcore .
$ docker run -it gdalnetcore
```

## Dependencies

This sample uses the following packages from NuGet on Windows:

- https://www.nuget.org/packages/Gdal.Core/2.3.0-beta-023

- https://www.nuget.org/packages/Gdal.Core.WindowsRuntime/2.3.0-beta-023 

And the following packages from MyGet on Linux:

- https://www.myget.org/feed/gdalcore/package/nuget/Gdal.Core (Gdal.Core 2.3.0-beta-024-1801)

- https://www.myget.org/feed/gdalcore/package/nuget/Gdal.Core.LinuxRuntime (Gdal.Core.LinuxRuntime 2.3.0-beta-024-1840)


In the csproj file there is a conditional packagereference depending on OS (Windows or Unix). NB: This program does not work on Mac.

### Dependencies on Linux: gdal-bin and libproj-dev:

To run on Linux system the dependencies gdal-bin and libproj-dev must be installed. See also the Dockerfile for sample.

```
$ sudo apt-get install gdal-bin
$ sudo apt-get install libproj-dev
```
