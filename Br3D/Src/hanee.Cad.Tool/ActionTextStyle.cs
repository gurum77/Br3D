using devDept.Eyeshot;
using hanee.ThreeD;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionTextStyle : ActionBase
    {
        static public FormTextStyle formTextStyle;
        Form owner;
        public ActionTextStyle(Environment environment, Form owner) : base(environment)
        {
            this.owner = owner;
        }

        public override void Run()
        {
            if (formTextStyle == null)
            {
                formTextStyle = new FormTextStyle(GetModel());
                formTextStyle.FormClosed += FormLayer_FormClosed;
            }

            formTextStyle.Show();
            formTextStyle.Owner = owner;
        }

        private void FormLayer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            formTextStyle = null;
        }
    }
}
