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
        protected bool outlineSection = false;  // section을 그릴때 외곽선으로만 그릴지?
        // 높이 입력 받을때 workplane을 비활성화 할지?
        protected bool disableWorkplaneForHeight = true;
        protected bool activeDynamicInputManagerForRadius = true;
        protected Plane oldPlane;
        protected Point3D centerPoint, radiusPoint, heightPoint;
        public ActionCylinder(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        public override async void Run()
        { await RunAsync(); }


        virtual protected Entity Make3D(bool tempEntity)
        {
            if (centerPoint == null || radiusPoint == null || heightPoint == null)
                return null;

            var radius = centerPoint.DistanceTo(radiusPoint);
            var height = oldPlane.DistanceTo(heightPoint);
            if (radius == 0 || height == 0)
                return null;

            var reverseHeight = height < 0;
            height = Math.Abs(height);

            Entity cylinder = null;
            if (tempEntity)
                cylinder = Mesh.CreateCylinder(radius, height, 10);
            else
                cylinder = Brep.CreateCylinder(radius, height);
            cylinder.TransformBy(new Transformation(centerPoint, oldPlane.AxisX, oldPlane.AxisY, reverseHeight ? oldPlane.AxisZ * -1: oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(cylinder, tempEntity);
            return cylinder;
        }
        virtual protected Entity MakeSection()
        {
            if (centerPoint == null || point3D == null)
                return null;

           
            var wp = GetWorkplane();
            var radius = wp.DistanceTo(centerPoint, point3D);
            if (radius <= 0)
                return null;

            var circlePlane = wp.Clone() as Plane;
            if (circlePlane == null)
                return null;
            circlePlane.Origin = centerPoint;

            Entity circle = null;
            if (outlineSection)
            {
                circle = new Circle(circlePlane, radius);
            }
            else
            {
                circle = Region.CreateCircle(circlePlane, radius);
            }
            
            if (circle == null)
                return null;

            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(circle, true);

            return circle;
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);
            if (centerPoint == null)
                return;

            if (radiusPoint == null)
            {
                var section = MakeSection();
                SetTempEtt(environment, section);
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, point3D, 0, true, "R=", GetWorkplane());

                return;
            }

            heightPoint = point3D;
            var height = oldPlane.DistanceTo(heightPoint);
            PreviewLabel.PreviewHeightLabel(model, centerPoint, height, 1, oldPlane, true, "H=");

            var cyl = Make3D(true);
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

            var ws = GetWorkspace();
            while (true)
            {
                centerPoint = await GetPoint3D(LanguageHelper.Tr("Center point"));
                if (IsCanceled() || IsEntered())
                    break;

                SetAutoWorkspace(centerPoint);

                if(activeDynamicInputManagerForRadius)
                    DynamicInputManager.ActiveLengthFactor(centerPoint, 1, LanguageHelper.Tr("Radius"));
                radiusPoint = await GetPoint3D(LanguageHelper.Tr("Radius point"));
                SetOrthoModeStartPoint(null);
                DynamicInputManager.Init();
                if (IsCanceled() || IsEntered())
                    break;
                

                var oldEnable = ws.enabled;
                oldPlane = ws.plane;
                if(disableWorkplaneForHeight)
                    ws.enabled = false;
                if (activeDynamicInputManagerForRadius)
                    DynamicInputManager.ActiveLengthFactor(centerPoint, 1, LanguageHelper.Tr("Height"), ws.plane.AxisZ);
                heightPoint = await GetPoint3D(LanguageHelper.Tr("Height point"));
                SetOrthoModeStartPoint(null);
                DynamicInputManager.Init();
                if (IsCanceled() || IsEntered())
                    break;
                if(disableWorkplaneForHeight)
                    ws.enabled = oldEnable;

                var cylinder = Make3D(false);
                if (cylinder == null)
                    break;

                GetModel().Entities.Add(cylinder);

                centerPoint = null;
                radiusPoint = null;
                heightPoint = null;
                GetModel().TempEntities.Clear();
                GetModel().Entities.Regen();
                GetModel().Invalidate();
                break;
            }

            EndAction();
            return true;
        }

      
    }
}