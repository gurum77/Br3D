﻿using devDept.Eyeshot;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionScale : ActionBase
    {
        protected Point3D fromPoint = null;
        protected KeyValuePair<Point3D, string> toPoint = new KeyValuePair<Point3D, string>();
        protected Point3D lastPoint = null;
        double baseLength = 1;
        double lastFactor = 0;

        double GetFactor()
        {
            // 입력한 factor가 있다면 그걸 리턴한다.
            if(toPoint.Key == null && !string.IsNullOrEmpty(toPoint.Value) && double.TryParse(toPoint.Value, out double factor))
            {
                return factor;
            }

            if (lastPoint == null)
                return 0;
            
            var vec = point3D - fromPoint;
            factor = vec.AsVector.Length / baseLength;
            return factor;
        }

        public ActionScale(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                environment.TempEntities.Clear();
                fromPoint = null;
                toPoint = new KeyValuePair<Point3D, string>();
                lastPoint = null;

                var entities = await GetEntities(LanguageHelper.Tr("Select entities"));
                if (IsCanceled())
                    break;

                // temp entities로 설정
                entities.ToTempEntities(GetModel());
                CalcBaseLength();

                var pk = await GetPoint3DOrText(LanguageHelper.Tr("From point(R : Reference)"), -1, "r");
                if (IsCanceled())
                    break;

                // 참조인 경우
                if(pk.Key == null)
                {
                    var refPt1 = await GetPoint3D(LanguageHelper.Tr("From reference point"));
                    if (IsCanceled())
                        break;
                    var refPt2 = await GetPoint3D(LanguageHelper.Tr("From reference point"));
                    if (IsCanceled())
                        break;

                    baseLength = refPt1.DistanceTo(refPt2);

                    // 다시 from point 입력
                    fromPoint = await GetPoint3D(LanguageHelper.Tr("From point"));
                    if (IsCanceled())
                        break;
                }
                else
                {
                    fromPoint = pk.Key;
                }

                if (fromPoint == null)
                    break;
                fromPoint = fromPoint.Clone() as Point3D;
                lastPoint = fromPoint.Clone() as Point3D;

                // 배율
                toPoint = await GetPoint3DOrText(LanguageHelper.Tr("To point"));
                if (IsCanceled())   
                    break;

                Finish(entities);

                break;
            }

            EndAction();
            return true;
        }

        private void CalcBaseLength()
        {
            // 객체를 선택하면 baseLength을 계산한다.
            Point3D boxMin = null, boxMax = null;
            foreach (var ent in environment.TempEntities)
            {
                if (ent.BoxMin == null || ent.BoxMax == null)
                    ent.Regen(0.001);
                if (ent.BoxMin == null || ent.BoxMax == null)
                    continue;

                    if (boxMin == null)
                    boxMin = ent.BoxMin.Clone() as Point3D;
                else
                {
                    boxMin.X = Math.Min(boxMin.X, ent.BoxMin.X);
                    boxMin.Y = Math.Min(boxMin.Y, ent.BoxMin.Y);
                    boxMin.Z = Math.Min(boxMin.Z, ent.BoxMin.Z);
                }


                if (boxMax == null)
                    boxMax = ent.BoxMax.Clone() as Point3D;
                else
                {
                    boxMax.X = Math.Max(boxMax.X, ent.BoxMax.X);
                    boxMax.Y = Math.Max(boxMax.Y, ent.BoxMax.Y);
                    boxMax.Z = Math.Max(boxMax.Z, ent.BoxMax.Z);
                }
            }

            if (boxMin != null && boxMax != null)
            {
                baseLength = boxMax.X - boxMin.X;
                baseLength = Math.Max(baseLength, boxMax.Y - boxMin.Y);
                baseLength = Math.Max(baseLength, boxMax.Z - boxMin.Z);
            }
            baseLength /= 2;

        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            if (fromPoint == null)
                return;

            var regenParams = new RegenParams(0.001, environment);

            var factor = GetFactor();
            if (factor != 0)
            {
                foreach (var ent in environment.TempEntities)
                {
                    if (lastFactor != 0)
                    {
                        ent.Scale(fromPoint, 1.0 / lastFactor);
                    }
                    ent.Scale(fromPoint, factor);
                    ent.Regen(regenParams);
                }

                ActionBase.cursorText = $"{LanguageHelper.Tr("Scale")} : {factor}";
                lastFactor = factor;
            }

            lastPoint = point3D.Clone() as Point3D;

            PreviewLabel.PreviewDistanceLabel(model, fromPoint, lastPoint, 0, true, "D=");
        }

        protected void Finish(List<devDept.Eyeshot.Entities.Entity> entities)
        {
            var factor = GetFactor();
            if (factor == 0)
                return;

            var trans = new Scaling(fromPoint, factor);
            TransformEntities(trans, entities.ToArray());
        }
    }
}
