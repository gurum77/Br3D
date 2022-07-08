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
        public ActionWorkspace(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

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
                SetTempEtt(GetHModel(), lp, true);
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

            var face = await GetFace(LanguageHelper.Tr("Select workspace face"), -1, true);
            var points = GetSelectedFace(face);

            var model = GetHModel();
            if (model != null && points != null && points.Length > 2)
                model.StartWorkspace(points[0], points[1], points[2]);

            EndAction();
            return true;
        }
    }
}
