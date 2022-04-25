
namespace hanee.ThreeD
{
    partial class ControlDistanceAngleDynamicInput
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
            this.controlDynamicInputEdit1 = new hanee.ThreeD.ControlDynamicInputEdit();
            this.controlDynamicInputEdit2 = new hanee.ThreeD.ControlDynamicInputEdit();
            this.SuspendLayout();
            // 
            // controlDynamicInputEdit1
            // 
            this.controlDynamicInputEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDynamicInputEdit1.Location = new System.Drawing.Point(0, 0);
            this.controlDynamicInputEdit1.Name = "controlDynamicInputEdit1";
            this.controlDynamicInputEdit1.Size = new System.Drawing.Size(173, 21);
            this.controlDynamicInputEdit1.TabIndex = 0;
            // 
            // controlDynamicInputEdit2
            // 
            this.controlDynamicInputEdit2.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDynamicInputEdit2.Location = new System.Drawing.Point(0, 21);
            this.controlDynamicInputEdit2.Name = "controlDynamicInputEdit2";
            this.controlDynamicInputEdit2.Size = new System.Drawing.Size(173, 21);
            this.controlDynamicInputEdit2.TabIndex = 1;
            // 
            // ControlDistanceAngleDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlDynamicInputEdit2);
            this.Controls.Add(this.controlDynamicInputEdit1);
            this.Name = "ControlDistanceAngleDynamicInput";
            this.Size = new System.Drawing.Size(173, 43);
            this.ResumeLayout(false);

        }

        #endregion

        public ControlDynamicInputEdit controlDynamicInputEdit1;
        public ControlDynamicInputEdit controlDynamicInputEdit2;
    }
}
