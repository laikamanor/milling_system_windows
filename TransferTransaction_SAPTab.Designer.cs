namespace AB
{
    partial class TransferTransaction_SAPTab
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpOpen = new System.Windows.Forms.TabPage();
            this.panelOpen = new System.Windows.Forms.Panel();
            this.tpClose = new System.Windows.Forms.TabPage();
            this.panelClose = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tpOpen.SuspendLayout();
            this.tpClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpOpen);
            this.tabControl1.Controls.Add(this.tpClose);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(568, 369);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpOpen
            // 
            this.tpOpen.Controls.Add(this.panelOpen);
            this.tpOpen.Location = new System.Drawing.Point(4, 22);
            this.tpOpen.Name = "tpOpen";
            this.tpOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpOpen.Size = new System.Drawing.Size(560, 343);
            this.tpOpen.TabIndex = 0;
            this.tpOpen.Text = "For SAP";
            this.tpOpen.UseVisualStyleBackColor = true;
            // 
            // panelOpen
            // 
            this.panelOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOpen.Location = new System.Drawing.Point(6, 6);
            this.panelOpen.Name = "panelOpen";
            this.panelOpen.Size = new System.Drawing.Size(548, 331);
            this.panelOpen.TabIndex = 0;
            // 
            // tpClose
            // 
            this.tpClose.Controls.Add(this.panelClose);
            this.tpClose.Location = new System.Drawing.Point(4, 22);
            this.tpClose.Name = "tpClose";
            this.tpClose.Padding = new System.Windows.Forms.Padding(3);
            this.tpClose.Size = new System.Drawing.Size(560, 343);
            this.tpClose.TabIndex = 1;
            this.tpClose.Text = "Done";
            this.tpClose.UseVisualStyleBackColor = true;
            // 
            // panelClose
            // 
            this.panelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelClose.Location = new System.Drawing.Point(6, 6);
            this.panelClose.Name = "panelClose";
            this.panelClose.Size = new System.Drawing.Size(548, 331);
            this.panelClose.TabIndex = 1;
            // 
            // TransferTransaction_SAPTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(592, 393);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransferTransaction_SAPTab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransferTransaction_SAPTab_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpOpen.ResumeLayout(false);
            this.tpClose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpOpen;
        private System.Windows.Forms.TabPage tpClose;
        private System.Windows.Forms.Panel panelOpen;
        private System.Windows.Forms.Panel panelClose;
    }
}