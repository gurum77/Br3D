using devDept.Eyeshot;
using devDept.Eyeshot.Entities;

namespace hanee.Cad.Tool
{
    public class ActionIntersection : ActionSubtract
    {
        public ActionIntersection(Environment environment) : base(environment)
        {
        }

        protected override Entity[] Calc()
        {
            if (ent1 is Brep brep1 && ent2 is Brep brep2)
            {
                return Brep.Intersection(brep1, brep2);
            }

            return null;
        }
    }
}
