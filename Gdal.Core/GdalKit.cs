using System.Collections.Generic;
using System.Linq;
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

            var gdaldrivers = Gdal.GetDriverCount();


            var gdalDrivers = new List<string>();
            for (var i = 0; i < Gdal.GetDriverCount(); i++)
            {
                if (Gdal.GetDriver(i).GetMetadataItem("DCAP_RASTER", null) == "YES")
                {
                    var shortname = Gdal.GetDriver(i).ShortName;
                    gdalDrivers.Add(shortname);
                }
            }
            info.GdalDrivers = gdalDrivers;

            return info;
        }
    }
}
