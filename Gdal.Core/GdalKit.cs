using System.Collections.Generic;
using MaxRev.Gdal.Core;
using OSGeo.GDAL;
using OSGeo.OGR;

namespace GdalNetCore.Core
{
    public class GdalKit
    {
        public GdalKit()
        {
            GdalBase.ConfigureAll();
        }
        public GdalInfo GetGdalInfo()
        {
            var info = new GdalInfo();

            info.ReleaseName = Gdal.VersionInfo("RELEASE_NAME");
            info.VersionNumber = Gdal.VersionInfo("VERSION_NUM");
            info.BuildInfo = Gdal.VersionInfo("BUILD_INFO");

            var ogrDrivers = new List<string>();
            for (var i = 0; i < Ogr.GetDriverCount(); i++)
            {
                ogrDrivers.Add(Ogr.GetDriver(i).GetName());
            }
            info.OgrDrivers = ogrDrivers;

            var gdalDrivers = new List<string>();
            for (var i = 0; i < Gdal.GetDriverCount(); i++)
            {
                var shortname = Gdal.GetDriver(i).ShortName;
                if (!info.OgrDrivers.Contains(shortname)){
                    gdalDrivers.Add(shortname);
                }
            }
            info.GdalDrivers = gdalDrivers;

            return info;
        }
    }
}
