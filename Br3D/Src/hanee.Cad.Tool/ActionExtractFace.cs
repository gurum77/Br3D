using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    // face 추출
    public class ActionExtractFace : ActionBase
    {
        public ActionExtractFace(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while(true)
            {
                var faceItem = await GetFace(LanguageHelper.Tr("Select a face"), -1, true);
                if (faceItem == null)
                    break;
                if (IsEntered() || IsCanceled())
                    break;
                if(faceItem.Item is Brep brep)
                {
                    brep.Rebuild();
                    var face = brep.Faces[faceItem.Index];
                    var surfaces = face.ConvertToSurface();
                    if(surfaces != null)
                    {
                        foreach(var sur in surfaces)
                        {
                            GetHModel().entityPropertiesManager.SetDefaultProperties(sur);
                        }
                        environment.Entities.AddRange(surfaces);
                    }
                    
                }

            }

            EndAction();
            return true;
        }
    }
}
