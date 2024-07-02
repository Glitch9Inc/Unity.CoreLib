using System;

namespace Glitch9.UI.MaterialDesign
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MaterialColorAttribute : Attribute
    {
        public ColorRole Role { get; }
        public ColorType Type { get; }

        public MaterialColorAttribute(ColorRole role, ColorType type)
        {
            Role = role;
            Type = type;
        }
    }
}