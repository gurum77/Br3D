
namespace hanee.ThreeD
{
    partial class ControlXyzDynamicInput
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
            this.components = new System.ComponentModel.Container();
            this.controlDynamicInputEdit3 = new hanee.ThreeD.ControlDynamicInputEdit();
            this.controlDynamicInputEdit2 = new hanee.ThreeD.ControlDynamicInputEdit();
            this.controlDynamicInputEdit1 = new hanee.ThreeD.ControlDynamicInputEdit();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // controlDynamicInputEdit3
            // 
            this.controlDynamicInputEdit3.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDynamicInputEdit3.Location = new System.Drawing.Point(0, 42);
            this.controlDynamicInputEdit3.Name = "controlDynamicInputEdit3";
            this.controlDynamicInputEdit3.Size = new System.Drawing.Size(149, 21);
            this.controlDynamicInputEdit3.TabIndex = 12;
            // 
            // controlDynamicInputEdit2
            // 
            this.controlDynamicInputEdit2.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDynamicInputEdit2.Location = new System.Drawing.Point(0, 21);
            this.controlDynamicInputEdit2.Name = "controlDynamicInputEdit2";
            this.controlDynamicInputEdit2.Size = new System.Drawing.Size(149, 21);
            this.controlDynamicInputEdit2.TabIndex = 11;
            // 
            // controlDynamicInputEdit1
            // 
            this.controlDynamicInputEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDynamicInputEdit1.Location = new System.Drawing.Point(0, 0);
            this.controlDynamicInputEdit1.Name = "controlDynamicInputEdit1";
            this.controlDynamicInputEdit1.Size = new System.Drawing.Size(149, 21);
            this.controlDynamicInputEdit1.TabIndex = 10;
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("unlock", "image://svgimages/actions/cleartablestyle.svg");
            this.svgImageCollection1.Add("lock", "image://svgimages/outlook inspired/private.svg");
            // 
            // ControlXyzDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlDynamicInputEdit3);
            this.Controls.Add(this.controlDynamicInputEdit2);
            this.Controls.Add(this.controlDynamicInputEdit1);
            this.Name = "ControlXyzDynamicInput";
            this.Size = new System.Drawing.Size(149, 64);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ControlDynamicInputEdit controlDynamicInputEdit1;
        private ControlDynamicInputEdit controlDynamicInputEdit2;
        private ControlDynamicInputEdit controlDynamicInputEdit3;
        public DevExpress.Utils.SvgImageCollection svgImageCollection1;
    }
}
