using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System;

namespace hanee.ThreeD
{
    public class GripPoint : Point
    {
        public enum GripType
        {
            self,
            circleRadius,
            middle
        }

        public Entity entity { get; set; }
        public Entity[] explodedEntities { get; set; } // block인 경우 exploded한 객체들(움직일때는 이걸 움직인다)
        public GripType gripType { get; set; }

        public GripPoint(Entity entity, GripType gripType, devDept.Geometry.Point3D pt) : base(pt, GripManager.gripSize)
        {
            if (gripType == GripType.self)
            {
                this.Position = pt;
            }

            this.entity = entity;
            this.gripType = gripType;
            this.Color = GetColorByGripType(gripType);
            this.ColorMethod = colorMethodType.byEntity;
        }

        private System.Drawing.Color GetColorByGripType(GripType gripType)
        {
            if (gripType == GripType.circleRadius)
                return System.Drawing.Color.Green;
            else if (gripType == GripType.middle)
                return System.Drawing.Color.Aqua;

            return System.Drawing.Color.Blue;
        }
    }
}
