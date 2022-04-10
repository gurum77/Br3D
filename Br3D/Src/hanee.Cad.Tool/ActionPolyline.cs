using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionPolyline : ActionBase
    {
        List<Point3D> points = new List<Point3D>();
        public bool spline { get; set; } = false;
        public ActionPolyline(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }


        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (points == null || points.Count < 1)
                return;

            var cutPoints = new List<Point3D>();
            cutPoints.AddRange(points);
            cutPoints.Add(point3D.Clone() as Point3D);

            var lp = MakePolyline(cutPoints, true);

            //lp.Regen(0.001);
            GetModel().TempEntities.Clear();
            GetModel().TempEntities.Add((Entity)lp);
            GetModel().Invalidate();
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var pt = await GetPoint3D(LanguageHelper.Tr("Point"));
                SetOrthoModeStartPoint(pt);

                // pline은 취소를 눌러도 입력 완료로 한다.
                if (IsEntered() || IsCanceled())
                {
                    if (points.Count > 1)
                    {
                        var pline = MakePolyline(points);
                        if (pline != null)
                        {
                            GetModel().Entities.Add((Entity)pline);
                        }
                    }
                    break;
                }
                else
                {
                    points.Add(pt);
                }
            }

            EndAction();
            return true;
        }

        ICurve MakePolyline(List<Point3D> curPoints, bool tempEntity = false)
        {
            if (curPoints == null || curPoints.Count < 2)
                return null;

            if (spline && curPoints.Count > 2)
            {
                var sp = Curve.CubicSplineInterpolation(curPoints);

                // spline은 미리보기 불가
                if (tempEntity)
                {
                    var subd = 100;
                    Point3D[] pts = new Point3D[subd + 1];

                    for (int i = 0; i <= subd; i++)
                    {
                        pts[i] = sp.PointAt(sp.Domain.ParameterAt((double)i / subd));
                    }
                    var lp = new LinearPath(pts);
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(lp, tempEntity);
                    return lp;
                }
                else
                {
                    var lp = new LinearPath(curPoints);
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(sp, tempEntity);
                    return sp;

                }
            }
            else
            {

                var lp = new LinearPath(curPoints);
                GetHModel()?.entityPropertiesManager?.SetDefaultProperties(lp, tempEntity);
                return lp;
            }
        }
    }
}
