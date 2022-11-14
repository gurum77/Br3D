using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionFillet : ActionBase
    {
        public bool chamfer { get; set; } = false;
        List<Entity> filletCurves = new List<Entity>();
        double radius = 1.0;
        public ActionFillet(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }


        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);


            var curve = GetClosestCurve();
            foreach (var ent in filletCurves)
            {
                if (ent == null)
                    continue;

                if (ent == curve)
                {
                    ent.Color = System.Drawing.Color.Red;
                    ent.ColorMethod = colorMethodType.byEntity;
                    ent.LineWeight = 2;
                    ent.LineWeightMethod = colorMethodType.byEntity;
                }
                else
                {
                    ent.Color = System.Drawing.Color.FromArgb(50, System.Drawing.Color.White);
                    ent.ColorMethod = colorMethodType.byEntity;
                    ent.LineWeight = 1;
                    ent.LineWeightMethod = colorMethodType.byEntity;
                }
            }
            environment.Invalidate();
        }

        private ICurve GetClosestCurve()
        {
            ICurve closestCurve = null;

            double minDist = 0;
            foreach (ICurve fc in filletCurves)
            {
                if (fc == null)
                    continue;

                fc.ClosestPointTo(point3D, out double t);
                var curDist = point3D.DistanceTo(fc.PointAt(t));
                if (closestCurve == null || curDist < minDist)
                {
                    closestCurve = fc;
                    minDist = curDist;
                }
            }

            return closestCurve;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var entityTypes = new Dictionary<Type, bool>();
            entityTypes.Add(typeof(Line), true);
            entityTypes.Add(typeof(LinearPath), true);
            entityTypes.Add(typeof(Arc), true);


            var regenOptions = new RegenOptions();
            while (true)
            {
                environment.TempEntities.Clear();
                filletCurves.Clear();
                Entity firstEntity = null;

                // 첫번째 객체 선택 / 옵션
                // 객체를 선택할때까지 반복
                while (true)
                {
                    var firstEntityOrKey = await GetEntityOrText(LanguageHelper.Tr("Select first curve") + $"({LanguageHelper.Tr("Radius")} : {radius}, R : {LanguageHelper.Tr("Input radius")})", -1, true, entityTypes, "r");
                    if (IsCanceled() || IsEntered())
                        break;

                    // 반지름 입력
                    if (firstEntityOrKey.Key == null && firstEntityOrKey.Value.EqualsIgnoreCase("R"))
                    {
                        radius = await GetDist(LanguageHelper.Tr("Radius"), null, -1, null, null, null, true);
                        if (IsCanceled() || IsEntered())
                            break;
                    }
                    else
                    {
                        firstEntity = firstEntityOrKey.Key;
                    }

                    if (firstEntity != null)
                        break;
                }

                if (firstEntity == null)
                    break;

                var secondEntity = await GetEntity(LanguageHelper.Tr("Select second curve"), -1, true, entityTypes);
                if (IsCanceled())
                    break;

                if (firstEntity == secondEntity)
                {
                    MessageBox.Show(LanguageHelper.Tr("Select different entities"));
                    continue;
                }


                var firstCurve = firstEntity as ICurve;
                var secondCurve = secondEntity as ICurve;
                if (firstCurve == null || secondCurve == null)
                    continue;

                try
                {
                    if (chamfer)
                    {
                        Curve.Chamfer(firstCurve, secondCurve, radius, true, true, false, false, out Line filletArc1);
                        filletArc1?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc1);

                        Curve.Chamfer(firstCurve, secondCurve, radius, true, false, false, false, out Line filletArc2);
                        filletArc2?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc2);

                        Curve.Chamfer(firstCurve, secondCurve, radius, false, true, false, false, out Line filletArc3);
                        filletArc3?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc3);

                        Curve.Chamfer(firstCurve, secondCurve, radius, false, false, false, false, out Line filletArc4);
                        filletArc4?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc4);

                        environment.TempEntities.Clear();
                        filletCurves.ToTempEntities(environment, false);
                    }
                    else
                    {

                        Curve.Fillet(firstCurve, secondCurve, radius, true, true, false, false, out Arc filletArc1);
                        filletArc1?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc1);

                        Curve.Fillet(firstCurve, secondCurve, radius, true, false, false, false, out Arc filletArc2);
                        filletArc2?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc2);

                        Curve.Fillet(firstCurve, secondCurve, radius, false, true, false, false, out Arc filletArc3);
                        filletArc3?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc3);

                        Curve.Fillet(firstCurve, secondCurve, radius, false, false, false, false, out Arc filletArc4);
                        filletArc4?.CopyAttributes(firstEntity);
                        filletCurves.Add(filletArc4);

                        environment.TempEntities.Clear();
                        filletCurves.ToTempEntities(environment, false);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    continue;
                }

                // 선택
                var selectionPoint = await GetPoint3D(LanguageHelper.Tr("Select fillet arc"));
                if (IsCanceled())
                    break;

                var filletCurve = GetClosestCurve();
                if (filletCurve == null)
                    continue;

                var idx = filletCurves.FindIndex(x => x == filletCurve);
                var flip1 = idx == 0 || idx == 1 ? true : false;
                var flip2 = idx == 0 || idx == 2 ? true : false;

                try
                {
                    // circle은 trim 을 할수 없음.
                    bool trim = firstCurve.IsClosed || secondCurve.IsClosed ? false : true;
                    if (chamfer)
                    {
                        Curve.Chamfer(firstCurve, secondCurve, radius, flip1, flip2, trim, trim, out Line filletLine);
                        if (filletLine != null)
                        {
                            var entities = new EntityList();
                            entities.Add(firstEntity);
                            entities.Add(secondEntity);
                            entities.Add(filletLine);
                            entities.Regen(regenOptions);

                            environment.Entities.AddRange(entities);
                        }
                    }
                    else
                    {
                        Curve.Fillet(firstCurve, secondCurve, radius, flip1, flip2, trim, trim, out Arc filletArc);
                        if (filletArc != null)
                        {
                            var entities = new EntityList();
                            entities.Add(firstEntity);
                            entities.Add(secondEntity);
                            entities.Add(filletArc);
                            entities.Regen(regenOptions);

                            environment.Entities.AddRange(entities);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }


                break;
            }

            EndAction();
            return true;
        }
    }
}
