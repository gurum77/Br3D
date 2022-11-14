using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionCone : ActionCylinder
    {
        public ActionCone(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        protected override Entity Make3D(bool tempEntity)
        {
            if (centerPoint == null || radius == null)
                return null;

            // 높이
            var curHeight = height == null ? secPlane.DistanceTo(point3D) : height.Value;
            if (radius == 0 || curHeight  == 0)
                return null;

            var reverseHeight = curHeight < 0;
            curHeight = Math.Abs(curHeight);
            Entity cone = null;
            if (tempEntity)
                cone = Mesh.CreateCone(radius.Value, 0, curHeight, 10);
            else
                cone = Brep.CreateCone(radius.Value, curHeight);
            cone.TransformBy(new Transformation(centerPoint, secPlane.AxisX, secPlane.AxisY, reverseHeight ? secPlane.AxisZ * -1: secPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(cone, tempEntity);
            return cone;
        }
    }
}
