using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace hanee.Geometry
{
    static public class MeshHelper
    {
        // mesh2개가 교차되는지?
        public static bool IsIntersection(this Mesh mesh, Mesh otherMesh)
        {
            foreach (var tri in mesh.Triangles)
            {
                var points = tri.GetPoints(mesh);
                if (points == null || points.Count != 3)
                    continue;
                foreach (var otherTri in otherMesh.Triangles)
                {
                    var otherPoints = otherTri.GetPoints(otherMesh);
                    if (otherPoints == null || otherPoints.Count != 3)
                        continue;

                    if (UtilityEx.TriangleTriangleIntersection(points[0], points[1], points[2], otherPoints[0], otherPoints[1], otherPoints[2], out bool touch) && touch)
                        return true;
                }
            }

            return false;
        }

        // triangle의 index와 triangle의 코너 index에 해당하는 vertex 리턴
        public static Point3D GetVertexByTriangle(this Mesh mesh, int triangleIdx, int cornorIdx)
        {
            if (triangleIdx < 0 || triangleIdx >= mesh.Triangles.Length)
                return null;

            IndexTriangle tri = mesh.Triangles[triangleIdx];
            int vertexIdx = -1;

            if (cornorIdx == 0)
                vertexIdx = tri.V1;
            else if (cornorIdx == 1)
                vertexIdx = tri.V2;
            else
                vertexIdx = tri.V3;

            if (vertexIdx < 0 || vertexIdx >= mesh.Vertices.Length)
                return null;
            return mesh.Vertices[vertexIdx];
        }

        /// <summary>
        /// mesh에서 v1, v2를 사용하는 다른 삼각형이 있는지 체크
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="tri"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool IsSharedEdge(this Mesh mesh, IndexTriangle tri, int v1, int v2)
        {
            for (int i = 0; i < 2; ++i)
            {
                if (mesh.Vertices[i == 0 ? v1 : v2].Equals(new Point3D(-25.24502, 51.42133, 0), 0.1))
                {

                }
            }

            for (int i = 0; i < mesh.Triangles.Length; ++i)
            {
                IndexTriangle tri2 = mesh.Triangles[i];
                if (tri2 == tri)
                    continue;

                if (tri2.V1 == 63 || tri2.V2 == 63 || tri2.V3 == 63)
                {

                }

                if ((v1 == tri2.V1 || v1 == tri2.V2 || v1 == tri2.V3) &&
                    (v2 == tri2.V1 || v2 == tri2.V2 || v2 == tri2.V3))
                    return true;

            }

            return false;
        }

        /// <summary>
        /// edge들로 region을 만든다.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static Region MakeRegionByEdges(Mesh mesh, List<KeyValuePair<int, int>> edges)
        {
            List<Line> lines = new List<Line>();
            foreach (var edge in edges)
            {
                Point3D pt1 = mesh.Vertices[edge.Key];
                Point3D pt2 = mesh.Vertices[edge.Value];
                lines.Add(new Line(pt1, pt2));
            }


            // line들을 하나의 linearPath로 변환
            LinearPath lp = LinearPathHelper.FromLines(lines);
            if (lp == null)
                return null;

            Region region = new Region(lp);

            return region;
        }

        /// <summary>
        /// 공유하지 않는 edge의 point를 리턴
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public List<Point3D> GetUnsharedEdgePoints(this Mesh mesh)
        {
            var unshardEdges = mesh.GetUnsharedEdges();
            if (unshardEdges == null)
                return null;

            Dictionary<Point3D, bool> dic = new Dictionary<Point3D, bool>();
            foreach (var edge in unshardEdges)
            {
                dic[mesh.Vertices[edge.Key]] = true;
                dic[mesh.Vertices[edge.Value]] = true;
            }

            return dic.Keys.ToList();
        }

        /// <summary>
        /// 공유하지 않는 edge를 찾는다.
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public List<KeyValuePair<int, int>> GetUnsharedEdges(this Mesh mesh)
        {
            if (mesh == null)
                return null;

            List<KeyValuePair<int, int>> unsharedEdges = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < mesh.Triangles.Length; ++i)
            {
                IndexTriangle tri = mesh.Triangles[i];

                // v1, v2를 공유하는지?
                if (!mesh.IsSharedEdge(tri, tri.V1, tri.V2))
                {
                    unsharedEdges.Add(new KeyValuePair<int, int>(tri.V1, tri.V2));
                }

                // v2, v3를 공유하는지?
                if (!mesh.IsSharedEdge(tri, tri.V2, tri.V3))
                {
                    unsharedEdges.Add(new KeyValuePair<int, int>(tri.V2, tri.V3));
                }

                // v3, v1를 공유하는지?
                if (!mesh.IsSharedEdge(tri, tri.V3, tri.V1))
                {
                    unsharedEdges.Add(new KeyValuePair<int, int>(tri.V3, tri.V1));
                }
            }

            // 중복 체크
            {
                for (int i = 0; i < unsharedEdges.Count; ++i)
                {
                    var e1 = unsharedEdges[i];
                    for (int j = 0; j < unsharedEdges.Count; ++j)
                    {
                        if (i == j)
                            continue;

                        var e2 = unsharedEdges[j];

                        if (e1.Key == e2.Key && e1.Value == e2.Value)
                        {
                            System.Diagnostics.Debug.Assert(false);
                        }
                        else if (e1.Key == e2.Value && e1.Value == e2.Key)
                        {
                            System.Diagnostics.Debug.Assert(false);
                        }
                    }
                }
            }


            return unsharedEdges;
        }



        /// <summary>
        /// mesh를 region으로 변환
        /// 2D mesh만 변환이 가능하다.
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public Region MeshToRegion(Mesh mesh)
        {
            if (mesh == null)
                return null;

            // 서로 공유하지 않는 edge를 추출
            List<KeyValuePair<int, int>> unsharedEdges = mesh.GetUnsharedEdges();


            // 공유하지 않는 edge들을 연결
            return MakeRegionByEdges(mesh, unsharedEdges);
        }
    }
}
