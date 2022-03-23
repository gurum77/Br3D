
namespace hanee.ThreeD
{
    partial class FormXyzDynamicInput
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXyzDynamicInput));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditX = new DevExpress.XtraEditors.TextEdit();
            this.textEditY = new DevExpress.XtraEditors.TextEdit();
            this.textEditZ = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemX = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemY = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEditX);
            this.layoutControl1.Controls.Add(this.textEditY);
            this.layoutControl1.Controls.Add(this.textEditZ);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(708, 0, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(177, 73);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEditX
            // 
            this.textEditX.Location = new System.Drawing.Point(43, 2);
            this.textEditX.Name = "textEditX";
            this.textEditX.Size = new System.Drawing.Size(132, 20);
            this.textEditX.StyleController = this.layoutControl1;
            this.textEditX.TabIndex = 4;
            this.textEditX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditX_KeyDown);
            // 
            // textEditY
            // 
            this.textEditY.Location = new System.Drawing.Point(43, 27);
            this.textEditY.Name = "textEditY";
            this.textEditY.Size = new System.Drawing.Size(132, 20);
            this.textEditY.StyleController = this.layoutControl1;
            this.textEditY.TabIndex = 5;
            this.textEditY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditY_KeyDown);
            // 
            // textEditZ
            // 
            this.textEditZ.Location = new System.Drawing.Point(43, 51);
            this.textEditZ.Name = "textEditZ";
            this.textEditZ.Size = new System.Drawing.Size(132, 20);
            this.textEditZ.StyleController = this.layoutControl1;
            this.textEditZ.TabIndex = 6;
            this.textEditZ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditZ_KeyDown);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemX,
            this.layoutControlItemY,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(177, 73);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemX
            // 
            this.layoutControlItemX.Control = this.textEditX;
            this.layoutControlItemX.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("layoutControlItemX.ImageOptions.SvgImage")));
            this.layoutControlItemX.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.layoutControlItemX.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemX.MinSize = new System.Drawing.Size(95, 24);
            this.layoutControlItemX.Name = "layoutControlItemX";
            this.layoutControlItemX.Size = new System.Drawing.Size(177, 25);
            this.layoutControlItemX.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemX.Text = "X";
            this.layoutControlItemX.TextSize = new System.Drawing.Size(29, 16);
            this.layoutControlItemX.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItemX_CustomDraw);
            // 
            // layoutControlItemY
            // 
            this.layoutControlItemY.Control = this.textEditY;
            this.layoutControlItemY.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("layoutControlItemY.ImageOptions.SvgImage")));
            this.layoutControlItemY.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.layoutControlItemY.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItemY.MinSize = new System.Drawing.Size(95, 24);
            this.layoutControlItemY.Name = "layoutControlItemY";
            this.layoutControlItemY.Size = new System.Drawing.Size(177, 24);
            this.layoutControlItemY.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemY.Text = "Y";
            this.layoutControlItemY.TextSize = new System.Drawing.Size(29, 16);
            this.layoutControlItemY.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItemY_CustomDraw);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditZ;
            this.layoutControlItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("layoutControlItem1.ImageOptions.SvgImage")));
            this.layoutControlItem1.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(95, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(177, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Z";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(29, 16);
            this.layoutControlItem1.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItemZ_CustomDraw);
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("unlock", "image://svgimages/icon builder/security_unlock.svg");
            this.svgImageCollection1.Add("lock", "image://svgimages/outlook inspired/private.svg");
            // 
            // FormPoint3DDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 73);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPoint3DDynamicInput";
            this.Text = "Dynamic input(ESC : Unlock)";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit textEditX;
        private DevExpress.XtraEditors.TextEdit textEditY;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemY;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraEditors.TextEdit textEditZ;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}