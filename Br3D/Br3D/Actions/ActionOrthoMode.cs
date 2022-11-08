using DevExpress.XtraBars;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    public class ActionOrthoMode : ActionBase
    {
        BarButtonItem barButtonItem;
        public ActionOrthoMode(devDept.Eyeshot.Environment environment, BarButtonItem barButtonItem) : base(environment)
        {
            this.barButtonItem = barButtonItem;
        }

        public override void Run()
        {
            StartAction();


            var hModel = GetHModel();
            hModel.orthoModeManager.enabled = !hModel.orthoModeManager.enabled;
            barButtonItem.Down = hModel.orthoModeManager.enabled;

            EndAction();
        }
    }
}
