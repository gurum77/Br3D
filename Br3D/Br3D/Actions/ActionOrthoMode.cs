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

        // ortho mode는 action 진행에 영향을 주면 안되므로 start action / end action을 호출 하지 않는다.
        public override void Run()
        {
            var hModel = GetHModel();
            hModel.orthoModeManager.enabled = !hModel.orthoModeManager.enabled;
            barButtonItem.Down = hModel.orthoModeManager.enabled;
        }
    }
}
