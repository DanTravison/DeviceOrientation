# DeviceOrientation
Prototype for controlling a view's DisplayOrientation.

This is a work in progress and is intended to pull together the information from various 
Stack Overlow, Github, and Apple/Android developer discussions into a working prototype.

The goal is to set and lock the display orientation for a specific view on iOS and Android.

For example, an application might have a main page that displays in any orientation and a secondary
page that requires a specific orientation. This is my specific use case but there may be others.

The prototype is currently tested on a 3rd generation IPad and Samsung Galaxy S9+ device.

# Next Steps
- Determine the appropriate locations in a view's code to call SetOrientation.
  - Initial thoughts are in OnAppearing and OnDisappearing.
- Determine an appropriate method for restoring the previous desired orientation.
  - e.g., should ScreenOrientation.Set return the current orientation that the view
can use for restoring.
- Consider adding an alternative to DisplayOrientation to extend orientations common
to iOS and Android such as Portrait up/down and Landscape left/right.
- Determine if the same capability exists on Windows for devices that support orientation changes.
- Test on simulators and additional devices.

# Example Usage:
- MainPage.xaml and MainPage.xaml.cs
Provides three buttons for changing the desired orientation, Portrait, Landscape, and Device
where Device allows the view to following the device's orientation.

# Platform
- ScreenOrientation.cs - provides a static Set(DisplayOrientation orientation) method that 
proxies to platform specific code.
  - Called by the view.

# IOS
- PlatformScreenOrientation.cs - static class providin the platform specific code for setting 
ScreenOrientation on IOS 
  - Implements  SetOrientation(DisplayOrientation orientation)
  - Implements UIInterfaceOrientationMask Desired
  - NOTE: Desired must be set before updating the screen orientation.
 
- IOSApplicationDelegate.cs - provides an abstract MauiUIApplicationDelegate
  - Implements GetSupportedInterfaceOrientations and exports it as 
application:supportedInterfaceOrientationsForWindow.
  - Uses PlatformScreenOrientation.Desired to respond to GetSupportedInterfaceOrientations.
  - NOTE: Without this logic, the orientation changes but does not remain 'locked' when the
device's orientation changes.

# Android
- PlatformScreenOrientation.cs - provides the platform specific code for setting ScreenOrientation
on Android.
  - Implements  SetOrientation(DisplayOrientation orientation)

# Platform differences
On Android, setting the screen orientation appears to lock it with no other code being necessary.

On iOS, IOSApplicationDelegate implements GetSupportedInterfaceOrientations and exports it as 
application:supportedInterfaceOrientationsForWindow. This queries PlatformScreenOrientation.Desired
for the target windowScene. This is required to lock the screen orientation for the view.
The application's AppDelegate should derive from this class.

# Shared Assembly support.
To place this code in a shared assembly, move the various files to the appropriate
folder in the shared assembly.

- Move Platform\ScreenOrientation.cs to an appropriate namespace in the shared assembly
- Move Platforms\Android\PlatformScreenOrientation.cs to the Platforms\Android folder
of the shared assembly.
- Move Platforms\IOS\PlatformScreenOrientation.cs and IOSApplicationDelegate.cs to the 
Platforms\iOS folder of the shared assembly.

## iOS Specific Change
The applications' AppDelegate is derived from IOSApplicationDelegate 
instead of MauiUIApplicationDelegate. This avoids placing platform-specific 
implementation logic the application.
