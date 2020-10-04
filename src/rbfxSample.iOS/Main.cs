using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ObjCRuntime;
using UIKit;

namespace rbfxSample.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            //UIApplication.Main(args, null, "AppDelegate");
            SDL_UIKitRunApp(0, IntPtr.Zero, SdlMain);
        }

        [MonoPInvokeCallback(typeof(SdlCallback))]
        public static int SdlMain(int argn, IntPtr argv)
        {
            Trace.WriteLine("MainActivity.SDLMain()");
            using (var context = new Urho3DNet.Context())
            {
                var options = new ApplicationOptions();
                //var assets = Android.OS.Environment.ExternalStorageDirectory.ToString();
                //options.EpResourcePrefixPaths = assets+";"+ assets+"/Assets";
                using (var application = new DemoApplication(context, options))
                {
                    return application.Run();
                }
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [MonoNativeFunctionWrapper]
        public delegate int SdlCallback(int argn, IntPtr argv);

        [DllImport("__Internal", EntryPoint = "SDL_UIKitRunApp")]
        public static extern int SDL_UIKitRunApp(int argc, IntPtr argv, SdlCallback callback);
    }
}