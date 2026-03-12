
NavigationGraph11Net10 is the equivalent of an update to NavigationGraph9Net9, which was uploaded to my GitHub on Dec 5, 2024. NavigationGraph9Net9 is an ``android9-android35`` app whereas NavigationGraph11Net10 
is an ``android10-android36.1`` app. Ironically, the latest version still shows the same warnings as the previous version when uploading to the Google Play Console. 
The warnings relate to Google Play Console's message  **Your app uses deprecated APIs or parameters for edge-to-edge.**

I should state that neither of these apps has been uploaded to GPC, as that is not very practical for a test project, but I have uploaded my production version apps (``android10-android36.1``) 
many times since Sept 1, 2025, which also produces the same warnings. 

In my case, it is only two warnings re using ``SetStatusBarColor`` and ``SetNavigationBarColor``. However, there were many more initially and can be many more, for example, "SetDecorFitsSystemWindows()``. 

Please see https://github.com/dotnet/android/issues/10304 for the explantion.

As you probably already know, GPC stated that all apps had to be Edge-to-Edge compliant by September 1st 2025. Since my apps don't use any of the above methods, the problem obviously exists in 
the AndroidX libraries.

So, the Sept 1, 2025, Edge-to-Edge requirement is not being strictly enforced by GPC for the moment because the Xamarin.Google.Android.Material (1.13.0.1) library is still using the above methods. 

The latest version from Google is com.google.android.material:material:<1.14.0-alpha09>. Since MS doesn't provide a Xamarin.Google.Android.Material version based on an alpha version, 
we therefore have to wait for Google to release a stable version, assumming that com.google.android.material:material when it becomes stable eliminates the original problem. 
In this example, the material lib is a dependency lib of Xamarin.AndroidX.Navigation.UI. See the project's csprog file for details.

**Edge-To-Edge Requirements**

See NavigationGraph9Net9 for a detailed explanation of Edge-To-Edge and links to various Google documentation of how to implement it in your app. Below is a summary of the implementation in this project.

The very first line of the OnCreate() of the BaseActivity calls  ``EdgeToEdge.Enable(this)``. ``SetSystemBarsAppearance()`` is used in the BaseActivity to set the system bars to light or dark depending 
on the System Theme setting. ``SplashScreen.InstallSplashScreen(this)`` is not called until the OnCreate() of the MainActivity.

***MainActivity***

NavigationView 

The menu content should cover the StatusBar and the BottomNavigationBar and therefore the contents of the menu should be visible through both bars. The menu should have rounded corners on the right side.
If the menu contains many menu items, the items should also be visible through the NavigationBar when scrolling.

``IOnApplyWindowInsetsListener``
See ``WindowInsetsCompat OnApplyWindowInsets(View? v, WindowInsetsCompat? insets)`` for how to use this listener to make the contents of the NavigationView visible through the 
StatusBar and the BottomNavigationBar and maintain the correct insets when rotating from Portrait to Landscape modes and vice versa. This is combined with the ``OnDestinationChange()`` event which 
calls ``SetShortEdgesIfRequired(navDestination)`` for each navDestination change.  

***RecyclerViews***

The contents of a recyclerview should be visible through the BottomNavigationBar when scrolling.  - see BooksFragment for an example.

***SettingsFragment***

The contents of the SettingsFragment should also be visible through the BottomNavigationBar when scrolling. 

***BottomSheetDiaglogs***

The contents of a BottomSheetDialog should also be visible through the BottomNavigationBar when scrolling. See FileMaintenaceFragment for an example of an Edge-To-Edge compliant BottomSheetDialog.

All of the above scrolling comments apply whether the app is using gesture navigation or 3 button navigation.

***BLUETOOTH_PERMISSIONS_REQUEST_CODE***

The HomeFragment contains a number of MenuItems including a BLUETOOTH_PERMISSIONS_REQUEST_CODE when opening the SettingsFragment. This is a completely contrived example to illustrate a point, 
as this project has no Bluetooth features or requirements and therefore the permissions test is not required. However it does illustrate an inconsistency re Material3 Themes in that the permission 
request does present a normal system Material3 Bluetooth permissions request dialog. The problem is that the colours used by the system Bluetooth Permission's dialog don't follow 
the colours of the Material3 themes that the app already provides. This obviously creates a servere colour clash problem for the user experience. Of course it is a one off experience, because it will never 
appear again once the user has granted the Bluetooth permissions. You could even include Pre-Permissions dialog explaining that a system Bluetooth Permission's dialog will follow explaining that is will 
have system colours appearance, but I'm sure most developers would agree it is something that any app could do without.

What becomes obvious once you change wallpapers or choose different colors for your system background colours is that the system Bluetooth Permission's dialog uses those colours or your wallpapers colours.

Note: To see the corrected effect you will need to use the Properties Window of the app and select ``App Info``. Select Permissions for ``Nearby devices`` and then unselect ``Allow`` and select ``Don't allow``. 
Then re run the app to see the Bluetooth Permission's dialog appear again. See the next section for how to use Dynamic Colours to make the Bluetooth Permission's dialog colours consistent with the Material3 themes used in the app.

**Dynamic Colours and Material3 Themes**

DynamicColors was introduced in API 31 Android 12, so it is possible that we can combine the use of DynamicColors with our Material3 themes to create a more consistent user experience. The Material Themes 
I've used in this project are the same themes I use in both my published apps. The colours used are based on the colours of three different vehicle images built using the Material3 Theme Builder. 
See https://m3.material.io/blog/material-theme-builder

We have provided two new preferences that allow you to use the wallpaper colors as your theme, by setting the preference "use_dynamic_colors". This will automatically change the theme to the colours of 
your wallpapaer. 
From the MainActivity a new method in the BaseActivity ``ApplyDynamicHeaderBackground()`` is called when the "use_dynamic_colors" preference is set true. ``ApplyDynamicHeaderBackground()`` removes 
the vehicle image (android:background) from the the nav_header.xml of the NavigationView and replaces the background with Resource.Attribute.colorPrimaryResource.Attribute.colorPrimary. 
The nav_header.xml could be further improved by changing its android:layout_height, because as it is it is probably now too tall.  

The second preference "retain_vehicle_images" allows you to retain the vehicle image in the NavigationView. See the MainActivity 

``// If dynamic colors are in use, override the nav header image with a solid background, except if !RetainVehicleImages``

    if (!RetainVehicleImages)
        ApplyDynamicHeaderBackground(navigationView);


If the user chooses a wallpaper with similar colours to one of our themes, then the Bluetooth Permission dialog's colours will be consistent with our themes.

***Examples using a predominant color wallpaper***

The first image is the device's wallpaper. 

The second image shows the result after activating the Use Dynamic Colour Theme. 
This shows the Navigation menu with the vehicle image removed and the area replaced by the Dynamic Colour Theme’s colorPrimary 
colour. 

The third image shows the Blue Audi image reinstated, but all the colours of the Blue Audi theme have been replaced with those of the Dynamic Colour theme.  

This is then repeated for the Red BMW and the Green BMW themes. 

<!-- Blue Audi -->
**Blue Audi – 1. predominantly blue Wallpaper, 2. Dynamic colours checked matching navView background, 3. reinstate the vehicle image in navView**
<table cellpadding="6"> 
  <tr>
    <td><img src="Images/blue_audi_lock.png" alt="Blue Audi lock screen" width="260"></td>
    <td><img src="Images/blue_audi_nav_plain.png" alt="Blue Audi NavigationView plain background" width="260"></td>
    <td><img src="Images/blue_audi_nav_vehicle.png" alt="Blue Audi NavigationView with vehicle image" width="260"></td>
  </tr>
</table>

<br/>

<!-- Red BMW -->
**Red BMW – 1. predominantly red Wallpaper, 2. Dynamic colours checked matching navView background, 3. reinstate the vehicle image in navView**

<table cellpadding="6">
  <tr>
    <td><img src="Images/red_bmw_lock.png" alt="Red BMW lock screen" width="260"></td>
    <td><img src="Images/red_bmw_nav_plain.png" alt="Red BMW NavigationView plain background" width="260"></td>
    <td><img src="Images/red_bmw_nav_vehicle.png" alt="Red BMW NavigationView with vehicle image" width="260"></td>
  </tr>
</table>

<br/>

<!-- Green BMW -->
**Green BMW – 1. predominantly green Wallpaper, 2. Dynamic colours checked matching navView background, 3. reinstate the vehicle image in navView**

<table cellpadding="6">
  <tr>
    <td><img src="Images/green_bmw_lock.png" alt="Green BMW lock screen" width="260"></td>
    <td><img src="Images/green_bmw_nav_plain.png" alt="Green BMW NavigationView plain background" width="260"></td>
    <td><img src="Images/green_bmw_nav_vehicle.png" alt="Green BMW NavigationView with vehicle image" width="260"></td>
  </tr>
</table>


**RaceResultFragment - totally Edge-To-Edge**

The RaceResultFragment is a fully immersive fragment. We have modified it in this version as compared to the NavigationGraph9Net9 project. It now defaults to using a carbon fibre background, 
which can be changed to either a dark or light background via the preference **Choose background colour for ResultRaceResultFragment.** The carbon fibre background is a bit more interesting than 
the plain black or white backgrounds and highlights the immersive fragment by making it very obvious that the background extends into the camera cutout area or notch which emphasises the Edge-To-Edge 
effect. We use it our published apps to show 3D like automtive gauges using the SkiaSharp.Views library to draw the gauges. To make any fragment fully immersive, all you need to do is inherit 
from ImmersiveFragment.

**Android36.1 Features**

The AboutDialogFragment describes how to use ``OperatingSystem.IsAndroidVersionAtLeast(36)`` in conjuction with ``Build.VERSION.SdkIntFull, Build.GetMajorSdkVersion(sdkFull) and 
Build.GetMinorSdkVersion(sdkFull)`` 
to enable support for ``OperatingSystem.IsAndroidVersionAtLeast(36.1)``

It also demonstrates using ``Build.VERSION.SecurityPatch``, so that it can show the user the security patch level of their device. 

Note all Nugets in use were updated 11/03/2026 to their latest versions, which includes the latest AndroidX and Material libraries.
