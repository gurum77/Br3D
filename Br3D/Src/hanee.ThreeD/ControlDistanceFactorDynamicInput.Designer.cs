
namespace hanee.ThreeD
{
    partial class ControlDistanceFactorDynamicInput
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
            this.SuspendLayout();
            // 
            // controlDynamicInputEdit1
            // 
            this.controlDynamicInputEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDynamicInputEdit1.Location = new System.Drawing.Point(0, 0);
            this.controlDynamicInputEdit1.Name = "controlDynamicInputEdit1";
            this.controlDynamicInputEdit1.Size = new System.Drawing.Size(170, 21);
            this.controlDynamicInputEdit1.TabIndex = 0;
            // 
            // ControlDistanceFactorDynamicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlDynamicInputEdit1);
            this.Name = "ControlDistanceFactorDynamicInput";
            this.Size = new System.Drawing.Size(170, 20);
            this.ResumeLayout(false);

        }

        #endregion

        public ControlDynamicInputEdit controlDynamicInputEdit1;
    }
}
