using System;

namespace Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Gets the current Unix timestamp in milliseconds.
        /// </summary>
        /// <returns>The current Unix timestamp in milliseconds.</returns>
        public static long GetCurrentUnixTimeMillis()
        {
            return (long)(DateTimeOffset.UtcNow - DateTimeOffset.UnixEpoch).TotalMilliseconds;
        }
    }
}