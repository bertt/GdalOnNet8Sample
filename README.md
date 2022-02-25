# GDAL on .NET Core Sample

Sample of running GDAL 2.3.0dev on .NET Core 3.1 Works on Linux and Windows (not Mac).

Sample contains 2 parts:

- Read Gml file

- Transform coordinate from espg:28992 to epsg:4326

## Gdal info

Windows:

```
Releasename: 2.3.0dev
Versionnumber: 2030000
Number of drivers: 59
Drivers: PCIDSK,PDF,DB2ODBC,ESRI Shapefile,MapInfo File,UK .NTF,OGR_SDTS,S57,DGN,OGR_VRT,REC,Memory,BNA,CSV,GML,GPX,LIBKML,KML,GeoJSON,OGR_GMT,GPKG,SQLite,ODBC,WAsP,PGeo,MSSQLSpatial,OpenFileGDB,XPlane,DXF,CAD,Geoconcept,GeoRSS,GPSTrackMaker,VFK,PGDUMP,OSM,GPSBabel,SUA,OpenAir,OGR_PDS,HTF,AeronavFAA,Geomedia,EDIGEO,SVG,Idrisi,ARCGEN,SEGUKOOA,SEGY,ODS,XLSX,Walk,SXF,Selafin,JML,VDV,TIGER,AVCBin,AVCE00
```

Linux:

```
Releasename: 2.3.0dev
Versionnumber: 2030000
Number of drivers: 65
Drivers in this release: PCIDSK,netCDF,PDF,ESRI Shapefile,MapInfo File,UK .NTF,OGR_SDTS,S57,DGN,OGR_VRT,REC,Memory,BNA,CSV,GML,GPX,LIBKML,KML,GeoJSON,OGR_GMT,GPKG,SQLite,WAsP,OpenFileGDB,XPlane,DXF,CAD,Geoconcept,GeoRSS,GPSTrackMaker,VFK,PGDUMP,OSM,GPSBabel,SUA,OpenAir,OGR_PDS,WFS,HTF,AeronavFAA,EDIGEO,GFT,SVG,CouchDB,Cloudant,Idrisi,ARCGEN,SEGUKOOA,SEGY,XLS,ODS,XLSX,ElasticSearch,Carto,AmigoCloud,SXF,Selafin,JML,PLSCENES,CSW,VDV,TIGER,AVCBin,AVCE00,HTTP
```

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
## Sample code (see program.cs)

Reading gml using Ogr:

```
Ogr.RegisterAll();
var gmlDriver = Ogr.GetDriverByName("GML");
var dsGml = gmlDriver.Open(@"LoD2_280_5657_1_NW.gml", 0);
var buildingLayer = dsGml.GetLayerByName("building");
var featuresGml = buildingLayer.GetFeatureCount(0);
Console.WriteLine($"Number of features: {featuresGml}");
```

Transform coordinate using Gdal:

```
Gdal.AllRegister();
var src = new SpatialReference("");
src.ImportFromEPSG(28992);
Console.WriteLine("SOURCE IsGeographic:" + src.IsGeographic() + " IsProjected:" + src.IsProjected());
var dst = new SpatialReference("");
dst.ImportFromEPSG(4326);
Console.WriteLine("DEST IsGeographic:" + dst.IsGeographic() + " IsProjected:" + dst.IsProjected());
var ct = new CoordinateTransformation(src, dst);
double[] p = new double[3];
p[0] = 85530; p[1] = 446100; p[2] = 0;
Console.WriteLine("From: x:" + p[0] + " y:" + p[1] + " z:" + p[2]);
ct.TransformPoint(p);
Console.WriteLine("To: x:" + p[0] + " y:" + p[1] + " z:" + p[2]);

```

## Dependencies

This sample uses the following packages from NuGet on Windows:

- https://www.nuget.org/packages/Gdal.Core/2.3.0-beta-023

- https://www.nuget.org/packages/Gdal.Core.WindowsRuntime/2.3.0-beta-023 

And the following packages from MyGet on Linux:

- https://www.myget.org/feed/gdalcore/package/nuget/Gdal.Core (Gdal.Core 2.3.0-beta-024-1801)

- https://www.myget.org/feed/gdalcore/package/nuget/Gdal.Core.LinuxRuntime (Gdal.Core.LinuxRuntime 2.3.0-beta-024-1840)

To use both NuGet and MyGet use 'RestoreSources' in csproj:

```
   <RestoreSources>
    $(RestoreSources);https://api.nuget.org/v3/index.json;https://www.myget.org/F/gdalcore/api/v3/index.json
   </RestoreSource>
```

In the csproj file there is a conditional packagereference depending on OS (Windows or Unix):

```
 <ItemGroup Condition="'$(OS)' == 'Unix'">
    <PackageReference Include="Gdal.Core" Version="2.3.0-beta-024-1801" />
    <PackageReference Include="Gdal.Core.LinuxRuntime" Version="2.3.0-beta-024-1840" />
  </ItemGroup>
  <ItemGroup Condition="'$(OS)' != 'Unix'">
      <PackageReference Include="Gdal.Core" Version="2.3.0-beta-023" />
      <PackageReference Include="Gdal.Core.WindowsRuntime" Version="2.3.0-beta-023" />
  </ItemGroup>
```

NB: This program does not work on Mac.

###

To perform projections there is a dependency to environment variabale 'GDAL_DATA'. It should point to a directory
containing gcs.csv file.

Error message when this dependency is missing:

```
ERROR 4: Unable to open EPSG support file gcs.csv.  Try setting the GDAL_DATA environment variable to point to the directory containing EPSG csv files.
```

Path for ArcMap 10.3:

C:\Program Files (x86)\ArcGIS\Desktop10.3\pedata\gdaldata

### Dependencies on Linux: gdal-bin and libproj-dev:

To run on Linux system the dependencies gdal-bin and libproj-dev must be installed. See also the Dockerfile for sample.

```
$ sudo apt-get install gdal-bin
$ sudo apt-get install libproj-dev
```
