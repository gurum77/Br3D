using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionVolume : ActionBase
    {
        public enum ShowResult
        {
            label,
            form
        }

        ShowResult showResult;

        public ActionVolume(devDept.Eyeshot.Model vp, ShowResult showResult = ShowResult.form) : base(vp)
        {
            this.showResult = showResult;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var ent = await GetEntity(LanguageHelper.Tr("Select entity"));
            if (IsCanceled() || IsEntered())
            {
                EndAction();
                return true;
            }

            ShowResultByEntity(ent);


            EndAction();
            return true;
        }


        // entity 의 면적을 표시
        private void ShowResultByEntity(Entity ent)
        {
            var vol = GetVolume(ent, out Point3D center);
            ShowResultByValues(vol, center);
        }


        // entity의 volume 리턴 
        double GetVolume(Entity ent, out Point3D center)
        {
            if (ent is Brep brep)
            {
                return brep.GetVolume(out center);
            }
            else if (ent is Mesh mesh)
            {
                return mesh.GetVolume(out center);
            }
            else if (ent is BlockReference br)
            {
                var entities = br.GetEntities(environment.Blocks);
                var totVolume = 0.0;
                var totCenter = new Point3D();
                var availableCount = 0;
                var trans = br.GetFullTransformation(environment.Blocks);
                foreach (var entity in entities)
                {
                    entity.TransformBy(trans);
                }

                foreach (var entity in entities)
                {

                    var curArea = GetVolume(entity, out Point3D curCenter);
                    if (curCenter == null)
                        continue;

                    totVolume += curArea;
                    totCenter += curCenter;
                    availableCount++;
                }

                if (availableCount > 0)
                    center = totCenter / availableCount;
                else
                    center = null;
                return totVolume;

            }
            center = null;
            return 0;
        }

        void ShowResultByValues(double vol, Point3D center)
        {
            List<string> results = new List<string>();
            if (center != null)
            {
                results.Add($"Volume = {Math.Abs(vol):0.000}");
                results.Add($"Centroid : X = {center.X:0.000}, Y = {center.Y:0.000}, Z = {center.Z:0.000}");
            }
            else
            {
                results.Add(LanguageHelper.Tr("Volume cannot be measured!"));
            }


            FormResult formResult = new FormResult();
            formResult.RichTextBox.Lines = results.ToArray();
            formResult.ShowDialog();
        }
    }
}
