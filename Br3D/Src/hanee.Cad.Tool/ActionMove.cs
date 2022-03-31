using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionMove : ActionBase
    {
        Point3D fromPoint = null;
        Point3D toPoint = null;
        Point3D lastPoint = null;

        public ActionMove(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (fromPoint == null)
                return;

            var vec = point3D - lastPoint;
            foreach (var ent in environment.TempEntities)
            {
                ent.Translate(vec.X, vec.Y, vec.Z);
            }

            lastPoint = point3D.Clone() as Point3D;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                environment.TempEntities.Clear();
                fromPoint = null;
                toPoint = null;
                lastPoint = null;

                var entities = await GetEntities("Select entities");
                if (IsCanceled())
                    break;

                fromPoint = await GetPoint3D("From point");
                if (IsCanceled())
                    break;

                foreach (var ent in entities)
                {
                    // block이면 explode해서 넣는다.
                    if (ent is BlockReference)
                    {
                        var br = ent as BlockReference;
                        var explodedEntities = br.Explode(GetModel().Blocks);
                        foreach (var ee in explodedEntities)
                        {
                            if (ee is BlockReference)
                                continue;

                            var tempEnt = ee.Clone() as Entity;
                            tempEnt.Color = System.Drawing.Color.FromArgb(50, tempEnt.Color);
                            environment.TempEntities.Add(tempEnt);
                        }
                    }
                    else
                    {
                        var tempEnt = ent.Clone() as Entity;
                        tempEnt.Color = System.Drawing.Color.FromArgb(50, tempEnt.Color);
                        environment.TempEntities.Add(tempEnt);

                    }
                }

                lastPoint = fromPoint.Clone() as Point3D;

                toPoint = await GetPoint3D("To point");
                if (IsCanceled())
                    break;

                var vec = toPoint - fromPoint;
                var entityList = new EntityList();
                entityList.AddRange(entities);
                entityList.Translate(vec.X, vec.Y, vec.Z);
                GetModel().Entities.Regen();
                GetModel().Invalidate();
                break;
            }

            EndAction();
            return true;
        }
    }
}
