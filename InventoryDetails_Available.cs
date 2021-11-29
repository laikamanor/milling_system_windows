using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.UI_Class;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class InventoryDetails_Available : Form
    {
        public InventoryDetails_Available(DataTable dt, int id)
        {
            gDt = dt;
            selectedID = id;
            InitializeComponent();
        }
        DataTable gDt = new DataTable();
        devexpress_class devc = new devexpress_class();
        int selectedID = 0;
        private void InventoryDetails_Available_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public void loadData(DataTable dt, bool isOpen)
        {
            
            try
            {
                gridControl1.Invoke(new Action(delegate ()
                {
                    string[] list = { "whsecode", "item_code", "total_available", "transdate", "stock_date", "quantity", "available_qty","stock_age_in_hour","receive_age_in_hour", "stock_age", "stock_age" };
                    gDt.SetColumnsOrder(list);
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    dt.SetColumnsOrder(list);
                    gridControl1.DataSource = dt;

                    //auto complete
                    string[] suggestions = { "whsecode" };
                    string suggestConcat = string.Join(";", suggestions);
                    gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(gridView1, gridControl1, suggestions);

                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
                        col.DisplayFormat.FormatType = fieldName.Equals("transdate") || fieldName.Equals("stock_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = fieldName.Equals("transdate") || fieldName.Equals("stock_date") ? "yyyy-MM-dd HH:mm:ss" : "{0:#,0.000}";
                        col.Caption = fieldName.Equals("stock_age") ? "Stock Age (Days)" : fieldName.Equals("receive_age") ? "Receive Age (Days)" : fieldName.Equals("stock_age_in_hour") ? "Stock Age (Hour)" : fieldName.Equals("receive_age_in_hour") ? "Receive Age (Hour)" : col.GetCaption();
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                        col.ColumnEdit = repositoryItemTextEdit1;

                        //fix column
                        col.Fixed = fieldName.Equals("whsecode") || fieldName.Equals("total_available") || fieldName.Equals("item_code") ? FixedStyle.Left : FixedStyle.None;

                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                        col.Visible = !gDt.Columns.Contains("stock_age") && fieldName.Equals("stock_age") ? false : !gDt.Columns.Contains("receive_age") && fieldName.Equals("receive_age") ? false : fieldName.Equals("stock_age_in_hour") ? false : fieldName.Equals("receive_age_in_hour") ? false : true;
                    }
                    gridView1.BestFitColumns();
                    int itemCount = 0;
                    int whseCount = 0;
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, "whsecode").ToString() != "")
                        {
                            whseCount += 1;
                        }
                    }

                    var myColumn = gridView1.Columns.FirstOrDefault((col) => col.GetCaption() == "Transdate");
                    if (myColumn != null)
                    {
                        gridView1.Columns["available_qty"].Summary.Clear();
                        gridView1.Columns["available_qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "available_qty", "Total Available: {0:#,0.000}");
                    }
                    else
                    {
                        gridView1.Columns["whsecode"].Summary.Clear();
                        gridView1.Columns["total_available"].Summary.Clear();
                        gridView1.Columns["whsecode"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "whsecode", "Total Whse: " + whseCount.ToString("N0"));
                        gridView1.Columns["total_available"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_available", "Total Available: {0:#,0.000}");
                    }
                    gridView1.BestFitColumns();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = loadFormat("", true);
            loadData(dt,true);
        }

        public DataTable loadFormat(string whse,bool isOpen)
        {
            var j = (from row in gDt.AsEnumerable()
                     group row by new
                     {
                         WhseCode = row.Field<string>("whsecode"),
                         ItemCode = row.Field<string>("item_code"),
                     } into grp
                     let row = grp.First()
                     select new
                     {
                         WhseCode = grp.Key.WhseCode,
                         ItemCode = grp.Key.ItemCode,
                         TotalAvailableQty =grp.Sum(r=> r.Field<double>("available_qty"))
                     }).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("whsecode", typeof(string));
            dt.Columns.Add("item_code", typeof(string));
            dt.Columns.Add("total_available", typeof(double));
            foreach (var q in j)
            {
                dt.Rows.Add(q.WhseCode,q.ItemCode, q.TotalAvailableQty);
            }

            var z = (from row in dt.AsEnumerable()
                     join row2 in gDt.AsEnumerable() on new { WhseCodee = row.Field<string>("whsecode"), ItemCodee = row.Field<string>("item_code") }
                     equals new { WhseCodee = row2.Field<string>("whsecode"), ItemCodee = row2.Field<string>("item_code") }
                     select new
                     {
                         WhseCode = row.Field<string>("whsecode"),
                         ItemCode = row.Field<string>("item_code"),
                         TotalAvailableQty = row.Field<double>("total_available"),
                         TransDate = row2.Field<DateTime>("transdate"),
                         StockDate = row2.Field<DateTime>("stock_date"),
                         Quantity = row2.Field<double>("quantity"),
                        AvailableQty = row2.Field<double>("available_qty"),
                        StockAge = !gDt.Columns.Contains("stock_age") ? 0.00 :  row2.Field<dynamic>("stock_age") ==null ? 0.00 :  row2.Field<dynamic>("stock_age"),
                        ReceiveAge = !gDt.Columns.Contains("receive_age") ? 0.00 : row2.Field<dynamic>("receive_age") == null ? 0.00 : row2.Field<dynamic>("receive_age"),
                        Vessel = row2.Field<string>("vessel"),
                         StockAgeInDays = row2.Field<dynamic>("stock_age_in_days"),
                         ReceiveAgeInDays = row2.Field<dynamic>("receive_age_in_days"),
                         StockAgeInHour = row2.Field<dynamic>("stock_age_in_hour"),
                        ReceiveAgeInHour = row2.Field<dynamic>("receive_age_in_hour"),
                     }).ToList();

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("whsecode", typeof(string));
            dt2.Columns.Add("item_code", typeof(string));
            dt2.Columns.Add("total_available", typeof(double));
            if (!isOpen)
            {
                dt2.Columns.Add("transdate", typeof(DateTime));
                dt2.Columns.Add("stock_date", typeof(DateTime));
                dt2.Columns.Add("quantity", typeof(double));
                dt2.Columns.Add("available_qty", typeof(double));
                dt2.Columns.Add("stock_age", typeof(long));
                dt2.Columns.Add("receive_age", typeof(long));
                dt2.Columns.Add("vessel", typeof(string));
                dt2.Columns.Add("stock_age_in_hour", typeof(double));
                dt2.Columns.Add("receive_age_in_hour", typeof(double));
                dt2.Columns.Add("stock_age_in_days", typeof(double));
                dt2.Columns.Add("receive_age_in_days", typeof(double));
            }

            string whsecode = "";
            foreach (var q in z)
            {
                if (isOpen)
                {
                    if (!whsecode.Equals(q.WhseCode))
                    {
                        dt2.Rows.Add(q.WhseCode,q.ItemCode, q.TotalAvailableQty);
                    }
                }
                else
                {
                    if (q.WhseCode == whse)
                    {
                        if (!whsecode.Equals(q.WhseCode))
                        {
                            dt2.Rows.Add(q.WhseCode, q.ItemCode, q.TotalAvailableQty, null, null, (double?)null, (double?)null, (long?)null, (long?)null, "", (double?)null, (double?)null);
                        }
                        dt2.Rows.Add(null, null, (double?)null, q.TransDate, q.StockDate, q.Quantity, q.AvailableQty, q.StockAge, q.ReceiveAge, q.Vessel, q.StockAgeInHour, q.ReceiveAgeInHour,q.StockAgeInDays,q.ReceiveAgeInDays);
                    }
                    else
                    {
                        //if (!whsecode.Equals(q.WhseCode))
                        //{
                        //    dt2.Rows.Add(q.WhseCode, q.ItemCode, q.TotalAvailableQty, null, null, (double?)null, (double?)null, (long?)null, (long?)null, "", (double?)null, (double?)null);
                        //    Console.WriteLine("una: " + q.WhseCode, q.ItemCode, q.TotalAvailableQty);
                        //}
                        //dt2.Rows.Add(null, null, (double?)null, q.TransDate, q.StockDate, q.Quantity, q.AvailableQty, q.StockAge, q.ReceiveAge, q.Vessel, q.StockAgeInHour, q.ReceiveAgeInHour);
                        //Console.WriteLine("dalawa: " + q.TransDate, q.StockDate);

                        //Console.WriteLine("dito?");
                    }
                }
                whsecode = q.WhseCode;
            }
            return dt2;
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            string code = gridView1.GetFocusedRowCellValue("whsecode").ToString();
            if (selectedColumnText.Equals("whsecode") && !string.IsNullOrEmpty(code))
            {
                var myColumn = gridView1.Columns.FirstOrDefault((col) => col.GetCaption() == "Transdate");
                bool isOpen = myColumn != null;
                DataTable dt = loadFormat(code, isOpen);
                loadData(dt, isOpen);
                this.Focus();
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle && e.Column.FieldName == "whsecode")
            {
                e.RepositoryItem = e.CellValue==null || string.IsNullOrEmpty(e.CellValue.ToString().Trim()) ? repositoryItemTextEdit1 : repositoryItemTextEdit2;
            }
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }
    }
}
