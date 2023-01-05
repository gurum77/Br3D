using devDept.Eyeshot.Entities;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionGroup : ActionBase
    {
        public ActionGroup(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var et = await GetEntitiesOrText(LanguageHelper.Tr("Select entities(L : List, G : Input group)"), -1, false, null, "l", "g");

                var entities = new List<Entity>();
                // group 리스트 보기
                if (et.Value != null && et.Value.EqualsIgnoreCase("l"))
                {
                    CmdBarManager.AddHistory();
                    var groupsIndexes = model.GetAllGroupIndexes();
                    foreach (var gi in groupsIndexes)
                    {
                        var entitiesInGroup = model.GetEntitiesInGroup(gi);
                        var count = entitiesInGroup == null ? 0 : entitiesInGroup.Count;
                        var str = $"Group index : {gi}, Entities : {count}";
                        CmdBarManager.AddHistory(str);
                    }

                    continue;
                }
                // group 번호를 입력해서 객체 선택
                else if(et.Value != null && et.Value.EqualsIgnoreCase("g"))
                {

                    var groupIndexText = await GetText("Group index");
                    if (IsCanceled())
                        break;

                    var inputGroupIndex = groupIndexText.ToInt();
                    entities = model.Entities.FindAll(ent => ent.GroupIndex == inputGroupIndex);
                    foreach (var ent in entities)
                        ent.Selected = true;
                }
                else
                {
                    entities = et.Key;
                }
                    
                if (IsCanceled())
                    break;

                var text = await GetText(LanguageHelper.Tr("G : Group, U : Ungroup"), -1, "g", "u");
                if (text == null)
                    break;
                if (IsCanceled() || IsEntered())
                    break;

                var groupIndex = -1;
                if(text.EqualsIgnoreCase("g"))
                {
                    groupIndex = model.GetLastGroupIndex() + 1;
                }

                foreach(var ent in entities)
                {
                    ent.GroupIndex = groupIndex;
                }

                break;
            }


            EndAction();
            return true;
        }
    }
}
