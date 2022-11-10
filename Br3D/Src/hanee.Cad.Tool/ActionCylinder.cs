using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
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
        protected Point3D centerPoint/*, radiusPoint, heightPoint*/;
        protected double? radius = null;
        protected double? height = null;
        public ActionCylinder(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        public override async void Run()
        { await RunAsync(); }


        virtual protected Entity Make3D(bool tempEntity)
        {
            if (centerPoint == null || radius == null || height == null)
                return null;

            if (radius.Value == 0 || height.Value == 0)
                return null;

            var reverseHeight = height.Value < 0;
            height = Math.Abs(height.Value);

            Entity cylinder = null;
            if (tempEntity)
                cylinder = Mesh.CreateCylinder(radius.Value, height.Value, 10);
            else
                cylinder = Brep.CreateCylinder(radius.Value, height.Value);
            cylinder.TransformBy(new Transformation(centerPoint, oldPlane.AxisX, oldPlane.AxisY, reverseHeight ? oldPlane.AxisZ * -1 : oldPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(cylinder, tempEntity);
            return cylinder;
        }
        virtual protected Entity MakeSection()
        {
            if (centerPoint == null || (point3D == null && radius == null))
                return null;


            var wp = GetWorkplane();
            var curRadius = radius == null ? wp.DistanceTo(centerPoint, point3D) : radius.Value;
            if (curRadius <= 0)
                return null;

            var circlePlane = wp.Clone() as Plane;
            if (circlePlane == null)
                return null;
            circlePlane.Origin = centerPoint;

            Entity circle = null;
            if (outlineSection)
            {
                circle = new Circle(circlePlane, curRadius);
            }
            else
            {
                circle = Region.CreateCircle(circlePlane, curRadius);
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

            var wp = GetWorkplane();
            
            if(radius == null)
            {
                var section = MakeSection();
                SetTempEtt(environment, section);
                var radiusPoint = radius == null ? point3D : centerPoint + wp.AxisX * radius.Value;
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, radiusPoint, 0, true, "R=", wp);
            }
            else
            {
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, centerPoint, 0, false, $"R={radius:0.000}");
            }
            


            if (radius != null)
            {
                var curHeight = oldPlane.DistanceTo(point3D);
                PreviewLabel.PreviewHeightLabel(model, centerPoint, curHeight, 1, oldPlane, true, "H=");
            }

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

                if (activeDynamicInputManagerForRadius)
                    DynamicInputManager.ActiveLengthFactor(centerPoint, 1, LanguageHelper.Tr("Radius"));
                radius = await GetDist(LanguageHelper.Tr("Radius"));
                SetOrthoModeStartPoint(null);
                DynamicInputManager.Init();
                if (IsCanceled() || IsEntered())
                    break;


                var oldEnable = ws.enabled;
                oldPlane = ws.plane;
                if (disableWorkplaneForHeight)
                    ws.enabled = false;
                if (activeDynamicInputManagerForRadius)
                    DynamicInputManager.ActiveLengthFactor(centerPoint, 1, LanguageHelper.Tr("Height"), ws.plane.AxisZ);
                height = await GetDist(LanguageHelper.Tr("Height"));
                SetOrthoModeStartPoint(null);
                DynamicInputManager.Init();
                if (IsCanceled() || IsEntered())
                    break;
                if (disableWorkplaneForHeight)
                    ws.enabled = oldEnable;

                var cylinder = Make3D(false);
                if (cylinder == null)
                    break;

                GetModel().Entities.Add(cylinder);

                centerPoint = null;
                radius = null;
                height = null;
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