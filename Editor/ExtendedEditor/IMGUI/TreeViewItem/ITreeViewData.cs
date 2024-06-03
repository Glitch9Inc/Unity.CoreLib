using System;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public interface ITreeViewData<TSelf, in TFilter> : IEquatable<TSelf>
        where TSelf : class, ITreeViewData<TSelf, TFilter>
        where TFilter : class, ITreeViewFilter<TFilter, TSelf>
    {
        /// <summary>
        /// Unique identifier for this tree view data.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Used as a title for <see cref="TreeViewChildWindow"/>
        /// </summary>
        string Title { get; }

        
        bool SetFilter(TFilter filter);
        bool Search(string searchString);
    }
}