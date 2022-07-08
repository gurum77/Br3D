using devDept.Geometry;
using System;

namespace hanee.Geometry
{
    static public class PlaneHelper
    {
        // plane을 수직수평이 되도록 만든다.
        static public void Normalize(this Plane plane)
        {
            NormalizeAxis(plane.AxisX);
            NormalizeAxis(plane.AxisY);
            NormalizeAxis(plane.AxisZ);
        }

        static public void NormalizeAxis(Vector3D axis)
        {
            if (Math.Abs(axis.X) == 0)
            {
                if (Math.Abs(axis.Y) > Math.Abs(axis.Z))
                {
                    axis.Y = axis.Y > 0 ? 1 : -1;
                    axis.Z = 0;
                }
                else if (Math.Abs(axis.Y) < Math.Abs(axis.Z))
                {
                    axis.Y = 0;
                    axis.Z = axis.Z > 0 ? 1 : -1;
                }
            }
            else if (Math.Abs(axis.Y) == 0)
            {
                if (Math.Abs(axis.X) > Math.Abs(axis.Z))
                {
                    axis.X = axis.X > 0 ? 1 : -1;
                    axis.Z = 0;
                }
                else if (Math.Abs(axis.X) < Math.Abs(axis.Z))
                {
                    axis.X = 0;
                    axis.Z = axis.Z > 0 ? 1 : -1;
                }
            }
            else if (Math.Abs(axis.Z) == 0)
            {
                if (Math.Abs(axis.X) > Math.Abs(axis.Y))
                {
                    axis.X = axis.X > 0 ? 1 : -1;
                    axis.Y = 0;
                }
                else if (Math.Abs(axis.X) < Math.Abs(axis.Y))
                {
                    axis.X = 0;
                    axis.Y = axis.Y > 0 ? 1 : -1;
                }
            }
        }
    }
}
