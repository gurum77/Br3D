using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;

namespace hanee.ThreeD
{
    static public class TempEntityListHelper
    {
        static public void Translate(this TempEntityList entities, Vector3D vec)
        {
            foreach (var ent in entities)
            {
                ent.Translate(vec);
            }
        }

        static public Point3D GetLeftBottom(this TempEntityList entities)
        {
            try
            {
                var regenParams = new RegenParams(0.001);
                Point3D leftBottom = null;
                foreach (Entity ent in entities)
                {
                    if (ent.BoxMin == null || ent.BoxMax == null)
                        ent.Regen(regenParams);
                    if (ent.BoxMin == null || ent.BoxMax == null)
                        continue;

                    if (leftBottom == null)
                    {
                        leftBottom = ent.BoxMin.Clone() as Point3D;
                    }
                    else
                    {
                        leftBottom.X = Math.Min(leftBottom.X, ent.BoxMin.X);
                        leftBottom.Y = Math.Min(leftBottom.Y, ent.BoxMin.Y);
                        leftBottom.Z = Math.Min(leftBottom.Z, ent.BoxMin.Z);
                    }
                }
                return leftBottom;
            }
            catch (Exception e)
            {
                return null;
            }


        }
        // template entity 수정후 regen
        // 필요한 객체만 하자.
        // 대부분은 안해도 된다.
        static public void RegenAfterModify(this TempEntityList entities)
        {
            foreach (Entity ent in entities)
            {
                if (ent is ICurve)
                    ent.Regen(0.001);
            }
        }
    }
}
