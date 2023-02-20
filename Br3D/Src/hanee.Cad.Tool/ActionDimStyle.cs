using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionDimStyle : ActionBase
    {
        public ActionDimStyle(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            List<Entity> entities = null;
            while(true)
            {
                var ek = await GetEntitiesOrText(LanguageHelper.Tr("Select dimensions(A:All dimensions)"), -1, false);
                if (IsCanceled())
                    break;

                if(ek.Key == null && ek.Value != null && ek.Value.EqualsIgnoreCase("a"))
                {
                    entities = model.Entities.FindAll(x => x is Dimension);
                    foreach(var ent in entities)
                    {
                        ent.Selected = true;
                    }
                    model.Invalidate();
                }
                else
                {
                    entities = ek.Key;
                }

                FormDimStyle form = new FormDimStyle(entities, model);
                if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var data = new RegenParams(0.001, model);
                    entities.ForEach(ent => ent.Regen(data));

                    model.Entities.Regen();
                    model.Invalidate();
                }

                break;
            }

            EndAction();
            return true;
        }
    }
}
