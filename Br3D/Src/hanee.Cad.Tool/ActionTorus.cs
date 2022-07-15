using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionTorus : ActionCylinder
    {
        public ActionTorus(devDept.Eyeshot.Environment environment) : base(environment)
        {
            disableWorkplaneForHeight = false;
        }

        protected override Entity Make3D(bool tempEntity)
        {
            if (centerPoint == null || radiusPoint == null || heightPoint == null)
                return null;

            var radius = centerPoint.DistanceTo(radiusPoint);
            var height = radiusPoint.DistanceTo(heightPoint);
            if (radius == 0 || height == 0)
                return null;

            var reverseHeight = height < 0;
            height = Math.Abs(height);

            Entity torus;
            if (tempEntity)
                torus = Mesh.CreateTorus(radius, height, 20, 20);
            else
                torus = Brep.CreateTorus(radius, height, Math.Min(0.001, (radius/10)));
            torus.TransformBy(new Transformation(centerPoint, oldPlane.AxisX, oldPlane.AxisY, oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(torus, tempEntity);

            
            return torus;
        }
    }
}
