namespace DeviceOrientation.Platform;

using Foundation;
using System.Diagnostics;
using UIKit;

internal class PlatformScreenOrientation
{
    static UIInterfaceOrientationMask _desired = UIInterfaceOrientationMask.All;

    internal static UIInterfaceOrientationMask Desired
    {
        get
        {
            return _desired;
        }
        private set
        {
            _desired = value;
        }
    }

    public static bool SetOrientation(DisplayOrientation orientation)
    {
        bool result = false;
        if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0))
        {

            UIInterfaceOrientationMask mask = orientation.Mask();
            Desired = mask;
            Trace.WriteLine(mask, "UIInterfaceOrientationMask");
 
            var windowScene = (UIApplication.SharedApplication.ConnectedScenes.ToArray()[0] as UIWindowScene);
            if (windowScene != null)
            {
#pragma warning disable CA1422 // Validate platform compatibility
                var viewController = UIApplication.SharedApplication.KeyWindow?.RootViewController;
#pragma warning restore CA1422 // Validate platform compatibility

                if (viewController != null)
                {
                    result = true;
                    windowScene.Title = nameof(PlatformScreenOrientation);
                    viewController.SetNeedsUpdateOfSupportedInterfaceOrientations();
                    windowScene.RequestGeometryUpdate(new UIWindowSceneGeometryPreferencesIOS(mask), error =>
                    {
                        Trace.WriteLine(error.ToString(), "RequestGeometryUpdate");
                        result = false;
                    });
                    windowScene.Title = "";
                    if (result == true)
                    {
                        Desired = mask;
                        viewController.NavigationController?.SetNeedsUpdateOfSupportedInterfaceOrientations();
                    }
                }
                else
                {
                    Trace.WriteLine("failed", "RootViewController");
                }
            }
            else
            {
                Trace.WriteLine("failed", "UIWindowScene");
            }
        }
        else
        {
            UIInterfaceOrientation uiOrientation = orientation.UIOrientation();
            Desired = uiOrientation.Mask();
            Trace.WriteLine(uiOrientation, "UIInterfaceOrientation");
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeRight), new NSString("orientation"));
            result = true;
        }
        Trace.WriteLine(result, "SetOrientation");
        return result;
    }
}

static class OrientationExtension
{
    public static UIInterfaceOrientationMask Mask(this DisplayOrientation orientation)
    {
        if (orientation == DisplayOrientation.Portrait)
        {
            return UIInterfaceOrientationMask.Portrait;
        }
        else if (orientation == DisplayOrientation.Landscape)
        {
            return UIInterfaceOrientationMask.Landscape;
        }
        return UIInterfaceOrientationMask.All;
    }

    public static UIInterfaceOrientationMask Mask(this UIInterfaceOrientation orientation)
    {
        switch (orientation)
        {
            case UIInterfaceOrientation.Portrait:
                return UIInterfaceOrientationMask.Portrait;
            case UIInterfaceOrientation.PortraitUpsideDown:
                return UIInterfaceOrientationMask.PortraitUpsideDown;
            case UIInterfaceOrientation.LandscapeLeft:
                return UIInterfaceOrientationMask.LandscapeLeft;
            case UIInterfaceOrientation.LandscapeRight:
                return UIInterfaceOrientationMask.LandscapeRight;
            default:
                return UIInterfaceOrientationMask.All;
        }
    }

    public static UIInterfaceOrientation UIOrientation(this DisplayOrientation orientation)
    {
        if (orientation == DisplayOrientation.Portrait)
        {
            return UIInterfaceOrientation.Portrait;
        }
        else if (orientation == DisplayOrientation.Landscape)
        {
            return UIInterfaceOrientation.LandscapeRight;
        }
        return UIInterfaceOrientation.Unknown;
    }

    public static UIInterfaceOrientation UIOrientation(this UIInterfaceOrientationMask mask)
    {
        switch (mask)
        {
            case UIInterfaceOrientationMask.Landscape:
            case UIInterfaceOrientationMask.LandscapeLeft:
                return UIInterfaceOrientation.LandscapeLeft;
            case UIInterfaceOrientationMask.LandscapeRight:
                return UIInterfaceOrientation.LandscapeRight;
            case UIInterfaceOrientationMask.Portrait:
                return UIInterfaceOrientation.Portrait;
            case UIInterfaceOrientationMask.PortraitUpsideDown:
                return UIInterfaceOrientation.PortraitUpsideDown;
            default:
                return UIInterfaceOrientation.Unknown;
        }
    }


}

