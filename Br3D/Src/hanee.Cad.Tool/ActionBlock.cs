using devDept.Eyeshot;
using hanee.ThreeD;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionBlock : ActionBase
    {
        public ActionBlock(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {

                var entities = await GetEntities(LanguageHelper.Tr("Select entities"));
                if (IsCanceled())
                    break;

                var basePoint = await GetPoint3D(LanguageHelper.Tr("Base point"));
                if (IsCanceled())
                    break;


                FormBlock form = new FormBlock(environment as Model);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    break;


                break;
            }

            EndAction();
            return true;
        }
    }
}

