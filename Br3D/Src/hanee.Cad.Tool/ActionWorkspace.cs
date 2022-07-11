using devDept.Eyeshot.Entities;
using devDept.Geometry;
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

                var points = GetSelectedFace(face);
                if (points != null)
                {
                    var lp = new LinearPath(points);
                    lp.LineWeight = 5;
                    lp.LineWeightMethod = colorMethodType.byEntity;
                    SetTempEtt(GetHModel(), lp, false);
                }
            }

        }

        Point3D[] GetSelectedFace(SelectedFace face)
        {
            if (face == null)
                return null;


            if (face.Item is Mesh mesh)
            {
                var hModel = GetHModel();
                var parent = face.Parents.Count > 0 ? face.Parents.Pop() : null;
                var fTrans = parent?.GetFullTransformation(hModel.Blocks);

                var tri = mesh.Triangles[face.Index];
                var pt1 = mesh.Vertices[tri.V1].Clone() as Point3D;
                var pt2 = mesh.Vertices[tri.V2].Clone() as Point3D;
                var pt3 = mesh.Vertices[tri.V3].Clone() as Point3D;
                if (fTrans != null)
                {
                    pt1.TransformBy(fTrans);
                    pt2.TransformBy(fTrans);
                    pt3.TransformBy(fTrans);
                }

                return new Point3D[] { pt1, pt2, pt3 };

            }

            return null;
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


            point1 = null;
            point2 = null;
            point3 = null;

            while (true)
            {
                var face = await GetFaceOrKey(LanguageHelper.Tr(" Select workspace face(3 : 3 points, W : World)"), -1, true);
                if (IsCanceled())
                    break;

                if (face.Key != null)
                {
                    var points = GetSelectedFace(face.Key);
                    if (points != null && points.Length > 2)
                    {
                        point1 = points[0];
                        point2 = points[1];
                        point3 = points[2];
                    }
                }
                // 표준 좌표계로 설정
                else if(face.Value?.KeyCode == Keys.W)
                {
                    break;
                }
                else
                {

                    point1 = await GetPoint3D(LanguageHelper.Tr("Origin point(f - Select face)"));
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
            else
                model.EndWorkspace();

            EndAction();
            return true;
        }
    }
}
