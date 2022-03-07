using devDept.Eyeshot;
using hanee.ThreeD;
using System;

namespace hanee.Cad.Tool
{
    public partial class FormLayer : DevExpress.XtraEditors.XtraForm
    {
        Model design;
        public FormLayer(Model design)
        {
            InitializeComponent();
            this.design = design;
            layerControl1.SetDesign(design);

            Translate();
        }

        public void RefreshDataSource()
        {
            layerControl1.RefreshDataSource();
        }

        private void Translate()
        {
            simpleButtonClose.Text = LanguageHelper.Tr("Close");
        }

        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            string newLayerName = "New Layer";
            int num = 1;
            while (true)
            {
                newLayerName = $"New Layer{num++}";
                if (!design.Layers.Contains(newLayerName))
                    break;
            }
            design.Layers.Add(newLayerName);
            RefreshDataSource();
        }

        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            layerControl1.GetGridView().DeleteSelectedRows();
        }

        private void layerControl1_Load(object sender, EventArgs e)
        {

        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}