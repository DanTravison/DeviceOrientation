namespace DeviceOrientation.Platform;

/// <summary>
/// Provides support for changing <see cref="DisplayOrientation"/>.
/// </summary>
public static class ScreenOrientation
{
    public static bool Set(DisplayOrientation orientation)
    {
#if ANDROID
        return PlatformScreenOrientation.SetOrientation(orientation);
#elif IOS
        return PlatformScreenOrientation.SetOrientation(orientation);
#else
        return false;
#endif
    }
}
