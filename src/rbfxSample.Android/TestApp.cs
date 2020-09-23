using Android.Provider;
using Urho3DNet;

namespace rbfxSample
{
    class TestApp : Urho3DNet.Application
    {
        private readonly ApplicationOptions _options;
        private Scene _scene;
        private Node _camera;
        private Viewport _viewport;

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

        public override void Start()
        {
            base.Start();

            _scene = new Scene(Context);
            _scene.CreateComponent<PhysicsWorld>();
            _scene.CreateComponent<Octree>();
            var zone = _scene.CreateComponent<Zone>();
            zone.SetBoundingBox(new BoundingBox(Vector3.One * -1000, Vector3.One*1000));
            zone.FogColor = Color.Cyan;

            var cameraRoot = _scene.CreateChild("Camera Pivot", CreateMode.Replicated);
            _camera = cameraRoot.CreateChild("Main Camera", CreateMode.Replicated);
            _camera.Position = new Vector3(0, 0, -10);
            _viewport = new Viewport(Context);
            _viewport.Scene = _scene;
            _viewport.Camera = (Camera)_camera.GetOrCreateComponent((StringHash)typeof(Camera).Name);
            _viewport.Camera.FarClip = 300f;

            Context.Renderer.SetViewport(0, _viewport);


        }
    }
}