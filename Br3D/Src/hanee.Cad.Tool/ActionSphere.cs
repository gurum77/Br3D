using devDept.Eyeshot.Entities;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionSphere : ActionCircle
    {
        public ActionSphere(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            if (startPoint == null)
                return;

            endPoint = point3D;


            var circle = MakeEntity(true);
            if (circle == null)
                return;

            SetTempEtt(environment, circle);
        }

        protected override Entity MakeEntity(bool tempEntity = false)
        {
            var radius = startPoint.DistanceTo(endPoint);
            if (radius <= 0.001)
                return null;

            Entity ent = null;
            if (tempEntity)
            {
                ent = Mesh.CreateSphere(radius, 10, 10);
            }
            else
            {
                ent = Brep.CreateSphere(radius, radius / 10);
            }

            ent.Translate(startPoint.X, startPoint.Y, startPoint.Z);
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(ent, tempEntity);

            return ent;
        }
    }
}
