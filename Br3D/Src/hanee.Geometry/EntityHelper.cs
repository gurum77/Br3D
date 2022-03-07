using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace hanee.Geometry
{
    // entity helper
    static public class EntityHelper
    {
        public static bool IsDepthTestAlwaysEntity(this Entity ent)
        {
            if (ent is DepthTestAlwaysBlockReference)
                return true;
            else if (ent is DepthTestAlwaysLinearPath)
                return true;
            else if (ent is DepthTestAlwaysRegion)
                return true;
            else if (ent is DepthTestAlwaysText)
                return true;

            return false;

        }
        public static Entity CloneToDepthTestAlwaysEntity(this Entity ent)
        {
            if (ent is LinearPath)
            {
                return new DepthTestAlwaysLinearPath(ent as LinearPath);
            }
            else if (ent is Text)
            {
                return new DepthTestAlwaysText(ent as Text);
            }
            else if (ent is Region)
            {
                return new DepthTestAlwaysRegion(ent as Region);
            }

            return ent.Clone() as Entity;
        }

        /// <summary>
        /// 객체 2D방식으로 회전 한다.
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angleInRadian"></param>
        public static void Rotate2D(this Entity ent, double x, double y, double angleInRadian)
        {
            ent.Rotate(angleInRadian, new Vector3D(0, 0, 1), new Point3D(x, y, 0));
        }

        // 유효한 객체인지?
        public static bool IsValid(this Entity ent)
        {
            LinearPath lp = ent as LinearPath;
            if (lp.Vertices.Length == 0)
                return false;

            return true;
        }
    }
}
