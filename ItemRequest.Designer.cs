namespace AB
{
    partial class ItemRequest
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemRequest));
            this.checkDueDate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblNoDataFound = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from_branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to_branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.due_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doc_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transfer_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transfer_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transfer_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnViewRemarks = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.checkTransDate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTransDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchQuery = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPlant = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbFromBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbToBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkDueDate
            // 
            this.checkDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDueDate.AutoSize = true;
            this.checkDueDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDueDate.ForeColor = System.Drawing.Color.Black;
            this.checkDueDate.Location = new System.Drawing.Point(385, 38);
            this.checkDueDate.Name = "checkDueDate";
            this.checkDueDate.Size = new System.Drawing.Size(15, 14);
            this.checkDueDate.TabIndex = 34;
            this.checkDueDate.UseVisualStyleBackColor = true;
            this.checkDueDate.Visible = false;
            this.checkDueDate.CheckedChanged += new System.EventHandler(this.checkDueDate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(406, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Due Date:";
            this.label4.Visible = false;
            // 
            // dtDueDate
            // 
            this.dtDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDueDate.CustomFormat = "yyyy-MM-dd";
            this.dtDueDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDueDate.Location = new System.Drawing.Point(522, 33);
            this.dtDueDate.Name = "dtDueDate";
            this.dtDueDate.Size = new System.Drawing.Size(154, 22);
            this.dtDueDate.TabIndex = 32;
            this.dtDueDate.Visible = false;
            // 
            // lblNoDataFound
            // 
            this.lblNoDataFound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNoDataFound.AutoSize = true;
            this.lblNoDataFound.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoDataFound.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoDataFound.Location = new System.Drawing.Point(404, 248);
            this.lblNoDataFound.Name = "lblNoDataFound";
            this.lblNoDataFound.Size = new System.Drawing.Size(105, 18);
            this.lblNoDataFound.TabIndex = 24;
            this.lblNoDataFound.Text = "No data found";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 40;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.transdate,
            this.reference,
            this.from_branch,
            this.to_branch,
            this.due_date,
            this.doc_status,
            this.remarks,
            this.transfer_id,
            this.transfer_ref,
            this.transfer_date,
            this.btnViewRemarks});
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.Gray;
            this.dgv.Location = new System.Drawing.Point(34, 149);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 10;
            this.dgv.Size = new System.Drawing.Size(842, 291);
            this.dgv.TabIndex = 23;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 100;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // transdate
            // 
            this.transdate.HeaderText = "Trans. Date";
            this.transdate.MinimumWidth = 100;
            this.transdate.Name = "transdate";
            this.transdate.ReadOnly = true;
            // 
            // reference
            // 
            this.reference.HeaderText = "Reference";
            this.reference.MinimumWidth = 120;
            this.reference.Name = "reference";
            this.reference.ReadOnly = true;
            // 
            // from_branch
            // 
            this.from_branch.HeaderText = "From Department";
            this.from_branch.MinimumWidth = 100;
            this.from_branch.Name = "from_branch";
            this.from_branch.ReadOnly = true;
            // 
            // to_branch
            // 
            this.to_branch.HeaderText = "To Department";
            this.to_branch.MinimumWidth = 100;
            this.to_branch.Name = "to_branch";
            this.to_branch.ReadOnly = true;
            // 
            // due_date
            // 
            this.due_date.HeaderText = "Due Date";
            this.due_date.MinimumWidth = 100;
            this.due_date.Name = "due_date";
            this.due_date.ReadOnly = true;
            this.due_date.Visible = false;
            // 
            // doc_status
            // 
            this.doc_status.HeaderText = "Doc Status";
            this.doc_status.MinimumWidth = 100;
            this.doc_status.Name = "doc_status";
            this.doc_status.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "Remarks";
            this.remarks.MinimumWidth = 150;
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // transfer_id
            // 
            this.transfer_id.HeaderText = "Transfer ID";
            this.transfer_id.MinimumWidth = 100;
            this.transfer_id.Name = "transfer_id";
            this.transfer_id.ReadOnly = true;
            this.transfer_id.Visible = false;
            // 
            // transfer_ref
            // 
            this.transfer_ref.HeaderText = "Transfer Reference";
            this.transfer_ref.MinimumWidth = 150;
            this.transfer_ref.Name = "transfer_ref";
            this.transfer_ref.ReadOnly = true;
            // 
            // transfer_date
            // 
            this.transfer_date.HeaderText = "Transfer Date";
            this.transfer_date.MinimumWidth = 150;
            this.transfer_date.Name = "transfer_date";
            this.transfer_date.ReadOnly = true;
            // 
            // btnViewRemarks
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.btnViewRemarks.DefaultCellStyle = dataGridViewCellStyle2;
            this.btnViewRemarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewRemarks.HeaderText = "Action";
            this.btnViewRemarks.Name = "btnViewRemarks";
            this.btnViewRemarks.ReadOnly = true;
            this.btnViewRemarks.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnViewRemarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnViewRemarks.Text = "View Remarks";
            this.btnViewRemarks.UseColumnTextForButtonValue = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(31, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 16);
            this.label13.TabIndex = 72;
            this.label13.Text = "To Department:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.DimGray;
            this.Label3.Location = new System.Drawing.Point(31, 55);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(123, 16);
            this.Label3.TabIndex = 71;
            this.Label3.Text = "From Department:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(784, 446);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 29);
            this.btnRefresh.TabIndex = 100;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // checkTransDate
            // 
            this.checkTransDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkTransDate.AutoSize = true;
            this.checkTransDate.Checked = true;
            this.checkTransDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTransDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTransDate.ForeColor = System.Drawing.Color.Black;
            this.checkTransDate.Location = new System.Drawing.Point(585, 87);
            this.checkTransDate.Name = "checkTransDate";
            this.checkTransDate.Size = new System.Drawing.Size(15, 14);
            this.checkTransDate.TabIndex = 103;
            this.checkTransDate.UseVisualStyleBackColor = true;
            this.checkTransDate.CheckedChanged += new System.EventHandler(this.checkTransDate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(606, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 102;
            this.label2.Text = "Trans. Date:";
            // 
            // dtTransDate
            // 
            this.dtTransDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTransDate.CustomFormat = "yyyy-MM-dd";
            this.dtTransDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTransDate.Location = new System.Drawing.Point(722, 82);
            this.dtTransDate.Name = "dtTransDate";
            this.dtTransDate.Size = new System.Drawing.Size(154, 22);
            this.dtTransDate.TabIndex = 101;
            // 
            // btnSearchQuery
            // 
            this.btnSearchQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchQuery.BackColor = System.Drawing.Color.Silver;
            this.btnSearchQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchQuery.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnSearchQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchQuery.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchQuery.ForeColor = System.Drawing.Color.Black;
            this.btnSearchQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchQuery.Image")));
            this.btnSearchQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchQuery.Location = new System.Drawing.Point(722, 113);
            this.btnSearchQuery.Name = "btnSearchQuery";
            this.btnSearchQuery.Size = new System.Drawing.Size(154, 32);
            this.btnSearchQuery.TabIndex = 115;
            this.btnSearchQuery.Text = "Search Query";
            this.btnSearchQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchQuery.UseVisualStyleBackColor = false;
            this.btnSearchQuery.Click += new System.EventHandler(this.btnSearchQuery_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(34, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 32);
            this.button1.TabIndex = 116;
            this.button1.Text = "Search Query";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(31, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 132;
            this.label7.Text = "Plant:";
            // 
            // cmbPlant
            // 
            this.cmbPlant.Location = new System.Drawing.Point(173, 21);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPlant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPlant.Size = new System.Drawing.Size(157, 20);
            this.cmbPlant.TabIndex = 131;
            this.cmbPlant.SelectedIndexChanged += new System.EventHandler(this.cmbPlant_SelectedIndexChanged);
            // 
            // cmbFromBranch
            // 
            this.cmbFromBranch.Location = new System.Drawing.Point(173, 52);
            this.cmbFromBranch.Name = "cmbFromBranch";
            this.cmbFromBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFromBranch.Size = new System.Drawing.Size(157, 20);
            this.cmbFromBranch.TabIndex = 133;
            // 
            // cmbToBranch
            // 
            this.cmbToBranch.Location = new System.Drawing.Point(173, 87);
            this.cmbToBranch.Name = "cmbToBranch";
            this.cmbToBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbToBranch.Size = new System.Drawing.Size(157, 20);
            this.cmbToBranch.TabIndex = 134;
            // 
            // ItemRequest2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(913, 505);
            this.Controls.Add(this.cmbToBranch);
            this.Controls.Add(this.cmbFromBranch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbPlant);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSearchQuery);
            this.Controls.Add(this.checkTransDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtTransDate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.checkDueDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtDueDate);
            this.Controls.Add(this.lblNoDataFound);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemRequest2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ItemRequest2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkDueDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDueDate;
        private System.Windows.Forms.Label lblNoDataFound;
        private System.Windows.Forms.DataGridView dgv;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox checkTransDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTransDate;
        internal System.Windows.Forms.Button btnSearchQuery;
        internal System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPlant;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFromBranch;
        private DevExpress.XtraEditors.ComboBoxEdit cmbToBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn transdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn from_branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn to_branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn due_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn doc_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfer_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfer_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfer_date;
        private System.Windows.Forms.DataGridViewButtonColumn btnViewRemarks;
    }
}