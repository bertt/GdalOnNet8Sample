using System.Collections.Generic;

namespace GdalNetCore.Core
{
    public class GdalInfo
    {
        public string ReleaseName { get; set; }
        public string VersionNumber { get; set; }
        public string BuildInfo { get; set; }
        public List<string> OgrDrivers { get; set; }
        public List<string> GdalDrivers { get; set; }
    }
}
