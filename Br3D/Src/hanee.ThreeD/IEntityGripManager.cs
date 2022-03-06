using devDept.Eyeshot.Entities;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public interface IEntityGripManager
    {
        List<GripPoint> GetGripPoints(Entity entity);
    }
}
