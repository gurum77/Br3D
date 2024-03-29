﻿using devDept.Eyeshot;
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
                var startPoint = await GetPoint3D(LanguageHelper.Tr("Start point for offset"));
                if (IsCanceled())
                    break;

                var endPoint = await GetPoint3D(LanguageHelper.Tr("End point for offset"));
                if (IsCanceled())
                    break;

                // 거리 계산
                var dist = endPoint.DistanceTo(startPoint);

                var entityType = new Dictionary<Type, bool> ();
                entityType.Add(typeof(Circle), true);
                entityType.Add(typeof(Arc), true);
                entityType.Add(typeof(Line), true);
                entityType.Add(typeof(LinearPath), true);
                
                while (true)
                {
                    // 객체 선택
                    var entity = await GetEntity(LanguageHelper.Tr("Select entity to offset"), -1, true, entityType);
                    if (IsCanceled())
                        break;

                    // 커브만 선택할수 있음.
                    var curve = entity as ICurve;
                    if (curve == null)
                        continue;

                    // offset 방향 
                    var offsetPoint = await GetPoint3D(LanguageHelper.Tr("Offset point"));
                    if (IsCanceled())
                        break;

                    // 선택한 객체 선택 해제
                    entity.Selected = false;

                    // offset 방향 정하기
                    curve.ClosestPointTo(offsetPoint, out double t);
                    
                    var dir2D = curve.TangentAt(t).To2D();
                    var offsetPoint2D = offsetPoint.To2D();
                    var curvePoint2D = curve.PointAt(t).To2D();

                    var cw  = UtilityEx.IsOrientedClockwise<Point2D>(new List<Point2D>() { offsetPoint2D, curvePoint2D, curvePoint2D+ dir2D });
                    var offsetCurve = curve.Offset(cw ? dist : -dist, GetWorkplane().AxisZ, 0.001, true);
                    if (offsetCurve == null)
                        continue;

                    var offsetEnt = offsetCurve as Entity;
                    if (offsetEnt == null)
                        continue;

                    AddEntities(offsetEnt);
                }

                break;
            }

            EndAction();

            return true;
        }
    }
}
