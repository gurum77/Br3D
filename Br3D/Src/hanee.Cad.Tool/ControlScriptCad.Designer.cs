
namespace hanee.Cad.Tool
{
    partial class ControlScriptCad
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonRun = new DevExpress.XtraEditors.SimpleButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.propertyGridControl1);
            this.panelControl1.Controls.Add(this.comboBoxEdit1);
            this.panelControl1.Controls.Add(this.simpleButtonRun);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 73);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(249, 110);
            this.panelControl1.TabIndex = 4;
            // 
            // simpleButtonRun
            // 
            this.simpleButtonRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButtonRun.Location = new System.Drawing.Point(2, 85);
            this.simpleButtonRun.Name = "simpleButtonRun";
            this.simpleButtonRun.Size = new System.Drawing.Size(245, 23);
            this.simpleButtonRun.TabIndex = 1;
            this.simpleButtonRun.Text = "Run";
            this.simpleButtonRun.Click += new System.EventHandler(this.simpleButtonRun_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(249, 73);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "x = 10\ny = 10\nz = 10";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxEdit1.Location = new System.Drawing.Point(2, 2);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(245, 20);
            this.comboBoxEdit1.TabIndex = 4;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(2, 22);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.propertyGridControl1.Size = new System.Drawing.Size(245, 63);
            this.propertyGridControl1.TabIndex = 5;
            // 
            // ControlScriptCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ControlScriptCad";
            this.Size = new System.Drawing.Size(249, 183);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRun;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    }
}
