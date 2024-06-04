using System;
using UnityEditor.IMGUI.Controls;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract class ExtendedTreeViewItem<TTreeViewItem, TData, TFilter> : TreeViewItem
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TData : class, ITreeViewData<TData>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
    {
        public TData Data { get; private set; }

        protected ExtendedTreeViewItem(int id, int depth, string displayName, TData data) : base(id, depth, displayName)
        {
            Data = data;
        }

        public bool IsFiltered(TFilter filter)
        {
            Validate.Argument.NotNull(filter);
            return filter.IsFiltered(Data);
        }

        public abstract int CompareTo(TTreeViewItem anotherItem, int columnIndex, bool ascending);
        public abstract bool Search(string searchString);


        protected int CompareByString(bool ascending, TTreeViewItem anotherItem, Func<TData, string> selector)
        {
            return ascending ? string.CompareOrdinal(selector(Data), selector(anotherItem.Data)) : string.CompareOrdinal(selector(anotherItem.Data), selector(Data));
        }

        protected int CompareByInt(bool ascending, TTreeViewItem anotherItem, Func<TData, int> selector)
        {
            return ascending ? selector(Data).CompareTo(selector(anotherItem.Data)) : selector(anotherItem.Data).CompareTo(selector(Data));
        }

        protected int CompareByFloat(bool ascending, TTreeViewItem anotherItem, Func<TData, float> selector)
        {
            return ascending ? selector(Data).CompareTo(selector(anotherItem.Data)) : selector(anotherItem.Data).CompareTo(selector(Data));
        }

        protected int CompareByBool(bool ascending, TTreeViewItem anotherItem, Func<TData, bool> selector)
        {
            return ascending ? selector(Data).CompareTo(selector(anotherItem.Data)) : selector(anotherItem.Data).CompareTo(selector(Data));
        }

        protected int CompareByDateTime(bool ascending, TTreeViewItem anotherItem, Func<TData, DateTime?> selector)
        {
            DateTime? firstDate = selector(Data);
            DateTime? secondDate = selector(anotherItem.Data);

            // Handle nulls
            if (!firstDate.HasValue && !secondDate.HasValue)
                return 0; // Both are null, hence equal
            if (!firstDate.HasValue)
                return -1; // Null is considered less than any value
            if (!secondDate.HasValue)
                return 1; // Any value is considered greater than null

            // If both are not null, compare them
            return ascending ? firstDate.Value.CompareTo(secondDate.Value) : secondDate.Value.CompareTo(firstDate.Value);
        }

        protected int CompareByUnixTime(bool ascending, TTreeViewItem anotherItem, Func<TData, UnixTime?> selector)
        {
            UnixTime? firstTime = selector(Data);
            UnixTime? secondTime = selector(anotherItem.Data);

            // Handle nulls
            if (!firstTime.HasValue && !secondTime.HasValue)
                return 0; // Both are null, hence equal
            if (!firstTime.HasValue)
                return -1; // Null is considered less than any value
            if (!secondTime.HasValue)
                return 1; // Any value is considered greater than null

            // If both are not null, compare them
            return ascending ? firstTime.Value.CompareTo(secondTime.Value) : secondTime.Value.CompareTo(firstTime.Value);
        }
    }
}