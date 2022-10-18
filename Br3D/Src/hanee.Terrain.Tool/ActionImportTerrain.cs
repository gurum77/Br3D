using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Terrain.Tool
{
    public class ActionImportTerrain : ActionBase
    {
        public ActionImportTerrain(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var k = await GetKey(LanguageHelper.Tr("X:LandXML 1.2"));
                if (IsEntered() || IsCanceled())
                    break;

                if (k.KeyCode == Keys.X)
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "LandXML 1.2|*.xml";
                    dlg.DefaultExt = "xml";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        var mesh = TerrainExchanger.FromLandXML(dlg.FileName);
                        if (mesh != null)
                        {

                            environment.Entities.Add(mesh);
                            environment.Entities.Regen(null);
                            environment.Invalidate();

                            var entities = new List<Entity>() { mesh };
                            environment.ZoomFit(entities, false);
                        }
                    }

                }

                break;
            }


            EndAction();
            return true;
        }
    }
}
