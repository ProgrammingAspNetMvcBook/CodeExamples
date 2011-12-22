using System;

namespace Ebuy
{
    public static class Clock
    {
        private static Func<DateTime> _nowThunk = () => DateTime.UtcNow;

        public static DateTime Now
        {
            get { return _nowThunk(); }
        }


        internal static ModifiedTimeLock ModifiedTime(DateTime time)
        {
            return new ModifiedTimeLock(() => time);
        }

        internal static ModifiedTimeLock ModifiedTime(Func<DateTime> time)
        {
            return new ModifiedTimeLock(time);
        }

        internal class ModifiedTimeLock : IDisposable
        {
            private readonly Func<DateTime> _previousThunk;

            public ModifiedTimeLock(Func<DateTime> time)
            {
                _previousThunk = _nowThunk;
                _nowThunk = time;
            }

            public void Dispose()
            {
                _nowThunk = _previousThunk;
            }
        }
    }
}
