
namespace hanee.Terrain.Tool
{
    partial class FormUpDownTerrainOptions
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
            this.spinEditHeight = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditUpDown = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spinEditRadius = new DevExpress.XtraEditors.SpinEdit();
            this.checkEditAutoUpdateColor = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditUpDown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRadius.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAutoUpdateColor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spinEditHeight
            // 
            this.spinEditHeight.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditHeight.Location = new System.Drawing.Point(74, 12);
            this.spinEditHeight.Name = "spinEditHeight";
            this.spinEditHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditHeight.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditHeight.Properties.MinValue = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.spinEditHeight.Size = new System.Drawing.Size(75, 22);
            this.spinEditHeight.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 15);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Height";
            // 
            // checkEditUpDown
            // 
            this.checkEditUpDown.EditValue = true;
            this.checkEditUpDown.Location = new System.Drawing.Point(12, 68);
            this.checkEditUpDown.Name = "checkEditUpDown";
            this.checkEditUpDown.Properties.Caption = "Up";
            this.checkEditUpDown.Size = new System.Drawing.Size(75, 20);
            this.checkEditUpDown.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 15);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Radius";
            // 
            // spinEditRadius
            // 
            this.spinEditRadius.EditValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.spinEditRadius.Location = new System.Drawing.Point(74, 40);
            this.spinEditRadius.Name = "spinEditRadius";
            this.spinEditRadius.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditRadius.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditRadius.Properties.MinValue = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.spinEditRadius.Size = new System.Drawing.Size(75, 22);
            this.spinEditRadius.TabIndex = 3;
            // 
            // checkEditAutoUpdateColor
            // 
            this.checkEditAutoUpdateColor.EditValue = true;
            this.checkEditAutoUpdateColor.Location = new System.Drawing.Point(12, 94);
            this.checkEditAutoUpdateColor.Name = "checkEditAutoUpdateColor";
            this.checkEditAutoUpdateColor.Properties.Caption = "Update color";
            this.checkEditAutoUpdateColor.Size = new System.Drawing.Size(105, 20);
            this.checkEditAutoUpdateColor.TabIndex = 5;
            // 
            // FormUpDownTerrainOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 132);
            this.ControlBox = false;
            this.Controls.Add(this.checkEditAutoUpdateColor);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.spinEditRadius);
            this.Controls.Add(this.checkEditUpDown);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.spinEditHeight);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUpDownTerrainOptions";
            this.Text = "Up/Down options";
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditUpDown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRadius.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAutoUpdateColor.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.SpinEdit spinEditHeight;
        public DevExpress.XtraEditors.CheckEdit checkEditUpDown;
        public DevExpress.XtraEditors.SpinEdit spinEditRadius;
        public DevExpress.XtraEditors.CheckEdit checkEditAutoUpdateColor;
    }
}