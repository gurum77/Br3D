using devDept.Eyeshot.Entities;
using devDept.Geometry;
using static devDept.Eyeshot.Environment;

namespace hanee.Geometry
{
    public static class SelectedFaceHelper
    {
        // 선택한 face를 temp entity로  변환
        public static Entity ToTempEntity(this SelectedFace face, devDept.Eyeshot.Environment env)
        {
            if (face.Item is Mesh mesh && face.Index > -1)
            {
                var tri = mesh.Triangles[face.Index];
                var lp = new LinearPath(mesh.Vertices[tri[0]].Clone() as Point3D,
                    mesh.Vertices[tri[1]].Clone() as Point3D,
                    mesh.Vertices[tri[2]].Clone() as Point3D,
                    mesh.Vertices[tri[0]].Clone() as Point3D);

             
                if (face.HasParents() && face.Parents.Count > 0)
                {
                    var parent = face.Parents.ToArray()[0];
                    if (parent is BlockReference br)
                    {
                        var trans = br.GetFullTransformation(env.Blocks);
                        lp.TransformBy(trans);
                    }
                }

                return lp;
            }

            return null;
        }
    }
}
