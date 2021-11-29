namespace AB
{
    partial class FlourBrandPollardBins_Tab
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpFlourBins = new System.Windows.Forms.TabPage();
            this.tcFlourBins = new System.Windows.Forms.TabControl();
            this.tpBins_FB_ForDisposition = new System.Windows.Forms.TabPage();
            this.panelFB_ForDisposition = new System.Windows.Forms.Panel();
            this.tpBins_FB_OffSpecs = new System.Windows.Forms.TabPage();
            this.tcBins_FB_Off = new System.Windows.Forms.TabControl();
            this.tpOpen = new System.Windows.Forms.TabPage();
            this.panelFB_OffOpen = new System.Windows.Forms.Panel();
            this.tpClosed = new System.Windows.Forms.TabPage();
            this.panelFB_OffClosed = new System.Windows.Forms.Panel();
            this.tpCancelled = new System.Windows.Forms.TabPage();
            this.panelFB_OffCancelled = new System.Windows.Forms.Panel();
            this.tpBins_FB_OnSpecs = new System.Windows.Forms.TabPage();
            this.panelFB_On = new System.Windows.Forms.Panel();
            this.tpBranPollardBins = new System.Windows.Forms.TabPage();
            this.tcBranPollardBins = new System.Windows.Forms.TabControl();
            this.tpBins_BPB_ForDisposition = new System.Windows.Forms.TabPage();
            this.panelBPB_ForDisposition = new System.Windows.Forms.Panel();
            this.tpBins_BPB_OffSpecs = new System.Windows.Forms.TabPage();
            this.tcBins_BPB_Off = new System.Windows.Forms.TabControl();
            this.tpBins_BPB_OffOpen = new System.Windows.Forms.TabPage();
            this.panelBPB_OffOpen = new System.Windows.Forms.Panel();
            this.tpBins_BPB_OffClosed = new System.Windows.Forms.TabPage();
            this.panelBFB_OffClosed = new System.Windows.Forms.Panel();
            this.tpBins_BPB_OffCancelled = new System.Windows.Forms.TabPage();
            this.panelBFB_OffCancelled = new System.Windows.Forms.Panel();
            this.tpBins_BPB_OnSpecs = new System.Windows.Forms.TabPage();
            this.panelBPB_On = new System.Windows.Forms.Panel();
            this.tcMain.SuspendLayout();
            this.tpFlourBins.SuspendLayout();
            this.tcFlourBins.SuspendLayout();
            this.tpBins_FB_ForDisposition.SuspendLayout();
            this.tpBins_FB_OffSpecs.SuspendLayout();
            this.tcBins_FB_Off.SuspendLayout();
            this.tpOpen.SuspendLayout();
            this.tpClosed.SuspendLayout();
            this.tpCancelled.SuspendLayout();
            this.tpBins_FB_OnSpecs.SuspendLayout();
            this.tpBranPollardBins.SuspendLayout();
            this.tcBranPollardBins.SuspendLayout();
            this.tpBins_BPB_ForDisposition.SuspendLayout();
            this.tpBins_BPB_OffSpecs.SuspendLayout();
            this.tcBins_BPB_Off.SuspendLayout();
            this.tpBins_BPB_OffOpen.SuspendLayout();
            this.tpBins_BPB_OffClosed.SuspendLayout();
            this.tpBins_BPB_OffCancelled.SuspendLayout();
            this.tpBins_BPB_OnSpecs.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpFlourBins);
            this.tcMain.Controls.Add(this.tpBranPollardBins);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(648, 474);
            this.tcMain.TabIndex = 4;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpFlourBins
            // 
            this.tpFlourBins.AutoScroll = true;
            this.tpFlourBins.Controls.Add(this.tcFlourBins);
            this.tpFlourBins.Location = new System.Drawing.Point(4, 26);
            this.tpFlourBins.Name = "tpFlourBins";
            this.tpFlourBins.Padding = new System.Windows.Forms.Padding(3);
            this.tpFlourBins.Size = new System.Drawing.Size(640, 444);
            this.tpFlourBins.TabIndex = 0;
            this.tpFlourBins.Text = "Flour Bins";
            this.tpFlourBins.UseVisualStyleBackColor = true;
            // 
            // tcFlourBins
            // 
            this.tcFlourBins.Controls.Add(this.tpBins_FB_ForDisposition);
            this.tcFlourBins.Controls.Add(this.tpBins_FB_OffSpecs);
            this.tcFlourBins.Controls.Add(this.tpBins_FB_OnSpecs);
            this.tcFlourBins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFlourBins.Location = new System.Drawing.Point(3, 3);
            this.tcFlourBins.Name = "tcFlourBins";
            this.tcFlourBins.SelectedIndex = 0;
            this.tcFlourBins.Size = new System.Drawing.Size(634, 438);
            this.tcFlourBins.TabIndex = 0;
            this.tcFlourBins.SelectedIndexChanged += new System.EventHandler(this.tcFlourBins_SelectedIndexChanged);
            // 
            // tpBins_FB_ForDisposition
            // 
            this.tpBins_FB_ForDisposition.Controls.Add(this.panelFB_ForDisposition);
            this.tpBins_FB_ForDisposition.Location = new System.Drawing.Point(4, 26);
            this.tpBins_FB_ForDisposition.Name = "tpBins_FB_ForDisposition";
            this.tpBins_FB_ForDisposition.Padding = new System.Windows.Forms.Padding(3);
            this.tpBins_FB_ForDisposition.Size = new System.Drawing.Size(626, 408);
            this.tpBins_FB_ForDisposition.TabIndex = 0;
            this.tpBins_FB_ForDisposition.Text = "For Disposition";
            this.tpBins_FB_ForDisposition.UseVisualStyleBackColor = true;
            // 
            // panelFB_ForDisposition
            // 
            this.panelFB_ForDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFB_ForDisposition.Location = new System.Drawing.Point(3, 3);
            this.panelFB_ForDisposition.Name = "panelFB_ForDisposition";
            this.panelFB_ForDisposition.Size = new System.Drawing.Size(620, 402);
            this.panelFB_ForDisposition.TabIndex = 1;
            // 
            // tpBins_FB_OffSpecs
            // 
            this.tpBins_FB_OffSpecs.Controls.Add(this.tcBins_FB_Off);
            this.tpBins_FB_OffSpecs.Location = new System.Drawing.Point(4, 26);
            this.tpBins_FB_OffSpecs.Name = "tpBins_FB_OffSpecs";
            this.tpBins_FB_OffSpecs.Padding = new System.Windows.Forms.Padding(3);
            this.tpBins_FB_OffSpecs.Size = new System.Drawing.Size(626, 408);
            this.tpBins_FB_OffSpecs.TabIndex = 1;
            this.tpBins_FB_OffSpecs.Text = "Off Specs";
            this.tpBins_FB_OffSpecs.UseVisualStyleBackColor = true;
            // 
            // tcBins_FB_Off
            // 
            this.tcBins_FB_Off.Controls.Add(this.tpOpen);
            this.tcBins_FB_Off.Controls.Add(this.tpClosed);
            this.tcBins_FB_Off.Controls.Add(this.tpCancelled);
            this.tcBins_FB_Off.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBins_FB_Off.Location = new System.Drawing.Point(3, 3);
            this.tcBins_FB_Off.Name = "tcBins_FB_Off";
            this.tcBins_FB_Off.SelectedIndex = 0;
            this.tcBins_FB_Off.Size = new System.Drawing.Size(620, 402);
            this.tcBins_FB_Off.TabIndex = 2;
            this.tcBins_FB_Off.SelectedIndexChanged += new System.EventHandler(this.tcBins_FB_Off_SelectedIndexChanged);
            // 
            // tpOpen
            // 
            this.tpOpen.Controls.Add(this.panelFB_OffOpen);
            this.tpOpen.Location = new System.Drawing.Point(4, 26);
            this.tpOpen.Name = "tpOpen";
            this.tpOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpOpen.Size = new System.Drawing.Size(612, 372);
            this.tpOpen.TabIndex = 0;
            this.tpOpen.Text = "Open";
            this.tpOpen.UseVisualStyleBackColor = true;
            // 
            // panelFB_OffOpen
            // 
            this.panelFB_OffOpen.BackColor = System.Drawing.Color.White;
            this.panelFB_OffOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFB_OffOpen.Location = new System.Drawing.Point(3, 3);
            this.panelFB_OffOpen.Name = "panelFB_OffOpen";
            this.panelFB_OffOpen.Size = new System.Drawing.Size(606, 366);
            this.panelFB_OffOpen.TabIndex = 0;
            // 
            // tpClosed
            // 
            this.tpClosed.Controls.Add(this.panelFB_OffClosed);
            this.tpClosed.Location = new System.Drawing.Point(4, 26);
            this.tpClosed.Name = "tpClosed";
            this.tpClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tpClosed.Size = new System.Drawing.Size(612, 372);
            this.tpClosed.TabIndex = 1;
            this.tpClosed.Text = "Closed";
            this.tpClosed.UseVisualStyleBackColor = true;
            // 
            // panelFB_OffClosed
            // 
            this.panelFB_OffClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFB_OffClosed.Location = new System.Drawing.Point(3, 3);
            this.panelFB_OffClosed.Name = "panelFB_OffClosed";
            this.panelFB_OffClosed.Size = new System.Drawing.Size(606, 366);
            this.panelFB_OffClosed.TabIndex = 1;
            // 
            // tpCancelled
            // 
            this.tpCancelled.Controls.Add(this.panelFB_OffCancelled);
            this.tpCancelled.Location = new System.Drawing.Point(4, 22);
            this.tpCancelled.Name = "tpCancelled";
            this.tpCancelled.Size = new System.Drawing.Size(598, 340);
            this.tpCancelled.TabIndex = 2;
            this.tpCancelled.Text = "Cancelled";
            this.tpCancelled.UseVisualStyleBackColor = true;
            // 
            // panelFB_OffCancelled
            // 
            this.panelFB_OffCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFB_OffCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelFB_OffCancelled.Name = "panelFB_OffCancelled";
            this.panelFB_OffCancelled.Size = new System.Drawing.Size(598, 340);
            this.panelFB_OffCancelled.TabIndex = 1;
            // 
            // tpBins_FB_OnSpecs
            // 
            this.tpBins_FB_OnSpecs.Controls.Add(this.panelFB_On);
            this.tpBins_FB_OnSpecs.Location = new System.Drawing.Point(4, 22);
            this.tpBins_FB_OnSpecs.Name = "tpBins_FB_OnSpecs";
            this.tpBins_FB_OnSpecs.Size = new System.Drawing.Size(612, 376);
            this.tpBins_FB_OnSpecs.TabIndex = 2;
            this.tpBins_FB_OnSpecs.Text = "On Specs";
            this.tpBins_FB_OnSpecs.UseVisualStyleBackColor = true;
            // 
            // panelFB_On
            // 
            this.panelFB_On.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFB_On.Location = new System.Drawing.Point(0, 0);
            this.panelFB_On.Name = "panelFB_On";
            this.panelFB_On.Size = new System.Drawing.Size(612, 376);
            this.panelFB_On.TabIndex = 2;
            // 
            // tpBranPollardBins
            // 
            this.tpBranPollardBins.Controls.Add(this.tcBranPollardBins);
            this.tpBranPollardBins.Location = new System.Drawing.Point(4, 26);
            this.tpBranPollardBins.Name = "tpBranPollardBins";
            this.tpBranPollardBins.Padding = new System.Windows.Forms.Padding(3);
            this.tpBranPollardBins.Size = new System.Drawing.Size(640, 444);
            this.tpBranPollardBins.TabIndex = 1;
            this.tpBranPollardBins.Text = "Bran/Pollard Bins";
            this.tpBranPollardBins.UseVisualStyleBackColor = true;
            // 
            // tcBranPollardBins
            // 
            this.tcBranPollardBins.Controls.Add(this.tpBins_BPB_ForDisposition);
            this.tcBranPollardBins.Controls.Add(this.tpBins_BPB_OffSpecs);
            this.tcBranPollardBins.Controls.Add(this.tpBins_BPB_OnSpecs);
            this.tcBranPollardBins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBranPollardBins.Location = new System.Drawing.Point(3, 3);
            this.tcBranPollardBins.Name = "tcBranPollardBins";
            this.tcBranPollardBins.SelectedIndex = 0;
            this.tcBranPollardBins.Size = new System.Drawing.Size(634, 438);
            this.tcBranPollardBins.TabIndex = 1;
            this.tcBranPollardBins.SelectedIndexChanged += new System.EventHandler(this.tcBranPollardBins_SelectedIndexChanged);
            // 
            // tpBins_BPB_ForDisposition
            // 
            this.tpBins_BPB_ForDisposition.Controls.Add(this.panelBPB_ForDisposition);
            this.tpBins_BPB_ForDisposition.Location = new System.Drawing.Point(4, 26);
            this.tpBins_BPB_ForDisposition.Name = "tpBins_BPB_ForDisposition";
            this.tpBins_BPB_ForDisposition.Padding = new System.Windows.Forms.Padding(3);
            this.tpBins_BPB_ForDisposition.Size = new System.Drawing.Size(626, 408);
            this.tpBins_BPB_ForDisposition.TabIndex = 0;
            this.tpBins_BPB_ForDisposition.Text = "For Disposition";
            this.tpBins_BPB_ForDisposition.UseVisualStyleBackColor = true;
            // 
            // panelBPB_ForDisposition
            // 
            this.panelBPB_ForDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBPB_ForDisposition.Location = new System.Drawing.Point(3, 3);
            this.panelBPB_ForDisposition.Name = "panelBPB_ForDisposition";
            this.panelBPB_ForDisposition.Size = new System.Drawing.Size(620, 402);
            this.panelBPB_ForDisposition.TabIndex = 1;
            // 
            // tpBins_BPB_OffSpecs
            // 
            this.tpBins_BPB_OffSpecs.Controls.Add(this.tcBins_BPB_Off);
            this.tpBins_BPB_OffSpecs.Location = new System.Drawing.Point(4, 26);
            this.tpBins_BPB_OffSpecs.Name = "tpBins_BPB_OffSpecs";
            this.tpBins_BPB_OffSpecs.Padding = new System.Windows.Forms.Padding(3);
            this.tpBins_BPB_OffSpecs.Size = new System.Drawing.Size(626, 408);
            this.tpBins_BPB_OffSpecs.TabIndex = 1;
            this.tpBins_BPB_OffSpecs.Text = "Off Specs";
            this.tpBins_BPB_OffSpecs.UseVisualStyleBackColor = true;
            // 
            // tcBins_BPB_Off
            // 
            this.tcBins_BPB_Off.Controls.Add(this.tpBins_BPB_OffOpen);
            this.tcBins_BPB_Off.Controls.Add(this.tpBins_BPB_OffClosed);
            this.tcBins_BPB_Off.Controls.Add(this.tpBins_BPB_OffCancelled);
            this.tcBins_BPB_Off.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBins_BPB_Off.Location = new System.Drawing.Point(3, 3);
            this.tcBins_BPB_Off.Name = "tcBins_BPB_Off";
            this.tcBins_BPB_Off.SelectedIndex = 0;
            this.tcBins_BPB_Off.Size = new System.Drawing.Size(620, 402);
            this.tcBins_BPB_Off.TabIndex = 2;
            this.tcBins_BPB_Off.SelectedIndexChanged += new System.EventHandler(this.tcBins_BPB_Off_SelectedIndexChanged);
            // 
            // tpBins_BPB_OffOpen
            // 
            this.tpBins_BPB_OffOpen.Controls.Add(this.panelBPB_OffOpen);
            this.tpBins_BPB_OffOpen.Location = new System.Drawing.Point(4, 26);
            this.tpBins_BPB_OffOpen.Name = "tpBins_BPB_OffOpen";
            this.tpBins_BPB_OffOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpBins_BPB_OffOpen.Size = new System.Drawing.Size(612, 372);
            this.tpBins_BPB_OffOpen.TabIndex = 0;
            this.tpBins_BPB_OffOpen.Text = "Open";
            this.tpBins_BPB_OffOpen.UseVisualStyleBackColor = true;
            // 
            // panelBPB_OffOpen
            // 
            this.panelBPB_OffOpen.BackColor = System.Drawing.Color.White;
            this.panelBPB_OffOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBPB_OffOpen.Location = new System.Drawing.Point(3, 3);
            this.panelBPB_OffOpen.Name = "panelBPB_OffOpen";
            this.panelBPB_OffOpen.Size = new System.Drawing.Size(606, 366);
            this.panelBPB_OffOpen.TabIndex = 0;
            // 
            // tpBins_BPB_OffClosed
            // 
            this.tpBins_BPB_OffClosed.Controls.Add(this.panelBFB_OffClosed);
            this.tpBins_BPB_OffClosed.Location = new System.Drawing.Point(4, 22);
            this.tpBins_BPB_OffClosed.Name = "tpBins_BPB_OffClosed";
            this.tpBins_BPB_OffClosed.Padding = new System.Windows.Forms.Padding(3);
            this.tpBins_BPB_OffClosed.Size = new System.Drawing.Size(598, 340);
            this.tpBins_BPB_OffClosed.TabIndex = 1;
            this.tpBins_BPB_OffClosed.Text = "Closed";
            this.tpBins_BPB_OffClosed.UseVisualStyleBackColor = true;
            // 
            // panelBFB_OffClosed
            // 
            this.panelBFB_OffClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBFB_OffClosed.Location = new System.Drawing.Point(3, 3);
            this.panelBFB_OffClosed.Name = "panelBFB_OffClosed";
            this.panelBFB_OffClosed.Size = new System.Drawing.Size(592, 334);
            this.panelBFB_OffClosed.TabIndex = 1;
            // 
            // tpBins_BPB_OffCancelled
            // 
            this.tpBins_BPB_OffCancelled.Controls.Add(this.panelBFB_OffCancelled);
            this.tpBins_BPB_OffCancelled.Location = new System.Drawing.Point(4, 22);
            this.tpBins_BPB_OffCancelled.Name = "tpBins_BPB_OffCancelled";
            this.tpBins_BPB_OffCancelled.Size = new System.Drawing.Size(598, 340);
            this.tpBins_BPB_OffCancelled.TabIndex = 2;
            this.tpBins_BPB_OffCancelled.Text = "Cancelled";
            this.tpBins_BPB_OffCancelled.UseVisualStyleBackColor = true;
            // 
            // panelBFB_OffCancelled
            // 
            this.panelBFB_OffCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBFB_OffCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelBFB_OffCancelled.Name = "panelBFB_OffCancelled";
            this.panelBFB_OffCancelled.Size = new System.Drawing.Size(598, 340);
            this.panelBFB_OffCancelled.TabIndex = 1;
            // 
            // tpBins_BPB_OnSpecs
            // 
            this.tpBins_BPB_OnSpecs.Controls.Add(this.panelBPB_On);
            this.tpBins_BPB_OnSpecs.Location = new System.Drawing.Point(4, 22);
            this.tpBins_BPB_OnSpecs.Name = "tpBins_BPB_OnSpecs";
            this.tpBins_BPB_OnSpecs.Size = new System.Drawing.Size(612, 376);
            this.tpBins_BPB_OnSpecs.TabIndex = 2;
            this.tpBins_BPB_OnSpecs.Text = "On Specs";
            this.tpBins_BPB_OnSpecs.UseVisualStyleBackColor = true;
            // 
            // panelBPB_On
            // 
            this.panelBPB_On.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBPB_On.Location = new System.Drawing.Point(0, 0);
            this.panelBPB_On.Name = "panelBPB_On";
            this.panelBPB_On.Size = new System.Drawing.Size(612, 376);
            this.panelBPB_On.TabIndex = 2;
            // 
            // FlourBrandPollardBins_Tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(648, 474);
            this.Controls.Add(this.tcMain);
            this.Name = "FlourBrandPollardBins_Tab";
            this.Text = "Flour/Bran Bins";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.QA_Disposition_Tab_Load);
            this.tcMain.ResumeLayout(false);
            this.tpFlourBins.ResumeLayout(false);
            this.tcFlourBins.ResumeLayout(false);
            this.tpBins_FB_ForDisposition.ResumeLayout(false);
            this.tpBins_FB_OffSpecs.ResumeLayout(false);
            this.tcBins_FB_Off.ResumeLayout(false);
            this.tpOpen.ResumeLayout(false);
            this.tpClosed.ResumeLayout(false);
            this.tpCancelled.ResumeLayout(false);
            this.tpBins_FB_OnSpecs.ResumeLayout(false);
            this.tpBranPollardBins.ResumeLayout(false);
            this.tcBranPollardBins.ResumeLayout(false);
            this.tpBins_BPB_ForDisposition.ResumeLayout(false);
            this.tpBins_BPB_OffSpecs.ResumeLayout(false);
            this.tcBins_BPB_Off.ResumeLayout(false);
            this.tpBins_BPB_OffOpen.ResumeLayout(false);
            this.tpBins_BPB_OffClosed.ResumeLayout(false);
            this.tpBins_BPB_OffCancelled.ResumeLayout(false);
            this.tpBins_BPB_OnSpecs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpFlourBins;
        private System.Windows.Forms.TabControl tcFlourBins;
        private System.Windows.Forms.TabPage tpBins_FB_ForDisposition;
        private System.Windows.Forms.Panel panelFB_ForDisposition;
        private System.Windows.Forms.TabPage tpBins_FB_OffSpecs;
        private System.Windows.Forms.TabControl tcBins_FB_Off;
        private System.Windows.Forms.TabPage tpOpen;
        private System.Windows.Forms.Panel panelFB_OffOpen;
        private System.Windows.Forms.TabPage tpClosed;
        private System.Windows.Forms.Panel panelFB_OffClosed;
        private System.Windows.Forms.TabPage tpCancelled;
        private System.Windows.Forms.Panel panelFB_OffCancelled;
        private System.Windows.Forms.TabPage tpBins_FB_OnSpecs;
        private System.Windows.Forms.Panel panelFB_On;
        private System.Windows.Forms.TabPage tpBranPollardBins;
        private System.Windows.Forms.TabControl tcBranPollardBins;
        private System.Windows.Forms.TabPage tpBins_BPB_ForDisposition;
        private System.Windows.Forms.Panel panelBPB_ForDisposition;
        private System.Windows.Forms.TabPage tpBins_BPB_OffSpecs;
        private System.Windows.Forms.TabControl tcBins_BPB_Off;
        private System.Windows.Forms.TabPage tpBins_BPB_OffOpen;
        private System.Windows.Forms.Panel panelBPB_OffOpen;
        private System.Windows.Forms.TabPage tpBins_BPB_OffClosed;
        private System.Windows.Forms.Panel panelBFB_OffClosed;
        private System.Windows.Forms.TabPage tpBins_BPB_OffCancelled;
        private System.Windows.Forms.Panel panelBFB_OffCancelled;
        private System.Windows.Forms.TabPage tpBins_BPB_OnSpecs;
        private System.Windows.Forms.Panel panelBPB_On;
    }
}