using devDept.Geometry;
using DevExpress.XtraEditors;
using hanee.Geometry;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormLengthAngleDynamicInput : XtraForm, IDynamicInputPoint3D
    {
        public FormLengthAngleDynamicInput()
        {
            InitializeComponent();
            textEditAngle.KeyDown += TextEditAngle_KeyDown;
            textEditLength.KeyDown += TextEditLength_KeyDown;
        }

        private void TextEditLength_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DynamicInputManager.fixedLength = textEditLength.Text.ToDouble();
        }

        private void TextEditAngle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DynamicInputManager.fixedAngle = textEditAngle.Text.ToDouble();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // enter 키 입력시 입력 완료
            if (keyData == Keys.Enter)
            {
                //ActionBase.Point3D = new devDept.Geometry.Point3D(textEditX.Text.ToDouble(), textEditY.Text.ToDouble(), textEditZ.Text.ToDouble());
                ActionBase.EndInput(ActionBase.UserInput.GettingPoint3D);
            }
            
            else if (keyData == Keys.Escape)
            {
                if (DynamicInputManager.fixedX != null || DynamicInputManager.fixedY != null || DynamicInputManager.fixedZ != null)
                {
                    DynamicInputManager.Init();
                }
                else
                {
                    ActionBase.Canceled = true;
                }
            }
            else if (keyData == Keys.Oem3)
            {
                DynamicInputManager.FlagPoint3DType();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Init()
        {
            DynamicInputManager.fixedLength = null;
            DynamicInputManager.fixedAngle = null;

            textEditAngle.SelectAll();
            textEditLength.SelectAll();
        }

        public void UpdateControls(devDept.Eyeshot.Environment environment)
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

            textEditLength.Text = ActionBase.Point3D.DistanceTo(mng.startPoint).ToString();
            textEditAngle.Text = (ActionBase.Point3D - mng.startPoint).AsVector.ToDegree().ToString();
            
            textEditLength.SelectAll();
            textEditAngle.SelectAll();


        }

        public void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            HModel hModel = environment as HModel;
            var mng = hModel?.orthoModeManager;
            if (mng == null || mng.startPoint == null)
                return;

            if (DynamicInputManager.fixedLength == null &&
                DynamicInputManager.fixedAngle == null)
                return;

            double len = DynamicInputManager.fixedLength == null ? pt.DistanceTo(mng.startPoint) : DynamicInputManager.fixedLength.Value;
            double ang = DynamicInputManager.fixedAngle == null ? (pt - mng.startPoint).AsVector.ToDegree() : DynamicInputManager.fixedAngle.Value;

            var newPt = mng.startPoint + ang.ToRadians().ToVector() * len;
            pt.X = newPt.X;
            pt.Y = newPt.Y;
        }
    }
}