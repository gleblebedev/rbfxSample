using Foundation;
using UIKit;
using System.Threading.Tasks;
using Urho3DNet;

namespace rbfxSample.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            LaunchGame();
            return true;
        }


        async void LaunchGame()
        {
            await Task.Yield();
            using (var context = new Context())
            {
                using (var app = new DemoApplication(context, new ApplicationOptions()))
                {
                    app.Run();
                }
            }

        }
    }
}

