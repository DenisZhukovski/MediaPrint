using System;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace MediaPrint.UnitTests
{
    public static class Asserts
    {
        public static void EqualJson(string expectedJson, string actualJson, ITestOutputHelper output = null)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                expectedJson = expectedJson.Replace("\r\n", string.Empty).Replace("\n", string.Empty);
                actualJson = actualJson.Replace("\r\n", string.Empty).Replace("\n", string.Empty);
            }
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
