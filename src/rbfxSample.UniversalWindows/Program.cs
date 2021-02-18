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
            Launcher.Run(_=> new DemoApplication(_, new ApplicationOptions()));
        }
    }
}