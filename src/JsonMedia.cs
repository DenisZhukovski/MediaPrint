using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaPrint
{
    public class JsonMedia : IMedia
    {
        internal readonly JObject _jObject;
        private readonly Formatting _formattig;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonMedia()
            : this(DefaultJsonSerializerSettings())
        {
        }

        public JsonMedia(JsonSerializerSettings jsonSerializerSettings)
            : this(Formatting.Indented, jsonSerializerSettings)
        {
        }

        public JsonMedia(Formatting formattig, JsonSerializerSettings jsonSerializerSettings)
            : this(new JObject(), formattig, jsonSerializerSettings)
        {
        }

        public JsonMedia(JObject jObject)
            : this(jObject, Formatting.Indented, DefaultJsonSerializerSettings())
        {
        }

        public JsonMedia(
            JObject jObject,
            Formatting formattig,
            JsonSerializerSettings jsonSerializerSettings)
        {
            _jObject = jObject;
            _formattig = formattig;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public IMedia Put(string name, object value)
        {
            _jObject[name] = JToken.FromObject(
                new Core.JValue(value, _formattig, _jsonSerializerSettings).Value()
            );
            return this;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_jObject, _formattig, _jsonSerializerSettings);
        }

        private static JsonSerializerSettings DefaultJsonSerializerSettings()
        {
            return new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
        }
    }
}
