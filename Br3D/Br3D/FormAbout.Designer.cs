
namespace Br3D
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.labelControlName = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.hyperlinkLabelControl1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlVersion = new DevExpress.XtraEditors.LabelControl();
            this.labelControlLT = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControlName
            // 
            this.labelControlName.AllowHtmlString = true;
            this.labelControlName.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlName.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelControlName.Appearance.Options.UseFont = true;
            this.labelControlName.Appearance.Options.UseForeColor = true;
            this.labelControlName.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControlName.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("labelControlName.ImageOptions.SvgImage")));
            this.labelControlName.Location = new System.Drawing.Point(89, 16);
            this.labelControlName.Name = "labelControlName";
            this.labelControlName.Size = new System.Drawing.Size(147, 65);
            this.labelControlName.TabIndex = 0;
            this.labelControlName.Text = "   Br3D";
            this.labelControlName.Click += new System.EventHandler(this.labelControlName_Click);
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonClose.Location = new System.Drawing.Point(287, 227);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonClose.TabIndex = 1;
            this.simpleButtonClose.Text = "Close";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // separatorControl1
            // 
            this.separatorControl1.AutoSizeMode = true;
            this.separatorControl1.Location = new System.Drawing.Point(28, 100);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(298, 19);
            this.separatorControl1.TabIndex = 2;
            // 
            // hyperlinkLabelControl1
            // 
            this.hyperlinkLabelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hyperlinkLabelControl1.Location = new System.Drawing.Point(28, 228);
            this.hyperlinkLabelControl1.Name = "hyperlinkLabelControl1";
            this.hyperlinkLabelControl1.Size = new System.Drawing.Size(197, 15);
            this.hyperlinkLabelControl1.TabIndex = 3;
            this.hyperlinkLabelControl1.Text = "http://hileejaeho.cafe24.com/kr-br3d";
            this.hyperlinkLabelControl1.Click += new System.EventHandler(this.hyperlinkLabelControl1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 123);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(337, 90);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "본 소프트웨어는 개인 사용자는 물론\r\n기업/(비)영리법인/공공기관/교육기관/인터넷PC방 등 어디서나\r\n자유롭게 사용 및 배포를 할 수 있습니다.\r" +
    "\n\r\nCopyright (C) 2022 ~ present HaneeSoft corporation. \r\nAll right reserved.";
            // 
            // labelControlVersion
            // 
            this.labelControlVersion.Location = new System.Drawing.Point(134, 80);
            this.labelControlVersion.Name = "labelControlVersion";
            this.labelControlVersion.Size = new System.Drawing.Size(75, 15);
            this.labelControlVersion.TabIndex = 5;
            this.labelControlVersion.Text = "Version 1.0.0.0";
            // 
            // labelControlLT
            // 
            this.labelControlLT.AllowHtmlString = true;
            this.labelControlLT.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlLT.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControlLT.Appearance.Options.UseFont = true;
            this.labelControlLT.Appearance.Options.UseForeColor = true;
            this.labelControlLT.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControlLT.Location = new System.Drawing.Point(251, 16);
            this.labelControlLT.Name = "labelControlLT";
            this.labelControlLT.Size = new System.Drawing.Size(49, 65);
            this.labelControlLT.TabIndex = 6;
            this.labelControlLT.Text = "LT";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 262);
            this.Controls.Add(this.labelControlLT);
            this.Controls.Add(this.labelControlVersion);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.hyperlinkLabelControl1);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.simpleButtonClose);
            this.Controls.Add(this.labelControlName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Br3D";
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControlName;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyperlinkLabelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControlVersion;
        private DevExpress.XtraEditors.LabelControl labelControlLT;
    }
}