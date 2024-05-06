using DeviceOrientation.Platform;
using Foundation;
using System.Diagnostics;
using UIKit;

namespace DeviceOrientation
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        [Export("application:supportedInterfaceOrientationsForWindow:")]
        public UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            UIInterfaceOrientationMask result;
            if (forWindow.WindowScene != null && forWindow.WindowScene.Title == nameof(PlatformScreenOrientation))
            {
                result = PlatformScreenOrientation.Desired;
            }
            else
            {
                result = application.SupportedInterfaceOrientationsForWindow(forWindow);
            }
            Trace.WriteLine(result, "GetSupportedInterfaceOrientations");
            return result;
        }
    }
}
