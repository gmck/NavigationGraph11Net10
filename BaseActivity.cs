using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.Preference;
using Google.Android.Material.Color;
using Google.Android.Material.Navigation;
using System;

namespace com.companyname.navigationgraph11net10
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : AppCompatActivity
    {
        protected ISharedPreferences? sharedPreferences;
        
        private string? requestedColorTheme;
        private bool useDynamicColors;
        private bool retainVehicleImages;

        // Expose these to derived activities
        protected bool UseDynamicColors => useDynamicColors;
        protected bool RetainVehicleImages => retainVehicleImages;

        #region OnCreate
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            EdgeToEdge.Enable(this);

            base.OnCreate(savedInstanceState);

            // This is ok, because not deprecated. 3 button navigation.
            if (OperatingSystem.IsAndroidVersionAtLeast(29))
                Window!.NavigationBarContrastEnforced = false;      // Makes NavigationBar fully transparent

            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);

            // colorThemeValue defaults to GreenBmw
            requestedColorTheme = sharedPreferences!.GetString("colorThemeValue", "3");
            useDynamicColors = sharedPreferences.GetBoolean("use_dynamic_colors", false);
            retainVehicleImages = sharedPreferences.GetBoolean("retain_vehicle_images", false);

            // DynamicColors was introduced in API 31 Android 12
            if (OperatingSystem.IsAndroidVersionAtLeast(31) & useDynamicColors)   
            {
                SetAppTheme(requestedColorTheme!);
                DynamicColors.ApplyToActivityIfAvailable(this);
            }
            else
                SetAppTheme(requestedColorTheme!);
        }
        #endregion

        #region SetAppTheme
        private void SetAppTheme(string requestedColorTheme)
        {
            
            if (requestedColorTheme == "1")
                SetTheme(Resource.Style.Theme_NavigationGraph_RedBmw);
            else if (requestedColorTheme == "2")
                SetTheme(Resource.Style.Theme_NavigationGraph_BlueAudi);
            else if (requestedColorTheme == "3")
                SetTheme(Resource.Style.Theme_NavigationGraph_GreenBmw);

            SetSystemBarsAppearance();
        }
        #endregion

        #region SetSystemBarsAppearance()
        private void SetSystemBarsAppearance()
        {
            WindowInsetsControllerCompat windowInsetsController = new(Window!, Window!.DecorView);

            if (!IsNightModeActive())
            {
                windowInsetsController.AppearanceLightStatusBars = true;
                windowInsetsController.AppearanceLightNavigationBars = true;
            }
            else
            {
                windowInsetsController.AppearanceLightStatusBars = false;
                windowInsetsController.AppearanceLightNavigationBars = false;
            }
        }
        #endregion

        #region IsNightModeActive
        private bool IsNightModeActive()
        {
            UiMode currentNightMode = Resources!.Configuration!.UiMode & UiMode.NightMask;
            return currentNightMode == UiMode.NightYes;
        }
        #endregion

        #region ApplyDynamicHeaderBackground
        /// <summary>
        /// When dynamic colors are enabled and available, override the nav header
        /// background to a solid theme color instead of the themed vehicle image.
        /// </summary>
        /// <param name="navigationView">The NavigationView hosting the header.</param>
        protected void ApplyDynamicHeaderBackground(NavigationView? navigationView)
        {
            // Only apply when user requested dynamic colors and they are actually available.
            if (!UseDynamicColors || !DynamicColors.IsDynamicColorAvailable || navigationView == null)
                return;

            View headerView = navigationView.GetHeaderView(0)!;
            if (headerView == null)
                return;

            View headerRoot = headerView.FindViewById<View>(Resource.Id.nav_header_root)!;
            if (headerRoot == null)
                return;

            // Resolve a background-like color from the active (dynamic) theme.
            // You can swap colorBackground for colorSurface - but virtually white on white if text is white. Would need to change text color.
            // Better going with colorPrimary - good contrast between menu header and menu choices
            // or colorPrimaryContainer a lighter shade for the menu header, but still good contrast with the menu choices.
            // or colorSurfaceVariant for a more muted look.
            // Could also reduce the width of the nav_header. Width size is based on my production app which has longer menu item labels 

            Android.Util.TypedValue typedValue = new ();
            bool found = Theme!.ResolveAttribute(Resource.Attribute.colorPrimary, typedValue, true);
            if (!found)
                return;

            int colorInt = typedValue.Data;
            headerRoot.Background = new ColorDrawable(new Android.Graphics.Color(colorInt));
        }
        #endregion
    }
}
