using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using GuiLabs.Undo;

namespace hanee.ThreeD
{
    static public class ActionManagerHelper
    {
        // 객체추가 action을 실행
        static public void AddEntity(this ActionManager mng, Model model, Entity ent)
        {
            var ac = new AddAction(model, ent);
            ActionBase.actionManager.RecordAction(ac);
        }

        static public void DeleteEntities(this ActionManager mng, Model model, params Entity[] entities)
        {
            var ac = new DeleteAction(model, entities);
            ActionBase.actionManager.RecordAction(ac);
        }

    }
}
