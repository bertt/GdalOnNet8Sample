# GDAL on .NET 8 Sample

Sample of running GDAL 3.9.1 on .NET 8 Works on osx-x64, osx-arm64, linux-x64 and win-x64.

Sample contains 3 parts:

- Read GML file

- Read GeoJSON file

- Transform coordinate from espg:28992 to epsg:4326

Related blog see https://bertt.wordpress.com/2022/02/25/using-gdal-in-net-6/

For using the GDAL api's see:

Raster: https://gdal.org/api/csharp/csharp_raster.html

Vector: https://gdal.org/api/csharp/csharp_vector.html

## Running

Prerequisite: Install .NET 8 https://dotnet.microsoft.com/en-us/download/dotnet/8.0

```
$ git clone https://github.com/bertt/GdalOnNetCoreSample.git
$ cd GdalOnNetCoreSample
$ cd Gdal.Console
$ dotnet run
```

Result:

```
Releasename: 3.9.1
Versionnumber: 3090100
Number of OGR drivers: 81
OGR Drivers: AmigoCloud,AVCBin,AVCE00,BAG,CAD,Carto,CSV,CSW,DGN,DXF,EDIGEO,EEDA,Elasticsearch,ESRI Shapefile,ESRIJSON,FITS,FlatGeobuf,Geoconcept,GeoJSON,GeoJSONSeq,GeoRSS,GML,GMLAS,GPKG,GPSBabel,GPX,GTFS,HTTP,Idrisi,Interlis 1,Interlis 2,JML,JP2OpenJPEG,JSONFG,KML,LIBKML,LVBAG,MapInfo File,MapML,MBTiles,Memory,MiraMonVector,MSSQLSpatial,MVT,MySQL,NAS,netCDF,NGW,OAPIF,ODBC,ODS,OGCAPI,OGR_GMT,OGR_PDS,OGR_SDTS,OGR_VRT,OpenFileGDB,OSM,PCIDSK,PDF,PDS4,PGDUMP,PGeo,PLSCENES,PMTiles,PostgreSQL,S57,Selafin,SQLite,SVG,SXF,TIGER,TopoJSON,UK .NTF,VDV,VFK,VICAR,WAsP,WFS,XLS,XLSX
Number of GDAL drivers: 150
GDAL Drivers: AAIGrid,ACE2,ADRG,AIG,AirSAR,BAG,BIGGIF,BLX,BMP,BSB,BT,BYN,CAD,CALS,CEOS,COASP,COG,COSAR,CPG,CTable2,CTG,DAAS,DERIVED,DIMAP,DIPEx,DOQ1,DOQ2,DTED,ECRGTOC,EEDAI,EHdr,EIR,ELAS,ENVI,ERS,ESAT,ESRIC,FAST,FIT,FITS,GenBin,GFF,GIF,GPKG,GRASSASCIIGrid,GRIB,GS7BG,GSAG,GSBG,GSC,GTI,GTiff,GTX,GXF,HDF4,HDF4Image,HDF5,HDF5Image,HF2,HFA,HTTP,ILWIS,IRIS,ISCE,ISG,ISIS2,ISIS3,JAXAPALSAR,JDEM,JP2OpenJPEG,JPEG,JPEGXL,KMLSUPEROVERLAY,KRO,L1B,LAN,LCP,Leveller,LOSLAS,MAP,MBTiles,MEM,MFF,MFF2,MRF,MSGN,NDF,netCDF,NGSGEOID,NGW,NITF,NOAA_B,NSIDCbin,NTv2,NWT_GRC,NWT_GRD,OGCAPI,OpenFileGDB,OZI,PAux,PCIDSK,PCRaster,PDF,PDS,PDS4,PLMOSAIC,PLSCENES,PNG,PNM,PostGISRaster,PRF,R,Rasterlite,RIK,RMF,ROI_PAC,RPFTOC,RRASTER,RS2,RST,S102,S104,S111,SAFE,SAGA,SAR_CEOS,SDTS,SENTINEL2,SGI,SIGDEM,SNODAS,SRP,SRTMHGT,STACIT,STACTA,Terragen,TGA,TIL,TSX,USGSDEM,VICAR,VRT,WCS,WEBP,WMS,WMTS,XPM,XYZ,Zarr,ZMap
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

- https://www.nuget.org/packages/MaxRev.Gdal.Core/ - version 3.9.0.218

- https://www.nuget.org/packages/MaxRev.Gdal.WindowsRuntime.Minimal/ - version 3.9.0.218

- https://www.nuget.org/packages/MaxRev.Gdal.LinuxRuntime.Minimal/ - version 3.9.0.218

- https://www.nuget.org/packages/MaxRev.Gdal.MacosRuntime.Minimal.x64/ - version 3.9.0.218

- https://www.nuget.org/packages/MaxRev.Gdal.MacosRuntime.Minimal.arm64/ - version 3.9.0.218

## Known issues

To perform projections there is a dependency to environment variable 'PROJ_LIB'. It should point to a directory
containing proj.db SQLite file.

Error message when this dependency is missing:

```
System.ApplicationException: 'PROJ: proj_get_authorities_from_database: Cannot find proj.db'
```

## History

2024-06-28: From 3.8 to 3.9

2024-04-30: From 3.7 to 3.8

2023-07-21: From GDAL 3.6 to 3.7, added macOS support