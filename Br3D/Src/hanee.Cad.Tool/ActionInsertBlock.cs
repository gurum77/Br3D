using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    // block 삽입
    public class ActionInsertBlock : ActionBase
    {
        Vector3D lastVec;

        public ActionInsertBlock(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (lastVec != null)
                environment.TempEntities.Translate(lastVec * -1);

            lastVec = point3D.AsVector;
            environment.TempEntities.Translate(lastVec);
            environment.TempEntities.RegenAfterModify();
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                // block 선택
                FormBlock form = new FormBlock(environment as Model, FormBlock.Mode.existBlockName);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    break;

                if (!environment.Blocks.TryGetValue(form.curBlockName, out Block block))
                    break;

                var br = new BlockReference(form.curBlockName);
                br.Scale(new Point3D(0, 0, 0), form.xScale, form.yScale, form.zScale);
                EntitiesHelper.AddEntityToTempEntities(br, environment, true);


                var insertionPoint = await GetPoint3D(LanguageHelper.Tr("Insertion point"));
                if (IsCanceled())
                    break;

                br.InsertionPoint = insertionPoint;

                AddEntities(br);
                break;
            }
            EndAction();
            return true;
        }

    }
}
