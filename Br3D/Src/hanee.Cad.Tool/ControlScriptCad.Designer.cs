
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlScriptCad));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.rowRadius = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowStartPoint = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowEndPoint = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowPoints = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCenterPoint = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowWidth = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButtonRun = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.simpleButtonHelp = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.propertyGridControl1);
            this.panelControl1.Controls.Add(this.comboBoxEdit1);
            this.panelControl1.Controls.Add(this.simpleButtonRun);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 102);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(247, 119);
            this.panelControl1.TabIndex = 4;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.AutoGenerateRows = false;
            this.propertyGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(0, 20);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.OptionsBehavior.AllowSort = false;
            this.propertyGridControl1.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.propertyGridControl1.OptionsView.ShowRootCategories = false;
            this.propertyGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowRadius,
            this.rowStartPoint,
            this.rowEndPoint,
            this.rowPoints,
            this.rowCenterPoint,
            this.rowWidth,
            this.rowColor});
            this.propertyGridControl1.Size = new System.Drawing.Size(247, 76);
            this.propertyGridControl1.TabIndex = 5;
            // 
            // rowRadius
            // 
            this.rowRadius.Name = "rowRadius";
            this.rowRadius.Properties.Caption = "Radius";
            this.rowRadius.Properties.FieldName = "radius";
            // 
            // rowStartPoint
            // 
            this.rowStartPoint.Name = "rowStartPoint";
            this.rowStartPoint.Properties.Caption = "Start Point";
            this.rowStartPoint.Properties.FieldName = "startPoint";
            // 
            // rowEndPoint
            // 
            this.rowEndPoint.Name = "rowEndPoint";
            this.rowEndPoint.Properties.Caption = "End Point";
            this.rowEndPoint.Properties.FieldName = "endPoint";
            // 
            // rowPoints
            // 
            this.rowPoints.Name = "rowPoints";
            this.rowPoints.Properties.Caption = "Points";
            this.rowPoints.Properties.FieldName = "points";
            // 
            // rowCenterPoint
            // 
            this.rowCenterPoint.Name = "rowCenterPoint";
            this.rowCenterPoint.Properties.Caption = "Center Point";
            this.rowCenterPoint.Properties.FieldName = "centerPoint";
            // 
            // rowWidth
            // 
            this.rowWidth.Name = "rowWidth";
            this.rowWidth.Properties.Caption = "Width";
            this.rowWidth.Properties.FieldName = "width";
            // 
            // rowColor
            // 
            this.rowColor.Name = "rowColor";
            this.rowColor.Properties.Caption = "Color";
            this.rowColor.Properties.FieldName = "color";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxEdit1.Location = new System.Drawing.Point(0, 0);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(247, 20);
            this.comboBoxEdit1.TabIndex = 4;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.ComboBoxEdit1_SelectedIndexChanged);
            // 
            // simpleButtonRun
            // 
            this.simpleButtonRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButtonRun.Location = new System.Drawing.Point(0, 96);
            this.simpleButtonRun.Name = "simpleButtonRun";
            this.simpleButtonRun.Size = new System.Drawing.Size(247, 23);
            this.simpleButtonRun.TabIndex = 1;
            this.simpleButtonRun.Text = "Run";
            this.simpleButtonRun.Click += new System.EventHandler(this.simpleButtonRun_Click);
            // 
            // labelControlTitle
            // 
            this.labelControlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlTitle.Location = new System.Drawing.Point(0, 0);
            this.labelControlTitle.Name = "labelControlTitle";
            this.labelControlTitle.Padding = new System.Windows.Forms.Padding(3);
            this.labelControlTitle.Size = new System.Drawing.Size(37, 20);
            this.labelControlTitle.TabIndex = 5;
            this.labelControlTitle.Text = "Script";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(247, 82);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "x = 10 y = 10 z = 10 \nx = 20 y = 20 z = 10\n";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // simpleButtonHelp
            // 
            this.simpleButtonHelp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonHelp.ImageOptions.SvgImage")));
            this.simpleButtonHelp.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.simpleButtonHelp.Location = new System.Drawing.Point(227, -1);
            this.simpleButtonHelp.Name = "simpleButtonHelp";
            this.simpleButtonHelp.Size = new System.Drawing.Size(21, 21);
            this.simpleButtonHelp.TabIndex = 7;
            this.simpleButtonHelp.Click += new System.EventHandler(this.simpleButtonHelp_Click);
            // 
            // ControlScriptCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.simpleButtonHelp);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.labelControlTitle);
            this.Controls.Add(this.panelControl1);
            this.Name = "ControlScriptCad";
            this.Size = new System.Drawing.Size(247, 221);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRun;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControlTitle;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRadius;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowStartPoint;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowEndPoint;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowPoints;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCenterPoint;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowWidth;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowColor;
        private DevExpress.XtraEditors.SimpleButton simpleButtonHelp;
    }
}
