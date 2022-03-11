using devDept.Eyeshot;
using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    public class GripPoint : Point
    {
        public enum GripType
        {
            self,
            circleRadius
        }

        public Entity entity { get; set; }
        public Entity[] explodedEntities{ get; set; } // block인 경우 exploded한 객체들(움직일때는 이걸 움직인다)
        public GripType gripType { get; set; }

        public GripPoint(Entity entity, GripType gripType, devDept.Geometry.Point3D pt) : base(pt, GripManager.gripSize)
        {
            if (gripType == GripType.self)
            {
                this.Position = pt;
            }
            
            this.entity = entity;
            this.gripType = gripType;
            this.Color = System.Drawing.Color.Blue;
            this.ColorMethod = colorMethodType.byEntity;
        }
    }
}
