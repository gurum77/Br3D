using devDept.Geometry;
using DevExpress.XtraEditors;
using hanee.Geometry;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormDistanceAngleDynamicInput : XtraForm, IDynamicInputPoint3D
    {
        devDept.Eyeshot.Environment environment { get; set; }
        public double? fixedLength { get; set; }
        public double? fixedAngle { get; set; }

        public TextEdit TextEditLength => textEditLength;
        public TextEdit TextEditAngle => textEditAngle;

        public FormDistanceAngleDynamicInput()
        {
            InitializeComponent();
            textEditAngle.KeyDown += TextEditAngle_KeyDown;
            textEditLength.KeyDown += TextEditLength_KeyDown;

            Translate();
        }
        void Translate()
        {
            layoutControlItemLength.Text = LanguageHelper.Tr("Length");
            layoutControlItemAngle.Text = LanguageHelper.Tr("Angle");
        }

        private void TextEditLength_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedLength = textEditLength.Text.ToDouble();
                Invalidate();
            }));
        }

        private void TextEditAngle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedAngle = textEditAngle.Text.ToDouble();
                Invalidate();
            }));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // enter 키 입력시 입력 완료
            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                if (fixedAngle != null || fixedLength != null)
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
                if (fixedLength != null || fixedAngle != null)
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
                if (textEditLength.IsEditorActive)
                {
                    textEditAngle.Focus();
                    textEditAngle.SelectAll();
                    return true;
                }
            }
            else if (keyData == Keys.Oem3)
            {
                DynamicInputManager.FlagPoint3DType();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Init(devDept.Eyeshot.Environment environment)
        {
            this.environment = environment;
            fixedLength = null;
            fixedAngle = null;

            textEditAngle.SelectAll();
            textEditLength.SelectAll();
        }

        public void UpdateControls()
        {
            HModel hModel = environment as HModel;
            if (hModel == null)
                return;

            var mng = hModel.orthoModeManager;
            if (mng == null || mng.startPoint == null)
            {
                DynamicInputManager.FlagPoint3DType();
                return;
            }


            if (fixedLength == null)
            {

                textEditLength.Text = ActionBase.Point3D.DistanceTo(mng.startPoint).ToString();
                textEditLength.SelectAll();
            }

            if (fixedAngle == null)
            {
                textEditAngle.Text = (ActionBase.Point3D - mng.startPoint).AsVector.ToDegree().ToString();
                textEditAngle.SelectAll();
            }

        }




        public void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            HModel hModel = environment as HModel;
            var mng = hModel?.orthoModeManager;
            if (mng == null || mng.startPoint == null)
                return;

            if (fixedLength == null &&
                fixedAngle == null)
                return;

            double len = fixedLength == null ? pt.DistanceTo(mng.startPoint) : fixedLength.Value;
            double ang = fixedAngle == null ? (pt - mng.startPoint).AsVector.ToDegree() : fixedAngle.Value;

            var newPt = mng.startPoint + ang.ToRadians().ToVector() * len;
            pt.X = newPt.X;
            pt.Y = newPt.Y;
        }

        private void layoutControlItemLength_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = fixedLength == null ? 0 : 1;
            DynamicInputManager.DrawLayoutControl(ref e, "Length", idx);
        }


        private void layoutControlItemAngle_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = fixedAngle == null ? 0 : 1;
            DynamicInputManager.DrawLayoutControl(ref e, "Angle", idx);
        }
    }
}