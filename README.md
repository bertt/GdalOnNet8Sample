# GdalOnNetCoreSample

Sample of running GDAL on .NET Core

## Running

```
$ git clone https://github.com/bertt/GdalOnNetCoreSample.git
$ cd GdalOnNetCoreSample
$ dotnet build
$ dotnet run
```

Output:

```
SOURCE IsGeographic:1 IsProjected:0
DEST IsGeographic:0 IsProjected:1
x:646305.79995079 y:183948.498850677 z:0
x:661409.396274113 y:239546.729789271 z:0
```

## Dependencies

This sample uses the following packages:

- https://www.nuget.org/packages/Gdal.Core/2.3.0-beta-023

- https://www.nuget.org/packages/Gdal.Core.WindowsRuntime/2.3.0-beta-023 

There is another package for running on Linux:

- https://www.nuget.org/packages/Gdal.Core.LinuxRuntime/2.3.0-beta-023

## Running on Linux

Trying to get this running on Linux. Changed GDalNetCore.csproj:

From

```
 <PackageReference Include="Gdal.Core.WindowsRuntime" Version="2.3.0-beta-023" />
```
 
To

```
 <PackageReference Include="Gdal.Core.LinuxRuntime" Version="2.3.0-beta-023" />
```

Install GDAL and libproj:

```
$ sudo apt-get install gdal-bin
$ sudo apt-get install libproj-dev
```

```
$ dotnet build
$ dotnet run

SOURCE IsGeographic:1 IsProjected:0
DEST IsGeographic:0 IsProjected:1
x:646305.79995079 y:183948.498850677 z:0
x:661409.396274113 y:239546.729789271 z:0

```


