using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    public class Action2D3DView : ActionBase
    {
        bool set2DView;
        FormMain formMain;
        public Action2D3DView(devDept.Eyeshot.Environment environment, FormMain formMain, bool set2DView) : base(environment)
        {
            this.formMain = formMain;
            this.set2DView = set2DView;
        }

        public override void Run()
        {
            StartAction();

            var hModel = GetHModel();
            if (set2DView)
                hModel.Set2DView();
            else
                hModel.Set3DView();
            formMain.Update2D3DButton();

            EndAction();
        }
    }
}
