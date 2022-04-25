using System;

namespace hanee.ThreeD
{
    public class FormDynamicInputBase : DevExpress.XtraEditors.XtraForm
    {
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        public FormDynamicInputBase()
        {
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormDynamicInputBase
            // 
            this.ClientSize = new System.Drawing.Size(298, 268);
            this.Name = "FormDynamicInputBase";
            this.ResumeLayout(false);

        }

        // todo : 포커스주거나 하는 동작은 성능에 영향을 많이 준다.
        // 다른 방법으로 dynamic input을 처리해야한다.
        public void SetPosition(devDept.Eyeshot.Environment environment)
        {
            if (environment == null)
                return;

            //var loc = environment.Location;
            //loc.X += environment.Size.Width - Size.Width;
            //loc.Y += environment.Size.Height / 2;
            //Location = loc;


            Location = new System.Drawing.Point(0, 0);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //if (!Visible)
            //    return;

            //// 마우스 커서 위치에서 오른쪽으로 50을 이동한다.
            //var loc = System.Windows.Forms.Cursor.Position;
            //loc.X += 50;
            //if (Location.Equals(loc))
            //    return;

            //Location = loc;

            //BeginInvoke(new Action(() =>
            //{
            //    IDynamicInput di = this as IDynamicInput;
            //    if (di != null)
            //    {
            //        //di.UpdateControls();
            //    }
            //}));
        }
    }
}
