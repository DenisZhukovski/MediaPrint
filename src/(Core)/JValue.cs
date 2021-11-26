using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediaPrint.Core
{
    public class JValue
    {
        private readonly object _value;
        private readonly Formatting _formattig;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JValue(
            object value,
            Formatting formattig,
            JsonSerializerSettings jsonSerializerSettings)
        {
            _value = value;
            _formattig = formattig;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public object Value()
        {
            if (_value == null || _value is JToken)
            {
                return _value;
            }
            else if (_value is JsonMedia jsonMedia)
            {
               return jsonMedia._jObject;
            }
            else if (_value is IPrintable printable)
            {
                return  printable.ToJson()._jObject;
            }
            else
            {
                if (_value is IEnumerable items && !(_value is string) && !(_value is IDictionary))
                {
                    return ToJArray(items);
                }
                else if (_value is IDictionary dictionary)
                {
                    return JObject.Parse(JsonConvert.SerializeObject(dictionary, _formattig, _jsonSerializerSettings));
                }
                else
                {
                    return AsString();
                }
            }
        }

        private string AsString()
        {
            string valueAsString;
            if (_value is DateTime)
            {
                valueAsString = ((DateTime)_value).ToString(_jsonSerializerSettings.DateFormatString);
            }
            else
            {
                valueAsString = _value.ToString();
            }
             return valueAsString;
        }

        private JArray ToJArray(IEnumerable items)
        {
            var array = new JArray();
            foreach (var item in items)
            {
                array.Add(new JValue(item, _formattig, _jsonSerializerSettings).Value());
            }
            return array;
        }
    }
}
