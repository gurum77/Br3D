using devDept.Eyeshot;
using hanee.ThreeD;
using System;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionLineType : ActionBase
    {
        static public FormLineType formLineType;
        Form owner;
        public ActionLineType(devDept.Eyeshot.Environment environment, Form owner) : base(environment)
        {
            this.owner = owner;
        }

        public override void Run()
        {
            if (formLineType == null)
            {
                formLineType = new FormLineType(GetModel());
                formLineType.FormClosed += FormLayer_FormClosed;
            }

            formLineType.Show();
            formLineType.Owner = owner;
        }

        private void FormLayer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            formLineType = null;
        }
    }
}
