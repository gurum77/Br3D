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

            var line = MakeLine(true);
            if (line == null)
                return;

            environment.TempEntities.ReplaceEntityAndRegen(line);
            environment.Invalidate();
        }

        Line MakeLine(bool tempEntity=false)
        {
            Line line = new Line(startPoint.Clone() as Point3D, endPoint.Clone() as Point3D);
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(line, tempEntity);
            return line;
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            DisableHideDynamicInput();

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
                environment.Entities.RegenAllCurved();
                environment.Invalidate();

                startPoint = endPoint;
            }

            
            EndAction();
            return true;
        }
    }
}
