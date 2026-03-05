**05/03/2026**
Build 1.1 

Added the Dynamic Colour Theme feature.
This includes the screenshots below. The images created a problem in that since they are not part of the project 
as per the csprog file, when you need to update them you also need to close and reopen the solution to see any changes you make.

**01/03/2026**

Build 1.0 Creating a new repo. NavigationGraph11Net10

1. Need to modify AboutDialogFragment to have the same Android36.1 features as OBDNowPros's AboutDialogFragment. 

2. Also need to import (into this readme) the three sets of images from OBDNowPro's Help file showing the use of Dynamic Color Themes with a chosen wallpaper along side 
the two versions of the NavigtionView showing either the Dynamic Color Theme with a color background for the NavigationView using the 
BaseActivity's ApplyDynamicHeaderBackground() or our standard NavigationView using the vehicle image in the Resource.Id.nav_header_root. 
Refer to the two new preferences *Use Dynamic Colour Theme* and *Retain Vehicle images while using Dynamic Color Scheme*. 
Might as well just take the text description from the Help file.  

Text from OBDNowsPro's Help file:

This build introduces a new feature from Google to our theming called Dynamic Colour Themes. As you know, OBDNowPros already has three Material3 themes (light and dark), which we call RedBMW, BlueAudi, and GreenBMW, each based on the colours of a vehicle image. Dynamic Colour Themes are based on your system's wallpapers or chosen colours, in other words, the user’s choice of a particular wallpaper or colour set. 
There are two new settings in the Settings window. Use Dynamic Colour Theme and Retain our images while using Dynamic Colour Theme. 

The choice Use Dynamic Colour Theme automatically removes the vehicle image from the Navigation menu and replaces its background with the Dynamic Colour Theme’s colorPrimary colour. This is done to prevent a clash of colours between the chosen vehicle image and the new Dynamic Colour Theme colorPrimary colour.
However, the second option, Retain our images while using Dynamic Colour Theme, allows you to keep the vehicle image when your chosen wallpaper colour closely matches one of our three default colour schemes based on our vehicle images.  
You can then choose the final effect, either the Light or Dark theme.

Example using a predominant blue wallpaper.
The first image is the device's wallpaper. The second image shows the result after activating the Use Dynamic Colour Theme. 
This shows the Navigation menu with the vehicle image removed and the area replaced by the Dynamic Colour Theme’s colorPrimary 
colour. 
The third image shows the Blue Audi image reinstated, but all the colours of the Blue Audi theme have been replaced with those of the Dynamic Colour theme.  


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



