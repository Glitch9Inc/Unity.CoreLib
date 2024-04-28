using UnityEngine;

namespace Glitch9
{
    [System.Serializable]
    public abstract class Variable
    {
        public abstract object GetValue();
        public abstract void SetValue(object value);
    }
}