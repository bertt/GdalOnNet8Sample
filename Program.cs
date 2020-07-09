using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using Wkx;

namespace GdalNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Gdal.AllRegister();
            Ogr.RegisterAll();

            // print OGR drivers
            var res = new List<string>();
            for(var i = 0; i < Ogr.GetDriverCount(); i++)
            {
                res.Add(Ogr.GetDriver(i).GetName());
            }
            Console.WriteLine("Drivers: " + string.Join(',', res));
            Console.WriteLine("Number of drivers: " + Ogr.GetDriverCount());

            // sample reading (city)gml file
            var gmlDriver = Ogr.GetDriverByName("GML");
            var dsGml = gmlDriver.Open(@"LoD2_280_5657_1_NW.gml", 0);
            var buildingLayer = dsGml.GetLayerByName("building");
            var featuresGml = buildingLayer.GetFeatureCount(0);
            Console.WriteLine($"Number of features: {featuresGml}");
            for(var f = 0; f < featuresGml; f++)
            {
                var featureGml = buildingLayer.GetNextFeature();
                var geometry = featureGml.GetGeometryRef();
                // serialize to wkt... when using wkb there is an error :-(
                var wkt = string.Empty;
                geometry.ExportToWkt(out wkt);
                var geom = Wkx.Geometry.Deserialize<WktSerializer>(wkt);
                Console.WriteLine(geom.GeometryType + ", " + ((MultiLineString)geom).Geometries.Count); // result is multilinestring...
            }

            // sample read geojson file
            var geojsonDriver = Ogr.GetDriverByName("GeoJSON");
            var ds = geojsonDriver.Open(@"gemeenten2016.geojson", 0);
            var layer1 = ds.GetLayerByName("gemeenten2016");
            var features = layer1.GetFeatureCount(0);
            Console.WriteLine("features in geojson: " + features);

            // sample transform coordinate from epsg:28992 to epsg:4326
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
            Console.WriteLine("Program finished, press any key to exit.");
            Console.ReadKey();
        }
    }
}
