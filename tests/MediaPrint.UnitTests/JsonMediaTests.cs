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
        public void PrintStringProperties()
        {
            Asserts.EqualJson(
                @"{
                    ""Test"" : ""Test String"",
                    ""Test2"" : ""Test String2""
                }",
                new JsonMedia()
                    .With("Test", "Test String")
                    .With("Test2", "Test String2")
                    .ToString()
            );
        }

        [Fact]
        public void PrintsComplexObjectAsJson()
        {
            Asserts.EqualJson(
                @"{
                  ""Desciption"": ""Description1"",
                  ""Items"": [
                    ""{\n  \""Name\"": \""Test1\"",\n  \""Date\"": \""2021-01-01T00:00:00\""\n}"",
                    ""{\n  \""Name\"": \""Test2\"",\n  \""Date\"": \""2021-01-02T00:00:00\""\n}""
                  ]
                }",
                new TestClassWithArray(
                    "Description1",
                    new List<TestClass>
                    {
                        new TestClass
                        {
                            Name = "Test1",
                            Date = new DateTime(2021, 1, 1),
                        },
                        new TestClass
                        {
                            Name = "Test2",
                            Date = new DateTime(2021, 1, 2),
                        },
                    }
                ).ToJson().ToString(),
                _output
            );
        }

        [Fact]
        public void PrintDictionary()
        {
            Asserts.EqualJson(
                @"{
                    ""Desciption"": ""Description1"",
                    ""Items"": {
                        ""Driver"": ""Driver1"",
                        ""Order"": ""Order1""
                    }
                }",
                new TestClassWithDictionary(
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

    }
}
