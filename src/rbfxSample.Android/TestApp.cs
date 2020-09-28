using Android.Provider;
using ImGuiNet;
using Urho3DNet;

namespace rbfxSample
{
    class TestApp : Urho3DNet.Application
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

        public TestApp(Context context, ApplicationOptions options) : base(context)
        {
            _options = options;
            foreach (var attribute in context.AllAttributes)
            {
                System.Diagnostics.Debug.WriteLine(attribute.Key+" = "+attribute.Value);
            }
        }

        public override void Setup()
        {
            if (!string.IsNullOrWhiteSpace(_options.EpResourcePrefixPaths))
                engineParameters_[Urho3D.EpResourcePrefixPaths] = _options.EpResourcePrefixPaths;
            base.Setup();
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

        public override void Start()
        {
            Context.Input.SetMouseVisible(true);

            // Viewport
            _scene = new Scene(Context);
            _scene.LoadFile("Scenes/SampleScene.xml");
            _scene.CreateComponent<PhysicsWorld>();

            var character = _scene.GetChild("character");
            _body = character.CreateComponent<RigidBody>();
            _body.CollisionLayer = 1u;
            _body.SetKinematic(true);
            _body.SetTrigger(true);
            _body.SetAngularFactor(Vector3.Zero);
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
                //Debug.Assert(this != null);

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
    [ObjectFactory]
    class RotateObject : LogicComponent
    {
        public RotateObject(Context context) : base(context)
        {
            UpdateEventMask = UpdateEvent.UseUpdate;
        }

        public override void Update(float timeStep)
        {
            var d = new Quaternion(0, 20 * timeStep, 0);
            Node.Rotate(d);
        }
    }
}