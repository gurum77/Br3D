using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    static public class EntitiesHelper
    {
        static public void Translate(this Entity[] entities, Vector3D vec)
        {
            foreach (Entity ent in entities)
                ent.Translate(vec);
        }

        static public void SetTempEntityColor(Entity entity, devDept.Eyeshot.Environment environment)
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
        static public void ToTempEntities(this Entity[] entities, devDept.Eyeshot.Environment environment, bool clone = true)
        {
            try
            {
                var regenParams = new RegenParams(0.001, environment);
                foreach (var ent in entities)
                {
                    if (ent == null)
                        continue;
                    if (ent.BoxMin == null || ent.BoxMax == null)
                        ent.Regen(regenParams);

                    AddEntityToTempEntities(ent, environment, clone);
                }

                environment.TempEntities.RegenAfterModify();
            }
            catch (Exception e)
            {

            }
        }

        // 선택한 객체를 temp entities로 설정
        static public void ToTempEntities(this List<Entity> entities, devDept.Eyeshot.Environment environment, bool clone = true)
        {
            try
            {
                var regenParams = new RegenParams(0.001, environment);
                foreach (var ent in entities)
                {
                    if (ent == null)
                        continue;
                    if (ent.BoxMin == null || ent.BoxMax == null)
                        ent.Regen(regenParams);

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
                if (text.Vertices == null || text.Vertices.Length == 0)
                    return;
                text.UpdateOrientedBoundingBox(new TraversalParams());
                var lp = text.OrientedBounding.GetBoundingLinearPath(text.InsertionPoint.Z);
                if (lp != null)
                    AddEntityToTempEntities(lp, environment, false);
            }
            else if (ent is ICurve || ent is Mesh)
            {
                if (ent is Mesh && (ent.Vertices == null || ent.Vertices.Length == 0))
                    return;
                var tempEnt = !clone ? ent : ent.Clone() as Entity;
                SetTempEntityColor(tempEnt, environment);
                environment.TempEntities.Add(tempEnt);
            }
            else
            {

            }
        }
    }
}
