using System;
using System.Collections.Generic;
using OSGeo.GDAL;
using OSGeo.OGR;

namespace GdalNetCore.Core
{
    public class GdalKit
    {
        public GdalKit()
        {
            Gdal.AllRegister();
            Ogr.RegisterAll();
        }
        public GdalInfo GetGdalInfo()
        {
            var info = new GdalInfo();

            info.ReleaseName = Gdal.VersionInfo("RELEASE_NAME");
            info.VersionNumber = Gdal.VersionInfo("VERSION_NUM");
            info.BuildInfo = Gdal.VersionInfo("BUILD_INFO");

            var drivers = new List<string>();
            for (var i = 0; i < Ogr.GetDriverCount(); i++)
            {
                drivers.Add(Ogr.GetDriver(i).GetName());
            }
            info.Drivers = drivers;
            return info;
        }
    }
}
