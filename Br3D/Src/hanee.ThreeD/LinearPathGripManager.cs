using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.Geometry;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class LinearPathGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var lp = entity as LinearPath;
            var originLp = originEntity as LinearPath;
            if (lp == null || originLp == null)
                return false;

            if (lp.Vertices.Length != originLp.Vertices.Length)
                return false;

            for (int i = 0; i < lp.Vertices.Length; i++)
            {
                originLp.Vertices[i].CopyFrom(lp.Vertices[i]);
            }

            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
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
