using System;
using Urho3DNet;

namespace rbfxSample.UniversalWindows
{
    internal class Program
    {
        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        [MTAThread]
        private static void Main()
        {
            Urho3DInterop.SDL_WinRTRunApp(SdlMain, IntPtr.Zero);

            //var exclusiveViewApplicationSource = new AppViewSource();
            //CoreApplication.Run(exclusiveViewApplicationSource);
        }

        static int SdlMain(int argn, IntPtr argv)
        {
            var o = new ApplicationOptions();
            using (var context = new Context())
            {
                using (var application = new DemoApplication(context, o))
                {
                    application.Run();
                }
            }
            return 0;
        }
    }
}