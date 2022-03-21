
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditX = new DevExpress.XtraEditors.TextEdit();
            this.textEditY = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemX = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemY = new DevExpress.XtraLayout.LayoutControlItem();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
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
            this.layoutControl1.Size = new System.Drawing.Size(360, 49);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEditX
            // 
            this.textEditX.Location = new System.Drawing.Point(32, 12);
            this.textEditX.Name = "textEditX";
            this.textEditX.Size = new System.Drawing.Size(142, 20);
            this.textEditX.StyleController = this.layoutControl1;
            this.textEditX.TabIndex = 4;
            this.textEditX.EditValueChanged += new System.EventHandler(this.textEditX_EditValueChanged);
            // 
            // textEditY
            // 
            this.textEditY.Location = new System.Drawing.Point(198, 12);
            this.textEditY.Name = "textEditY";
            this.textEditY.Size = new System.Drawing.Size(150, 20);
            this.textEditY.StyleController = this.layoutControl1;
            this.textEditY.TabIndex = 5;
            this.textEditY.EditValueChanged += new System.EventHandler(this.textEditY_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemX,
            this.layoutControlItemY});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(360, 49);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemX
            // 
            this.layoutControlItemX.Control = this.textEditX;
            this.layoutControlItemX.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.layoutControlItemX.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemX.Name = "layoutControlItemX";
            this.layoutControlItemX.Size = new System.Drawing.Size(166, 29);
            this.layoutControlItemX.Text = "X";
            this.layoutControlItemX.TextSize = new System.Drawing.Size(8, 14);
            this.layoutControlItemX.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItemX_CustomDraw);
            // 
            // layoutControlItemY
            // 
            this.layoutControlItemY.Control = this.textEditY;
            this.layoutControlItemY.Location = new System.Drawing.Point(166, 0);
            this.layoutControlItemY.Name = "layoutControlItemY";
            this.layoutControlItemY.Size = new System.Drawing.Size(174, 29);
            this.layoutControlItemY.Text = "Y";
            this.layoutControlItemY.TextSize = new System.Drawing.Size(8, 14);
            this.layoutControlItemY.CustomDraw += new System.EventHandler<DevExpress.XtraLayout.ItemCustomDrawEventArgs>(this.layoutControlItem2_CustomDraw);
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("security_unlock", "image://svgimages/icon builder/security_unlock.svg");
            this.svgImageCollection1.Add("private", "image://svgimages/outlook inspired/private.svg");
            // 
            // FormDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 49);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "FormDynamicInput";
            this.Text = "Dynamic input(ESC : Unlock)";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemY)).EndInit();
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
    }
}