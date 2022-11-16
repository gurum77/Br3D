using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;
using static devDept.Eyeshot.Environment;

namespace hanee.Cad.Tool
{
    public class ActionWorkspace : ActionBase
    {
        Point3D point1, point2, point3;

        public ActionWorkspace(devDept.Eyeshot.Environment environment) : base(environment)
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
            if(model == null)
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
                var face = await GetFaceOrText(LanguageHelper.Tr(" Select workspace face(3 : 3 points, W : World, XY : XY, XZ : XZ, YZ : YZ)"), -1, false, "3", "W", "XY", "XZ", "YZ");
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
                // 표준 좌표계로 설정
                else if(face.Value != null && face.Value.EqualsIgnoreCase("W"))
                {
                    break;
                }
                // xy평면
                else if(face.Value != null && face.Value.EqualsIgnoreCase("XY"))
                {
                    point1 = new Point3D(0, 0, 0);
                    point2 = new Point3D(1, 0, 0);
                    point3 = new Point3D(0, 1, 0);
                }
                // xz 평면
                else if(face.Value != null && face.Value.EqualsIgnoreCase("XZ"))
                {
                    point1 = new Point3D(0, 0, 0);
                    point2 = new Point3D(1, 0, 0);
                    point3 = new Point3D(0, 0, 1);
                }
                // yz평면
                else if(face.Value != null && face.Value.EqualsIgnoreCase("YZ"))
                {
                    point1 = new Point3D(0, 0, 0);
                    point2 = new Point3D(0, 1, 0);
                    point3 = new Point3D(0, 0, 1);
                }
                else if(face.Value != null && face.Value.EqualsIgnoreCase("3"))
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
                model.StartWorkspace(point1, point2, point3);
               

            EndAction();
            return true;
        }
    }
}
