using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.UI_Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using System.IO;
using RestSharp;

namespace AB
{
    public partial class TransferTransaction_SAP : Form
    {
        public TransferTransaction_SAP(string type)
        {
            gType = type;
            InitializeComponent();
        }
        public string gType = "";
        api_class apic = new api_class();
        utility_class utilityc = new utility_class();
        DataTable dtBranches = new DataTable();
        DataTable dtWarehouses = new DataTable();
        private void TransferTransaction_SAP_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            checkFromDate.Checked = checkToDate.Checked = true;
            dtFromDate.EditValue = dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            cmbPerPage.SelectedIndex = 0;
            btnViewItems.Text = gType.Equals("Open") ? "Consolidated for SAP #" : "View Items";
            btnViewItems.Size = gType.Equals("Open") ? new Size(199, 35) : new Size(122, 35);
            btnViewItems.Image = gType.Equals("Open") ? Properties.Resources.sap : Properties.Resources.view;
            loadBranches(cmbFromBranch);
            loadBranches(cmbToBranch);
            loadWarehouse(cmbFromBranch.Text, cmbFromWhse);
            loadWarehouse(cmbToBranch.Text, cmbToWhse);
            bg(backgroundWorker1);
        }

        public void loadWarehouse(string branch, DevExpress.XtraEditors.ComboBoxEdit cmb)
        {
            try
            {
                cmb.Invoke(new Action(delegate ()
                {
                    cmb.Properties.Items.Clear();
                }));
                string sBranchCode = apic.findValueInDataTable(dtBranches, branch, "name", "code");
                //DataRow[] row = dtWarehouses.Select("branch = '" + sBranchCode + "'");
                //dtWarehouses = row.Length > 0 ? row.CopyToDataTable() : apic.getDtDownloadResources(sResult, "data");
                Console.WriteLine("/api/whse/get_all", "?branch=" + sBranchCode);
                string sResult = "";
                sResult = apic.loadData("/api/whse/get_all", "?branch=" + sBranchCode, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult.Trim()) && sResult.Substring(0, 1).Equals("{"))
                {
                    dtWarehouses = apic.getDtDownloadResources(sResult, "data");

                }
                if (dtWarehouses.Rows.Count > 1)
                {
                    cmb.Invoke(new Action(delegate ()
                    {
                        cmb.Properties.Items.Add("All");
                    }));
                }
                foreach (DataRow row in dtWarehouses.Rows)
                {
                    cmb.Invoke(new Action(delegate ()
                    {
                        cmb.Properties.Items.Add(row["whsename"].ToString());
                    }));
                }
                cmb.Invoke(new Action(delegate ()
                {
                    string whse = cmb.Name.Equals("cmbToWhse") ? "" : (string)Login.jsonResult["data"]["whse"];
                    string s = cmb.Name.Equals("cmbToWhse") ? "" : apic.findValueInDataTable(dtWarehouses, whse, "whsecode", "whsename");

                    cmb.SelectedIndex = cmb.Name.Equals("cmbToWhse") ? 0 : string.IsNullOrEmpty(s.Trim()) ? 0 : cmb.Properties.Items.IndexOf(s);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadBranches(DevExpress.XtraEditors.ComboBoxEdit cmb)
        {
            try
            {
                cmb.Invoke(new Action(delegate ()
                {
                    cmb.Properties.Items.Clear();
                }));
                string[] lists = { "isAdmin", "isSuperAdmin","isManager" };
                if (apic.haveAccess(lists))
                {
                    string sResult = "";

                    sResult = apic.loadData("/api/branch/get_all", "", "", "", Method.GET, true);
                    if (string.IsNullOrEmpty(sResult.Trim())){
                        apic.showCustomMsgBox("Validation", sResult);
                    }
                    else if (sResult.Substring(0, 1).Equals("{"))
                    {
                        Console.WriteLine(sResult);
                        dtBranches = apic.getDtDownloadResources(sResult, "data");
                        cmb.Invoke(new Action(delegate ()
                        {
                            cmb.Properties.Items.Add("All");
                        }));

                        foreach (DataRow row in dtBranches.Rows)
                        {
                           
                            cmb.Invoke(new Action(delegate ()
                            {
                                cmb.Properties.Items.Add(row["name"].ToString());
                            }));
                        }
                        cmb.Invoke(new Action(delegate ()
                        {
                            string branch = cmb.Name.Equals("cmbToBranch") ? "" : (string)Login.jsonResult["data"]["branch"];
                            string s = cmb.Name.Equals("cmbToBranch") ? "" : apic.findValueInDataTable(dtBranches, branch, "code", "name");
                            cmb.SelectedIndex = cmb.Name.Equals("cmbToBranch") ? 0 : cmb.Properties.Items.IndexOf(s);
                        }));
                    }
                    else
                    {
                        apic.showCustomMsgBox("Validation", sResult);
                    }

                }
                else
                {
                    cmb.Invoke(new Action(delegate ()
                    {
                        cmb.Properties.Items.Add(Login.jsonResult["data"]["branch"]);
                        cmb.SelectedIndex = 0;
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadData(string pageNumber)
        {
            Cursor.Current = Cursors.WaitCursor;
            string sHasSApNum = "?has_sap_num=" + (gType.Equals("Open") ? 0 : 1);
            string sPerPage = "&per_page=";
            string sPageNum = "&page=" + pageNumber;
            string sFromDate = "&from_date=", sToDate = "&to_date=";
            bool isFromDate = false, isToDate = false;
            string sFromBranch = "&from_branch=", sToBranch = "&to_branch=", sFromWhse = "&from_whse=", sToWhse = "&to_whse=", sFromTime = "&from_time=", sToTime = "&to_time=";
            cmbPerPage.Invoke(new Action(delegate ()
            {
                sPerPage += cmbPerPage.Text;
            }));
            checkFromDate.Invoke(new Action(delegate ()
            {
                isFromDate = checkFromDate.Checked;
            }));
            checkToDate.Invoke(new Action(delegate ()
            {
                isToDate = checkToDate.Checked;
            }));
            dtFromDate.Invoke(new Action(delegate ()
            {
                sFromDate += !isFromDate ? "" : dtFromDate.Text;
            }));
              dtToDate.Invoke(new Action(delegate ()
            {
                sToDate += !isToDate ? "" : dtToDate.Text;
            }));
            cmbFromBranch.Invoke(new Action(delegate ()
            {
                sFromBranch += apic.findValueInDataTable(dtBranches, cmbFromBranch.Text, "name", "code");
            }));
            cmbToBranch.Invoke(new Action(delegate ()
            {
                sToBranch += apic.findValueInDataTable(dtBranches, cmbToBranch.Text, "name", "code");
            }));
            cmbFromWhse.Invoke(new Action(delegate ()
            {
                sFromWhse += apic.findValueInDataTable(dtWarehouses, cmbFromWhse.Text, "whsename", "whsecode");
            }));
            cmbToWhse.Invoke(new Action(delegate ()
            {
                sToWhse += apic.findValueInDataTable(dtWarehouses, cmbToWhse.Text, "whsename", "whsecode");
            }));
            cmbFromTime.Invoke(new Action(delegate ()
            {
                sFromTime += cmbFromTime.Text;
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime += cmbToTime.Text;
            }));


            string sParams = sHasSApNum + sPerPage + sPageNum + sFromDate + sToDate + sFromBranch + sToBranch + sFromWhse + sToWhse + sFromTime + sToTime;

            string sResult = apic.loadData("/api/inv/trfr/update_sap/get_all", sParams, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jTransfer = (JArray)joResult["data"]["transfer"];
                    JArray jPageList = (JArray)joResult["data"]["page_list"];

                    bool hasNext = false, hasPrev = false;
                    hasNext = (bool)joResult["data"]["has_next"];
                    hasPrev = (bool)joResult["data"]["has_prev"];
                    int currentPage = (int)joResult["data"]["current_page"];
                    loadPagination(jPageList,hasNext, hasPrev,currentPage);
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jTransfer.ToString(), (typeof(DataTable)));
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dt;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string v = col.FieldName;
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = v.ToLower().Equals("transdate") || v.ToLower().Equals("sap_date_updated") ? DevExpress.Utils.FormatType.Custom : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = v.ToLower().Equals("transdate") || v.ToLower().Equals("sap_date_updated") ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.Visible = v.ToLower().Equals("sap_date_updated") && gType.Equals("Open") ? false : col.GetCaption().Equals("Docstatus") || col.GetCaption().Equals("Id") || col.GetCaption().Equals("Seriescode") ? false : true;
                        }
                        if (dt.Rows.Count > 0)
                        {
                            gridView1.OptionsSelection.MultiSelect = true;
                            gridView1.Columns["reference"].Summary.Clear();
                            gridView1.Columns["reference"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "reference", "Count: {0:N0}");
                        }

                        gridView1.BestFitColumns();
                    }));
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public void loadPagination(JArray lists,bool hasNext, bool hasPrev, int currentPage)
        {
            panelPagination.Invoke(new Action(delegate ()
            {
                panelPagination.Controls.Clear();
            }));
            FontFamily fontArial = new FontFamily("Arial");
            Button btnPrev = new Button();
            btnPrev.Size = new Size(70, 30);
            btnPrev.Font = new Font(fontArial, 9, FontStyle.Bold);
            btnPrev.Location = new Point(12, 7);
            btnPrev.Cursor = Cursors.Hand;
            btnPrev.Text = "< Prev";
            btnPrev.BackColor = Color.FromArgb(225, 227, 234);
            btnPrev.ForeColor = Color.FromArgb(76,76,76);
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(130,130,130);
            btnPrev.Enabled = hasPrev;
            btnPrev.Click += new EventHandler(btnPrev_click);

            panelPagination.Invoke(new Action(delegate ()
            {
                panelPagination.Controls.Add(btnPrev);
            }));

            Size btnSize = new Size(52, 30);
            int btnLocX = 87;
            foreach(string list in lists)
            {
                Button btnNumber = new Button();
                btnNumber.Size = btnSize;
                btnNumber.Cursor = Cursors.Hand;
                btnNumber.Location = new Point(btnLocX, 7);
                btnNumber.Text = list;
                btnNumber.Click += new EventHandler(btnNumber_click);

                double num = 0.00, doubleTemp = 0.00;
                num = double.TryParse(list, out doubleTemp) ? Convert.ToDouble(list) : doubleTemp;
                btnNumber.Enabled = num <= 0 ? false : true;
                btnNumber.FlatAppearance.BorderSize = num <= 0 ? 0 : 1;
                btnNumber.FlatAppearance.BorderColor = Color.FromArgb(130, 130, 130);
                btnNumber.Font = new Font(fontArial, 9, FontStyle.Bold);
                btnNumber.FlatStyle = FlatStyle.Flat;
                btnNumber.BackColor = num <=0 ? Color.White : Color.FromArgb(225, 227, 234);
                btnNumber.ForeColor = num <= 0 ? Color.White : Color.FromArgb(76, 76, 76);

                //first
                if (currentPage == num)
                {
                    btnNumber.BackColor = Color.FromArgb(178, 187, 217);
                    btnNumber.ForeColor = Color.FromArgb(76,76,76);
                }

                panelPagination.Invoke(new Action(delegate ()
                {
                    panelPagination.Controls.Add(btnNumber);
                    btnLocX += 54;
                }));
            }
            Button btnNext = new Button();
            btnNext.Size = new Size(70, 30);
            btnNext.Cursor = Cursors.Hand;
            btnNext.Text = "Next >";
            btnNext.Location = new Point(btnLocX, 7);
            btnNext.BackColor = Color.FromArgb(225, 227, 234);
            btnNext.ForeColor = Color.FromArgb(76, 76, 76);
            btnNext.Enabled = hasNext;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(130,130,130);
            btnNext.Font = new Font(fontArial, 9, FontStyle.Bold);
            btnNext.Click += new EventHandler(btnNext_click);
            panelPagination.Invoke(new Action(delegate ()
            {
                panelPagination.Controls.Add(btnNext);
            }));
        }

        private void btnNext_click(object sender, EventArgs e)
        {
            foreach (Control c in panelPagination.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    Button btnC = (c as Button);
                    if (btnC.BackColor == Color.FromArgb(178, 187, 217))
                    {
                        double num = 0.00, doubleTemp = 0.00;
                        num = double.TryParse(btnC.Text, out doubleTemp) ? Convert.ToDouble(btnC.Text) : doubleTemp;
                        num += 1;

                        BackgroundWorker bg1 = new BackgroundWorker();
                        bg1.WorkerReportsProgress = true;
                        bg1.DoWork += delegate
                        {
                            loadData(num.ToString());
                        };
                        bg1.RunWorkerCompleted += delegate
                        {
                            closeForm();
                        };
                        bg(bg1);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("not a button");
                }
            }
        }

        private void btnPrev_click(object sender, EventArgs e)
        {
            foreach (Control c in panelPagination.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    Button btnC = (c as Button);
                   if(btnC.BackColor== Color.FromArgb(178, 187, 217))
                    {
                        double num = 0.00, doubleTemp = 0.00;
                        num = double.TryParse(btnC.Text, out doubleTemp) ? Convert.ToDouble(btnC.Text) : doubleTemp;
                        num -= 1;
                        BackgroundWorker bg1 = new BackgroundWorker();
                        bg1.WorkerReportsProgress = true;
                        bg1.DoWork += delegate
                        {
                            loadData(num.ToString());
                        };
                        bg1.RunWorkerCompleted += delegate
                        {
                            closeForm();
                        };
                        bg(bg1);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("not a button");
                }
            }
        }

        private void btnNumber_click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            BackgroundWorker bg1 = new BackgroundWorker();
            bg1.WorkerReportsProgress = true;
            bg1.DoWork += delegate
            {
                loadData(button.Text);
            };
            bg1.RunWorkerCompleted += delegate
            {
                closeForm();
            };
            bg(bg1);
        }

        public void bg(BackgroundWorker bgw)
        {
            if (!bgw.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgw.RunWorkerAsync();
            }
        }

        public void closeForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Loading")
                {
                    frm.Hide();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData("");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void cmbPerPage_SelectedValueChanged(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void cmbFromBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            loadWarehouse(cmbFromBranch.Text, cmbFromWhse);
        }

        private void cmbToBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            loadWarehouse(cmbToBranch.Text, cmbToWhse);
        }


        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0;
            id = int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            TransferItems transferItems = new TransferItems("Transfer Transactions SAP IT " + gType);
            transferItems.selectedID = id;
            transferItems.Text = "Transfer Items";
            transferItems.ShowDialog();
            if (TransferItems.isSubmit)
            {
                bg(backgroundWorker1);
            }
        }

        private void btnViewItems_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            if (gridView1.RowCount > 0)
            {
                string ids = "";
                foreach (int row in selectedRows)
                {
                    int id = 0, intTemp = 0;
                    id = Int32.TryParse(gridView1.GetRowCellValue(row, "id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(row, "id").ToString()) : intTemp;
                    ids += id + ",";
                }
                ids = ids.Length > 0 ? ids.Substring(0, ids.Length - 1) : ids;
                if (ids.Length > 0)
                {
                    Transfer_forSAPDetails frm = new Transfer_forSAPDetails(gType, ids);
                    frm.ShowDialog();
                    if (Transfer_forSAPDetails.isSubmit)
                    {
                        Transfer_forSAPDetails.isSubmit = false;
                        bg(backgroundWorker1);
                    }
                }
                else
                {
                    apic.showCustomMsgBox("Validation", "No data selected");
                }
            }
            else
            {
                apic.showCustomMsgBox("Validation", "No data found");
            }
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void btnSearchQuery2_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void checkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkFromDate.Visible;
        }

        private void cmbPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
