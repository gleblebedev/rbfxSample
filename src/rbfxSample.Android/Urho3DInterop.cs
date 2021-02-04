using System;
using System.Runtime.InteropServices;

namespace rbfxSample
{
    public static class Urho3DInterop
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int SdlCallback(int argn, IntPtr argv);

        [DllImport("Urho3D", CharSet = CharSet.Ansi, EntryPoint = "SetExternalSdlMain", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetExternalSdlMain(SdlCallback callback);
    }
}