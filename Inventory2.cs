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
using RestSharp;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using System.Web;
using System.Threading;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Inventory2 : Form
    {
        public Inventory2()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        utility_class utilityc = new utility_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtBranches = new DataTable(), dtWarehouses = new DataTable(), dtPlant = new DataTable();
        AutoResetEvent doneEvent = new AutoResetEvent(false);
        private void btnSelectMultipleItem_Click(object sender, EventArgs e)
        {
            if (gridView1.Columns["item_code"] != null)
            {
                gridView1.ShowFilterPopup(gridView1.Columns["item_code"]);
            }
        }

        private void Inventory2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            loadPlant();
            loadItemGroup();
            bg(backgroundWorkerLoadData);
        }


        public  void loadData(string branchCode, string whseCode, string itemGroup, string fromDate, string toDate, string fromTime, string toTime,string plant)
        {
            string sURL = "/api/inv/warehouse/report?branch=" + branchCode + "&from_date=" + fromDate + "&to_date=" + toDate + "&whse=" + whseCode + "&item_group=" + itemGroup + "&from_time=" + fromTime + "&to_time=" + toTime + "&plant=" + plant;
            string sResult =  apic.loadData(sURL, "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        dtData.SetColumnsOrder("item_code", "beginning", "in_transit", "receipt_from_prod", "received", "transferred_in", "adjustment_in", "total_in", "out_for_transfer", "adjustment_out", "transferred_out", "issue_for_prod", "total_out", "available");
                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight= DevExpress.Utils.DefaultBoolean.True;

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
                            col.Caption = fieldName.Equals("in_transit") ? "In Transit (For Received)" : fieldName.Equals("issue_for_prod") ? "Issue For Prod/Packing" : col.GetCaption();
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
                string[] lists = { "isAdmin", "isSuperAdmin","isManager" };
                if (apic.haveAccess(lists))
                {
                    string sResult = "";
                    sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", Method.GET, true);
                    if (sResult.Substring(0, 1).Equals("{"))
                    {
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
                                cmbBranch.SelectedIndex = cmbBranch.Properties.Items.IndexOf(s) <=0 ? 0 : cmbBranch.Properties.Items.IndexOf(s);
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
            catch(Exception ex)
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
                    dtWarehouses =  apic.getDtDownloadResources(sResult, "data");
                    dtWarehouses = dtWarehouses.SortAlphaNumeric("whsename");
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

        private void backgroundWorkerCmbBranch_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                doneEvent.Set();
            }
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

        private void backgroundWorkerCmbItemGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //loadItemGroup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                doneEvent.Set();
            }
        }

        private void backgroundWorkerCmbItemGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorkerLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        public string delegateControl(Control c,string s)
        {
            string result = "";
            c.Invoke(new Action(delegate ()
            {
                result = s;
            }));
            return result;
        }

        private void backgroundWorkerLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sBranch = delegateControl(cmbBranch, cmbBranch.Text), sWhse = delegateControl(cmbWarehouse, cmbWarehouse.Text), sFromDate = delegateControl(dtFromDate, dtFromDate.Text), sToDate = delegateControl(dtToDate, dtToDate.Text), sItemGroup = delegateControl(cmbItemGroup, cmbItemGroup.Text), sFromTime = delegateControl(cmbFromTime, cmbFromTime.Text), sToTime = delegateControl(cmbToTime, cmbToTime.Text), sPlant = delegateControl(cmbPlant, cmbPlant.Text);
                string branchCode = apic.findValueInDataTable(dtBranches, sBranch, "name", "code");
                string whseCode = apic.findValueInDataTable(dtWarehouses, sWhse, "whsename", "whsecode");
                sItemGroup = sItemGroup.Equals("All") ? "" : sItemGroup;
                string plant = apic.findValueInDataTable(dtPlant, sPlant, "name", "code");

                loadData(branchCode, whseCode, sItemGroup, sFromDate, sToDate, sFromTime, sToTime,plant);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                doneEvent.Set();
            }
        }



        private void btnrefresh_Click(object sender, EventArgs e)
        {
            bg(backgroundWorkerLoadData);
        }

        public List<string> listCodes()
        {
            string[] input = {
            "in_transit",
                "receive_from_prod",
                "receive",
                "transfer_in",
                "adjustment_in",
                "out_transit",
                "transfer_out",
                "adjustment_out",
                "issue_for_prod",
                "pullout",
                "sales",
                "available"
            };

            List<string> result = new List<string>(input);
            return result;
        }

        public List<string> listSelectedColumns()
        {
            string[] input = {
                "In Transit (For Received)",
                "Receipt From Prod",
                "Received",
                "Transferred In",
                "Adjustment In",
                "Out For Transfer",
                "Transferred Out",
                "Adjustment Out",
                "Issue For Prod/Packing",
                "Pull Out",
                "Sold",
                "Available"

            };

            List<string> result = new List<string>(input);
            return result;
        }

        public DataTable listDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("caption", typeof(string));
            try
            {
                for (int i = 0; i < listCodes().Count; i++)
                {
                    dt.Rows.Add(listCodes()[i], listSelectedColumns()[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dt;
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.GetCaption();
            string code = gridView1.GetFocusedRowCellValue("item_code").ToString();
            foreach (DataRow row in listDetails().Rows)
            {
                if (selectedColumnText == row["caption"].ToString())
                {
                    loadDetails(row["code"].ToString(), code,selectedColumnText);
                    break;
                }
            }
        }

        public void loadDetails(string objType,string itemCode, string columnCaption)
        {
            string sBranch = apic.findValueInDataTable(dtBranches, cmbBranch.Text, "name", "code"),
                sWhse = apic.findValueInDataTable(dtWarehouses, cmbWarehouse.Text, "whsename", "whsecode"),
                sPlant = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            string sParams = "?branch=" + sBranch + "&from_date=" + dtFromDate.Text + "&to_date=" + dtToDate.Text + "&whsecode=" + sWhse + (columnCaption.Equals("Available") ? "" : "&transaction=" + objType) + "&item_code=" + (itemCode.Contains("+") ? HttpUtility.UrlEncode(itemCode) : itemCode + "&from_time=" + cmbFromTime.Text + "&to_time=" + cmbToTime.Text) + "&plant=" + sPlant;

            string sUrl = columnCaption.Equals("Available") ? "/api/inv/aging/report" : "/api/inv/whse/detailed/transaction";
            string sResult = apic.loadData(sUrl, sParams, "", "", Method.GET, true);
            JObject joResult = JObject.Parse(sResult);

            DataTable dtInvDetails = (DataTable)JsonConvert.DeserializeObject((string)joResult["data"].ToString(), (typeof(DataTable)));

            //"Receive From Prod",
            //    "Receive",
            //    "Transfer In",
            //    "Adj In",

            int id = columnCaption.Equals("In Transit (For Received)") || columnCaption.Equals("Out For Transfer") ? 2 : columnCaption.Equals("Receive From Prod") || columnCaption.Equals("Received")  || columnCaption.Equals("Adjustment In") || columnCaption.Equals("Transferred In") ? 5 : 13;
            if (columnCaption.Equals("Available"))
            {
                InventoryDetails_Available frm = new InventoryDetails_Available(dtInvDetails, id);
                frm.Text = columnCaption;
                frm.ShowDialog();
            }
            //else if (columnCaption.Equals("Received") || columnCaption.Equals("Transfer In") || columnCaption.Equals("In Transit (For Received)") || columnCaption.Equals("Out Transit (Out For Delivery)") || columnCaption.Equals("Transfer Out"))
            //{
            //    Console.WriteLine((string)joResult["data"].ToString());
            //    InventoryDetails_Receive frm = new InventoryDetails_Receive(dtInvDetails);
            //    frm.Text = columnCaption;
            //    frm.ShowDialog();
            //}
            else
            {

                InventoryDetails frm = new InventoryDetails(dtInvDetails, id);
                frm.Text = columnCaption;
                frm.ShowDialog();
            }

        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorkerLoadData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg(backgroundWorkerLoadData);
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
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

        private void Inventory2_ResizeEnd(object sender, EventArgs e)
        {
         
        }

        private void cmbPlant_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            loadBranches();
        }


        private void backgroundWorkerCmbBranch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
    }
}
