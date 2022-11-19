using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Br3D.Actions
{
    class ActionOpen : ActionBase
    {
        FormMain formMain;
        static public string fileName;
        public ActionOpen(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            StartAction();

          

            // 파일 선택
            if (string.IsNullOrEmpty(fileName))
            {
                var dlg = new XtraOpenFileDialog();
                Dictionary<string, string> additionalSupportFormats = new Dictionary<string, string>();
                dlg.Filter = FileHelper.FilterForOpenDialog(additionalSupportFormats);
                dlg.FilterIndex = 0;
                dlg.AddExtension = true;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    EndAction();
                    return;
                }

                fileName = dlg.FileName;
            }



            // 이미 열려 있는 파일이면 리턴
            if (formMain.opendFilePath.Equals(fileName))
            {
                EndAction();
                return;
            }

            if (!formMain.CheckSaveForModifiedFile())
            {
                EndAction();
                return;
            }


            formMain.Import(fileName);
            
            // mur list 등록
            MRUManager.AddItem(fileName);

            fileName = "";

            EndAction();
        }
    }
}
