using Android.Content;
using Android.Util;
using AndroidX.Preference;

namespace com.companyname.navigationgraph11net10
{
    public class RaceResultFragmentListPreference : ListPreference
    {

        internal readonly string DefaultColorValue = "Select background color";
        internal string[] themeEntries = ["Carbon Fibre", "Dark", "Light"];
        internal string[] themeValues = ["1", "2", "3"];

        #region Ctors
        public RaceResultFragmentListPreference(Context context) : base(context, null)
        {

        }
        public RaceResultFragmentListPreference(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }
        #endregion

        #region Init
        public void Init()
        {
            SetEntries(themeEntries);
            SetEntryValues(themeValues);

            if (!string.IsNullOrEmpty(Value))
                Summary = themeEntries[FindIndexOfValue(Value)];  // Get the current theme
            else
                Summary = DefaultColorValue;
        }
        #endregion
    }
}
