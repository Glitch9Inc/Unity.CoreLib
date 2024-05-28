using System.Text;
using UnityEngine.Pool;

namespace Glitch9
{
    public static class StringBuilderPool
    {
        internal static readonly ObjectPool<StringBuilder> Pool = new(() => new StringBuilder(), null, sb => sb.Clear());

        public static StringBuilder Get() => Pool.Get();
        public static PooledObject<StringBuilder> Get(out StringBuilder value) => Pool.Get(out value);
        public static void Release(StringBuilder toRelease) => Pool.Release(toRelease);
    }
}