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
using RestSharp;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class InventoryPerWhse : Form
    {
        public InventoryPerWhse()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtPlant = new DataTable(), dtBranches = new DataTable(), dtWarehouses = new DataTable();
        private void InventoryPerWhse_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            loadPlant();
            loadItemGroup();
            bg(backgroundWorker1);
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

        public void loadWarehouse(string branch)
        {
            try
            {
                cmbWarehouse.Invoke(new Action(delegate ()
                {
                    cmbWarehouse.Properties.Items.Clear();
                }));
                string sBranchCode = apic.findValueInDataTable(dtBranches, branch, "name", "code");
                string sResult = "";
                sResult = apic.loadData("/api/whse/get_all", "?branch=" + branch, "", "", Method.GET, true);
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

        public void loadItemGroup()
        {
            try
            {
                cmbItemGroup.Invoke(new Action(delegate ()
                {
                    cmbItemGroup.Properties.Items.Clear();
                }));
                string sResult = apic.loadData("/api/item/item_grp/getall", "", "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResponse = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResponse["data"];
                    DataTable dtItemGroup = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                    if (dtItemGroup.Rows.Count > 0)
                    {
                        cmbItemGroup.Invoke(new Action(delegate ()
                        {
                            cmbItemGroup.Properties.Items.Add("All");
                        }));
                    }
                    foreach (DataRow row in dtItemGroup.Rows)
                    {
                        cmbItemGroup.Invoke(new Action(delegate ()
                        {
                            cmbItemGroup.Properties.Items.Add(row["code"].ToString());
                        }));
                    }
                    cmbItemGroup.Invoke(new Action(delegate ()
                    {
                        cmbItemGroup.SelectedIndex = 0;
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string delegateControl(Control c, string s)
        {
            string result = "";
            c.Invoke(new Action(delegate ()
            {
                result = s;
            }));
            return result;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sBranch = delegateControl(cmbBranch, cmbBranch.Text), sWhse = delegateControl(cmbWarehouse, cmbWarehouse.Text), sFromDate = delegateControl(dtFromDate, dtFromDate.Text), sToDate = delegateControl(dtToDate, dtToDate.Text), sItemGroup = delegateControl(cmbItemGroup, cmbItemGroup.Text), sFromTime = delegateControl(cmbFromTime, cmbFromTime.Text), sToTime = delegateControl(cmbToTime, cmbToTime.Text), sPlant = delegateControl(cmbPlant, cmbPlant.Text);
                string branchCode = apic.findValueInDataTable(dtBranches, sBranch, "name", "code");
                string whseCode = apic.findValueInDataTable(dtWarehouses, sWhse, "whsename", "whsecode");
                sItemGroup = sItemGroup.Equals("All") ? "" : sItemGroup;
                string plant = apic.findValueInDataTable(dtPlant, sPlant, "name", "code");

                loadData(branchCode, whseCode, sItemGroup, sFromDate, sToDate, sFromTime, sToTime, plant);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadData(string branchCode, string whseCode, string itemGroup, string fromDate, string toDate, string fromTime, string toTime, string plant)
        {
            string sURL = "/api/inv/item/per_bin/get_all?branch=" + branchCode + "&from_date=" + fromDate + "&to_date=" + toDate + "&whse=" + whseCode + "&item_group=" + itemGroup + "&from_time=" + fromTime + "&to_time=" + toTime + "&plant=" + plant;
            string sResult = apic.loadData(sURL, "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                Console.WriteLine(jaData);
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        dtData.SetColumnsOrder("item_code","whsecode", "beginning", "in_transit", "receipt_from_prod", "received", "transferred_in", "adjustment_in", "total_in", "out_for_transfer", "adjustment_out", "transferred_out", "issue_for_prod", "total_out", "available");

                        dtData = dtData.SortAlphaNumeric("whsecode");

                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                        //auto complete
                        string[] suggestions = { "item_code", };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                        foreach (GridColumn col in gridView1.Columns)
                        {

                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            //col.Caption = fieldName.Equals("adjustment_in") ? "Adjustment In" : fieldName.Equals("Adj Out") ? "Adjustment Out" : v.Equals("Transferred") ? "Transferred Out" : fieldName.Equals("In Transit") ? v + " (For Received)" : v.Equals("Out Transit") ? "Out For Transfer" : fieldName.Equals("IssueForProd") ? "Issue For Prod/Packing" : col.GetCaption();
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.Visible = !(fieldName.Equals("plant") || fieldName.Equals("branch"));
                            //col.Width = 150;

                            col.DisplayFormat.FormatType = fieldName.Equals("item_code") ? DevExpress.Utils.FormatType.None : DevExpress.Utils.FormatType.Numeric;
                            col.DisplayFormat.FormatString = fieldName.Equals("item_code") ? "" : "{0:#,0.000}";
                            //col.Visible = v.Equals("Sold") || v.Equals("Pull Out") ? false : true;

                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                            //fixed column
                            col.Fixed = fieldName.Equals("item_code") || fieldName.Equals("whsecode") ? FixedStyle.Left : FixedStyle.None;

                            col.AppearanceCell.BackColor = fieldName.Equals("beginning") || fieldName.Equals("total_in") || fieldName.Equals("total_out") || fieldName.Equals("available") ? Color.FromArgb(230, 225, 90) : fieldName.Equals("in_transit") || fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") ? Color.FromArgb(255, 255, 128) : fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") ? Color.FromArgb(192, 255, 192) : Color.Transparent;
                            
                            //gridView1.Columns.ColumnByFieldName("total_in").VisibleIndex = gridView1.Columns.ColumnByFieldName("total_in").VisibleIndex + 1;
                            //gridView1.Columns.ColumnByFieldName("out_for_transfer").VisibleIndex = gridView1.Columns.ColumnByFieldName("out_for_transfer").VisibleIndex - 1;
                            //gridView1.Columns.ColumnByFieldName("issue_for_prod").VisibleIndex = gridView1.Columns.ColumnByFieldName("issue_for_prod").VisibleIndex + 1;
                        }

                        if (gridView1.Columns["item_code"] != null)
                        {
                            gridView1.Columns["item_code"].Summary.Clear();
                            gridView1.Columns["item_code"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "item_code", "Total Item: {0:N0}");

                        }
                        if (gridView1.Columns["available"] != null)
                        {
                            gridView1.Columns["available"].Summary.Clear();
                            gridView1.Columns["available"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "available", "Total Available: {0:#,0.000}");

                        }

                        gridView1.BestFitColumns();
                    }));
                }
            }
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

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches();
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
       
        }

        private void backgroundWorkerCmbWarehouse_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                cmbBranch.Invoke(new Action(delegate ()
                {
                    string branchCode = apic.findValueInDataTable(dtBranches, cmbBranch.Text, "name", "code");
                    loadWarehouse(branchCode);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void backgroundWorkerCmbWarehouse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bg(backgroundWorkerCmbWarehouse);
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void btnSelectMultipleItem_Click(object sender, EventArgs e)
        {
            if (gridView1.Columns["item_code"] != null)
            {
                gridView1.ShowFilterPopup(gridView1.Columns["item_code"]);
            }
        }

        public DataTable toPrint(int status)
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("item_code", typeof(string));
            dt2.Columns.Add("whsecode", typeof(string));
            dt2.Columns.Add("available", typeof(double));
            dt2.Columns.Add("branch", typeof(string));
            if (status == 1)
            {
                dt2.Columns.Add("transact_by", typeof(string));
                dt2.Columns.Add("transdate", typeof(string));
            }
            dt2.Columns.Add("plant", typeof(string));
            dt2.Columns.Add("beginning", typeof(double));
            dt2.Columns.Add("in_transit", typeof(double));
            dt2.Columns.Add("receipt_from_prod", typeof(double));
            dt2.Columns.Add("received", typeof(double));
            dt2.Columns.Add("transferred_in", typeof(double));
            dt2.Columns.Add("adjustment_in", typeof(double));
            dt2.Columns.Add("adjustment_out", typeof(double));
            dt2.Columns.Add("out_for_transfer", typeof(double));
            dt2.Columns.Add("transferred_out", typeof(double));
            dt2.Columns.Add("issue_for_prod", typeof(double));
            dt2.Columns.Add("total_out", typeof(double));
            dt2.Columns.Add("total_in", typeof(double));
            dt2.Columns.Add("actual_count", typeof(double));
            dt2.Columns.Add("variance", typeof(double));
            dt2.Columns.Add("comments", typeof(string));
            dt2.Columns.Add("row_index", typeof(int));
            double doubleTemp = 0.00;
            string currentUser = Login.jsonResult["data"]["username"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["username"].ToString();
            DateTime dtTransDate = DateTime.Now;
            int rowIndex = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                string itemCode = gridView1.GetRowCellValue(i, "item_code").ToString();
                string whseCode = gridView1.GetRowCellValue(i, "whsecode").ToString();
                string plant = gridView1.GetRowCellValue(i, "plant").ToString();
                string branch = gridView1.GetRowCellValue(i, "branch").ToString();
                double availableQty = validateStringtoDouble(gridView1.GetRowCellValue(i, "available").ToString());

                double beginning = validateStringtoDouble(gridView1.GetRowCellValue(i, "beginning").ToString());

                double inTransit = validateStringtoDouble(gridView1.GetRowCellValue(i, "in_transit").ToString());

                double receiptFromProd = validateStringtoDouble(gridView1.GetRowCellValue(i, "receipt_from_prod").ToString());

                double received = validateStringtoDouble(gridView1.GetRowCellValue(i, "received").ToString());

                double transferredIn = validateStringtoDouble(gridView1.GetRowCellValue(i, "transferred_in").ToString());

                double adjustmentIn = validateStringtoDouble(gridView1.GetRowCellValue(i, "adjustment_in").ToString());

                double adjustmentOut = validateStringtoDouble(gridView1.GetRowCellValue(i, "adjustment_out").ToString());

                double outForTransfer = validateStringtoDouble(gridView1.GetRowCellValue(i, "out_for_transfer").ToString());

                double transferredOut = validateStringtoDouble(gridView1.GetRowCellValue(i, "transferred_out").ToString());

                double issueForProd = validateStringtoDouble(gridView1.GetRowCellValue(i, "issue_for_prod").ToString());

                double totalOut = validateStringtoDouble(gridView1.GetRowCellValue(i, "total_out").ToString());

                double totalIn = validateStringtoDouble(gridView1.GetRowCellValue(i, "total_in").ToString());

                if (status == 1)
                {
                    //double variance = 0.00 - availableQty;

                    dt2.Rows.Add(itemCode, whseCode, availableQty, branch, currentUser, dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), plant, beginning, inTransit, receiptFromProd, received, transferredIn, adjustmentIn, adjustmentOut, outForTransfer, transferredOut, issueForProd, totalOut, totalIn,null,null,null,rowIndex);
                }else
                {
                    //double variance = 0.00 - availableQty;
                    dt2.Rows.Add(itemCode, whseCode, availableQty, branch, plant, beginning, inTransit, receiptFromProd, received, transferredIn, adjustmentIn, adjustmentOut, outForTransfer, transferredOut, issueForProd, totalOut, totalIn,null,null,null,rowIndex);
                }
                rowIndex++;
            }
            return dt2;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("whsecode", typeof(string));
            //dt.Columns.Add("item_code", typeof(string));
            //dt.Columns.Add("available", typeof(double));
            //dt.Columns.Add("actual_count", typeof(double));
            //dt.Columns.Add("variance", typeof(double));
            //double doubleTemp = 0.00;
            //for (int i = 0; i < gridView1.DataRowCount; i++)
            //{ 
            //    string itemCode = gridView1.GetRowCellValue(i, "item_code").ToString();
            //    string whseCode = gridView1.GetRowCellValue(i, "whsecode").ToString();
            //    double availableQty = gridView1.GetRowCellValue(i, "available") == null ? doubleTemp : double.TryParse(gridView1.GetRowCellValue(i, "available").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetRowCellValue(i, "available").ToString()) : doubleTemp;

            //    double variance = 0.00 - availableQty;

            //    dt.Rows.Add(whseCode, itemCode, availableQty,0.00, variance);
            //}
            RemarksShiftDate frm = new RemarksShiftDate(toPrint(1), toPrint(0));
            frm.ShowDialog();
        }

        public double validateStringtoDouble(string val)
        {
            double doubleTemp = 0.00, result = 0.00;
            result = val == null ? doubleTemp : double.TryParse(val, out doubleTemp) ? Convert.ToDouble(val) : doubleTemp;
            return result;
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName.Equals("age"))
            {
                double age = 0.00, doubleTemp = 0.00;
                age = double.TryParse(e.CellValue.ToString(), out doubleTemp) ? Convert.ToDouble(e.CellValue.ToString()) : doubleTemp;
                if (age >= 3)
                {
                    e.Appearance.BackColor = Color.Orange;
                }
            }
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
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

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedColumnText = gridView1.FocusedColumn.FieldName;
                if (selectedColumnText.Equals("available"))
                {
                    string sBranch = "?branch=" + gridView1.GetFocusedRowCellValue("branch").ToString();
                    string sWhse = "&whsecode=" + gridView1.GetFocusedRowCellValue("whsecode").ToString();
                    string sFromDate = "&rom_date=" + dtFromDate.Text;
                    string sToDate = "&to_date=" + dtFromDate.Text;
                    string sItemCode = "&item_code=" + gridView1.GetFocusedRowCellValue("item_code").ToString();
                    string sPlant = "&plant=" + gridView1.GetFocusedRowCellValue("plant").ToString();
                    string sFromTime = "&from_time=" + cmbFromTime.Text;
                    string sToTime = "&to_time=" + cmbToTime.Text;

                    string sParams = sBranch + sWhse + sFromDate + sToDate + sItemCode + sPlant + sFromTime + sToTime;

                    string sUrl = "/api/inv/aging/report";

                    string sResult = apic.loadData(sUrl, sParams, "", "", Method.GET, true);
                    JObject joResult = JObject.Parse(sResult);

                    DataTable dtInvDetails = (DataTable)JsonConvert.DeserializeObject((string)joResult["data"].ToString(), (typeof(DataTable)));

                    InventoryDetails_Available frm = new InventoryDetails_Available(dtInvDetails, 13);
                    frm.Text = "Available";
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
    }
}
