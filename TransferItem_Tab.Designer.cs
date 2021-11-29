namespace AB
{
    partial class TransferItem_Tab
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
            this.panelClosed = new System.Windows.Forms.Panel();
            this.tpCancelled = new System.Windows.Forms.TabPage();
            this.panelCancelled = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tpOpen.SuspendLayout();
            this.tpClose.SuspendLayout();
            this.tpCancelled.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpOpen);
            this.tabControl1.Controls.Add(this.tpClose);
            this.tabControl1.Controls.Add(this.tpCancelled);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 511);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpOpen
            // 
            this.tpOpen.Controls.Add(this.panelOpen);
            this.tpOpen.Location = new System.Drawing.Point(4, 26);
            this.tpOpen.Name = "tpOpen";
            this.tpOpen.Padding = new System.Windows.Forms.Padding(3);
            this.tpOpen.Size = new System.Drawing.Size(619, 481);
            this.tpOpen.TabIndex = 0;
            this.tpOpen.Text = "Open";
            this.tpOpen.UseVisualStyleBackColor = true;
            // 
            // panelOpen
            // 
            this.panelOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpen.Location = new System.Drawing.Point(3, 3);
            this.panelOpen.Name = "panelOpen";
            this.panelOpen.Size = new System.Drawing.Size(613, 475);
            this.panelOpen.TabIndex = 0;
            // 
            // tpClose
            // 
            this.tpClose.Controls.Add(this.panelClosed);
            this.tpClose.Location = new System.Drawing.Point(4, 26);
            this.tpClose.Name = "tpClose";
            this.tpClose.Padding = new System.Windows.Forms.Padding(3);
            this.tpClose.Size = new System.Drawing.Size(619, 481);
            this.tpClose.TabIndex = 1;
            this.tpClose.Text = "Closed";
            this.tpClose.UseVisualStyleBackColor = true;
            // 
            // panelClosed
            // 
            this.panelClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClosed.Location = new System.Drawing.Point(3, 3);
            this.panelClosed.Name = "panelClosed";
            this.panelClosed.Size = new System.Drawing.Size(613, 475);
            this.panelClosed.TabIndex = 1;
            // 
            // tpCancelled
            // 
            this.tpCancelled.Controls.Add(this.panelCancelled);
            this.tpCancelled.Location = new System.Drawing.Point(4, 26);
            this.tpCancelled.Name = "tpCancelled";
            this.tpCancelled.Size = new System.Drawing.Size(619, 481);
            this.tpCancelled.TabIndex = 2;
            this.tpCancelled.Text = "Cancelled";
            this.tpCancelled.UseVisualStyleBackColor = true;
            // 
            // panelCancelled
            // 
            this.panelCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelCancelled.Name = "panelCancelled";
            this.panelCancelled.Size = new System.Drawing.Size(619, 481);
            this.panelCancelled.TabIndex = 1;
            // 
            // TransferItem_Tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(627, 511);
            this.Controls.Add(this.tabControl1);
            this.Name = "TransferItem_Tab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Transactions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransferItem_Tab_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpOpen.ResumeLayout(false);
            this.tpClose.ResumeLayout(false);
            this.tpCancelled.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpOpen;
        private System.Windows.Forms.TabPage tpClose;
        private System.Windows.Forms.TabPage tpCancelled;
        private System.Windows.Forms.Panel panelOpen;
        private System.Windows.Forms.Panel panelClosed;
        private System.Windows.Forms.Panel panelCancelled;
    }
}