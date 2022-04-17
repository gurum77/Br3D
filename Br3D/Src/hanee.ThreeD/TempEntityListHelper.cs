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

        static public void GetBoundary(this TempEntityList entities, out Point3D boxMin, out Point3D boxMax)
        {
            boxMin = null;
            boxMax = null;

            try
            {
                var regenParams = new RegenParams(0.001);
                foreach (Entity ent in entities)
                {
                    if (ent.BoxMin == null || ent.BoxMax == null)
                        ent.Regen(regenParams);
                    if (ent.BoxMin == null || ent.BoxMax == null)
                        continue;

                    if (boxMin == null || boxMax == null)
                    {
                        boxMin = ent.BoxMin.Clone() as Point3D;
                        boxMax = ent.BoxMax.Clone() as Point3D;
                    }
                    else
                    {
                        boxMin.X = Math.Min(boxMin.X, ent.BoxMin.X);
                        boxMin.Y = Math.Min(boxMin.Y, ent.BoxMin.Y);
                        boxMin.Z = Math.Min(boxMin.Z, ent.BoxMin.Z);

                        boxMax.X = Math.Max(boxMax.X, ent.BoxMax.X);
                        boxMax.Y = Math.Max(boxMax.Y, ent.BoxMax.Y);
                        boxMax.Z = Math.Max(boxMax.Z, ent.BoxMax.Z);
                    }
                }
                
            }
            catch (Exception e)
            {
                
            }
        }

        static public Point3D GetCenter(this TempEntityList entities)
        {
            entities.GetBoundary(out Point3D boxMin, out Point3D boxMax);
            if (boxMin == null || boxMax == null)
                return null;

            return (boxMin + boxMax) / 2;
        }

        static public Point3D GetLeftBottom(this TempEntityList entities)
        {
            entities.GetBoundary(out Point3D boxMin, out Point3D boxMax);
            return boxMin;
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
