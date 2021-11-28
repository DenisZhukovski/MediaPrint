using System;
using System.Collections.Generic;
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
        public void ValueOrDefault_ReturnsObject_WhenExists()
        {
            Assert.Equal(
                new PrintableClass("Test1", new DateTime(2021, 1, 1)),
                new DictionaryMedia()
                   .With("Test", new PrintableClass("Test1", new DateTime(2021, 1, 1)))
                   .ValueOrDefault<PrintableClass>("Test")
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
        public void Equal_WhenContainTheSameValues()
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
        public void Equal_WhenTheSameReference()
        {
            var media = new DictionaryMedia().With(
                "Test",
                new PrintableClass("Test Name", DateTime.Now)
            );
            Assert.Equal(
                media,
                media
            );
        }

        [Fact]
        public void Equal_WhenContainTheSameValuesButEnumerable()
        {
            Assert.Equal(
                 new DictionaryMedia().With(
                     "Test",
                     new PrintableClassWithIPrintableArray(
                        "Description1",
                        new List<PrintableClass>
                        {
                            new PrintableClass("Test1", new DateTime(2021, 1, 1)),
                            new PrintableClass("Test2", new DateTime(2021, 1, 2))
                        }
                    )
                 ),
                 new DictionaryMedia().With(
                     "Test",
                     new PrintableClassWithIPrintableArray(
                        "Description1",
                        new List<PrintableClass>
                        {
                            new PrintableClass("Test1", new DateTime(2021, 1, 1)),
                            new PrintableClass("Test2", new DateTime(2021, 1, 2))
                        }
                    )
                 )
            );
        }

        [Fact]
        public void Equal_WhenTheSameDictionary()
        {
            Assert.True(
                 new DictionaryMedia().With(
                     "Test",
                     new PrintableClassWithIPrintableArray(
                        "Description1",
                        new List<PrintableClass>
                        {
                            new PrintableClass("Test1", new DateTime(2021, 1, 1)),
                            new PrintableClass("Test2", new DateTime(2021, 1, 2))
                        }
                    )
                 ).Equals(
                 new Dictionary<string, object>
                 {
                     { "TEST",
                         new PrintableClassWithIPrintableArray(
                             "Description1",
                             new List<PrintableClass>
                             {
                                 new PrintableClass("Test1", new DateTime(2021, 1, 1)),
                                 new PrintableClass("Test2", new DateTime(2021, 1, 2))
                             }
                         )
                     }
                 })
            );
        }

        [Fact]
        public void NotEqual_WhenContainDifferentValues()
        {
            Assert.NotEqual(
                 new DictionaryMedia().With(
                     "Test",
                     new PrintableClass("Test Name", new DateTime(2021, 1, 1))
                 ),
                 new DictionaryMedia()
                    .With("Test", new PrintableClass("Test Name", new DateTime(2021, 1, 1)))
                    .With("SomeNumber", 1)
            );
        }

        [Fact]
        public void NotEqual_WhenContainDifferentKeys()
        {
            Assert.NotEqual(
                 new DictionaryMedia().With(
                     "Test1",
                     new PrintableClass("Test Name", new DateTime(2021, 1, 1))
                 ),
                 new DictionaryMedia().With(
                     "Test2",
                     new PrintableClass("Test Name", new DateTime(2021, 1, 1))
                 )
            );
        }

        [Fact]
        public void ThrowsArgumentNullException_WhenFormatProviderIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DictionaryMedia((IFormatProvider)null)
            );
        }

        [Fact]
        public void ThrowsArgumentNullException_WhenMediaIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DictionaryMedia((Dictionary<string, object>)null)
            );
        }

        [Fact]
        public void ThrowsArgumentNullException_WhenBothArgumentsAreNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DictionaryMedia(null, null)
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
