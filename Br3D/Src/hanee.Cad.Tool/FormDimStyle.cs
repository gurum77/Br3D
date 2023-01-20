using devDept.Eyeshot.Entities;
using System.Collections.Generic;

namespace hanee.Cad.Tool
{
    public partial class FormDimStyle : DevExpress.XtraEditors.XtraForm
    {
        public FormDimStyle(List<Entity> entities)
        {
            InitializeComponent();

            var dimensions = new List<Dimension>();
            foreach (var ent in entities)
            {
                if(ent is Dimension dim)
                {
                    dimensions.Add(dim);
                }
            }
            DimStyleProperties dsProp = new DimStyleProperties(dimensions);
            propertyGridControl1.SelectedObject = dsProp;
        }

        private void simpleButtonOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}