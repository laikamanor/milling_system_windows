namespace AB
{
    partial class Transfer2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transfer2));
            this.label3 = new System.Windows.Forms.Label();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.lblNoDataFound = new System.Windows.Forms.Label();
            this.txtsearchTransactions = new System.Windows.Forms.TextBox();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbWhse = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbToWhse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.checkFromDate = new System.Windows.Forms.CheckBox();
            this.checkToDate = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBranchToBranch = new System.Windows.Forms.CheckBox();
            this.btnSearchQuery = new System.Windows.Forms.Button();
            this.btnSearchQuery2 = new System.Windows.Forms.Button();
            this.cmbToTime = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbFromTime = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbPlant = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from_whse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to_whse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sap_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.variance_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rec_reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rec_trandate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_branch_to_branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_confirmed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inter_whse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plate_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.agi_truck_scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chti_truck_scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vessel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_close = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlant.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(581, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "To Date:";
            // 
            // dtToDate
            // 
            this.dtToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtToDate.CustomFormat = "yyyy-MM-dd";
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDate.Location = new System.Drawing.Point(676, 145);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(117, 22);
            this.dtToDate.TabIndex = 40;
            // 
            // lblNoDataFound
            // 
            this.lblNoDataFound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNoDataFound.AutoSize = true;
            this.lblNoDataFound.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoDataFound.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoDataFound.Location = new System.Drawing.Point(359, 320);
            this.lblNoDataFound.Name = "lblNoDataFound";
            this.lblNoDataFound.Size = new System.Drawing.Size(105, 18);
            this.lblNoDataFound.TabIndex = 39;
            this.lblNoDataFound.Text = "No data found";
            // 
            // txtsearchTransactions
            // 
            this.txtsearchTransactions.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtsearchTransactions.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtsearchTransactions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearchTransactions.ForeColor = System.Drawing.Color.DimGray;
            this.txtsearchTransactions.Location = new System.Drawing.Point(9, 205);
            this.txtsearchTransactions.Name = "txtsearchTransactions";
            this.txtsearchTransactions.Size = new System.Drawing.Size(299, 21);
            this.txtsearchTransactions.TabIndex = 37;
            this.txtsearchTransactions.Text = "Search Reference...";
            this.txtsearchTransactions.Enter += new System.EventHandler(this.txtsearchTransactions_Enter);
            this.txtsearchTransactions.Leave += new System.EventHandler(this.txtsearchTransactions_Leave);
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTransactions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransactions.ColumnHeadersHeight = 40;
            this.dgvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.transnumber,
            this.transdate,
            this.reference,
            this.from_whse,
            this.to_whse,
            this.transtype,
            this.sap_number,
            this.docstatus,
            this.variance_count,
            this.rec_reference,
            this.rec_trandate,
            this.is_branch_to_branch,
            this.date_confirmed,
            this.inter_whse,
            this.plate_num,
            this.shift,
            this.agi_truck_scale,
            this.chti_truck_scale,
            this.vessel,
            this.driver,
            this.remarks,
            this.date_close});
            this.dgvTransactions.EnableHeadersVisualStyles = false;
            this.dgvTransactions.GridColor = System.Drawing.Color.DarkGray;
            this.dgvTransactions.Location = new System.Drawing.Point(9, 267);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.RowHeadersWidth = 10;
            this.dgvTransactions.Size = new System.Drawing.Size(784, 142);
            this.dgvTransactions.TabIndex = 34;
            this.dgvTransactions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransactions_CellContentClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 45;
            this.label1.Text = "From Warehouse:";
            // 
            // cmbWhse
            // 
            this.cmbWhse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbWhse.BackColor = System.Drawing.SystemColors.Control;
            this.cmbWhse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbWhse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWhse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWhse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWhse.ForeColor = System.Drawing.Color.Black;
            this.cmbWhse.FormattingEnabled = true;
            this.cmbWhse.Location = new System.Drawing.Point(145, 42);
            this.cmbWhse.Name = "cmbWhse";
            this.cmbWhse.Size = new System.Drawing.Size(154, 24);
            this.cmbWhse.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 47;
            this.label5.Text = "To Warehouse:";
            // 
            // cmbToWhse
            // 
            this.cmbToWhse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbToWhse.BackColor = System.Drawing.SystemColors.Control;
            this.cmbToWhse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbToWhse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToWhse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbToWhse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToWhse.ForeColor = System.Drawing.Color.Black;
            this.cmbToWhse.FormattingEnabled = true;
            this.cmbToWhse.Location = new System.Drawing.Point(145, 74);
            this.cmbToWhse.Name = "cmbToWhse";
            this.cmbToWhse.Size = new System.Drawing.Size(154, 24);
            this.cmbToWhse.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(581, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 49;
            this.label6.Text = "From Date:";
            // 
            // dtFromDate
            // 
            this.dtFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFromDate.CustomFormat = "yyyy-MM-dd";
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDate.Location = new System.Drawing.Point(676, 117);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(117, 22);
            this.dtFromDate.TabIndex = 48;
            // 
            // checkFromDate
            // 
            this.checkFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkFromDate.AutoSize = true;
            this.checkFromDate.Checked = true;
            this.checkFromDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkFromDate.Location = new System.Drawing.Point(560, 124);
            this.checkFromDate.Name = "checkFromDate";
            this.checkFromDate.Size = new System.Drawing.Size(15, 14);
            this.checkFromDate.TabIndex = 50;
            this.checkFromDate.UseVisualStyleBackColor = true;
            this.checkFromDate.CheckedChanged += new System.EventHandler(this.checkFromDate_CheckedChanged);
            // 
            // checkToDate
            // 
            this.checkToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkToDate.AutoSize = true;
            this.checkToDate.Checked = true;
            this.checkToDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkToDate.Location = new System.Drawing.Point(560, 152);
            this.checkToDate.Name = "checkToDate";
            this.checkToDate.Size = new System.Drawing.Size(15, 14);
            this.checkToDate.TabIndex = 51;
            this.checkToDate.UseVisualStyleBackColor = true;
            this.checkToDate.CheckedChanged += new System.EventHandler(this.checkToDate_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBranchToBranch);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbWhse);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbToWhse);
            this.groupBox1.Location = new System.Drawing.Point(9, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 109);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            // 
            // checkBranchToBranch
            // 
            this.checkBranchToBranch.AutoSize = true;
            this.checkBranchToBranch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBranchToBranch.Location = new System.Drawing.Point(9, 16);
            this.checkBranchToBranch.Name = "checkBranchToBranch";
            this.checkBranchToBranch.Size = new System.Drawing.Size(209, 20);
            this.checkBranchToBranch.TabIndex = 48;
            this.checkBranchToBranch.Text = "Is Department to Department";
            this.checkBranchToBranch.UseVisualStyleBackColor = true;
            this.checkBranchToBranch.CheckedChanged += new System.EventHandler(this.checkBranchToBranch_CheckedChanged);
            // 
            // btnSearchQuery
            // 
            this.btnSearchQuery.BackColor = System.Drawing.Color.Silver;
            this.btnSearchQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchQuery.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnSearchQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchQuery.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchQuery.ForeColor = System.Drawing.Color.Black;
            this.btnSearchQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchQuery.Image")));
            this.btnSearchQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchQuery.Location = new System.Drawing.Point(9, 233);
            this.btnSearchQuery.Name = "btnSearchQuery";
            this.btnSearchQuery.Size = new System.Drawing.Size(147, 32);
            this.btnSearchQuery.TabIndex = 53;
            this.btnSearchQuery.Text = "Search Query";
            this.btnSearchQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchQuery.UseVisualStyleBackColor = false;
            this.btnSearchQuery.Click += new System.EventHandler(this.btnSearchQuery_Click);
            // 
            // btnSearchQuery2
            // 
            this.btnSearchQuery2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchQuery2.BackColor = System.Drawing.Color.Silver;
            this.btnSearchQuery2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchQuery2.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnSearchQuery2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchQuery2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchQuery2.ForeColor = System.Drawing.Color.Black;
            this.btnSearchQuery2.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchQuery2.Image")));
            this.btnSearchQuery2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchQuery2.Location = new System.Drawing.Point(646, 233);
            this.btnSearchQuery2.Name = "btnSearchQuery2";
            this.btnSearchQuery2.Size = new System.Drawing.Size(147, 32);
            this.btnSearchQuery2.TabIndex = 54;
            this.btnSearchQuery2.Text = "Search Query";
            this.btnSearchQuery2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchQuery2.UseVisualStyleBackColor = false;
            this.btnSearchQuery2.Click += new System.EventHandler(this.btnSearchQuery2_Click);
            // 
            // cmbToTime
            // 
            this.cmbToTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbToTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.cmbToTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbToTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbToTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToTime.ForeColor = System.Drawing.Color.Black;
            this.cmbToTime.FormattingEnabled = true;
            this.cmbToTime.Items.AddRange(new object[] {
            "00:00",
            "01:00",
            "02:00",
            "03:00",
            "04:00",
            "05:00",
            "06:00",
            "07:00",
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00",
            "19:00",
            "20:00",
            "21:00",
            "22:00",
            "23:00",
            "23:59"});
            this.cmbToTime.Location = new System.Drawing.Point(676, 203);
            this.cmbToTime.Name = "cmbToTime";
            this.cmbToTime.Size = new System.Drawing.Size(117, 23);
            this.cmbToTime.TabIndex = 69;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(581, 208);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 15);
            this.label12.TabIndex = 68;
            this.label12.Text = "To Time:";
            // 
            // cmbFromTime
            // 
            this.cmbFromTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFromTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.cmbFromTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFromTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFromTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromTime.ForeColor = System.Drawing.Color.Black;
            this.cmbFromTime.FormattingEnabled = true;
            this.cmbFromTime.Items.AddRange(new object[] {
            "00:00",
            "01:00",
            "02:00",
            "03:00",
            "04:00",
            "05:00",
            "06:00",
            "07:00",
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00",
            "19:00",
            "20:00",
            "21:00",
            "22:00",
            "23:00",
            "23:59"});
            this.cmbFromTime.Location = new System.Drawing.Point(676, 174);
            this.cmbFromTime.Name = "cmbFromTime";
            this.cmbFromTime.Size = new System.Drawing.Size(117, 23);
            this.cmbFromTime.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(581, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 66;
            this.label2.Text = "From Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(15, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 130;
            this.label7.Text = "Plant:";
            // 
            // cmbBranch
            // 
            this.cmbBranch.Location = new System.Drawing.Point(105, 67);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbBranch.Size = new System.Drawing.Size(157, 20);
            this.cmbBranch.TabIndex = 129;
            this.cmbBranch.SelectedIndexChanged += new System.EventHandler(this.cmbBranch_SelectedIndexChanged);
            // 
            // cmbPlant
            // 
            this.cmbPlant.Location = new System.Drawing.Point(105, 36);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPlant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPlant.Size = new System.Drawing.Size(157, 20);
            this.cmbPlant.TabIndex = 128;
            this.cmbPlant.SelectedIndexChanged += new System.EventHandler(this.cmbPlant_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(15, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 16);
            this.label8.TabIndex = 127;
            this.label8.Text = "Department:";
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // transnumber
            // 
            this.transnumber.HeaderText = "Trans. #";
            this.transnumber.Name = "transnumber";
            this.transnumber.ReadOnly = true;
            this.transnumber.Visible = false;
            // 
            // transdate
            // 
            this.transdate.HeaderText = "Trans. Date";
            this.transdate.Name = "transdate";
            this.transdate.ReadOnly = true;
            // 
            // reference
            // 
            this.reference.HeaderText = "Reference";
            this.reference.Name = "reference";
            this.reference.ReadOnly = true;
            // 
            // from_whse
            // 
            this.from_whse.HeaderText = "From Warehouse";
            this.from_whse.Name = "from_whse";
            this.from_whse.ReadOnly = true;
            // 
            // to_whse
            // 
            this.to_whse.HeaderText = "To Warehouse";
            this.to_whse.Name = "to_whse";
            this.to_whse.ReadOnly = true;
            // 
            // transtype
            // 
            this.transtype.HeaderText = "Transtype";
            this.transtype.Name = "transtype";
            this.transtype.ReadOnly = true;
            // 
            // sap_number
            // 
            this.sap_number.HeaderText = "SAP #";
            this.sap_number.Name = "sap_number";
            this.sap_number.ReadOnly = true;
            // 
            // docstatus
            // 
            this.docstatus.HeaderText = "Doc. Status";
            this.docstatus.Name = "docstatus";
            this.docstatus.ReadOnly = true;
            this.docstatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.docstatus.Visible = false;
            // 
            // variance_count
            // 
            this.variance_count.HeaderText = "Variance";
            this.variance_count.Name = "variance_count";
            this.variance_count.ReadOnly = true;
            this.variance_count.Visible = false;
            // 
            // rec_reference
            // 
            this.rec_reference.HeaderText = "Rec. Reference";
            this.rec_reference.Name = "rec_reference";
            this.rec_reference.ReadOnly = true;
            // 
            // rec_trandate
            // 
            this.rec_trandate.HeaderText = "Rec. Transdate";
            this.rec_trandate.Name = "rec_trandate";
            this.rec_trandate.ReadOnly = true;
            // 
            // is_branch_to_branch
            // 
            this.is_branch_to_branch.HeaderText = "is_branch_to_branch";
            this.is_branch_to_branch.Name = "is_branch_to_branch";
            this.is_branch_to_branch.Visible = false;
            // 
            // date_confirmed
            // 
            this.date_confirmed.HeaderText = "Date Confirmed";
            this.date_confirmed.Name = "date_confirmed";
            this.date_confirmed.ReadOnly = true;
            // 
            // inter_whse
            // 
            this.inter_whse.HeaderText = "Inter Whse";
            this.inter_whse.Name = "inter_whse";
            this.inter_whse.ReadOnly = true;
            this.inter_whse.Visible = false;
            // 
            // plate_num
            // 
            this.plate_num.HeaderText = "Plate Num";
            this.plate_num.Name = "plate_num";
            this.plate_num.ReadOnly = true;
            // 
            // shift
            // 
            this.shift.HeaderText = "Shift";
            this.shift.Name = "shift";
            this.shift.ReadOnly = true;
            // 
            // agi_truck_scale
            // 
            this.agi_truck_scale.HeaderText = "AGI Truck Scale";
            this.agi_truck_scale.Name = "agi_truck_scale";
            this.agi_truck_scale.ReadOnly = true;
            // 
            // chti_truck_scale
            // 
            this.chti_truck_scale.HeaderText = "CHTI Truck Scale";
            this.chti_truck_scale.Name = "chti_truck_scale";
            this.chti_truck_scale.ReadOnly = true;
            // 
            // vessel
            // 
            this.vessel.HeaderText = "Vessel";
            this.vessel.Name = "vessel";
            this.vessel.ReadOnly = true;
            // 
            // driver
            // 
            this.driver.HeaderText = "Driver";
            this.driver.Name = "driver";
            this.driver.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "Remarks";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // date_close
            // 
            this.date_close.HeaderText = "Date Close";
            this.date_close.Name = "date_close";
            this.date_close.ReadOnly = true;
            this.date_close.Visible = false;
            // 
            // Transfer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(799, 446);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.cmbPlant);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbToTime);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbFromTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearchQuery2);
            this.Controls.Add(this.btnSearchQuery);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkToDate);
            this.Controls.Add(this.checkFromDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtFromDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtToDate);
            this.Controls.Add(this.lblNoDataFound);
            this.Controls.Add(this.txtsearchTransactions);
            this.Controls.Add(this.dgvTransactions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Transfer2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "x";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transfer2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlant.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label lblNoDataFound;
        internal System.Windows.Forms.TextBox txtsearchTransactions;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWhse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbToWhse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.CheckBox checkFromDate;
        private System.Windows.Forms.CheckBox checkToDate;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBranchToBranch;
        internal System.Windows.Forms.Button btnSearchQuery;
        internal System.Windows.Forms.Button btnSearchQuery2;
        internal System.Windows.Forms.ComboBox cmbToTime;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.ComboBox cmbFromTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ComboBoxEdit cmbBranch;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPlant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn transnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn transdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn from_whse;
        private System.Windows.Forms.DataGridViewTextBoxColumn to_whse;
        private System.Windows.Forms.DataGridViewTextBoxColumn transtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn sap_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn docstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn variance_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn rec_reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn rec_trandate;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_branch_to_branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_confirmed;
        private System.Windows.Forms.DataGridViewTextBoxColumn inter_whse;
        private System.Windows.Forms.DataGridViewTextBoxColumn plate_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn shift;
        private System.Windows.Forms.DataGridViewTextBoxColumn agi_truck_scale;
        private System.Windows.Forms.DataGridViewTextBoxColumn chti_truck_scale;
        private System.Windows.Forms.DataGridViewTextBoxColumn vessel;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_close;
    }
}