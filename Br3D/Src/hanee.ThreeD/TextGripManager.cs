using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.Geometry;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class TextGripManager : IEntityGripManager
    {
        public bool EndEdit(Entity entity, Entity originEntity)
        {
            var text = entity as Text;
            var originText = originEntity as Text;
            if (text == null || originText == null)
                return false;

            originText.InsertionPoint.CopyFrom(text.InsertionPoint);
            return true;
        }

        public List<GripPoint> GetGripPoints(Entity entity, Model model)
        {
            Text text = entity as Text;
            if (text == null)
                return null;


            GripPoint gp = new GripPoint(text, GripPoint.GripType.self, text.InsertionPoint);

            return new List<GripPoint>() { gp };
        }
    }
}
