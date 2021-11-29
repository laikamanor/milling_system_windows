namespace AB
{
    partial class TemperMonitoring_1stTab
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
            this.tpForDispo = new System.Windows.Forms.TabPage();
            this.panelForDisposition = new System.Windows.Forms.Panel();
            this.tpDone = new System.Windows.Forms.TabPage();
            this.panelDone = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tpForDispo.SuspendLayout();
            this.tpDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpForDispo);
            this.tabControl1.Controls.Add(this.tpDone);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(737, 534);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpForDispo
            // 
            this.tpForDispo.Controls.Add(this.panelForDisposition);
            this.tpForDispo.Location = new System.Drawing.Point(4, 26);
            this.tpForDispo.Name = "tpForDispo";
            this.tpForDispo.Padding = new System.Windows.Forms.Padding(3);
            this.tpForDispo.Size = new System.Drawing.Size(729, 504);
            this.tpForDispo.TabIndex = 0;
            this.tpForDispo.Text = "For Disposition";
            this.tpForDispo.UseVisualStyleBackColor = true;
            // 
            // panelForDisposition
            // 
            this.panelForDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForDisposition.Location = new System.Drawing.Point(3, 3);
            this.panelForDisposition.Name = "panelForDisposition";
            this.panelForDisposition.Size = new System.Drawing.Size(723, 498);
            this.panelForDisposition.TabIndex = 0;
            // 
            // tpDone
            // 
            this.tpDone.Controls.Add(this.panelDone);
            this.tpDone.Location = new System.Drawing.Point(4, 26);
            this.tpDone.Name = "tpDone";
            this.tpDone.Padding = new System.Windows.Forms.Padding(3);
            this.tpDone.Size = new System.Drawing.Size(729, 504);
            this.tpDone.TabIndex = 1;
            this.tpDone.Text = "Done";
            this.tpDone.UseVisualStyleBackColor = true;
            // 
            // panelDone
            // 
            this.panelDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDone.Location = new System.Drawing.Point(3, 3);
            this.panelDone.Name = "panelDone";
            this.panelDone.Size = new System.Drawing.Size(723, 498);
            this.panelDone.TabIndex = 1;
            // 
            // TemperMonitoring_1stTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(737, 534);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TemperMonitoring_1stTab";
            this.Load += new System.EventHandler(this.TemperMonitoring_1stTab_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpForDispo.ResumeLayout(false);
            this.tpDone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpForDispo;
        private System.Windows.Forms.TabPage tpDone;
        private System.Windows.Forms.Panel panelForDisposition;
        private System.Windows.Forms.Panel panelDone;
    }
}