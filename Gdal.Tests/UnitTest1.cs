using GdalNetCore.Core;

namespace Gdal.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var kit = new GdalKit();
        var info = kit.GetGdalInfo();
        Assert.IsTrue(info.ReleaseName=="3.8.3");
        Assert.IsTrue(info.GdalDrivers.Count == 147);
        Assert.IsTrue(info.OgrDrivers.Count == 80);
    }
}