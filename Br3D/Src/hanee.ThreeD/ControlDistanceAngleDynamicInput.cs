using devDept.Geometry;
using DevExpress.XtraEditors;
using hanee.Geometry;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class ControlDistanceAngleDynamicInput : DevExpress.XtraEditors.XtraUserControl, IDynamicInputPoint3D
    {
        devDept.Eyeshot.Environment environment { get; set; }
        public double? fixedLength { get; set; }
        public double? fixedAngle { get; set; }

        public TextEdit textEditLength => controlDynamicInputEdit1.textEdit1;
        public TextEdit textEditAngle => controlDynamicInputEdit2.textEdit1;

        PictureEdit pictureLength => controlDynamicInputEdit1.pictureEdit1;
        PictureEdit pictureAngle => controlDynamicInputEdit2.pictureEdit1;

        public ControlDistanceAngleDynamicInput()
        {
            InitializeComponent();

            textEditLength.KeyDown += TextEditLength_KeyDown;
            textEditAngle.KeyDown += TextEditAngle_KeyDown;

            Translate();
        }

        private void TextEditAngle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedAngle = textEditAngle.Text.ToDouble();
                pictureAngle.Image = DynamicInputManager.GetImage(1);
                Invalidate();
            }));
        }

        private void TextEditLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedLength = textEditLength.Text.ToDouble();
                pictureLength.Image = DynamicInputManager.GetImage(1);
                Invalidate();
            }));
        }

        void Translate()
        {
            controlDynamicInputEdit1.labelControl1.Text = LanguageHelper.Tr("Length");
            controlDynamicInputEdit2.labelControl1.Text = LanguageHelper.Tr("Angle");
            simpleButtonByXYZ.Text = LanguageHelper.Tr("By XYZ");
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

            controlDynamicInputEdit1.Visible = true;
            controlDynamicInputEdit2.Visible = true;
            pictureLength.Image = DynamicInputManager.GetImage(0);
            pictureAngle.Image = DynamicInputManager.GetImage(0);
        }

        public void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            HModel hModel = environment as HModel;
            var mng = hModel?.orthoModeManager;
            var plane = environment.GetWorkplane();
            if (mng == null || mng.startPoint == null || plane == null)
                return;

            if (fixedLength == null &&
                fixedAngle == null)
                return;

            
            double len = fixedLength == null ? pt.DistanceTo(mng.startPoint) : fixedLength.Value;
            double ang = fixedAngle == null ? plane.ProjectDegree(mng.startPoint, pt) : fixedAngle.Value;
            var vec = plane.VectorByDegree(ang);

            var newPt = mng.startPoint + vec * len;
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

        private void simpleButtonByXYZ_Click(object sender, EventArgs e)
        {
            DynamicInputManager.FlagPoint3DType();
        }
    }
}
