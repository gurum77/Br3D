using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;

namespace hanee.Geometry
{
    static public class IndexTriangleHelper
    {
        /// <summary>
        /// Y좌표가 가장큰 점
        /// </summary>
        /// <param name=""></param>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public Point3D GetYMaxPoint(this IndexTriangle tri, Mesh mesh)
        {
            List<Point3D> points = tri.GetPoints(mesh);
            if (points == null)
                return null;

            Point3D yMaxPoint = null;
            foreach (var p in points)
            {
                if (yMaxPoint == null || yMaxPoint.Y < p.Y)
                {
                    yMaxPoint = p;
                }

            }

            return yMaxPoint;
        }

        /// <summary>
        /// Y좌표가 가장 작은 점
        /// </summary>
        /// <param name=""></param>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public Point3D GetYMinPoint(this IndexTriangle tri, Mesh mesh)
        {
            List<Point3D> points = tri.GetPoints(mesh);
            if (points == null)
                return null;

            Point3D yMinPoint = null;
            foreach (var p in points)
            {
                if (yMinPoint == null || yMinPoint.Y > p.Y)
                {
                    yMinPoint = p;
                }

            }

            return yMinPoint;
        }

        /// <summary>
        /// mesh내 삼각형의 중심좌표
        /// </summary>
        /// <param name="tri"></param>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public Point3D GetCenter(this IndexTriangle tri, Mesh mesh) => Util.GetCenter(tri.GetPoints(mesh));

        /// <summary>
        /// triangle의 세 좌표를 리턴
        /// </summary>
        /// <param name="tri"></param>
        /// <param name="mesh"></param>
        /// <returns></returns>
        static public List<Point3D> GetPoints(this IndexTriangle tri, Mesh mesh) => new List<Point3D>() { mesh.Vertices[tri.V1], mesh.Vertices[tri.V2], mesh.Vertices[tri.V3] };
    }
}
