# MediaPrint
<h3 align="center">
   
  [![NuGet](https://img.shields.io/nuget/v/MediaPrint.svg)](https://www.nuget.org/packages/MediaPrint/) 
  [![Downloads](https://img.shields.io/nuget/dt/MediaPrint.svg)](https://www.nuget.org/MediaPrint/)
  [![Stars](https://img.shields.io/github/stars/DenisZhukovski/MediaPrint?color=brightgreen)](https://github.com/DenisZhukovski/MediaPrint/stargazers) 
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) 
  [![Hits-of-Code](https://hitsofcode.com/github/deniszhukovski/mediaprint?branch=main)](https://hitsofcode.com/github/deniszhukovski/mediaprint?branch=main/view)
  [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_MediaPrint&metric=ncloc)](https://sonarcloud.io/dashboard?id=DenisZhukovski_MediaPrint)
  [![EO principles respected here](https://www.elegantobjects.org/badge.svg)](https://www.elegantobjects.org)
  [![PDD status](https://www.0pdd.com/svg?name=deniszhukovski/mediaprint)](https://www.0pdd.com/p?name=deniszhukovski/mediaprint)
</h3>

The main goal is to help developers to keep their objects encapsulated and not show the state outside of their internal borders. It's been inspired by [yegor256](https://github.com/yegor256) and his ideas presented in [this webinar](https://www.youtube.com/watch?v=_Q0cNykXB04). The package helps objects to provide an internal state as data. Once an object needs to be printed out as data it has to implement the interface described below.

```cs
public interface IPrintable
{
    void PrintTo(IMedia media);
}
```
[IMedia](https://github.com/DenisZhukovski/MediaPrint/blob/main/src/(Core)/IMedia.cs) interface represents an output where the object will be printed out. It can be easily printed out into any data format but main idea is to let the objects to print themselves. The most interesting idea is to let UI pages implement *IMedia* interface and bind objects data directly on UI elements.
There are two printable formats supported out of the box:
- Json
- Dictionary

# As Json

The most used data format is Json. Once an object implements [IPrintable](https://github.com/DenisZhukovski/MediaPrint/blob/main/src/(Core)/IPrintable.cs) interface it can use ToJson extension method printing itself into json format.

```cs
var myFooJson = new Foo().ToJson(); // Foo class should implement IPrntable interface
```

# As Dictionary

Sometimes it's necessary to have an access to objects' data but located in memory. [DictionaryMedia](https://github.com/DenisZhukovski/MediaPrint/blob/main/src/DictionaryMedia.cs) class was introduced to manage this needs.

```cs
var myFooDictionary = new Foo().ToDictionary(); // Foo class should implement IPrntable interface
```
# Status

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_MediaPrint&metric=alert_status)](https://sonarcloud.io/dashboard?id=DenisZhukovski_MediaPrint)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_MediaPrint&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=DenisZhukovski_MediaPrint)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_MediaPrint&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=DenisZhukovski_MediaPrint)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=DenisZhukovski_MediaPrint&metric=security_rating)](https://sonarcloud.io/dashboard?id=DenisZhukovski_MediaPrint)
