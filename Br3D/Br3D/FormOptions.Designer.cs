
namespace Br3D
{
    partial class FormOptions
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
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageColor = new DevExpress.XtraTab.XtraTabPage();
            this.groupControlBackground2D = new DevExpress.XtraEditors.GroupControl();
            this.labelControlAll = new DevExpress.XtraEditors.LabelControl();
            this.colorPickEditBackground2D = new DevExpress.XtraEditors.ColorPickEdit();
            this.groupControlBackground = new DevExpress.XtraEditors.GroupControl();
            this.labelControlBackgroundBottom = new DevExpress.XtraEditors.LabelControl();
            this.labelControlBackgroundTop = new DevExpress.XtraEditors.LabelControl();
            this.colorPickEditBackgroundBottom = new DevExpress.XtraEditors.ColorPickEdit();
            this.colorPickEditBackgroundTop = new DevExpress.XtraEditors.ColorPickEdit();
            this.xtraTabPageGeneral = new DevExpress.XtraTab.XtraTabPage();
            this.groupControlSaveImage = new DevExpress.XtraEditors.GroupControl();
            this.checkEditSaveImageWithBackground = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditSaveImageWithUI = new DevExpress.XtraEditors.CheckEdit();
            this.xtraTabPageFileAssociation = new DevExpress.XtraTab.XtraTabPage();
            this.controlFileAssociation1 = new Br3D.ControlFileAssociation();
            this.simpleButtonDefault = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPageDrawing = new DevExpress.XtraTab.XtraTabPage();
            this.groupControlDrawingScale = new DevExpress.XtraEditors.GroupControl();
            this.labelControlLineTypeScale = new DevExpress.XtraEditors.LabelControl();
            this.textEditLineTypeScale = new DevExpress.XtraEditors.TextEdit();
            this.optionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBackground2D)).BeginInit();
            this.groupControlBackground2D.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditBackground2D.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBackground)).BeginInit();
            this.groupControlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditBackgroundBottom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditBackgroundTop.Properties)).BeginInit();
            this.xtraTabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSaveImage)).BeginInit();
            this.groupControlSaveImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaveImageWithBackground.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaveImageWithUI.Properties)).BeginInit();
            this.xtraTabPageFileAssociation.SuspendLayout();
            this.xtraTabPageDrawing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDrawingScale)).BeginInit();
            this.groupControlDrawingScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLineTypeScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCancel.Location = new System.Drawing.Point(232, 368);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonCancel.TabIndex = 0;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOk.Location = new System.Drawing.Point(151, 368);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOk.TabIndex = 1;
            this.simpleButtonOk.Text = "OK";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageDrawing;
            this.xtraTabControl1.Size = new System.Drawing.Size(295, 350);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDrawing,
            this.xtraTabPageColor,
            this.xtraTabPageGeneral,
            this.xtraTabPageFileAssociation});
            // 
            // xtraTabPageColor
            // 
            this.xtraTabPageColor.Controls.Add(this.groupControlBackground2D);
            this.xtraTabPageColor.Controls.Add(this.groupControlBackground);
            this.xtraTabPageColor.Name = "xtraTabPageColor";
            this.xtraTabPageColor.Size = new System.Drawing.Size(293, 323);
            this.xtraTabPageColor.Text = "Color";
            // 
            // groupControlBackground2D
            // 
            this.groupControlBackground2D.Controls.Add(this.labelControlAll);
            this.groupControlBackground2D.Controls.Add(this.colorPickEditBackground2D);
            this.groupControlBackground2D.Location = new System.Drawing.Point(14, 116);
            this.groupControlBackground2D.Name = "groupControlBackground2D";
            this.groupControlBackground2D.Size = new System.Drawing.Size(264, 67);
            this.groupControlBackground2D.TabIndex = 4;
            this.groupControlBackground2D.Text = "Background(2D)";
            // 
            // labelControlAll
            // 
            this.labelControlAll.Location = new System.Drawing.Point(8, 34);
            this.labelControlAll.Name = "labelControlAll";
            this.labelControlAll.Size = new System.Drawing.Size(14, 15);
            this.labelControlAll.TabIndex = 2;
            this.labelControlAll.Text = "All";
            // 
            // colorPickEditBackground2D
            // 
            this.colorPickEditBackground2D.EditValue = System.Drawing.Color.Empty;
            this.colorPickEditBackground2D.Location = new System.Drawing.Point(85, 31);
            this.colorPickEditBackground2D.Name = "colorPickEditBackground2D";
            this.colorPickEditBackground2D.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEditBackground2D.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditBackground2D.Size = new System.Drawing.Size(100, 22);
            this.colorPickEditBackground2D.TabIndex = 0;
            // 
            // groupControlBackground
            // 
            this.groupControlBackground.Controls.Add(this.labelControlBackgroundBottom);
            this.groupControlBackground.Controls.Add(this.labelControlBackgroundTop);
            this.groupControlBackground.Controls.Add(this.colorPickEditBackgroundBottom);
            this.groupControlBackground.Controls.Add(this.colorPickEditBackgroundTop);
            this.groupControlBackground.Location = new System.Drawing.Point(14, 12);
            this.groupControlBackground.Name = "groupControlBackground";
            this.groupControlBackground.Size = new System.Drawing.Size(264, 98);
            this.groupControlBackground.TabIndex = 1;
            this.groupControlBackground.Text = "Background(3D)";
            // 
            // labelControlBackgroundBottom
            // 
            this.labelControlBackgroundBottom.Location = new System.Drawing.Point(8, 66);
            this.labelControlBackgroundBottom.Name = "labelControlBackgroundBottom";
            this.labelControlBackgroundBottom.Size = new System.Drawing.Size(40, 15);
            this.labelControlBackgroundBottom.TabIndex = 3;
            this.labelControlBackgroundBottom.Text = "Bottom";
            // 
            // labelControlBackgroundTop
            // 
            this.labelControlBackgroundTop.Location = new System.Drawing.Point(8, 34);
            this.labelControlBackgroundTop.Name = "labelControlBackgroundTop";
            this.labelControlBackgroundTop.Size = new System.Drawing.Size(20, 15);
            this.labelControlBackgroundTop.TabIndex = 2;
            this.labelControlBackgroundTop.Text = "Top";
            // 
            // colorPickEditBackgroundBottom
            // 
            this.colorPickEditBackgroundBottom.EditValue = System.Drawing.Color.Empty;
            this.colorPickEditBackgroundBottom.Location = new System.Drawing.Point(85, 63);
            this.colorPickEditBackgroundBottom.Name = "colorPickEditBackgroundBottom";
            this.colorPickEditBackgroundBottom.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEditBackgroundBottom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditBackgroundBottom.Size = new System.Drawing.Size(100, 22);
            this.colorPickEditBackgroundBottom.TabIndex = 1;
            // 
            // colorPickEditBackgroundTop
            // 
            this.colorPickEditBackgroundTop.EditValue = System.Drawing.Color.Empty;
            this.colorPickEditBackgroundTop.Location = new System.Drawing.Point(85, 31);
            this.colorPickEditBackgroundTop.Name = "colorPickEditBackgroundTop";
            this.colorPickEditBackgroundTop.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEditBackgroundTop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditBackgroundTop.Size = new System.Drawing.Size(100, 22);
            this.colorPickEditBackgroundTop.TabIndex = 0;
            // 
            // xtraTabPageGeneral
            // 
            this.xtraTabPageGeneral.Controls.Add(this.groupControlSaveImage);
            this.xtraTabPageGeneral.Name = "xtraTabPageGeneral";
            this.xtraTabPageGeneral.Size = new System.Drawing.Size(293, 323);
            this.xtraTabPageGeneral.Text = "General";
            // 
            // groupControlSaveImage
            // 
            this.groupControlSaveImage.Controls.Add(this.checkEditSaveImageWithBackground);
            this.groupControlSaveImage.Controls.Add(this.checkEditSaveImageWithUI);
            this.groupControlSaveImage.Location = new System.Drawing.Point(13, 14);
            this.groupControlSaveImage.Name = "groupControlSaveImage";
            this.groupControlSaveImage.Size = new System.Drawing.Size(269, 100);
            this.groupControlSaveImage.TabIndex = 2;
            this.groupControlSaveImage.Text = "Save Image";
            // 
            // checkEditSaveImageWithBackground
            // 
            this.checkEditSaveImageWithBackground.Location = new System.Drawing.Point(5, 61);
            this.checkEditSaveImageWithBackground.Name = "checkEditSaveImageWithBackground";
            this.checkEditSaveImageWithBackground.Properties.Caption = "Background";
            this.checkEditSaveImageWithBackground.Size = new System.Drawing.Size(170, 20);
            this.checkEditSaveImageWithBackground.TabIndex = 1;
            // 
            // checkEditSaveImageWithUI
            // 
            this.checkEditSaveImageWithUI.Location = new System.Drawing.Point(5, 35);
            this.checkEditSaveImageWithUI.Name = "checkEditSaveImageWithUI";
            this.checkEditSaveImageWithUI.Properties.Caption = "UI";
            this.checkEditSaveImageWithUI.Size = new System.Drawing.Size(170, 20);
            this.checkEditSaveImageWithUI.TabIndex = 0;
            // 
            // xtraTabPageFileAssociation
            // 
            this.xtraTabPageFileAssociation.Controls.Add(this.controlFileAssociation1);
            this.xtraTabPageFileAssociation.Name = "xtraTabPageFileAssociation";
            this.xtraTabPageFileAssociation.Size = new System.Drawing.Size(293, 323);
            this.xtraTabPageFileAssociation.Text = "File Association";
            // 
            // controlFileAssociation1
            // 
            this.controlFileAssociation1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.controlFileAssociation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFileAssociation1.Location = new System.Drawing.Point(0, 0);
            this.controlFileAssociation1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.controlFileAssociation1.Name = "controlFileAssociation1";
            this.controlFileAssociation1.Padding = new System.Windows.Forms.Padding(5);
            this.controlFileAssociation1.Size = new System.Drawing.Size(293, 323);
            this.controlFileAssociation1.TabIndex = 0;
            // 
            // simpleButtonDefault
            // 
            this.simpleButtonDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButtonDefault.Location = new System.Drawing.Point(13, 368);
            this.simpleButtonDefault.Name = "simpleButtonDefault";
            this.simpleButtonDefault.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonDefault.TabIndex = 3;
            this.simpleButtonDefault.Text = "Default";
            this.simpleButtonDefault.Click += new System.EventHandler(this.simpleButtonDefault_Click);
            // 
            // xtraTabPageDrawing
            // 
            this.xtraTabPageDrawing.Controls.Add(this.groupControlDrawingScale);
            this.xtraTabPageDrawing.Name = "xtraTabPageDrawing";
            this.xtraTabPageDrawing.Size = new System.Drawing.Size(293, 323);
            this.xtraTabPageDrawing.Text = "Drawing";
            // 
            // groupControlDrawingScale
            // 
            this.groupControlDrawingScale.Controls.Add(this.textEditLineTypeScale);
            this.groupControlDrawingScale.Controls.Add(this.labelControlLineTypeScale);
            this.groupControlDrawingScale.Location = new System.Drawing.Point(13, 15);
            this.groupControlDrawingScale.Name = "groupControlDrawingScale";
            this.groupControlDrawingScale.Size = new System.Drawing.Size(266, 71);
            this.groupControlDrawingScale.TabIndex = 0;
            this.groupControlDrawingScale.Text = "Scale";
            // 
            // labelControlLineTypeScale
            // 
            this.labelControlLineTypeScale.Location = new System.Drawing.Point(18, 35);
            this.labelControlLineTypeScale.Name = "labelControlLineTypeScale";
            this.labelControlLineTypeScale.Size = new System.Drawing.Size(74, 15);
            this.labelControlLineTypeScale.TabIndex = 0;
            this.labelControlLineTypeScale.Text = "Linetype scale";
            // 
            // textEditLineTypeScale
            // 
            this.textEditLineTypeScale.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.optionsBindingSource, "curLinetypeScale", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.textEditLineTypeScale.EditValue = "1";
            this.textEditLineTypeScale.Location = new System.Drawing.Point(152, 32);
            this.textEditLineTypeScale.Name = "textEditLineTypeScale";
            this.textEditLineTypeScale.Properties.DisplayFormat.FormatString = "0.00";
            this.textEditLineTypeScale.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEditLineTypeScale.Size = new System.Drawing.Size(100, 22);
            this.textEditLineTypeScale.TabIndex = 1;
            // 
            // optionsBindingSource
            // 
            this.optionsBindingSource.DataSource = typeof(hanee.ThreeD.Options);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 403);
            this.Controls.Add(this.simpleButtonDefault);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.simpleButtonOk);
            this.Controls.Add(this.simpleButtonCancel);
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBackground2D)).EndInit();
            this.groupControlBackground2D.ResumeLayout(false);
            this.groupControlBackground2D.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditBackground2D.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlBackground)).EndInit();
            this.groupControlBackground.ResumeLayout(false);
            this.groupControlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditBackgroundBottom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditBackgroundTop.Properties)).EndInit();
            this.xtraTabPageGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSaveImage)).EndInit();
            this.groupControlSaveImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaveImageWithBackground.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditSaveImageWithUI.Properties)).EndInit();
            this.xtraTabPageFileAssociation.ResumeLayout(false);
            this.xtraTabPageDrawing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDrawingScale)).EndInit();
            this.groupControlDrawingScale.ResumeLayout(false);
            this.groupControlDrawingScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLineTypeScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageColor;
        private DevExpress.XtraEditors.GroupControl groupControlBackground;
        private DevExpress.XtraEditors.LabelControl labelControlBackgroundBottom;
        private DevExpress.XtraEditors.LabelControl labelControlBackgroundTop;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditBackgroundBottom;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditBackgroundTop;
        private DevExpress.XtraEditors.GroupControl groupControlBackground2D;
        private DevExpress.XtraEditors.LabelControl labelControlAll;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditBackground2D;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDefault;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageFileAssociation;
        private ControlFileAssociation controlFileAssociation1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageGeneral;
        private DevExpress.XtraEditors.GroupControl groupControlSaveImage;
        private DevExpress.XtraEditors.CheckEdit checkEditSaveImageWithBackground;
        private DevExpress.XtraEditors.CheckEdit checkEditSaveImageWithUI;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDrawing;
        private DevExpress.XtraEditors.GroupControl groupControlDrawingScale;
        private DevExpress.XtraEditors.TextEdit textEditLineTypeScale;
        private DevExpress.XtraEditors.LabelControl labelControlLineTypeScale;
        private System.Windows.Forms.BindingSource optionsBindingSource;
    }
}