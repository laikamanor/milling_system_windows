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
    public partial class TransferItem_Details : Form
    {
        public TransferItem_Details(int id)
        {
            InitializeComponent();
            selectedID = id;
        }
        int selectedID = 0;
        public static bool isSubmit = false;
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtData = new DataTable();
        private void TransferItem_Details_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            btnCancel.Visible = !(lblDocStatus.Text.Trim().ToLower().Contains("open") || lblDocStatus.Text.Trim().ToLower().Contains("closed"));
            loadData();
        }

        public void loadData()
        {
            try
            {
                string sParams = selectedID.ToString();
                string sResult = apic.loadData("/api/inv/trfr/getdetails/", sParams, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    DateTime dtTemp = new DateTime();
                    JObject joResponse = JObject.Parse(sResult);
                    JObject joData = joResponse["data"] == null ? new JObject() : (JObject)joResponse["data"];

                    lblReference.Text = joData["reference"] == null ? "" : joData["reference"].ToString();
                    string docStatus = joData["docstatus"] == null ? "" : joData["docstatus"].ToString();
                    docStatus = docStatus.Equals("O") ? "Open" : docStatus.Equals("C") ? "Closed" : docStatus.Equals("N") ? "Cancelled" : "";
                    lblDocStatus.Text = docStatus;
                    lblTransDate.Text = joData["transdate"] == null ? "" : DateTime.TryParse(joData["transdate"].ToString().Replace("T", " "), out dtTemp) ? Convert.ToDateTime(joData["transdate"].ToString().Replace("T", " ")).ToString("yyyy-MM-dd HH:mm:ss") : "";
                    JArray jaTransRow = joData["transrow"] == null ? new JArray() : (JArray)joData["transrow"];
                    lblToWhse.Text = jaTransRow[0]["to_whse"].ToString();
                    dtData = (DataTable)JsonConvert.DeserializeObject(jaTransRow.ToString(), (typeof(DataTable)));

                    gridControl1.DataSource = null;
                    string[] columnVisible = new string[]
                    {
                            "item_code", "quantity", "actualrec", "uom","from_whse","to_whse"
                    };
                    dtData.SetColumnsOrder(columnVisible);

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
                        col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("actualrec") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("actualrec") ? "{0:#,0.000}" : "";
                        col.Visible = fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("actualrec") || fieldName.Equals("uom") || fieldName.Equals("from_whse") || fieldName.Equals("to_whse");

                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                    }
                    //auto complete
                    string[] suggestions = { "reference" };
                    string suggestConcat = string.Join(";", suggestions);
                    gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(gridView1, gridControl1, suggestions);
                    gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string sResult = apic.loadData("/api/inv/trfr/cancel/", selectedID.ToString(), "application/json", joBody.ToString(), Method.PUT, true);
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

        private void btnCreateIssuePacking_Click(object sender, EventArgs e)
        {
            JArray ja = new JArray();
            double doubleTemp = 0.00;
            foreach(DataRow row in dtData.Rows)
            {
                JObject jo = new JObject();
                string itemCode = row.IsNull("item_code") ? "" : row["item_code"].ToString();
                string uom = row.IsNull("uom") ? "" : row["uom"].ToString();
                string whseCode = row.IsNull("to_whse") ? "" : row["to_whse"].ToString();
                double actualRec = row.IsNull("actualrec") ? doubleTemp : double.TryParse(row["actualrec"].ToString(), out doubleTemp) ? Convert.ToDouble(row["actualrec"].ToString()) : doubleTemp ;
                jo.Add("item_code", itemCode);
                jo.Add("uom", uom);
                jo.Add("whsecode", whseCode);
                jo.Add("quantity", actualRec);
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
