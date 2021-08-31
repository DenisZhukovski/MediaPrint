using System;
using MediaPrint.UnitTests.Extensions;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace MediaPrint.UnitTests
{
    public static class Asserts
    {
        public static void EqualJson(string expectedJson, string actualJson, ITestOutputHelper output = null)
        {
            expectedJson = expectedJson.NoNewLines();
            actualJson = actualJson.NoNewLines();
            JObject expected = JObject.Parse(expectedJson);
            JObject actual = JObject.Parse(actualJson);
            if (output != null)
            {
                output.WriteLine("Platform:" + Environment.OSVersion.Platform.ToString());
                output.WriteLine("Expected:" + expectedJson);
                output.WriteLine("Actual:" + actualJson);
            }

            Xunit.Assert.Equal(expected, actual, JToken.EqualityComparer);
        }
    }
}
