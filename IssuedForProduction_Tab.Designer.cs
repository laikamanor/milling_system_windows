namespace AB
{
    partial class IssuedForProduction_Tab
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tcDepts = new System.Windows.Forms.TabControl();
            this.tpCW = new System.Windows.Forms.TabPage();
            this.tcCWStatus = new System.Windows.Forms.TabControl();
            this.tpCWOpen = new System.Windows.Forms.TabPage();
            this.panelCWOpen = new System.Windows.Forms.Panel();
            this.tpCWClosed = new System.Windows.Forms.TabPage();
            this.panelCWClosed = new System.Windows.Forms.Panel();
            this.tpCWCancelled = new System.Windows.Forms.TabPage();
            this.panelCWCancelled = new System.Windows.Forms.Panel();
            this.tpFB = new System.Windows.Forms.TabPage();
            this.tcFBStatus = new System.Windows.Forms.TabControl();
            this.tpFBOpen = new System.Windows.Forms.TabPage();
            this.panelFBOpen = new System.Windows.Forms.Panel();
            this.tpFBClosed = new System.Windows.Forms.TabPage();
            this.panelFBClosed = new System.Windows.Forms.Panel();
            this.tpFBCancelled = new System.Windows.Forms.TabPage();
            this.panelFBCancelled = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tcDepts.SuspendLayout();
            this.tpCW.SuspendLayout();
            this.tcCWStatus.SuspendLayout();
            this.tpCWOpen.SuspendLayout();
            this.tpCWClosed.SuspendLayout();
            this.tpCWCancelled.SuspendLayout();
            this.tpFB.SuspendLayout();
            this.tcFBStatus.SuspendLayout();
            this.tpFBOpen.SuspendLayout();
            this.tpFBClosed.SuspendLayout();
            this.tpFBCancelled.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // tcDepts
            // 
            this.tcDepts.Controls.Add(this.tpCW);
            this.tcDepts.Controls.Add(this.tpFB);
            this.tcDepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDepts.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcDepts.Location = new System.Drawing.Point(0, 0);
            this.tcDepts.Name = "tcDepts";
            this.tcDepts.SelectedIndex = 0;
            this.tcDepts.Size = new System.Drawing.Size(870, 495);
            this.tcDepts.TabIndex = 0;
            this.tcDepts.SelectedIndexChanged += new System.EventHandler(this.tcDepts_SelectedIndexChanged);
            // 
            // tpCW
            // 
            this.tpCW.Controls.Add(this.tcCWStatus);
            this.tpCW.Location = new System.Drawing.Point(4, 26);
            this.tpCW.Name = "tpCW";
            this.tpCW.Padding = new System.Windows.Forms.Padding(3);
            this.tpCW.Size = new System.Drawing.Size(862, 465);
            this.tpCW.TabIndex = 0;
            this.tpCW.Text = "Clean Wheat";
            this.tpCW.UseVisualStyleBackColor = true;
            // 
            // tcCWStatus
            // 
            this.tcCWStatus.Controls.Add(this.tpCWOpen);
            this.tcCWStatus.Controls.Add(this.tpCWClosed);
            this.tcCWStatus.Controls.Add(this.tpCWCancelled);
            this.tcCWStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCWStatus.Location = new System.Drawing.Point(3, 3);
            this.tcCWStatus.Name = "tcCWStatus";
            this.tcCWStatus.SelectedIndex = 0;
            this.tcCWStatus.Size = new System.Drawing.Size(856, 459);
            this.tcCWStatus.TabIndex = 0;
            this.tcCWStatus.SelectedIndexChanged += new System.EventHandler(this.tcCWStatus_SelectedIndexChanged);
            // 
            // tpCWOpen
            // 
            this.tpCWOpen.Controls.Add(this.panelCWOpen);
            this.tpCWOpen.Location = new System.Drawing.Point(4, 26);
            this.tpCWOpen.Name = "tpCWOpen";
            this.tpCWOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpCWOpen.Size = new System.Drawing.Size(848, 429);
            this.tpCWOpen.TabIndex = 0;
            this.tpCWOpen.Text = "Open";
            this.tpCWOpen.UseVisualStyleBackColor = true;
            // 
            // panelCWOpen
            // 
            this.panelCWOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCWOpen.Location = new System.Drawing.Point(3, 3);
            this.panelCWOpen.Name = "panelCWOpen";
            this.panelCWOpen.Size = new System.Drawing.Size(842, 423);
            this.panelCWOpen.TabIndex = 0;
            // 
            // tpCWClosed
            // 
            this.tpCWClosed.Controls.Add(this.panelCWClosed);
            this.tpCWClosed.Location = new System.Drawing.Point(4, 26);
            this.tpCWClosed.Name = "tpCWClosed";
            this.tpCWClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tpCWClosed.Size = new System.Drawing.Size(848, 429);
            this.tpCWClosed.TabIndex = 1;
            this.tpCWClosed.Text = "Closed";
            this.tpCWClosed.UseVisualStyleBackColor = true;
            // 
            // panelCWClosed
            // 
            this.panelCWClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCWClosed.Location = new System.Drawing.Point(3, 3);
            this.panelCWClosed.Name = "panelCWClosed";
            this.panelCWClosed.Size = new System.Drawing.Size(842, 423);
            this.panelCWClosed.TabIndex = 1;
            // 
            // tpCWCancelled
            // 
            this.tpCWCancelled.Controls.Add(this.panelCWCancelled);
            this.tpCWCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpCWCancelled.Name = "tpCWCancelled";
            this.tpCWCancelled.Size = new System.Drawing.Size(848, 429);
            this.tpCWCancelled.TabIndex = 2;
            this.tpCWCancelled.Text = "Cancelled";
            this.tpCWCancelled.UseVisualStyleBackColor = true;
            // 
            // panelCWCancelled
            // 
            this.panelCWCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCWCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelCWCancelled.Name = "panelCWCancelled";
            this.panelCWCancelled.Size = new System.Drawing.Size(848, 429);
            this.panelCWCancelled.TabIndex = 1;
            // 
            // tpFB
            // 
            this.tpFB.Controls.Add(this.tcFBStatus);
            this.tpFB.Location = new System.Drawing.Point(4, 26);
            this.tpFB.Name = "tpFB";
            this.tpFB.Padding = new System.Windows.Forms.Padding(3);
            this.tpFB.Size = new System.Drawing.Size(862, 465);
            this.tpFB.TabIndex = 1;
            this.tpFB.Text = "Feedback";
            this.tpFB.UseVisualStyleBackColor = true;
            // 
            // tcFBStatus
            // 
            this.tcFBStatus.Controls.Add(this.tpFBOpen);
            this.tcFBStatus.Controls.Add(this.tpFBClosed);
            this.tcFBStatus.Controls.Add(this.tpFBCancelled);
            this.tcFBStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFBStatus.Location = new System.Drawing.Point(3, 3);
            this.tcFBStatus.Name = "tcFBStatus";
            this.tcFBStatus.SelectedIndex = 0;
            this.tcFBStatus.Size = new System.Drawing.Size(856, 459);
            this.tcFBStatus.TabIndex = 1;
            this.tcFBStatus.SelectedIndexChanged += new System.EventHandler(this.tcFBStatus_SelectedIndexChanged);
            // 
            // tpFBOpen
            // 
            this.tpFBOpen.Controls.Add(this.panelFBOpen);
            this.tpFBOpen.Location = new System.Drawing.Point(4, 26);
            this.tpFBOpen.Name = "tpFBOpen";
            this.tpFBOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpFBOpen.Size = new System.Drawing.Size(848, 429);
            this.tpFBOpen.TabIndex = 0;
            this.tpFBOpen.Text = "Open";
            this.tpFBOpen.UseVisualStyleBackColor = true;
            // 
            // panelFBOpen
            // 
            this.panelFBOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFBOpen.Location = new System.Drawing.Point(3, 3);
            this.panelFBOpen.Name = "panelFBOpen";
            this.panelFBOpen.Size = new System.Drawing.Size(842, 423);
            this.panelFBOpen.TabIndex = 0;
            // 
            // tpFBClosed
            // 
            this.tpFBClosed.Controls.Add(this.panelFBClosed);
            this.tpFBClosed.Location = new System.Drawing.Point(4, 26);
            this.tpFBClosed.Name = "tpFBClosed";
            this.tpFBClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tpFBClosed.Size = new System.Drawing.Size(848, 429);
            this.tpFBClosed.TabIndex = 1;
            this.tpFBClosed.Text = "Closed";
            this.tpFBClosed.UseVisualStyleBackColor = true;
            // 
            // panelFBClosed
            // 
            this.panelFBClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFBClosed.Location = new System.Drawing.Point(3, 3);
            this.panelFBClosed.Name = "panelFBClosed";
            this.panelFBClosed.Size = new System.Drawing.Size(842, 423);
            this.panelFBClosed.TabIndex = 1;
            // 
            // tpFBCancelled
            // 
            this.tpFBCancelled.Controls.Add(this.panelFBCancelled);
            this.tpFBCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpFBCancelled.Name = "tpFBCancelled";
            this.tpFBCancelled.Size = new System.Drawing.Size(848, 429);
            this.tpFBCancelled.TabIndex = 2;
            this.tpFBCancelled.Text = "Cancelled";
            this.tpFBCancelled.UseVisualStyleBackColor = true;
            // 
            // panelFBCancelled
            // 
            this.panelFBCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFBCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelFBCancelled.Name = "panelFBCancelled";
            this.panelFBCancelled.Size = new System.Drawing.Size(848, 429);
            this.panelFBCancelled.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // IssuedForProduction_Tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(870, 495);
            this.Controls.Add(this.tcDepts);
            this.Name = "IssuedForProduction_Tab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issued For Production";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IssuedForProduction_Tab_Load);
            this.tcDepts.ResumeLayout(false);
            this.tpCW.ResumeLayout(false);
            this.tcCWStatus.ResumeLayout(false);
            this.tpCWOpen.ResumeLayout(false);
            this.tpCWClosed.ResumeLayout(false);
            this.tpCWCancelled.ResumeLayout(false);
            this.tpFB.ResumeLayout(false);
            this.tcFBStatus.ResumeLayout(false);
            this.tpFBOpen.ResumeLayout(false);
            this.tpFBClosed.ResumeLayout(false);
            this.tpFBCancelled.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabControl tcDepts;
        private System.Windows.Forms.TabPage tpCW;
        private System.Windows.Forms.TabControl tcCWStatus;
        private System.Windows.Forms.TabPage tpCWOpen;
        private System.Windows.Forms.Panel panelCWOpen;
        private System.Windows.Forms.TabPage tpCWClosed;
        private System.Windows.Forms.Panel panelCWClosed;
        private System.Windows.Forms.TabPage tpCWCancelled;
        private System.Windows.Forms.Panel panelCWCancelled;
        private System.Windows.Forms.TabPage tpFB;
        private System.Windows.Forms.TabControl tcFBStatus;
        private System.Windows.Forms.TabPage tpFBOpen;
        private System.Windows.Forms.Panel panelFBOpen;
        private System.Windows.Forms.TabPage tpFBClosed;
        private System.Windows.Forms.Panel panelFBClosed;
        private System.Windows.Forms.TabPage tpFBCancelled;
        private System.Windows.Forms.Panel panelFBCancelled;
        private System.Windows.Forms.Timer timer1;
    }
}