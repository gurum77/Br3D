using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    public class LinearPathGripManager : IEntityGripManager
    {
        public List<GripPoint> GetGripPoints(Entity entity)
        {
            var lp = entity as LinearPath;
            if (lp == null)
                return null;

            var points = new List<GripPoint>();
            foreach (var v in lp.Vertices)
            {
                GripPoint p = new GripPoint(lp, GripPoint.GripType.self, v);
                points.Add(p);
            }

            return points;
        }
    }
}
