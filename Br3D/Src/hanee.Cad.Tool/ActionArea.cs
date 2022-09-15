using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionArea : ActionBase
    {
        public enum ShowResult
        {
            label,
            form
        }

        public enum Method
        {
            entity,
            points
        }

        List<Point3D> points = null;
        Method method = Method.entity;
        ShowResult showResult;
        public ActionArea(devDept.Eyeshot.Model vp, ShowResult showResult = ShowResult.form) : base(vp)
        {
            this.showResult = showResult;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            
            Entity ent = null;
            var entOrKey = await GetEntityOrKey(LanguageHelper.Tr("Select entity[p : pick points"));
            if (IsCanceled() || IsEntered())
            {
                EndAction();
                return true;
            }


            // pick point 방식인지?
            if (entOrKey.Value != null && entOrKey.Value.KeyCode == Keys.P)
            {
                method = Method.points;
                points = new List<Point3D>();
                while (true)
                {

                    var pt = await GetPoint3D(points.Count == 0 ? LanguageHelper.Tr("Pick first point") : LanguageHelper.Tr("Next point or Enter"));
                    if (IsCanceled())
                        break;
                    if (IsEntered())
                        break;

                    points.Add(pt);
                }
            }
            else
            {
                method = Method.entity;
                ent = entOrKey.Key;
            }


            if (IsCanceled())
            {
                EndAction();
                return true;
            }

            // cancel이 아니면..
            if (method == Method.points)
                ShowResultByPoints(points);
            else if (method == Method.entity)
                ShowResultByEntity(ent);



            EndAction();
            return true;
        }

        void ShowResultByValues(double area, Point3D center)
        {
            List<string> results = new List<string>();
            results.Add($"Area = {Math.Abs(area):0.000}㎡");
            results.Add($"Centroid : X = {center.X:0.000}, Y = {center.Y:0.000}, Z = {center.Z:0.000}");

            FormResult formResult = new FormResult();
            formResult.RichTextBox.Lines = results.ToArray();
            formResult.ShowDialog();
        }

        double GetArea(Entity ent, out Point3D center)
        {
            if (ent is Brep brep)
            {
                return brep.GetArea(out center);
            }
            else if (ent is Mesh mesh)
            {
                AreaProperties ap = new AreaProperties(mesh.Vertices, mesh.Triangles);
                center = ap.Centroid;
                return ap.Area;
            }
            else if (ent is Surface sur)
            {
                return sur.GetArea(out center);
            }
            else if (ent is devDept.Eyeshot.Entities.Region re)
            {
                return re.GetArea(out center);

            }
            else if (ent is BlockReference br)
            {

                var entities = br.GetEntities(environment.Blocks);
                var totArea = 0.0;
                var totCenter = new Point3D();
                var availableCount = 0;
                
                foreach (var entity in entities)
                {
                    var curArea = GetArea(entity, out Point3D curCenter);
                    if (curCenter == null)
                        continue;

                    totArea += curArea;
                    totCenter += curCenter;
                    availableCount++;
                }

                if (availableCount > 0)
                    center = totCenter / availableCount;
                else
                    center = null;
            }
            center = null;
            return 0;
        }

        // entity 의 면적을 표시
        private void ShowResultByEntity(Entity ent)
        {
            var area = GetArea(ent, out Point3D center);
            ShowResultByValues(area, center);
        }



        // 입력한 좌표로 면적 표시
        private void ShowResultByPoints(List<Point3D> points)
        {
            AreaProperties ap = new AreaProperties(points);
            ShowResultByValues(ap.Area, ap.Centroid);
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment vp, MouseEventArgs e)
        {
            if (method == Method.points && points != null && points.Count > 0)
            {
                List<Point3D> tmp = new List<Point3D>();
                tmp.AddRange(points);
                tmp.Add(point3D);
                tmp.Add(points[0]);

                ActionBase.previewEntity = new LinearPath(tmp)
                {
                    Color = Color.White,
                    ColorMethod = colorMethodType.byEntity
                };
            }
            else
            {
                ActionBase.previewEntity = null;
            }

        }
    }
}
