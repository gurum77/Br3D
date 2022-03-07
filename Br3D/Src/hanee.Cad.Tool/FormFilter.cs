using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public partial class FormFilter : Form
    {
        Model model;
        public FormFilter(Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        public void SetModel(Model model)
        {
            this.model = model;
        }
        // 객체 number로 선택
        private void buttonSelectByNo_Click(object sender, EventArgs e)
        {
            int no = 0;
            if(int.TryParse(textBoxNo.Text, out no))
            {
                Entity ent = model.Entities[no - 1];
                if(ent != null)
                {
                    ent.Selected = true;
                    model.Invalidate();
                }

            }
        }
    }
}
