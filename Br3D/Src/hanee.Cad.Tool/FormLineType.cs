using devDept.Eyeshot;
using hanee.ThreeD;
using System;

namespace hanee.Cad.Tool
{
    public partial class FormLineType : DevExpress.XtraEditors.XtraForm
    {
        Model design;
        public FormLineType(devDept.Eyeshot.Model design)
        {
            InitializeComponent();

            this.design = design;
            lineTypeControl1.SetDesign(design);

            Translate();
        }


        public void RefreshDataSource()
        {
            lineTypeControl1.RefreshDataSource();
        }

        private void Translate()
        {
            Text = LanguageHelper.Tr("Line Type");
            simpleButtonClose.Text = LanguageHelper.Tr("Close");
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}