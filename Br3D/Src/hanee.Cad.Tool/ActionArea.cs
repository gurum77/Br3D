using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionArea : ActionBase
    {
        List<Point3D> points;
        public ActionArea(devDept.Eyeshot.Model vp) : base(vp)
        {
            points = null;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();
            points = new List<Point3D>();
            while (true)
            {
                var pt = await GetPoint3D(points.Count == 0 ? "Pick first point" : "Next point or Enter");
                if (IsCanceled())
                    break;
                if (IsEntered())
                    break;

                points.Add(pt);
            }

            if(IsCanceled())
            {
                EndAction();
                return true;
            }

            // cancel이 아니면..
            devDept.Eyeshot.Entities.Region region = new devDept.Eyeshot.Entities.Region(new LinearPath(points));
            region.Regen(null);
            var centroid = new Point3D();
            double area = region.GetArea(out centroid);
            double perimeter = region.GetPerimeter();
            List<string> results = new List<string>();
            results.Add($"Area = {Math.Abs(area):0.000}㎡");
            results.Add($"Perimeter = {perimeter:0.000}m");
            results.Add($"Centroid : X = {centroid.X:0.000}, Y = {centroid.Y:0.000}, Z = {centroid.Z:0.000}");
            
            FormResult formResult = new FormResult();
            formResult.RichTextBox.Lines = results.ToArray();
            formResult.ShowDialog();

            EndAction();
            return true;
        }


        protected override void OnMouseMove(devDept.Eyeshot.Environment vp, MouseEventArgs e)
        {
            if(points != null && points.Count > 0)
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
