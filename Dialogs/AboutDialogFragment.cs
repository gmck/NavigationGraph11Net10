#nullable enable
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using com.companyname.navigationgraph11net10.Classes;
using Google.Android.Material.Dialog;
using System;
using System.Collections.Generic;

namespace com.companyname.navigationgraph11net10.Dialogs
{
    public class AboutDialogFragment  : AppCompatDialogFragment
    {
        protected static readonly string logTag = "NavGraph11 - About:";

        internal TextView? textViewVersionName;
        internal TextView? textViewTargetVersionName;
        internal TextView? textViewAndroidVersionName;
        internal List<AndroidVersions>? androidVersions;
        internal TextView? textviewSecurityPatchValue;

        #region Ctors
        public AboutDialogFragment() { } // Required Parameter less ctor
        #endregion

        #region NewInstance
        public static AboutDialogFragment NewInstance()
        {
            AboutDialogFragment fragment = new()
            {
                Cancelable = false,
            };
            return fragment;
        }
        #endregion

        #region OnCreate
        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        #endregion

        #region OnCreateDialog
        public override Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            PackageInfo packageInfo;
            PackageManager packageManager = Activity!.PackageManager!;
             
            if (OperatingSystem.IsAndroidVersionAtLeast(33))  // Android 13
                packageInfo = packageManager.GetPackageInfo(Activity!.PackageName!, PackageManager.PackageInfoFlags.Of(PackageInfoFlagsLong.None));
            else
#pragma warning disable CS0618 // Type or member is obsolete
                packageInfo = packageManager.GetPackageInfo(Activity!.PackageName!, 0)!;
#pragma warning restore CS0618 // Type or member is obsolete

            androidVersions ??= [
                    //  AndroidName      AndroidBuildCode    AndroidCodeName         AndroidApiNumber
                    new AndroidVersions("Android 7.0",   "N",                "Nougat",               "Api 24"),
                    new AndroidVersions("Android 7.1",   "N_MR1",            "Nougat",               "Api 25"),
                    new AndroidVersions("Android 8.0",   "O",                "Oreo",                 "Api 26"),
                    new AndroidVersions("Android 8.1",   "O_MRI",            "Oreo",                 "Api 27"),
                    new AndroidVersions("Android 9",     "P",                "Pie",                  "Api 28"),
                    new AndroidVersions("Android 10",    "Q",                "Quince Tart",          "Api 29"),
                    new AndroidVersions("Android 11",    "R",                "Red Valvet Cake",      "Api 30"),
                    new AndroidVersions("Android 12",    "S",                "Snow Cone",            "Api 31"),
                    new AndroidVersions("Android 12L",   "S_V2",             "Snow Cone",            "Api 32"),
                    new AndroidVersions("Android 13",    "TIRAMISU",         "TiraMisu",             "Api 33"),
                    new AndroidVersions("Android 14",    "UPSIDE_DOWN_CAKE", "Upside Down Cake",     "Api 34"),
                    new AndroidVersions("Android 15",    "VANILA_ICE_CREAM", "Vanila Ice Cream",     "Api 35"),
                    new AndroidVersions("Android 16",    "BAKLAVA",          "Baklava",              "Api 36"),
                    new AndroidVersions("Android 16.1",  "BAKLAVA_1",        "Baklava_1",            "Api 36.1")
                ];

            // Security patch (ISO date, e.g. "2025-12-05"), optional
            string? securityPatch = Build.VERSION.SecurityPatch;
            string? securityPatchDisplay = !string.IsNullOrEmpty(securityPatch) ? securityPatch : "Unknown";  // 2025-12-05 or unknown

            // Example: BP4A.251205.006.E1 Pixel 10 or "CP11.251114.006" for the Pixel6 running 16.1 beta. This includes the ISO Date, so just use buildId and ignore the above.
            string? buildId = Build.Display ?? Build.VERSION.Incremental ?? Build.Id ?? Build.Unknown;

            // Build/runtime info and select AndroidVersions entry
            string? build = Build.VERSION.Release;
            Log.Debug(logTag, $"Release='{build}'");

            AndroidVersions? androidVersion = null;

            // For API 36+, prefer matching by API number (handles 36.0 vs 36.1)
            // Had to change here with the new APIs in Android 16.1 and introduce SdkIntFull and GetMajor/MinorSdkVersion
            if (OperatingSystem.IsAndroidVersionAtLeast(36))
            {
                int sdkFull = Build.VERSION.SdkIntFull;                 // e.g., 3600001
                int apiMajor = Build.GetMajorSdkVersion(sdkFull);       // e.g., 36
                int apiMinor = Build.GetMinorSdkVersion(sdkFull);       // e.g., 1

                string apiLabel = apiMinor > 0 ? $"Api {apiMajor}.{apiMinor}" : $"Api {apiMajor}";

                Log.Debug(logTag, $"SdkIntFull={sdkFull}, major={apiMajor}, minor={apiMinor}, Is36={OperatingSystem.IsAndroidVersionAtLeast(36)}, Is36_1={OperatingSystem.IsAndroidVersionAtLeast(36, 1)}");

                androidVersion = androidVersions!.Find(x => string.Equals(x.AndroidApiNumber, apiLabel, StringComparison.OrdinalIgnoreCase));
            }

            LayoutInflater? inflater = LayoutInflater.From(Activity);
            View? view = inflater!.Inflate(Resource.Layout.about_dialog, null);

            textViewVersionName = view!.FindViewById<TextView>(Resource.Id.textview_versionName);
            string buildDate = GetString(Resource.String.build_date); 
            textViewVersionName!.Text = packageInfo.VersionName +" - " + buildDate;

            textViewTargetVersionName = view.FindViewById<TextView>(Resource.Id.textview_targetVersionName);
            textViewAndroidVersionName = view.FindViewById<TextView>(Resource.Id.textview_androidVersionName);

            string targetSdkVersion = ((int)packageInfo!.ApplicationInfo!.TargetSdkVersion).ToString() + " - " + packageInfo!.ApplicationInfo!.TargetSdkVersion;
            textViewTargetVersionName!.Text = targetSdkVersion;

            textviewSecurityPatchValue = view.FindViewById<TextView>(Resource.Id.textview_security_patch_value);
            textviewSecurityPatchValue!.Text = buildId; // securityPatchDisplay;

            // Make sure we did obtain a legit androidVersion from AndroidVersions
            if (androidVersion != null)
            {
                string androidName = !string.IsNullOrEmpty(androidVersion.AndroidName) ? " - " + androidVersion.AndroidName : string.Empty;
                textViewAndroidVersionName!.Text = androidVersion.AndroidName + " - " + androidVersion.AndroidApiNumber + " - " + androidVersion.AndroidCodeName;
            }
            else
                textViewAndroidVersionName!.Text = build;

            MaterialAlertDialogBuilder builder = new(Activity); 
            builder.SetTitle(Resource.String.about_dialog_title);
            builder.SetView(view);
            builder.SetPositiveButton(Android.Resource.String.Ok, delegate (object? o, DialogClickEventArgs e)
            {
                Dismiss();
            });
            return builder.Create();

        }
        #endregion
    }
}