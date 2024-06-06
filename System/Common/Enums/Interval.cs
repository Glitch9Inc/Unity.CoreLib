using System;

namespace Glitch9
{
    [Flags]
    public enum Interval
    {
        None = 0,
        Daily = 1 << 0,  // 1
        Weekly = 1 << 1, // 2
        Monthly = 1 << 2 // 4
    }
}