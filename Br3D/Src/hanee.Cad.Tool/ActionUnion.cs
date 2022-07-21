using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionUnion : ActionSubtract
    {
        public ActionUnion(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        protected override Entity[] Calc()
        {
            if (ent1 is Brep brep1 && ent2 is Brep brep2)
            {
                return Brep.Union(brep1, brep2);
            }

            return null;
        }
    }
}
