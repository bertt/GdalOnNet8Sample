# GDAL on .NET 6 Sample

Sample of running GDAL 3.3.3 on .NET 6.

Works on Linux and Windows (not Mac).

Blog: https://bertt.wordpress.com/2022/02/25/using-gdal-in-net-6/

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
PAM_ENABLED=YES
OGR_ENABLED=YES
GEOS_ENABLED=YES
GEOS_VERSION=3.9.0-CAPI-1.14.1

Releasename: 3.3.3
Versionnumber: 3030300
Number of drivers: 80
Drivers: ESRIC,PCIDSK,PDS4,VICAR,JP2OpenJPEG,PDF,MBTiles,EEDA,OGCAPI,DB2ODBC,ESRI Shapefile,MapInfo File,UK .NTF,LVBAG,OGR_SDTS,S57,DGN,OGR_VRT,REC,Memory,CSV,NAS,GML,GPX,KML,GeoJSON,GeoJSONSeq,ESRIJSON,TopoJSON,Interlis 1,Interlis 2,OGR_GMT,GPKG,SQLite,ODBC,WAsP,PGeo,MSSQLSpatial,PostgreSQL,OpenFileGDB,DXF,CAD,FlatGeobuf,Geoconcept,GeoRSS,GPSTrackMaker,VFK,PGDUMP,OSM,GPSBabel,OGR_PDS,WFS,OAPIF,Geomedia,EDIGEO,SVG,CouchDB,Cloudant,Idrisi,ARCGEN,ODS,XLSX,Elasticsearch,Walk,Carto,AmigoCloud,SXF,Selafin,JML,PLSCENES,CSW,VDV,GMLAS,MVT,NGW,MapML,TIGER,AVCBin,AVCE00,HTTP
```

## Docker

```
$ docker build -t gdal.console -f Dockerfile .
$ docker run -it gdal.console
```

## Sample code (see program.cs)

Reading gml using Ogr:

```
GdalBase.ConfigureAll();
var gmlDriver = Ogr.GetDriverByName("GML");
var dsGml = gmlDriver.Open(@"LoD2_280_5657_1_NW.gml", 0);
var buildingLayer = dsGml.GetLayerByName("building");
var featuresGml = buildingLayer.GetFeatureCount(0);
Console.WriteLine($"Number of features: {featuresGml}");
```

Transform coordinate using Gdal:

```
 GdalBase.ConfigureAll();
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

- https://www.nuget.org/packages/MaxRev.Gdal.Core/ - version 3.3.3.110

- https://www.nuget.org/packages/MaxRev.Gdal.WindowsRuntime.Minimal/ - version 3.3.3.110

- https://www.nuget.org/packages/MaxRev.Gdal.LinuxRuntime.Minimal/ - version 3.3.3.110


NB: This program does not work on Mac.

###

To perform projections there is a dependency to environment variable 'PROJ_LIB'. It should point to a directory
containing proj.db SQLite file.

Error message when this dependency is missing:

```
System.ApplicationException: 'PROJ: proj_get_authorities_from_database: Cannot find proj.db'
```
