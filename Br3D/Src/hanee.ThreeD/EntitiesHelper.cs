using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    static public class EntitiesHelper
    {
        // 선택한 객체를 temp entities로 설정
        static public void ToTempEntities(this List<Entity> entities, devDept.Eyeshot.Environment environment, bool clone=true)
        {
            var regenParams = new RegenParams(0.001, environment);

            foreach (var ent in entities)
            {
                if (ent == null)
                    continue;

                // block이면 explode해서 넣는다.
                if (ent is BlockReference)
                {
                    var br = ent as BlockReference;
                    var explodedEntities = br.Explode(environment.Blocks);
                    foreach (var ee in explodedEntities)
                    {
                        if (ee is BlockReference)
                            continue;

                        var tempEnt = !clone ? ee : ee.Clone() as Entity;
                        var color = tempEnt.Color;
                        if (tempEnt.ColorMethod == colorMethodType.byLayer)
                        {
                            var layer = environment.Layers[tempEnt.LayerName];
                            if (layer != null)
                                color = layer.Color;
                        }
                        tempEnt.Color = System.Drawing.Color.FromArgb(150, color);
                        tempEnt.Regen(regenParams);
                        environment.TempEntities.Add(tempEnt);
                    }
                }
                else
                {
                    var tempEnt = !clone ? ent : ent.Clone() as Entity;
                    var color = tempEnt.Color;
                    if (tempEnt.ColorMethod == colorMethodType.byLayer)
                    {
                        var layer = environment.Layers[tempEnt.LayerName];
                        if (layer != null)
                            color = layer.Color;
                    }
                    tempEnt.Color = System.Drawing.Color.FromArgb(150, color);
                    tempEnt.Regen(regenParams);
                    environment.TempEntities.Add(tempEnt);
                }
            }
        }
    }
}
