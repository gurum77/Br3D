using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    public class CircleGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var circle = entity as Circle;
            var originCircle = originEntity as Circle;
            if (circle == null || originCircle == null)
                return false;

            originCircle.Center.CopyFrom(circle.Center);
            originCircle.Radius = circle.Radius;
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var circle = entity as Circle;
            if (circle == null)
                return null;

            GripPoint sp = new GripPoint(circle, GripPoint.GripType.self, circle.Center);
            GripPoint ep = new GripPoint(circle, GripPoint.GripType.circleRadius, circle.StartPoint);

            return new List<GripPoint>() { sp, ep };
        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            var regenParams = new RegenParams(0.001, model);
            if (gp.gripType == GripPoint.GripType.circleRadius)
            {
                var circle = gp.entity as Circle;
                circle.Radius = circle.Center.DistanceTo(gp.Position);
            }

            gp.entity.Regen(regenParams);
        }
    }
}
