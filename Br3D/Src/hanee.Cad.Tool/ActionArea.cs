using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static devDept.Eyeshot.Environment;

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
            face,
            points
        }

        List<Point3D> points = null;
        List<SelectedFace> faces = null;
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


            var entOrKey = await GetEntityOrKey(LanguageHelper.Tr("Select entity(F : Select face, P : Pick points)"));
            if (IsCanceled() || IsEntered())
            {
                EndAction();
                return true;
            }


            // select face  방식인지?
            if (entOrKey.Value != null && entOrKey.Value.KeyCode == Keys.F)
            {
                environment.TempEntities.Clear();
                faces = new List<SelectedFace>();
                method = Method.face;
                while (true)
                {
                    var face = await GetFace(LanguageHelper.Tr("Select face(Enter : Finish)"));
                    if (face == null || IsCanceled() || IsEntered())
                        break;

                    faces.Add(face);

                    var tmpEntities = face.ToTempEntity(environment);
                    if (tmpEntities != null)
                    {
                        tmpEntities.ForEach(x =>
                        {
                            x.LineWeight = 2;
                            x.LineWeightMethod = colorMethodType.byEntity;
                            x.Color = System.Drawing.Color.Red;
                            x.ColorMethod = colorMethodType.byEntity;
                        });

                        environment.TempEntities.AddRange(tmpEntities);
                        environment.TempEntities.RegenAfterModify();
                    }
                }
            }
            // pick point 방식인지?
            else if (entOrKey.Value != null && entOrKey.Value.KeyCode == Keys.P)
            {
                method = Method.points;
                points = new List<Point3D>();
                while (true)
                {

                    var pt = await GetPoint3D(points.Count == 0 ? LanguageHelper.Tr("Pick first point") : LanguageHelper.Tr("Next point or Enter(Enter : Finish)"));
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
            else if (method == Method.face)
                ShowResultByFaces(faces);



            EndAction();
            return true;
        }

        void ShowResultByValues(double area, Point3D center)
        {
            List<string> results = new List<string>();
            if (center != null)
            {
                results.Add($"Area = {Math.Abs(area):0.000}");
                results.Add($"Centroid : X = {center.X:0.000}, Y = {center.Y:0.000}, Z = {center.Z:0.000}");
            }
            else
            {
                results.Add(LanguageHelper.Tr("Area cannot be measured!"));
            }


            FormResult formResult = new FormResult();
            formResult.RichTextBox.Lines = results.ToArray();
            formResult.ShowDialog();
        }

        // selected face의 면적 리턴
        double GetArea(SelectedFace face, out Point3D center)
        {
            if (face.Item is Brep brep)
            {
                // surface의 면적은 curve 1개로 이루어진 경우 curve 의 면적을 계산하는게 가장 정확하다.
                var entities = face.ToTempEntity(environment);
                if (entities != null && entities.Count == 1)
                {
                    return GetArea(entities[0], out center);
                }
                else
                {
                    center = new Point3D();

                    var bf = brep.Faces[face.Index];

                    // surface로 분해
                    brep.Rebuild();
                    var surfaces = bf.ConvertToSurface(brep);

                    var count = 0;
                    double totArea = 0;
                    foreach (var surface in surfaces)
                    {
                        surface.Regen(new devDept.Eyeshot.RegenParams(0.001));
                        var curArea = surface.GetArea(out Point3D curCenter);
                        if (curCenter == null)
                            continue;
                        count++;
                        totArea += curArea;
                        center = center + curCenter;
                    }
                    center = center / count;
                    return totArea;
                }


            }
            else
            {
                var tmpEntities = face.ToTempEntity(environment);

                var count = 0;
                double totArea = 0;
                center = new Point3D();

                foreach (var tmpEnt in tmpEntities)
                {
                    var curArea = GetArea(tmpEnt, out Point3D curCenter);
                    if (curCenter == null)
                        continue;
                    totArea += curArea;
                    center += curCenter;
                    count++;
                }
                if (count > 0)
                    center = center / count;

                return totArea;
            }
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
            else if (ent is LinearPath lp)
            {
                AreaProperties ap = new AreaProperties(lp.Vertices);
                center = ap.Centroid;
                return ap.Area;
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
                return totArea;
            }
            else if(ent is Circle c)
            {
                center = c.Center;
                return Math.PI * c.Radius * c.Radius;
            }

            center = null;
            return 0;
        }

        // faces의 면적을 표시
        void ShowResultByFaces(List<SelectedFace> faces)
        {
            int count = 0;
            double totArea = 0;
            Point3D totCenter = new Point3D();
            foreach (var face in faces)
            {
                var area = GetArea(face, out Point3D center);
                if (center == null)
                    continue;
                count++;
                totArea += area;
                totCenter += center;
            }
            if (count == 0)
                return;

            ShowResultByValues(totArea, totCenter / count);
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
                //ActionBase.previewEntity = null;
            }


        }
    }
}
