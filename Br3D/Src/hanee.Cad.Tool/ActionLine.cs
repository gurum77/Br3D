using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionLine : ActionBase
    {
        Point3D startPoint, endPoint;
        public ActionLine(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (startPoint == null)
                return;

            endPoint = point3D;

            var line = MakeLine(true);
            if (line == null)
                return;

            PreviewLabel.PreviewDistanceLabel(model, startPoint, endPoint, 0);
            environment.TempEntities.ReplaceEntityAndRegen(line);
            environment.Invalidate();
        }

        Line MakeLine(bool tempEntity = false)
        {
            if (startPoint == null || endPoint == null)
                return null;

            Line line = new Line(startPoint.Clone() as Point3D, endPoint.Clone() as Point3D);
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(line, tempEntity);
            return line;
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            DisableHideDynamicInput();

            Point3D firstPoint = null;

            startPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
            firstPoint = startPoint;
            SetOrthoModeStartPoint(startPoint);

            int lineCount = 0;  // 지금까지 그린 선의 갯수
            while (!IsCanceled() && !IsEntered())
            {
                KeyValuePair<Point3D, string> pk;
                if (lineCount < 2)
                    pk = await GetPoint3DOrText(LanguageHelper.Tr("Next point"));
                else
                    pk = await GetPoint3DOrText(LanguageHelper.Tr("Next point(C : Close)"), -1, "c");
                if (IsCanceled() || IsEntered())
                    break;

                if (pk.Value != null && pk.Value.EqualsIgnoreCase("c"))
                {
                    // C를 누르면 firstPoint와 연결한 선을 만들고 입력 종료
                    endPoint = firstPoint;
                    Entered = true;
                }
                else if (pk.Value == null)
                {
                    endPoint = pk.Key;
                }

                SetAutoWorkspace();
                SetOrthoModeStartPoint(endPoint);
                Line line = MakeLine();
                if (line != null)
                {
                    environment.Entities.Add(line);
                    environment.Entities.RegenAllCurved();
                    environment.Invalidate();

                    lineCount++;

                    startPoint = endPoint;
                }

                if (IsEntered())
                    break;
            }


            EndAction();
            return true;
        }
    }
}
