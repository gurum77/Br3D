using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Milling;
using devDept.Geometry;
using System.Collections.Generic;

namespace hanee.Cam
{
    static public class ToolpathHelper
    {
        // toolpath로 stock을 만든다.
        static public SimulationStock CreateSimulationStock(this Toolpath path)
        {
            path.Regen(0.01);

            Point3D boxMin, boxMax;
            GetFeedBoxSize(path, out boxMin, out boxMax);

            Point3D center = Point3D.MidPoint(boxMin, boxMax);

            double sizeX = (boxMax.X - boxMin.X) * 1.1;

            double sizeY = (boxMax.Y - boxMin.Y) * 1.1;

            double sizeZ = 0.1 * path.BoxSize.Z;

            double x = center.X - sizeX / 2;

            double y = center.Y - sizeY / 2;

            AdjustBoxSize(ref sizeX, ref sizeY, ref sizeZ);

            return Stock.CreateBox(x, y, sizeX, sizeY, new Interval(boxMin.Z - sizeZ, boxMax.Z)).GetSimulationStock();
        }

        private static double boxHeight = 1;
        private static void AdjustBoxSize(ref double sizeX, ref double sizeY, ref double sizeZ)
        {
            if (sizeX == 0)
                sizeX = boxHeight;
            if (sizeY == 0)
                sizeY = boxHeight;
            if (sizeZ == 0)
                sizeZ = boxHeight;
        }

        public static void GetFeedBoxSize(Toolpath path, out Point3D boxMin, out Point3D boxMax)
        {
            List<Point3D> feedPoints = new List<Point3D>();
            foreach (Toolpath.Motion motion in path.MotionList)
            {
                if (motion is Toolpath.CircularMotion)
                {
                    Toolpath.CircularMotion cm = (Toolpath.CircularMotion)motion;
                    feedPoints.Add(cm.Plane.Origin + cm.Radius * cm.Plane.AxisX + cm.Radius * cm.Plane.AxisY);
                    feedPoints.Add(cm.Plane.Origin - cm.Radius * cm.Plane.AxisX - cm.Radius * cm.Plane.AxisY);
                }
                else if (motion is Toolpath.LinearMotion)
                {
                    Toolpath.LinearMotion lm = (Toolpath.LinearMotion)motion;
                    if (lm.Code != motionType.G00)
                    {
                        feedPoints.Add(lm.From);
                        feedPoints.Add(lm.To);
                    }
                }
            }
            UtilityEx.ComputeBoundingBox(feedPoints, out boxMin, out boxMax);
        }
    }
}
