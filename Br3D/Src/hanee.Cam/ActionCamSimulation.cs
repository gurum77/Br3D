using devDept.Eyeshot;
using devDept.Eyeshot.Milling;
using devDept.Geometry;
using DevExpress.XtraBars;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cam
{
    public class ActionCamSimulation : ActionBase
    {
        string nccFileName;
        public ActionCamSimulation(devDept.Eyeshot.Environment environment, string nccFileName) : base(environment)
        {
            this.nccFileName = nccFileName;
        }

        public override void Run()
        {
            StartAction();

            FormSimulation form = new FormSimulation(nccFileName);
            form.ShowDialog();
           
            EndAction();
        }
    }
}
