namespace AB
{
    partial class changePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(changePassword));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.checkBoxNewPassword = new System.Windows.Forms.CheckBox();
            this.checkBoxReTypePassword = new System.Windows.Forms.CheckBox();
            this.txtReTypePassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(23, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "*New Password:";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNewPassword.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword.Location = new System.Drawing.Point(27, 57);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(379, 26);
            this.txtNewPassword.TabIndex = 0;
            this.txtNewPassword.Tag = "";
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassword_KeyDown);
            // 
            // checkBoxNewPassword
            // 
            this.checkBoxNewPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxNewPassword.AutoSize = true;
            this.checkBoxNewPassword.Location = new System.Drawing.Point(412, 64);
            this.checkBoxNewPassword.Name = "checkBoxNewPassword";
            this.checkBoxNewPassword.Size = new System.Drawing.Size(15, 14);
            this.checkBoxNewPassword.TabIndex = 3;
            this.checkBoxNewPassword.UseVisualStyleBackColor = true;
            this.checkBoxNewPassword.CheckedChanged += new System.EventHandler(this.checkBoxNewPassword_CheckedChanged);
            // 
            // checkBoxReTypePassword
            // 
            this.checkBoxReTypePassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxReTypePassword.AutoSize = true;
            this.checkBoxReTypePassword.Location = new System.Drawing.Point(412, 136);
            this.checkBoxReTypePassword.Name = "checkBoxReTypePassword";
            this.checkBoxReTypePassword.Size = new System.Drawing.Size(15, 14);
            this.checkBoxReTypePassword.TabIndex = 4;
            this.checkBoxReTypePassword.UseVisualStyleBackColor = true;
            this.checkBoxReTypePassword.CheckedChanged += new System.EventHandler(this.checkBoxReTypePassword_CheckedChanged);
            // 
            // txtReTypePassword
            // 
            this.txtReTypePassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtReTypePassword.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReTypePassword.Location = new System.Drawing.Point(27, 129);
            this.txtReTypePassword.Name = "txtReTypePassword";
            this.txtReTypePassword.PasswordChar = '*';
            this.txtReTypePassword.Size = new System.Drawing.Size(379, 26);
            this.txtReTypePassword.TabIndex = 1;
            this.txtReTypePassword.Tag = "";
            this.txtReTypePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassword_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(23, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "*Re-Type Password:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(27, 175);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(379, 45);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // changePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(452, 259);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.checkBoxReTypePassword);
            this.Controls.Add(this.txtReTypePassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxNewPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "changePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.changePassword_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassword_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.CheckBox checkBoxNewPassword;
        private System.Windows.Forms.CheckBox checkBoxReTypePassword;
        private System.Windows.Forms.TextBox txtReTypePassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}