using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
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
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class showAvailableQtyPerWhse : Form
    {
        public showAvailableQtyPerWhse(string itemCode,string selectedUom, string mode)
        {
            InitializeComponent();
            gItemCode = itemCode;
            gUom = selectedUom;
            gMode = mode;
        }
        public string hiddenTitle = "";
        string gItemCode = "", gUom = "", gMode = "";
        api_class apic = new api_class();
        public static bool isSubmit = false;
        public static string selectedWhse = "";
        public static double quantity = 0.00;
        private void showAvailableQtyPerWhse_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblItem.Text = gItemCode;
            loadData();
        }

        public void loadData()
        {
            string sItemCode = "?item_code=" + gItemCode;
            string sParams = sItemCode;
            string sResult = apic.loadData("/api/inv/per_whse/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dt;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("stock_age") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("stock_age") ? "{0:#,0.000}" : "";
                            col.Visible = !fieldName.Equals("item_code");
                        }
                        gridView1.BestFitColumns();
                    }));
                }
            }
        }

        private void lblItem_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            selectedWhse = gridView1.GetFocusedRowCellValue("warehouse") == null ? "" : gridView1.GetFocusedRowCellValue("warehouse").ToString();
            double doubleTemp = 0.00;
            quantity = gridView1.GetFocusedRowCellValue("quantity") == null ? 0.00 : double.TryParse(gridView1.GetFocusedRowCellValue("quantity").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetFocusedRowCellValue("quantity").ToString()) : doubleTemp;
            isSubmit = true;
            //lblFromWhse.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            if(hiddenTitle.Equals("Issue For Production") || hiddenTitle.Equals("Issue For Packing"))
            {
                IssueForProdPacking_Details.isSubmit = false;
                IssueForProdPacking_Details.quantity = 0;
                IssueForProdPacking_Details.itemCode = "";
                IssueForProdPacking_Details.uom = "";
                IssueForProdPacking_Details.fromWhse = "";
                IssueForProdPacking_Details frm2 = new IssueForProdPacking_Details(gItemCode, gUom, true);
                frm2.lblItemCode.Text = "Item Code: " + gItemCode;
                frm2.lblUom.Text = "UOM: " + gUom;
                frm2.txtQuantity.Text = "0";
                frm2.lblFromWhse.Text = showAvailableQtyPerWhse.selectedWhse;
                frm2.txtQuantity.Text = quantity.ToString("n3");
                frm2.ShowDialog();
                if (IssueForProdPacking_Details.isSubmit)
                {
                    this.Hide();
                }
            }else if(hiddenTitle.Equals("System Transfer Item") && gMode.Equals("Add"))
            {
                SystemTransferItem_Details.fromWhse = "";
                SystemTransferItem_Details.toWhse = "";
                SystemTransferItem_Details.isSubmit = false;
                SystemTransferItem_Details.quantity = 0;
                SystemTransferItem_Details frm = new SystemTransferItem_Details(gItemCode,gUom, quantity,gMode);
                frm.txtQuantity.Text = quantity.ToString("n3");
                frm.lblFromWhse.Text = showAvailableQtyPerWhse.selectedWhse;
                frm.ShowDialog();
                if (SystemTransferItem_Details.isSubmit)
                {
                    this.Hide();
                }
            }
            else if (hiddenTitle.Equals("System Transfer Item") && gMode.Equals("Edit"))
            {
                this.Hide();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Focus();
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

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Focus();
        }
    }
}
