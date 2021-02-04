using System;
using System.Runtime.InteropServices;

namespace rbfxSample.UniversalWindows
{
    public static class Urho3DInterop
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int SdlCallback(int argn, IntPtr argv);

        [DllImport("Urho3D", CharSet = CharSet.Ansi, EntryPoint = "SDL_WinRTRunApp", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_WinRTRunApp(SdlCallback callback, IntPtr windowHandler);
    }
}