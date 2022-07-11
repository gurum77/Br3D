using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionCircle : ActionBase
    {
        Point3D startPoint, endPoint;
        public ActionCircle(Environment environment) : base(environment)
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
            

            var circle = MakeCircle(true);
            if (circle == null)
                return;
            environment.TempEntities.ReplaceEntityAndRegen(circle);
        }

        Circle MakeCircle(bool tempEntity=false)
        {
            var radius = startPoint.DistanceTo(endPoint);
            if (radius <= 0.001)
                return null;

            Circle circle = new Circle(GetWorkplane(), startPoint, radius);
            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(circle, tempEntity);
            
            return circle;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (!IsCanceled())
            {
                startPoint = await GetPoint3D(LanguageHelper.Tr("Center point"));
                if (IsCanceled() || IsEntered())
                    break;
                SetOrthoModeStartPoint(startPoint);

                DynamicInputManager.ActiveLengthFactor(startPoint, 1, LanguageHelper.Tr("Radius"));
                endPoint = await GetPoint3D(LanguageHelper.Tr("Radius point"));
                if (IsCanceled() || IsEntered())
                    break;
                SetOrthoModeStartPoint(null);
                Circle circle = MakeCircle();
                if (circle != null)
                {
                    environment.Entities.Add(circle);
                    environment.TempEntities.Clear();
                    environment.Invalidate();

                }

                startPoint = null;
                endPoint = null;
            }

            EndAction();
            return true;
        }
    }
}
