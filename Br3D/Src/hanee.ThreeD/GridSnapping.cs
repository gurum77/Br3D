using devDept.Geometry;
using System;
namespace hanee.ThreeD
{
    public class GridSnapping
    {
        public bool enabled { get; set; } = true;
        public int gridSnapDecimals { get; set; } = 1;  // grid snap 단위( 0 : 정수, 1 : 0.1, 2 : 0.01, 3 : 0.001 ...)

        /// <summary>
        /// Tries to snap grid vertex for the current mouse point
        /// </summary>
        public Point3D GetGridSnapPoint3D(Point3D pt)
        {
            if (!enabled)
                return pt;

            if (pt == null)
                return pt;

            var decimals = gridSnapDecimals;
            pt.X = Math.Round(pt.X, decimals);
            pt.Y = Math.Round(pt.Y, decimals);
            pt.Z = Math.Round(pt.Z, decimals);
            return pt;
        }
    }
}
