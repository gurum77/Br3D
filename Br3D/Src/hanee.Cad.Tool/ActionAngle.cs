using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionAngle : ActionBase
    {
        Point3D ptCen, pt1, pt2;
        public ActionAngle(Model vp) : base(vp)
        {
            ptCen = null;
            pt1 = null;
            pt2 = null;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();
            while (true)
            {
                ptCen = null;
                pt1 = null;
                pt2 = null;

                ptCen = await GetPoint3D(LanguageHelper.Tr("Center point"));
                if (IsCanceled())
                    break;

                pt1 = await GetPoint3D(LanguageHelper.Tr("First point"));
                if (IsCanceled())
                    break;

                pt2 = await GetPoint3D(LanguageHelper.Tr("Second point"));
                if (IsCanceled())
                    break;

                Vector3D v1 = (pt1 - ptCen).AsVector;
                Vector3D v2 = (pt2 - ptCen).AsVector;
                v1.Normalize();
                v2.Normalize();
                Plane plane = new Plane(new Point3D(0, 0, 0), v1, v2);

                double angle = Utility.VectorsAngle(v1, v2, plane);

                List<string> results = new List<string>();
                results.Add($"{LanguageHelper.Tr("Angle")} = {Math.Abs(angle):0.000}º");
                results.Add($"{LanguageHelper.Tr("Center point")} = {ptCen.X:0.000}, {ptCen.Y:0.000}, {ptCen.Z:0.000}");
                results.Add($"{LanguageHelper.Tr("First point")} = {pt1.X:0.000}, {pt1.Y:0.000}, {pt1.Z:0.000}");
                results.Add($"{LanguageHelper.Tr("Second point")} = {pt2.X:0.000}, {pt2.Y:0.000}, {pt2.Z:0.000}");

                FormResult formResult = new FormResult();
                formResult.RichTextBox.Lines = results.ToArray();
                formResult.ShowDialog();

                break;
            }


            EndAction();
            return true;
        }


        protected override void OnMouseMove(devDept.Eyeshot.Environment vp, MouseEventArgs e)
        {
            EntityList entities = new EntityList();
            if (ptCen != null)
            {
                // 현재 그리는 선
                Line l = new Line(ptCen, point3D);
                entities.Add(l);
            }

            if (pt1 != null)
            {
                // line1
                Line l = new Line(ptCen, pt1);
                entities.Add(l);
            }

            ActionBase.PreviewEntities = entities.ToArray();
        }
    }
}
