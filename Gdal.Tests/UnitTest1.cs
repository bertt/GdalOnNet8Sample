using GdalNetCore.Core;

namespace Gdal.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var kit = new GdalKit();
        var info = kit.GetGdalInfo();
        Assert.That(info.ReleaseName=="3.9.0");
        Assert.That(info.GdalDrivers.Count == 150);
        Assert.That(info.OgrDrivers.Count == 81);
    }
}