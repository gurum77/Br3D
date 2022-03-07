using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace hanee.Geometry
{
    static public class LinearPathHelper
    {
        [Flags]
        public enum Border
        {
            none = 0x00000000,
            left = 0x00000001,
            top = 0x00000002,
            right = 0x00000004,
            bottom = 0x00000008,
            all = 0x0000ffff
        }
        // 수평선을 만든다.
        static public LinearPath CreateHorizontalLine(Point3D center, double width)
        {
            List<Point3D> points = new List<Point3D>();
            points.Add(new Point3D(center.X - width / 2, center.Y, center.Z));
            points.Add(new Point3D(center.X + width / 2, center.Y, center.Z));
            return new LinearPath(points);
        }

        static public List<Point3D> GetRectanlePoints(double x, double y, double z, double w, double h, bool centered = true)
        {
            double hw = w / 2;
            double hh = h / 2;
            List<Point3D> points = new List<Point3D>();
            if (centered)
            {
                points.Add(new Point3D(x - hw, y - hh, z));
                points.Add(new Point3D(x + hw, y - hh, z));
                points.Add(new Point3D(x + hw, y + hh, z));
                points.Add(new Point3D(x - hw, y + hh, z));
                points.Add(new Point3D(x - hw, y - hh, z));
            }
            else
            {
                points.Add(new Point3D(x, y, z));
                points.Add(new Point3D(x + w, y, z));
                points.Add(new Point3D(x + w, y + h, z));
                points.Add(new Point3D(x, y + h, z));
                points.Add(new Point3D(x, y, z));
            }

            return points;
        }
        static public EntityList CreateBorder(double x, double y, double z, double w, double h, bool centered = true, Border border = Border.all)
        {
            List<Point3D> points = GetRectanlePoints(x, y, z, w, h, centered);
            if (points == null)
                return null;

            EntityList entities = new EntityList();

            if (border.HasFlag(Border.left))
            {
                entities.Add(new Line(points[3], points[0]));
            }

            if (border.HasFlag(Border.top))
            {
                entities.Add(new Line(points[0], points[1]));
            }

            if (border.HasFlag(Border.right))
            {
                entities.Add(new Line(points[1], points[2]));
            }

            if (border.HasFlag(Border.bottom))
            {
                entities.Add(new Line(points[2], points[3]));
            }

            foreach (var ent in entities)
            {
                ent.Color = Color.White;
                ent.ColorMethod = colorMethodType.byEntity;
            }

            return entities;
        }

        // 사각형을 만든다.
        static public LinearPath CreateRectangle(double x, double y, double z, double w, double h, bool centered = true)
        {
            List<Point3D> points = GetRectanlePoints(x, y, z, w, h, centered);
            if (points == null)
                return null;


            return new LinearPath(points)
            {
                Color = Color.White,
                ColorMethod = colorMethodType.byEntity
            };
        }
        /// <summary>
        /// lines으로 linearPath를 만든다. 
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        static public LinearPath FromLines(List<Line> lines)
        {
            // 길이가 거의 0인 선은 제외
            for (int i = 0; i < lines.Count(); ++i)
            {
                if (lines[i].Length() < 0.001)
                {
                    lines.RemoveAt(i);
                    i--;
                }
            }
            // 서로연결되도록 정렬
            List<Line> sortedLines = new List<Line>();
            while (lines.Count > 0)
            {
                bool findConnectableLine = false;
                for (int i = 0; i < lines.Count; ++i)
                {
                    Line line = MakeConnectableLine(sortedLines, lines[i], true);
                    if (line != null)
                    {
                        sortedLines.Add(line);
                        lines.RemoveAt(i);
                        i--;
                        findConnectableLine = true;
                        continue;
                    }

                    line = MakeConnectableLine(sortedLines, lines[i], false);
                    if (line != null)
                    {
                        sortedLines.Insert(0, line);
                        lines.RemoveAt(i);
                        i--;
                        findConnectableLine = true;
                        continue;
                    }

                }

                // 연결할 수 있는걸 못 찾으면 그만 찾자
                if (findConnectableLine == false)
                    break;
            }

            if (sortedLines.Count == 0)
                return null;

            List<Point3D> points = new List<Point3D>();
            foreach (var c in sortedLines)
            {
                // 연속된 같은 점은 추가하지 않는다.
                if (points.Count == 0 || !points.Last().Equals(c.StartPoint, 0.0001))
                    points.Add(c.StartPoint);
            }
            // 마지막 좌표도 추가
            points.Add(sortedLines.Last().EndPoint);

            if (points.Count < 2)
                return null;



            return new LinearPath(points);
        }

        /// <summary>
        /// sortedCurves에 연결할 수 있는 curve로 만들어서 리턴
        /// 못 만들면 null을 리턴
        /// </summary>
        /// <param name="sortedLines"></param>
        /// <param name="line"></param>
        /// <param name="atLast"> true : 마지막에 연결할 수 있는걸 찾는다. false : 처음에 연결할 수 있는걸 찾는다. </param>
        /// <returns></returns>
        private static Line MakeConnectableLine(List<Line> sortedLines, Line line, bool atLast)
        {
            if (line == null)
                return null;

            if (sortedLines == null || sortedLines.Count == 0)
                return line;

            foreach (var sc in sortedLines)
            {
                // 맨 뒤에 연결할 수 있는걸 찾는다.
                if (atLast)
                {
                    // 정상연결
                    if (sc.EndPoint.Equals(line.StartPoint, 0.001))
                    {
                        return line.Clone() as Line;
                    }
                    // 뒤집어서 연결가능
                    else if (sc.EndPoint.Equals(line.EndPoint, 0.001))
                    {
                        Line lineNew = line.Clone() as Line;
                        lineNew.Reverse();
                        return lineNew;
                    }
                }
                // 맨 앞에 연결할 수 있는걸 찾는다.
                else
                {
                    // 정상연결
                    if (sc.StartPoint.Equals(line.EndPoint, 0.001))
                    {
                        return line.Clone() as Line;
                    }
                    // 뒤집어서 연결가능
                    else if (sc.StartPoint.Equals(line.StartPoint, 0.001))
                    {
                        Line lineNew = line.Clone() as Line;
                        lineNew.Reverse();
                        return lineNew;
                    }
                }

            }

            return null;
        }

        public static Point3D GetMidPoint(this LinearPath lp)
        {
            return lp.PointAt(lp.Length() / 2.0);
        }

        // linear path를 닫는다.
        public static void Close(LinearPath lp)
        {
            if (lp.IsClosed)
                return;

            if (lp.Vertices.Length < 3)
                return;

            var vertices = lp.Vertices.ToList();
            vertices.Add(vertices.First().Clone() as Point3D);
            lp.Vertices = vertices.ToArray();
        }
    }
}
