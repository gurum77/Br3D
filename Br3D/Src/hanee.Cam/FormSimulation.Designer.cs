
namespace hanee.Cam
{
    partial class FormSimulation
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
            this.controlSimulation1 = new hanee.Cam.ControlSimulation();
            this.SuspendLayout();
            // 
            // controlSimulation1
            // 
            this.controlSimulation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlSimulation1.Location = new System.Drawing.Point(0, 0);
            this.controlSimulation1.Name = "controlSimulation1";
            this.controlSimulation1.Size = new System.Drawing.Size(937, 507);
            this.controlSimulation1.TabIndex = 0;
            // 
            // FormSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 507);
            this.Controls.Add(this.controlSimulation1);
            this.Name = "FormSimulation";
            this.Text = "Simulation";
            this.Load += new System.EventHandler(this.FormSimulation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ControlSimulation controlSimulation1;
    }
}