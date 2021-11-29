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
    public partial class SummaryAvailableBalances_Details : Form
    {
        public SummaryAvailableBalances_Details(int id)
        {
            InitializeComponent();
            selectedID= id;
        }
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        int selectedID = 0;
        DataTable dtData = new DataTable();
        private void SummaryAvailableBalances_Details_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
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


        public void loadData()
        {
            string sParams = selectedID.ToString();
            string sResult = apic.loadData("/api/save/inventory/report/per_shift/details/", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                Console.WriteLine("details: " + jaData);
                dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        dtData.SetColumnsOrder("transdate", "shift_date", "shift", "item_code", "whsecode", "receipt_from_prod", "received", "transferred_in", "adjustment_in", "total_in", "out_for_transfer", "adjustment_out", "transferred_out", "issue_for_prod", "total_out", "ending_balance", "actual_count", "variance", "username", "remarks");
                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;

                            //col.Visible = !(fieldName.Equals("id"));

                            col.DisplayFormat.FormatType = fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") || fieldName.Equals("total_in") || fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") || fieldName.Equals("total_out") || fieldName.Equals("ending_balance") || fieldName.Equals("actual_count") || fieldName.Equals("variance") || fieldName.Equals("actual_count") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") || fieldName.Equals("shift_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;

                            col.DisplayFormat.FormatString = fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") || fieldName.Equals("total_in") || fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") || fieldName.Equals("total_out") || fieldName.Equals("ending_balance") || fieldName.Equals("actual_count") || fieldName.Equals("variance") ? "{0:#,0.000}" : fieldName.Equals("transdate") || fieldName.Equals("shift_date") ? "yyyy-MM-dd HH:mm:ss" : "";

                            //col.Caption = fieldName.Equals("username") ? "Transact By" : fieldName.Equals("branch") ? "Department" : col.Caption;

                            col.Visible = !(fieldName.Equals("id"));

                            col.Caption = fieldName.Equals("username") ? "Created By" : col.Caption;

                            col.AppearanceCell.BackColor = fieldName.Equals("total_in") || fieldName.Equals("total_out") || fieldName.Equals("ending_balance") || fieldName.Equals("variance") || fieldName.Equals("actual_count") ? Color.FromArgb(230, 225, 90) : fieldName.Equals("receipt_from_prod") || fieldName.Equals("received") || fieldName.Equals("transferred_in") || fieldName.Equals("adjustment_in") ? Color.FromArgb(255, 255, 128) : fieldName.Equals("out_for_transfer") || fieldName.Equals("adjustment_out") || fieldName.Equals("transferred_out") || fieldName.Equals("issue_for_prod") ? Color.FromArgb(192, 255, 192) : Color.Transparent;

                            ////fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                            //fixed
                            col.Fixed = fieldName.Equals("transdate") || fieldName.Equals("shift_date") || fieldName.Equals("shift") || fieldName.Equals("item_code") ? FixedStyle.Left : FixedStyle.None;
                        }
                        gridView1.BestFitColumns();
                        //auto complete
                        string[] suggestions = { "item_code" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                        gridView1.BestFitColumns();
                        var col2 = gridView1.Columns["remarks"];
                        if (col2 != null)
                        {
                            col2.Width = 200;
                        }
                    }));
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
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


        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
         
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //e.Cancel = true;
        }


        public double validateStringtoDouble(string val)
        {
            double doubleTemp = 0.00, result = 0.00;
            result = val == null ? doubleTemp : double.TryParse(val, out doubleTemp) ? Convert.ToDouble(val) : doubleTemp;
            return result;
        }

        public DataTable toPrint(int status)
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("item_code", typeof(string));
            dt2.Columns.Add("whsecode", typeof(string));
            dt2.Columns.Add("available", typeof(double));
            dt2.Columns.Add("branch", typeof(string));
            dt2.Columns.Add("transact_by", typeof(string));
            dt2.Columns.Add("transdate", typeof(string));
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
            dt2.Columns.Add("shift", typeof(string));
            dt2.Columns.Add("shift_date", typeof(string));
            dt2.Columns.Add("remarks", typeof(string));
            dt2.Columns.Add("actual_count", typeof(double));
            dt2.Columns.Add("variance", typeof(double));
            double doubleTemp = 0.00;
            DateTime dtTransDate = DateTime.Now;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                string itemCode = gridView1.GetRowCellValue(i, "item_code") == null ? "" : gridView1.GetRowCellValue(i, "item_code").ToString();
                string whseCode = gridView1.GetRowCellValue(i, "whsecode") == null ? "" : gridView1.GetRowCellValue(i, "whsecode").ToString();
                string plant = gridView1.GetRowCellValue(i, "plant") == null ? "" : gridView1.GetRowCellValue(i, "plant").ToString();
                string branch = gridView1.GetRowCellValue(i, "branch") == null ? "" : gridView1.GetRowCellValue(i, "branch").ToString();

                string username = gridView1.GetRowCellValue(i, "username") == null ? "" : gridView1.GetRowCellValue(i, "username").ToString();

                string shift = gridView1.GetRowCellValue(i, "shift") == null ? "" : gridView1.GetRowCellValue(i, "shift").ToString();

                string shiftDate = gridView1.GetRowCellValue(i, "shift_date") == null ? "" : gridView1.GetRowCellValue(i, "shift_date").ToString();

                string remarks = gridView1.GetRowCellValue(i, "remarks") == null ? "" : gridView1.GetRowCellValue(i, "remarks").ToString();

                double availableQty = gridView1.GetRowCellValue(i, "ending_balance") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "ending_balance").ToString());

                double beginning = gridView1.GetRowCellValue(i, "beginning") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "beginning").ToString());

                double inTransit = gridView1.GetRowCellValue(i, "in_transit") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "in_transit").ToString());

                double receiptFromProd = gridView1.GetRowCellValue(i, "receipt_from_prod") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "receipt_from_prod").ToString());

                double received = gridView1.GetRowCellValue(i, "received") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "received").ToString());

                double transferredIn = gridView1.GetRowCellValue(i, "transferred_in") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "transferred_in").ToString());

                double adjustmentIn = gridView1.GetRowCellValue(i, "adjustment_in") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "adjustment_in").ToString());

                double adjustmentOut = gridView1.GetRowCellValue(i, "adjustment_out") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "adjustment_out").ToString());

                double outForTransfer = gridView1.GetRowCellValue(i, "out_for_transfer") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "out_for_transfer").ToString());

                double transferredOut = gridView1.GetRowCellValue(i, "transferred_out") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "transferred_out").ToString());

                double issueForProd = gridView1.GetRowCellValue(i, "issue_for_prod") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "issue_for_prod").ToString());

                double totalOut = gridView1.GetRowCellValue(i, "total_out") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "total_out").ToString());

                double totalIn = gridView1.GetRowCellValue(i, "total_in") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "total_in").ToString());

                double variance = gridView1.GetRowCellValue(i, "variance") == null ? 0.00 : validateStringtoDouble(gridView1.GetRowCellValue(i, "variance").ToString());


                if ((gridView1.GetRowCellValue(i, "actual_count").ToString()) == "")
                {
                    dt2.Rows.Add(itemCode, whseCode, availableQty, branch, username, dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), plant, beginning, inTransit, receiptFromProd, received, transferredIn, adjustmentIn, adjustmentOut, outForTransfer, transferredOut, issueForProd, totalOut, totalIn, shift, shiftDate, remarks, null, null);
                    Console.WriteLine("not here?");
                }else
                {
                    double actualCount = validateStringtoDouble(gridView1.GetRowCellValue(i, "actual_count").ToString());

                    dt2.Rows.Add(itemCode, whseCode, availableQty, branch, username, dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), plant, beginning, inTransit, receiptFromProd, received, transferredIn, adjustmentIn, adjustmentOut, outForTransfer, transferredOut, issueForProd, totalOut, totalIn, shift, shiftDate, remarks, actualCount, variance);
                    Console.WriteLine("hays");
                }


            }
            return dt2;
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            string currentUser = Login.jsonResult["data"]["username"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["username"].ToString();
            crInventoryPerWhse frm = new crInventoryPerWhse(toPrint(0), currentUser);
            frm.ShowDialog();
        }
    }
}
