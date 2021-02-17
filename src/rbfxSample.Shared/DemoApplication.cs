using System.Diagnostics;
using System.IO;
using ImGuiNet;
using Urho3DNet;

namespace rbfxSample
{
    public class DemoApplication : Application
    {
        private readonly ApplicationOptions _options;
        private Scene _scene;
        private Viewport _viewport;
        private Node _camera;

        private KinematicCharacterController _characterController;

        private RigidBody _body;

        private CollisionShape _shape;
        //private Node _cube;
        //private Node _light;

        public DemoApplication(Context context, ApplicationOptions options) : base(context)
        {
            _options = options;
        }

        protected override void Dispose(bool disposing)
        {
            Context.Renderer.SetViewport(0, null);    // Enable disposal of viewport by making it unreferenced by engine.
            _viewport?.Dispose();
            _scene?.Dispose();
            _camera?.Dispose();
            //_cube.Dispose();
            //_light.Dispose();
            base.Dispose(disposing);
        }

        public override void Setup()
        {
            bool windowed = _options.Windowed;
            EngineParameters[Urho3D.EpFullScreen] =  !windowed;
            if (windowed)
            {
                EngineParameters[Urho3D.EpWindowResizable] = true;
            }
            //EngineParameters[Urho3D.EpOrientation] = "Portrait";
            if (_options.Width.HasValue)
                EngineParameters[Urho3D.EpWindowWidth] = _options.Width.Value;
            if (_options.Height.HasValue)
                EngineParameters[Urho3D.EpWindowHeight] = _options.Height.Value;
            EngineParameters[Urho3D.EpWindowTitle] = "Urho3D/rbfx sample";
            EngineParameters[Urho3D.EpHighDpi] = _options.HighDpi;
            if (!string.IsNullOrWhiteSpace(_options.RenderPath))
                EngineParameters[Urho3D.EpRenderPath] = _options.RenderPath;
        }

        public override void Start()
        {
            Context.Input.SetMouseVisible(true);
            Context.Engine.CreateDebugHud().Mode = DebugHudMode.DebughudShowAll;

            //Context.Renderer.DefaultZone.FogColor = new Color(1,0,0,1);
            //_scene = new Scene(Context);
            //_scene.CreateComponent<Octree>();
            //var z = _scene.CreateComponent<Zone>();
            //z.FogColor = new Color(0, 1, 0, 1);
            //_camera = _scene.CreateChild();
            //_viewport = new Viewport(Context, _scene, _camera.CreateComponent<Camera>());
            //_viewport.DrawDebug = true;
            //Context.Renderer.SetViewport(0, _viewport);
            //return;

            Context.Renderer.DefaultZone.FogColor = new Color(1.0f, 0.0f, 0.2f, 1.0f);
            
            // Viewport
            _scene = new Scene(Context);
            _scene.LoadFile("Scenes/SampleScene.xml");
            _scene.CreateComponent<PhysicsWorld>();

            var character = _scene.GetChild("character");
            _body = character.CreateComponent<RigidBody>();
            _body.CollisionLayer = 1u;
            _body.IsKinematic = true;
            _body.IsTrigger = true;
            _body.AngularFactor = (Vector3.Zero);
            _body.CollisionEventMode = CollisionEventMode.CollisionAlways;

            _shape = character.CreateComponent<CollisionShape>();
            _shape.SetCapsule(0.7f, 1.8f, new Vector3(0.0f, 0.9f, 0.0f));
            
            _characterController = character.CreateComponent<KinematicCharacterController>();

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