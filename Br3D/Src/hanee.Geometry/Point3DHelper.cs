using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;

namespace hanee.Geometry
{
    static public class Point3DHelper
    {
        public static void CopyFrom(this Point3D pt, Point3D fromPt)
        {
            pt.X = fromPt.X;
            pt.Y = fromPt.Y;
            pt.Z = fromPt.Z;
        }

        public static bool Equals2D(this Point3D pt, Point3D ptOther, double tol)
        {
            double dist = pt.To2D().DistanceTo(ptOther.To2D());
            if (dist <= tol)
                return true;
            return false;
        }

        /// <summary>
        /// 좌표가 같은지 오차를 줘서 비교
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="ptOther"></param>
        /// <param name="tol"></param>
        /// <returns></returns>
        public static bool Equals(this Point3D pt, Point3D ptOther, double tol)
        {
            double dist = pt.DistanceTo(ptOther);
            if (dist <= tol)
                return true;

            return false;
        }

        /// <summary>
        /// Point3D에 대한 scale 적용(실제로는 위치만 이동됨)
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="sz"></param>
        public static void Scale(this Point3D pt, Point3D basePoint, double sx, double sy, double sz)
        {
            pt.X = basePoint.X + ((pt.X - basePoint.X) * sx);
            pt.Y = basePoint.Y + ((pt.Y - basePoint.Y) * sy);
            pt.Z = basePoint.Z + ((pt.Z - basePoint.Z) * sz);
        }


        public static Point2D GetSwap(this Point2D pt)
        {
            return new Point2D(pt.Y, pt.X);
        }

        public static Vector2D ToDir(this Point2D pt)
        {
            var dir = pt.AsVector;
            dir.Normalize();
            return dir;
        }


        public static Vector3D ToDir(this Point3D pt)
        {
            var dir = pt.AsVector;
            dir.Normalize();
            return dir;
        }

        /// <summary>
        /// 2D좌표를 3D좌표로 리턴 
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static Point3D To3D(this Point2D pt)
        {
            return new Point3D(pt.X, pt.Y);
        }


        /// <summary>
        /// 3D좌표를 2D좌표로 리턴 
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static Point2D To2D(this Point3D pt)
        {
            return new Point2D(pt.X, pt.Y);
        }

        // pt가 closed된 curve 안쪽인지?
        public static bool IsInsideCurve(this Point3D pt, ICurve curve)
        {
            if (!curve.IsClosed)
                return false;

            Region r = new Region(curve);
            var res = r.IsPointInside(pt);
            return res;
        }

        public static bool IsInsideRegions(this Point3D pt, List<Region> regions)
        {
            if (regions == null)
                return false;

            foreach (var r in regions)
            {
                if (r.IsPointInside(pt))
                    return true;
            }

            return false;
        }


    }
}
