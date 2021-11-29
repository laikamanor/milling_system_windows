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
            this.tcTemperMonitoring = new System.Windows.Forms.TabControl();
            this.tpForDisposition = new System.Windows.Forms.TabPage();
            this.tpDone = new System.Windows.Forms.TabPage();
            this.panelDone = new System.Windows.Forms.Panel();
            this.panelForDisposition = new System.Windows.Forms.Panel();
            this.tcTemperMonitoring.SuspendLayout();
            this.tpForDisposition.SuspendLayout();
            this.tpDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTemperMonitoring
            // 
            this.tcTemperMonitoring.Controls.Add(this.tpForDisposition);
            this.tcTemperMonitoring.Controls.Add(this.tpDone);
            this.tcTemperMonitoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTemperMonitoring.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcTemperMonitoring.Location = new System.Drawing.Point(0, 0);
            this.tcTemperMonitoring.Name = "tcTemperMonitoring";
            this.tcTemperMonitoring.SelectedIndex = 0;
            this.tcTemperMonitoring.Size = new System.Drawing.Size(761, 422);
            this.tcTemperMonitoring.TabIndex = 0;
            this.tcTemperMonitoring.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpForDisposition
            // 
            this.tpForDisposition.Controls.Add(this.panelForDisposition);
            this.tpForDisposition.Location = new System.Drawing.Point(4, 26);
            this.tpForDisposition.Name = "tpForDisposition";
            this.tpForDisposition.Padding = new System.Windows.Forms.Padding(3);
            this.tpForDisposition.Size = new System.Drawing.Size(753, 392);
            this.tpForDisposition.TabIndex = 0;
            this.tpForDisposition.Text = "For Disposition";
            this.tpForDisposition.UseVisualStyleBackColor = true;
            // 
            // tpDone
            // 
            this.tpDone.Controls.Add(this.panelDone);
            this.tpDone.Location = new System.Drawing.Point(4, 26);
            this.tpDone.Name = "tpDone";
            this.tpDone.Padding = new System.Windows.Forms.Padding(3);
            this.tpDone.Size = new System.Drawing.Size(753, 392);
            this.tpDone.TabIndex = 1;
            this.tpDone.Text = "Done";
            this.tpDone.UseVisualStyleBackColor = true;
            // 
            // panelDone
            // 
            this.panelDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDone.Location = new System.Drawing.Point(3, 3);
            this.panelDone.Name = "panelDone";
            this.panelDone.Size = new System.Drawing.Size(747, 386);
            this.panelDone.TabIndex = 1;
            // 
            // panelForDisposition
            // 
            this.panelForDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForDisposition.Location = new System.Drawing.Point(3, 3);
            this.panelForDisposition.Name = "panelForDisposition";
            this.panelForDisposition.Size = new System.Drawing.Size(747, 386);
            this.panelForDisposition.TabIndex = 0;
            // 
            // TemperMonitoringTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(761, 422);
            this.Controls.Add(this.tcTemperMonitoring);
            this.Name = "TemperMonitoringTab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temper Monitoring";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TemperMonitoring_Load);
            this.tcTemperMonitoring.ResumeLayout(false);
            this.tpForDisposition.ResumeLayout(false);
            this.tpDone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTemperMonitoring;
        private System.Windows.Forms.TabPage tpForDisposition;
        private System.Windows.Forms.TabPage tpDone;
        private System.Windows.Forms.Panel panelDone;
        private System.Windows.Forms.Panel panelForDisposition;
    }
}