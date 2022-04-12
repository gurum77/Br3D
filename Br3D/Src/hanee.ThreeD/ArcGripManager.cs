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
    public class ArcGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var arc = entity as Arc;
            var originArc = originEntity as Arc;
            if (arc == null || originArc == null)
                return false;

            originArc.Center.CopyFrom(arc.Center);
            originArc.Radius = arc.Radius;
            
           
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var arc = entity as Arc;
            if (arc == null)
                return null;

            GripPoint cp = new GripPoint(arc, GripPoint.GripType.self, arc.Center);
            GripPoint mp = new GripPoint(arc, GripPoint.GripType.circleRadius, arc.MidPoint);


            return new List<GripPoint>() { cp, mp};

        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            var arc = gp.entity as Arc;
            if (gp.gripType == GripPoint.GripType.circleRadius)
                arc.Radius = gp.Position.DistanceTo(arc.Center);
           
            var regenParams = new RegenParams(0.001, model);
            gp.entity.Regen(regenParams);
        }
    }
}
