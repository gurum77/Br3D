using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;

namespace hanee.Cad.Tool
{
    public class ActionBox : ActionCylinder
    {
        public ActionBox(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        double GetWidth(Plane plane, Point3D secondPoint)
        {

            if (base.centerPoint == null || secondPoint == null)
                return 0;

            var widthPoint = secondPoint.IntersectionWith(plane.AxisY, centerPoint, plane.AxisX);
            if (widthPoint == null)
                return 0;

            return widthPoint.DistanceTo(centerPoint) * 2;
        }

        double GetDepth(Plane plane, Point3D secondPoint)
        {
            if (base.centerPoint == null || point3D == null)
                return 0;

            var depthPoint = secondPoint.IntersectionWith(plane.AxisX, centerPoint, plane.AxisY);
            if (depthPoint == null)
                return 0;

            return depthPoint.DistanceTo(centerPoint) * 2;
        }
        protected override Entity MakeSection()
        {
            var plane = GetWorkplane();
            var width = GetWidth(plane, point3D);
            var depth = GetDepth(plane, point3D);
            if (width == 0 || depth == 0)
                return null;

            
            var rect = LinearPathHelper.CreateRectangle(0, 0, 0, width, depth, true);
            var trans = new Transformation(centerPoint, plane.AxisX, plane.AxisY, plane.AxisZ);
            rect.TransformBy(trans);
            return rect;
        }

        protected override Entity Make3D(bool tempEntity)
        {
            if (oldPlane == null)
                return null;

            var width = GetWidth(oldPlane, radiusPoint);
            var depth = GetDepth(oldPlane, radiusPoint);
            var height = oldPlane.DistanceTo(heightPoint);
            if (width == 0 || depth == 0 || height == 0)
                return null;

            var reverseHeight = height < 0;
            height = Math.Abs(height);

            Entity box = null;
            if (tempEntity)
                box = Mesh.CreateBox(width, depth, height);
            else
                box = Brep.CreateBox(width, depth, height);
            
            box.TransformBy(new Transformation(base.centerPoint, oldPlane.AxisX, oldPlane.AxisY, reverseHeight ? oldPlane.AxisZ  * -1: oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(box, tempEntity);

            return box;
        }
    }
}
