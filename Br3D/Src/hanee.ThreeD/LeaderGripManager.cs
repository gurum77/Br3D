using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class LeaderGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var leader = entity as Leader;
            var originLeader = originEntity as Leader;
            if (leader == null || originLeader == null)
                return false;


            for (int i = 0; i < leader.Vertices.Length; i++)
            {
                if (i >= originLeader.Vertices.Length)
                    continue;

                Point3D v = leader.Vertices[i];
                originLeader.Vertices[i].CopyFrom(v);
            }
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var gripPoints = new List<GripPoint>();
            var leader = entity as Leader;
            if (leader == null)
                return gripPoints;

            var lp = new LinearPath(leader.Vertices);
            
            lp.Color = System.Drawing.Color.White;
            lp.ColorMethod = colorMethodType.byEntity;
            foreach (var v in leader.Vertices)
            {
                var gp = new GripPoint(entity, GripPoint.GripType.self, v);
                gp.explodedEntities = new Entity[] { lp };
                gripPoints.Add(gp);
            }

            


            return gripPoints;
        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            var regenParams = new RegenParams(0.001, model);
            foreach (var ent in gp.explodedEntities)
                ent.Regen(regenParams);
        }
    }
}
