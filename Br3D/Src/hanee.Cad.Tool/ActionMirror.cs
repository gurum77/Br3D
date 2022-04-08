using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionMirror : ActionBase
    {
        Point3D firstPoint;
        Mirror lastMirror;
        public ActionMirror(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var regenOptions = new RegenOptions();
            
            while (true)
            {
                // 객체 선택
                var entities = await GetEntities(LanguageHelper.Tr("Select entities"));
                if (IsCanceled())
                    break;

                entities.ToTempEntities(environment);

                // 첫번째 점
                firstPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
                if (IsCanceled())
                    break;

                orthoModeManager.startPoint = firstPoint;

                // 두번째 점
                var secondPoint = await GetPoint3D(LanguageHelper.Tr("Second point"));
                if (IsCanceled())
                    break;

                // 객체를 mirror한다.
                var mirror = GetMirror(firstPoint, secondPoint);
                if (mirror == null)
                    continue;
                foreach (var ent in entities)
                {
                    ent.TransformBy(mirror);
                }
                GetModel().Entities.Regen(regenOptions);
                GetModel().Invalidate();
                break;
            }

            EndAction();
            return true;
        }

        Mirror GetMirror(Point3D pt1, Point3D pt2)
        {
            var axisX = new Vector3D(pt2, pt1);
            var mirrorPlane = new Plane(pt1, axisX, Vector3D.AxisZ);
            var mirror = new Mirror(mirrorPlane);
            return mirror;
        }
        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);
            if (firstPoint == null)
                return;

            var mirror = GetMirror(firstPoint, point3D);
            if (mirror == null)
                return;

            lastMirror?.Invert();
            foreach (Entity ent in environment.TempEntities)
            {
                if (lastMirror != null)
                    ent.TransformBy(lastMirror);
                ent.TransformBy(mirror);
            }
            environment.TempEntities.RegenAfterModify();
            lastMirror = mirror;

            previewEntity = new Line(firstPoint, point3D);
        }
    }
}
