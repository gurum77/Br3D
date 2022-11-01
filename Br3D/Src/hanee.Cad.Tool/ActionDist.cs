using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionDist : ActionBase
    {
        enum Step
        {
            firstPoint,
            secondPoint
        }

        public enum ShowResult
        {
            label,
            form
        }
        readonly HModel brModel;
        Point3D pt1, pt2, ptText;
        List<Point3D> points;
        Step step;
        ShowResult showResult;



        public ActionDist(devDept.Eyeshot.Model vp, ShowResult showResult = ShowResult.form) : base(vp)
        {
            brModel = vp as HModel;
            this.showResult = showResult;
            points = new List<Point3D>();
        }


        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment vp, MouseEventArgs e)
        {
            if (step != Step.secondPoint || point3D == null)
            {
                ActionBase.previewEntity = null;
                return;
            }

            pt2 = point3D;
            if (pt1 == null)
                return;

            ptText = (pt1 + pt2) / 2;

            if (showResult == ShowResult.form)
                ActionBase.previewEntity = CreateLinearPath();
            else
                ActionBase.previewEntity = CreateLine();
        }

        static public DistanceText CreateDistanceText(Environment env, Point3D pt1, Point3D pt2)
        {
            if (env is HModel)
            {
                DistanceText dt = new DistanceText((Model)env, pt1, pt2, pt1.DistanceTo(pt2).ToString("0.000"), Define.DefaultFont, Define.DefaultTextColor, new Vector2D(0, 0))
                {
                    Alignment = System.Drawing.ContentAlignment.MiddleCenter,
                    FillColor = System.Drawing.Color.Yellow
                };

                return dt;
            }
            else
            {
                return null;
            }
        }

        DistanceText CreateDistanceText()
        {
            return CreateDistanceText(environment, pt1, pt2);
        }

        LinearPath CreateLinearPath()
        {
            if (points == null || points.Count < 1)
                return null;

            List<Point3D> tmp = new List<Point3D>();
            tmp.AddRange(points);
            tmp.Add(pt2);
            return new LinearPath(tmp);
        }
        Line CreateLine()
        {
            if (pt1 == null || pt2 == null || ptText == null)
                return null;

            Line line = new Line(pt1, pt2)
            {
                Color = System.Drawing.Color.Red,
                LineWeight = 2.0f,
                LineWeightMethod = colorMethodType.byEntity
            };

            return line;
        }
        LinearDim CreateLinearDim()
        {
            if (pt1 == null || pt2 == null || ptText == null)
                return null;

            if (pt2.X < pt1.X || pt2.Y < pt1.Y)
            {
                Point3D p0 = pt1;
                Point3D p1 = pt2;

                Utility.Swap(ref p0, ref p1);

                pt1 = p0;
                pt2 = p1;
            }

            Plane plane = null;
            Vector3D axisX = new Vector3D(pt1, pt2);
            axisX.Normalize();

            // x축이 수직이면 y축은 임의로1,0,0으로 한다.
            Vector3D axisY = Vector3D.AxisX;
            if (!axisX.Equals(Vector3D.AxisZ) && !axisX.Equals(Vector3D.AxisZ * -1))
                axisY = Vector3D.Cross(Vector3D.AxisZ, axisX);

            plane = new Plane(pt1, axisX, axisY);

            LinearDim alignedDim = new LinearDim(plane, pt1, pt2, ptText, DimensionOptionsHelper.DimTextHeight)
            {
                Billboard = false,
                Color = System.Drawing.Color.Black,
                LayerName = "0"
            };

            return alignedDim;
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            var model = environment as Model;
            if (model != null)
            {
                if (showResult == ShowResult.form)
                {

                    while (true)
                    {
                        if (points.Count == 0)
                        {
                            step = Step.firstPoint;
                            pt1 = await GetPoint3D(LanguageHelper.Tr("Pick first point"));
                            if (IsCanceled()) { break; }
                            if (IsEntered()) { break; }

                            points.Add(pt1);
                        }
                        else
                        {
                            step = Step.secondPoint;
                            pt2 = await GetPoint3D(LanguageHelper.Tr("Next point or Enter"));
                            if (IsCanceled()) { break; }
                            if (IsEntered()) { break; }

                            points.Add(pt2);
                            pt1 = pt2;
                        }

                    }

                    // show result
                    List<string> results = new List<string>();
                    double dist = GetTotalDist(points);
                    results.Add($"Distance = {dist:0.0000}");
                    for (int i = 1; i < points.Count; ++i)
                    {
                        var pt1 = points[i - 1];
                        var pt2 = points[i];
                        results.Add($"  △X{i} = {pt2.X - pt1.X:0.0000}, △Y{i} = {pt2.Y - pt1.Y:0.0000}, △Z{i} = {pt2.Z - pt1.Z:0.0000}");
                    }

                    FormResult formResult = new FormResult();
                    formResult.RichTextBox.Lines = results.ToArray();
                    formResult.ShowDialog();

                }
                else
                {
                    while (true)
                    {
                        step = Step.firstPoint;
                        pt1 = await GetPoint3D(LanguageHelper.Tr("Specify first point"));
                        if (IsCanceled()) { break; }

                        step = Step.secondPoint;
                        pt2 = await GetPoint3D(LanguageHelper.Tr("Specify second point"));
                        if (IsCanceled()) { break; }

                        // 가운데에다 바로 기입한다.
                        ptText = (pt1 + pt2) / 2;

                        DistanceText distanceText = CreateDistanceText();
                        if (distanceText != null)
                        {
                            model.ActiveViewport.Labels.Add(distanceText);
                            model.Invalidate();
                        }

                        pt1 = null;
                        pt2 = null;
                        ptText = null;
                        ActionBase.previewEntity = null;
                    }
                }

            }


            EndAction();

            return true;
        }

        private double GetTotalDist(List<Point3D> points)
        {
            double dist = 0;
            for (int i = 1; i < points.Count; ++i)
            {
                dist += points[i].DistanceTo(points[i - 1]);
            }
            return dist;
        }
    }
}
