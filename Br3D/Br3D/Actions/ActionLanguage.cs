using hanee.ThreeD;
using System.Threading.Tasks;

namespace Br3D
{
    public class ActionLanguage : ActionBase
    {
        FormMain formMain;
        string language;
        public ActionLanguage(devDept.Eyeshot.Environment environment, FormMain formMain, string language) : base(environment)
        {
            this.formMain = formMain;
            this.language = language;
        }

        public override void Run()
        {
            StartAction();

            Options.Instance.language = language;
            formMain.InitTileElementStatus();
            LanguageHelper.Load(Options.Instance.language);
            formMain.Translate();

            EndAction();
        }
    }
}
