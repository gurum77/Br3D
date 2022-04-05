using hanee.ThreeD;
using System;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionErase : ActionBase
    {
        public ActionErase(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var entities = await GetEntities("Select entities(Enter : erase)");
                if (IsCanceled())
                    break;

                GetModel().Entities.DeleteSelected();
                GetModel().Invalidate();
            }

            EndAction();
            return true;
        }
    }
}
