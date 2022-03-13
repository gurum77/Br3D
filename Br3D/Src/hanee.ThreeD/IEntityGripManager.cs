using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public interface IEntityGripManager
    {
        List<GripPoint> GetGripPoints(Entity entity, Model model);
        bool EndEdit(Entity entity, Entity originEntity);
        void MouseMove(Model model, GripPoint gp, Point3D newPt);
    }
}
