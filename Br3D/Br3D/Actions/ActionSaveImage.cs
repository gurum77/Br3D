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
    class ActionSaveImage : ActionBase
    {
        FormMain formMain;
        public ActionSaveImage(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            StartAction();

            var dlg = new XtraSaveFileDialog();
            dlg.Filter = "Bitmap (*.bmp)|*.bmp|" +
                "Portable Network Graphics (*.png)|*.png|" +
                "Windows metafile (*.wmf)|*.wmf|" +
                "Enhanced Windows Metafile (*.emf)|*.emf";

            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                if (!Options.Instance.saveImageWithUI)
                {
                    formMain.ShowGrid(false);
                    formMain.ShowToolbar(false);
                    formMain.ShowSymbol(false);
                    formMain.ShowBoundary(false);
                }

                double lineWeightFactor = 1;
                switch (dlg.FilterIndex)
                {

                    case 1:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp, Options.Instance.saveImageWithBackground);
                        break;
                    case 2:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Png, Options.Instance.saveImageWithBackground);
                        break;
                    case 3:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Wmf, Options.Instance.saveImageWithBackground);
                        break;
                    case 4:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Emf, Options.Instance.saveImageWithBackground);
                        break;

                }

                if (!Options.Instance.saveImageWithUI)
                {
                    formMain.ShowGrid(formMain.barButtonItemShowGrid.Down);
                    formMain.ShowToolbar(formMain.barButtonItemShowToolbar.Down);
                    formMain.ShowSymbol(formMain.barButtonItemShowSymbol.Down);
                    formMain.ShowBoundary(formMain.barButtonItemShowBoundary.Down);
                }

            }

            EndAction();
        }
    }
}
