using devDept.Eyeshot;
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
    public partial class ControlSimulation : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlSimulation()
        {
            InitializeComponent();
            manufacture1.Unlock("US21-D8G5N-12J8F-5F65-RD3W");
        }

        private void ControlSimulation_Load(object sender, EventArgs e)
        {
            manufacture1.ActiveViewport.Rotate.RotationMode = rotationType.Turntable;
            manufacture1.ActiveViewport.Rotate.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.Ctrl);
            manufacture1.ActiveViewport.Pan.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.None);
        }

        // 파일 로딩
        public bool OpenFile(string nccFileName)
        {
            ReadFileAsync rf = FileHelper.GetReadFileAsync(nccFileName);
            if (rf is ReadGCode)
            {
                rf.DoWork();
                rf.AddToScene(manufacture1);
                manufacture1.SetView(viewType.Trimetric, true, manufacture1.AnimateCamera);
                manufacture1.DrawGCode(new devDept.Eyeshot.Milling.EndMill(0.6, 0.3));
                manufacture1.ZoomFit();
                manufacture1.Invalidate();
                return true;
            }

            return false;
        }

    }
}
