using devDept.Eyeshot.Entities;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class LineGripManager : IEntityGripManager
    {
        public List<GripPoint> GetGripPoints(Entity entity)
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
