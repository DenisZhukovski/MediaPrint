using System;
using System.Collections;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MediaPrint.Core
{
    public class JValue
    {
        private readonly object _value;
        private readonly JsonSerializerOptions _jsonSerializerSettings;

        public JValue(
            object value,
            JsonSerializerOptions jsonSerializerSettings)
        {
            _value = value;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public JsonNode Value()
        {
            switch (_value)
            {
                case null:
                    return null;
                case JsonNode node:
                    return node;
                case JsonMedia jsonMedia:
                    return jsonMedia._jObject;
                case IPrintable printable:
                    return  printable.ToJson()._jObject;
                case IEnumerable items when !(_value is string) && !(_value is IDictionary):
                    return ToJArray(items);
                case IDictionary dictionary:
                    return JsonNode.Parse(JsonSerializer.Serialize(dictionary, _jsonSerializerSettings));
            }

            if (_value is ValueType and not Enum)
            {
                return JsonValue.Create(_value);
            }

            return AsString();
        }

        private string AsString()
        {
            string valueAsString;
            if (_value is DateTime time)
            {
                valueAsString = time.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                valueAsString = _value.ToString();
            }
            return valueAsString;
        }

        private JsonArray ToJArray(IEnumerable items)
        {
            var array = new JsonArray();
            foreach (var item in items)
            {
                array.Add(new JValue(item, _jsonSerializerSettings).Value());
            }
            return array;
        }
    }
}
