using System;
using System.Reflection;
using System.Windows.Forms;

namespace Br3D
{
    public partial class FormAbout : DevExpress.XtraEditors.XtraForm
    {
        public FormAbout()
        {
            InitializeComponent();

            labelControlVersion.Text = $"Version {GetVersion()}";
            if (VersionHelper.isLT)
            {
                labelControlLT.Visible = true;
                this.Text = "About Br3D LT";
            }
            else
            {
                labelControlLT.Visible = false;
            }
        }

        public string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString();

        }

        private void labelControlName_Click(object sender, EventArgs e)
        {

        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}