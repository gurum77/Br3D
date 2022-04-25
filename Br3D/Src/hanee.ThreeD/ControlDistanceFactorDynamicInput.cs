using devDept.Geometry;
using DevExpress.XtraEditors;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class ControlDistanceFactorDynamicInput : DevExpress.XtraEditors.XtraUserControl, IDynamicInputPoint3D
    {
        devDept.Eyeshot.Environment environment { get; set; }
        public double baseLength { get; set; } = 1;
        public double? fixedFactor { get; set; }

        TextEdit textEditFactor => controlDynamicInputEdit1.textEdit1;
        PictureEdit pictureEditFactor => controlDynamicInputEdit1.pictureEdit1;

        public ControlDistanceFactorDynamicInput()
        {
            InitializeComponent();

            textEditFactor.KeyDown += TextEditFactor_KeyDown;

            Translate();
        }

        void Translate()
        {
            controlDynamicInputEdit1.labelControl1.Text = LanguageHelper.Tr("Scale");
        }
        private void TextEditFactor_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedFactor = textEditFactor.Text.ToDouble();
                pictureEditFactor.Image = DynamicInputManager.GetImage(1);
                Invalidate();
            }));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // enter 키 입력시 입력 완료
            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                if (fixedFactor != null)
                {
                    var pt3D = ActionBase.Point3D;
                    ModifyPoint3D(DynamicInputManager.environment, ref pt3D);
                    ActionBase.Point3D = pt3D;

                    ActionBase.EndInput(ActionBase.UserInput.GettingPoint3D);
                }
                else
                {
                    ActionBase.Entered = true;
                }
            }

            else if (keyData == Keys.Escape)
            {
                if (fixedFactor != null)
                {
                    Init(environment);
                }
                else
                {
                    ActionBase.Canceled = true;
                }
            }
            else if (keyData == Keys.Oemcomma)
            {
                return true;
            }
            //else if (keyData == Keys.Oem3)  // 물결
            //{
            //    DynamicInputManager.FlagPoint3DType();
            //}

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            HModel hModel = environment as HModel;
            var mng = hModel?.orthoModeManager;
            if (mng == null || mng.startPoint == null)
                return;

            if (fixedFactor == null)
                return;

            double len = fixedFactor.Value * baseLength;
            var dir = (pt - mng.startPoint).ToDir();
            if (dir.IsZero)
                dir = new Vector3D(1, 0, 0);
            var newPt = mng.startPoint + dir * len;

            pt.X = newPt.X;
            pt.Y = newPt.Y;
            pt.Z = newPt.Z;
        }

        public void UpdateControls()
        {
            HModel hModel = environment as HModel;
            if (hModel == null)
                return;

            var mng = hModel.orthoModeManager;
            if (mng == null || mng.startPoint == null)
            {
                return;
            }

            if (fixedFactor == null)
            {
                var factor = ActionBase.Point3D.DistanceTo(mng.startPoint) / baseLength;
                textEditFactor.Text = factor.ToString();
                textEditFactor.SelectAll();

            }
        }

        public void Init(devDept.Eyeshot.Environment environment)
        {
            this.environment = environment;
            fixedFactor = null;
            baseLength = 1;
            textEditFactor.SelectAll();
            pictureEditFactor.Image = DynamicInputManager.GetImage(0);
        }
    }
}
