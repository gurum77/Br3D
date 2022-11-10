﻿using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionTorus : ActionCylinder
    {
        public ActionTorus(devDept.Eyeshot.Environment environment) : base(environment)
        {
            disableWorkplaneForHeight = false;
            outlineSection = true;
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (radius == null || height == null)
                return;

            //PreviewLabel.PreviewDistanceLabel(model, radiusPoint, heightPoint, 1, true, "R2=");
        }
        protected override Entity Make3D(bool tempEntity)
        {
            return null;
            //if (centerPoint == null || radiusPoint == null || heightPoint == null)
            //    return null;

            //var radius = centerPoint.DistanceTo(radiusPoint);
            //var height = radiusPoint.DistanceTo(heightPoint);
            //if (radius == 0 || height == 0)
            //    return null;

            //var reverseHeight = height < 0;
            //height = Math.Abs(height);

            //Entity torus;
            //if (tempEntity)
            //    torus = Mesh.CreateTorus(radius, height, 20, 20);
            //else
            //    torus = Brep.CreateTorus(radius, height, Math.Min(0.001, (radius/10)));
            //torus.TransformBy(new Transformation(centerPoint, oldPlane.AxisX, oldPlane.AxisY, oldPlane.AxisZ));
            //GetHModel()?.entityPropertiesManager?.SetDefaultProperties(torus, tempEntity);

            
            //return torus;
        }
    }
}
