using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    static public class TempEntityListHelper
    {
        // temp entity list에 같은 객체가 있다면 교체를 한다.
        // 없으면 추가한다.
        static public void ReplaceEntitiesAndRegen(this TempEntityList entities, params Entity[] newEntities)
        {
            var exceptEntities = new Dictionary<Entity, bool>();
            for (int i = 0; i < newEntities.Length; ++i)
            {
                var newEntity = newEntities[i];
                
                foreach (var ent in entities)
                {
                    if (ent.GetType() == newEntity.GetType())
                    {
                        // 이미 변경한 객체는 통과
                        if (exceptEntities.ContainsKey(ent))
                            continue;

                        ent.CopyFrom(newEntity);
                        exceptEntities.Add(ent, true);
                    }
                }

                // 없으면 추가한다.
                entities.Add(newEntity);
            }

            entities.RegenAfterModify();
        }

        // temp entity list에 같은 객체가 있다면 교체를 한다.
        // 없으면 추가한다.
        // 변경된 entity를 리턴
        static public Entity ReplaceEntityAndRegen(this TempEntityList entities, Entity newEntity)
        {
            foreach (var ent in entities)
            {
                if (ent.GetType() == newEntity.GetType())
                {
                    ent.CopyFrom(newEntity);
                    entities.RegenAfterModify();
                    return ent;
                }
            }

            // 없으면 추가한다.
            entities.Add(newEntity);
            entities.RegenAfterModify();
            return newEntity;
        }

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
            catch
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
                if (ent is ICurve || ent is Mesh)
                    ent.Regen(0.001);
            }
        }
    }
}
