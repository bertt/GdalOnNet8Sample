using System;
using System.Collections.Generic;
using System.Text;

namespace GdalNetCore.Core
{
    public class GdalInfo
    {
        public string ReleaseName { get; set; }
        public string VersionNumber { get; set; }
        public string BuildInfo { get; set; }
        public List<string> Drivers { get; set; }
    }
}
