using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MediaPrint.UnitTests
{
    public class DictionaryMediaTests
    {
        private readonly ITestOutputHelper _output;

        public DictionaryMediaTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ParsesString()
        {
            Assert.Equal(
                "Test String",
                new DictionaryMedia()
                    .With("Test", "Test String")
                    .Value<string>("Test")
            );
        }

        [Fact]
        public void ParsesStringForLazyObjects()
        {
            var lazyObject = new PrintableClass("Test Name", new DateTime(2020, 12, 30));
            Assert.Equal(
                lazyObject.ToString(),
                new DictionaryMedia()
                    .With("Test", lazyObject)
                    .Value<string>("Test")
            );
        }

        [Fact]
        public void ParsesEnum()
        {
            Assert.Equal(
                 TestEnum.Test1,
                 new DictionaryMedia()
                    .With("Test", TestEnum.Test1)
                    .Value<TestEnum>("Test")
            );
        }

        [Fact]
        public void ParsesEnumFromString()
        {
            Assert.Equal(
                 TestEnum.Test1,
                 new DictionaryMedia()
                    .With("Test", "Test1")
                    .Value<TestEnum>("Test")
            );
        }

        [Fact]
        public void ParsesEnumFromInt()
        {
            Assert.Equal(
                 TestEnum.Test1,
                 new DictionaryMedia()
                    .With("Test", 1)
                    .Value<TestEnum>("Test")
            );
        }

        [Fact]
        public void ContainsKey()
        {
            Assert.True(
                new DictionaryMedia()
                   .With("Test", 1)
                   .Contains("Test")
            );
        }

        [Fact]
        public void ValueOrDefault_ReturnsEmptyStringForString_WhenDoesNotExist()
        {
            Assert.Equal(
                string.Empty,
                new DictionaryMedia()
                   .With("Test", 1)
                   .ValueOrDefault<string>("NotExisting")
            );
        }

        [Fact]
        public void ValueOrDefault_ReturnsNullForObject_WhenDoesNotExist()
        {
            Assert.Null(
                new DictionaryMedia()
                   .With("Test", 1)
                   .ValueOrDefault<PrintableClass>("NotExisting")
            );
        }

        [Fact]
        public void ValueOrDefault_ReturnsMinDateTimeForDateTime_WhenDoesNotExist()
        {
            Assert.Equal(
                DateTime.MinValue,
                new DictionaryMedia()
                   .With("Test", 1)
                   .ValueOrDefault<DateTime>("NotExisting")
            );
        }

        [Fact]
        public void ParsesObjectFromString()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.Equal(
                 expected,
                 new DictionaryMedia().With(
                     "Test",
                     JsonConvert.SerializeObject((object)expected)
                 )
                 .Value<PrintableClass>("Test")
            );
        }

        [Fact]
        public void ConvertsTypeIfItsObjectType()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.Equal(
                 expected,
                 new DictionaryMedia()
                    .With("Test", expected)
                    .Value<PrintableClass>("Test")
            );
        }

        [Fact]
        public void ParsesFromJObject()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.Equal(
                 expected.Name,
                 new DictionaryMedia()
                    .With(
                        "Test",
                        JObject.Parse(JsonConvert.SerializeObject(expected))["Name"]
                    )
                    .Value<string>("Test")
            );
        }

        [Fact]
        public void CastDictionaryMediaFromIPrintable()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.Equal(
                 expected.Name,
                 new DictionaryMedia()
                    .With(
                        "Test",
                        expected
                    )
                    .Value<DictionaryMedia>("Test")
                    .Value<string>("Name")
            );
        }

        [Fact]
        public void DictionariesAreEqual_WhenContainTheSameValues()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.Equal(
                 new DictionaryMedia().With(
                     "Test",
                     expected
                 ),
                 new DictionaryMedia().With(
                     "Test",
                     expected
                 )
            );
        }

        [Fact]
        public void DictionariesAreNotEqual_WhenContainDifferentValues()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.NotEqual(
                 new DictionaryMedia().With(
                     "Test",
                     expected
                 ),
                 new DictionaryMedia()
                    .With("Test", expected)
                    .With("SomeNumber", 1)
            );
        }

        [Fact]
        public void DictionariesAreNotEqual_WhenContainDifferentKeys()
        {
            var expected = new PrintableClass("Test Name", DateTime.Now);
            Assert.NotEqual(
                 new DictionaryMedia().With(
                     "Test1",
                     expected
                 ),
                 new DictionaryMedia()
                    .With("Test2", expected)
            );
        }

        [Fact]
        public void ToStringInJsonFormat()
        {
            Asserts.EqualJson(
               @"{
                    ""TEST"" : ""Test String"",
                    ""TEST2"" : ""Test String2""
                }",
               new DictionaryMedia()
                   .With("Test", "Test String")
                   .With("Test2", "Test String2")
                   .ToString(),
               _output
           );
        }
    }
}
