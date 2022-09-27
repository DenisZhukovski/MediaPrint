using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MediaPrint.UnitTests
{
    public class JsonMediaTests
    {
        private readonly ITestOutputHelper _output;

        public JsonMediaTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void StringsDataIntoJson()
        {
            Asserts.EqualJson(
                @"{
                    ""Test"" : ""Test String"",
                    ""Test2"" : ""Test String2""
                }",
                new JsonMedia()
                    .With("Test", "Test String")
                    .With("Test2", "Test String2")
                    .ToString(),
                _output
            );
        }

        [Fact]
        public void BoolDataIntoJson()
        {
            Asserts.EqualJson(
                @"{
                    ""IsTrue"" : true,
                    ""IsFalse"" : false
                }",
                new JsonMedia()
                    .With("IsTrue", true)
                    .With("IsFalse", false)
                    .ToString(),
                _output
            );
        }

        [Fact]
        public void ClassWithArrayIntoJson()
        {
            Asserts.EqualJson(
                @"{
                  ""Desciption"": ""Description1"",
                  ""Items"": [
                    {
                        ""Name"": ""Test1"",
                        ""Date"": ""2021-01-01T00:00:00""
                    },
                    {
                        ""Name"": ""Test2"",
                        ""Date"": ""2021-01-02T00:00:00""
                    }
                  ]
                }",
                new PrintableClassWithIPrintableArray(
                    "Description1",
                    new List<PrintableClass>
                    {
                        new PrintableClass("Test1", new DateTime(2021, 1, 1)),
                        new PrintableClass("Test2", new DateTime(2021, 1, 2)),
                    }
                ).ToString(),
                _output
            );
        }

        [Fact]
        public void ClassWithIPrintableIntoJson()
        {
            Asserts.EqualJson(
                @"{
                  ""Desciption"": ""Description1"",
                  ""Item"":
                  {
                    ""Name"": ""Test1"",
                    ""Date"": ""2021-01-01T00:00:00""
                  }
                }",
                new PrintableClassWithPrintableElement(
                    "Description1",
                    new PrintableClass("Test1", new DateTime(2021, 1, 1))
                ).ToString(),
                _output
            );
        }

        [Fact]
        public void DictionaryIntoJson()
        {
            Asserts.EqualJson(
                @"{
                    ""Desciption"": ""Description1"",
                    ""Items"": {
                        ""Driver"": ""Driver1"",
                        ""Order"": ""Order1""
                    }
                }",
                new PrintableClassWithDictionary(
                    "Description1",
                    new Dictionary<string, object>
                    {
                        { "Driver", "Driver1" },
                        { "Order", "Order1" }
                    }
                ).ToJson().ToString(),
                _output
            );
        }

        [Fact]
        public void IPrintableIntoJson()
        {
            Asserts.EqualJson(
                @"{
                    ""Test"": {
                        ""Name"": ""Name1"",
                        ""Date"": ""1983-09-22T00:00:00""
                    }
                }",
                new JsonMedia().With(
                    "Test",
                    new PrintableClass("Name1", new DateTime(1983, 09, 22))
                ).ToString(),
                _output
            );
        }

        [Fact]
        public void JsonMediaIntoJson()
        {
            Asserts.EqualJson(
                @"{
                    ""Test"": {
                        ""Date"": ""1983-09-22T00:00:00""
                    }
                }",
                new JsonMedia().With(
                    "Test",
                    new JsonMedia().With("Date", new DateTime(1983, 09, 22))
                ).ToString(),
                _output
            );
        }

        [Fact]
        public void JsonMediaPutOverride()
        {
            Asserts.EqualJson(
                @"{ ""Date"": ""1982-02-28T00:00:00"" }",
                 new JsonMedia()
                    .Put("Date", new DateTime(1983, 09, 22))
                    .Put("Date", new DateTime(1982, 02, 28))
                    .ToString(),
                _output
            );
        }

        [Fact]
        public void EnumDataIntoJson()
        {
            Asserts.EqualJson(
                @"{
                    ""FirstDayOfWeek"" : ""Monday"",
                    ""SecondDayOfWeek"" : ""Tuesday""
                }",
                new JsonMedia()
                    .With("FirstDayOfWeek", DayOfWeek.Monday)
                    .With("SecondDayOfWeek", DayOfWeek.Tuesday)
                    .ToString(),
                _output
            );
        }
    }
}
