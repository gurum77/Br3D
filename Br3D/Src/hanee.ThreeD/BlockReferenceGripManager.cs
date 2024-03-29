﻿using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class BlockReferenceGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var br = entity as BlockReference;
            var originBr = originEntity as BlockReference;
            if (br == null || originBr == null)
                return false;

            originBr.InsertionPoint = br.InsertionPoint;
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var br = entity as BlockReference;
            if (br == null)
                return null;



            GripPoint gp = new GripPoint(br, GripPoint.GripType.self, br.InsertionPoint);
            // block은 explode해서 추가한다.
            gp.explodedEntities = br.Explode(model.Blocks);
            if (gp.explodedEntities != null)
            {

                var regenParams = new RegenParams(0.001, model);

                foreach (var ee in gp.explodedEntities)
                    ee.Regen(regenParams);
            }
            return new List<GripPoint>() { gp };
        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            var br = gp.entity as BlockReference;
            if (br == null)
                return;

            var regenParams = new RegenParams(0.001, model);
            var vec = (newPt - gp.Position).AsVector;
            br.InsertionPoint = newPt;
            foreach (var ent in gp.explodedEntities)
            {
                ent.Translate(vec);
                ent.Regen(regenParams);
            }
        }
    }
}
