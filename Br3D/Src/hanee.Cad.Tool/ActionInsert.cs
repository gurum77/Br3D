using devDept.Eyeshot;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionInsert : ActionBase
    {
        Vector3D lastVec;
        public ActionInsert(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public async override void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (lastVec != null)
                environment.TempEntities.Translate(lastVec * -1);

            var vec = point3D.AsVector;
            environment.TempEntities.Translate(vec);
            environment.TempEntities.RegenAfterModify();

            lastVec = vec;
        }
        public async Task<bool> RunAsync()
        {
            StartAction();


            while (true)
            {
                lastVec = null;

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
                var rfa = rf as devDept.Eyeshot.Translators.ReadAutodesk;

                // 좌측하단
                var leftBottom = environment.TempEntities.GetLeftBottom();
                if (leftBottom == null)
                    break;

                var byLeftBottom = true;
                // 좌측 하단이 0,0이 되도록 이동
                var vec = leftBottom.AsVector * -1;
                environment.TempEntities.Translate(vec);

                Point3D insertionPoint;
                while(true)
                {
                    var ptOrKey = await GetPoint3DOrKey(LanguageHelper.Tr("Insertion point(L : By Left-bottom, B : By Base point")));
                    if (IsCanceled())
                        break;
                    if (IsEntered())
                        break;

                    if(ptOrKey.Key != null)
                    {
                        insertionPoint = ptOrKey.Key;
                        break;
                    }

                    // 좌측하단 기준이 아닌데 좌측하단으로 바꾸는 경우
                    if(ptOrKey.Value.KeyCode == Keys.L && !byLeftBottom)
                    {
                        // 우선 초기상태로 되돌리고
                        environment.TempEntities.Translate(vec);

                        // 좌측하단으로 변경
                        vec = leftBottom.AsVector * -1;
                        environment.TempEntities.Translate(vec);
                    }
                    else if (ptOrKey.Value.KeyCode == Keys.B && byLeftBottom)
                    {
                        // 우선 초기상태로 되돌리고
                        environment.TempEntities.Translate(vec);

                        // 좌측하단으로 변경
                        vec = leftBottom.AsVector * -1;
                        environment.TempEntities.Translate(vec);
                    }




                }
                

                // 이동
                rf.Entities.Translate(baseVec);
                rf.Entities.Translate(insertionPoint.AsVector);

                rf.AddToScene(environment);
                environment.Invalidate();
                break;
            }

            EndAction();
            return true;
        }
    }
}
