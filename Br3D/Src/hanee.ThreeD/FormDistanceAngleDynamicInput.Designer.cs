
namespace hanee.ThreeD
{
    partial class FormDistanceAngleDynamicInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDistanceAngleDynamicInput));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditLength = new DevExpress.XtraEditors.TextEdit();
            this.textEditAngle = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemLength = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAngle = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemLength = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItemAngle = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEditLength);
            this.layoutControl1.Controls.Add(this.textEditAngle);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(203, 49);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEditLength
            // 
            this.textEditLength.EditValue = "";
            this.textEditLength.Location = new System.Drawing.Point(74, 2);
            this.textEditLength.Name = "textEditLength";
            this.textEditLength.Size = new System.Drawing.Size(63, 20);
            this.textEditLength.StyleController = this.layoutControl1;
            this.textEditLength.TabIndex = 4;
            // 
            // textEditAngle
            // 
            this.textEditAngle.Location = new System.Drawing.Point(74, 26);
            this.textEditAngle.Name = "textEditAngle";
            this.textEditAngle.Size = new System.Drawing.Size(63, 20);
            this.textEditAngle.StyleController = this.layoutControl1;
            this.textEditAngle.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLength,
            this.layoutControlItemAngle,
            this.simpleLabelItemLength,
            this.simpleLabelItemAngle});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(203, 49);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemLength
            // 
            this.layoutControlItemLength.Control = this.textEditLength;
            this.layoutControlItemLength.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("layoutControlItemLength.ImageOptions.SvgImage")));
            this.layoutControlItemLength.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.layoutControlItemLength.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemLength.Name = "layoutControlItemLength";
            this.layoutControlItemLength.Size = new System.Drawing.Size(139, 24);
            this.layoutControlItemLength.Text = "Length";
            this.layoutControlItemLength.TextSize = new System.Drawing.Size(60, 16);
            this.layoutControlItemLength.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItemLength_CustomDraw);
            // 
            // layoutControlItemAngle
            // 
            this.layoutControlItemAngle.Control = this.textEditAngle;
            this.layoutControlItemAngle.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("layoutControlItemAngle.ImageOptions.SvgImage")));
            this.layoutControlItemAngle.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.layoutControlItemAngle.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemAngle.Name = "layoutControlItemAngle";
            this.layoutControlItemAngle.Size = new System.Drawing.Size(139, 25);
            this.layoutControlItemAngle.Text = "Angle";
            this.layoutControlItemAngle.TextSize = new System.Drawing.Size(60, 16);
            this.layoutControlItemAngle.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItemAngle_CustomDraw);
            // 
            // simpleLabelItemLength
            // 
            this.simpleLabelItemLength.AllowHotTrack = false;
            this.simpleLabelItemLength.Location = new System.Drawing.Point(139, 0);
            this.simpleLabelItemLength.Name = "simpleLabelItemLength";
            this.simpleLabelItemLength.Size = new System.Drawing.Size(64, 24);
            this.simpleLabelItemLength.Text = "m";
            this.simpleLabelItemLength.TextSize = new System.Drawing.Size(60, 14);
            // 
            // simpleLabelItemAngle
            // 
            this.simpleLabelItemAngle.AllowHotTrack = false;
            this.simpleLabelItemAngle.Location = new System.Drawing.Point(139, 24);
            this.simpleLabelItemAngle.Name = "simpleLabelItemAngle";
            this.simpleLabelItemAngle.Size = new System.Drawing.Size(64, 25);
            this.simpleLabelItemAngle.Text = "도";
            this.simpleLabelItemAngle.TextSize = new System.Drawing.Size(60, 14);
            // 
            // FormLengthAngleDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 49);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLengthAngleDynamicInput";
            this.Text = "FormPoint3DDynamicInputByLengthAngle";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemAngle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit textEditLength;
        private DevExpress.XtraEditors.TextEdit textEditAngle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLength;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAngle;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemLength;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemAngle;
    }
}