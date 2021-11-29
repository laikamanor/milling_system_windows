namespace AB
{
    partial class MIssueForPacking_TransferItem
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnCreateIssuePacking = new System.Windows.Forms.Button();
            this.lblTransdate = new System.Windows.Forms.Label();
            this.lblReference = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRemarks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTargetQuantity = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFGItem = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFGUOM = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 176);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(725, 273);
            this.gridControl1.TabIndex = 17;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.ReadOnly = true;
            // 
            // btnCreateIssuePacking
            // 
            this.btnCreateIssuePacking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateIssuePacking.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCreateIssuePacking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateIssuePacking.FlatAppearance.BorderSize = 0;
            this.btnCreateIssuePacking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateIssuePacking.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateIssuePacking.ForeColor = System.Drawing.Color.White;
            this.btnCreateIssuePacking.Location = new System.Drawing.Point(506, 455);
            this.btnCreateIssuePacking.Name = "btnCreateIssuePacking";
            this.btnCreateIssuePacking.Size = new System.Drawing.Size(231, 41);
            this.btnCreateIssuePacking.TabIndex = 70;
            this.btnCreateIssuePacking.Text = "Create Issue For Packing";
            this.btnCreateIssuePacking.UseVisualStyleBackColor = false;
            this.btnCreateIssuePacking.Click += new System.EventHandler(this.btnCreateIssuePacking_Click);
            // 
            // lblTransdate
            // 
            this.lblTransdate.AutoSize = true;
            this.lblTransdate.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransdate.Location = new System.Drawing.Point(111, 34);
            this.lblTransdate.Name = "lblTransdate";
            this.lblTransdate.Size = new System.Drawing.Size(32, 18);
            this.lblTransdate.TabIndex = 71;
            this.lblTransdate.Text = "N/A";
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReference.Location = new System.Drawing.Point(111, 64);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(32, 18);
            this.lblReference.TabIndex = 72;
            this.lblReference.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(9, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 18);
            this.label1.TabIndex = 74;
            this.label1.Text = "Reference #:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 73;
            this.label2.Text = "Transdate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(9, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 76;
            this.label3.Text = "Remarks:";
            // 
            // lblRemarks
            // 
            this.lblRemarks.BackColor = System.Drawing.Color.White;
            this.lblRemarks.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRemarks.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRemarks.Location = new System.Drawing.Point(114, 92);
            this.lblRemarks.Multiline = true;
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.ReadOnly = true;
            this.lblRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.lblRemarks.Size = new System.Drawing.Size(231, 76);
            this.lblRemarks.TabIndex = 77;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(449, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 18);
            this.label4.TabIndex = 81;
            this.label4.Text = "Target Quantity:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(449, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 18);
            this.label5.TabIndex = 80;
            this.label5.Text = "Created By:";
            // 
            // lblTargetQuantity
            // 
            this.lblTargetQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetQuantity.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetQuantity.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTargetQuantity.Location = new System.Drawing.Point(576, 150);
            this.lblTargetQuantity.Name = "lblTargetQuantity";
            this.lblTargetQuantity.Size = new System.Drawing.Size(161, 18);
            this.lblTargetQuantity.TabIndex = 79;
            this.lblTargetQuantity.Text = "0.000";
            this.lblTargetQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCreatedBy.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(576, 56);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(161, 18);
            this.lblCreatedBy.TabIndex = 78;
            this.lblCreatedBy.Text = "N/A";
            this.lblCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(449, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 18);
            this.label8.TabIndex = 83;
            this.label8.Text = "FG Item:";
            // 
            // lblFGItem
            // 
            this.lblFGItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFGItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFGItem.Location = new System.Drawing.Point(576, 85);
            this.lblFGItem.Name = "lblFGItem";
            this.lblFGItem.Size = new System.Drawing.Size(161, 18);
            this.lblFGItem.TabIndex = 82;
            this.lblFGItem.Text = "N/A";
            this.lblFGItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(449, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 18);
            this.label6.TabIndex = 85;
            this.label6.Text = "FG UOM:";
            // 
            // lblFGUOM
            // 
            this.lblFGUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFGUOM.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFGUOM.Location = new System.Drawing.Point(576, 118);
            this.lblFGUOM.Name = "lblFGUOM";
            this.lblFGUOM.Size = new System.Drawing.Size(161, 18);
            this.lblFGUOM.TabIndex = 84;
            this.lblFGUOM.Text = "N/A";
            this.lblFGUOM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MIssueForPacking_TransferItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(749, 508);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblFGUOM);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblFGItem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTargetQuantity);
            this.Controls.Add(this.lblCreatedBy);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblReference);
            this.Controls.Add(this.lblTransdate);
            this.Controls.Add(this.btnCreateIssuePacking);
            this.Controls.Add(this.gridControl1);
            this.MinimizeBox = false;
            this.Name = "MIssueForPacking_TransferItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Item";
            this.Load += new System.EventHandler(this.MIssueForPacking_TransferItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        public System.Windows.Forms.Button btnCreateIssuePacking;
        private System.Windows.Forms.Label lblTransdate;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lblRemarks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTargetQuantity;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFGItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFGUOM;
    }
}