using GdalNetCore.Core;

namespace Gdal.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var kit = new GdalKit();
        var info = kit.GetGdalInfo();
        Assert.IsTrue(info.ReleaseName=="3.7.0");
        Assert.IsTrue(info.GdalDrivers.Count == 146);
        Assert.IsTrue(info.OgrDrivers.Count == 78);
    }
}