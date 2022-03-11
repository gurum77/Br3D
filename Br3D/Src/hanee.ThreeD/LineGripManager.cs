using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.Geometry;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class LineGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var line = entity as Line;
            var originLine = originEntity as Line;
            if (line == null || originLine == null)
                return false;

            originLine.StartPoint.CopyFrom(line.StartPoint);
            originLine.EndPoint.CopyFrom(line.EndPoint);
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var line = entity as Line;
            if (line == null)
                return null;

            GripPoint sp = new GripPoint(line, GripPoint.GripType.self, line.StartPoint);
            GripPoint ep = new GripPoint(line, GripPoint.GripType.self, line.EndPoint);

            return new List<GripPoint>() { sp, ep };
        }
    }
}
