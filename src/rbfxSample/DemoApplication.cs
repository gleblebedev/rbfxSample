using System.Diagnostics;
using System.IO;
using CommandLine.Text;
using ImGuiNet;
using Urho3DNet;

namespace rbfxSample
{
    class DemoApplication : Application
    {
        private readonly ApplicationOptions _options;
        private Scene _scene;
        private Viewport _viewport;
        private Node _camera;

        private CharacterController _characterController;
        //private Node _cube;
        //private Node _light;

        public DemoApplication(Context context, ApplicationOptions options) : base(context)
        {
            _options = options;
        }

        protected override void Dispose(bool disposing)
        {
            Context.Renderer.SetViewport(0, null);    // Enable disposal of viewport by making it unreferenced by engine.
            _viewport.Dispose();
            _scene.Dispose();
            _camera.Dispose();
            //_cube.Dispose();
            //_light.Dispose();
            base.Dispose(disposing);
        }

        public override void Setup()
        {
            var currentDir = Directory.GetCurrentDirectory();
            engineParameters_[Urho3D.EpFullScreen] = !_options.Windowed;
            if (_options.Windowed)
            {
                engineParameters_[Urho3D.EpWindowResizable] = true;
            }
            if (_options.Width.HasValue)
                engineParameters_[Urho3D.EpWindowWidth] = _options.Width.Value;
            if (_options.Height.HasValue)
            engineParameters_[Urho3D.EpWindowHeight] = _options.Height.Value;
            engineParameters_[Urho3D.EpWindowTitle] = "Urho3D/rbfx sample";
            engineParameters_[Urho3D.EpResourcePrefixPaths] = $"{currentDir};{currentDir}/..";
            engineParameters_[Urho3D.EpHighDpi] = _options.HighDpi;
            engineParameters_[Urho3D.EpRenderPath] = _options.RenderPath;
        }

        public override void Start()
        {
            Context.Input.SetMouseVisible(true);

            // Viewport
            _scene = new Scene(Context);
            _scene.LoadFile("Scenes/SampleScene.xml");

            var character = _scene.GetChild("character");
            //_characterController = character.CreateComponent<CharacterController>();

            _camera = _scene.GetChild("Main Camera", true);
            _viewport = new Viewport(Context);
            _viewport.Scene = _scene;
            _viewport.Camera = _camera.GetComponent<Camera>();
            //var renderPath = _viewport.RenderPath;
            //renderPath.Append(Context.Cache.GetResource<XMLFile>("PostProcess/GammaCorrection.xml"));
            Context.Renderer.SetViewport(0, _viewport);
            _camera.Parent.CreateComponent<RotateObject>();

            SubscribeToEvent(E.Update, args =>
            {
                var timestep = args[E.Update.TimeStep].Float;
                Debug.Assert(this != null);

                if (ImGui.Begin("Urho3D.NET"))
                {
                    ImGui.TextColored(Color.Red, $"Ftm by luyssport\nLicense: CC Attribution - NonCommercial - ShareAlike\nhttps://sketchfab.com/3d-models/ftm-0970f30574d047b1976ba0aa6f2ef855\nFrame time: {timestep}");
                    if (ImGui.Button("Exit"))
                    {
                        Context.Engine.Exit();
                    }
                }
                ImGui.End();

                if (Context.Input.GetKeyDown(Key.KeyEscape))
                {
                    Context.Engine.Exit();
                }
            });
        }
    }
}