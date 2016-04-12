using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace SectionedRecyclerViewAdapter.Demo
{
    /// <summary>
    /// Adapter for Movies by Year
    /// </summary>
    public class Demo1Adapter : SectionedRecyclerViewAdapter<MovieInfo>
    {
        /// <summary>
        /// Data to be shown
        /// </summary>
        private Dictionary<string, List<MovieInfo>> Movies { get; set; }

        /// <summary>
        /// Our simple constructor
        /// </summary>
        /// <param name="movies"></param>
        public Demo1Adapter(Dictionary<string, List<MovieInfo>> movies)
        {
            this.Movies = movies;
        }

        /// <summary>
        /// Show section header?
        /// </summary>
        public override bool ShowHeader
        {
            get { return true; }
        }

        /// <summary>
        /// Our movies already grouped in Dictionary.
        /// So we know how many sections are required.
        /// </summary>
        /// <returns></returns>
        public override int NumbersOfSections()
        {
            return this.Movies.Keys.Count;
        }

        /// <summary>
        /// Let's set each sections item count
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public override int RowsInSection(int section)
        {
            var key = this.Movies.Keys.ElementAt(section);
            return this.Movies[key].Count();
        }

        /// <summary>
        /// Get item with (section, position) pair
        /// </summary>
        /// <param name="indexPath"></param>
        /// <returns></returns>
        public override MovieInfo GetItem(IndexPath indexPath)
        {
            var section = this.Movies.ElementAt(indexPath.SectionIndex);
            return section.Value[indexPath.ItemIndex.GetValueOrDefault()];
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
                Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);

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
        public override void OnBindItemViewHolder(RecyclerView.ViewHolder holder, IndexPath indexPath)
        {
            var viewHolder = (holder as ItemViewHolder);
            var movieInfo = this.GetItem(indexPath);

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
                Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);

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
            var section = this.Movies.ElementAt(sectionIndex);

            viewHolder.txvGroupName.Text = section.Key;
            viewHolder.txvItemCount.Text = $"{section.Value.Count} movie(s) listed in this section";
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

            public ItemViewHolder(View itemView) : base(itemView)
            {
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