using devDept.Geometry;

namespace hanee.Geometry
{
    static public class Vector3DHelper
    {
        static public Vector2D To2D(this Vector3D vec)
        {
            return new Vector2D(vec.X, vec.Y);
        }
    }
}
