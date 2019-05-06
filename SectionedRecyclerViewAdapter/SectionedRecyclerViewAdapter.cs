using System;
using System.ComponentModel;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;


namespace SectionedRecyclerViewAdapter {
    public abstract class SectionedRecyclerViewAdapter<T> : RecyclerView.Adapter {

        public enum ItemType {
            Section, Item
        }

        public class IndexPath {
            public int OriginalPosition { get; set; }
            public int SectionIndex { get; set; }
            public int? ItemIndex { get; set; }

            public ItemType ItemType {
                get {
                    return this.ItemIndex.HasValue ? ItemType.Item : ItemType.Section;
                }
            }
        }

        public virtual int NumbersOfSections() { return 1; }

        public abstract int RowsInSection(int section);

        public abstract void OnBindItemViewHolder(RecyclerView.ViewHolder holder, IndexPath indexPath);

        public abstract RecyclerView.ViewHolder OnCreateItemViewHolder(ViewGroup parent);

        public virtual void OnBindSectionViewHolder(RecyclerView.ViewHolder holder, int section) { }

        public virtual RecyclerView.ViewHolder OnCreateSectionViewHolder(ViewGroup parent) { return null; }

        public abstract T GetItem(IndexPath indexPath);

        public virtual bool ShowHeader { get { return true; } }

        private IndexPath GetIndexPathWithPosition(int position) {
            var counter = 0;
            var numbersOfSections = this.NumbersOfSections();
            var result = new IndexPath() { OriginalPosition = position };

            for (int i = 0; i < numbersOfSections; i++) {
                result.SectionIndex = i;
                if (this.ShowHeader) {
                    result.ItemIndex = null;
                    if (counter++ == position) break;
                } else {
                    result.ItemIndex = 0;
                }
                for (int j = 0; j < this.RowsInSection(i); j++) {
                    result.ItemIndex = j;
                    if (counter++ == position) {
                        i = numbersOfSections - 1; //break outer loop
                        break;
                    }
                }
            }
            if (counter > 0 && counter <= position)
                throw new IndexOutOfRangeException();
            return result;
        }

        public override int ItemCount {
            get {
                var numbersOfSections = this.NumbersOfSections();
                return (this.ShowHeader ? numbersOfSections : 0) +
                       Enumerable.Range(0, numbersOfSections).Select(i => this.RowsInSection(i)).Sum();
            }
        }

        public override int GetItemViewType(int position) {
            return (int)this.GetIndexPathWithPosition(position).ItemType;
        }

        #pragma warning disable 809

        [Obsolete("Do not use this method directly", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            var indexPath = this.GetIndexPathWithPosition(position);

            switch (indexPath.ItemType) {
                case ItemType.Section:
                    this.OnBindSectionViewHolder(holder, indexPath.SectionIndex);
                    break;
                case ItemType.Item:
                    this.OnBindItemViewHolder(holder, indexPath);
                    break;
            }
        }

        [Obsolete("Do not use this method directly", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            switch ((ItemType)viewType) {
                case ItemType.Section:
                    return this.OnCreateSectionViewHolder(parent);
                case ItemType.Item:
                    return this.OnCreateItemViewHolder(parent);
            }
            return null;
        }

        #pragma warning restore 809
    }
}
