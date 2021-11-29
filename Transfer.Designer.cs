namespace AB
{
    partial class Transfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transfer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelOpen = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelClosed = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panelCancelled = new System.Windows.Forms.Panel();
            this.tpSAP = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpForSAP = new System.Windows.Forms.TabPage();
            this.panelForSAP = new System.Windows.Forms.Panel();
            this.tpDone = new System.Windows.Forms.TabPage();
            this.panelDone = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tpSAP.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpForSAP.SuspendLayout();
            this.tpDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tpSAP);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelOpen);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Open";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelOpen
            // 
            this.panelOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpen.Location = new System.Drawing.Point(3, 3);
            this.panelOpen.Name = "panelOpen";
            this.panelOpen.Size = new System.Drawing.Size(786, 414);
            this.panelOpen.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelClosed);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Closed";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelClosed
            // 
            this.panelClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClosed.Location = new System.Drawing.Point(3, 3);
            this.panelClosed.Name = "panelClosed";
            this.panelClosed.Size = new System.Drawing.Size(786, 418);
            this.panelClosed.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panelCancelled);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cancelled";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panelCancelled
            // 
            this.panelCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCancelled.Location = new System.Drawing.Point(3, 3);
            this.panelCancelled.Name = "panelCancelled";
            this.panelCancelled.Size = new System.Drawing.Size(786, 418);
            this.panelCancelled.TabIndex = 1;
            // 
            // tpSAP
            // 
            this.tpSAP.Controls.Add(this.tabControl2);
            this.tpSAP.Location = new System.Drawing.Point(4, 22);
            this.tpSAP.Name = "tpSAP";
            this.tpSAP.Size = new System.Drawing.Size(792, 424);
            this.tpSAP.TabIndex = 3;
            this.tpSAP.Text = "SAP";
            this.tpSAP.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpForSAP);
            this.tabControl2.Controls.Add(this.tpDone);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(792, 424);
            this.tabControl2.TabIndex = 1;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tpForSAP
            // 
            this.tpForSAP.Controls.Add(this.panelForSAP);
            this.tpForSAP.Location = new System.Drawing.Point(4, 26);
            this.tpForSAP.Name = "tpForSAP";
            this.tpForSAP.Padding = new System.Windows.Forms.Padding(3);
            this.tpForSAP.Size = new System.Drawing.Size(784, 394);
            this.tpForSAP.TabIndex = 0;
            this.tpForSAP.Text = "For SAP";
            this.tpForSAP.UseVisualStyleBackColor = true;
            // 
            // panelForSAP
            // 
            this.panelForSAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForSAP.Location = new System.Drawing.Point(3, 3);
            this.panelForSAP.Name = "panelForSAP";
            this.panelForSAP.Size = new System.Drawing.Size(778, 388);
            this.panelForSAP.TabIndex = 2;
            // 
            // tpDone
            // 
            this.tpDone.Controls.Add(this.panelDone);
            this.tpDone.Location = new System.Drawing.Point(4, 22);
            this.tpDone.Name = "tpDone";
            this.tpDone.Padding = new System.Windows.Forms.Padding(3);
            this.tpDone.Size = new System.Drawing.Size(784, 398);
            this.tpDone.TabIndex = 1;
            this.tpDone.Text = "Done";
            this.tpDone.UseVisualStyleBackColor = true;
            // 
            // panelDone
            // 
            this.panelDone.BackColor = System.Drawing.Color.White;
            this.panelDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDone.Location = new System.Drawing.Point(3, 3);
            this.panelDone.Name = "panelDone";
            this.panelDone.Size = new System.Drawing.Size(778, 392);
            this.panelDone.TabIndex = 2;
            // 
            // Transfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Transfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Transactions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transfer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tpSAP.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tpForSAP.ResumeLayout(false);
            this.tpDone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelOpen;
        private System.Windows.Forms.Panel panelClosed;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panelCancelled;
        private System.Windows.Forms.TabPage tpSAP;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpForSAP;
        private System.Windows.Forms.TabPage tpDone;
        private System.Windows.Forms.Panel panelForSAP;
        private System.Windows.Forms.Panel panelDone;
    }
}