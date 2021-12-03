namespace AB
{
    partial class TemperMonitoringTab
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
            this.panelDone = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelDone
            // 
            this.panelDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDone.Location = new System.Drawing.Point(0, 0);
            this.panelDone.Name = "panelDone";
            this.panelDone.Size = new System.Drawing.Size(761, 422);
            this.panelDone.TabIndex = 2;
            // 
            // TemperMonitoringTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(761, 422);
            this.Controls.Add(this.panelDone);
            this.Name = "TemperMonitoringTab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temper Monitoring";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TemperMonitoring_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDone;
    }
}