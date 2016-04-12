using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace SectionedRecyclerViewAdapter.Demo
{
    [Activity(Label = "SectionedRecyclerViewAdapter.Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnDemo1 = FindViewById<Button>(Resource.Id.btnDemo1);
            Button btnDemo2 = FindViewById<Button>(Resource.Id.btnDemo2);

            btnDemo1.Click += delegate
            {
                this.StartActivity(new Intent(this, typeof(Demo1Activity)));
            };

            btnDemo2.Click += delegate
            {
                this.StartActivity(new Intent(this, typeof(Demo2Activity)));
            };

        }
    }
}

