using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionOffset : ActionBase
    {
        public ActionOffset(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();


            var regenParams = new RegenParams(0.001, environment);
            while (true)
            {
                // 간격띄우기 첫번째 점
                var startPoint = await GetPoint3D("Start point for distance");
                if (IsCanceled())
                    break;


                DynamicInputManager.ActiveLengthFactor(startPoint, 1, "Distance");
                var endPoint = await GetPoint3D("End point for distance");
                if (IsCanceled())
                    break;

                // 거리 계산
                var dist = endPoint.DistanceTo(startPoint);

                var entityType = new Dictionary<Type, bool> ();
                entityType.Add(typeof(ICurve), true);
                while (true)
                {
                    // 객체 선택
                    var entity = await GetEntity("Select entity to offset", -1, true, entityType);
                    if (IsCanceled())
                        break;

                    // 커브만 선택할수 있음.
                    var curve = entity as ICurve;
                    if (curve == null)
                        continue;

                    // offset 방향 지정
                    var offsetPoint = await GetPoint3D("Offset point");
                    if (IsCanceled())
                        break;

                    entity.Selected = false;
                    curve.ClosestPointTo(offsetPoint, out double t);
                    
                    var dir2D = curve.TangentAt(t).To2D();
                    var offsetPoint2D = offsetPoint.To2D();
                    var curvePoint2D = curve.PointAt(t).To2D();

                    var cw  = UtilityEx.IsOrientedClockwise<Point2D>(new List<Point2D>() { offsetPoint2D, curvePoint2D, curvePoint2D+ dir2D });
                    var offsetCurve = curve.Offset(cw ? dist : -dist, Vector3D.AxisZ, 0.001, true);
                    if (offsetCurve == null)
                        continue;

                    var offsetEnt = offsetCurve as Entity;
                    if (offsetEnt == null)
                        continue;

                    offsetEnt.Regen(regenParams);
                    GetModel().Entities.Add(offsetEnt);
                    GetModel().Invalidate();
                }

                break;
            }

            EndAction();

            return true;
        }
    }
}
