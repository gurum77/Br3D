
namespace hanee.Terrain.Tool
{
    partial class FormCreateContour
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
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEditMinorLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditMajorLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEditMinorHeight = new DevExpress.XtraEditors.TextEdit();
            this.textEditMajorHeight = new DevExpress.XtraEditors.TextEdit();
            this.labelControlMinorContour = new DevExpress.XtraEditors.LabelControl();
            this.labelControlMajorContour = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHeight = new DevExpress.XtraEditors.LabelControl();
            this.labelControlLayer = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEditSaveAsFile = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMinorLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMajorLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMinorHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMajorHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaveAsFile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCancel.Location = new System.Drawing.Point(278, 140);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 21);
            this.simpleButtonCancel.TabIndex = 0;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOk.Location = new System.Drawing.Point(197, 140);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(75, 21);
            this.simpleButtonOk.TabIndex = 1;
            this.simpleButtonOk.Text = "Ok";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // comboBoxEditMinorLayer
            // 
            this.comboBoxEditMinorLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditMinorLayer.Location = new System.Drawing.Point(183, 48);
            this.comboBoxEditMinorLayer.Name = "comboBoxEditMinorLayer";
            this.comboBoxEditMinorLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditMinorLayer.Size = new System.Drawing.Size(153, 20);
            this.comboBoxEditMinorLayer.TabIndex = 2;
            // 
            // comboBoxEditMajorLayer
            // 
            this.comboBoxEditMajorLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditMajorLayer.Location = new System.Drawing.Point(183, 74);
            this.comboBoxEditMajorLayer.Name = "comboBoxEditMajorLayer";
            this.comboBoxEditMajorLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditMajorLayer.Size = new System.Drawing.Size(153, 20);
            this.comboBoxEditMajorLayer.TabIndex = 3;
            // 
            // textEditMinorHeight
            // 
            this.textEditMinorHeight.EditValue = "1";
            this.textEditMinorHeight.Location = new System.Drawing.Point(100, 48);
            this.textEditMinorHeight.Name = "textEditMinorHeight";
            this.textEditMinorHeight.Size = new System.Drawing.Size(47, 20);
            this.textEditMinorHeight.TabIndex = 4;
            // 
            // textEditMajorHeight
            // 
            this.textEditMajorHeight.EditValue = "5";
            this.textEditMajorHeight.Location = new System.Drawing.Point(100, 74);
            this.textEditMajorHeight.Name = "textEditMajorHeight";
            this.textEditMajorHeight.Size = new System.Drawing.Size(47, 20);
            this.textEditMajorHeight.TabIndex = 5;
            // 
            // labelControlMinorContour
            // 
            this.labelControlMinorContour.Location = new System.Drawing.Point(8, 50);
            this.labelControlMinorContour.Name = "labelControlMinorContour";
            this.labelControlMinorContour.Size = new System.Drawing.Size(76, 14);
            this.labelControlMinorContour.TabIndex = 6;
            this.labelControlMinorContour.Text = "Minor contour";
            // 
            // labelControlMajorContour
            // 
            this.labelControlMajorContour.Location = new System.Drawing.Point(8, 77);
            this.labelControlMajorContour.Name = "labelControlMajorContour";
            this.labelControlMajorContour.Size = new System.Drawing.Size(76, 14);
            this.labelControlMajorContour.TabIndex = 7;
            this.labelControlMajorContour.Text = "Major contour";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(159, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(10, 14);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "m";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(159, 50);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(10, 14);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "m";
            // 
            // labelControlHeight
            // 
            this.labelControlHeight.Location = new System.Drawing.Point(100, 25);
            this.labelControlHeight.Name = "labelControlHeight";
            this.labelControlHeight.Size = new System.Drawing.Size(36, 14);
            this.labelControlHeight.TabIndex = 10;
            this.labelControlHeight.Text = "Height";
            // 
            // labelControlLayer
            // 
            this.labelControlLayer.Location = new System.Drawing.Point(183, 25);
            this.labelControlLayer.Name = "labelControlLayer";
            this.labelControlLayer.Size = new System.Drawing.Size(29, 14);
            this.labelControlLayer.TabIndex = 11;
            this.labelControlLayer.Text = "Layer";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.labelControlHeight);
            this.groupControl1.Controls.Add(this.comboBoxEditMinorLayer);
            this.groupControl1.Controls.Add(this.comboBoxEditMajorLayer);
            this.groupControl1.Controls.Add(this.labelControlLayer);
            this.groupControl1.Controls.Add(this.textEditMinorHeight);
            this.groupControl1.Controls.Add(this.textEditMajorHeight);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControlMinorContour);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControlMajorContour);
            this.groupControl1.Location = new System.Drawing.Point(12, 11);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(341, 114);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "Options";
            // 
            // checkEditSaveAsFile
            // 
            this.checkEditSaveAsFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEditSaveAsFile.Location = new System.Drawing.Point(12, 140);
            this.checkEditSaveAsFile.Name = "checkEditSaveAsFile";
            this.checkEditSaveAsFile.Properties.Caption = "Save as file";
            this.checkEditSaveAsFile.Size = new System.Drawing.Size(95, 20);
            this.checkEditSaveAsFile.TabIndex = 15;
            // 
            // FormCreateContour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 173);
            this.Controls.Add(this.checkEditSaveAsFile);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButtonOk);
            this.Controls.Add(this.simpleButtonCancel);
            this.Name = "FormCreateContour";
            this.Text = "Create contour";
            this.Load += new System.EventHandler(this.FormCreateContour_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMinorLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMajorLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMinorHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMajorHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaveAsFile.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraEditors.LabelControl labelControlMinorContour;
        private DevExpress.XtraEditors.LabelControl labelControlMajorContour;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControlHeight;
        private DevExpress.XtraEditors.LabelControl labelControlLayer;
        public DevExpress.XtraEditors.TextEdit textEditMinorHeight;
        public DevExpress.XtraEditors.TextEdit textEditMajorHeight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditMinorLayer;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditMajorLayer;
        public DevExpress.XtraEditors.CheckEdit checkEditSaveAsFile;
    }
}