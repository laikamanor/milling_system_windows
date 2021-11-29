namespace AB
{
    partial class ItemRequest3
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemRequest3));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grd = new System.Windows.Forms.DataGridView();
            this.btnSearchQuery = new System.Windows.Forms.Button();
            this.checkTransDate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTransDate = new System.Windows.Forms.DateTimePicker();
            this.cmbToBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbFromBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPlant = new DevExpress.XtraEditors.ComboBoxEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.transdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from_department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to_department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.view_remarks = new System.Windows.Forms.DataGridViewButtonColumn();
            this.transfer_reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transfer_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transfer_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grd
            // 
            this.grd.AllowUserToAddRows = false;
            this.grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd.BackgroundColor = System.Drawing.Color.White;
            this.grd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grd.ColumnHeadersHeight = 40;
            this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.transdate,
            this.reference,
            this.from_department,
            this.to_department,
            this.remarks,
            this.view_remarks,
            this.transfer_reference,
            this.transfer_date,
            this.id,
            this.transfer_id});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd.DefaultCellStyle = dataGridViewCellStyle8;
            this.grd.EnableHeadersVisualStyles = false;
            this.grd.Location = new System.Drawing.Point(12, 161);
            this.grd.Name = "grd";
            this.grd.RowHeadersWidth = 20;
            this.grd.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            this.grd.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd.Size = new System.Drawing.Size(769, 315);
            this.grd.TabIndex = 184;
            this.grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellContentClick);
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
            this.btnSearchQuery.Location = new System.Drawing.Point(627, 123);
            this.btnSearchQuery.Name = "btnSearchQuery";
            this.btnSearchQuery.Size = new System.Drawing.Size(154, 32);
            this.btnSearchQuery.TabIndex = 195;
            this.btnSearchQuery.Text = "Search Query";
            this.btnSearchQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchQuery.UseVisualStyleBackColor = false;
            this.btnSearchQuery.Click += new System.EventHandler(this.btnSearchQuery_Click);
            // 
            // checkTransDate
            // 
            this.checkTransDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkTransDate.AutoSize = true;
            this.checkTransDate.Checked = true;
            this.checkTransDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTransDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTransDate.ForeColor = System.Drawing.Color.Black;
            this.checkTransDate.Location = new System.Drawing.Point(490, 97);
            this.checkTransDate.Name = "checkTransDate";
            this.checkTransDate.Size = new System.Drawing.Size(15, 14);
            this.checkTransDate.TabIndex = 194;
            this.checkTransDate.UseVisualStyleBackColor = true;
            this.checkTransDate.CheckedChanged += new System.EventHandler(this.checkTransDate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(511, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 193;
            this.label2.Text = "Trans. Date:";
            // 
            // dtTransDate
            // 
            this.dtTransDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTransDate.CustomFormat = "yyyy-MM-dd";
            this.dtTransDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTransDate.Location = new System.Drawing.Point(627, 92);
            this.dtTransDate.Name = "dtTransDate";
            this.dtTransDate.Size = new System.Drawing.Size(154, 22);
            this.dtTransDate.TabIndex = 192;
            // 
            // cmbToBranch
            // 
            this.cmbToBranch.Location = new System.Drawing.Point(151, 97);
            this.cmbToBranch.Name = "cmbToBranch";
            this.cmbToBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbToBranch.Size = new System.Drawing.Size(157, 20);
            this.cmbToBranch.TabIndex = 191;
            // 
            // cmbFromBranch
            // 
            this.cmbFromBranch.Location = new System.Drawing.Point(151, 62);
            this.cmbFromBranch.Name = "cmbFromBranch";
            this.cmbFromBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFromBranch.Size = new System.Drawing.Size(157, 20);
            this.cmbFromBranch.TabIndex = 190;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(9, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 189;
            this.label7.Text = "Plant:";
            // 
            // cmbPlant
            // 
            this.cmbPlant.Location = new System.Drawing.Point(151, 31);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPlant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPlant.Size = new System.Drawing.Size(157, 20);
            this.cmbPlant.TabIndex = 188;
            this.cmbPlant.SelectedIndexChanged += new System.EventHandler(this.cmbPlant_SelectedIndexChanged);
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
            this.button1.Location = new System.Drawing.Point(12, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 32);
            this.button1.TabIndex = 187;
            this.button1.Text = "Search Query";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(9, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 16);
            this.label13.TabIndex = 186;
            this.label13.Text = "To Department:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.DimGray;
            this.Label3.Location = new System.Drawing.Point(9, 65);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(123, 16);
            this.Label3.TabIndex = 185;
            this.Label3.Text = "From Department:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // transdate
            // 
            dataGridViewCellStyle6.Format = "yyyy-MM-dd HH:mm:ss";
            this.transdate.DefaultCellStyle = dataGridViewCellStyle6;
            this.transdate.HeaderText = "Transdate";
            this.transdate.Name = "transdate";
            this.transdate.ReadOnly = true;
            // 
            // reference
            // 
            this.reference.HeaderText = "Reference";
            this.reference.Name = "reference";
            this.reference.ReadOnly = true;
            // 
            // from_department
            // 
            this.from_department.HeaderText = "From Department";
            this.from_department.Name = "from_department";
            this.from_department.ReadOnly = true;
            // 
            // to_department
            // 
            this.to_department.HeaderText = "To Department";
            this.to_department.Name = "to_department";
            this.to_department.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "Remarks";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // view_remarks
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(214)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.view_remarks.DefaultCellStyle = dataGridViewCellStyle7;
            this.view_remarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.view_remarks.HeaderText = "Action";
            this.view_remarks.Name = "view_remarks";
            this.view_remarks.ReadOnly = true;
            this.view_remarks.Text = "View Remarks";
            this.view_remarks.UseColumnTextForButtonValue = true;
            // 
            // transfer_reference
            // 
            this.transfer_reference.HeaderText = "Transfer Reference";
            this.transfer_reference.Name = "transfer_reference";
            this.transfer_reference.ReadOnly = true;
            // 
            // transfer_date
            // 
            this.transfer_date.HeaderText = "Transfer Date";
            this.transfer_date.Name = "transfer_date";
            this.transfer_date.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // transfer_id
            // 
            this.transfer_id.HeaderText = "transfer id";
            this.transfer_id.Name = "transfer_id";
            this.transfer_id.ReadOnly = true;
            this.transfer_id.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 482);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 197;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(55, 486);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 196;
            this.label1.Text = "Same Reference";
            // 
            // ItemRequest3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(793, 516);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearchQuery);
            this.Controls.Add(this.checkTransDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtTransDate);
            this.Controls.Add(this.cmbToBranch);
            this.Controls.Add(this.cmbFromBranch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbPlant);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.grd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemRequest3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Request";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ItemRequest3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grd;
        internal System.Windows.Forms.Button btnSearchQuery;
        private System.Windows.Forms.CheckBox checkTransDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTransDate;
        private DevExpress.XtraEditors.ComboBoxEdit cmbToBranch;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFromBranch;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPlant;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label Label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn transdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn from_department;
        private System.Windows.Forms.DataGridViewTextBoxColumn to_department;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewButtonColumn view_remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfer_reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfer_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn transfer_id;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}