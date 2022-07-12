using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    public class Workspace
    {
        public Plane plane { get; set; } = Plane.XY;
        public bool enabled { get; set; } = false;

        public Point2D Project(Point3D P) => plane.Project(P);
        public Point3D PointAt(Point2D pt) => plane.PointAt(pt);
       
    }
}
