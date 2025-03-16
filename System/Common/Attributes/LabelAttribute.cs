using System;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class LabelAttribute : PropertyAttribute
    {
        public string Name { get; private set; }

        public LabelAttribute(string name)
        {
            Name = name;
        }
    }
}
