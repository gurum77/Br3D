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
        protected Plane secPlane;   // 단면의 plane
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
            if (centerPoint == null || radius == null)
                return null;

            // 높이
            var curHeight = height == null ? secPlane.DistanceTo(point3D) : height.Value;
            if (radius == 0 || curHeight == 0)
                return null;

            var reverseHeight = curHeight < 0;
            curHeight = Math.Abs(curHeight);

            Entity cylinder = null;
            if (tempEntity)
                cylinder = Mesh.CreateCylinder(radius.Value, curHeight, 10);
            else
                cylinder = Brep.CreateCylinder(radius.Value, curHeight);
            cylinder.TransformBy(new Transformation(centerPoint, secPlane.AxisX, secPlane.AxisY, reverseHeight ? secPlane.AxisZ * -1 : secPlane.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(cylinder, tempEntity);
            return cylinder;
        }

        virtual protected Entity MakeSection()
        {
            if (centerPoint == null || (point3D == null && radius == null))
                return null;

            var wp = secPlane == null ?  GetWorkplane() : secPlane;
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

            // 단면 그리기
            var section = MakeSection();
            SetTempEtt(environment, section);

            // 단면 R 치수 표기
            // 반지름 입력 전
            if(radius == null)
            {
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, point3D, 0, true, "R=");
            }
            // 반지름 입력 후
            else if(secPlane != null && radius != null)
            {
                PreviewLabel.PreviewDistanceLabel(model, centerPoint, centerPoint + secPlane.AxisX * radius.Value, 0, true, "R=");
            }
            

            // 높이 치수
            if (radius != null)
            {
                var curHeight = secPlane.DistanceTo(point3D);
                PreviewLabel.PreviewHeightLabel(model, centerPoint, curHeight, 1, secPlane, true, "H=");
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
                secPlane = GetWorkplane();

                if (activeDynamicInputManagerForRadius)
                    DynamicInputManager.ActiveLengthFactor(centerPoint, 1, LanguageHelper.Tr("Radius"));
                radius = await GetDist(LanguageHelper.Tr("Radius"), centerPoint);
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
                height = await GetDist(LanguageHelper.Tr("Height"), centerPoint, -1, null, null, secPlane);
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