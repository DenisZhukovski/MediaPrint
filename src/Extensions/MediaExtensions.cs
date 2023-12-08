using System;

namespace MediaPrint
{
    public static class MediaExtensions
    {
        public static T With<T>(this T media, string name, object value)
            where T : IMedia
        {
            media.Put(name, value);
            return media;
        }

        public static IMedia PutIf(this IMedia media, string name, Func<object> value, Func<bool> condition)
        {
            if (condition())
            {
                return media.Put(name, value());
            }
            return media;
        }

        public static T ValueOrDefault<T>(this DictionaryMedia media, string key, T defaultValue)
        {
            if (media.Contains(key))
            {
                return media.Value<T>(key);
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
