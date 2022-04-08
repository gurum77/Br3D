using devDept.Eyeshot.Entities;
using devDept.Geometry;

namespace hanee.Geometry
{
    static public class OrientedBoundingRectHelper
    {
        static public LinearPath GetBoundingLinearPath(this OrientedBoundingRect rect, double z)
        {
            if (!rect.Size.IsValid())
                return null;

            var pt1 = new Point3D(0, 0);
            var pt2 = new Point3D(rect.Size.X, 0);
            var pt3 = new Point3D(rect.Size.X, rect.Size.Y);
            var pt4 = new Point3D(0, rect.Size.Y);
            var pt5 = new Point3D(0, 0);
            var lp = new LinearPath(pt1, pt2, pt3, pt4, pt5);
            lp.TransformBy(rect.Transformation);
            
            
            return lp;
        }
    }
}
