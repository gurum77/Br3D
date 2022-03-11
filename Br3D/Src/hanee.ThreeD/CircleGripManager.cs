using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
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
    }
}
