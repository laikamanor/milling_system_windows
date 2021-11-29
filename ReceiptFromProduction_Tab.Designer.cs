namespace AB
{
    partial class ReceiptFromProduction_Tab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptFromProduction));
            this.tcProd = new System.Windows.Forms.TabControl();
            this.tpOpen = new System.Windows.Forms.TabPage();
            this.panelIssueProdOrder = new System.Windows.Forms.Panel();
            this.tpClosed = new System.Windows.Forms.TabPage();
            this.panelForSAP = new System.Windows.Forms.Panel();
            this.tpCancelled = new System.Windows.Forms.TabPage();
            this.panelCancelled = new System.Windows.Forms.Panel();
            this.tcProd.SuspendLayout();
            this.tpOpen.SuspendLayout();
            this.tpClosed.SuspendLayout();
            this.tpCancelled.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcProd
            // 
            this.tcProd.Controls.Add(this.tpOpen);
            this.tcProd.Controls.Add(this.tpClosed);
            this.tcProd.Controls.Add(this.tpCancelled);
            this.tcProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProd.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcProd.Location = new System.Drawing.Point(0, 0);
            this.tcProd.Name = "tcProd";
            this.tcProd.SelectedIndex = 0;
            this.tcProd.Size = new System.Drawing.Size(827, 501);
            this.tcProd.TabIndex = 3;
            this.tcProd.SelectedIndexChanged += new System.EventHandler(this.tcProd_SelectedIndexChanged);
            // 
            // tpOpen
            // 
            this.tpOpen.Controls.Add(this.panelIssueProdOrder);
            this.tpOpen.Location = new System.Drawing.Point(4, 26);
            this.tpOpen.Name = "tpOpen";
            this.tpOpen.Size = new System.Drawing.Size(819, 471);
            this.tpOpen.TabIndex = 0;
            this.tpOpen.Text = "Open";
            this.tpOpen.UseVisualStyleBackColor = true;
            // 
            // panelIssueProdOrder
            // 
            this.panelIssueProdOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIssueProdOrder.AutoScroll = true;
            this.panelIssueProdOrder.Location = new System.Drawing.Point(3, 6);
            this.panelIssueProdOrder.Name = "panelIssueProdOrder";
            this.panelIssueProdOrder.Size = new System.Drawing.Size(813, 459);
            this.panelIssueProdOrder.TabIndex = 1;
            // 
            // tpClosed
            // 
            this.tpClosed.Controls.Add(this.panelForSAP);
            this.tpClosed.Location = new System.Drawing.Point(4, 22);
            this.tpClosed.Name = "tpClosed";
            this.tpClosed.Size = new System.Drawing.Size(819, 475);
            this.tpClosed.TabIndex = 1;
            this.tpClosed.Text = "Closed";
            this.tpClosed.UseVisualStyleBackColor = true;
            // 
            // panelForSAP
            // 
            this.panelForSAP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForSAP.AutoScroll = true;
            this.panelForSAP.Location = new System.Drawing.Point(3, 6);
            this.panelForSAP.Name = "panelForSAP";
            this.panelForSAP.Size = new System.Drawing.Size(813, 463);
            this.panelForSAP.TabIndex = 2;
            // 
            // tpCancelled
            // 
            this.tpCancelled.Controls.Add(this.panelCancelled);
            this.tpCancelled.Location = new System.Drawing.Point(4, 22);
            this.tpCancelled.Name = "tpCancelled";
            this.tpCancelled.Size = new System.Drawing.Size(819, 475);
            this.tpCancelled.TabIndex = 2;
            this.tpCancelled.Text = "Cancelled";
            this.tpCancelled.UseVisualStyleBackColor = true;
            // 
            // panelCancelled
            // 
            this.panelCancelled.AutoScroll = true;
            this.panelCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCancelled.Location = new System.Drawing.Point(0, 0);
            this.panelCancelled.Name = "panelCancelled";
            this.panelCancelled.Size = new System.Drawing.Size(819, 475);
            this.panelCancelled.TabIndex = 2;
            // 
            // ReceiptFromProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(827, 501);
            this.Controls.Add(this.tcProd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReceiptFromProduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt From Production";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReceiptFromProduction_Load);
            this.tcProd.ResumeLayout(false);
            this.tpOpen.ResumeLayout(false);
            this.tpClosed.ResumeLayout(false);
            this.tpCancelled.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcProd;
        private System.Windows.Forms.TabPage tpOpen;
        private System.Windows.Forms.Panel panelIssueProdOrder;
        private System.Windows.Forms.TabPage tpClosed;
        private System.Windows.Forms.Panel panelForSAP;
        private System.Windows.Forms.TabPage tpCancelled;
        private System.Windows.Forms.Panel panelCancelled;
    }
}