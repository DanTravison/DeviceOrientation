namespace DeviceOrientation.Platform;

using ScreenOrienation = Android.Content.PM.ScreenOrientation;

internal static class PlatformScreenOrientation
{
    public static bool SetOrientation(DisplayOrientation orientation)
    {
        var currentActivity = ActivityStateManager.Default.GetCurrentActivity();
        if (currentActivity != null)
        {
            ScreenOrienation desired = orientation.Platform();
            if (desired != currentActivity.RequestedOrientation)
            {
                currentActivity.RequestedOrientation = desired;
            }
            return true;
        }
        return false;
    }
}

static class OrientationExtension
{
    public static ScreenOrienation Platform(this DisplayOrientation orientation)
    {
        if (orientation == DisplayOrientation.Portrait)
        {
            return ScreenOrienation.Portrait;
        }
        else if (orientation == DisplayOrientation.Landscape)
        {
            return ScreenOrienation.Landscape;
        }
        return ScreenOrienation.User;
    }
}
