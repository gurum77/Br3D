using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionLine : ActionBase
    {
        Point3D startPoint, endPoint;
        public ActionLine(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (startPoint == null)
                return;

            endPoint = point3D;
            environment.TempEntities.Clear();

            var line = MakeLine();
            if (line == null)
                return;

            line.Regen(0.001);
            environment.TempEntities.Add(line);
            environment.Invalidate();
        }

        Line MakeLine()
        {
            Line line = new Line(startPoint.Clone() as Point3D, endPoint.Clone() as Point3D);
            line.Color = System.Drawing.Color.Yellow;
            line.ColorMethod = colorMethodType.byEntity;
            return line;
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            startPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
            SetOrthoModeStartPoint(startPoint);


            while (!IsCanceled() && !IsEntered())
            {
                endPoint = await GetPoint3D(LanguageHelper.Tr("Next point"));
                if (IsCanceled() || IsEntered())
                    break;

                SetOrthoModeStartPoint(endPoint);
                Line line = MakeLine();
                environment.Entities.Add(line);

                startPoint = endPoint;
            }

            EndAction();
            return true;
        }
    }
}
