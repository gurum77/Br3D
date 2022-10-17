using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hanee.Geometry
{
    public class Util
    {
        // 해당 측점이 지정된 간격에 대한 tick위치인지?
        static public bool IsTick(double sta, double offset)
        {
            int count = (int)Math.Round(sta / offset, 0);

            return (offset * count).Equals(sta, 0.001);
        }

        // 범위내 모든 측점간격을 가져온다.
        // 시점 / 종점을 포함한 측점간격을 리턴
        static public List<double> GetAllChainaInRange(double startSta, double startEnd, double offset, bool includeStartEnd = true)
        {
            if (offset <= 0.1)
                return null;

            List<double> chainages = new List<double>();
            // 시점 
            if (includeStartEnd)
                chainages.Add(startSta);

            // 첫번째 정체인
            double firstChain = GetFirstChain(startSta, offset);

            while (firstChain < startEnd)
            {
                chainages.Add(firstChain);
                firstChain += offset;
            }


            // 종점
            if (includeStartEnd)
                chainages.Add(startEnd);


            return chainages;
        }

        // 첫번째 정체인의 측점을 리턴
        // 시작측점 제외
        static public double GetFirstChain(double startSta, double offset)
        {
            double staGap = (offset * ((int)(startSta / offset) + 1)) - startSta;

            if (startSta < 0 && staGap > offset)
                staGap -= offset;

            // 시작측점을 무조건 제외
            if (staGap == 0)
                staGap = offset;

            return startSta + staGap;
        }

        static public bool IsCCW2D(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double temp = x1 * y2 + x2 * y3 + x3 * y1;
            temp = temp - y1 * x2 - y2 * x3 - y3 * x1;
            // 반시계
            if (temp > 0)
            {
                return true;
            }
            // 시계
            else if (temp < 0)
            {
                return false;
            }
            // 일직선
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 여러 point의 중심 좌표 리턴
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        static public Point3D GetCenter(List<Point3D> points)
        {
            Point3D center = new Point3D();
            foreach (var p in points)
            {
                center += p;
            }

            center = center / points.Count;

            return center;
        }
        // Point2D까지 모드 clone해서 리턴
        static public Point2D[] CloneArray(Point2D[] points)
        {
            Point2D[] newPoints = new Point2D[points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                newPoints[i] = points[i].Clone() as Point2D;
            }
            return newPoints;
        }

        // 연속된 중복좌표를 제거해서 리턴
        static public List<double> GetUnduplicatedDoubles(List<double> values, double tol = 0.00001)
        {
            if (values == null)
                return null;

            for (int i = 0; i < values.Count; ++i)
            {
                var pt1 = i > 0 ? values[i - 1] : values.Last();
                var pt2 = values[i];
                if (pt1.Equals(pt2, tol))
                    values.RemoveAt(i--);

                if (values.Count == 1)
                    break;
            }

            return values;
        }

        /// <summary>
        /// 연속된 중복 좌표를 제거한다.(시점과 종점도 비교..)
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        static public List<Point3D> GetUnduplicatedPoints(List<Point3D> points, double tol = 0.0001)
        {
            if (points == null)
                return null;

            for (int i = 0; i < points.Count; ++i)
            {
                Point3D pt1 = i > 0 ? points[i - 1] : points.Last();
                Point3D pt2 = points[i];
                if (pt1.Equals(pt2, tol))
                    points.RemoveAt(i--);

                if (points.Count == 1)
                    break;
            }

            return points;
        }



    }
}
