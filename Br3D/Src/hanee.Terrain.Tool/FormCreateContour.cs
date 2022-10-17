using devDept.Eyeshot;
using System;

namespace hanee.Terrain.Tool
{
    public partial class FormCreateContour : DevExpress.XtraEditors.XtraForm
    {
        Model model;
        public FormCreateContour(Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        private void FormCreateContour_Load(object sender, EventArgs e)
        {
            foreach(var la in model.Layers)
            {
                comboBoxEditMinorLayer.Properties.Items.Add(la.Name);
                comboBoxEditMajorLayer.Properties.Items.Add(la.Name);
            }

            if (comboBoxEditMinorLayer.Properties.Items.Count > 0)
                comboBoxEditMinorLayer.SelectedIndex = 0;
            if (comboBoxEditMajorLayer.Properties.Items.Count > 0)
                comboBoxEditMajorLayer.SelectedIndex = 0;
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}