using devDept.Geometry;
using hanee.Geometry;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class OrthoModeManager
    {

        public System.Drawing.Color AxisXColor { get; set; } = System.Drawing.Color.FromArgb(150, System.Drawing.Color.Red);
        public System.Drawing.Color AxisYColor { get; set; } = System.Drawing.Color.FromArgb(150, System.Drawing.Color.Green);
        public System.Drawing.Color AxisZColor { get; set; } = System.Drawing.Color.FromArgb(150, System.Drawing.Color.Blue);
        public System.Drawing.Color AxisBodyColor { get; set; } = System.Drawing.Color.FromArgb(100, System.Drawing.Color.Gray);

        public enum Axis
        {
            none,
            x,
            y,
            z
        }
        public Point3D startPoint { get; set; } = null;
        public bool enabled { get; set; } = true;
        HModel model { get; set; }
        public Axis axis { get; private set; }
        public Point3D lastOrthoPoint { get; set; } = null;
        public Vector3D lastOrthoDir { get; set; } = null;

        public OrthoModeManager(HModel model)
        {
            this.model = model;
        }


        public Point3D GetOrthoPoint3D(MouseEventArgs e, Point3D curPoint)
        {
            axis = Axis.none;
            lastOrthoPoint = null;
            lastOrthoDir = null;

            if (curPoint == null)
                return null;
            if (model == null)
                return curPoint;

            if (!enabled)
                return curPoint;
            if (startPoint == null)
                return curPoint;

            Point3D ptAxisX = null;
            Point3D ptAxisY = null;
            Point3D ptAxisZ = null;
            double distAxisX = LineHelper.GetInfiniteLength();
            double distAxisY = LineHelper.GetInfiniteLength();
            double distAxisZ = LineHelper.GetInfiniteLength();

            List<KeyValuePair_elements<double, Point3D>> points = new List<KeyValuePair_elements<double, Point3D>>();

            var line = LineHelper.CreateInfinitLine(startPoint, Vector3D.AxisX);
            if (line.Project(curPoint, out double t))
            {
                ptAxisX = line.PointAt(t);
                distAxisX = ptAxisX.DistanceTo(curPoint);
                points.Add(new KeyValuePair_elements<double, Point3D>(distAxisX, ptAxisX));
            }

            line = LineHelper.CreateInfinitLine(startPoint, Vector3D.AxisY);
            if (line.Project(curPoint, out t))
            {
                ptAxisY = line.PointAt(t);
                distAxisY = ptAxisY.DistanceTo(curPoint);
                points.Add(new KeyValuePair_elements<double, Point3D>(distAxisY, ptAxisY));
            }

            line = LineHelper.CreateInfinitLine(startPoint, Vector3D.AxisZ);
            if (line.Project(curPoint, out t))
            {
                ptAxisZ = line.PointAt(t);
                distAxisZ = ptAxisZ.DistanceTo(curPoint);
                points.Add(new KeyValuePair_elements<double, Point3D>(distAxisZ, ptAxisZ));
            }

            if (points.Count == 0)
                return curPoint;

            // 정렬
            points.Sort((x1, x2) => x1.Key < x2.Key ? -1 : 1);

            var result = points[0].Value;
            if (result == ptAxisX)
            {
                axis = Axis.x;
                lastOrthoDir = Vector3D.AxisX;
            }
            else if (result == ptAxisY)
            {

                axis = Axis.y;
                lastOrthoDir = Vector3D.AxisY;
            }
            else
            {

                axis = Axis.z;
                lastOrthoDir = Vector3D.AxisZ;
            }

            lastOrthoPoint = result;
            return result;
        }

        internal void DrawOverlayForOrthoMode()
        {
            if (axis == Axis.none)
                return;

            // Draw in 2D the parts of the lines before the near camera plane
            model.renderContext.SetShader(devDept.Graphics.shaderType.NoLights);

            for (int i = 0; i < model.Viewports.Count; i++)
            {
                var viewport = model.Viewports[i];

                var sl = model.GetScreenLength(startPoint, 1);
                var len = 50.0f / sl;
                var axisLen = len / 3;
                var axisThick = len / 10;


                var points = new List<Point3D>();
                var axisPoints = new List<Point3D>();
                var color = AxisXColor;

                if (axis == Axis.x || axis == Axis.y)
                {
                    // 평면에 네모
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XY, startPoint, len, len), AxisBodyColor);

                    // x 축
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XY, startPoint + Plane.XY.AxisX * len / 2, axisLen, axisThick), AxisXColor);
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XY, startPoint + Plane.XY.AxisX * -len / 2, axisLen, axisThick), AxisXColor);

                    // y축
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XY, startPoint + Plane.XY.AxisY * len / 2, axisThick, axisLen), AxisYColor);
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XY, startPoint + Plane.XY.AxisY * -len / 2, axisThick, axisLen), AxisYColor);
                }
                else if (axis == Axis.z)
                {
                    // 평면에 네모
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XZ, startPoint, len, len), AxisBodyColor);

                    // x 축
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XZ, startPoint + Plane.XZ.AxisX * len / 2, axisLen, axisThick), AxisXColor);
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XZ, startPoint + Plane.XZ.AxisX * -len / 2, axisLen, axisThick), AxisXColor);

                    // z축
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XZ, startPoint + Plane.XZ.AxisY * len / 2, axisThick, axisLen), AxisZColor);
                    DrawTriangles(GetRectanglePointsOnPlane(Plane.XZ, startPoint + Plane.XZ.AxisY * -len / 2, axisThick, axisLen), AxisZColor);
                }
            }
        }

        private void DrawTriangles(List<Point3D> points, System.Drawing.Color color)
        {
            var screenPoints = points.ConvertAll(x => model.WorldToScreen(x));
            model.renderContext.SetColorWireframe(color);
            model.renderContext.SetState(devDept.Graphics.blendStateType.Blend);
            model.renderContext.DrawTriangles(screenPoints.ToArray());
        }

        private List<Point3D> GetRectanglePointsOnPlane(Plane plane, Point3D startPoint, float width, float height)
        {
            var leftBottomPoint = startPoint + plane.AxisX * -width / 2 + plane.AxisY * -height / 2;
            var points = new List<Point3D>();
            points.Add(leftBottomPoint);
            points.Add(leftBottomPoint + plane.AxisX * width);
            points.Add(leftBottomPoint + plane.AxisY * height);

            points.Add(leftBottomPoint + plane.AxisY * height);
            points.Add(leftBottomPoint + plane.AxisX * width);
            points.Add(leftBottomPoint + plane.AxisX * width + plane.AxisY * height);

            return points;
        }
    }
}
