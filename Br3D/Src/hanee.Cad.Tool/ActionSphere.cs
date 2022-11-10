using devDept.Eyeshot.Entities;
using hanee.ThreeD;
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
            if (centerPoint == null)
                return;

            var circle = MakeEntity(true);
            if (circle == null)
                return;

            SetTempEtt(environment, circle);

            PreviewLabel.PreviewDistanceLabel(model, centerPoint, point3D, 0, true, "R=");
        }

        protected override Entity MakeEntity(bool tempEntity = false)
        {
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

            ent.Translate(centerPoint.X, centerPoint.Y, centerPoint.Z);
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(ent, tempEntity);

            return ent;
        }
    }
}
