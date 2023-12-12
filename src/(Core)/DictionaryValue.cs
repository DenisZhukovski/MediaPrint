using System;
using System.Globalization;
using System.Text.Json;

namespace MediaPrint
{
    public class DictionaryValue<T>
    {
        private readonly object _value;
        private readonly IFormatProvider _formatProvider;

        public DictionaryValue(object value)
            : this(value, CultureInfo.InvariantCulture)
        {
        }

        public DictionaryValue(object value, IFormatProvider formatProvider)
        {
            _value = value;
            _formatProvider = formatProvider;
        }

        public T Value
        {
            get
            {
                if (_value == null || _value is T)
                {
                    return (T)_value;
                }

                Type type = typeof(T);
                if (type.IsEnum)
                {
                    return (T)Enum.Parse(type, _value.ToString());
                }

                if (_value is JsonElement element)
                {
                    return element.Deserialize<T>();
                }

                if (type == typeof(string))
                {
                    return (T)(object)_value.ToString();
                }

                if (type == typeof(DictionaryMedia) && _value is IPrintable printable)
                {
                    return (T)(object)printable.ToDictionary();
                }

                if (type.IsClass)
                {
                    return JsonSerializer.Deserialize<T>(_value.ToString());
                }

                return (T)Convert.ChangeType(_value, typeof(T), _formatProvider);
            }
        }
    }
}
