using System;

namespace Glitch9
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; protected set; }

        public DisplayNameAttribute()
        {
        }

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}