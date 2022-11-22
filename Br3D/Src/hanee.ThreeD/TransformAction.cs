using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using GuiLabs.Undo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    class TransformAction : AbstractAction
    {
        Entity[] entities;
        Transformation trans;
        Model model;
        public TransformAction(Model model, Transformation trans, params Entity[] entities)
        {
            this.model = model;
            this.trans = trans;
            this.entities = entities;
        }
        protected override void ExecuteCore()
        {
            if (trans == null || entities == null)
                return;

            foreach (var ent in entities)
                ent.TransformBy(trans);
            var ro = new RegenOptions();
            model.Entities.Regen(ro);
            model.Invalidate();
        }

        protected override void UnExecuteCore()
        {
            if (trans == null || entities == null)
                return;

            var invertTrans = trans.Clone() as Transformation;
            invertTrans.Invert();
            foreach (var ent in entities)
                ent.TransformBy(invertTrans);

            var ro = new RegenOptions();
            model.Entities.Regen(ro);
            model.Invalidate();
        }
    }
}
