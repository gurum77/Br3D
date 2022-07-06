using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionWorkspace : ActionBase
    {
        public ActionWorkspace(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var face = await GetFace(LanguageHelper.Tr("Select workspace face"), -1, true);
            if (face != null)
            {
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



                    hModel.workSpace.plane.CreateFromPoints(pt1, pt2, pt3);
                    hModel.workSpace.enabled = true;

                    var sym = hModel.ActiveViewport.OriginSymbols.Length > 1 ? hModel.ActiveViewport.OriginSymbols[1] : null;
                    if (sym != null)
                    {
                        sym.Transformation = new Transformation(hModel.workSpace.plane.Origin, hModel.workSpace.plane.AxisX, hModel.workSpace.plane.AxisY, hModel.workSpace.plane.AxisZ);
                        sym.Visible = true;
                        hModel.Invalidate();
                    }
                }

            }

            EndAction();
            return true;
        }
    }
}
