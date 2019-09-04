using System;

namespace RFDLib.Time
{
    public static class Time
    {
        static System.Diagnostics.Stopwatch _SW = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// Get time since start in milliseconds.
        /// </summary>
        /// <returns></returns>
        public static UInt64 GetTicks()
        {
            if (!_SW.IsRunning)
            {
                _SW.Start();
            }
            return (UInt64)_SW.ElapsedMilliseconds;
        }

        public static UInt64 GetTicksSince(UInt64 Previous)
        {
            return GetTicks() - Previous;
        }
    }
}