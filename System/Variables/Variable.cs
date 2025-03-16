using System;

namespace Glitch9
{
    [Serializable]
    public abstract class Variable
    {
        public abstract object GetValue();
        public abstract void SetValue(object value);
    }
}