using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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

namespace AB
{
    public partial class InventoryDetails_Receive : Form
    {
        public InventoryDetails_Receive(DataTable dt)
        {
            gDt = dt;
            InitializeComponent();
        }
        DataTable gDt = new DataTable();
        private void InventoryDetails_Receive_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public void loadData(DataTable dt)
        {
            string[] list = {"reference","total_quantity", "transdate", "item_code", "quantity", "from_warehouse", "to_warehouse", "base_ref","shift","vessel","plate_num","agi_truck_scale","chti_truck_scale","sap_number", "remarks","processed_by"};
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                dt.SetColumnsOrder(list);
                gridControl1.DataSource = dt;
                foreach (GridColumn col in gridView1.Columns)
                {
                    string fieldName = col.FieldName;
                    col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("total_quantity") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                    col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("total_quantity") ? "n2" : fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "";
                    string s = col.GetCaption().Replace("_", " ");
                    s = s.Equals("warehouse2") ? "from warehouse" : s.Equals("warehouse") ? "to warehouse" : s;
                    col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                    col.Visible = col.FieldName.Equals("sap_number") || col.FieldName.Equals("transdate") || col.FieldName.Equals("reference") || col.FieldName.Equals("item_code") || col.FieldName.Equals("quantity") || col.FieldName.Equals("from_warehouse") || col.FieldName.Equals("to_warehouse") || col.FieldName.Equals("base_ref") || col.FieldName.Equals("shift") || col.FieldName.Equals("vessel") || col.FieldName.Equals("plate_num") || col.FieldName.Equals("agi_truck_scale") || col.FieldName.Equals("chti_truck_scale") || col.FieldName.Equals("remarks") || col.FieldName.Equals("processed_by") || col.FieldName.Equals("total_quantity");
                    col.ColumnEdit = fieldName.Equals("reference") ? repositoryItemTextEdit2 : repositoryItemTextEdit1;
                }
                int itemCount = 0;
                int refCount = 0;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    if (gridView1.GetRowCellValue(i, "reference").ToString() != "")
                    {
                        refCount += 1;
                    }
                    else if (gridView1.GetRowCellValue(i, "item_code").ToString() != "")
                    {
                        itemCount += 1;
                    }
                }
                var myColumn = gridView1.Columns.FirstOrDefault((col) => col.GetCaption() == "Item Code");
                if (myColumn != null)
                {
                    gridView1.Columns["item_code"].Summary.Clear();
                    gridView1.Columns["total_quantity"].Summary.Clear();
                    gridView1.Columns["item_code"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "item_code", "Total Item: " + itemCount.ToString("N0"));
                    gridView1.Columns["total_quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_quantity", "Total Quantity: {0:n2}");
                }
                else
                {
                    gridView1.Columns["reference"].Summary.Clear();
                    gridView1.Columns["total_quantity"].Summary.Clear();
                    gridView1.Columns["reference"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "reference", "Total Reference: " + refCount.ToString("N0"));
                    gridView1.Columns["total_quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "total_quantity", "Total Quantity: {0:n2}");

                }
                //if(this.Text.Equals("Transfer Out"))
                //{
                //    gridView1.Columns["from_warehouse"].VisibleIndex = gridView1.Columns["quantity"].VisibleIndex - 1;
                //}
                gridView1.BestFitColumns();
            }));

        }


        public DataTable loadFormat(string reff, bool isOpen)
        {
            double doubleTemp = 0.00;

            var j = (from row in gDt.AsEnumerable()
                     group row by new
                     {
                         Reference = row.Field<string>("reference"),
                     } into grp
                     let row = grp.First()
                     select new
                     {
                         Reference = grp.Key.Reference,
                         TotalQty = grp.Sum(r => r.Field<double>("quantity"))
                     }).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("reference", typeof(string));
            dt.Columns.Add("total_quantity", typeof(double));
            foreach (var q in j)
            {
                dt.Rows.Add(q.Reference, q.TotalQty);
            }
            string fromWhse = this.Text.Equals("Out Transit (Out For Delivery)") || this.Text.Equals("In Transit (For Received)") || this.Text.Equals("Transfer Out") ? "warehouse" : "warehouse2";
            string toWhse = this.Text.Equals("Out Transit (Out For Delivery)") || this.Text.Equals("In Transit (For Received)") || this.Text.Equals("Transfer Out") ? "warehouse2" : "warehouse";
            //string[] list = { "sap_number", "transdate", "reference", "item_code", "quantity", "warehouse2", "warehouse", "base_ref", "shift", "vessel", "plate_num", "agi_truck_scale", "chti_truck_scale", "remarks", "processed_by" };
            var z = (from row in dt.AsEnumerable()
                     join row2 in gDt.AsEnumerable() on row.Field<string>("reference") equals row2.Field<string>("reference")
                     select new 
                     {
                         SapNumber = row2.Field<string>("sap_number"),
                         Reference = row.Field<string>("reference"),
                         Transdate = row2.Field<string>("transdate"),
                         ItemCode = row2.Field<string>("item_code"),
                         TotalQty = row.Field<double>("total_quantity"),
                         FromWhse = row2.Field<string>(fromWhse),
                         ToWhse = row2.Field<string>(toWhse),
                         Quantity = row2.Field<double>("quantity"),

                         BaseRef = row2.Field<string>("base_ref"),
                         Shift = row2.Field<string>("shift"),
                         Vessel = row2.Field<string>("vessel"),
                         AgiTS = row2.Field<string>("agi_truck_scale"),
                         CHTITS = row2.Field<string>("chti_truck_scale"),
                         Remarks = row2.Field<string>("remarks"),
                         ProcessedBy = row2.Field<string>("username"),
                         IsBranchToBranch = row2.Field<bool>("is_branch_to_branch"),
                         InterWhse = row2.Field<bool>("inter_whse"),
                         OffSpecs = row2.Field<dynamic>("off_specs") == null ? false : row2.Field<dynamic>("off_specs"),
                         OnSpecs = row2.Field<dynamic>("on_specs") == null ? false : row2.Field<dynamic>("on_specs"),
                     }).ToList();

            DataTable dt2 = new DataTable();

            dt2.Columns.Add("reference", typeof(string));
            dt2.Columns.Add("total_quantity", typeof(double));
            if (!isOpen)
            {
                dt2.Columns.Add("sap_number", typeof(string));
                dt2.Columns.Add("transdate", typeof(string));
                dt2.Columns.Add("item_code", typeof(string));
                dt2.Columns.Add("quantity", typeof(double));
                dt2.Columns.Add("from_warehouse", typeof(string));
                dt2.Columns.Add("to_warehouse", typeof(string));
                dt2.Columns.Add("base_ref", typeof(string));
                dt2.Columns.Add("shift", typeof(string));
                dt2.Columns.Add("vessel", typeof(string));
                dt2.Columns.Add("agi_truck_scale", typeof(string));
                dt2.Columns.Add("chti_truck_scale", typeof(string));
                dt2.Columns.Add("processed_by", typeof(string));
                dt2.Columns.Add("remarks", typeof(string));
                dt2.Columns.Add("is_branch_to_branch", typeof(bool));
                dt2.Columns.Add("inter_whse", typeof(bool));
                dt2.Columns.Add("off_specs", typeof(bool));
                dt2.Columns.Add("on_specs", typeof(bool));
            }

            string reference = "";
            foreach (var q in z)
            {
                if (isOpen)
                {
                    if (!reference.Equals(q.Reference))
                    {
                        dt2.Rows.Add(q.Reference, q.TotalQty);
                    }
                }
                else
                {
                    if (q.Reference == reff)
                    {
                        if (!reference.Equals(q.Reference))
                        {
                            dt2.Rows.Add(q.Reference,q.TotalQty, null, (double?)null, null, null, null,null, null,null,null,null,null,null, null, null, null);
                        }
                        dt2.Rows.Add(null, (double?)null,q.SapNumber,q.Transdate, q.ItemCode, q.Quantity, q.FromWhse, q.ToWhse, q.BaseRef, q.Shift, q.Vessel, q.AgiTS, q.CHTITS, q.ProcessedBy, q.Remarks, q.IsBranchToBranch, q.InterWhse, q.OffSpecs, q.OnSpecs);
                    }
                }
                reference = q.Reference;
            }
            return dt2;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = loadFormat("", true);
            loadData(dt);
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            
        }

        private void repositoryItemTextEdit2_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            string code = gridView1.GetFocusedRowCellValue("reference").ToString();
            if (selectedColumnText.Equals("reference") && !string.IsNullOrEmpty(code))
            {
                var myColumn = gridView1.Columns.FirstOrDefault((col) => col.FieldName == "from_warehouse");
                bool isOpen = myColumn != null;
                DataTable dt = loadFormat(code, isOpen);
                loadData(dt);
                this.Focus();
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle && e.Column.FieldName == "reference")
            {
                e.RepositoryItem = e.CellValue == null || string.IsNullOrEmpty(e.CellValue.ToString().Trim()) ? repositoryItemTextEdit1 : repositoryItemTextEdit2;
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
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            bool boolTemp = false;
            GridView currentView = sender as GridView;
            var myColumn = gridView1.Columns.FirstOrDefault((col) => col.FieldName == "from_warehouse");
            if (myColumn != null && e.Column.FieldName == "from_warehouse" || e.Column.FieldName == "to_warehouse")
            {
                bool isBranchToBranch = bool.TryParse(gridView1.GetRowCellValue(e.RowHandle, "is_branch_to_branch").ToString(), out boolTemp) ? Convert.ToBoolean(gridView1.GetRowCellValue(e.RowHandle, "is_branch_to_branch").ToString()) : boolTemp;
                bool interWhse = bool.TryParse(gridView1.GetRowCellValue(e.RowHandle, "inter_whse").ToString(), out boolTemp) ? Convert.ToBoolean(gridView1.GetRowCellValue(e.RowHandle, "inter_whse").ToString()) : boolTemp;
                if (isBranchToBranch)
                {
                    e.Appearance.BackColor = Color.FromArgb(115, 255, 110);
                }
                else if (interWhse)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 173, 110);
                }
            }else
            {
                if (e.RowHandle == HotTrackRow)
                    e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
                else
                    e.Appearance.BackColor = e.Appearance.BackColor;
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
    }
}
