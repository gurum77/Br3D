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
            if (centerPoint == null || radius == null || height== null)
                return null;

            if (radius == 0 || height == 0)
                return null;

            var reverseHeight = height < 0;
            height = Math.Abs(height.Value);
            Entity cone = null;
            if (tempEntity)
                cone = Mesh.CreateCone(radius.Value, 0, height.Value, 10);
            else
                cone = Brep.CreateCone(radius.Value, height.Value);
            cone.TransformBy(new Transformation(centerPoint, oldPlane.AxisX, oldPlane.AxisY, reverseHeight ? oldPlane.AxisZ * -1: oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(cone, tempEntity);
            return cone;
        }
    }
}
