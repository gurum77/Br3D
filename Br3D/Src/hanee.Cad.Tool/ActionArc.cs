using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionArc : ActionBase
    {
        Point3D pt1, pt2, pt3;
        public ActionArc(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                pt1 = await GetPoint3D(LanguageHelper.Tr("First point"));
                if (IsCanceled())
                    break;
                pt2 = await GetPoint3D(LanguageHelper.Tr("Second point"));
                if (IsCanceled())
                    break;
                pt3 = await GetPoint3D(LanguageHelper.Tr("Third point"));
                if (IsCanceled())
                    break;

                var arc = MakeArc();
                GetModel().Entities.Add(arc);

                pt1 = null;
                pt2 = null;
                pt3 = null;
            }

            EndAction();
            return true;
        }

        private Arc MakeArc()
        {
            if (pt1 == null || pt2 == null || pt3 == null)
                return null;

            var arc = new Arc(pt1, pt2, pt3, false);
            arc.Color = System.Drawing.Color.Yellow;
            arc.ColorMethod = colorMethodType.byEntity;
            return arc;
        }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (pt1 == null || pt2 == null)
                return;

            pt3 = point3D;

            var arc = MakeArc();
            if (arc == null)
                return;

            arc.Regen(0.001);
            environment.TempEntities.Clear();
            environment.TempEntities.Add(arc);
            environment.Invalidate();
        }
    }
}
