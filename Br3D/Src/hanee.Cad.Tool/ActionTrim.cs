using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionTrim : ActionBase
    {
        public ActionTrim(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();


            var entityTypes = new Dictionary<Type, bool>();
            entityTypes.Add(typeof(Line), true);
            entityTypes.Add(typeof(LinearPath), true);
            entityTypes.Add(typeof(Arc), true);
            entityTypes.Add(typeof(Circle), true);
            entityTypes.Add(typeof(Curve), true);
            entityTypes.Add(typeof(Ellipse), true);
            entityTypes.Add(typeof(EllipticalArc), true);
            entityTypes.Add(typeof(CompositeCurve), true);

            // 기준 객체 선택
            var entities = await GetEntities(LanguageHelper.Tr("Select entities"), -1, false, entityTypes);
            if (IsCanceled())
            {
                EndAction();
                return true;
            }


            while (true)
            {
                // trim할 객체 선택
                var entityToTrim = await GetEntity(LanguageHelper.Tr("Select entity to trim"), -1, true, entityTypes);
                if (IsCanceled())
                    break;

                var curveToTrim = entityToTrim as ICurve;
                if (curveToTrim == null)
                    continue;

                // trim할 객체선택할때 클릭지점
                var trimPoint = ActionBase.GetPoint3DByMouseLocation(environment, ActionBase.currentMousePoint);
                curveToTrim.ClosestPointTo(trimPoint, out double trimParam);

                // 교점
                var matchParams = curveToTrim.IntersectWith(entities);
                if (matchParams == null)
                    continue;

                var newEntities = new List<Entity>();

                ICurve[] trimmedCurves = null;
                double u;
                curveToTrim.ClosestPointTo(trimPoint, out u);
                var distSelected = Point3D.Distance(trimPoint, curveToTrim.PointAt(u));
                distSelected += distSelected / 1e3;

                if (u <= matchParams[0])
                {
                    curveToTrim.SplitBy(new Point3D[] { curveToTrim.PointAt(matchParams[0]) }, out trimmedCurves);
                }
                else if (u > matchParams[matchParams.Count - 1])
                {
                    curveToTrim.SplitBy(new Point3D[] { curveToTrim.PointAt(matchParams[matchParams.Count - 1]) },
                        out trimmedCurves);
                }
                else
                {
                    for (int i = 0; i < matchParams.Count - 1; i++)
                    {
                        if (u > matchParams[i] && u <= matchParams[i + 1])
                        {
                            curveToTrim.SplitBy(
                                new Point3D[]
                                    {curveToTrim.PointAt(matchParams[i]), curveToTrim.PointAt(matchParams[i + 1])},
                                out trimmedCurves);
                        }
                    }
                }

                //Decide which portion of curve to be deleted
                for (int i = 0; i < trimmedCurves.Length; i++)
                {
                    ICurve trimmedCurve = trimmedCurves[i];
                    double t;

                    trimmedCurve.ClosestPointTo(trimPoint, out t);
                    {

                        if ((t < trimmedCurve.Domain.t0 || t > trimmedCurve.Domain.t1)
                            || Point3D.Distance(trimPoint, trimmedCurve.PointAt(t)) > distSelected)
                        {
                            newEntities.Add((Entity)trimmedCurve);
                        }
                    }
                }


                // 새로운 객체가 생겼으면 기존 객체 삭제하고 새로운 객체를 db에 추가
                if (newEntities.Count > 0)
                {
                    CreateTransaction();
                    AddEntities(newEntities.ToArray());
                    DeleteEntities(entityToTrim);
                    CommitTransation();
                }
            }


            EndAction();
            return true;

        }
    }
}
