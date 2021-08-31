# MediaPrint
The main goal is to help developers to keep their objects encapsulated and not show the state outside of it internal borders. It's been inspired by [yegor256](https://github.com/yegor256) and his ideas presented in [this webinar](https://www.youtube.com/watch?v=_Q0cNykXB04). The package helps objects to provide an internal state as data. Once an object needs to be printed out as data it has to implement the interface described below.

```cs
public interface IPrintable
{
    void PrintTo(IMedia media);
}
```
[IMedia](https://github.com/DenisZhukovski/MediaPrint/blob/main/src/(Core)/IMedia.cs) interface represents an output where the object will be printed out. It can be easily printed out into any data format but main idea is to let the objects to print themselves. The most interesting idea is to let UI pages to implement *IMedia* interface and bind objects data directly on UI elements.
There are two printable formats are supported out of the box:
- Json
- Dictionary

# As Json

The most used data format is Json. Once an object implements [IPrintable](https://github.com/DenisZhukovski/MediaPrint/blob/main/src/(Core)/IPrintable.cs) interface it can use ToJson extension method printing itself into json format.

```cs
var myFooJson = new Foo().ToJson(); // Foo class should implement IPrntable interface
```

# As Dictionary

Sometimes its necessary to have an access to objects data but located in memory. [DictionaryMedia](https://github.com/DenisZhukovski/MediaPrint/blob/main/src/DictionaryMedia.cs) class is been introduced to manage this needs.

```cs
var myFooDictionary = new Foo().ToDictionary(); // Foo class should implement IPrntable interface
```
