using devDept.Eyeshot.Entities;
using System.Collections.Generic;

namespace hanee.Cad.Tool
{
    public partial class FormDimStyle : DevExpress.XtraEditors.XtraForm
    {
        List<Entity> entities = null;
        public FormDimStyle(List<Entity> entities)
        {
            InitializeComponent();

            this.entities = entities;
        }
    }
}