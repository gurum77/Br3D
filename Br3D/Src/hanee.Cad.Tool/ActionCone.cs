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

        protected override Brep Make3D(bool tempEntity)
        {
            if (centerPoint == null || radiusPoint == null || heightPoint == null)
                return null;

            var radius = centerPoint.DistanceTo(radiusPoint);
            var height = oldPlane.DistanceTo(heightPoint);
            if (radius == 0 || height == 0)
                return null;

            var reverseHeight = height < 0;
            height = Math.Abs(height);
            var cone = Brep.CreateCone(radius, height);
            cone.TransformBy(new Transformation(centerPoint, oldPlane.AxisX, oldPlane.AxisY, reverseHeight ? oldPlane.AxisZ * -1: oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(cone, tempEntity);
            return cone;
        }
    }
}
