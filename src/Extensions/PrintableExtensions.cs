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
    }
}
