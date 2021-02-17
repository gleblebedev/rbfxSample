using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Urho3DNet;
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
            BuildMap();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            //UIApplication.Main(args, null, "AppDelegate");
            SDL_UIKitRunApp(0, IntPtr.Zero, SdlMain);

            _application.Dispose();

            _context.Dispose();
        }

        static void BuildMap()
        {
            var factories = new Dictionary<StringHash, Func<Context, Urho3DNet.Object>>();
            factories[new StringHash(typeof(Input))] = (c) => new Input(c);
            factories[new StringHash(typeof(Engine))] = (c) => new Engine(c);
            factories[new StringHash(typeof(Renderer))] = (c) => new Renderer(c);
            factories[new StringHash(typeof(Serializable))] = (c) => new Serializable(c);
            factories[new StringHash(typeof(Animatable))] = (c) => new Animatable(c);
            factories[new StringHash(typeof(Node))] = (c) => new Node(c);
            factories[new StringHash(typeof(Scene))] = (c) => new Scene(c);
            //factories[new StringHash(typeof(PhysicsWorld))] = (c) => new PhysicsWorld(c);
            factories[new StringHash(typeof(Component))] = (c) => new Component(c);
            //factories[new StringHash(typeof(RigidBody))] = (c) => new RigidBody(c);
            //factories[new StringHash(typeof(CollisionShape))] = (c) => new CollisionShape(c);
            //factories[new StringHash(typeof(KinematicCharacterController))] = (c) => new KinematicCharacterController(c);
            factories[new StringHash(typeof(Viewport))] = (c) => new Viewport(c);
            factories[new StringHash(typeof(Camera))] = (c) => new Camera(c);
            //factories[new StringHash(typeof(RotateObject))] = (c) => new RotateObject(c);
            factories[new StringHash(typeof(DebugHud))] = (c) => new DebugHud(c);
            factories[new StringHash(typeof(Zone))] = (c) => new Zone(c);
            factories[new StringHash(typeof(LogicComponent))] = (c) => new LogicComponent(c);
            factories[new StringHash(typeof(Octree))] = (c) => new Octree(c);
            factories[new StringHash(typeof(Drawable))] = (c) => new Drawable(c);
            //factories[new StringHash(typeof(RenderPath))] = (c) => new RenderPath(c);

        }

        [MonoPInvokeCallback(typeof(SdlCallback))]
        public static int SdlMain(int argn, IntPtr argv)
        {
            Trace.WriteLine("MainActivity.SDLMain()");
            _context = new Urho3DNet.Context();
            {
                var options = new ApplicationOptions();
                options.Windowed = true;
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