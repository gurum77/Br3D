using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace hanee.Geometry
{
    public class LineHelper
    {

        // 너무 길면 계산이 안되는 경우가 있음.
        const double infiniteLength = 100000;

        // 선의 최대 길이
        static public double GetInfiniteLength()
        {
            return infiniteLength;
        }
        /// <summary>
        /// 한쪽 방향으로 거의 무한대인 수직선을 만든다.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="toUpper"></param>
        /// <returns></returns>
        static public Line CreateOneDirVerticalInfinitLine(double x, double y, bool toUpper)
        {
            if (toUpper)
                return new Line(x, y, x, infiniteLength);
            else
                return new Line(x, -infiniteLength, x, y);
        }

        /// <summary>
        /// 한쪽 방향으로 거의 무한대인 수평선을 만든다.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="toUpper"></param>
        /// <returns></returns>
        static public Line CreateOneDirHorizontalInfinitLine(double x, double y, bool toLeft)
        {
            if (toLeft)
                return new Line(x, y, x - infiniteLength, y);
            else
                return new Line(x, y, x + infiniteLength, y);
        }

        static public Line CreateOneDirInfiniteLine(Point3D pt, Vector3D dir)
        {
            return new Line(pt, pt + dir * infiniteLength);
        }

        /// <summary>
        /// 거의 무한대의 선을 만든다.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public Line CreateInfinitLine(Point2D ptBase, Vector2D dir)
        {
            Point2D pt1 = ptBase + dir * -infiniteLength;
            Point2D pt2 = ptBase + dir * infiniteLength;
            Line line = new Line(pt1.X, pt1.Y, pt2.X, pt2.Y);
            return line;
        }

        /// <summary>
        /// 거의 무한대의 선을 만든다.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public Line CreateInfinitLine(Point3D ptBase, Vector3D dir)
        {
            Point3D pt1 = ptBase + dir * -infiniteLength;
            Point3D pt2 = ptBase + dir * infiniteLength;
            Line line = new Line(pt1.X, pt1.Y, pt1.Z, pt2.X, pt2.Y, pt2.Z);
            return line;
        }

        /// <summary>
        /// 거의 무한대의 선을 만든다.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public Line CreateInfinitLine(Plane plane, Point3D ptBase, Vector3D dir)
        {
            Point3D pt1 = ptBase + dir * -infiniteLength;
            Point3D pt2 = ptBase + dir * infiniteLength;

            var pt12D = plane.Project(pt1);
            var pt22D = plane.Project(pt2);
            pt1 = plane.PointAt(pt12D);
            pt2 = plane.PointAt(pt22D);

            Line line = new Line(pt1.X, pt1.Y, pt1.Z, pt2.X, pt2.Y, pt2.Z);
            
            return line;
        }

        /// <summary>
        /// 거의 무한대의 수평선을 만든다.
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        static public Line CreateHorizontalInfinitLine(double y)
        {
            return new Line(-infiniteLength, y, infiniteLength, y);
        }

        /// <summary>
        /// 거의 무한대의 수직선을 만든다.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public Line CreateVerticalInfinitLine(double x)
        {
            return new Line(x, -infiniteLength, x, infiniteLength);
        }


        // lineTar에 z방향으로 교차되는 점을 찾는다.
        public static Point3D FindMatchPointOnLineByOtherLineByZAxis(Line lineTar, Line lineSrc)
        {

            Line lineTar2D = lineTar.Clone() as Line;
            lineTar2D.StartPoint.Z = 0;
            lineTar2D.EndPoint.Z = 0;

            Line lineSrc2D = lineSrc.Clone() as Line;
            lineSrc2D.StartPoint.Z = 0;
            lineSrc2D.EndPoint.Z = 0;

            // 끝점이 line상에 있는지 본다.
            if (lineTar2D.StartPoint.IsOnCurve(lineSrc2D, 0.001))
            {
                return lineTar.StartPoint.Clone() as Point3D;
            }
            if (lineTar2D.EndPoint.IsOnCurve(lineSrc2D, 0.001))
            {
                return lineTar.EndPoint.Clone() as Point3D;
            }

            // 2로 교점을 찾는다. 
            var points = lineTar2D.IntersectWith(lineSrc2D);
            if (points == null || points.Length == 0)
                return null;

            // z값을 가진 점으로 리턴 
            var matchPoint = points[0];
            double param = 0;
            lineTar2D.ClosestPointTo(matchPoint, out param);

            matchPoint.Z = lineTar.StartPoint.Z + (lineTar.EndPoint.Z - lineTar.StartPoint.Z) * (param / lineTar2D.Length());

            return matchPoint;
        }
    }
}
