﻿using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionPolyline : ActionBase
    {
        List<Point3D> points = new List<Point3D>();
        public ActionPolyline(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }


        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (points == null || points.Count < 1)
                return;

            var cutPoints = new List<Point3D>();
            cutPoints.AddRange(points);
            cutPoints.Add(point3D.Clone() as Point3D);

            var lp = MakePolyline(cutPoints);
            lp.Regen(0.001);
            GetModel().TempEntities.Clear();
            GetModel().TempEntities.Add(lp);
            GetModel().Invalidate();
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var pt = await GetPoint3D(LanguageHelper.Tr("Point"));
                if (IsCanceled())
                    break;

                SetOrthoModeStartPoint(pt);
                if (IsEntered())
                {
                    if (points.Count > 1)
                    {
                        var pline = MakePolyline(points);
                        if (pline != null)
                        {
                            GetModel().Entities.Add(pline);
                        }
                    }
                    break;
                }
                else
                {
                    points.Add(pt);
                }
            }

            EndAction();
            return true;
        }

        LinearPath MakePolyline(List<Point3D> curPoints)
        {
            if (curPoints == null || curPoints.Count < 2)
                return null;

            var lp = new LinearPath(curPoints);
            lp.Color = System.Drawing.Color.Yellow;
            lp.ColorMethod = colorMethodType.byEntity;
            return lp;
        }
    }
}
