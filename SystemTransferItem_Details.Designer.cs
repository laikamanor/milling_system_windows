namespace AB
{
    partial class SystemTransferItem_Details
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
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnAddCart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFromWhse = new System.Windows.Forms.Label();
            this.lblToWhse = new System.Windows.Forms.Label();
            this.btnFromWhse = new System.Windows.Forms.Button();
            this.btnToWhse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItemCode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(12, 9);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(444, 72);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "ITEM CODE:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtQuantity.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(47, 85);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(124, 26);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.Text = "0";
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // btnMinus
            // 
            this.btnMinus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMinus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinus.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinus.ForeColor = System.Drawing.Color.White;
            this.btnMinus.Location = new System.Drawing.Point(12, 85);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(35, 26);
            this.btnMinus.TabIndex = 3;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlus.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlus.ForeColor = System.Drawing.Color.White;
            this.btnPlus.Location = new System.Drawing.Point(171, 85);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(35, 26);
            this.btnPlus.TabIndex = 4;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnAddCart
            // 
            this.btnAddCart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnAddCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCart.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnAddCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCart.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCart.ForeColor = System.Drawing.Color.White;
            this.btnAddCart.Location = new System.Drawing.Point(12, 258);
            this.btnAddCart.Name = "btnAddCart";
            this.btnAddCart.Size = new System.Drawing.Size(444, 41);
            this.btnAddCart.TabIndex = 5;
            this.btnAddCart.Text = "Add to Cart";
            this.btnAddCart.UseVisualStyleBackColor = false;
            this.btnAddCart.Click += new System.EventHandler(this.btnAddCart_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(9, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "*From Warehouse:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(9, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "*To Warehouse:";
            // 
            // lblFromWhse
            // 
            this.lblFromWhse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFromWhse.AutoSize = true;
            this.lblFromWhse.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromWhse.ForeColor = System.Drawing.Color.DimGray;
            this.lblFromWhse.Location = new System.Drawing.Point(9, 157);
            this.lblFromWhse.Name = "lblFromWhse";
            this.lblFromWhse.Size = new System.Drawing.Size(32, 18);
            this.lblFromWhse.TabIndex = 10;
            this.lblFromWhse.Text = "N/A";
            // 
            // lblToWhse
            // 
            this.lblToWhse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblToWhse.AutoSize = true;
            this.lblToWhse.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToWhse.ForeColor = System.Drawing.Color.DimGray;
            this.lblToWhse.Location = new System.Drawing.Point(9, 218);
            this.lblToWhse.Name = "lblToWhse";
            this.lblToWhse.Size = new System.Drawing.Size(32, 18);
            this.lblToWhse.TabIndex = 11;
            this.lblToWhse.Text = "N/A";
            // 
            // btnFromWhse
            // 
            this.btnFromWhse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFromWhse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnFromWhse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFromWhse.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnFromWhse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFromWhse.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFromWhse.ForeColor = System.Drawing.Color.White;
            this.btnFromWhse.Location = new System.Drawing.Point(146, 125);
            this.btnFromWhse.Name = "btnFromWhse";
            this.btnFromWhse.Size = new System.Drawing.Size(35, 25);
            this.btnFromWhse.TabIndex = 12;
            this.btnFromWhse.Text = "...";
            this.btnFromWhse.UseVisualStyleBackColor = false;
            this.btnFromWhse.Click += new System.EventHandler(this.btnFromWhse_Click);
            // 
            // btnToWhse
            // 
            this.btnToWhse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnToWhse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnToWhse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToWhse.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnToWhse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToWhse.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToWhse.ForeColor = System.Drawing.Color.White;
            this.btnToWhse.Location = new System.Drawing.Point(146, 186);
            this.btnToWhse.Name = "btnToWhse";
            this.btnToWhse.Size = new System.Drawing.Size(35, 25);
            this.btnToWhse.TabIndex = 13;
            this.btnToWhse.Text = "...";
            this.btnToWhse.UseVisualStyleBackColor = false;
            this.btnToWhse.Click += new System.EventHandler(this.btnToWhse_Click);
            // 
            // SystemTransferItem_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(468, 332);
            this.Controls.Add(this.btnToWhse);
            this.Controls.Add(this.btnFromWhse);
            this.Controls.Add(this.lblToWhse);
            this.Controls.Add(this.lblFromWhse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddCart);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblItemCode);
            this.Name = "SystemTransferItem_Details";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddQuantity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnAddCart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFromWhse;
        private System.Windows.Forms.Button btnToWhse;
        public System.Windows.Forms.Label lblItemCode;
        public System.Windows.Forms.TextBox txtQuantity;
        public System.Windows.Forms.Label lblFromWhse;
        public System.Windows.Forms.Label lblToWhse;
    }
}