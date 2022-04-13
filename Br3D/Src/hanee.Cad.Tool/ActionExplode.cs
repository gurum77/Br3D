using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionExplode : ActionBase
    {
        public ActionExplode(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }


        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var regenOptions = new RegenOptions();
            while (true)
            {
                var entities = await GetEntities(LanguageHelper.Tr("Select entities(Enter : explode)"));
                if (IsCanceled())
                    break;

                var explodedEntities = new EntityList();
                foreach (var ent in entities)
                {
                    ICurve curve = ent as ICurve;
                    if (curve != null && (ent is LinearPath || ent is CompositeCurve))
                    {

                        var dividedCurves = curve.GetIndividualCurves();
                        if (dividedCurves.Length == 1)
                            continue;

                        foreach (var dc in dividedCurves)
                        {
                            var de = dc as Entity;
                            if (de != null)
                            {
                                de.CopyAttributesFast(ent);
                                explodedEntities.Add(de);
                            }
                        }
                        continue;
                    }

                    BlockReference br = ent as BlockReference;
                    if (br != null)
                    {
                        var tmp = br.Explode(environment.Blocks, true, false, environment);
                        if (tmp == null)
                            continue;

                        explodedEntities.AddRange(tmp);
                        continue;
                    }
                }


                GetModel().Entities.DeleteSelected();

                explodedEntities.Regen(regenOptions);
                GetModel().Entities.AddRange(explodedEntities);
                
                GetModel().Invalidate();

                break;
            }

            EndAction();
            return true;
        }
    }
}
