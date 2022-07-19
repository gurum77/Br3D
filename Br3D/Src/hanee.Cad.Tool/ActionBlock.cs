using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DevExpress.XtraEditors;
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

                

                FormBlock form = new FormBlock(environment as Model, FormBlock.Mode.newBlockName);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    break;

                var blockName = form.curBlockName;
                environment.Blocks.TryGetValue(blockName, out Block block);
                if (block == null)
                {
                    block = new Block(blockName);
                    environment.Blocks.Add(block);
                }
                block.Entities.Clear();
                block.Entities.AddRange(entities);
                block.BasePoint = basePoint.Clone() as Point3D;
                environment.Entities.RemoveAll(x => entities.Contains(x));

                var br = new BlockReference(blockName);
                br.InsertionPoint = basePoint.Clone() as Point3D;
                environment.Entities.Add(br);

                environment.Entities.Regen();
                environment.Invalidate();

                break;
            }

            EndAction();
            return true;
        }
    }
}

