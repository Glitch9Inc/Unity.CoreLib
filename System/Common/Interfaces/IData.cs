using System;

namespace Glitch9
{
    public interface IData<TSelf> : IEquatable<TSelf>
        where TSelf : class, IData<TSelf>
    {
        /// <summary>
        /// Unique identifier for this data.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Name of this data.
        /// </summary>
        string Name { get; }
    }
}