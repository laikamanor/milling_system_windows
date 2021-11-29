namespace AB
{
    partial class forProd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(forProd));
            this.tcCWDoc = new System.Windows.Forms.TabControl();
            this.tpOpen = new System.Windows.Forms.TabPage();
            this.panelCWOpen = new System.Windows.Forms.Panel();
            this.tpClosed = new System.Windows.Forms.TabPage();
            this.panelCWClosed = new System.Windows.Forms.Panel();
            this.tpCancelled = new System.Windows.Forms.TabPage();
            this.panelCWCancelled = new System.Windows.Forms.Panel();
            this.tcDepts = new System.Windows.Forms.TabControl();
            this.tpCleanWheat = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tpFeedback = new System.Windows.Forms.TabPage();
            this.tcF = new System.Windows.Forms.TabControl();
            this.tpFOpen = new System.Windows.Forms.TabPage();
            this.panelFOpen = new System.Windows.Forms.Panel();
            this.tpFClosed = new System.Windows.Forms.TabPage();
            this.panelFClosed = new System.Windows.Forms.Panel();
            this.tpFCancelled = new System.Windows.Forms.TabPage();
            this.panelFCancelled = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tcCWDoc.SuspendLayout();
            this.tpOpen.SuspendLayout();
            this.tpClosed.SuspendLayout();
            this.tpCancelled.SuspendLayout();
            this.tcDepts.SuspendLayout();
            this.tpCleanWheat.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpFeedback.SuspendLayout();
            this.tcF.SuspendLayout();
            this.tpFOpen.SuspendLayout();
            this.tpFClosed.SuspendLayout();
            this.tpFCancelled.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcCWDoc
            // 
            this.tcCWDoc.Controls.Add(this.tpOpen);
            this.tcCWDoc.Controls.Add(this.tpClosed);
            this.tcCWDoc.Controls.Add(this.tpCancelled);
            this.tcCWDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCWDoc.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcCWDoc.Location = new System.Drawing.Point(0, 0);
            this.tcCWDoc.Name = "tcCWDoc";
            this.tcCWDoc.SelectedIndex = 0;
            this.tcCWDoc.Size = new System.Drawing.Size(819, 471);
            this.tcCWDoc.TabIndex = 2;
            this.tcCWDoc.SelectedIndexChanged += new System.EventHandler(this.tcProd_SelectedIndexChanged);
            // 
            // tpOpen
            // 
            this.tpOpen.Controls.Add(this.panelCWOpen);
            this.tpOpen.Location = new System.Drawing.Point(4, 26);
            this.tpOpen.Name = "tpOpen";
            this.tpOpen.Size = new System.Drawing.Size(811, 441);
            this.tpOpen.TabIndex = 0;
            this.tpOpen.Text = "Open";
            this.tpOpen.UseVisualStyleBackColor = true;
            // 
            // panelCWOpen
            // 
            this.panelCWOpen.AutoScroll = true;
            this.panelCWOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCWOpen.Location = new System.Drawing.Point(0, 0);
            this.panelCWOpen.Name = "panelCWOpen";
            this.panelCWOpen.Size = new System.Drawing.Size(811, 441);
            this.panelCWOpen.TabIndex = 1;
            // 
            // tpClosed
            // 
            this.tpClosed.Controls.Add(this.panelCWClosed);
            this.tpClosed.Location = new System.Drawing.Point(4, 26);
            this.tpClosed.Name = "tpClosed";
            this.tpClosed.Size = new System.Drawing.Size(811, 441);
            this.tpClosed.TabIndex = 1;
            this.tpClosed.Text = "Closed";
            this.tpClosed.UseVisualStyleBackColor = true;
            // 
            // panelCWClosed
            // 
            this.panelCWClosed.AutoScroll = true;
            this.panelCWClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCWClosed.Location = new System.Drawing.Point(0, 0);
            this.panelCWClosed.Name = "panelCWClosed";
            this.panelCWClosed.Size = new System.Drawing.Size(811, 441);
            this.panelCWClosed.TabIndex = 2;
            // 
            // tpCancelled
            // 
            this.tpCancelled.Controls.Add(this.panelCWCancelled);
            this.tpCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpCancelled.Name = "tpCancelled";
            this.tpCancelled.Size = new System.Drawing.Size(811, 441);
            this.tpCancelled.TabIndex = 2;
            this.tpCancelled.Text = "Cancelled";
            this.tpCancelled.UseVisualStyleBackColor = true;
            // 
            // panelCWCancelled
            // 
            this.panelCWCancelled.AutoScroll = true;
            this.panelCWCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCWCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelCWCancelled.Name = "panelCWCancelled";
            this.panelCWCancelled.Size = new System.Drawing.Size(811, 441);
            this.panelCWCancelled.TabIndex = 2;
            // 
            // tcDepts
            // 
            this.tcDepts.Controls.Add(this.tpCleanWheat);
            this.tcDepts.Controls.Add(this.tpFeedback);
            this.tcDepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDepts.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcDepts.Location = new System.Drawing.Point(0, 0);
            this.tcDepts.Name = "tcDepts";
            this.tcDepts.SelectedIndex = 0;
            this.tcDepts.Size = new System.Drawing.Size(827, 501);
            this.tcDepts.TabIndex = 3;
            this.tcDepts.SelectedIndexChanged += new System.EventHandler(this.tcDepts_SelectedIndexChanged);
            // 
            // tpCleanWheat
            // 
            this.tpCleanWheat.Controls.Add(this.panel1);
            this.tpCleanWheat.Location = new System.Drawing.Point(4, 26);
            this.tpCleanWheat.Name = "tpCleanWheat";
            this.tpCleanWheat.Size = new System.Drawing.Size(819, 471);
            this.tpCleanWheat.TabIndex = 0;
            this.tpCleanWheat.Text = "Clean Wheat";
            this.tpCleanWheat.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tcCWDoc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 471);
            this.panel1.TabIndex = 1;
            // 
            // tpFeedback
            // 
            this.tpFeedback.Controls.Add(this.tcF);
            this.tpFeedback.Location = new System.Drawing.Point(4, 26);
            this.tpFeedback.Name = "tpFeedback";
            this.tpFeedback.Size = new System.Drawing.Size(819, 471);
            this.tpFeedback.TabIndex = 1;
            this.tpFeedback.Text = "Feedback";
            this.tpFeedback.UseVisualStyleBackColor = true;
            // 
            // tcF
            // 
            this.tcF.Controls.Add(this.tpFOpen);
            this.tcF.Controls.Add(this.tpFClosed);
            this.tcF.Controls.Add(this.tpFCancelled);
            this.tcF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcF.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcF.Location = new System.Drawing.Point(0, 0);
            this.tcF.Name = "tcF";
            this.tcF.SelectedIndex = 0;
            this.tcF.Size = new System.Drawing.Size(819, 471);
            this.tcF.TabIndex = 3;
            this.tcF.SelectedIndexChanged += new System.EventHandler(this.tcF_SelectedIndexChanged);
            // 
            // tpFOpen
            // 
            this.tpFOpen.Controls.Add(this.panelFOpen);
            this.tpFOpen.Location = new System.Drawing.Point(4, 26);
            this.tpFOpen.Name = "tpFOpen";
            this.tpFOpen.Size = new System.Drawing.Size(811, 441);
            this.tpFOpen.TabIndex = 0;
            this.tpFOpen.Text = "Open";
            this.tpFOpen.UseVisualStyleBackColor = true;
            // 
            // panelFOpen
            // 
            this.panelFOpen.AutoScroll = true;
            this.panelFOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFOpen.Location = new System.Drawing.Point(0, 0);
            this.panelFOpen.Name = "panelFOpen";
            this.panelFOpen.Size = new System.Drawing.Size(811, 441);
            this.panelFOpen.TabIndex = 1;
            // 
            // tpFClosed
            // 
            this.tpFClosed.Controls.Add(this.panelFClosed);
            this.tpFClosed.Location = new System.Drawing.Point(4, 26);
            this.tpFClosed.Name = "tpFClosed";
            this.tpFClosed.Size = new System.Drawing.Size(811, 441);
            this.tpFClosed.TabIndex = 1;
            this.tpFClosed.Text = "Closed";
            this.tpFClosed.UseVisualStyleBackColor = true;
            // 
            // panelFClosed
            // 
            this.panelFClosed.AutoScroll = true;
            this.panelFClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFClosed.Location = new System.Drawing.Point(0, 0);
            this.panelFClosed.Name = "panelFClosed";
            this.panelFClosed.Size = new System.Drawing.Size(811, 441);
            this.panelFClosed.TabIndex = 2;
            // 
            // tpFCancelled
            // 
            this.tpFCancelled.Controls.Add(this.panelFCancelled);
            this.tpFCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpFCancelled.Name = "tpFCancelled";
            this.tpFCancelled.Size = new System.Drawing.Size(811, 441);
            this.tpFCancelled.TabIndex = 2;
            this.tpFCancelled.Text = "Cancelled";
            this.tpFCancelled.UseVisualStyleBackColor = true;
            // 
            // panelFCancelled
            // 
            this.panelFCancelled.AutoScroll = true;
            this.panelFCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelFCancelled.Name = "panelFCancelled";
            this.panelFCancelled.Size = new System.Drawing.Size(811, 441);
            this.panelFCancelled.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // IssuedForProduction_Tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(827, 501);
            this.Controls.Add(this.tcDepts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IssuedForProduction_Tab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issued For Production";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IssueForProduction_Load);
            this.tcCWDoc.ResumeLayout(false);
            this.tpOpen.ResumeLayout(false);
            this.tpClosed.ResumeLayout(false);
            this.tpCancelled.ResumeLayout(false);
            this.tcDepts.ResumeLayout(false);
            this.tpCleanWheat.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpFeedback.ResumeLayout(false);
            this.tcF.ResumeLayout(false);
            this.tpFOpen.ResumeLayout(false);
            this.tpFClosed.ResumeLayout(false);
            this.tpFCancelled.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCWDoc;
        private System.Windows.Forms.TabPage tpOpen;
        private System.Windows.Forms.TabPage tpClosed;
        private System.Windows.Forms.Panel panelCWOpen;
        private System.Windows.Forms.Panel panelCWClosed;
        private System.Windows.Forms.TabPage tpCancelled;
        private System.Windows.Forms.Panel panelCWCancelled;
        private System.Windows.Forms.TabControl tcDepts;
        private System.Windows.Forms.TabPage tpCleanWheat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tpFeedback;
        private System.Windows.Forms.TabControl tcF;
        private System.Windows.Forms.TabPage tpFOpen;
        private System.Windows.Forms.Panel panelFOpen;
        private System.Windows.Forms.TabPage tpFClosed;
        private System.Windows.Forms.Panel panelFClosed;
        private System.Windows.Forms.TabPage tpFCancelled;
        private System.Windows.Forms.Panel panelFCancelled;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}