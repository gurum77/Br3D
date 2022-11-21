using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using GuiLabs.Undo;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    class DeleteAction : AbstractAction
    {
        Entity[] entities;
        Model model;
        public DeleteAction(Model model, params Entity [] entities)
        {
            this.model = model;
            this.entities = entities;
        }

        protected override void ExecuteCore()
        {
            if (entities == null)
            {
                return;
            }

            foreach (var ent in entities)
            {
                model.Entities.Remove(ent);
                model.Invalidate();

            }
        }

        protected override void UnExecuteCore()
        {
            if (entities == null)
                return;
            model.Entities.AddRange(entities);
            model.Invalidate();
        }
    }
}
