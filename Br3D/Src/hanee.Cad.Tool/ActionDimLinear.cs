using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionDimLinear : ActionBase
    {
        public enum DimDirection
        {
            horizontal,
            vertical,
            aligned
        }

        Point3D firstPoint;
        Point3D secondPoint;

        public DimDirection dimDirection { get; set; } = DimDirection.horizontal;
        public ActionDimLinear(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            var current = point3D;
            if (firstPoint == null || secondPoint == null || current == null)
                return;

            GetDimInfo(current, out Vector3D axisX, out Point3D extPt1, out Point3D extPt2, out Point3D pt1, out Point3D pt2);

            var entities = new List<Entity>();
            entities.Add(new Line(firstPoint, extPt1));
            entities.Add(new Line(secondPoint, extPt2));
            entities.Add(new Line(pt1, pt2));
            PreviewEntities = entities.ToArray();

            base.OnMouseMove(environment, e);

        }

        private void GetDimInfo(Point3D current, out Vector3D axisX, out Point3D extPt1, out Point3D extPt2, out Point3D pt1, out Point3D pt2)
        {
            axisX = null;
            extPt1 = null;
            extPt2 = null;
            pt1 = null;
            pt2 = null;

            var ws = GetWorkspace();
            if (ws == null)
                return;

            axisX = ws.plane.AxisX.Clone() as Vector3D;
            var current2D = ws.plane.Project(current);
            var firstPoint2D = ws.plane.Project(firstPoint);
            var secondPoint2D = ws.plane.Project(secondPoint);


            if (dimDirection == DimDirection.horizontal || dimDirection == DimDirection.aligned)
            {
            
                var extPt12D = new Point2D(firstPoint2D.X, current2D.Y);
                var extPt22D = new Point2D(secondPoint2D.X, current2D.Y);
                if (current2D.Y > firstPoint2D.Y && current2D.Y > firstPoint2D.Y)
                {
                    extPt12D.Y += Define.DefaultTextHeight / 2;
                    extPt22D.Y += Define.DefaultTextHeight / 2;
                }
                else
                {
                    extPt12D.Y -= Define.DefaultTextHeight / 2;
                    extPt22D.Y -= Define.DefaultTextHeight / 2;
                }

                Segment2D extLine12D = new Segment2D(firstPoint2D, extPt12D);
                Segment2D extLine22D = new Segment2D(secondPoint2D, extPt22D);
                var pt12D = current2D.ProjectTo(extLine12D);
                var pt22D = current2D.ProjectTo(extLine22D);

                pt1 = ws.PointAt(pt12D);
                pt2 = ws.PointAt(pt22D);
                extPt1 = ws.PointAt(extPt12D);
                extPt2 = ws.PointAt(extPt22D);
            }
            else if (dimDirection == DimDirection.vertical)
            {

                var extPt12D = new Point2D(current2D.X, firstPoint2D.Y);
                var extPt22D = new Point2D(current2D.X, secondPoint2D.Y);

                if (current2D.X > firstPoint2D.X && current2D.X > secondPoint2D.X)
                {
                    extPt12D.X += Define.DefaultTextHeight / 2;
                    extPt22D.X += Define.DefaultTextHeight / 2;
                }
                else
                {
                    extPt12D.X -= Define.DefaultTextHeight / 2;
                    extPt22D.X -= Define.DefaultTextHeight / 2;
                }

                Segment2D extLine12D = new Segment2D(firstPoint2D, extPt12D);
                Segment2D extLine22D = new Segment2D(secondPoint2D, extPt22D);
                var pt12D = current.ProjectTo(extLine12D);
                var pt22D = current.ProjectTo(extLine22D);

                pt1 = ws.PointAt(pt12D);
                pt2 = ws.PointAt(pt22D);
                extPt1 = ws.PointAt(extPt12D);
                extPt2 = ws.PointAt(extPt22D);
            }
           

        }

        public async Task<bool> RunAsync()
        {
            var ws = GetWorkspace();

            StartAction();

            while (true)
            {
                firstPoint = null;
                secondPoint = null;

                firstPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
                if (IsCanceled())
                    break;

                if (ws != null)
                {
                    ws.plane.Origin.Z = firstPoint.Z;
                    ws.enabled = true;
                }


                secondPoint = await GetPoint3D(LanguageHelper.Tr("Second point"));
                if (IsCanceled())
                    break;

                if (dimDirection == DimDirection.aligned)
                {
                    var newAxisX = (secondPoint - firstPoint).AsVector;
                    newAxisX.Normalize();

                    var newAxisY = Vector3D.Cross(ws.plane.AxisZ, newAxisX);
                    ws.plane = new Plane(new Point3D(0, 0, firstPoint.Z), newAxisX, newAxisY);
                }

                var textPoint = await GetPoint3D($"{firstPoint.DistanceTo(secondPoint):0.000}");
                if (IsCanceled())
                    break;

                environment.TempEntities.Clear();

                GetDimInfo(textPoint, out Vector3D axisX, out Point3D extPt1, out Point3D extPt2, out Point3D pt1, out Point3D pt2);
                var plane = ws.plane;

                textPoint = ((firstPoint + secondPoint) / 2).IntersectionWith(ws.plane.AxisY, textPoint, axisX);
                var dim = new LinearDim(plane, firstPoint, secondPoint, textPoint, Define.DefaultTextHeight);
                if (dim != null)
                {
                    AddEntities(dim);
                }
                break;
            }


            EndAction();

            if (ws != null)
                ws.enabled = false;
            return true;
        }
    }
}
