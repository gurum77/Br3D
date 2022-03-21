using hanee.Geometry;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class FormDynamicInput : DevExpress.XtraEditors.XtraForm
    {
        bool lockX = false;
        bool lockY = false;
        public FormDynamicInput()
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

        // 현재 상황에 맞게 control을 업데이트 한다.
        public void UpdateControls()
        {
            textEditX.Text = ActionBase.Point3D.X.ToString();
            textEditY.Text = ActionBase.Point3D.Y.ToString();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // enter 키 입력시 입력 완료
            if (keyData == Keys.Enter)
            {
                ActionBase.Point3D = new devDept.Geometry.Point3D(textEditX.Text.ToDouble(), textEditY.Text.ToDouble(), 0);
                ActionBase.userInputting[(int)ActionBase.UserInput.GettingPoint3D] = false;
            }
            else if (keyData == Keys.Escape)
            {
                if (lockX == true || lockY == true)
                {
                    lockX = false;
                    lockY = false;
                }
                else
                {
                    ActionBase.Canceled = true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textEditX_EditValueChanged(object sender, EventArgs e)
        {
            lockX = true;
        }

        private void layoutControlItemX_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            layoutControlItemX.ImageOptions.SvgImage = svgImageCollection1[lockX ? 0 : 1];

        }

        private void textEditY_EditValueChanged(object sender, EventArgs e)
        {
            lockY = true;
        }

        private void layoutControlItem2_CustomDraw(object sender, DevExpress.XtraLayout.ItemCustomDrawEventArgs e)
        {
            layoutControlItemY.ImageOptions.SvgImage = svgImageCollection1[lockY ? 0 : 1];
        }
    }
}