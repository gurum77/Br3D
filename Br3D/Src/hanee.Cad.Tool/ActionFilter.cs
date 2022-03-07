using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hanee.ThreeD;

namespace hanee.Cad.Tool
{
    public class ActionFilter : ActionBase
    {
        static FormFilter form = null;
        public ActionFilter(devDept.Eyeshot.Environment environment) : base(environment)
        {
            if(form == null)
            {
                form = new FormFilter(GetModel());
            }
            else
            {
                form.SetModel(GetModel());
            }
        }

        public override void Run()
        {
            StartAction();

            form.Show();

            EndAction();
        }

    }
}
