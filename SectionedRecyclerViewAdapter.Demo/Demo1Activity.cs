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

            //Let's make a Dictionary like this.
            //
            //| Key        | Value                                          | Year | Rank  |
            //|----------- |----------------------------------------------- |------|------ |
            //| old movies | The Godfather                                  | 1972 | 2     |
            //|            | 12 Angry Man                                   | 1957 | 6     |
            //|            | Fight Club                                     | 1979 | 10    |
            //|            | Star Wars: Episode V - The Empire Strikes Back | 1980 | 12    |
            //| new movies | The Shawshank Redemption                       | 1994 | 1     |
            //|            | The Dark Knight                                | 2008 | 4     |
            //|            | Schindler's List                               | 1993 | 5     |
            //|            | Pulp Fiction                                   | 1994 | 7     |
            var movies = MovieInfoRepository.
                GetMovies().
                GroupBy(m => !m.Year.HasValue ? "not specified" : m.Year < 1990 ? "old movies" : "new movies").
                OrderByDescending(g => g.Key).
                ToDictionary(g => g.Key, g => g.ToList());

            Demo1Adapter adapter = new Demo1Adapter(movies);

            rcvMovies.SetAdapter(adapter);
            rcvMovies.SetLayoutManager(new LinearLayoutManager(this) { Orientation = LinearLayoutManager.Vertical });
        }
    }

}

