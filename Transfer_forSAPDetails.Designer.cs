namespace AB
{
    partial class Transfer_forSAPDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transfer_forSAPDetails));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnSelectMultipleItem = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.buttonUpdateSAP = new System.Windows.Forms.Button();
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
            this.gridControl1.Location = new System.Drawing.Point(12, 56);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(660, 335);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.ReadOnly = true;
            // 
            // btnSelectMultipleItem
            // 
            this.btnSelectMultipleItem.BackColor = System.Drawing.Color.Silver;
            this.btnSelectMultipleItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectMultipleItem.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSelectMultipleItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectMultipleItem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectMultipleItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.btnSelectMultipleItem.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMultipleItem.Image")));
            this.btnSelectMultipleItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectMultipleItem.Location = new System.Drawing.Point(12, 25);
            this.btnSelectMultipleItem.Name = "btnSelectMultipleItem";
            this.btnSelectMultipleItem.Size = new System.Drawing.Size(188, 25);
            this.btnSelectMultipleItem.TabIndex = 93;
            this.btnSelectMultipleItem.Text = "Select Multiple Item";
            this.btnSelectMultipleItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectMultipleItem.UseVisualStyleBackColor = false;
            this.btnSelectMultipleItem.Click += new System.EventHandler(this.btnSelectMultipleItem_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopy.BackColor = System.Drawing.Color.Silver;
            this.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(12, 397);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(139, 31);
            this.btnCopy.TabIndex = 101;
            this.btnCopy.Text = "Copy All Rows";
            this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // buttonUpdateSAP
            // 
            this.buttonUpdateSAP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateSAP.BackColor = System.Drawing.Color.Silver;
            this.buttonUpdateSAP.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonUpdateSAP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdateSAP.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonUpdateSAP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.buttonUpdateSAP.Image = ((System.Drawing.Image)(resources.GetObject("buttonUpdateSAP.Image")));
            this.buttonUpdateSAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdateSAP.Location = new System.Drawing.Point(533, 397);
            this.buttonUpdateSAP.Name = "buttonUpdateSAP";
            this.buttonUpdateSAP.Size = new System.Drawing.Size(139, 31);
            this.buttonUpdateSAP.TabIndex = 102;
            this.buttonUpdateSAP.Text = "Update SAP #";
            this.buttonUpdateSAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpdateSAP.UseVisualStyleBackColor = false;
            this.buttonUpdateSAP.Click += new System.EventHandler(this.buttonUpdateSAP_Click);
            // 
            // Transfer_forSAPDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 440);
            this.Controls.Add(this.buttonUpdateSAP);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnSelectMultipleItem);
            this.Controls.Add(this.gridControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Transfer_forSAPDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detais";
            this.Load += new System.EventHandler(this.Transfer_forSAPDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.Button btnSelectMultipleItem;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button buttonUpdateSAP;
    }
}