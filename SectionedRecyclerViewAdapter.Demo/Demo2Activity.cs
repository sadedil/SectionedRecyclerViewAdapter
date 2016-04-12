using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using System.Linq;

namespace SectionedRecyclerViewAdapter.Demo
{
    [Activity(Label = "Demo2 - Dynaimc grouping", Icon = "@drawable/icon")]
    public class Demo2Activity : Activity
    {

        private RadioGroup rgSectionGroup { get; set; }

        private RadioButton rbtNoGrouping { get; set; }

        private RadioButton rbtRank { get; set; }

        private RadioButton rbtDecade { get; set; }

        private RadioButton rbtFirstLetter { get; set; }

        private RecyclerView rcvMovies { get; set; }

        private Demo2Adapter Adapter { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Demo2Activity);

            this.rgSectionGroup = FindViewById<RadioGroup>(Resource.Id.rgSectionGroup);
            this.rbtNoGrouping = FindViewById<RadioButton>(Resource.Id.rbtNoGrouping);
            this.rbtRank = FindViewById<RadioButton>(Resource.Id.rbtRank);
            this.rbtDecade = FindViewById<RadioButton>(Resource.Id.rbtDecade);
            this.rbtFirstLetter = FindViewById<RadioButton>(Resource.Id.rbtFirstLetter);
            this.rcvMovies = FindViewById<RecyclerView>(Resource.Id.rcvMovies);

            this.rgSectionGroup.CheckedChange += RgSectionGroup_CheckedChange;
        }

        protected override void OnStart()
        {
            base.OnStart();

            var movies = MovieInfoRepository.GetMovies();

            this.Adapter = new Demo2Adapter();
            this.Adapter.SetData(movies);
            this.Adapter.ItemClick += Adapter_ItemClick;

            rcvMovies.SetAdapter(this.Adapter);
            rcvMovies.SetLayoutManager(new LinearLayoutManager(this) { Orientation = LinearLayoutManager.Vertical });
        }

        private void Adapter_ItemClick(object sender, Demo2Adapter.ItemClickEventArgs e)
        {
            var movieInfo = this.Adapter.GetItem(e.IndexPath);
            this.ShowDialog("Selected movie info", $"Selected movie: {movieInfo.Name} ({movieInfo.Year})");
        }

        private void RgSectionGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            switch (e.CheckedId)
            {
                case Resource.Id.rbtNoGrouping:
                    this.Adapter.GroupingFunction = Demo2Adapter.DefaultGroupingFunction;
                    break;
                case Resource.Id.rbtRank:
                    this.Adapter.GroupingFunction = m => $"{m.Rank:00}";
                    break;
                case Resource.Id.rbtDecade:
                    this.Adapter.GroupingFunction = m => $"{m.Year / 10 * 10}'s";
                    break;
                case Resource.Id.rbtFirstLetter:
                    this.Adapter.GroupingFunction = m => m.Name?.Substring(0, 1);
                    break;
            }

            this.Adapter.RefreshLookup();
            this.Adapter.NotifyDataSetChanged();
        }

        private void ShowDialog(string title, string message)
        {
            using (AlertDialog.Builder alert = new AlertDialog.Builder(this))
            {
                alert.SetTitle(title);
                alert.SetMessage(message);

                using (Dialog dialog = alert.Create())
                    dialog.Show();
            }
        }

    }
}