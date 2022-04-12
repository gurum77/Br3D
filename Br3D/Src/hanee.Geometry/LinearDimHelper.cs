using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace hanee.Geometry
{
    static public class LinearDimHelper
    {
        static public LinearPath PreviewEntity(this LinearDim ld)
        {
            var axisX = ld.Plane.AxisX;
            var axisY = ld.Plane.AxisY;
            var pt1 = ld.ExtLine1.IntersectionWith(axisY, ld.DimLinePosition, axisX);
            var pt2 = ld.ExtLine2.IntersectionWith(axisY, ld.DimLinePosition, axisX);

            var lp = new LinearPath(ld.ExtLine1, pt1, ld.DimLinePosition, pt2, ld.ExtLine2);
            lp.CopyAttributes(ld);
                        
            return lp;
        }

        /// <summary>
        /// 지시선의 끝 좌표
        /// </summary>
        /// <param name="dim"></param>
        /// <param name="line1">true : 첫번째 지시선</param>
        /// <returns></returns>
        static public Point3D GetExtLineEnd(this LinearDim dim, bool line1)
        {
            Line dimLine = LineHelper.CreateInfinitLine(dim.DimLinePosition, dim.Plane.AxisX);
            Line extLine = LineHelper.CreateInfinitLine(line1 ? dim.ExtLine1 : dim.ExtLine2, dim.Plane.AxisY);
            Point3D[] points = dimLine.IntersectWith(extLine);
            return points?.Length > 0 ? points[0] : null;
        }
    }
}
