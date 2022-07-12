using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Br3D
{

    public partial class ControlAds : DevExpress.XtraEditors.XtraUserControl
    {

        [Bindable(true)]
        public string url { get; set; } = "https://hileejaeho.cafe24.com/br3d-ad";

        public ControlAds()
        {
#if !DEBUG
            DevExpress.Utils.BrowserEmulationHelper.DisableBrowserEmulation(System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
#endif
            InitializeComponent();

            Visible = false;
        }

        public void ShowAd()
        {
            webBrowser1.ScrollBarsEnabled = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Visible = true;
        }

        private void ControlAds_Load(object sender, EventArgs e)
        {

        }
    }
}
