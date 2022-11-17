using devDept.Eyeshot;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    public class ActionDisplayMode : ActionBase
    {
        displayType displayMode;
        FormMain formMain;
        public ActionDisplayMode(devDept.Eyeshot.Environment environment, FormMain formMain, displayType displayMode) : base(environment)
        {
            this.displayMode = displayMode;
            this.formMain = formMain;
        }

        // display mode 는 action 진행에 영향을 주면 안되므로 start action / end action을 호출 하지 않는다.
        public override void Run()
        {
            if (model == null)
                return;
            if (displayMode == displayType.Rendered)
                model.ActiveViewport.DisplayMode = displayType.Rendered;
            else if (displayMode == displayType.Shaded)
                model.ActiveViewport.DisplayMode = displayType.Shaded;
            else if (displayMode == displayType.HiddenLines)
                model.ActiveViewport.DisplayMode = displayType.HiddenLines;
            else if (displayMode == displayType.Wireframe)
                model.ActiveViewport.DisplayMode = displayType.Wireframe;
            model.Invalidate();
            formMain.UpdateDisplayModeButton();

            CmdBarManager.InitTextEdit();
        }
    }
}
