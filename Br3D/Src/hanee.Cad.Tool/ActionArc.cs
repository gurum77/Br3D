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
        public enum Method
        {
            firstSecondThird,
            centerStartEnd
        }

        Method method;
        Point3D pt1, pt2, pt3;
        public ActionArc(Environment environment, Method method) : base(environment)
        {
            this.method = method;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            if (method == Method.firstSecondThird)
            {

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
            }
            else if (method == Method.centerStartEnd)
            {

                while (true)
                {
                    pt1 = await GetPoint3D(LanguageHelper.Tr("Center point"));
                    if (IsCanceled())
                        break;
                    pt2 = await GetPoint3D(LanguageHelper.Tr("Start point"));
                    if (IsCanceled())
                        break;
                    pt3 = await GetPoint3D(LanguageHelper.Tr("End point"));
                    if (IsCanceled())
                        break;

                    var arc = MakeArc();
                    GetModel().Entities.Add(arc);

                    pt1 = null;
                    pt2 = null;
                    pt3 = null;
                }
            }
            EndAction();
            return true;
        }

        private Arc MakeArc()
        {
            if (pt1 == null || pt2 == null || pt3 == null)
                return null;

            if (pt1.Equals(pt2))
                return null;
            if (pt1.Equals(pt3))
                return null;
            if (pt2.Equals(pt3))
                return null;

            Arc arc = null;
            if (method == Method.firstSecondThird)
                arc = new Arc(pt1, pt2, pt3, false);
            else if (method == Method.centerStartEnd)
                arc = new Arc(pt1, pt2, pt3);

            arc.Color = System.Drawing.Color.Yellow;
            arc.ColorMethod = colorMethodType.byEntity;
            return arc;
        }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (pt1 == null)
                return;

            if (pt2 != null)
                pt3 = point3D;

            environment.TempEntities.Clear();

            if (pt2 == null)
            {
                var line = new Line(pt1.Clone() as Point3D, Point3D.Clone() as Point3D);
                line.Color = System.Drawing.Color.Yellow;
                line.ColorMethod = colorMethodType.byEntity;
                line.Regen(0.001);
                environment.TempEntities.Add(line);
            }
            else
            {
                var arc = MakeArc();
                if (arc == null)
                    return;

                arc.Regen(0.001);
                environment.TempEntities.Add(arc);
            }
            environment.Invalidate();
        }
    }
}
