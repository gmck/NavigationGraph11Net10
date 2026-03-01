using Android.Util;
using Android.Views;
using AndroidX.Core.View;
using AndroidX.Fragment.App;

namespace com.companyname.navigationgraph11net10.Fragments
{
    public class ImmersiveFragment : Fragment
    {

        public ImmersiveFragment() { }

        #region OnStart
        public override void OnStart()
        {
            base.OnStart();

            // Hide the Status bar and Navigation bar and the SupportActionBar and the BottomNavigationView
            HideSystemUi();
            HideToolbarDisableNavDrawer();
        }
        #endregion

        #region OnDestroy
        public override void OnDestroyView()
        {
            // Opposite of HideSystemUi
            ShowSystemUi();
            ShowToolbarEnableNavDrawer();

            base.OnDestroyView();
        }
        #endregion

        #region HideSystemUi
        public void HideSystemUi()
        {
            // Had to add the following line to ensure the immersiveFragment went full screen on launch, without it it left a black rectangle where the statusbar had been. 
            // Note it would display correctly after one rotation on return from the rotation was ok.
            Activity!.Window!.AddFlags(WindowManagerFlags.LayoutNoLimits);

            WindowInsetsControllerCompat? windowInsetsControllerCompat = WindowCompat.GetInsetsController(Activity.Window, Activity.Window.DecorView);
            windowInsetsControllerCompat!.Hide(WindowInsetsCompat.Type.SystemBars());
            windowInsetsControllerCompat.SystemBarsBehavior = WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe;
        }
        #endregion

        #region ShowSystemUi
        private void ShowSystemUi()
        {
            Log.Debug("ImmersiveFragment", "ShowSystemUi called");
            
            Activity!.Window!.ClearFlags(WindowManagerFlags.LayoutNoLimits);// We had to add, therefore we need to clear it.
            
            WindowInsetsControllerCompat? windowInsetsControllerCompat = WindowCompat.GetInsetsController(Activity.Window, Activity.Window.DecorView);
            windowInsetsControllerCompat!.Show(WindowInsetsCompat.Type.SystemBars());
        }
        #endregion

        #region HideToolbarDisableNavDrawer
        private void HideToolbarDisableNavDrawer()
        {
            if (Activity is MainActivity mainActivity)
            {
                mainActivity.SupportActionBar!.Hide();
                mainActivity.DisableDrawerLayout();
            }
        }
        #endregion

        #region ShowToolbarEnableNavDrawer
        private void ShowToolbarEnableNavDrawer()
        {
            if (Activity is MainActivity mainActivity)
            {
                mainActivity.SupportActionBar!.Show();
                mainActivity.EnableDrawerLayout();
            }
        }
        #endregion
    }
}


