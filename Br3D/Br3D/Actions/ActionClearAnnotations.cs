using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    class ActionClearAnnotations : ActionBase
    {
        public ActionClearAnnotations(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override void Run()
        {
            StartAction();

            model.ActiveViewport.Labels.Clear();
            model.Invalidate();

            EndAction();
        }
    }
}
