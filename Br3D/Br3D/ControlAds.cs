using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Br3D
{

    public partial class ControlAds : DevExpress.XtraEditors.XtraUserControl
    {

        [Bindable(true)]
        public string url { get; set; } = "https://hileejaeho.cafe24.com/br3d-ad";

        public ControlAds()
        {
            InitializeComponent();
        }

        public async void ShowAd()
        {
            var cacheFolderPath = Path.Combine(Path.GetTempPath(), "Br3D.exe", "WebView2");
            var webview2Env = await CoreWebView2Environment.CreateAsync(null, cacheFolderPath);
            await webView21.EnsureCoreWebView2Async(webview2Env);
            webView21.Source = new Uri(url);
            webView21.NavigationCompleted += WebView21_NavigationCompleted;
        }

        private void WebView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                ((WebView2)sender).ExecuteScriptAsync("document.querySelector('body').style.overflow='hidden'");
                Visible = true;
            }
        }

        private void ControlAds_Load(object sender, EventArgs e)
        {

        }
    }
}
;