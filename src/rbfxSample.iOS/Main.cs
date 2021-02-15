using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ObjCRuntime;
using UIKit;

namespace rbfxSample.iOS
{
    public class Application
    {
        static Urho3DNet.Context _context;
        static DemoApplication _application;

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            //UIApplication.Main(args, null, "AppDelegate");
            SDL_UIKitRunApp(0, IntPtr.Zero, SdlMain);

            _application.Dispose();

            _context.Dispose();
        }

        [MonoPInvokeCallback(typeof(SdlCallback))]
        public static int SdlMain(int argn, IntPtr argv)
        {
            Trace.WriteLine("MainActivity.SDLMain()");
            _context = new Urho3DNet.Context();
            {
                var options = new ApplicationOptions();
                //var assets = Android.OS.Environment.ExternalStorageDirectory.ToString();
                //options.EpResourcePrefixPaths = assets+";"+ assets+"/Assets";
                _application = new DemoApplication(_context, options);
                {
                    return _application.Run();
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