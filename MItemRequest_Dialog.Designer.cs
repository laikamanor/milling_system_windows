namespace AB
{
    partial class MItemRequest_Dialog
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
            this.btnFromDept = new System.Windows.Forms.Button();
            this.lblSelectedFromDept = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDueDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFromDept
            // 
            this.btnFromDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnFromDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFromDept.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnFromDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFromDept.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFromDept.ForeColor = System.Drawing.Color.White;
            this.btnFromDept.Location = new System.Drawing.Point(463, 50);
            this.btnFromDept.Name = "btnFromDept";
            this.btnFromDept.Size = new System.Drawing.Size(35, 25);
            this.btnFromDept.TabIndex = 72;
            this.btnFromDept.Text = "...";
            this.btnFromDept.UseVisualStyleBackColor = false;
            this.btnFromDept.Click += new System.EventHandler(this.btnFromDept_Click);
            // 
            // lblSelectedFromDept
            // 
            this.lblSelectedFromDept.BackColor = System.Drawing.SystemColors.Control;
            this.lblSelectedFromDept.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFromDept.ForeColor = System.Drawing.Color.Black;
            this.lblSelectedFromDept.Location = new System.Drawing.Point(12, 54);
            this.lblSelectedFromDept.Name = "lblSelectedFromDept";
            this.lblSelectedFromDept.Size = new System.Drawing.Size(445, 18);
            this.lblSelectedFromDept.TabIndex = 71;
            this.lblSelectedFromDept.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 18);
            this.label1.TabIndex = 70;
            this.label1.Text = "*From Department:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 73;
            this.label2.Text = "*Due Date:";
            // 
            // dtDueDate
            // 
            this.dtDueDate.CustomFormat = "yyyy-MM-dd";
            this.dtDueDate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDueDate.Location = new System.Drawing.Point(15, 111);
            this.dtDueDate.Name = "dtDueDate";
            this.dtDueDate.Size = new System.Drawing.Size(231, 25);
            this.dtDueDate.TabIndex = 74;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(12, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 18);
            this.label12.TabIndex = 76;
            this.label12.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(15, 169);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(483, 74);
            this.txtRemarks.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(15, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 208);
            this.panel1.TabIndex = 77;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(15, 478);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(483, 35);
            this.btnSubmit.TabIndex = 78;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // MItemRequest_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(510, 535);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.dtDueDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFromDept);
            this.Controls.Add(this.lblSelectedFromDept);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MItemRequest_Dialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Request";
            this.Load += new System.EventHandler(this.MItemRequest_Dialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFromDept;
        public System.Windows.Forms.Label lblSelectedFromDept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtDueDate;
        public System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSubmit;
    }
}