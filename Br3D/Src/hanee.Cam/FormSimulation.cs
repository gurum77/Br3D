using devDept.Eyeshot.Translators;
using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cam
{
    public partial class FormSimulation : DevExpress.XtraEditors.XtraForm
    {
        string nccFileName;
        public FormSimulation(string nccFileName)
        {
            InitializeComponent();
            this.nccFileName = nccFileName;
            
        }

        private void FormSimulation_Load(object sender, EventArgs e)
        {
            controlSimulation1.OpenFile(nccFileName);
        }
    }
}