using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using MediaPrint.Core;

[assembly: InternalsVisibleTo("MediaPrint.UnitTests")]

namespace MediaPrint
{
    public sealed class  DictionaryMedia : IMedia,
        IEnumerable<KeyValuePair<string, object>>,
        IEquatable<Dictionary<string, object>>
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

        public DictionaryMedia(Dictionary<string, object> media)
            : this(media, CultureInfo.InvariantCulture)
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
            return object.ReferenceEquals(this, obj)
                || (obj is DictionaryMedia media
                && new TheSameDictionary(_media, media._media).ToBool());
        }

        public bool Equals(Dictionary<string, object> other)
        {
            return new TheSameDictionary(_media, other).ToBool();
        }

        public override int GetHashCode()
        {
#if NETSTANDARD2_0
            return _media.GetHashCode();
#else
            return HashCode.Combine(_media);
#endif
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _media.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _media.GetEnumerator();
        }
    }
}
