using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.Geometry;
using System;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    static public class EntitiesHelper
    {
        static void SetTempEntityColor(Entity entity, devDept.Eyeshot.Environment environment)
        {
            var options = Options.Instance;
            if (options.tempEntityColorMethod == Options.TempEntityColorMethod.byTransparencyColor)
            {
                var color = entity.Color;
                if (entity.ColorMethod == colorMethodType.byLayer)
                {
                    var layer = environment.Layers[entity.LayerName];
                    if (layer != null)
                        color = layer.Color;
                }
                entity.Color = System.Drawing.Color.FromArgb(150, color);
            }
            else
            {
                entity.Color = options.tempEntityColor;
            }
            entity.ColorMethod = colorMethodType.byEntity;

        }

        // 선택한 객체를 temp entities로 설정
        static public void ToTempEntities(this List<Entity> entities, devDept.Eyeshot.Environment environment, bool clone = true)
        {
            try
            {

                foreach (var ent in entities)
                {
                    if (ent == null)
                        continue;

                    AddEntityToTempEntities(ent, environment, clone);
                }

                environment.TempEntities.RegenAfterModify();
            }
            catch (Exception e)
            {
                
            }
        }

        private static void AddEntityToTempEntities(Entity ent, devDept.Eyeshot.Environment environment, bool clone)
        {
            // block이면 explode해서 넣는다.
            if (ent is BlockReference)
            {
                var br = ent as BlockReference;
                var explodedEntities = br.Explode(environment.Blocks);
                foreach (var ee in explodedEntities)
                {
                    if (ee is BlockReference)
                        continue;

                    AddEntityToTempEntities(ee, environment, clone);
                }
            }
            else if (ent is Text)
            {
                var text = ent as Text;
                text.UpdateOrientedBoundingBox(new TraversalParams());
                var lp = text.OrientedBounding.GetBoundingLinearPath(text.InsertionPoint.Z);
                if (lp != null)
                    AddEntityToTempEntities(lp, environment, false);
            }
            else
            {
                var tempEnt = !clone ? ent : ent.Clone() as Entity;
                SetTempEntityColor(tempEnt, environment);
                environment.TempEntities.Add(tempEnt);
            }
        }
    }
}
