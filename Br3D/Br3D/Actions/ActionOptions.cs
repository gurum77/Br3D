using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Br3D.Actions
{
    public class ActionOptions : ActionBase
    {
        FormMain formMain;
        public ActionOptions(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            StartAction();

            FormOptions form = new FormOptions();
            if (form.ShowDialog() == DialogResult.OK)
            {
                bool regen = Options.Instance.IsNeedRegen(form.lastOptions);
                if (regen)
                {
                    if (form.lastOptions.curLinetypeScale != Options.Instance.curLinetypeScale)
                    {
                        // ltscale이 변경되었으면 객체에 반영해야 함
                        foreach (var ent in model.Entities)
                        {
                            ent.LineTypeScale = Options.Instance.curLinetypeScale;
                        }
                    }
                }

                formMain.ApplyOptions(regen);
            }

            EndAction();
        }
    }
}
