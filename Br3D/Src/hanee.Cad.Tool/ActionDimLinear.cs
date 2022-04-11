using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
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

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            var current = point3D;
            if (firstPoint == null || secondPoint == null || current == null)
                return;

            GetHModel().TempEntities.Clear();
            var entities = new List<Entity>();
            if (dimDirection == DimDirection.horizontal)
            {
                var axisX = Vector3D.AxisX;

                
                var extPt1 = new Point3D(firstPoint.X, current.Y);
                var extPt2 = new Point3D(secondPoint.X, current.Y);

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
                Point3D pt1 = current.ProjectTo(extLine1);
                Point3D pt2 = current.ProjectTo(extLine2);

                entities.Add(new Line(firstPoint, extPt1));
                entities.Add(new Line(secondPoint, extPt2));
                entities.Add(new Line(pt1, pt2));
                PreviewEntities = entities.ToArray();
            }
            else if (dimDirection == DimDirection.vertical)
            {



            }
            else if (dimDirection == DimDirection.aligned)
            {

            }
            base.OnMouseMove(environment, e);

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

                var textPoint = await GetPoint3D(LanguageHelper.Tr("Text point"));
                if (IsCanceled())
                    break;


                if (dimDirection == DimDirection.horizontal)
                {
                    //var dim = new LinearDim()
                }
            }


            EndAction();
            return true;
        }
    }
}
