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
    public class RadialDimGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var rd = entity as RadialDim;
            var originRd = originEntity as RadialDim;
            if (rd == null || originRd == null)
                return false;

            originRd.DimLinePosition.CopyFrom(rd.DimLinePosition);
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            var rd = entity as RadialDim;


            var l = new Line(rd.InsertionPoint, rd.DimLinePosition);
            var gp = new GripPoint(entity, GripPoint.GripType.self, rd.DimLinePosition);
            gp.explodedEntities = new Entity[] { l };

            return new List<GripPoint>() { gp };
        }

        public void MouseMove(Model model, GripPoint gp, Point3D newPt)
        {
            
        }
    }
}
