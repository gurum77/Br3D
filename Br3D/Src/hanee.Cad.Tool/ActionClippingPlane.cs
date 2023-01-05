using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static devDept.Eyeshot.Environment;

namespace hanee.Cad.Tool
{
    public class ActionClippingPlane : ActionBase
    {
        Point3D point1, point2, point3;

        public ActionClippingPlane(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            SetTempEtt(environment, null, true);

            if (point3D != null)
            {
                // x축 지정중인 경우
                if (point1 != null && point2 == null)
                {
                    var line = new Line(point1, point3D);
                    line.Color = System.Drawing.Color.Red;
                    line.ColorMethod = colorMethodType.byEntity;
                    line.LineWeight = 3;
                    line.LineWeightMethod = colorMethodType.byEntity;
                    SetTempEtt(environment, line, false);
                }
                // y축 지정중인 경우
                else if (point1 != null && point2 != null)
                {
                    var line = new Line(point1, point2);
                    line.Color = System.Drawing.Color.Red;
                    line.ColorMethod = colorMethodType.byEntity;
                    line.LineWeight = 3;
                    line.LineWeightMethod = colorMethodType.byEntity;
                    SetTempEtt(environment, line, false);

                    line = new Line(point1, point3D);
                    line.Color = System.Drawing.Color.Green;
                    line.ColorMethod = colorMethodType.byEntity;
                    line.LineWeight = 3;
                    line.LineWeightMethod = colorMethodType.byEntity;
                    SetTempEtt(environment, line, false);
                }
            }

            if (point1 == null && point2 == null && point3 == null)
            {
                var face = ActionBase.LastSelectedItem?.Item as SelectedFace;
                if (face == null)
                {
                    SetTempEtt(GetHModel(), null);
                    return;
                }

                var points = environment.GetSelectedFace(face);
                if (points != null)
                {
                    var lp = new LinearPath(points);
                    lp.LineWeight = 5;
                    lp.LineWeightMethod = colorMethodType.byEntity;
                    SetTempEtt(GetHModel(), lp, false);
                }
            }

        }



        public async Task<bool> RunAsync()
        {
            StartAction();

            var model = GetHModel();
            if (model == null)
            {
                EndAction();
                return true;
            }

            // 일단 초기화
            model.EndWorkspace();

            point1 = null;
            point2 = null;
            point3 = null;

            while (true)
            {
                var face = await GetFaceOrText(LanguageHelper.Tr("Specify plane(X : Exit, 3 : 3 points, XY : XY, XZ : XZ, YZ : YZ)"), -1, false, "X", "3", "W", "XY", "XZ", "YZ");
                if (IsCanceled())
                    break;

                if (face.Key != null)
                {
                    var points = environment.GetSelectedFace(face.Key);
                    if (points != null && points.Length > 2)
                    {
                        point1 = points[0];
                        point2 = points[1];
                        point3 = points[2];
                    }
                }
                // off
                else if(face.Value != null && face.Value.EqualsIgnoreCase("X"))
                {
                    MakeClippingPlane(null, null, null);
                    break;
                }
                // xy평면
                else if (face.Value != null && face.Value.EqualsIgnoreCase("XY"))
                {
                    point1 = new Point3D(0, 0, 0);
                    point2 = new Point3D(1, 0, 0);
                    point3 = new Point3D(0, 1, 0);

                    AdjustPointsToCenter(ref point1, ref point2, ref point3);
                }
                // xz 평면
                else if (face.Value != null && face.Value.EqualsIgnoreCase("XZ"))
                {
                    point1 = new Point3D(0, 0, 0);
                    point2 = new Point3D(1, 0, 0);
                    point3 = new Point3D(0, 0, 1);

                    AdjustPointsToCenter(ref point1, ref point2, ref point3);
                }
                // yz평면
                else if (face.Value != null && face.Value.EqualsIgnoreCase("YZ"))
                {
                    point1 = new Point3D(0, 0, 0);
                    point2 = new Point3D(0, 1, 0);
                    point3 = new Point3D(0, 0, 1);

                    AdjustPointsToCenter(ref point1, ref point2, ref point3);
                }
                else if (face.Value != null && face.Value.EqualsIgnoreCase("3"))
                {

                    point1 = await GetPoint3D(LanguageHelper.Tr("Origin point"));
                    if (IsCanceled())
                        break;
                    point2 = await GetPoint3D(LanguageHelper.Tr("X-axis point"));
                    if (IsCanceled())
                        break;
                    point3 = await GetPoint3D(LanguageHelper.Tr("Y-axis point"));
                    if (IsCanceled())
                        break;
                }

                break;
            }


            if (point1 != null && point2 != null && point3 != null)
            {
                MakeClippingPlane(point1, point2, point3);
            }

            EndAction();
            return true;
        }

        private void AdjustPointsToCenter(ref Point3D point1, ref Point3D point2, ref Point3D point3)
        {
            var plane = new Plane(point1, point2, point3);

            var center = (model.Entities.BoxMin + model.Entities.BoxMax) / 2;
            plane.Origin = center;

            point1 = plane.Project3D(point1);
            point2 = plane.Project3D(point2);
            point3 = plane.Project3D(point3);
        }

        protected void MakeClippingPlane(Point3D pt1, Point3D pt2, Point3D pt3)
        {
            if(pt1 == null || pt2 == null || pt3 == null)
            {
                model.ClippingPlane1.Cancel();
                return;
            }

            // 이미 편집중인 경우에는 cancel을 하고 다시 설정
            if(model.ClippingPlane1.Active == true)
            {
                model.ClippingPlane1.Cancel();
            }

            // plane 
            var plane = new Plane(pt1, pt2, pt3);

            model.ClippingPlane1.Plane = plane;
            model.ClippingPlane1.ShowPlane = true;
            model.ClippingPlane1.Edit(null);
        }
    }
}
