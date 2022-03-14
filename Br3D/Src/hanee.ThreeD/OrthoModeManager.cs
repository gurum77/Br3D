using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class OrthoModeManager
    {
        public System.Drawing.Color AxisXColor { get; set; } = System.Drawing.Color.Red;
        public System.Drawing.Color AxisYColor { get; set; } = System.Drawing.Color.Green;
        public System.Drawing.Color AxisZColor { get; set; } = System.Drawing.Color.Blue;
        Point3D ptXNear, ptXFar, ptYNear, ptYFar, ptZNear, ptZFar;

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
        public Axis axis{get; private set;}
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

                var line = LineHelper.CreateInfinitLine(lastOrthoPoint, lastOrthoDir);
                var pt1  = viewport.WorldToScreen(line.StartPoint);
                var pt2 = viewport.WorldToScreen(line.EndPoint);

                model.renderContext.SetLineSize(1);
                model.renderContext.EnableThickLines();
                if(axis == Axis.x)
                    model.renderContext.SetColorWireframe(AxisXColor);
                else if(axis == Axis.y)
                    model.renderContext.SetColorWireframe(AxisYColor);
                else if (axis == Axis.z)
                    model.renderContext.SetColorWireframe(AxisZColor);
                model.renderContext.DrawLines(new Point3D[] { pt1, pt2 });

                //ComputeNearFarIntersections(model.Viewports[i]);

                //// X axis 
                //DrawLinesBeforeNear(model.Viewports[i], ptXNear, ptXFar, AxisXColor);

                //// Y axis 
                //DrawLinesBeforeNear(model.Viewports[i], ptYNear, ptYFar, AxisYColor);

                //// Z axis 
                //DrawLinesBeforeNear(model.Viewports[i], ptZNear, ptZFar, AxisZColor);
            }

        }

        private void DrawLinesBeyondFar(Viewport viewport, Point3D nearPt, Point3D farPt, System.Drawing.Color color)
        {
            if (farPt == null || nearPt == null)
                return;

            Vector3D dir = Vector3D.Subtract(farPt, nearPt);
            dir.Normalize();

            if (viewport == null)
                viewport = model.ActiveViewport;

            Point3D pt1 = viewport.WorldToScreen(farPt);
            Point3D pt2 = viewport.WorldToScreen(farPt + dir);
            DrawLine(viewport, pt1, pt2, color, true);
        }

        private void DrawLinesBeforeNear(Viewport viewport, Point3D nearPt, Point3D farPt, System.Drawing.Color color)
        {
            if (farPt == null || nearPt == null)
                return;

            Vector3D dir = Vector3D.Subtract(farPt, nearPt);
            dir.Normalize();

            if(viewport == null)
                viewport = model.ActiveViewport;

            var pt1 = viewport.WorldToScreen(nearPt);
            var pt2 = viewport.WorldToScreen(nearPt - dir);
            DrawLine(viewport, pt1, pt2, color, false);
        }

        private void DrawLine(Viewport viewport, Point3D pt1, Point3D pt2, System.Drawing.Color color, bool convertToViewport)
        {
            if (pt1 == null || pt2 == null)
                return;

            Segment2D screenLine = new Segment2D(pt1, pt2);

            int[] viewFrame = viewport.GetViewFrame();

            double left = viewFrame[0];
            double right = viewFrame[0] + viewFrame[2];
            double bottom = viewFrame[1];
            double top = viewFrame[1] + viewFrame[3] - 1;
            Point2D lowerLeft = new Point2D(left, bottom);
            Point2D lowerRight = new Point2D(right, bottom);
            Point2D upperLeft = new Point2D(left, top);
            Point2D upperRight = new Point2D(right, top);


            Segment2D[] screenLines = new Segment2D[]
            {
            new Segment2D(lowerLeft, lowerRight),
            new Segment2D(upperLeft, upperRight),
            new Segment2D(lowerLeft, upperLeft),
            new Segment2D(lowerRight, upperRight)
            };

            Point2D ptAxis1 = null, ptAxis2 = null;

            Vector2D dir = Vector2D.Subtract(pt2, pt1);
            dir.Normalize();

            // extend the segment outside the window limits
            screenLine.P1 = screenLine.P0 + dir * (viewport.Size.Width + viewport.Size.Height);

            // Compute the intersection of the screen line against the lower and upper border of the viewport 
            Segment2D.Intersection(screenLine, screenLines[0], out ptAxis1);
            Segment2D.Intersection(screenLine, screenLines[1], out ptAxis2);

            if (ptAxis1 != null)

                screenLine.P1 = ptAxis1;

            if (ptAxis2 != null)

                screenLine.P1 = ptAxis2;

            bool clipAgainstVertical = true;

            // Compute the intersection of the infinite screen line against the left and right border of the viewport 
            Segment2D.Intersection(screenLine, screenLines[2], out ptAxis1);
            Segment2D.Intersection(screenLine, screenLines[3], out ptAxis2);

            if (ptAxis1 != null)

                screenLine.P1 = ptAxis1;

            if (ptAxis2 != null)

                screenLine.P1 = ptAxis2;

            model.renderContext.SetLineSize(1);
            model.renderContext.EnableThickLines();
            model.renderContext.SetColorWireframe(color);

            var tol = 1e-6;

            // Consider some tolerance
            if (screenLine.P0.X >= left - tol && screenLine.P0.X <= right + tol &&
                screenLine.P0.Y >= bottom - tol && screenLine.P0.Y <= top + tol &&
                screenLine.P1.X >= left - tol && screenLine.P1.X <= right + tol &&
                screenLine.P1.Y >= bottom - tol && screenLine.P1.Y <= top + tol)
            {
                if (convertToViewport)
                {
                    // When drawing the lines beyond far inside the DrawViewportBackground, the camera is set to just the Viewport, not to the whole ViewportLayout,
                    // so if we have multiple viewports we must adjust the screen coordinates
                    screenLine.P0 = new Point2D(screenLine.P0.X - left, screenLine.P0.Y - bottom);
                    screenLine.P1 = new Point2D(screenLine.P1.X - left, screenLine.P1.Y - bottom);
                }

                model.renderContext.DrawLines(new float[]
                {
                (float) screenLine.P0.X, (float) screenLine.P0.Y, 0,
                (float) screenLine.P1.X, (float) screenLine.P1.Y, 0
                });
            }
        }

        private void ComputeNearFarIntersections(Viewport viewport)
        {
            Plane farPlane = viewport.Camera.FarPlane;
            Plane nearPlane = viewport.Camera.NearPlane;

            Segment3D sX = new Segment3D(0, 0, 0, 1, 0, 0);
            Segment3D sY = new Segment3D(0, 0, 0, 0, 1, 0);
            Segment3D sZ = new Segment3D(0, 0, 0, 0, 0, 1);
            // Compute the intersections with the camera planes
            if (!Vector3D.AreParallel(viewport.Camera.ViewNormal, Vector3D.AxisX))
            {
                sX.IntersectWith(nearPlane, true, out ptXNear);
                sX.IntersectWith(farPlane, true, out ptXFar);
                if (ptXNear == null)
                {
                    ptXNear = new Point3D(-1e10, 0, 0);
                    ptXFar = new Point3D(1e10, 0, 0);

                }
            }
            else
            {
                ptXNear = null;
                ptXFar = null;
            }

            if (!Vector3D.AreParallel(viewport.Camera.ViewNormal, Vector3D.AxisY))
            {
                sY.IntersectWith(nearPlane, true, out ptYNear);
                sY.IntersectWith(farPlane, true, out ptYFar);
                if (ptYNear == null)
                {
                    ptYNear = new Point3D(0, -1e10, 0);
                    ptYFar = new Point3D(0, 1e10, 0);
                }
            }
            else
            {
                ptYNear = null;
                ptYFar = null;
            }
            if (!Vector3D.AreParallel(viewport.Camera.ViewNormal, Vector3D.AxisZ))
            {
                sZ.IntersectWith(nearPlane, true, out ptZNear);
                sZ.IntersectWith(farPlane, true, out ptZFar);
                if (ptZNear == null)
                {
                    ptZNear = new Point3D(0, 0, -1e10);
                    ptZFar = new Point3D(0, 0, 1e10);
                }
            }


            else
            {
                ptZNear = null;
                ptZFar = null;
            }

        }
    }
}
