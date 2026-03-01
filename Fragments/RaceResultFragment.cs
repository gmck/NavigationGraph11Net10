using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Preference;
using static System.Net.Mime.MediaTypeNames;

namespace com.companyname.navigationgraph11net10.Fragments
{
    public class RaceResultFragment : ImmersiveFragment
    {

        private BackgroundType BackgroundType { get; set; }
        private int backgroundColor;
        private ISharedPreferences? sharedPreferences;
        private int foregroundColor;

        public RaceResultFragment() { }

        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Activity!);
            string? backgroundDisplayType = sharedPreferences!.GetString("backgroundDisplayType", "1");

            // BackgroundType
            if (backgroundDisplayType == "1")
                BackgroundType = BackgroundType.CarbonFibre;
            else if (backgroundDisplayType == "2")
                BackgroundType = BackgroundType.Dark;
            else if (backgroundDisplayType == "3")
                BackgroundType = BackgroundType.Light;
        }

        #region OnCreateView - GLM Working version
        public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            // We have three types of backgrounds
            if (BackgroundType == BackgroundType.CarbonFibre)
            {
                // Set the foreground color for the text to white.
                foregroundColor = ContextCompat.GetColor(Context, Resource.Color.white);

                // now the DecorView background
                Activity!.Window!.DecorView.Background = ContextCompat.GetDrawable(Activity, Resource.Drawable.carbon_fibre_background);
            }
            else if (BackgroundType == BackgroundType.Dark)
            {
                //Set the foreground color for the text to white.
                foregroundColor = ContextCompat.GetColor(Context, Resource.Color.white);

                //now the decorview background - dark colour.
                backgroundColor = ContextCompat.GetColor(Context!, Resource.Color.black);
                Activity!.Window!.DecorView.SetBackgroundColor(new Color(backgroundColor));
            }
            else if (BackgroundType == BackgroundType.Light)
            {
                // defaults to correct background or surfacecolour, which is white so no need to set it.
                // so we need to set the text colour to something dark
                foregroundColor = ContextCompat.GetColor(Context, Resource.Color.black);
            }

            View? view = inflater.Inflate(Resource.Layout.fragment_raceresult, container, false);
            TextView? textView = view!.FindViewById<TextView>(Resource.Id.text_race_result);
            textView!.SetTextColor(new Color(foregroundColor));
            textView.Text = "Immersive RaceResult fragment";
            return view;
        }
        #endregion

        #region OnCreateView - Copilot version - not working - text not displaying for carbon fibre and dark background types, but does display for light background type. 
        // fails to display any text - should display the string "Immersive RaceResult frgment" in the ceentered textview for both the carbon fibre and dark backgroundTypes.
        // Does display the text for light backgroundType. Tried many of the colorOn... colors and they all fail.

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        //{
        //    // Resolve the themed text color once
        //    var tv = new TypedValue();
        //    Activity!.Theme!.ResolveAttribute(Resource.Attribute.colorOnBackground, tv, true);
        //    foregroundColor = ContextCompat.GetColor(Context!, tv.ResourceId);

        //    // We have three types of backgrounds
        //    if (BackgroundType == BackgroundType.CarbonFibre)
        //    {
        //        Activity!.Window!.DecorView.Background =
        //            ContextCompat.GetDrawable(Activity, Resource.Drawable.carbon_fibre_background);
        //    }
        //    else if (BackgroundType == BackgroundType.Dark)
        //    {
        //        backgroundColor = ContextCompat.GetColor(Context!, Resource.Color.black);
        //        Activity!.Window!.DecorView.SetBackgroundColor(new Color(backgroundColor));
        //    }
        //    else if (BackgroundType == BackgroundType.Light)
        //    {
        //        // Defaults to correct background colour (your theme’s surface / background).
        //        // No need to touch the window background.
        //    }

        //    View? view = inflater.Inflate(Resource.Layout.fragment_raceresult, container, false);
        //    TextView? textView = view!.FindViewById<TextView>(Resource.Id.text_race_result);
        //    textView!.SetTextColor(new Color(foregroundColor));
        //    textView.Text = "Immersive RaceResult fragment";
        //    return view;
        //}
        #endregion


        #region OnDestroyView
        public override void OnDestroyView()
        {
            Log.Debug("RaceResultFragment", "OnDestroyView called");
            // Rather than have this in the ShowSystemUi method of ImmersiveFragment, we need it here because this will execute before the OnDestroyView
            // of the ImmersiveFragment
            TypedValue typedValue = new();
            Activity!.Theme!.ResolveAttribute(Resource.Attribute.colorSurface, typedValue, true);
            int color = ContextCompat.GetColor(Context!, typedValue.ResourceId);
            Activity!.Window!.DecorView.SetBackgroundColor(new Color(color));

            base.OnDestroyView();
        }
        #endregion
    }
}