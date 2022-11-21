using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using GuiLabs.Undo;

namespace hanee.ThreeD
{
    class AddAction : AbstractAction
    {
        Entity entity;
        Model model;
        public AddAction(Model model, Entity entity)
        {
            this.model = model;
            this.entity = entity;
        }
        protected override void ExecuteCore()
        {
            model.Entities.Add(entity);
            model.Invalidate();
        }

        protected override void UnExecuteCore()
        {
            model.Entities.Remove(entity);
            model.Invalidate();
        }
    }
}
