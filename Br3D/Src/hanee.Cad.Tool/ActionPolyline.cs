using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Environment = devDept.Eyeshot.Environment;

namespace hanee.Cad.Tool
{
    public class ActionPolyline : ActionBase
    {
        List<Point3D> points = new List<Point3D>();
        float width = 0.5f;
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
                KeyValuePair<Point3D, KeyEventArgs> pk;
                Point3D point = null;
                if (points.Count == 0)
                    pk = await GetPoint3DOrKey(LanguageHelper.Tr("Point(W : Width)"), -1, new KeyEventArgs(Keys.W));
                else if (points.Count < 3)
                    pk = await GetPoint3DOrKey(LanguageHelper.Tr("Next point(W : Width)"), -1, new KeyEventArgs(Keys.W));
                else
                    pk = await GetPoint3DOrKey(LanguageHelper.Tr("Next point(W : Width, C : Close)"), -1, new KeyEventArgs(Keys.W), new KeyEventArgs(Keys.C));

                

                if (pk.Value != null && pk.Value.KeyCode == Keys.C)
                {
                    // C를 누르며 입력 완료
                    point = points[0].Clone() as Point3D;
                    points.Add(point);
                    Entered = true;
                }
                else if (pk.Value != null && pk.Value.KeyCode == Keys.W)
                {
                    FormInputMessage form = new FormInputMessage();
                    form.Text = LanguageHelper.Tr("Width(> 0)");
                    form.RichTextBox.Text = width.ToString();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        width = form.RichTextBox.Text.ToFloat();
                        if (width <= 0)
                            width = 0.5f;
                    }
                    continue;
                }
                else if (pk.Key != null)
                {
                    point = pk.Key;
                }

                if (point == null)
                    break;

                if(points.Count == 0)
                    SetAutoWorkspace();
                SetOrthoModeStartPoint(point);

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
                    points.Add(point);
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
                    lp.LineWeight = width;
                    lp.LineWeightMethod = colorMethodType.byEntity;
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(lp, tempEntity);
                    return lp;
                }
                else
                {
                    var lp = new LinearPath(curPoints);
                    lp.LineWeight = width;
                    lp.LineWeightMethod = colorMethodType.byEntity;
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(sp, tempEntity);
                    return sp;

                }
            }
            else
            {

                var lp = new LinearPath(curPoints);
                lp.LineWeight = width;
                lp.LineWeightMethod = colorMethodType.byEntity;
                GetHModel()?.entityPropertiesManager?.SetDefaultProperties(lp, tempEntity);
                return lp;
            }
        }
    }
}
