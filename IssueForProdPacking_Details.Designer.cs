namespace AB
{
    partial class IssueForProdPacking_Details
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
            this.btnAddCart = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblUom = new System.Windows.Forms.Label();
            this.btnFromWhse = new System.Windows.Forms.Button();
            this.lblFromWhse = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.btnAddCart.Location = new System.Drawing.Point(12, 299);
            this.btnAddCart.Name = "btnAddCart";
            this.btnAddCart.Size = new System.Drawing.Size(444, 41);
            this.btnAddCart.TabIndex = 18;
            this.btnAddCart.Text = "Add to Cart";
            this.btnAddCart.UseVisualStyleBackColor = false;
            this.btnAddCart.Click += new System.EventHandler(this.btnAddCart_Click);
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
            this.btnPlus.Location = new System.Drawing.Point(171, 196);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(35, 26);
            this.btnPlus.TabIndex = 17;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
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
            this.btnMinus.Location = new System.Drawing.Point(12, 196);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(35, 26);
            this.btnMinus.TabIndex = 16;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtQuantity.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(47, 196);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(124, 26);
            this.txtQuantity.TabIndex = 15;
            this.txtQuantity.Text = "0";
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItemCode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(12, 72);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(444, 72);
            this.lblItemCode.TabIndex = 14;
            this.lblItemCode.Text = "ITEM CODE:";
            // 
            // lblUom
            // 
            this.lblUom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUom.Location = new System.Drawing.Point(12, 161);
            this.lblUom.Name = "lblUom";
            this.lblUom.Size = new System.Drawing.Size(444, 23);
            this.lblUom.TabIndex = 19;
            this.lblUom.Text = "UOM:";
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
            this.btnFromWhse.Location = new System.Drawing.Point(146, 236);
            this.btnFromWhse.Name = "btnFromWhse";
            this.btnFromWhse.Size = new System.Drawing.Size(35, 25);
            this.btnFromWhse.TabIndex = 22;
            this.btnFromWhse.Text = "...";
            this.btnFromWhse.UseVisualStyleBackColor = false;
            this.btnFromWhse.Click += new System.EventHandler(this.btnFromWhse_Click);
            // 
            // lblFromWhse
            // 
            this.lblFromWhse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFromWhse.AutoSize = true;
            this.lblFromWhse.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromWhse.ForeColor = System.Drawing.Color.DimGray;
            this.lblFromWhse.Location = new System.Drawing.Point(9, 268);
            this.lblFromWhse.Name = "lblFromWhse";
            this.lblFromWhse.Size = new System.Drawing.Size(32, 18);
            this.lblFromWhse.TabIndex = 21;
            this.lblFromWhse.Text = "N/A";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(9, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "*From Warehouse:";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(135)))), ((int)(((byte)(167)))));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(12, 20);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(67, 30);
            this.btnBack.TabIndex = 23;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // IssueForProdPacking_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(468, 369);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFromWhse);
            this.Controls.Add(this.lblFromWhse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUom);
            this.Controls.Add(this.btnAddCart);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblItemCode);
            this.MinimizeBox = false;
            this.Name = "IssueForProdPacking_Details";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IssueForProdPacking_Details_FormClosed);
            this.Load += new System.EventHandler(this.ItemDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddCart;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        public System.Windows.Forms.TextBox txtQuantity;
        public System.Windows.Forms.Label lblItemCode;
        public System.Windows.Forms.Label lblUom;
        private System.Windows.Forms.Button btnFromWhse;
        public System.Windows.Forms.Label lblFromWhse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
    }
}