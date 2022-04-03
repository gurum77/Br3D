using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionCopy : ActionMove
    {
        public ActionCopy(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        override protected void Finish(List<Entity> entities, Vector3D vec)
        {
            var entityList = new EntityList();
            foreach (var ent in entities)
            {
                entityList.Add(ent.Clone() as Entity);
            }
            entityList.Translate(vec.X, vec.Y, vec.Z);
            GetModel().Entities.AddRange(entityList);
            GetModel().Entities.Regen();
            GetModel().Invalidate();
        }
    }
}
