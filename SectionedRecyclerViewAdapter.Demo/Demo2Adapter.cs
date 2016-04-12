using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SectionedRecyclerViewAdapter.Demo
{
    /// <summary>
    /// Adapter for grouping movies dynamically
    /// </summary>
    public class Demo2Adapter : SectionedRecyclerViewAdapter<MovieInfo>
    {
        #region Properties & Constants

        /// <summary>
        /// Our simple and default grouping function
        /// It shows all items in single section without grouping
        /// </summary>
        public readonly static Func<MovieInfo, string> DefaultGroupingFunction = m => string.Empty;

        /// <summary>
        /// Our grouping function
        /// Can be set outside of this class
        /// </summary>
        public Func<MovieInfo, string> GroupingFunction { get; set; } = Demo2Adapter.DefaultGroupingFunction;

        /// <summary>
        /// Original data to be shown
        /// </summary>
        public List<MovieInfo> Movies { get; private set; }

        /// <summary>
        /// Groupped data for easy use in this adapter
        /// </summary>
        /// <remarks>
        /// You must be populate this lookup, after every change in the Movies list.
        /// RefreshLookup methods does this.
        /// </remarks>
        private ILookup<string, MovieInfo> MoviesLookup { get; set; }

        #endregion

        #region Events

        //Events region Not necessary for SectionedRecyclerViewAdapter Implementation
        //But we want to show, how can you use

        public class ItemClickEventArgs : EventArgs
        {
            public IndexPath IndexPath { get; set; }
        }

        public event EventHandler<ItemClickEventArgs> ItemClick;
        protected virtual void OnItemClick(ItemClickEventArgs e)
        {
            this.ItemClick?.Invoke(this, e);
        }
        #endregion

        /// <summary>
        /// Our simple constructor
        /// </summary>
        public Demo2Adapter()
        {
        }

        /// <summary>
        /// Change the data to be shown
        /// </summary>
        /// <param name="movies"></param>
        /// <remarks>
        /// Don't forget the call RefreshLookup method
        /// </remarks>
        public void SetData(List<MovieInfo> movies)
        {
            this.Movies = movies;
            this.RefreshLookup();
        }

        /// <summary>
        /// It rebuilds MoviesLookup based on Movies list.
        /// </summary>
        public void RefreshLookup()
        {
            this.MoviesLookup = this.Movies.OrderBy(this.GroupingFunction).ToLookup(this.GroupingFunction);
        }

        /// <summary>
        /// Show section header?
        /// </summary>
        /// <remarks>
        /// If the GroupingFunction is the default one, don't show the header
        /// </remarks>
        public override bool ShowHeader
        {
            get { return !this.GroupingFunction.Equals(DefaultGroupingFunction); }
        }

        /// <summary>
        /// How many sections are displayed?
        /// </summary>
        /// <returns></returns>
        public override int NumbersOfSections()
        {
            return this.MoviesLookup.Count;
        }


        /// <summary>
        /// Let's set each sections item count
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public override int RowsInSection(int section)
        {
            var group = this.MoviesLookup.ElementAt(section);
            return group.Count();
        }

        /// <summary>
        /// Get item with (section, position) pair
        /// </summary>
        /// <param name="indexPath"></param>
        /// <returns></returns>
        public override MovieInfo GetItem(IndexPath indexPath)
        {
            var group = this.MoviesLookup.ElementAt(indexPath.SectionIndex);
            return group.ElementAt(indexPath.ItemIndex.GetValueOrDefault());
        }

        /// <summary>
        /// Let's create view for Item template
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        /// <remarks>
        /// We use Android's internal Android.Resource.Layout.SimpleListItem2 resource.
        /// You can change this with your custom XML Layout
        /// </remarks>
        public override RecyclerView.ViewHolder OnCreateItemViewHolder(ViewGroup parent)
        {
            View row = LayoutInflater.
                From(parent.Context).
                Inflate(Android.Resource.Layout.SimpleListItem2, parent, false); //Or Custom Resource

            var viewHolder = new ItemViewHolder(row);

            viewHolder.txvOtherInfo.SetTextSize(Android.Util.ComplexUnitType.Sp, 10);
            viewHolder.txvOtherInfo.SetTextColor(Android.Graphics.Color.LightGray);

            return viewHolder;
        }

        /// <summary>
        /// Let's populate Item views
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="indexPath"></param>
        /// <remarks>
        /// You can set the events in this block
        /// </remarks>
        public override void OnBindItemViewHolder(RecyclerView.ViewHolder holder, IndexPath indexPath)
        {
            var viewHolder = (holder as ItemViewHolder);
            var movieInfo = this.GetItem(indexPath);

            viewHolder.ClickAction = () => this.OnItemClick(new ItemClickEventArgs() { IndexPath = indexPath });
            viewHolder.txvMovieName.Text = movieInfo.Name;
            viewHolder.txvOtherInfo.Text = $"Rank: {movieInfo.Rank}, Year: {movieInfo.Year}";
        }

        /// <summary>
        /// Let's create view for Section template
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        /// <remarks>
        /// We use Android's internal Android.Resource.Layout.SimpleListItem2 resource.
        /// You can change this with your custom XML Layout
        /// </remarks>
        public override RecyclerView.ViewHolder OnCreateSectionViewHolder(ViewGroup parent)
        {
            View row = LayoutInflater.
                From(parent.Context).
                Inflate(Android.Resource.Layout.SimpleListItem2, parent, false); //Or Custom Resource

            var viewHolder = new SectionViewHolder(row);

            viewHolder.txvGroupName.SetTextSize(Android.Util.ComplexUnitType.Sp, 30);
            viewHolder.txvItemCount.SetTextSize(Android.Util.ComplexUnitType.Sp, 10);

            viewHolder.txvGroupName.SetTextColor(Android.Graphics.Color.Red);
            viewHolder.txvItemCount.SetTextColor(Android.Graphics.Color.Pink);

            return viewHolder;
        }

        /// <summary>
        /// Let's populate Section views
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="sectionIndex"></param>
        public override void OnBindSectionViewHolder(RecyclerView.ViewHolder holder, int sectionIndex)
        {
            var viewHolder = (holder as SectionViewHolder);
            var group = this.MoviesLookup.ElementAt(sectionIndex);

            viewHolder.txvGroupName.Text = group.Key;
            viewHolder.txvItemCount.Text = $"{group.Count()} movie(s) listed in this section";
        }


        /// <summary>
        /// This internal class that holds ItemView's data
        /// </summary>
        /// <remarks>
        /// Text1 and Text2 Id's came from, Android.Resource.Layout.SimpleListItem2
        /// If you use custom XML layout in OnCreateItemViewHolder, you must change this Id's accordingly
        /// </remarks>
        internal class ItemViewHolder : RecyclerView.ViewHolder
        {
            public TextView txvMovieName { get; set; }
            public TextView txvOtherInfo { get; set; }

            public Action ClickAction { get; set; }

            public ItemViewHolder(View itemView) : base(itemView)
            {
                this.ItemView.Click += (sender, e) => { this.ClickAction?.Invoke(); };

                this.txvMovieName = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
                this.txvOtherInfo = itemView.FindViewById<TextView>(Android.Resource.Id.Text2);
            }

        }

        /// <summary>
        /// This internal class holds SectionView's data
        /// </summary>
        /// <remarks>
        /// Text1 and Text2 Id's came from, Android.Resource.Layout.SimpleListItem2
        /// If you use custom XML layout in OnCreateItemViewHolder, you must change this Id's accordingly
        /// </remarks>
        internal class SectionViewHolder : RecyclerView.ViewHolder
        {
            public TextView txvGroupName { get; set; }

            public TextView txvItemCount { get; set; }

            public SectionViewHolder(View itemView) : base(itemView)
            {
                this.txvGroupName = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
                this.txvItemCount = itemView.FindViewById<TextView>(Android.Resource.Id.Text2);
            }
        }

    }
}