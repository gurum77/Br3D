using hanee.Geometry;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormPoint3DDynamicInput : DevExpress.XtraEditors.XtraForm, IDynamicInput
    {
        System.Drawing.Brush foreBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public FormPoint3DDynamicInput()
        {
            InitializeComponent();
         
        }

        private void FormDynamicInput_MouseEnter(object sender, EventArgs e)
        {
            var loc = Cursor.Position;
            loc = PointToScreen(loc);
            loc.X += 50;
            this.Location = loc;
        }

        public void Init()
        {
            textEditX.Focus();
            textEditX.SelectAll();
        }
        

        // 현재 상황에 맞게 control을 업데이트 한다.
        public void UpdateControls()
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
            // enter 키 입력시 입력 완료
            if (keyData == Keys.Enter)
            {
                ActionBase.Point3D = new devDept.Geometry.Point3D(textEditX.Text.ToDouble(), textEditY.Text.ToDouble(), textEditZ.Text.ToDouble());
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textEditX_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                DynamicInputManager.fixedX = textEditX.Text.ToDouble();
                Invalidate();
            }));
        }



        private void textEditY_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                DynamicInputManager.fixedY = textEditY.Text.ToDouble();
                Invalidate();
            }));
        }


        private void textEditZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                DynamicInputManager.fixedZ = textEditZ.Text.ToDouble();
                Invalidate();
            }));

        }

        private void layoutControlItemX_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = DynamicInputManager.fixedX == null ? 0 : 1;
            if (DynamicInputManager.fixedX != null)
                e.Cache.DrawImage(svgImageCollection1.GetImage(idx), new System.Drawing.Point(e.Bounds.X, e.Bounds.Y));
            e.Cache.DrawString("X", DefaultFont, foreBrush, e.Bounds.X + 20, e.Bounds.Y + 7);
            e.Handled = true;

        }

        private void layoutControlItemY_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = DynamicInputManager.fixedY == null ? 0 : 1;
            if (DynamicInputManager.fixedY != null)
                e.Cache.DrawImage(svgImageCollection1.GetImage(idx), new System.Drawing.Point(e.Bounds.X, e.Bounds.Y));
            e.Cache.DrawString("Y", DefaultFont, foreBrush, e.Bounds.X + 20, e.Bounds.Y + 7);
            e.Handled = true;
        }

        private void layoutControlItemZ_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            var idx = DynamicInputManager.fixedZ == null ? 0 : 1;
            if (DynamicInputManager.fixedZ != null)
                e.Cache.DrawImage(svgImageCollection1.GetImage(idx), new System.Drawing.Point(e.Bounds.X, e.Bounds.Y));
            e.Cache.DrawString("Z", DefaultFont, foreBrush, e.Bounds.X + 20, e.Bounds.Y + 7);
            e.Handled = true;
        }
    }
}