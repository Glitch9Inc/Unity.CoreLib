using System;
using UnityEditor.IMGUI.Controls;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract class ExtendedTreeViewItem<TTreeViewItem, TData, TFilter> : TreeViewItem
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TData : class, IData<TData>
        where TFilter : class, IFilter<TFilter, TData>
    {
        public TData Data { get; private set; }

        protected ExtendedTreeViewItem(int id, int depth, string displayName, TData data) : base(id, depth, displayName)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            Data = data;
        }

        public bool IsFiltered(TFilter filter)
        {
            Validate.Argument.NotNull(filter);
            return filter.IsFiltered(Data);
        }

        public abstract int CompareTo(TTreeViewItem anotherItem, int columnIndex, bool ascending);
        public abstract bool Search(string searchString);


        protected int CompareByString(bool ascending, TTreeViewItem anotherItem, Func<TTreeViewItem, string> selector)
        {
            return ascending ? string.CompareOrdinal(selector(this as TTreeViewItem), selector(anotherItem)) : string.CompareOrdinal(selector(anotherItem), selector(this as TTreeViewItem));
        }

        protected int CompareByInt(bool ascending, TTreeViewItem anotherItem, Func<TTreeViewItem, int> selector)
        {
            return ascending ? selector(this as TTreeViewItem).CompareTo(selector(anotherItem)) : selector(anotherItem).CompareTo(selector(this as TTreeViewItem));
        }

        protected int CompareByFloat(bool ascending, TTreeViewItem anotherItem, Func<TTreeViewItem, float> selector)
        {
            return ascending ? selector(this as TTreeViewItem).CompareTo(selector(anotherItem)) : selector(anotherItem).CompareTo(selector(this as TTreeViewItem));
        }

        protected int CompareByBool(bool ascending, TTreeViewItem anotherItem, Func<TTreeViewItem, bool> selector)
        {
            return ascending ? selector(this as TTreeViewItem).CompareTo(selector(anotherItem)) : selector(anotherItem).CompareTo(selector(this as TTreeViewItem));
        }

        protected int CompareByDateTime(bool ascending, TTreeViewItem anotherItem, Func<TTreeViewItem, DateTime?> selector)
        {
            DateTime? firstDate = selector(this as TTreeViewItem);
            DateTime? secondDate = selector(anotherItem);

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

        protected int CompareByUnixTime(bool ascending, TTreeViewItem anotherItem, Func<TTreeViewItem, UnixTime?> selector)
        {
            UnixTime? firstTime = selector(this as TTreeViewItem);
            UnixTime? secondTime = selector(anotherItem);

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