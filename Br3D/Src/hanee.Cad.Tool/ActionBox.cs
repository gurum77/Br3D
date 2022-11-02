using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionBox : ActionCylinder
    {
        public ActionBox(devDept.Eyeshot.Environment environment) : base(environment)
        {
            activeDynamicInputManagerForRadius = false;
        }

        double GetWidth(Plane plane, Point3D secondPoint)
        {

            if (base.centerPoint == null || secondPoint == null)
                return 0;

            var widthPoint = secondPoint.IntersectionWith(plane, plane.AxisY, centerPoint, plane.AxisX);
            if (widthPoint == null)
                return 0;

            return widthPoint.DistanceTo(centerPoint) * 2;
        }

        double GetDepth(Plane plane, Point3D secondPoint)
        {
            if (base.centerPoint == null || point3D == null)
                return 0;

            var depthPoint = secondPoint.IntersectionWith(plane, plane.AxisX, centerPoint, plane.AxisY);
            if (depthPoint == null)
                return 0;

            return depthPoint.DistanceTo(centerPoint) * 2;
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            // 
            base.OnMouseMove(environment, e);


            var sec = MakeSection() as Region;
            if (sec == null || sec.ContourList.Count == 0)
                return;
            
            var lp = sec.ContourList[0] as CompositeCurve;
            if (lp == null || lp.CurveList.Count < 4)
                return;

            var l1 = lp.CurveList[0] as Line;
            var l2 = lp.CurveList[1] as Line;
            if (l1 == null || l2 == null)
                return;
            PreviewLabel.PreviewDistanceLabel(model, l1.StartPoint, l1.EndPoint, 1, false, "W=");
            PreviewLabel.PreviewDistanceLabel(model, l2.StartPoint, l2.EndPoint, 2, false, "H=");

        }

     
        protected override Entity MakeSection()
        {
            // radiusPoint가 입력이 되어 있다면 plane이 바뀌기 때문에 section을 만들 수 없다.
            if (radiusPoint != null)
                return null;
                
            var plane = GetWorkplane();
            var width = GetWidth(plane, point3D);
            var depth = GetDepth(plane, point3D);
            if (width == 0 || depth == 0)
                return null;

            var secPlane = plane.Clone() as Plane;
            secPlane.Origin = centerPoint;
            var region = Region.CreateRectangle(secPlane, width, depth, true);
            GetHModel().entityPropertiesManager.SetDefaultProperties(region, true);
            return region;
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
            
            var leftBottom = base.centerPoint;
            leftBottom = leftBottom + oldPlane.AxisX * -width / 2;
            leftBottom = leftBottom + oldPlane.AxisY * -depth / 2;
            box.TransformBy(new Transformation(leftBottom, oldPlane.AxisX, oldPlane.AxisY, reverseHeight ? oldPlane.AxisZ  * -1: oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(box, tempEntity);

            return box;
        }
    }
}
