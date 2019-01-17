using OSGeo.GDAL;
using OSGeo.OSR;
using System;

namespace GdalNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Gdal.AllRegister();
            Console.WriteLine("Hello World!");

            SpatialReference src = new SpatialReference("");
            src.ImportFromProj4("+proj=latlong +datum=WGS84 +no_defs");
            Console.WriteLine("SOURCE IsGeographic:" + src.IsGeographic() + " IsProjected:" + src.IsProjected());
            SpatialReference dst = new SpatialReference("");
            dst.ImportFromProj4("+proj=somerc +lat_0=47.14439372222222 +lon_0=19.04857177777778 +x_0=650000 +y_0=200000 +ellps=GRS67 +units=m +no_defs");
            Console.WriteLine("DEST IsGeographic:" + dst.IsGeographic() + " IsProjected:" + dst.IsProjected());
            /* -------------------------------------------------------------------- */
            /*      making the transform                                            */
            /* -------------------------------------------------------------------- */
            CoordinateTransformation ct = new CoordinateTransformation(src, dst);
            double[] p = new double[3];
            p[0] = 19; p[1] = 47; p[2] = 0;
            ct.TransformPoint(p);
            Console.WriteLine("x:" + p[0] + " y:" + p[1] + " z:" + p[2]);
            ct.TransformPoint(p, 19.2, 47.5, 0);
            Console.WriteLine("x:" + p[0] + " y:" + p[1] + " z:" + p[2]);


        }
    }
}
