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
    public class ActionTorus : ActionBase
    {
        protected Point3D centerPoint;
        protected double? majorRadius = null;
        protected double? minorRadius = null;

        public ActionTorus(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while(true)
            {
                centerPoint = await GetPoint3D(LanguageHelper.Tr("Center point"));
                if (IsCanceled() || IsEntered())
                    break;

                SetAutoWorkspace(centerPoint);
                majorRadius = await GetDist(LanguageHelper.Tr("Major radius"), centerPoint);
                if (IsCanceled() || IsEntered())
                    break;

                var wp = GetWorkplane();
                var majorRadiusPoint = centerPoint + wp.AxisX * majorRadius.Value;
                minorRadius = await GetDist(LanguageHelper.Tr("Minor radius"), majorRadiusPoint);
                if (IsCanceled() || IsEntered())
                    break;

                var ent = Make3D(false);
                if (ent == null)
                    break;

                GetModel().Entities.Add(ent);

                centerPoint = null;
                majorRadius = null;
                minorRadius = null;
                GetModel().TempEntities.Clear();
                GetModel().Entities.Regen();
                GetModel().Invalidate();
                break;
            }

            EndAction();
            return true;
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            if (centerPoint == null)
                return;

            // majorRadius 입력전이면 원을 그린다.
            if (majorRadius == null)
            {
                var wp = GetWorkplane();
                var radius = centerPoint.DistanceTo(point3D);
                if (radius <= 0)
                    return;

                var circle = new Circle(wp, centerPoint, radius);
                GetHModel()?.entityPropertiesManager?.SetDefaultProperties(circle, true);
                environment.TempEntities.ReplaceEntityAndRegen(circle);

                // 치수
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, point3D, 0, true, "R1=");
            }
            else if (minorRadius == null)
            {
                var majorRadius = GetMajorRadius();
                var minorRadius = GetMinorRadius();

                // 입력한 치수로 원을 그린다.
                var wp = GetWorkplane();
                var circle = new Circle(wp, centerPoint, majorRadius);
                GetHModel()?.entityPropertiesManager?.SetDefaultProperties(circle, true);
                environment.TempEntities.ReplaceEntityAndRegen(circle);

                var ent = Make3D(true);
                GetHModel()?.entityPropertiesManager?.SetDefaultProperties(ent, true);
                environment.TempEntities.ReplaceEntityAndRegen(ent);

                // 치수
                var dir = (point3D - centerPoint).ToDir();
                var majorRadiusPoint = centerPoint + dir * majorRadius;
                var minorRadiusPoint = majorRadiusPoint + dir * minorRadius;
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, majorRadiusPoint, 0, true, "R1=");
                PreviewLabel.PreviewDistanceLabel(model, majorRadiusPoint, minorRadiusPoint, 1, true, "R2=");
            }
            
        }

        double GetMajorRadius()
        {
            var curMajorRadius = majorRadius == null ? point3D.DistanceTo(centerPoint) : majorRadius.Value;
            curMajorRadius = Math.Abs(curMajorRadius);
            return curMajorRadius;
        }

        double GetMinorRadius()
        {
            var curMinorRadius = minorRadius == null ? point3D.DistanceTo(centerPoint) : minorRadius.Value;
            // minor radius는 
            if (minorRadius == null)
            {
                var dir = (point3D - centerPoint).ToDir();
                var curMajorRadius = GetMajorRadius();
                var majorRadiusPoint = centerPoint + dir * curMajorRadius;
                // click으로 minorradius를 입력할때 기준 좌표 재설정
                SetOrthoModeStartPoint(majorRadiusPoint);
                curMinorRadius = point3D.DistanceTo(majorRadiusPoint);
            }

            curMinorRadius = Math.Abs(curMinorRadius);
            return curMinorRadius;
        }

        // majorRadius 까지는 입력이 되어야 3D를 만들 수 있다.
        protected Entity Make3D(bool tempEntity)
        {
            if (centerPoint == null || majorRadius == null)
                return null;

            var curMajorRadius = GetMajorRadius();
            var curMinorRadius = GetMinorRadius();
            if (curMajorRadius == 0 || curMinorRadius == 0)
                return null;


            Entity torus;
            if (tempEntity)
                torus = Mesh.CreateTorus(curMajorRadius, curMinorRadius, 20, 20);
            else
                torus = Brep.CreateTorus(curMajorRadius, curMinorRadius, Math.Min(0.001, (curMinorRadius / 10)));

            var plane = GetWorkplane();
            if (plane == null)
                return null;
            torus.TransformBy(new Transformation(centerPoint, plane.AxisX, plane.AxisY, plane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(torus, tempEntity);

            return torus;
        }
    }
}
