using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormXyzDynamicInput : DevExpress.XtraEditors.XtraForm, IDynamicInputPoint3D
    {
        double? fixedX { get; set; }
        double? fixedY { get; set; }
        double? fixedZ { get; set; }

        public FormXyzDynamicInput()
        {
            InitializeComponent();
            Translate();
        }

        void Translate()
        {
        }

        public void Init()
        {
            fixedX = null;
            fixedY = null;
            fixedZ = null;

            textEditX.Focus();
            textEditX.SelectAll();
        }


        // 현재 상황에 맞게 control을 업데이트 한다.
        public void UpdateControls(devDept.Eyeshot.Environment environment)
        {
            textEditX.Text = ActionBase.Point3D.X.ToString();
            textEditY.Text = ActionBase.Point3D.Y.ToString();
            textEditZ.Text = ActionBase.Point3D.Z.ToString();
            textEditX.SelectAll();
            textEditY.SelectAll();
            textEditZ.SelectAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // enter 키 입력시 입력 완료(fixed 된 값이 없으면 action에서 enter친걸로 하고, 아니면 클릭한걸로 친다)
            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                if (fixedX != null || fixedY != null || fixedZ != null)
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
                if (fixedX != null || fixedY != null || fixedZ != null)
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
                if (textEditX.IsEditorActive)
                {
                    textEditY.Focus();
                    textEditY.SelectAll();
                    return true;
                }
                else if (textEditY.IsEditorActive)
                {
                    textEditZ.Focus();
                    textEditZ.SelectAll();
                    return true;
                }
            }
            else if (keyData == Keys.Oem3)
            {
                DynamicInputManager.FlagPoint3DType();

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textEditX_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedX = textEditX.Text.ToDouble();
                Invalidate();
                if (ActionBase.runningAction != null)
                {
                    var me = new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
                    ActionBase.MouseMoveHandler(DynamicInputManager.environment, me, true);
                }
            }));
        }



        private void textEditY_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedY = textEditY.Text.ToDouble();
                Invalidate();
            }));
        }


        private void textEditZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedZ = textEditZ.Text.ToDouble();
                Invalidate();
            }));

        }

        private void layoutControlItemX_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = fixedX == null ? 0 : 1;
            DynamicInputManager.DrawLayoutControl(ref e, "X", idx);

        }

        private void layoutControlItemY_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = fixedY == null ? 0 : 1;
            DynamicInputManager.DrawLayoutControl(ref e, "Y", idx);
        }

        private void layoutControlItemZ_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = fixedZ == null ? 0 : 1;
            DynamicInputManager.DrawLayoutControl(ref e, "Z", idx);
        }

        public void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            if (fixedX != null)
                pt.X = fixedX.Value;
            if (fixedY != null)
                pt.Y = fixedY.Value;
            if (fixedZ != null)
                pt.Z = fixedZ.Value;
        }
    }
}