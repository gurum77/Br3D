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

            if (dimDirection == DimDirection.horizontal)
            {
                axisX = Vector3D.AxisX;
                extPt1 = new Point3D(firstPoint.X, current.Y);
                extPt2 = new Point3D(secondPoint.X, current.Y);

                if (current.Y > firstPoint.Y && current.Y > secondPoint.Y)
                {
                    extPt1.Y += Define.DefaultTextHeight / 2;
                    extPt2.Y += Define.DefaultTextHeight / 2;
                }
                else
                {
                    extPt1.Y -= Define.DefaultTextHeight / 2;
                    extPt2.Y -= Define.DefaultTextHeight / 2;
                }

                Segment3D extLine1 = new Segment3D(firstPoint, extPt1);
                Segment3D extLine2 = new Segment3D(secondPoint, extPt2);
                pt1 = current.ProjectTo(extLine1);
                pt2 = current.ProjectTo(extLine2);
            }
            else if (dimDirection == DimDirection.vertical)
            {
                axisX = Vector3D.AxisY;

                extPt1 = new Point3D(current.X, firstPoint.Y);
                extPt2 = new Point3D(current.X, secondPoint.Y);

                if (current.X > firstPoint.X && current.X > secondPoint.X)
                {
                    extPt1.X += Define.DefaultTextHeight / 2;
                    extPt2.X += Define.DefaultTextHeight / 2;
                }
                else
                {
                    extPt1.X -= Define.DefaultTextHeight / 2;
                    extPt2.X -= Define.DefaultTextHeight / 2;
                }

                Segment3D extLine1 = new Segment3D(firstPoint, extPt1);
                Segment3D extLine2 = new Segment3D(secondPoint, extPt2);
                pt1 = current.ProjectTo(extLine1);
                pt2 = current.ProjectTo(extLine2);
            }
            else if (dimDirection == DimDirection.aligned)
            {
                if (secondPoint.X < firstPoint.X || secondPoint.Y < firstPoint.Y)
                {
                    Point3D p0 = firstPoint;
                    Point3D p1 = secondPoint;

                    Utility.Swap(ref p0, ref p1);

                    firstPoint = p0;
                    secondPoint = p1;
                }

                axisX = new Vector3D(firstPoint, secondPoint);
                Vector3D axisY = Vector3D.Cross(Vector3D.AxisZ, axisX);

                var drawingPlane = new Plane(firstPoint, axisX, axisY);

                Vector2D v1 = new Vector2D(firstPoint, secondPoint);
                Vector2D v2 = new Vector2D(firstPoint, current);

                double sign = System.Math.Sign(Vector2D.SignedAngleBetween(v1, v2));

                //offset p0-p1 at current
                Segment2D segment = new Segment2D(firstPoint, secondPoint);
                double offsetDist = current.DistanceTo(segment);
                extPt1 = firstPoint + sign * drawingPlane.AxisY * (offsetDist + Define.DefaultTextHeight / 2);
                extPt2 = secondPoint + sign * drawingPlane.AxisY * (offsetDist + Define.DefaultTextHeight / 2);
                pt1 = firstPoint + sign * drawingPlane.AxisY * offsetDist;
                pt2 = secondPoint + sign * drawingPlane.AxisY * offsetDist;
            }

        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                firstPoint = null;
                secondPoint = null;

                firstPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
                if (IsCanceled())
                    break;

                secondPoint = await GetPoint3D(LanguageHelper.Tr("Second point"));
                if (IsCanceled())
                    break;

                var textPoint = await GetPoint3D($"{firstPoint.DistanceTo(secondPoint):0.000}");
                if (IsCanceled())
                    break;


                GetDimInfo(textPoint, out Vector3D axisX, out Point3D extPt1, out Point3D extPt2, out Point3D pt1, out Point3D pt2);
                var axisY = Vector3D.Cross(Vector3D.AxisZ, axisX);
                var plane = new Plane(new Point3D(0, 0, 0), axisX, axisY);

                textPoint = ((firstPoint + secondPoint) / 2).IntersectionWith(axisY, textPoint, axisX);
                var dim = new LinearDim(plane, firstPoint, secondPoint, textPoint, Define.DefaultTextHeight);
                environment.Entities.Add(dim);
                environment.Entities.Regen();

                environment.TempEntities.Clear();
                environment.Invalidate();

            }


            EndAction();
            return true;
        }
    }
}
