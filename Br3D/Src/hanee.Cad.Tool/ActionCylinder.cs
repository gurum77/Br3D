using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionCylinder : ActionBase
    {
        Point3D centerPoint, radiusPoint, heightPoint;
        public ActionCylinder(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);
            if (centerPoint == null || radiusPoint == null)
                return;

            heightPoint = point3D;

            var cyl = MakeCylinder();
            if (cyl == null)
                return;

            cyl.Regen(0.001);
            environment.TempEntities.Clear();
            environment.TempEntities.Add(cyl);
            environment.Invalidate();
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                centerPoint = await GetPoint3D(LanguageHelper.Tr("Center point"));
                if (IsCanceled())
                    break;


                radiusPoint = await GetPoint3D(LanguageHelper.Tr("Radius point"));
                if (IsCanceled())
                    break;

                heightPoint = await GetPoint3D(LanguageHelper.Tr("Height point"));
                if (IsCanceled())
                    break;

                var cylinder = MakeCylinder();
                GetModel().Entities.Add(cylinder);

                centerPoint = null;
                radiusPoint = null;
                heightPoint = null;
                GetModel().TempEntities.Clear();
                GetModel().Entities.Regen();
                GetModel().Invalidate();
            }

            EndAction();
            return true;
        }

        private Brep MakeCylinder()
        {
            if (centerPoint == null || radiusPoint == null || heightPoint == null)
                return null;

            var centerPointPlane = Plane.XY;
            centerPointPlane.Translate(centerPoint.X, centerPoint.Y, centerPoint.Z);

            var matchPoint = centerPointPlane.Project(radiusPoint);
            if (matchPoint != null)
            {
                radiusPoint = centerPointPlane.PointAt(matchPoint);
            }

            var heightPointPlane = Plane.XY;
            heightPointPlane.Translate(heightPoint.X, heightPoint.Y, heightPoint.Z);
            matchPoint = heightPointPlane.Project(centerPoint);
            if (matchPoint != null)
            {
                heightPoint = heightPointPlane.PointAt(matchPoint);
            }


            var radius = centerPoint.DistanceTo(radiusPoint);
            var height = centerPoint.DistanceTo(heightPoint);
            if (radius == 0 || height == 0)
                return null;

            var cylinder = Brep.CreateCylinder(radius, height);
            cylinder.Translate(centerPoint.X, centerPoint.Y, centerPoint.Z);

            cylinder.Color = System.Drawing.Color.Yellow;
            cylinder.ColorMethod = colorMethodType.byEntity;
            return cylinder;
        }
    }
}