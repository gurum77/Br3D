using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormDynamicInput : DevExpress.XtraEditors.XtraForm
    {
        public FormDynamicInput()
        {
            InitializeComponent();
        }

        private void FormDynamicInput_MouseEnter(object sender, EventArgs e)
        {
            var loc = Cursor.Position;
            loc = PointToScreen(loc);
            loc.X += 50;
            this.Location = loc;

        }
    }
}