using Urho3DNet;

namespace rbfxSample
{
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