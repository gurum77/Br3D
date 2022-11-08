using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    public class ActionAbout : ActionBase
    {
        public ActionAbout(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override void Run()
        {
            StartAction();
            
            new FormAbout().ShowDialog();

            EndAction();
        }
    }
}
