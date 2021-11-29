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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using RestSharp;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class SummaryAvailableBalances : Form
    {
        public SummaryAvailableBalances()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtWarehouses = new DataTable(), dtBranches = new DataTable(), dtPlant = new DataTable(), dtShift = new DataTable();
        private void SummaryAvailableBalances_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtShiftDate.EditValue = DateTime.Now;
            loadShift();
            loadPlant();
            bg();
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;
            }
        }

        public void closeForm()
        {
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        public string delegateControl(Control c)
        {
            string result = "";
            gridControl1.Invoke(new Action(delegate ()
            {
                result = c.Text;
            }));
            return result;
        }

        public double validateStringtoDouble(string val)
        {
            double doubleTemp = 0.00, result = 0.00;
            result = val == null ? doubleTemp : double.TryParse(val, out doubleTemp) ? Convert.ToDouble(val) : doubleTemp;
            return result;
        }

        //public DataTable toPrint()
        //{
        //    DataTable dt2 = new DataTable();
        //    dt2.Columns.Add("item_code", typeof(string));
        //    dt2.Columns.Add("whsecode", typeof(string));
        //    dt2.Columns.Add("available", typeof(double));
        //    dt2.Columns.Add("branch", typeof(string));
        //    dt2.Columns.Add("transact_by", typeof(string));
        //    dt2.Columns.Add("transdate", typeof(string));
        //    dt2.Columns.Add("plant", typeof(string));
        //    dt2.Columns.Add("beginning", typeof(double));
        //    dt2.Columns.Add("in_transit", typeof(double));
        //    dt2.Columns.Add("receipt_from_prod", typeof(double));
        //    dt2.Columns.Add("received", typeof(double));
        //    dt2.Columns.Add("transferred_in", typeof(double));
        //    dt2.Columns.Add("adjustment_in", typeof(double));
        //    dt2.Columns.Add("adjustment_out", typeof(double));
        //    dt2.Columns.Add("out_for_transfer", typeof(double));
        //    dt2.Columns.Add("transferred_out", typeof(double));
        //    dt2.Columns.Add("issue_for_prod", typeof(double));
        //    dt2.Columns.Add("total_out", typeof(double));
        //    dt2.Columns.Add("total_in", typeof(double));
        //    dt2.Columns.Add("shift_date");
        //    dt2.Columns.Add("remarks");
        //    dt2.Columns.Add("shift");
        //    double doubleTemp = 0.00;
        //    DateTime dtTransDate = DateTime.Now;
        //    for (int i = 0; i < gridView1.DataRowCount; i++)
        //    {
        //        string itemCode = gridView1.GetRowCellValue(i, "item_code").ToString();
        //        string whseCode = gridView1.GetRowCellValue(i, "whsecode").ToString();
        //        string plant = "";
        //        string branch = gridView1.GetRowCellValue(i, "branch").ToString();
        //        double availableQty = validateStringtoDouble(gridView1.GetRowCellValue(i, "ending_balance").ToString());

        //        double beginning = 0.00;

        //        double inTransit = 0.00;

        //        double receiptFromProd = validateStringtoDouble(gridView1.GetRowCellValue(i, "receipt_from_prod").ToString());

        //        double received = validateStringtoDouble(gridView1.GetRowCellValue(i, "received").ToString());

        //        double transferredIn = validateStringtoDouble(gridView1.GetRowCellValue(i, "transferred_in").ToString());

        //        double adjustmentIn = validateStringtoDouble(gridView1.GetRowCellValue(i, "adjustment_in").ToString());

        //        double adjustmentOut = validateStringtoDouble(gridView1.GetRowCellValue(i, "adjustment_out").ToString());

        //        double outForTransfer = validateStringtoDouble(gridView1.GetRowCellValue(i, "out_for_transfer").ToString());

        //        double transferredOut = validateStringtoDouble(gridView1.GetRowCellValue(i, "transferred_out").ToString());

        //        double issueForProd = validateStringtoDouble(gridView1.GetRowCellValue(i, "issue_for_prod").ToString());

        //        double totalOut = validateStringtoDouble(gridView1.GetRowCellValue(i, "total_out").ToString());
                    
        //        double totalIn = validateStringtoDouble(gridView1.GetRowCellValue(i, "total_in").ToString());

        //        string shiftDate = gridView1.GetRowCellValue(i, "shift_date").ToString();

        //        string shift = gridView1.GetRowCellValue(i, "shift").ToString();

        //        string remarks = "";

        //        string username = gridView1.GetRowCellValue(i, "username").ToString();

        //        dt2.Rows.Add(itemCode, whseCode, availableQty, branch, username, dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), plant, beginning, inTransit, receiptFromProd, received, transferredIn, adjustmentIn, adjustmentOut, outForTransfer, transferredOut, issueForProd, totalOut, totalIn, shiftDate, remarks,shift);
        //    }
        //    return dt2;
        //}

        public void loadData()
        {
            string sShift = "?shift=" + apic.findValueInDataTable(dtShift, delegateControl(cmbShift), "description", "code");
            string sShiftDate = "&shift_date=" + delegateControl(dtShiftDate);
            string sPlant = "&plant=" +  apic.findValueInDataTable(dtPlant, delegateControl(cmbPlant), "name", "code");
            string sDept = "&department=" + apic.findValueInDataTable(dtBranches, delegateControl(cmbBranch), "name", "code");
            string sWhse = "&whsecode=" + apic.findValueInDataTable(dtWarehouses, delegateControl(cmbWarehouse), "whsename", "whsecode");
            string sParams = sShift + sShiftDate + sPlant + sDept + sWhse;
            string sResult = apic.loadData("/api/save/inventory/report/per_shift/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));

                if(dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("view_details");
                }

                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : fieldName.Equals("view_details") ? repositoryItemButtonEdit1  : repositoryItemTextEdit1;


                            col.DisplayFormat.FormatType = fieldName.Equals("transdate") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;

                            col.DisplayFormat.FormatString = fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "";

                            col.Visible = !(fieldName.Equals("id"));

                            col.Caption = fieldName.Equals("username") ? "Created By" : col.Caption;

                            //col.DisplayFormat.FormatType = fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") || fieldName.Equals("total_in") || fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") || fieldName.Equals("total_out") || fieldName.Equals("ending_balance") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") || fieldName.Equals("shift_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            //col.DisplayFormat.FormatString = fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") || fieldName.Equals("total_in") || fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") || fieldName.Equals("total_out") || fieldName.Equals("ending_balance") ? "{0:#,0.000}" : fieldName.Equals("transdate") || fieldName.Equals("shift_date") ? "yyyy-MM-dd HH:mm:ss" : "";

                            //col.Caption = fieldName.Equals("username") ? "Transact By" : fieldName.Equals("branch") ? "Department" : col.Caption;

                            //col.Visible = !(fieldName.Equals("id"));

                            //col.AppearanceCell.BackColor = fieldName.Equals("total_in") || fieldName.Equals("total_out") || fieldName.Equals("ending_balance") ? Color.FromArgb(230, 225, 90) : fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") ? Color.FromArgb(255, 255, 128) : fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") ? Color.FromArgb(192, 255, 192) : Color.Transparent;

                            ////fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                            ////fixed
                            //col.Fixed = fieldName.Equals("transdate") || fieldName.Equals("shift_date") || fieldName.Equals("shift") || fieldName.Equals("branch") || fieldName.Equals("whsecode") || fieldName.Equals("item_code") ? FixedStyle.Left : FixedStyle.None;
                        }
                        gridView1.BestFitColumns();
                        //auto complete
                        string[] suggestions = { "shift" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                        gridView1.BestFitColumns();
                        var col2 = gridView1.Columns["remarks"];
                        if(col2 != null)
                        {
                            col2.Width = 200;
                        }
                    }));
                }
            }
        }

        public void loadPlant()
        {
            cmbPlant.Properties.Items.Clear();
            cmbPlant.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/plant/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtPlant.Rows)
                    {
                        cmbPlant.Properties.Items.Add(row["name"].ToString());
                    }
                    string plantCode = Login.jsonResult["data"]["plant"].ToString();
                    string plantName = apic.findValueInDataTable(dtPlant, plantCode, "code", "name");
                    cmbPlant.SelectedIndex = cmbPlant.Properties.Items.IndexOf(plantName) <= 0 ? 0 : cmbPlant.Properties.Items.IndexOf(plantName);
                }
            }
            else
            {
                cmbPlant.SelectedIndex = 0;
            }
        }

        public void loadShift()
        {
            cmbShift.Properties.Items.Clear();
            cmbShift.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/production/shift/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtShift = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    foreach (DataRow row in dtShift.Rows)
                    {
                        cmbShift.Properties.Items.Add(row["description"].ToString());
                    }
                    cmbShift.SelectedIndex = cmbShift.Properties.Items.IndexOf(Login.selectedShift) <= 0 ? 0 : cmbShift.Properties.Items.IndexOf(Login.selectedShift);
                }
            }
            else
            {
                cmbShift.SelectedIndex = 0;
            }
        }

        public void loadBranches()
        {
            try
            {
                string plantCode = "";
                cmbPlant.Invoke(new Action(delegate ()
                {
                    plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                }));
                cmbBranch.Invoke(new Action(delegate ()
                {
                    cmbBranch.Properties.Items.Clear();
                }));
                string[] lists = { "isAdmin", "isSuperAdmin", "isManager" };
                if (apic.haveAccess(lists))
                {
                    string sResult = "";
                    sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", Method.GET, true);
                    if (sResult.Substring(0, 1).Equals("{"))
                    {
                        //DataTable dtData = apic.getDtDownloadResources(sResult, "data");
                        //string sBranch = apic.getFirstRowDownloadResources(dtData, "data");

                        dtBranches = apic.getDtDownloadResources(sResult, "data");
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                cmbBranch.Properties.Items.Add("All");
                            }));
                        }
                        foreach (DataRow row in dtBranches.Rows)
                        {
                            if (IsHandleCreated)
                            {
                                cmbBranch.Invoke(new Action(delegate ()
                                {
                                    cmbBranch.Properties.Items.Add(row["name"].ToString());
                                }));
                            }

                        }
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                string branch = (string)Login.jsonResult["data"]["branch"];
                                string s = apic.findValueInDataTable(dtBranches, branch, "code", "name");
                                cmbBranch.SelectedIndex = cmbBranch.Properties.Items.IndexOf(s) <= 0 ? 0 : cmbBranch.Properties.Items.IndexOf(s);

                            }));
                        }
                    }
                    else
                    {
                        apic.showCustomMsgBox("Validation", sResult);
                    }
                }
                else
                {
                    if (IsHandleCreated)
                    {
                        cmbBranch.Invoke(new Action(delegate ()
                        {
                            cmbBranch.Properties.Items.Add(Login.jsonResult["data"]["branch"]);
                            cmbBranch.SelectedIndex = 0;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadWarehouse(string branch, string plant)
        {
            try
            {
                cmbWarehouse.Invoke(new Action(delegate ()
                {
                    cmbWarehouse.Properties.Items.Clear();
                }));
                string sBranchCode = apic.findValueInDataTable(dtBranches, branch, "name", "code");
                string sPlantCode = apic.findValueInDataTable(dtPlant, plant, "name", "code");

                string sResult = "";
                sResult = apic.loadData("/api/whse/get_all", "?branch=" + sBranchCode + "&plant=" + sPlantCode , "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    dtWarehouses = apic.getDtDownloadResources(sResult, "data");
                }
                if (dtWarehouses.Rows.Count > 1)
                {
                    cmbWarehouse.Invoke(new Action(delegate ()
                    {
                        cmbWarehouse.Properties.Items.Add("All");
                    }));
                }
                foreach (DataRow row in dtWarehouses.Rows)
                {
                    cmbWarehouse.Invoke(new Action(delegate ()
                    {
                        cmbWarehouse.Properties.Items.Add(row["whsename"].ToString());
                    }));
                }
                cmbWarehouse.Invoke(new Action(delegate ()
                {
                    string whse = (string)Login.jsonResult["data"]["whse"];
                    string s = apic.findValueInDataTable(dtWarehouses, whse, "whsecode", "whsename");
                    cmbWarehouse.SelectedIndex = cmbBranch.Text.Equals("All") || string.IsNullOrEmpty(s.Trim()) ? 0 : cmbWarehouse.Properties.Items.IndexOf(s);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
        private int hotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        private int HotTrackRow
        {
            get
            {
                return hotTrackRow;
            }
            set
            {
                if (hotTrackRow != value)
                {
                    int prevHotTrackRow = hotTrackRow;
                    hotTrackRow = value;
                    gridView1.RefreshRow(prevHotTrackRow);
                    gridView1.RefreshRow(hotTrackRow);

                    if (hotTrackRow >= 0)
                        gridControl1.Cursor = Cursors.Hand;
                    else
                        gridControl1.Cursor = Cursors.Default;
                }
            }
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //string currentUser = Login.jsonResult["data"]["username"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["username"].ToString();
            //crInventoryPerWhse frm = new crInventoryPerWhse(toPrint(), currentUser);
            //frm.ShowDialog();
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
           
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0, transferID = 0;
            id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            if (selectedColumnText.Equals("view_details"))
            {
                SummaryAvailableBalances_Details frm = new SummaryAvailableBalances_Details(id);
                frm.ShowDialog();
            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadWarehouse(cmbBranch.Text, cmbPlant.Text);
        }

        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }
    }
}
