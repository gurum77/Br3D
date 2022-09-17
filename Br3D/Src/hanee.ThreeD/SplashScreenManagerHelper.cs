using DevExpress.XtraSplashScreen;

namespace hanee.ThreeD
{
    public static class SplashScreenManagerHelper
    {
        static public void SafeCloseForm()
        {
            if (SplashScreenManager.Default == null)
                return;
            SplashScreenManager.CloseForm();
        }
    }
}
