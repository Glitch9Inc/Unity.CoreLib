using System;

namespace Glitch9
{
    [Flags]
    public enum LogType
    {
        Log = 1 << 0,
        Warning = 1 << 1,
        Error = 1 << 2,
        Exception = 1 << 3,
        Native = 1 << 4,
        NativeWarning = 1 << 5,
        NativeError = 1 << 6,
    }
}