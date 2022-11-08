using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Br3D.Actions
{
    class ActionReload : ActionBase
    {
        FormMain formMain;
        public ActionReload(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            StartAction();

            var re = XtraMessageBox.Show(LanguageHelper.Tr("Do you want to reload the current file?"), LanguageHelper.Tr("Reload"), MessageBoxButtons.YesNo);
            if (re == DialogResult.No)
            {
                EndAction();
                return;
            }

            formMain.Import(formMain.opendFilePath, true);
            EndAction();
        }
    }
}
