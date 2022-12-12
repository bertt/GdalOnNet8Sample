# GDAL on .NET 6 Sample

Sample of running GDAL 3.6.0.100 on .NET 6 Works on Linux and Windows (not Mac).

Sample contains 3 parts:

- Read GML file

- Read GeoJSON file

- Transform coordinate from espg:28992 to epsg:4326

## Running

```
$ git clone https://github.com/bertt/GdalOnNetCoreSample.git
$ cd GdalOnNetCoreSample
$ cd Gdal.Console
$ dotnet run
```

Result:

```
Buildinfo: PAM_ENABLED=YES
OGR_ENABLED=YES
GEOS_ENABLED=YES
GEOS_VERSION=3.10.2-CAPI-1.16.0
PROJ_BUILD_VERSION=7.2.1
PROJ_RUNTIME_VERSION=7.2.1
COMPILER=MSVC 193431935

Releasename: 3.6.0
Versionnumber: 3060000
Number of drivers: 78
Drivers: ESRIC,FITS,PCIDSK,netCDF,PDS4,VICAR,JP2OpenJPEG,PDF,MBTiles,BAG,EEDA,OGCAPI,ESRI Shapefile,MapInfo File,UK .NTF,LVBAG,OGR_SDTS,S57,DGN,OGR_VRT,Memory,CSV,NAS,GML,GPX,LIBKML,KML,GeoJSON,GeoJSONSeq,ESRIJSON,TopoJSON,Interlis 1,Interlis 2,OGR_GMT,GPKG,SQLite,ODBC,WAsP,PGeo,MSSQLSpatial,PostgreSQL,MySQL,OpenFileGDB,DXF,CAD,FlatGeobuf,Geoconcept,GeoRSS,VFK,PGDUMP,OSM,GPSBabel,OGR_PDS,WFS,OAPIF,EDIGEO,SVG,Idrisi,XLS,ODS,XLSX,Elasticsearch,Carto,AmigoCloud,SXF,Selafin,JML,PLSCENES,CSW,VDV,GMLAS,MVT,NGW,MapML,TIGER,AVCBin,AVCE00,HTTP
```

## Docker

```
$ docker build -t gdal.console -f Dockerfile .
$ docker run -it gdal.console
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

This sample uses the following packages from NuGet:

- https://www.nuget.org/packages/MaxRev.Gdal.Core/ - version 3.6.0.100

- https://www.nuget.org/packages/MaxRev.Gdal.WindowsRuntime.Minimal/ - version 3.6.0.100

- https://www.nuget.org/packages/MaxRev.Gdal.LinuxRuntime.Minimal/ - version 3.6.0.100


NB: This program does not work on Mac.

###

To perform projections there is a dependency to environment variabale 'PROJ_LIB'. It should point to a directory
containing proj.db SQLite file.

Error message when this dependency is missing:

```
System.ApplicationException: 'PROJ: proj_get_authorities_from_database: Cannot find proj.db'
```

## History

22-12-12 using MaxRev.Gdal 3.6

22-03-04: using MaxRev.Gdal 3.3

19-11-13: using Gdal.Core 2.3 

