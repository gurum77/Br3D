using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionBox : ActionBase
    {
        protected Point3D basePoint;
        protected double? width;
        protected double? height;
        protected double? length;
        public ActionBox(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                basePoint = await GetPoint3D(LanguageHelper.Tr("Base point"));
                if (IsCanceled() || IsEntered())
                    break;

                SetOrthoModeStartPoint(basePoint);
                SetAutoWorkspace(basePoint);
                var wp = GetWorkplane();

                
                width = await GetDist(LanguageHelper.Tr("Width"), basePoint);

                SetOrthoModeStartPoint(basePoint + wp.AxisX * width.Value);
                if (IsCanceled() || IsEntered())
                    break;

                
                height = await GetDist(LanguageHelper.Tr("Height"), basePoint);
                SetOrthoModeStartPoint(basePoint + wp.AxisY * height.Value); 
                if (IsCanceled() || IsEntered())
                    break;

                
                length = await GetDist(LanguageHelper.Tr("Length"), basePoint);
                SetOrthoModeStartPoint(null);
                if (IsCanceled() || IsEntered())
                    break;

                var box = Make3D(false);
                if (box == null)
                    break;

                AddEntity(box);
                
                basePoint = null;
                width = null;
                height = null;
                GetModel().TempEntities.Clear();
                GetModel().Entities.Regen();
                GetModel().Invalidate();
                break;
            }

            EndAction();
            return true;
        }

        double GetWidth(Plane plane, Point3D secondPoint)
        {
            if (basePoint == null || secondPoint == null)
                return 0;

            var widthPoint = secondPoint.IntersectionWith(plane, plane.AxisY, basePoint, plane.AxisX);
            if (widthPoint == null)
                return 0;

            return widthPoint.DistanceTo(basePoint);
        }

        double GetDepth(Plane plane, Point3D secondPoint)
        {
            if (basePoint == null || point3D == null)
                return 0;

            var depthPoint = secondPoint.IntersectionWith(plane, plane.AxisX, basePoint, plane.AxisY);
            if (depthPoint == null)
                return 0;

            return depthPoint.DistanceTo(basePoint);
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            // 
            base.OnMouseMove(environment, e);


            // 단면
            //if(width == null || height == null)
            {
                var sec = MakeSection() as Region;
                if (sec == null || sec.ContourList.Count == 0)
                    return;

                SetTempEtt(environment, sec);

                var lp = sec.ContourList[0] as CompositeCurve;
                if (lp == null || lp.CurveList.Count < 4)
                    return;

                var l1 = lp.CurveList[0] as Line;
                var l2 = lp.CurveList[1] as Line;
                if (l1 == null || l2 == null)
                    return;
                PreviewLabel.PreviewDistanceLabel(model, l1.StartPoint, l1.EndPoint, 1, false, "W=");
                PreviewLabel.PreviewDistanceLabel(model, l2.StartPoint, l2.EndPoint, 2, false, "H=");
            }
            //else
            //{
            //    var str = $"W={width:0.000},H={height:0.000}";
            //    PreviewLabel.PreviewDistanceLabel(model, basePoint, basePoint, 1, false, str);
            //    PreviewLabel.PreviewDistanceLabel(model, basePoint, basePoint, 2, false, str);
            //}
            
        }

     
        protected Entity MakeSection()
        {
            var plane = GetWorkplane();
            var curWidth = width == null ? GetWidth(plane, point3D) : width.Value;
            var curHeight =height == null ?  GetDepth(plane, point3D) : height.Value;
            if (curWidth == 0 || curHeight == 0)
                return null;

            var secPlane = plane.Clone() as Plane;
            secPlane.Origin = basePoint;
            var region = Region.CreateRectangle(secPlane, curWidth, curHeight, false);
            GetHModel().entityPropertiesManager.SetDefaultProperties(region, true);
            return region;
        }

        protected Entity Make3D(bool tempEntity)
        {
            if (basePoint == null || width == null || height == null || length == null)
                return null;

            if (width.Value == 0 || height.Value == 0 || length.Value == 0)
                return null;


            var reverseHeight = length < 0;
            length = Math.Abs(length.Value);

            Entity box = null;
            if (tempEntity)
                box = Mesh.CreateBox(width.Value, height.Value, length.Value);
            else
                box = Brep.CreateBox(width.Value, height.Value, length.Value);

            var wp = GetWorkplane();
            var leftBottom = basePoint;
            box.TransformBy(new Transformation(leftBottom, wp.AxisX, wp.AxisY, reverseHeight ? wp.AxisZ * -1 : wp.AxisZ));
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(box, tempEntity);

            return box;
        }
    }
}
