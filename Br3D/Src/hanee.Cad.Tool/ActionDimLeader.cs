using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionDimLeader : ActionBase
    {
        List<Point3D> points = new List<Point3D>();
        public ActionDimLeader(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);
            if (points == null || points.Count == 0)
                return;

            var newPoints = new List<Point3D>();
            newPoints.AddRange(points);
            newPoints.Add(Point3D);
            var lp = new LinearPath(newPoints);
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(lp, true);
            environment.TempEntities.Clear();
            environment.TempEntities.Add(lp);
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            while(true)
            {
                points.Clear();  
                while (true)
                {
                    var pt = await GetPoint3D(LanguageHelper.Tr("Point(Enter : Finish)"));
                    if (pt == null)
                        break;
                    
                    if (IsCanceled())
                    {
                        points.Clear();
                        break;
                    }
                    if (IsEntered())
                        break;
                    
                    points.Add(pt);
                }

                if (points.Count < 2)
                    break;

                var txt = $"{points[0].ToString()}";
                FormInputMessage form = new FormInputMessage();
                form.RichTextBox.Text = txt;
                form.RichTextBox.SelectAll();
                if (form.ShowDialog() == DialogResult.Cancel)
                    break;

                var plane = GetWorkplane();
                var leader = new Leader(plane, points);
                leader.ArrowheadSize = Define.DefaultTextHeight;
                environment.Entities.Add(leader);

                var insPoint = points[points.Count - 1];
                var text = ActionText.MakeText(insPoint,  insPoint + plane.AxisX, leader.ArrowheadSize, form.RichTextBox, plane);
                environment.Entities.Add(text);

                environment.TempEntities.Clear();
                environment.Invalidate();
            }

            EndAction();
            return true;
        }
    }
}
