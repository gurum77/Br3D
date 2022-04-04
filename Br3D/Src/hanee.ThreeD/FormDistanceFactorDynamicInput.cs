using devDept.Geometry;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using hanee.Geometry;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormDistanceFactorDynamicInput : XtraForm, IDynamicInputPoint3D
    {
        public double baseLength { get; set; } = 1;
        public double? fixedFactor { get; set; }
        public LayoutControlItem LayoutControlItemFactor => layoutControlItemFactor;
        public FormDistanceFactorDynamicInput()
        {
            InitializeComponent();
            textEditFactor.KeyDown += TextEditFactor_KeyDown;
            layoutControlItemFactor.CustomDraw += LayoutControlItemFactor_CustomDraw;
        }

        private void LayoutControlItemFactor_CustomDraw(object sender, ItemCustomDrawEventArgs e)
        {
            var idx = fixedFactor == null ? 0 : 1;
            DynamicInputManager.DrawLayoutControl(ref e, layoutControlItemFactor.Text, idx);
        }

        private void TextEditFactor_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedFactor = textEditFactor.Text.ToDouble();
                Invalidate();
            }));
        }

        public void Init()
        {
            fixedFactor = null;
            baseLength = 1;
            textEditFactor.SelectAll();
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

        public void UpdateControls(devDept.Eyeshot.Environment environment)
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
                    Init();
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


    }
}