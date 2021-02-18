using Urho3DNet;

namespace rbfxSample.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            Launcher.Run(_=>
            {
                var options = new ApplicationOptions();
                options.Windowed = true;
                return new DemoApplication(_, options);
            });
        }
    }
}