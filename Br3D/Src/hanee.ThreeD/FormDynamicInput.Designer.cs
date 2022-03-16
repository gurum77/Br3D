
namespace hanee.ThreeD
{
    partial class FormDynamicInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.textEditX = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemX = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditY = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEditX);
            this.layoutControl1.Controls.Add(this.textEditY);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(708, 0, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(360, 52);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemX,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(360, 52);
            this.Root.TextVisible = false;
            // 
            // textEditX
            // 
            this.textEditX.Location = new System.Drawing.Point(31, 12);
            this.textEditX.Name = "textEditX";
            this.textEditX.Size = new System.Drawing.Size(147, 22);
            this.textEditX.StyleController = this.layoutControl1;
            this.textEditX.TabIndex = 4;
            // 
            // layoutControlItemX
            // 
            this.layoutControlItemX.Control = this.textEditX;
            this.layoutControlItemX.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemX.Name = "layoutControlItemX";
            this.layoutControlItemX.Size = new System.Drawing.Size(170, 32);
            this.layoutControlItemX.Text = "X";
            this.layoutControlItemX.TextSize = new System.Drawing.Size(7, 15);
            // 
            // textEditY
            // 
            this.textEditY.Location = new System.Drawing.Point(201, 12);
            this.textEditY.Name = "textEditY";
            this.textEditY.Size = new System.Drawing.Size(147, 22);
            this.textEditY.StyleController = this.layoutControl1;
            this.textEditY.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditY;
            this.layoutControlItem2.Location = new System.Drawing.Point(170, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(170, 32);
            this.layoutControlItem2.Text = "Y";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(7, 15);
            // 
            // FormDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 52);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "FormDynamicInput";
            this.Text = "Dynamic input";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit textEditX;
        private DevExpress.XtraEditors.TextEdit textEditY;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}