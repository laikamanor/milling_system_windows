using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class InventoryDetails : Form
    {
        public InventoryDetails(DataTable dt, int id)
        {
            gDt = dt;
            selectedID = id;
            InitializeComponent();
        }
        DataTable gDt = new DataTable();
        int selectedID = 0;
        devexpress_class devc = new devexpress_class();
        private void InventoryDetails_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public void loadData()
        {
            try
            {
                gridControl1.Invoke(new Action(delegate ()
                {
                    string[] list = { "reference", "total_quantity", "transdate", "item_code", "quantity", "warehouse2", "warehouse", "base_ref", "shift", "vessel", "plate_num", "agi_truck_scale", "chti_truck_scale", "sap_number", "processed_by", "remarks" };
                    gDt.SetColumnsOrder(list);
                    gridControl1.DataSource = gDt;

                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                    //auto complete
                    string[] suggestions = { "reference", };
                    string suggestConcat = string.Join(";", suggestions);
                    gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(gridView1, gridControl1, suggestions);

                    gridView1.OptionsSelection.MultiSelectMode = gDt.Rows.Count > 0 ? GridMultiSelectMode.CheckBoxRowSelect : GridMultiSelectMode.RowSelect;
                    gridView1.OptionsSelection.MultiSelect = gDt.Rows.Count > 0 ? true : false;
    
                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
                        col.DisplayFormat.FormatType = fieldName.Equals("transdate") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "{0:#,0.000}";
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());

                        //fix column
                        col.Fixed = fieldName.Equals("reference") || fieldName.Equals("transdate") || fieldName.Equals("item_code") || fieldName.Equals("quantity") ? FixedStyle.Left : FixedStyle.None;

                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                        ////from whse
                        //if ((selectedID == 2 || selectedID == 8) && col.GetCaption() == "Warehouse")
                        //{
                        //    col.Caption = "From Warehouse";
                        //}
                        //else if ((selectedID == 2 || selectedID == 8) && col.GetCaption() == "Warehouse2")
                        //{
                        //    col.Caption = "To Warehouse";
                        //}
                        //else if (selectedID <= 5 && col.GetCaption() == "Warehouse2")
                        //{
                        //    col.Caption = "From Warehouse";
                        //}
                        //else if (selectedID <= 5 && col.GetCaption() == "Warehouse")
                        //{
                        //    col.Caption = "To Warehouse";
                        //}
                        //else if (selectedID <= 13 && col.GetCaption() == "Warehouse")
                        //{
                        //    col.Caption = "From Warehouse";
                        //}
                        //else if(selectedID <= 13 && col.GetCaption() == "Warehouse2")
                        //{
                        //    col.Caption = "To Warehouse";
                        //}

                        col.Caption = (selectedID == 2 || selectedID == 8) && col.GetCaption() == "Warehouse" ? "From Warehouse" : selectedID <= 5 && col.GetCaption() == "Warehouse" ? "To Warehouse" : selectedID <= 13 && col.GetCaption() == "Warehouse" ? "From Warehouse" : col.Caption;

                        col.Caption = (selectedID == 2 || selectedID == 8) && col.GetCaption() == "Warehouse2" ? "To Warehouse"  : selectedID <= 5 && col.GetCaption() == "Warehouse2" ? "From Warehouse" : selectedID <= 13 && col.GetCaption() == "Warehouse2" ? "To Warehouse" : col.Caption;


                        switch (col.Caption)
                        {
                            case "Cust Code":
                                col.Caption = "Cust. Code";
                                col.Visible = this.Text.Equals("Sold") ? true : false;
                                break;
                            case "Discprcnt":
                                col.Caption = "Disc. %";
                                col.Visible = this.Text.Equals("Sold") ? true : false;
                                break;
                            case "Unit Price":
                                col.Visible = this.Text.Equals("Sold") ? true : false;
                                break;
                            case "Disc Amount":
                                col.Caption = "Disc. Amount";
                                col.Visible = this.Text.Equals("Sold") ? true : false;
                                break;
                            case "Net Amount":
                                col.Visible = this.Text.Equals("Sold") ? true : false;
                                break;
                            case "Trans Id":
                                col.Visible = false;
                                break;
                            case "From Warehouse":
                                //col.Visible = this.Text.Equals("Sold") ? true : false;
                                col.Caption = this.Text.Equals("Sold") ? "Warehouse" : col.Caption;
                                break;
                            case "Username":
                                col.Caption = "Processed By";
                                break;
                            case "To Warehouse":
                                col.Visible = this.Text.Equals("Sold") ? false : true;
                                break;
                            case "Sap Number":
                                col.DisplayFormat.FormatString = "";
                                break;
                            default:
                                col.Visible = true;
                                break;
                        }
                        if (col.Caption.Equals("Is Branch To Branch") || col.Caption.Equals("Inter Whse") || col.FieldName.Equals("off_specs") || col.FieldName.Equals("on_specs")) col.Visible = false;

                        (gridControl1.MainView as GridView).Columns[col.AbsoluteIndex].ColumnEdit = repositoryItemTextEdit1;
                    }
                    gridView1.BestFitColumns();
                    if (this.Text == "Sold" && gridView1.Columns.Count > 0)
                    {
                        gridView1.Columns.ColumnByFieldName("cust_code").VisibleIndex = gridView1.Columns.ColumnByFieldName("item_code").VisibleIndex;

                        gridView1.Columns.ColumnByFieldName("item_code").VisibleIndex = gridView1.Columns.ColumnByFieldName("quantity").VisibleIndex;

                        gridView1.Columns.ColumnByFieldName("unit_price").VisibleIndex = gridView1.Columns.ColumnByFieldName("warehouse").VisibleIndex;

                        gridView1.Columns.ColumnByFieldName("discprcnt").VisibleIndex = gridView1.Columns.ColumnByFieldName("warehouse").VisibleIndex;

                        gridView1.Columns.ColumnByFieldName("disc_amount").VisibleIndex = gridView1.Columns.ColumnByFieldName("warehouse").VisibleIndex;

                        gridView1.Columns.ColumnByFieldName("net_amount").VisibleIndex = gridView1.Columns.ColumnByFieldName("warehouse").VisibleIndex;
                        lblDiscAmount.Visible = true;
                        lblNetAmount.Visible = true;
                    }
                    else
                    {

                        if (gridView1.Columns.Count > 0)
                        {
                            if (gridView1.Columns["warehouse"] != null || gridView1.Columns["warehouse"] != null)
                            {
                                //gridView1.Columns.ColumnByFieldName("warehouse2").VisibleIndex = selectedID == 2 ? gridView1.Columns.ColumnByFieldName("warehouse2").VisibleIndex : selectedID <= 5 ? gridView1.Columns.ColumnByFieldName("quantity").VisibleIndex + 2 : gridView1.Columns.ColumnByFieldName("warehouse2").VisibleIndex;
                                gridView1.Columns.ColumnByFieldName("warehouse").VisibleIndex = selectedID == 2 ||  selectedID == 13 ? gridView1.Columns.ColumnByFieldName("quantity").VisibleIndex + 1 : gridView1.Columns.ColumnByFieldName("warehouse").VisibleIndex;
                            }
                        }

                        lblDiscAmount.Visible = false;
                        lblNetAmount.Visible = false;
                    }
                    gridView1.BestFitColumns();
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int[] array1;
            double quantity = 0.00, discAmount = 0.00, netAmount = 0.00, doubleTemp = 0.00;
            array1 = gridView1.GetSelectedRows();
            foreach (int a in array1)
            {
                quantity += gridView1.GetRowCellValue(a, "quantity") != null ? double.TryParse(gridView1.GetRowCellValue(a, "quantity").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetRowCellValue(a, "quantity").ToString()) : doubleTemp : doubleTemp;
                discAmount += gridView1.GetRowCellValue(a, "disc_amount") != null ? double.TryParse(gridView1.GetRowCellValue(a, "disc_amount").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetRowCellValue(a, "disc_amount").ToString()) : doubleTemp : doubleTemp;
                netAmount += gridView1.GetRowCellValue(a, "net_amount") != null ? double.TryParse(gridView1.GetRowCellValue(a, "net_amount").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetRowCellValue(a, "net_amount").ToString()) : doubleTemp : doubleTemp;
            }
            lblTotalQuantity.Text = "Total Quantity: " + quantity.ToString("n3");
            lblDiscAmount.Text = "Total Disc. Amount: " + discAmount.ToString("n3");
            lblNetAmount.Text = "Total Net Amount: " + netAmount.ToString("n3");
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                double doubleTemp = 0.00;
                double discAmt = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "disc_amount") != null ? double.TryParse(gridView1.GetRowCellValue(e.RowHandle, "disc_amount").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "disc_amount").ToString()) : doubleTemp : doubleTemp;
                if (discAmt > 0)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
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


        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            bool boolTemp = false;
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == "warehouse" || e.Column.FieldName == "warehouse2")
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
            }
            else
            {
                if (this.Text.Equals("Receipt From Prod"))
                {
                    bool offSpecs = bool.TryParse(gridView1.GetRowCellValue(e.RowHandle, "off_specs").ToString(), out boolTemp) ? Convert.ToBoolean(gridView1.GetRowCellValue(e.RowHandle, "off_specs").ToString()) : boolTemp;
                    bool onSpecs = bool.TryParse(gridView1.GetRowCellValue(e.RowHandle, "on_specs").ToString(), out boolTemp) ? Convert.ToBoolean(gridView1.GetRowCellValue(e.RowHandle, "on_specs").ToString()) : boolTemp;
                    if (offSpecs)
                    {
                        e.Appearance.BackColor = Color.FromArgb(226, 247, 215);
                    }
                    else if (onSpecs)
                    {
                        e.Appearance.BackColor = Color.FromArgb(247, 215, 215);
                    }
                }
                if (e.RowHandle == HotTrackRow)
                    e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
                else
                    e.Appearance.BackColor = e.Appearance.BackColor;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn column in gridView1.VisibleColumns)
            {
                dt.Columns.Add(column.GetCaption().Replace(" ", "_").ToLower().Trim(), column.ColumnType);
            }
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = dt.NewRow();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    row[column.GetCaption().Replace(" ", "_").ToLower().Trim()] = gridView1.GetRowCellValue(i, column);
                }
                dt.Rows.Add(row);
            }
            crInventoryClick frm = new crInventoryClick(dt, this.Text);
            frm.ShowDialog();
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
