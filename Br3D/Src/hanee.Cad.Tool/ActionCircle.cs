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
            environment.TempEntities.Clear();

            var circle = MakeCircle();
            if (circle == null)
                return;

            circle.Regen(0.001);
            environment.TempEntities.Add(circle);
            environment.Invalidate();
        }

        Circle MakeCircle()
        {
            var radius = startPoint.DistanceTo(endPoint);
            if (radius <= 0)
                return null;

            Circle circle = new Circle(startPoint, radius);
            circle.Color = System.Drawing.Color.Yellow;
            circle.ColorMethod = colorMethodType.byEntity;
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
                endPoint = await GetPoint3D(LanguageHelper.Tr("Radius point"));
                if (IsCanceled() || IsEntered())
                    break;

                Circle circle = MakeCircle();
                environment.Entities.Add(circle);
                environment.TempEntities.Clear();
                environment.Invalidate();

                startPoint = null;
                endPoint = null;
            }

            EndAction();
            return true;
        }
    }
}
