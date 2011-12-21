using System;

namespace Ebuy
{
    public static class Clock
    {
        internal static Func<DateTime> NowThunk = () => DateTime.UtcNow;

        public static DateTime Now
        {
            get { return NowThunk(); }
        }
    }
}
