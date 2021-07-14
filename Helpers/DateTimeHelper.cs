namespace Helpers
{
    public static class DateTimeHelper
    {
        private static readonly System.DateTime Jan1St1970;

        static DateTimeHelper()
        {
            Jan1St1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        }

        public static long CurrentUnixTimeMillis()
        {
            return (long)(System.DateTime.UtcNow - Jan1St1970).TotalMilliseconds;
        }
    }
}