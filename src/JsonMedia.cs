using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaPrint
{
    public class JsonMedia : IMedia
    {
        private readonly JObject _jObject;
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
            if (value == null)
            {
                _jObject[name] = null;
            }
            else if (value is JToken jObject)
            {
                _jObject[name] = jObject;
            }
            else if (value is JsonMedia jsonMedia)
            {
                _jObject[name] = jsonMedia._jObject;
            }
            else if (value is IPrintable printable)
            {
                _jObject[name] = printable.ToJson()._jObject;
            }
            else
            {
                if (value is IEnumerable items && !(value is string) && !(value is IDictionary))
                {
                    var array = new JArray();
                    foreach (var item in items)
                    {
                        array.Add(item.ToString());
                    }
                    _jObject[name] = array;
                }
                else if (value is IDictionary dictionary)
                {
                    _jObject[name] = JObject.Parse(JsonConvert.SerializeObject(dictionary, _formattig, _jsonSerializerSettings));
                }
                else
                {
                    string valueAsString;
                    if (value is DateTime)
                    {
                        valueAsString = ((DateTime)value).ToString(_jsonSerializerSettings.DateFormatString);
                    }
                    else
                    {
                        valueAsString = value.ToString();
                    }
                    _jObject.Add(new JProperty(name, valueAsString));
                }
            }
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
