using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Java.Lang;
using Org.Libsdl.App;
using Urho3DNet;
using Application = Urho3DNet.Application;
using Context = Android.Content.Context;
using Exception = Java.Lang.Exception;
using Trace=System.Diagnostics.Trace;

namespace rbfxSample
{
    [Activity(Label = "@string/app_name",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden,
        Theme = "@android:style/Theme.NoTitleBar.Fullscreen", HardwareAccelerated = true, MainLauncher = true)]
    public class MainActivity: UrhoActivity
    {
        protected override void OnResume()
        {
            Trace.WriteLine("MainActivity.OnResume()");

            base.OnResume();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Trace.WriteLine("MainActivity.OnCreate()");

            Launcher.Run(_=>new DemoApplication(_, new ApplicationOptions()));

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}