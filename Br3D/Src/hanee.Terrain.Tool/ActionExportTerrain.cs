using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Terrain.Tool
{
    public class ActionExportTerrain : ActionBase
    {
        public ActionExportTerrain(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();
            while(true)
            {
                var mesh = await GetEntity(LanguageHelper.Tr("Select mesh"));
                if (IsEntered() || IsCanceled())
                    break;
                
                var k = await GetKey(LanguageHelper.Tr("X:LandXML 1.2"));
                if (IsEntered() || IsCanceled())
                    break;

                if (k.KeyCode == Keys.X)
                {
                    var dlg = new SaveFileDialog();
                    dlg.Filter = "LandXML 1.2|*.xml";
                    dlg.DefaultExt = "xml";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (TerrainExchanger.ToLandXML(mesh as Mesh, dlg.FileName))
                            MessageBox.Show("Export completed!");
                    }
                }

                break;
            }

            EndAction();
            return true;
        }
    }
}
