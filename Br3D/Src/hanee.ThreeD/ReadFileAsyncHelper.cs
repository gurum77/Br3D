using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using hanee.Geometry;
using System;
using Environment = devDept.Eyeshot.Environment;

namespace hanee.ThreeD
{
    static public class ReadFileAsyncHelper
    {
        static public void ToTempEntities(this ReadFileAsync rf, Environment environment, bool clone)
        {
            try
            {

                foreach (var ent in rf.Entities)
                {
                    if (ent == null)
                        continue;

                    AddEntityToTempEntities(ent, rf, environment, clone);
                }

                environment.TempEntities.RegenAfterModify();
            }
            catch (Exception e)
            {

            }
        }


        private static void AddEntityToTempEntities(Entity ent, ReadFileAsync rf, devDept.Eyeshot.Environment environment, bool clone)
        {
            BlockKeyedCollection blocks = null;
            var rfa = rf as ReadAutodesk;
            if (rfa != null)
                blocks = rfa.Blocks;

            // block이면 explode해서 넣는다.
            if (ent is BlockReference)
            {
                if (blocks == null)
                    return;
                var br = ent as BlockReference;
                var explodedEntities = br.Explode(blocks);
                foreach (var ee in explodedEntities)
                {
                    if (ee is BlockReference)
                        continue;

                    AddEntityToTempEntities(ee, rf, environment, clone);
                }
            }
            else if (ent is Text)
            {
                var text = ent as Text;
                text.UpdateOrientedBoundingBox(new TraversalParams());
                var lp = text.OrientedBounding.GetBoundingLinearPath(text.InsertionPoint.Z);
                if (lp != null)
                    AddEntityToTempEntities(lp, rf, environment, false);
            }
            else
            {
                var tempEnt = !clone ? ent : ent.Clone() as Entity;
                EntitiesHelper.SetTempEntityColor(tempEnt, environment);
                environment.TempEntities.Add(tempEnt);
            }
        }
    }
}
