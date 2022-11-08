using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    class ActionSave : ActionBase
    {
        FormMain formMain;
        public ActionSave(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            StartAction();
            
            if (string.IsNullOrEmpty(formMain.opendFilePath))
            {
                formMain.SaveAs();
            }
            else
            {
                formMain.Export(formMain.opendFilePath);
            }

            EndAction();
        }
    }
}
