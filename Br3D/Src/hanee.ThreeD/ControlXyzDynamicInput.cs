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
    public partial class ControlXyzDynamicInput : DevExpress.XtraEditors.XtraUserControl, IDynamicInputPoint3D
    {
        devDept.Eyeshot.Environment environment { get; set; }
        double? fixedX { get; set; }
        double? fixedY { get; set; }
        double? fixedZ { get; set; }

        TextEdit textEditX => controlDynamicInputEdit1.textEdit1;
        TextEdit textEditY => controlDynamicInputEdit2.textEdit1;
        TextEdit textEditZ => controlDynamicInputEdit3.textEdit1;

        PictureEdit pictureX => controlDynamicInputEdit1.pictureEdit1;
        PictureEdit pictureY => controlDynamicInputEdit2.pictureEdit1;
        PictureEdit pictureZ => controlDynamicInputEdit3.pictureEdit1;


        public ControlXyzDynamicInput()
        {
            InitializeComponent();
            controlDynamicInputEdit1.labelControl1.Text = "X";
            controlDynamicInputEdit2.labelControl1.Text = "Y";
            controlDynamicInputEdit3.labelControl1.Text = "Z";

            textEditX.KeyDown += TextEditX_KeyDown;
            textEditY.KeyDown += TextEditY_KeyDown;
            textEditZ.KeyDown += TextEditZ_KeyDown;
        }

        private void TextEditZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedZ = textEditZ.Text.ToDouble();
                pictureZ.Image = DynamicInputManager.GetImage(1);
                Invalidate();
                if (ActionBase.runningAction != null)
                {
                    var me = new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
                    ActionBase.MouseMoveHandler(DynamicInputManager.environment, me, true);
                }
            }));
        }

        private void TextEditY_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedY = textEditY.Text.ToDouble();
                pictureY.Image = DynamicInputManager.GetImage(1);
                Invalidate();
                if (ActionBase.runningAction != null)
                {
                    var me = new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
                    ActionBase.MouseMoveHandler(DynamicInputManager.environment, me, true);
                }
            }));
        }

        private void TextEditX_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.IsDigit())
                return;

            BeginInvoke(new Action(() =>
            {
                fixedX = textEditX.Text.ToDouble();
                pictureX.Image = DynamicInputManager.GetImage(1);
                Invalidate();
                if (ActionBase.runningAction != null)
                {
                    var me = new MouseEventArgs(MouseButtons.None, 0, Cursor.Position.X, Cursor.Position.Y, 0);
                    ActionBase.MouseMoveHandler(DynamicInputManager.environment, me, true);
                }


            }));
        }

        public void Init(devDept.Eyeshot.Environment environment)
        {
            this.environment = environment;

            fixedX = null;
            fixedY = null;
            fixedZ = null;

            controlDynamicInputEdit1.pictureEdit1.Image = DynamicInputManager.GetImage(0);
            controlDynamicInputEdit2.pictureEdit1.Image = DynamicInputManager.GetImage(0);
            controlDynamicInputEdit3.pictureEdit1.Image = DynamicInputManager.GetImage(0);
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
                    Init(environment);
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
            else if (keyData.IsAlphabet())
            {
                if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingKey] == true)
                {
                    ActionBase.Key = new KeyEventArgs(keyData);
                    ActionBase.EndInput(ActionBase.UserInput.GettingKey);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void UpdateControls()
        {
            if (ActionBase.Point3D == null)
                return;

            SuspendLayout();

            if (fixedX == null) 
            {
                if(textEditX.Handle != null)
                    textEditX.Text = ActionBase.Point3D.X.ToString();
            }

            if (fixedY == null)
            {
                if(textEditY.Handle != null)
                    textEditY.Text = ActionBase.Point3D.Y.ToString();
            }

            if (fixedZ == null)
            {
                if(textEditZ.Handle != null)
                    textEditZ.Text = ActionBase.Point3D.Z.ToString();
            }

            ResumeLayout();
        }


        public void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            if (fixedX != null)
                pt.X = fixedX.Value;
            if (fixedY != null)
                pt.Y = fixedY.Value;
            if (fixedZ != null)
                pt.Z = fixedZ.Value;

            //Cursor.Position
        }
    }
}
