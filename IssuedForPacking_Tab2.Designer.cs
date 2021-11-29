namespace AB
{
    partial class IssuedForPacking_Tab2
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
            this.tcTab = new System.Windows.Forms.TabControl();
            this.tpForIssue = new System.Windows.Forms.TabPage();
            this.panelForIssue = new System.Windows.Forms.Panel();
            this.tpFlourPackingBins = new System.Windows.Forms.TabPage();
            this.tcFPBStatus = new System.Windows.Forms.TabControl();
            this.tpFPBOpen = new System.Windows.Forms.TabPage();
            this.panelFPBOpen = new System.Windows.Forms.Panel();
            this.tpFPBClosed = new System.Windows.Forms.TabPage();
            this.panelFPBClosed = new System.Windows.Forms.Panel();
            this.tpFPBCancelled = new System.Windows.Forms.TabPage();
            this.panelFPBCancelled = new System.Windows.Forms.Panel();
            this.tpBranPollardPackingBins = new System.Windows.Forms.TabPage();
            this.tcBPPBStatus = new System.Windows.Forms.TabControl();
            this.tpBPPBOpen = new System.Windows.Forms.TabPage();
            this.panelBPPBOpen = new System.Windows.Forms.Panel();
            this.tpBPPBClosed = new System.Windows.Forms.TabPage();
            this.panelBPPBClosed = new System.Windows.Forms.Panel();
            this.tpBPPBCancelled = new System.Windows.Forms.TabPage();
            this.panelBPPBCancelled = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tcTab.SuspendLayout();
            this.tpForIssue.SuspendLayout();
            this.tpFlourPackingBins.SuspendLayout();
            this.tcFPBStatus.SuspendLayout();
            this.tpFPBOpen.SuspendLayout();
            this.tpFPBClosed.SuspendLayout();
            this.tpFPBCancelled.SuspendLayout();
            this.tpBranPollardPackingBins.SuspendLayout();
            this.tcBPPBStatus.SuspendLayout();
            this.tpBPPBOpen.SuspendLayout();
            this.tpBPPBClosed.SuspendLayout();
            this.tpBPPBCancelled.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTab
            // 
            this.tcTab.Controls.Add(this.tpForIssue);
            this.tcTab.Controls.Add(this.tpFlourPackingBins);
            this.tcTab.Controls.Add(this.tpBranPollardPackingBins);
            this.tcTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTab.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcTab.Location = new System.Drawing.Point(0, 0);
            this.tcTab.Name = "tcTab";
            this.tcTab.SelectedIndex = 0;
            this.tcTab.Size = new System.Drawing.Size(857, 581);
            this.tcTab.TabIndex = 0;
            this.tcTab.SelectedIndexChanged += new System.EventHandler(this.tcTab_SelectedIndexChanged);
            // 
            // tpForIssue
            // 
            this.tpForIssue.Controls.Add(this.panelForIssue);
            this.tpForIssue.Location = new System.Drawing.Point(4, 26);
            this.tpForIssue.Name = "tpForIssue";
            this.tpForIssue.Size = new System.Drawing.Size(849, 551);
            this.tpForIssue.TabIndex = 2;
            this.tpForIssue.Text = "For Issue";
            this.tpForIssue.UseVisualStyleBackColor = true;
            // 
            // panelForIssue
            // 
            this.panelForIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForIssue.Location = new System.Drawing.Point(0, 0);
            this.panelForIssue.Name = "panelForIssue";
            this.panelForIssue.Size = new System.Drawing.Size(849, 551);
            this.panelForIssue.TabIndex = 1;
            // 
            // tpFlourPackingBins
            // 
            this.tpFlourPackingBins.Controls.Add(this.tcFPBStatus);
            this.tpFlourPackingBins.Location = new System.Drawing.Point(4, 26);
            this.tpFlourPackingBins.Name = "tpFlourPackingBins";
            this.tpFlourPackingBins.Padding = new System.Windows.Forms.Padding(3);
            this.tpFlourPackingBins.Size = new System.Drawing.Size(849, 551);
            this.tpFlourPackingBins.TabIndex = 0;
            this.tpFlourPackingBins.Text = "Flour Packing Bins";
            this.tpFlourPackingBins.UseVisualStyleBackColor = true;
            // 
            // tcFPBStatus
            // 
            this.tcFPBStatus.Controls.Add(this.tpFPBOpen);
            this.tcFPBStatus.Controls.Add(this.tpFPBClosed);
            this.tcFPBStatus.Controls.Add(this.tpFPBCancelled);
            this.tcFPBStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFPBStatus.Location = new System.Drawing.Point(3, 3);
            this.tcFPBStatus.Name = "tcFPBStatus";
            this.tcFPBStatus.SelectedIndex = 0;
            this.tcFPBStatus.Size = new System.Drawing.Size(843, 545);
            this.tcFPBStatus.TabIndex = 1;
            this.tcFPBStatus.SelectedIndexChanged += new System.EventHandler(this.tcFPBStatus_SelectedIndexChanged);
            // 
            // tpFPBOpen
            // 
            this.tpFPBOpen.Controls.Add(this.panelFPBOpen);
            this.tpFPBOpen.Location = new System.Drawing.Point(4, 26);
            this.tpFPBOpen.Name = "tpFPBOpen";
            this.tpFPBOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpFPBOpen.Size = new System.Drawing.Size(835, 515);
            this.tpFPBOpen.TabIndex = 0;
            this.tpFPBOpen.Text = "Open";
            this.tpFPBOpen.UseVisualStyleBackColor = true;
            // 
            // panelFPBOpen
            // 
            this.panelFPBOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFPBOpen.Location = new System.Drawing.Point(3, 3);
            this.panelFPBOpen.Name = "panelFPBOpen";
            this.panelFPBOpen.Size = new System.Drawing.Size(829, 509);
            this.panelFPBOpen.TabIndex = 0;
            // 
            // tpFPBClosed
            // 
            this.tpFPBClosed.Controls.Add(this.panelFPBClosed);
            this.tpFPBClosed.Location = new System.Drawing.Point(4, 26);
            this.tpFPBClosed.Name = "tpFPBClosed";
            this.tpFPBClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tpFPBClosed.Size = new System.Drawing.Size(835, 515);
            this.tpFPBClosed.TabIndex = 1;
            this.tpFPBClosed.Text = "Closed";
            this.tpFPBClosed.UseVisualStyleBackColor = true;
            // 
            // panelFPBClosed
            // 
            this.panelFPBClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFPBClosed.Location = new System.Drawing.Point(3, 3);
            this.panelFPBClosed.Name = "panelFPBClosed";
            this.panelFPBClosed.Size = new System.Drawing.Size(829, 509);
            this.panelFPBClosed.TabIndex = 1;
            // 
            // tpFPBCancelled
            // 
            this.tpFPBCancelled.Controls.Add(this.panelFPBCancelled);
            this.tpFPBCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpFPBCancelled.Name = "tpFPBCancelled";
            this.tpFPBCancelled.Size = new System.Drawing.Size(835, 515);
            this.tpFPBCancelled.TabIndex = 2;
            this.tpFPBCancelled.Text = "Cancelled";
            this.tpFPBCancelled.UseVisualStyleBackColor = true;
            // 
            // panelFPBCancelled
            // 
            this.panelFPBCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFPBCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelFPBCancelled.Name = "panelFPBCancelled";
            this.panelFPBCancelled.Size = new System.Drawing.Size(835, 515);
            this.panelFPBCancelled.TabIndex = 1;
            // 
            // tpBranPollardPackingBins
            // 
            this.tpBranPollardPackingBins.Controls.Add(this.tcBPPBStatus);
            this.tpBranPollardPackingBins.Location = new System.Drawing.Point(4, 26);
            this.tpBranPollardPackingBins.Name = "tpBranPollardPackingBins";
            this.tpBranPollardPackingBins.Padding = new System.Windows.Forms.Padding(3);
            this.tpBranPollardPackingBins.Size = new System.Drawing.Size(849, 551);
            this.tpBranPollardPackingBins.TabIndex = 1;
            this.tpBranPollardPackingBins.Text = "Bran/Pollard Packing Bins";
            this.tpBranPollardPackingBins.UseVisualStyleBackColor = true;
            // 
            // tcBPPBStatus
            // 
            this.tcBPPBStatus.Controls.Add(this.tpBPPBOpen);
            this.tcBPPBStatus.Controls.Add(this.tpBPPBClosed);
            this.tcBPPBStatus.Controls.Add(this.tpBPPBCancelled);
            this.tcBPPBStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBPPBStatus.Location = new System.Drawing.Point(3, 3);
            this.tcBPPBStatus.Name = "tcBPPBStatus";
            this.tcBPPBStatus.SelectedIndex = 0;
            this.tcBPPBStatus.Size = new System.Drawing.Size(843, 545);
            this.tcBPPBStatus.TabIndex = 2;
            this.tcBPPBStatus.SelectedIndexChanged += new System.EventHandler(this.tcBPPBStatus_SelectedIndexChanged);
            // 
            // tpBPPBOpen
            // 
            this.tpBPPBOpen.Controls.Add(this.panelBPPBOpen);
            this.tpBPPBOpen.Location = new System.Drawing.Point(4, 26);
            this.tpBPPBOpen.Name = "tpBPPBOpen";
            this.tpBPPBOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpBPPBOpen.Size = new System.Drawing.Size(835, 515);
            this.tpBPPBOpen.TabIndex = 0;
            this.tpBPPBOpen.Text = "Open";
            this.tpBPPBOpen.UseVisualStyleBackColor = true;
            // 
            // panelBPPBOpen
            // 
            this.panelBPPBOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBPPBOpen.Location = new System.Drawing.Point(3, 3);
            this.panelBPPBOpen.Name = "panelBPPBOpen";
            this.panelBPPBOpen.Size = new System.Drawing.Size(829, 509);
            this.panelBPPBOpen.TabIndex = 0;
            // 
            // tpBPPBClosed
            // 
            this.tpBPPBClosed.Controls.Add(this.panelBPPBClosed);
            this.tpBPPBClosed.Location = new System.Drawing.Point(4, 26);
            this.tpBPPBClosed.Name = "tpBPPBClosed";
            this.tpBPPBClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tpBPPBClosed.Size = new System.Drawing.Size(821, 479);
            this.tpBPPBClosed.TabIndex = 1;
            this.tpBPPBClosed.Text = "Closed";
            this.tpBPPBClosed.UseVisualStyleBackColor = true;
            // 
            // panelBPPBClosed
            // 
            this.panelBPPBClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBPPBClosed.Location = new System.Drawing.Point(3, 3);
            this.panelBPPBClosed.Name = "panelBPPBClosed";
            this.panelBPPBClosed.Size = new System.Drawing.Size(815, 473);
            this.panelBPPBClosed.TabIndex = 1;
            // 
            // tpBPPBCancelled
            // 
            this.tpBPPBCancelled.Controls.Add(this.panelBPPBCancelled);
            this.tpBPPBCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpBPPBCancelled.Name = "tpBPPBCancelled";
            this.tpBPPBCancelled.Size = new System.Drawing.Size(821, 479);
            this.tpBPPBCancelled.TabIndex = 2;
            this.tpBPPBCancelled.Text = "Cancelled";
            this.tpBPPBCancelled.UseVisualStyleBackColor = true;
            // 
            // panelBPPBCancelled
            // 
            this.panelBPPBCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBPPBCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelBPPBCancelled.Name = "panelBPPBCancelled";
            this.panelBPPBCancelled.Size = new System.Drawing.Size(821, 479);
            this.panelBPPBCancelled.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // IssuedForPacking_Tab2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(857, 581);
            this.Controls.Add(this.tcTab);
            this.Name = "IssuedForPacking_Tab2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issue For Packing";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IssuedForPacking_Tab2_FormClosing);
            this.Load += new System.EventHandler(this.IssuedForPacking_Tab2_Load);
            this.tcTab.ResumeLayout(false);
            this.tpForIssue.ResumeLayout(false);
            this.tpFlourPackingBins.ResumeLayout(false);
            this.tcFPBStatus.ResumeLayout(false);
            this.tpFPBOpen.ResumeLayout(false);
            this.tpFPBClosed.ResumeLayout(false);
            this.tpFPBCancelled.ResumeLayout(false);
            this.tpBranPollardPackingBins.ResumeLayout(false);
            this.tcBPPBStatus.ResumeLayout(false);
            this.tpBPPBOpen.ResumeLayout(false);
            this.tpBPPBClosed.ResumeLayout(false);
            this.tpBPPBCancelled.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTab;
        private System.Windows.Forms.TabPage tpFlourPackingBins;
        private System.Windows.Forms.TabPage tpBranPollardPackingBins;
        private System.Windows.Forms.TabControl tcFPBStatus;
        private System.Windows.Forms.TabPage tpFPBOpen;
        private System.Windows.Forms.Panel panelFPBOpen;
        private System.Windows.Forms.TabPage tpFPBClosed;
        private System.Windows.Forms.Panel panelFPBClosed;
        private System.Windows.Forms.TabPage tpFPBCancelled;
        private System.Windows.Forms.Panel panelFPBCancelled;
        private System.Windows.Forms.TabControl tcBPPBStatus;
        private System.Windows.Forms.TabPage tpBPPBOpen;
        private System.Windows.Forms.Panel panelBPPBOpen;
        private System.Windows.Forms.TabPage tpBPPBClosed;
        private System.Windows.Forms.Panel panelBPPBClosed;
        private System.Windows.Forms.TabPage tpBPPBCancelled;
        private System.Windows.Forms.Panel panelBPPBCancelled;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tpForIssue;
        private System.Windows.Forms.Panel panelForIssue;
    }
}