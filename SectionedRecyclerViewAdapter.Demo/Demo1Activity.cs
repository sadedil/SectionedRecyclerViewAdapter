using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using System.Linq;

namespace SectionedRecyclerViewAdapter.Demo
{
    [Activity(Label = "Demo1 - Movies by date", Icon = "@drawable/icon")]
    public class Demo1Activity : Activity
    {
        public RecyclerView rcvMovies { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Demo1Activity);

            this.rcvMovies = FindViewById<RecyclerView>(Resource.Id.rcvMovies);
        }

        protected override void OnStart()
        {
            base.OnStart();

            var movies = MovieInfoRepository.
                GetMovies().
                GroupBy(m => m.Year < 1990 ? "old movies" : "new movies").
                OrderByDescending(g => g.Key).
                ToDictionary(g => g.Key, g => g.ToList());

            Demo1Adapter adapter = new Demo1Adapter(movies);

            rcvMovies.SetAdapter(adapter);
            rcvMovies.SetLayoutManager(new LinearLayoutManager(this) { Orientation = LinearLayoutManager.Vertical });
        }
    }

}

