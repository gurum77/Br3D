using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    public class ActionNew : ActionBase
    {
        FormMain formMain;
        public ActionNew(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            StartAction();

            if (!formMain.CheckSaveForModifiedFile())
            {
                EndAction();
                return;
            }

            formMain.NewFile();

            EndAction();
        }
    }
}
