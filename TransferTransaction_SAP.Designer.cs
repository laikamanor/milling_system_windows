namespace AB
{
    partial class TransferTransaction_SAP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferTransaction_SAP));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cmbPerPage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFromBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbToBranch = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbToWhse = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFromWhse = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.checkFromDate = new System.Windows.Forms.CheckBox();
            this.checkToDate = new System.Windows.Forms.CheckBox();
            this.panelPagination = new System.Windows.Forms.Panel();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnViewItems = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearchQuery = new System.Windows.Forms.Button();
            this.btnSearchQuery2 = new System.Windows.Forms.Button();
            this.cmbToTime = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbFromTime = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPerPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToWhse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromWhse.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 184);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(669, 283);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.ReadOnly = true;
            this.repositoryItemTextEdit1.Click += new System.EventHandler(this.repositoryItemTextEdit1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // cmbPerPage
            // 
            this.cmbPerPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbPerPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbPerPage.Location = new System.Drawing.Point(77, 10);
            this.cmbPerPage.Name = "cmbPerPage";
            this.cmbPerPage.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbPerPage.Properties.Appearance.Options.UseFont = true;
            this.cmbPerPage.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbPerPage.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbPerPage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPerPage.Properties.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "200"});
            this.cmbPerPage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPerPage.Size = new System.Drawing.Size(115, 22);
            this.cmbPerPage.TabIndex = 1;
            this.cmbPerPage.SelectedIndexChanged += new System.EventHandler(this.cmbPerPage_SelectedIndexChanged);
            this.cmbPerPage.SelectedValueChanged += new System.EventHandler(this.cmbPerPage_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Per Page:";
            // 
            // dtToDate
            // 
            this.dtToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtToDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtToDate.EditValue = null;
            this.dtToDate.Location = new System.Drawing.Point(574, 59);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dtToDate.Properties.Appearance.Options.UseFont = true;
            this.dtToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtToDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtToDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtToDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtToDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtToDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dtToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtToDate.Size = new System.Drawing.Size(107, 22);
            this.dtToDate.TabIndex = 92;
            // 
            // dtFromDate
            // 
            this.dtFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFromDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtFromDate.EditValue = null;
            this.dtFromDate.Location = new System.Drawing.Point(574, 29);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dtFromDate.Properties.Appearance.Options.UseFont = true;
            this.dtFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFromDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtFromDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtFromDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dtFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtFromDate.Size = new System.Drawing.Size(107, 22);
            this.dtFromDate.TabIndex = 91;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(471, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 90;
            this.label3.Text = "To Date:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(471, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 89;
            this.label5.Text = "From Date:";
            // 
            // cmbFromBranch
            // 
            this.cmbFromBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFromBranch.Location = new System.Drawing.Point(112, 28);
            this.cmbFromBranch.Name = "cmbFromBranch";
            this.cmbFromBranch.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmbFromBranch.Properties.Appearance.Options.UseFont = true;
            this.cmbFromBranch.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromBranch.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbFromBranch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cmbFromBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFromBranch.Size = new System.Drawing.Size(188, 22);
            this.cmbFromBranch.TabIndex = 94;
            this.cmbFromBranch.SelectedValueChanged += new System.EventHandler(this.cmbFromBranch_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(9, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 93;
            this.label2.Text = "From Branch:";
            // 
            // cmbToBranch
            // 
            this.cmbToBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbToBranch.Location = new System.Drawing.Point(112, 59);
            this.cmbToBranch.Name = "cmbToBranch";
            this.cmbToBranch.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmbToBranch.Properties.Appearance.Options.UseFont = true;
            this.cmbToBranch.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToBranch.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbToBranch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cmbToBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbToBranch.Size = new System.Drawing.Size(188, 22);
            this.cmbToBranch.TabIndex = 96;
            this.cmbToBranch.SelectedValueChanged += new System.EventHandler(this.cmbToBranch_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(9, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 95;
            this.label4.Text = "To Branch:";
            // 
            // cmbToWhse
            // 
            this.cmbToWhse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbToWhse.Location = new System.Drawing.Point(112, 121);
            this.cmbToWhse.Name = "cmbToWhse";
            this.cmbToWhse.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmbToWhse.Properties.Appearance.Options.UseFont = true;
            this.cmbToWhse.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToWhse.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbToWhse.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cmbToWhse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToWhse.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbToWhse.Size = new System.Drawing.Size(188, 22);
            this.cmbToWhse.TabIndex = 100;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(9, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 16);
            this.label6.TabIndex = 99;
            this.label6.Text = "To Whse:";
            // 
            // cmbFromWhse
            // 
            this.cmbFromWhse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFromWhse.Location = new System.Drawing.Point(112, 90);
            this.cmbFromWhse.Name = "cmbFromWhse";
            this.cmbFromWhse.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmbFromWhse.Properties.Appearance.Options.UseFont = true;
            this.cmbFromWhse.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromWhse.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbFromWhse.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cmbFromWhse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromWhse.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFromWhse.Size = new System.Drawing.Size(188, 22);
            this.cmbFromWhse.TabIndex = 98;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(9, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 16);
            this.label7.TabIndex = 97;
            this.label7.Text = "From Whse:";
            // 
            // checkFromDate
            // 
            this.checkFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkFromDate.AutoSize = true;
            this.checkFromDate.Checked = true;
            this.checkFromDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkFromDate.Location = new System.Drawing.Point(450, 36);
            this.checkFromDate.Name = "checkFromDate";
            this.checkFromDate.Size = new System.Drawing.Size(15, 14);
            this.checkFromDate.TabIndex = 101;
            this.checkFromDate.UseVisualStyleBackColor = true;
            this.checkFromDate.CheckedChanged += new System.EventHandler(this.checkFromDate_CheckedChanged);
            // 
            // checkToDate
            // 
            this.checkToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkToDate.AutoSize = true;
            this.checkToDate.Checked = true;
            this.checkToDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkToDate.Location = new System.Drawing.Point(450, 64);
            this.checkToDate.Name = "checkToDate";
            this.checkToDate.Size = new System.Drawing.Size(15, 14);
            this.checkToDate.TabIndex = 102;
            this.checkToDate.UseVisualStyleBackColor = true;
            this.checkToDate.CheckedChanged += new System.EventHandler(this.checkToDate_CheckedChanged);
            // 
            // panelPagination
            // 
            this.panelPagination.BackColor = System.Drawing.Color.White;
            this.panelPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPagination.Location = new System.Drawing.Point(0, 514);
            this.panelPagination.Name = "panelPagination";
            this.panelPagination.Size = new System.Drawing.Size(693, 47);
            this.panelPagination.TabIndex = 103;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnViewItems);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbPerPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 41);
            this.panel1.TabIndex = 105;
            // 
            // btnViewItems
            // 
            this.btnViewItems.BackColor = System.Drawing.Color.Silver;
            this.btnViewItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewItems.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnViewItems.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnViewItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewItems.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewItems.Image = ((System.Drawing.Image)(resources.GetObject("btnViewItems.Image")));
            this.btnViewItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewItems.Location = new System.Drawing.Point(474, 0);
            this.btnViewItems.Name = "btnViewItems";
            this.btnViewItems.Size = new System.Drawing.Size(205, 41);
            this.btnViewItems.TabIndex = 106;
            this.btnViewItems.Text = "Consolidated for SAP #";
            this.btnViewItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewItems.UseVisualStyleBackColor = false;
            this.btnViewItems.Click += new System.EventHandler(this.btnViewItems_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(679, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(14, 41);
            this.panel2.TabIndex = 105;
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
            this.btnSearchQuery.Location = new System.Drawing.Point(12, 149);
            this.btnSearchQuery.Name = "btnSearchQuery";
            this.btnSearchQuery.Size = new System.Drawing.Size(147, 32);
            this.btnSearchQuery.TabIndex = 106;
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
            this.btnSearchQuery2.Location = new System.Drawing.Point(534, 149);
            this.btnSearchQuery2.Name = "btnSearchQuery2";
            this.btnSearchQuery2.Size = new System.Drawing.Size(147, 32);
            this.btnSearchQuery2.TabIndex = 107;
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
            this.cmbToTime.Location = new System.Drawing.Point(574, 122);
            this.cmbToTime.Name = "cmbToTime";
            this.cmbToTime.Size = new System.Drawing.Size(107, 23);
            this.cmbToTime.TabIndex = 111;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(469, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 15);
            this.label12.TabIndex = 110;
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
            this.cmbFromTime.Location = new System.Drawing.Point(574, 93);
            this.cmbFromTime.Name = "cmbFromTime";
            this.cmbFromTime.Size = new System.Drawing.Size(107, 23);
            this.cmbFromTime.TabIndex = 109;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(469, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 108;
            this.label8.Text = "From Time:";
            // 
            // TransferTransaction_SAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 561);
            this.Controls.Add(this.cmbToTime);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbFromTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSearchQuery2);
            this.Controls.Add(this.btnSearchQuery);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelPagination);
            this.Controls.Add(this.checkToDate);
            this.Controls.Add(this.checkFromDate);
            this.Controls.Add(this.cmbToWhse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbFromWhse);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbToBranch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbFromBranch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtToDate);
            this.Controls.Add(this.dtFromDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransferTransaction_SAP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAP IT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransferTransaction_SAP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPerPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToWhse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromWhse.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPerPage;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dtToDate;
        private DevExpress.XtraEditors.DateEdit dtFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFromBranch;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbToBranch;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbToWhse;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFromWhse;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.CheckBox checkFromDate;
        private System.Windows.Forms.CheckBox checkToDate;
        private System.Windows.Forms.Panel panelPagination;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnViewItems;
        internal System.Windows.Forms.Button btnSearchQuery;
        internal System.Windows.Forms.Button btnSearchQuery2;
        internal System.Windows.Forms.ComboBox cmbToTime;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.ComboBox cmbFromTime;
        private System.Windows.Forms.Label label8;
    }
}