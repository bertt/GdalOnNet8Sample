using System;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;

namespace GdalNetCore.Core
{
    public static class Class1
    {
        public static string GetHello()
        {
            Gdal.AllRegister();
            Ogr.RegisterAll();

            return "hallo" + Ogr.GetDriverCount();
        }
    }
}
