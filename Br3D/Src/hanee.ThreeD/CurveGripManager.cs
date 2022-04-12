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
    public class CurveGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var curve = entity as Curve;
            var originCurve = originEntity as Curve;
            if (curve == null || originCurve == null)
                return false;

            if (curve.ControlPoints.Length != originCurve.ControlPoints.Length)
                return false;

            for (int i = 0; i < curve.ControlPoints.Length; ++i)
            {
                originCurve.ControlPoints[i].CopyFrom(curve.ControlPoints[i]);
            }

            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var curve = entity as Curve;
            if (curve == null)
                return null;


            var gripPoints = new List<GripPoint>();
            foreach (Point4D v in curve.ControlPoints)
            {
                var gp = new GripPoint(entity, GripPoint.GripType.self, v);
                gripPoints.Add(gp);
            }

            return gripPoints;
        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            gp.entity.Regen(0.001);
        }
    }
}
