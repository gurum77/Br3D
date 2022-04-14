using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionInsert : ActionBase
    {
        Point3D lastPoint = null;
        public ActionInsert(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public async override void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (lastPoint != null)
            {
                var vec = (point3D - lastPoint).AsVector;
                environment.TempEntities.Translate(vec);
                
                environment.TempEntities.RegenAfterModify();
            }

            lastPoint = point3D.Clone() as Point3D;
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                OpenFileDialog dlg = new OpenFileDialog();

                var additionalSupportFormats = new Dictionary<string, string>();
                dlg.Filter = FileHelper.FilterForOpenDialog(additionalSupportFormats);
                dlg.FilterIndex = 0;
                dlg.AddExtension = true;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                    break;

                devDept.Eyeshot.Translators.ReadFileAsync rf = FileHelper.GetReadFileAsync(dlg.FileName);
                if (rf == null)
                    break;
                rf.DoWork();

                rf.FillAllCollectionsData(environment);
                rf.Entities.ToTempEntities(environment, true);

                // 좌측하단
                var leftBottom = environment.TempEntities.GetLeftBottom();
                if (leftBottom == null)
                    break;

                // 좌측 하단이 0,0이 되도록 이동
                var vec = leftBottom.AsVector * -1;
                environment.TempEntities.Translate(vec);

                var pt = await GetPoint3D(LanguageHelper.Tr("Insertion point"));
                if (IsCanceled())
                    break;
                if (IsEntered())
                    break;


                vec = (pt - leftBottom).AsVector;
                foreach (var ent in rf.Entities)
                {
                    ent.Translate(vec);
                }
                rf.AddToScene(environment);
                environment.Invalidate();
                break;
            }

            EndAction();
            return true;
        }
    }
}
