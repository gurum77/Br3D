using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    public class ActionHomepage : ActionBase
    {
        public ActionHomepage(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override void Run()
        {
            StartAction();

            System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");

            EndAction();
        }
    }
}
