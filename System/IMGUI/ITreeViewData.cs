using System;

namespace Glitch9
{
    public interface ITreeViewData<TSelf> : IEquatable<TSelf>
        where TSelf : class, ITreeViewData<TSelf>
    {
        /// <summary>
        /// Unique identifier for this tree view data.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Used as a title for <see cref="TreeViewChildWindow"/>
        /// </summary>
        string Title { get; }
    }
}