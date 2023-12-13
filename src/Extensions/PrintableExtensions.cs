namespace MediaPrint
{
    public static class PrintableExtensions
    {
        public static DictionaryMedia ToDictionary(this IPrintable printable)
        {
            var dictionary = new DictionaryMedia();
            printable.PrintTo(dictionary);
            return dictionary;
        }

        public static JsonMedia ToJson(this IPrintable printable)
        {
            var json = new JsonMedia();
            printable.PrintTo(json);
            return json;
        }

        public static T Value<T>(this IPrintable printable, string key)
        {
            return printable.ToDictionary().Value<T>(key);
        }

        public static T ValueOrDefault<T>(this IPrintable printable, string key)
        {
            return printable.ToDictionary().ValueOrDefault<T>(key, default);
        }

        public static T ValueOrDefault<T>(this IPrintable printable, string key, T defaultValue)
        {
            return printable.ToDictionary().ValueOrDefault(key, defaultValue);
        }

        public static bool HasValue(this IPrintable printable, string key)
        {
            return printable.ToDictionary().Contains(key);
        }
    }
}
