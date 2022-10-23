
namespace hanee.Terrain.Tool
{
    partial class FormCreateGrid
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
            this.labelControlResolution = new DevExpress.XtraEditors.LabelControl();
            this.labelControlXSize = new DevExpress.XtraEditors.LabelControl();
            this.labelControlYSize = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.textEditResolution = new DevExpress.XtraEditors.TextEdit();
            this.textEditXSize = new DevExpress.XtraEditors.TextEdit();
            this.textEditYSize = new DevExpress.XtraEditors.TextEdit();
            this.textEditY = new DevExpress.XtraEditors.TextEdit();
            this.textEditX = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlLeftBottom = new DevExpress.XtraEditors.LabelControl();
            this.groupControlPosition = new DevExpress.XtraEditors.GroupControl();
            this.labelControlUnits = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditResolution.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditXSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditYSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPosition)).BeginInit();
            this.groupControlPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControlResolution
            // 
            this.labelControlResolution.Location = new System.Drawing.Point(26, 120);
            this.labelControlResolution.Name = "labelControlResolution";
            this.labelControlResolution.Size = new System.Drawing.Size(56, 14);
            this.labelControlResolution.TabIndex = 0;
            this.labelControlResolution.Text = "Resolution";
            // 
            // labelControlXSize
            // 
            this.labelControlXSize.Location = new System.Drawing.Point(26, 146);
            this.labelControlXSize.Name = "labelControlXSize";
            this.labelControlXSize.Size = new System.Drawing.Size(32, 14);
            this.labelControlXSize.TabIndex = 1;
            this.labelControlXSize.Text = "X Size";
            // 
            // labelControlYSize
            // 
            this.labelControlYSize.Location = new System.Drawing.Point(26, 172);
            this.labelControlYSize.Name = "labelControlYSize";
            this.labelControlYSize.Size = new System.Drawing.Size(33, 14);
            this.labelControlYSize.TabIndex = 2;
            this.labelControlYSize.Text = "Y Size";
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCancel.Location = new System.Drawing.Point(149, 214);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 3;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOk.Location = new System.Drawing.Point(68, 214);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOk.TabIndex = 4;
            this.simpleButtonOk.Text = "Ok";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // textEditResolution
            // 
            this.textEditResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditResolution.EditValue = "0.5";
            this.textEditResolution.Location = new System.Drawing.Point(119, 117);
            this.textEditResolution.Name = "textEditResolution";
            this.textEditResolution.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditResolution.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditResolution.Size = new System.Drawing.Size(74, 20);
            this.textEditResolution.TabIndex = 5;
            // 
            // textEditXSize
            // 
            this.textEditXSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditXSize.EditValue = "100";
            this.textEditXSize.Location = new System.Drawing.Point(119, 143);
            this.textEditXSize.Name = "textEditXSize";
            this.textEditXSize.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditXSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditXSize.Size = new System.Drawing.Size(74, 20);
            this.textEditXSize.TabIndex = 6;
            // 
            // textEditYSize
            // 
            this.textEditYSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditYSize.EditValue = "100";
            this.textEditYSize.Location = new System.Drawing.Point(119, 169);
            this.textEditYSize.Name = "textEditYSize";
            this.textEditYSize.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditYSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditYSize.Size = new System.Drawing.Size(74, 20);
            this.textEditYSize.TabIndex = 7;
            // 
            // textEditY
            // 
            this.textEditY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditY.EditValue = "0";
            this.textEditY.Location = new System.Drawing.Point(107, 58);
            this.textEditY.Name = "textEditY";
            this.textEditY.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditY.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditY.Size = new System.Drawing.Size(100, 20);
            this.textEditY.TabIndex = 11;
            // 
            // textEditX
            // 
            this.textEditX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditX.EditValue = "0";
            this.textEditX.Location = new System.Drawing.Point(107, 32);
            this.textEditX.Name = "textEditX";
            this.textEditX.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditX.Size = new System.Drawing.Size(100, 20);
            this.textEditX.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 61);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 14);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Y(Bottom)";
            // 
            // labelControlLeftBottom
            // 
            this.labelControlLeftBottom.Location = new System.Drawing.Point(14, 35);
            this.labelControlLeftBottom.Name = "labelControlLeftBottom";
            this.labelControlLeftBottom.Size = new System.Drawing.Size(39, 14);
            this.labelControlLeftBottom.TabIndex = 8;
            this.labelControlLeftBottom.Text = "X(Left)";
            // 
            // groupControlPosition
            // 
            this.groupControlPosition.Controls.Add(this.labelControlLeftBottom);
            this.groupControlPosition.Controls.Add(this.textEditY);
            this.groupControlPosition.Controls.Add(this.labelControl1);
            this.groupControlPosition.Controls.Add(this.textEditX);
            this.groupControlPosition.Location = new System.Drawing.Point(12, 12);
            this.groupControlPosition.Name = "groupControlPosition";
            this.groupControlPosition.Size = new System.Drawing.Size(212, 91);
            this.groupControlPosition.TabIndex = 12;
            this.groupControlPosition.Text = "Coordinates";
            // 
            // labelControlUnits
            // 
            this.labelControlUnits.Location = new System.Drawing.Point(199, 120);
            this.labelControlUnits.Name = "labelControlUnits";
            this.labelControlUnits.Size = new System.Drawing.Size(10, 14);
            this.labelControlUnits.TabIndex = 13;
            this.labelControlUnits.Text = "m";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(199, 146);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(13, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Ea";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(199, 172);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(13, 14);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Ea";
            // 
            // FormCreateGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 249);
            this.ControlBox = false;
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControlUnits);
            this.Controls.Add(this.groupControlPosition);
            this.Controls.Add(this.textEditYSize);
            this.Controls.Add(this.textEditXSize);
            this.Controls.Add(this.textEditResolution);
            this.Controls.Add(this.simpleButtonOk);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.labelControlYSize);
            this.Controls.Add(this.labelControlXSize);
            this.Controls.Add(this.labelControlResolution);
            this.Name = "FormCreateGrid";
            this.Text = "Create grid";
            this.Load += new System.EventHandler(this.FormCreateGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditResolution.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditXSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditYSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPosition)).EndInit();
            this.groupControlPosition.ResumeLayout(false);
            this.groupControlPosition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControlResolution;
        private DevExpress.XtraEditors.LabelControl labelControlXSize;
        private DevExpress.XtraEditors.LabelControl labelControlYSize;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControlLeftBottom;
        private DevExpress.XtraEditors.GroupControl groupControlPosition;
        public DevExpress.XtraEditors.TextEdit textEditResolution;
        public DevExpress.XtraEditors.TextEdit textEditXSize;
        public DevExpress.XtraEditors.TextEdit textEditYSize;
        public DevExpress.XtraEditors.TextEdit textEditY;
        public DevExpress.XtraEditors.TextEdit textEditX;
        private DevExpress.XtraEditors.LabelControl labelControlUnits;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}