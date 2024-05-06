namespace DeviceOrientation.Platform;

using Foundation;
using System.Diagnostics;
using UIKit;

/// <summary>
/// Provides an abstract base <see cref="MauiUIApplicationDelegate"/> 
/// to support dynamically changing the device orientation.
/// </summary>
/// <remarks>
/// Derive the application's generated AppDelegate class from this base class
/// instead of deriving directly from <see cref="MauiUIApplicationDelegate"/>.
/// </remarks>
public abstract class IOSApplicationDelegate : MauiUIApplicationDelegate
{
    [Export("application:supportedInterfaceOrientationsForWindow:")]
    internal UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
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
