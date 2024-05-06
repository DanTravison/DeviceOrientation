using DeviceOrientation.Platform;
using Foundation;

namespace DeviceOrientation;

// NOTE: We're simulating using ScreenOrientation support from a shared library
// by deriving from the custom IOSApplicationDelegate instead of directly
// from MauiUIApplicationDelegate.
[Register("AppDelegate")]
public class AppDelegate : IOSApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
