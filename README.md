# DeviceOrientation
Prototype for controlling a view's DisplayOrientation.

This is a work in progress. It is currently testing on a 3rd generation IPad and Samsung Galaxy S9+ device.

The goal is to set and lock the display orientation for a specific view on iOS and Android.

The intent is for the view with the custom orientation to set it on OnAppearing and restore
it in OnDisappearing.

For example, an application might have a main page that displays in any orientation and a secondary
page that requires a specific orientation.

# Example Usage:

- MainPage.xaml and MainPage.xaml.cs
Provides three buttons for changing the desired orientation, Portrait, Landscape, and Device
where Device allows the view to follow the device's orientation.

# Platform
- ScreenOrientation.cs - provides a static Set(DisplayOrientation orientation) method that 
proxies to platform specific code.

# IOS
- PlatformScreenOrientation.cs - provides the platform specific code for setting 
ScreenOrientation on IOS and defines the static Desired property for AppDelegate to query.
Desired is set before setting the screen orientation.
 
- AppDelegate.cs - implements GetSupportedInterfaceOrientations and exports it as 
application:supportedInterfaceOrientationsForWindow to support querying 
the desired UIInterfaceOrientationMask.

# Android
- PlatformScreenOrientation.cs - provides the platform specific code for setting ScreenOrientation
on Android.

# Platform differences
On Android, setting the screen orientation appears to lock it with no other code being necessary.

On iOS - On iOS AppDelegate implements GetSupportedInterfaceOrientations and exports it as 
application:supportedInterfaceOrientationsForWindow. This queries PlatformScreenOrientation.Desired
for the target windowScene.
