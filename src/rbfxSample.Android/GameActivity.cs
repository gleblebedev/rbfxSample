using Android.App;
using Android.OS;
using Android.Runtime;
using Org.Libsdl.App;
using Trace = System.Diagnostics.Trace;

namespace rbfxSample
{
    [Activity(Label = "GameActivity", Theme = "@style/AppTheme")]
    public class GameActivity : SDLActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Trace.WriteLine("GameActivity.OnCreate()");
            
            base.OnCreate(savedInstanceState);
        }

      
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}