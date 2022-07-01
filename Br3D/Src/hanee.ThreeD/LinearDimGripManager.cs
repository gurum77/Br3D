using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class LinearDimGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var ld = entity as LinearDim;
            var originLd = originEntity as LinearDim;
            if (ld == null || originLd == null)
                return false;

            originLd.ExtLine1.CopyFrom(ld.ExtLine1);
            originLd.ExtLine2.CopyFrom(ld.ExtLine2);
            originLd.DimLinePosition.CopyFrom(ld.DimLinePosition);
            originLd.TextOverride = $"<>";
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var ld = entity as LinearDim;
            if (ld == null)
                return null;

            model.StartWorkspace(ld.Plane);
            
            var lp = ld.PreviewEntity();

            var gripPoints = new List<GripPoint>();
            var gp = new GripPoint(ld, GripPoint.GripType.self, ld.ExtLine1);
            gp.explodedEntities = new Entity[] { lp };
            gripPoints.Add(gp);

            gp = new GripPoint(ld, GripPoint.GripType.self, ld.ExtLine2);
            gp.explodedEntities = new Entity[] { lp };
            gripPoints.Add(gp);

            gp = new GripPoint(ld, GripPoint.GripType.self, ld.DimLinePosition);
            gp.explodedEntities = new Entity[] { lp };
            gripPoints.Add(gp);

            return gripPoints;
        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            var ld = gp.entity as LinearDim;
            var newLp = ld.PreviewEntity() as LinearPath;
            if (newLp == null || gp.explodedEntities?.Length == 0)
                return;
            
            var lp = gp.explodedEntities[0] as LinearPath;
            if (newLp.Vertices.Length != lp.Vertices.Length)
                return;

            for (int i = 0; i < lp.Vertices.Length; ++i)
            {
                lp.Vertices[i].CopyFrom(newLp.Vertices[i]);
            }
        }
    }
}
