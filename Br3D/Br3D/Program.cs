using DevExpress.XtraSplashScreen;
using System;
using System.Windows.Forms;

namespace Br3D
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VersionHelper.InitVersion();

            SplashScreenManager.ShowForm(typeof(SplashScreen1), true, false);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
