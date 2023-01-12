using devDept.Eyeshot.Entities;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionGroup : ActionBase
    {
        Action regenObjectTreeAction;
        public ActionGroup(devDept.Eyeshot.Environment environment, Action regenObjectTreeAction) : base(environment)
        {
            this.regenObjectTreeAction = regenObjectTreeAction;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var et = await GetEntitiesOrText(LanguageHelper.Tr("Select entities(L : List, I : Group index)"), -1, false, null, "l", "i");

                var entities = new List<Entity>();
                // group 리스트 보기
                if (et.Value != null && et.Value.EqualsIgnoreCase("l"))
                {
                    CmdBarManager.InitTextEdit();
                    CmdBarManager.AddHistory(LanguageHelper.Tr("***** Group List *****"));
                    var groupsIndexes = model.GetAllGroupIndexes();
                    foreach (var gi in groupsIndexes)
                    {
                        var entitiesInGroup = model.GetEntitiesInGroup(gi);
                        var count = entitiesInGroup == null ? 0 : entitiesInGroup.Count;
                        var str1 = LanguageHelper.Tr("Group Index");
                        var str2 = LanguageHelper.Tr("Entities");
                        var str = $"{str1} : {gi}, {str2} : {count}";
                        CmdBarManager.AddHistory(str);
                    }
                    CmdBarManager.AddHistory("**********************");
                    continue;
                }
                // group 번호를 입력해서 객체 선택
                else if(et.Value != null && et.Value.EqualsIgnoreCase("i"))
                {

                    var groupIndexText = await GetText(LanguageHelper.Tr("Group Index"));
                    if (IsCanceled())
                        break;

                    var inputGroupIndex = groupIndexText.ToInt();
                    entities = model.Entities.FindAll(ent => ent.GroupIndex == inputGroupIndex);
                    foreach (var ent in entities)
                        ent.Selected = true;
                    model.Invalidate();
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

                if (regenObjectTreeAction != null)
                    regenObjectTreeAction();

                break;
            }


            EndAction();
            return true;
        }
    }
}
