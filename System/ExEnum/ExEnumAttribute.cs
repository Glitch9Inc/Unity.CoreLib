using System;

namespace Glitch9
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExEnumAttribute : Attribute
    {
        public string DisplayName { get; protected set; }

        public ExEnumAttribute()
        {
        }

        public ExEnumAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}