using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System.Collections.Generic;
using System.Linq;

namespace hanee.ThreeD
{
    static public class ModelHelper
    {
        static public List<Entity> GetEntitiesInGroup(this Model model, int groupIndex)
        {
            return model.Entities.FindAll(ent => ent.GroupIndex == groupIndex);
        }

        // 모든 group index를 리턴
        static public List<int> GetAllGroupIndexes(this Model model)
        {
            var groupIndexes = new Dictionary<int, bool>();
            foreach(var ent in model.Entities)
            {
                if (ent.GroupIndex < 0)
                    continue;
                if (groupIndexes.ContainsKey(ent.GroupIndex))
                    continue;
                groupIndexes.Add(ent.GroupIndex, true);
            }

            return groupIndexes.Keys.ToList();
        }

        static public int GetLastGroupIndex(this Model model)
        {
            int lastGroupIndex = 0;
            foreach(var ent in model.Entities)
            {
                if (lastGroupIndex < ent.GroupIndex)
                    lastGroupIndex = ent.GroupIndex;
            }

            return lastGroupIndex;
        }

        static public bool IsBusy(this Model model)
        {
            if (model.IsBusy)
                return true;
            if (HModel.playing)
                return true;
                

            return false;
        }

        static public void SaveBackgroundColor(this Model model)
        {
            if (model is HModel hModel)
            {
                hModel.SaveBackgroundColor();
            }
        }

        static public void Set2DView(this Model model)
        {
            if (model is HModel hModel)
            {
                hModel.Set2DView();
            }
        }

        static public void Set3DView(this Model model)
        {
            if (model is HModel hModel)
            {
                hModel.Set3DView();
            }
        }

        static public bool IsTopViewOnly(this Model model, Viewport vp)
        {
            if (model is HModel hModel)
                return hModel.IsTopViewOnly(vp);
            return false;
        }
        static public GripManager GetGripManager(this Model model)
        {
            if (model is HModel hModel)
                return hModel.gripManager;
            return null;
        }

        static public Snapping GetSnapping(this Model model)
        {
            if (model is HModel hModel)
                return hModel.Snapping;
            return null;
        }

        static public OrthoModeManager GetOrthoModeManager(this Model model)
        {
            if (model is HModel hModel)
                return hModel.orthoModeManager;
            return null;
        }
        static public GridSnapping GetGridSnapping(this Model model)
        {
            if (model is HModel hModel)
                return hModel.gridSnapping;
            return null;
        }
    }
}
