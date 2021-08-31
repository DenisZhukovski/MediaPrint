using System;
using System.Collections.Generic;
using System.Globalization;

namespace MediaPrint
{
    public class DictionaryMedia : IMedia
    {
        private readonly Dictionary<string, object> _media;
        private readonly IFormatProvider _formatProvider;

        public DictionaryMedia()
            : this(CultureInfo.InvariantCulture)
        {
        }

        public DictionaryMedia(IFormatProvider formatProvider)
            : this(new Dictionary<string, object>(), formatProvider)
        {
        }

        public DictionaryMedia(Dictionary<string, object> media, IFormatProvider formatProvider)
        {
            _media = media ?? throw new ArgumentNullException(nameof(media));
            _formatProvider = formatProvider ?? throw new ArgumentNullException(nameof(formatProvider));
        }

        public IEnumerable<string> Keys => _media.Keys;

        public IMedia Put(string name, object value)
        {
            _media[name.ToUpperInvariant()] = value;
            return this;
        }

        public T Value<T>(string name)
        {
            return new DictionaryValue<T>(
                _media[name.ToUpperInvariant()],
                _formatProvider
            ).Value;
        }

        public T ValueOrDefault<T>(string name)
        {
            if (Contains(name))
            {
                return new DictionaryValue<T>(
                    _media[name.ToUpperInvariant()],
                    _formatProvider
                ).Value;
            }
            return (typeof(T) == typeof(string))
                ? (T)(object)string.Empty
                : default(T);
        }

        public bool Contains(string name)
        {
            return _media.ContainsKey(name.ToUpperInvariant());
        }

        public override string ToString()
        {
            var jsonMedia = new JsonMedia();
            foreach (var key in _media.Keys)
            {
                jsonMedia.Put(key, _media[key]);
            }
            return jsonMedia.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is DictionaryMedia expected && _media.Count == expected._media.Count)
            {
                foreach (var key in expected.Keys)
                {
                    var isEqual = object.Equals(
                        expected.Value<object>(key),
                        this.ValueOrDefault<object>(key)
                    );
                    if (!isEqual)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
#if NETSTANDARD2_0
            return _media.GetHashCode();
#else
            return HashCode.Combine(_media);
#endif
        }
    }
}
