using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
namespace AB
{
    public partial class MIssueForPacking_TransferItem : Form
    {
        public MIssueForPacking_TransferItem(int id)
        {
            InitializeComponent();
            selectedID = id;
        }
        int selectedID = 0;
        public static bool isSubmit = false;
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtData = new DataTable();
        private void MIssueForPacking_TransferItem_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblFGItem.Visible = lblFGUOM.Visible = lblTargetQuantity.Visible = label8.Visible = label4.Visible = label6.Visible = btnCreateIssuePacking.Visible;
            loadData();
        }


        public void loadData()
        {
            string sParams = "";
            string sResult = apic.loadData("/api/production/for_issue_for_packing/details/", selectedID.ToString(), "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);

                    JObject joData = joResult["data"].IsNullOrEmpty() ? new JObject() : joResult["data"].Type == JTokenType.Object ? (JObject)joResult["data"] : new JObject();

                    DateTime dtTransDate = new DateTime();
                    dtTransDate = joData["transdate"].IsNullOrEmpty() ? new DateTime() : joData["transdate"].Type == JTokenType.Date ? Convert.ToDateTime(joData["transdate"].ToString()) : new DateTime();
                    lblTransdate.Text = dtTransDate.Equals(DateTime.MinValue) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss");
                    lblReference.Text = joData["reference"].IsNullOrEmpty() ? "" : joData["reference"].ToString();
                    lblRemarks.Text = joData["remarks"].IsNullOrEmpty() ? "" : joData["remarks"].ToString();
                    lblCreatedBy.Text = joData["created_by"].IsNullOrEmpty() ? "" : joData["created_by"].ToString();
                    lblFGItem.Text = joData["fg_item"].IsNullOrEmpty() ? "" : joData["fg_item"].ToString();
                    lblFGUOM.Text = joData["fg_uom"].IsNullOrEmpty() ? "" : joData["fg_uom"].ToString();
                    lblTargetQuantity.Text = joData["target_qty"].IsNullOrEmpty() ? "" : joData["target_qty"].Type == JTokenType.Float ? Convert.ToDouble(joData["target_qty"].ToString()).ToString("n3") : "";

                    JArray jaData = joResult["data"]["rows"].IsNullOrEmpty() ? new JArray() : joResult["data"]["rows"].Type == JTokenType.Array ? (JArray)joResult["data"]["rows"] : new JArray();
                    dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    gridControl1.DataSource = null;

                    dtData.SetColumnsOrder("item_code", "quantity", "balance", "uom", "from_whse", "to_whse");

                    gridControl1.DataSource = dtData;
                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
                        string v = col.GetCaption();
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                        col.ColumnEdit = repositoryItemTextEdit1;
                        col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("balance") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("balance") ? "{0:#,0.000}" : "";
                        col.Visible = !(fieldName.Equals("transfer_id") || fieldName.Equals("transrow_id"));

                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
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

        private void btnCreateIssuePacking_Click(object sender, EventArgs e)
        {
            JArray ja = new JArray();
            double doubleTemp = 0.00;
            int intTemp = 0;
            foreach (DataRow row in dtData.Rows)
            {
                JObject jo = new JObject();
                string itemCode = !dtData.Columns.Contains("item_code") ? "" : row.IsNull("item_code") ? "" : row["item_code"].ToString();
                string uom = !dtData.Columns.Contains("uom") ? "" : row.IsNull("uom") ? "" : row["uom"].ToString();
                string whseCode = !dtData.Columns.Contains("to_whse") ? "" : row.IsNull("to_whse") ? "" : row["to_whse"].ToString();
                double balance = !dtData.Columns.Contains("balance") ? doubleTemp : row.IsNull("balance") ? doubleTemp : double.TryParse(row["balance"].ToString(), out doubleTemp) ? Convert.ToDouble(row["balance"].ToString()) : doubleTemp;
                int transRowID = !dtData.Columns.Contains("transrow_id") ? intTemp : row.IsNull("transrow_id") ? intTemp : int.TryParse(row["transrow_id"].ToString(), out intTemp) ? Convert.ToInt32(row["transrow_id"].ToString()) : intTemp;
                jo.Add("item_code", itemCode);
                jo.Add("uom", uom);
                jo.Add("whsecode", whseCode);
                jo.Add("balance", balance);
                jo.Add("actual_quantity", balance);
                jo.Add("variance", 0.000);
                jo.Add("transrow_id", transRowID);
                ja.Add(jo);
            }
            //IssueForProdPacking.jaSelected = ja;
            //IssueForProdPacking frm = new IssueForProdPacking("Issue For Packing",selectedID);
            //frm.frmm = this;
            //MainMenu.showForm(frm);

            IssueForProdPacking_Dialog.isSubmit = false;
            IssueForProdPacking_Dialog frm = new IssueForProdPacking_Dialog(ja, "Issue For Packing", selectedID);
            frm.frmm = this;
            frm.Text = "Create Issue For Packing";
            frm.selectedID = selectedID;
            frm.txtQuantity.Text = "";
            if (!(string.IsNullOrEmpty(lblFGItem.Text.Trim()) || lblFGItem.Text == "N/A"))
            {
                frm.lblFGItem.Text = lblFGItem.Text;
            }
            if (!(string.IsNullOrEmpty(lblFGUOM.Text.Trim()) || lblFGUOM.Text == "N/A"))
            {
                frm.lblFGUom.Text = lblFGUOM.Text;
            }
            frm.lblTargetQuantity.Text = lblTargetQuantity.Text;
            //frm.lblTargetQuantity.Text = lblTargetQuantity.Text;
            frm.ShowDialog();
            if (IssueForProdPacking_Dialog.isSubmit)
            {
                isSubmit = true;
                this.Hide();
            }
            //SystemTransferItem.jaSelected = new JArray();
            //SystemTransferItem frm = new SystemTransferItem(0);
            //MainMenu.showForm(frm);
        }
    }
}
