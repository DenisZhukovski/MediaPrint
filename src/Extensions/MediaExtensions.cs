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
    }
}
