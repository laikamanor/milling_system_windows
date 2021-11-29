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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Receiveitem_Details : Form
    {
        public Receiveitem_Details(int id)
        {
            InitializeComponent();
            selectedID = id;
        }
        int selectedID = 0;
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        public static bool isSubmit = false;
        private void Receiveitem_Details_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadData();
            btnCancel.Visible = !(lblDocStatus.Text.Trim().ToLower().Contains("cancelled") || lblDocStatus.Text.Equals("C"));
        }

        public void loadData()
        {
          try
            {
                string sParams = selectedID.ToString();
                string sResult = apic.loadData("/api/inv/recv/details/", sParams, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    DateTime dtTemp = new DateTime();
                    JObject joResponse = JObject.Parse(sResult);
                    JObject joData = joResponse["data"] == null ? new JObject() : (JObject)joResponse["data"];
                   
                    lblReference.Text = joData["reference"] == null ? "" : joData["reference"].ToString();
                    string docStatus = joData["docstatus"] == null ? "" : joData["docstatus"].ToString();
                    docStatus= docStatus.Equals("O") ? "Open" : docStatus.Equals("C") ? "Closed" : docStatus.Equals("N") ? "Cancelled" : "";
                    lblDocStatus.Text = docStatus;
                    lblTransDate.Text = joData["transdate"] == null ? "" : DateTime.TryParse(joData["transdate"].ToString().Replace("T", " "), out dtTemp) ? Convert.ToDateTime(joData["transdate"].ToString().Replace("T", " ")).ToString("yyyy-MM-dd HH:mm:ss") : "";
                    JArray jaRecRow = joData["recrow"] == null ? new JArray() : (JArray)joData["recrow"];
                    DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaRecRow.ToString(), (typeof(DataTable)));

                    DataTable dtNewData = new DataTable();
                    dtNewData.Columns.Add("item_code", typeof(string));
                    dtNewData.Columns.Add("quantity", typeof(double));
                    dtNewData.Columns.Add("actualrec", typeof(double));
                    dtNewData.Columns.Add("variance", typeof(double));
                    dtNewData.Columns.Add("uom", typeof(string));
                    foreach (DataRow row in dtData.Rows)
                    {
                        string itemCode = row["item_code"] == null ? "" : row["item_code"].ToString(),
                            uom = row["uom"] == null ? "" : row["uom"].ToString(),
                            fromWhse = row["from_whse"] == null ? "" : row["from_whse"].ToString();
                        double quantity = 0.00, actualRec = 0.00, variance = 0.00, doubleTemp = 0.00;
                        quantity = row["quantity"] == null ? 0.00 : double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;
                        actualRec = row["actualrec"] == null ? 0.00 : double.TryParse(row["actualrec"].ToString(), out doubleTemp) ? Convert.ToDouble(row["actualrec"].ToString()) : doubleTemp;
                        variance = (actualRec - quantity);
                        dtNewData.Rows.Add(itemCode, quantity, actualRec, variance, uom);
                        lblFromWhse.Text = fromWhse;
                    }
                    gridControl1.DataSource = null;
                    string[] columnVisible = new string[]
                    {
                            "item_code", "quantity", "actualrec","variance", "uom"
                    };
                    dtNewData.SetColumnsOrder(columnVisible);


                    //auto complete
                    string[] suggestions = { "item_code" };
                    string suggestConcat = string.Join(";", suggestions);
                    gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(gridView1, gridControl1, suggestions);


                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                    gridControl1.DataSource = dtNewData;
                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
                        string v = col.GetCaption();
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                        col.ColumnEdit = repositoryItemTextEdit1;
                        col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("actualrec") || fieldName.Equals("variance") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("actualrec") || fieldName.Equals("variance") ? "{0:#,0.000}" : "";
                        col.Visible = fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("actualrec") || fieldName.Equals("variance") || fieldName.Equals("uom");


                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                    }
                    gridView1.BestFitColumns();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                double doubleTemp = 0.00;
                double variance = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "variance") != null ? double.TryParse(gridView1.GetRowCellValue(e.RowHandle, "variance").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle, "variance").ToString()) : doubleTemp : doubleTemp;
                if (variance < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(248, 255, 43);
                }
                else if (variance > 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(0, 227, 76);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Remarks.isSubmit = false;
            Remarks.rem = "";
            Remarks remarkss = new Remarks();
            remarkss.ShowDialog();
            if (Remarks.isSubmit)
            {
                string remarks = Remarks.rem;
                try
                {
                    JObject joBody = new JObject();
                    joBody.Add("remarks", remarks);
                    string sResult = apic.loadData("/api/inv/recv/cancel/", selectedID.ToString(), "application/json", joBody.ToString(), Method.PUT, true);
                    if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                    {
                        JObject jObjectResponse = JObject.Parse(sResult);
                        string msg = jObjectResponse["message"] == null ? "" : jObjectResponse["message"].ToString();
                        bool isSuccess = false, boolTemp = false;
                        isSuccess = jObjectResponse["success"] == null ? false : bool.TryParse(jObjectResponse["success"].ToString(), out boolTemp) ? Convert.ToBoolean(jObjectResponse["success"].ToString()) : boolTemp;
                        MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        if (isSuccess)
                        {
                            isSubmit = true;
                            this.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
