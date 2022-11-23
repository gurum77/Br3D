using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionSubtract : ActionBase
    {
        protected Entity ent1, ent2;
        public ActionSubtract(devDept.Eyeshot.Environment environment) : base(environment)
        {
            
        }

        public override async void Run()
        { await RunAsync(); }

        protected virtual Entity[] Calc()
        {
            if (ent1 is Brep brep1 && ent2 is Brep brep2)
            {
                return Brep.Difference(brep1, brep2);
            }

            return null;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                ent1 = await GetEntity(LanguageHelper.Tr("Select first entity"));
                if (IsCanceled())
                    break;

                var selectableTypes = new Dictionary<Type, bool>();
                if (ent1 is Brep)
                    selectableTypes.Add(typeof(Brep), true);
                else if (ent1 is Region)
                    selectableTypes.Add(typeof(Region), true);

                ent2 = await GetEntity(LanguageHelper.Tr("Select second entity"), -1, true, selectableTypes);
                if (IsCanceled())
                    break;

                var results = Calc();
                if (results != null)
                {
                    CreateTransaction();
                    DeleteEntities(ent1, ent2);
                    foreach (var r in results)
                        r.Regen(0.0010);
                    AddEntities(results);
                    CommitTransation();
                }

                break;
            }
            EndAction();
            return true;
        }
    }
}
