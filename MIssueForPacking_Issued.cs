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
using System.Globalization;
using RestSharp;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using AB.API_Class;

namespace AB
{
    public partial class MIssueForPacking_Issued : Form
    {
        public MIssueForPacking_Issued(int id,string itemCode, string whseCode)
        {
            InitializeComponent();
            selectedID = id;
            gItemCode = itemCode;
            gWhseCode = whseCode;
        }
        int selectedID = 0;
        string gItemCode = "", gWhseCode = "";
        public static bool isSubmit = false;
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtData = new DataTable();
        int currentColorIndex = 0;
        DataTable dtColor = new DataTable();
        private void MIssueForPacking_Issued_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
            loadData();
        }

        public void loadData()
        {
            string sParams = "?item_code=" + gItemCode + "&whsecode=" + gWhseCode;
            string sResult = apic.loadData("/api/production/get_issued_by_transfer_id/", selectedID.ToString() + sParams,"", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);

                    DateTime dtTransDate = new DateTime();
                    //dtTransDate = joData["transdate"].IsNullOrEmpty() ? new DateTime() : joData["transdate"].Type == JTokenType.Date ? Convert.ToDateTime(joData["transdate"].ToString()) : new DateTime();
                    //lblTransdate.Text = dtTransDate.Equals(DateTime.MinValue) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss");
                    //lblReference.Text = joData["reference"].IsNullOrEmpty() ? "" : joData["reference"].ToString();
                    //lblRemarks.Text = joData["remarks"].IsNullOrEmpty() ? "" : joData["remarks"].ToString();
                    //lblCreatedBy.Text = joData["created_by"].IsNullOrEmpty() ? "" : joData["created_by"].ToString();
                    //lblFGItem.Text = joData["fg_item"].IsNullOrEmpty() ? "" : joData["fg_item"].ToString();
                    //lblTargetQuantity.Text = joData["target_qty"].IsNullOrEmpty() ? "" : joData["target_qty"].Type == JTokenType.Float ? Convert.ToDouble(joData["target_qty"].ToString()).ToString("n3") : "";

                    JArray jaData = joResult["data"].IsNullOrEmpty() ? new JArray() : joResult["data"].Type == JTokenType.Array ? (JArray)joResult["data"] : new JArray();
                    dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    gridControl1.DataSource = null;

                    dtData.SetColumnsOrder("id", "transdate", "reference","item_code","quantity","whsecode", "remarks", "fg_item", "fg_quantity","fg_targeted_qty", "created_by");

                    currentColorIndex = 0;
                    color_class colorc = new color_class();
                    dtColor.Rows.Clear();
                    foreach (DataRow row in dtData.Rows)
                    {
                        string currentRef = row["reference"].ToString();
                        foreach (DataRow row2 in dtData.Rows)
                        {
                            currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                            string currentRef1 = row2["reference"].ToString();
                            bool v = (currentRef == currentRef1) && (dtData.Rows.IndexOf(row) != dtData.Rows.IndexOf(row2));
                            if (v)
                            {
                                Color cc = colorc.c[currentColorIndex];
                                string hex = string.Format("{0:X2}{1:X2}{2:X2}", cc.R, cc.G, cc.B);
                                dtColor.Rows.Add(dtData.Rows.IndexOf(row), hex);
                                dtColor.Rows.Add(dtData.Rows.IndexOf(row2), hex);
                            }
                            else if (currentRef != currentRef1)
                            {
                                currentColorIndex++;
                            }
                        }
                    }

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
                        col.DisplayFormat.FormatType = fieldName.Equals("fg_quantity") || fieldName.Equals("quantity") || fieldName.Equals("fg_targeted_qty") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") ?DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = fieldName.Equals("fg_quantity") || fieldName.Equals("quantity") || fieldName.Equals("fg_targeted_qty") ? "{0:#,0.000}" : fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "";
                        col.Visible = !(fieldName.Equals("id"));

                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                    }

                    if (gridView1.Columns["quantity"] != null)
                    {
                        gridView1.Columns["quantity"].Summary.Clear();
                        gridView1.Columns["quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "quantity", "Total Issued Qty: {0:n3}");

                    }

                    //auto complete
                    string[] suggestions = { "item_code" };
                    string suggestConcat = string.Join(";", suggestions);
                    gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(gridView1, gridControl1, suggestions);
                    gridView1.BestFitColumns();
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
            if (e.Column.FieldName.Equals("reference"))
            {
                foreach (DataRow row in dtColor.Rows)
                {
                    int index = 0, intTemp = 0;
                    index = int.TryParse(row["index"].ToString(), out intTemp) ? Convert.ToInt32(row["index"].ToString()) : intTemp;
                    if (index == e.RowHandle)
                    {
                        //Color color = new Color(), colorTemp = new Color();
                        //Console.WriteLine(row["color"].ToString());
                        //e.Appearance.BackColor = ColorTranslator.FromHtml(row["color"].ToString());
                        //Color.from
                        e.Appearance.BackColor = ColorTranslator.FromHtml("#" + row["color"].ToString());
                    }
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
    }
}
