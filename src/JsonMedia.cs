using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using MediaPrint.Core;

namespace MediaPrint
{
    public class JsonMedia : IMedia
    {
        internal readonly JsonObject _jObject;
        private readonly JsonSerializerOptions _jsonSerializerSettings;

        public JsonMedia()
            : this(DefaultJsonSerializerSettings())
        {
        }

        public JsonMedia(JsonSerializerOptions jsonSerializerSettings)
            : this(new JsonObject(), jsonSerializerSettings)
        {
        }

        public JsonMedia(JsonObject jObject)
            : this(jObject, DefaultJsonSerializerSettings())
        {
        }

        public JsonMedia(
            JsonObject jObject,
            JsonSerializerOptions jsonSerializerSettings)
        {
            _jObject = jObject;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public IMedia Put(string name, object value)
        {
            _jObject[name] = new JValue(value, _jsonSerializerSettings).Value();
            return this;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(_jObject, _jsonSerializerSettings);
        }

        private static JsonSerializerOptions DefaultJsonSerializerSettings()
        {
            return new JsonSerializerOptions { WriteIndented = true };
        }
    }
}
