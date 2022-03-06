using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    public class GripPoint : Point
    {
        public enum GripType
        {
            self,
        }

        public Entity entity { get; set; }
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
