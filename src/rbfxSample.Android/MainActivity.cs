using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using Org.Libsdl.App;
using Urho3DNet;
using Exception = Java.Lang.Exception;
using Trace=System.Diagnostics.Trace;

namespace rbfxSample
{
    [Activity(Label = "@string/app_name",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden,
        Theme = "@android:style/Theme.NoTitleBar.Fullscreen", HardwareAccelerated = true, MainLauncher = true)]
    public class MainActivity: UrhoActivity
    {
        private Urho3DInterop.SdlCallback callback;
        protected override void OnResume()
        {
            Trace.WriteLine("MainActivity.OnResume()");

            base.OnResume();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Trace.WriteLine("MainActivity.OnCreate()");

            callback = (Urho3DInterop.SdlCallback) (SdlMain);
            Urho3DInterop.SetExternalSdlMain(callback);
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        private int SdlMain(int n, IntPtr v)
        {
            Trace.WriteLine("MainActivity.SDLMain()");
            using (var context = new Urho3DNet.Context())
            {
                var options = new ApplicationOptions();
                //var assets = Android.OS.Environment.ExternalStorageDirectory.ToString();
                //options.EpResourcePrefixPaths = assets+";"+ assets+"/Assets";
                using (var application = new TestApp(context, options))
                {
                    return application.Run();
                }
            }
        }
    }
}