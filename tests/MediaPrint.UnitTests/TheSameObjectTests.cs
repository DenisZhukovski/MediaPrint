using System.Collections.Generic;
using MediaPrint.Core;
using Xunit;

namespace MediaPrint.UnitTests
{
    public class TheSameObjectTests
    {
        [Fact]
        public void Equal_WhenBothAreNull()
        {
            Assert.True(
                new TheSameObject(null, null).ToBool()
            );
        }

        [Fact]
        public void Equal_Objects()
        {
            Assert.True(
                new TheSameObject(
                    new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)),
                    new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1))
                ).ToBool()
            );
        }

        [Fact]
        public void Equal_Collections()
        {
            Assert.True(
                new TheSameObject(
                    new List<PrintableClass>
                    {
                        new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)),
                        new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)),
                    },
                    new List<PrintableClass>
                    {
                        new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)),
                        new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)),
                    }
                ).ToBool()
            );
        }

        [Fact]
        public void Equal_Dictionaries()
        {
            Assert.True(
                new TheSameObject(
                    new Dictionary<string, PrintableClass>
                    {
                        { "Test1",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) },
                        { "Test2",  new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)) }
                    },
                    new Dictionary<string, PrintableClass>
                    {
                        { "Test1",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) },
                        { "Test2",  new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)) }
                    }
                ).ToBool()
            );
        }

        [Fact]
        public void NotEqual_Objects()
        {
            Assert.False(
                new TheSameObject(
                    new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)),
                    new PrintableClass("Test Name2", new System.DateTime(2021, 1, 1))
                ).ToBool()
            );
        }

        [Fact]
        public void NotEqual_Collections()
        {
            Assert.False(
                new TheSameObject(
                    new List<PrintableClass>
                    {
                        new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)),
                        new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)),
                    },
                    new List<PrintableClass>
                    {
                        new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)),
                        new PrintableClass("Test Name4",  new System.DateTime(2021, 1, 2)),
                    }
                ).ToBool()
            );
        }

        [Fact]
        public void NotEqual_Dictionaries()
        {
            Assert.False(
                new TheSameObject(
                    new Dictionary<string, PrintableClass>
                    {
                        { "Test1",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) },
                        { "Test2",  new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)) }
                    },
                    new Dictionary<string, PrintableClass>
                    {
                        { "Test1",  new PrintableClass("Test Name1", new System.DateTime(2021, 1, 1)) },
                        { "Test4",  new PrintableClass("Test Name2",  new System.DateTime(2021, 1, 2)) }
                    }
                ).ToBool()
            );
        }
    }
}
