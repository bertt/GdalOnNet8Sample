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
        Console.WriteLine("Gdal drivers: " + info.GdalDrivers.Count);
        Assert.That(info.GdalDrivers.Count == 150);
        Console.WriteLine("Ogr drivers: " + info.OgrDrivers.Count);
        Assert.That(info.OgrDrivers.Count == 81);
    }
}