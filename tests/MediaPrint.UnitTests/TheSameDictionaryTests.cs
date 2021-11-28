using System.Collections.Generic;
using MediaPrint.Core;
using Xunit;

namespace MediaPrint.UnitTests
{
    public class TheSameDictionaryTests
    {
        [Fact]
        public void Equal_TheSameKeyValuePairs()
        {
            Assert.True(
                new TheSameDictionary(
                    new Dictionary<string, object>
                    {
                        { "Test",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) }
                    },
                    new Dictionary<string, object>
                    {
                        { "Test",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) }
                    }
                ).ToBool()
            );
        }

        [Fact]
        public void NotEqual_DifferentKeyValues()
        {
            Assert.True(
                new TheSameDictionary(
                    new Dictionary<string, object>
                    {
                        { "Test",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) }
                    },
                    new Dictionary<string, object>
                    {
                        { "Test",  new PrintableClass("Test Name2", new System.DateTime(2021, 1, 1)) }
                    }
                ).ToBool()
            );
        }

        [Fact]
        public void NotEqual_DifferentKeys()
        {
            Assert.True(
                new TheSameDictionary(
                    new Dictionary<string, object>
                    {
                        { "Test",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) }
                    },
                    new Dictionary<string, object>
                    {
                        { "Test2",  new PrintableClass("Test Name", new System.DateTime(2021, 1, 1)) }
                    }
                ).ToBool()
            );
        }
    }
}
