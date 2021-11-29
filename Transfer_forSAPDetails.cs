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
using System.Globalization;

namespace AB
{
    public partial class Transfer_forSAPDetails : Form
    {
        public Transfer_forSAPDetails(string type, string sSelectedIds)
        {
            gType = type;
            selectedIds = sSelectedIds;
            InitializeComponent();
        }
        string selectedIds = "", gType = "";
        api_class apic = new api_class();
        public static bool isSubmit = false;    
        private void Transfer_forSAPDetails_Load(object sender, EventArgs e)
        {
            buttonUpdateSAP.Visible = gType.Equals("Open") ? true : false;
            loadData();
        }

        public void loadData()
        {
            string sResult =  apic.loadData("/api/inv/trfr/for_sap/details", "?ids=%5B" + selectedIds + "%5D", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    JObject jResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)jResult["data"];
                    Console.WriteLine(jaData.ToString());
                    DataTable dtResult = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    gridControl1.DataSource = dtResult;
                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
                    {
                        col.Caption = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(col.GetCaption().ToLower());
                        col.DisplayFormat.FormatType = col.GetCaption().Equals("Quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = col.GetCaption().Equals("Quantity") ? "n2" : "";
                        col.ColumnEdit = repositoryItemTextEdit1;
                        gridView1.Columns["item_code"].Summary.Clear();
                        gridView1.Columns["item_code"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "item_code", "Count: {0:N0}");
                    }
                }
            }
        }

        private void btnSelectMultipleItem_Click(object sender, EventArgs e)
        {
            if (gridView1.Columns["item_code"] != null)
            {
                gridView1.ShowFilterPopup(gridView1.Columns["item_code"]);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            gridView1.SelectAll();
            gridView1.CopyToClipboard();
            MessageBox.Show("Copied to clipboard", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonUpdateSAP_Click(object sender, EventArgs e)
        {
            bool hasAccess = gType.Equals("Open") ? true : false;
            if (hasAccess)
            {
                SAP_Remarks frm = new SAP_Remarks();
                frm.ShowDialog();
                if (SAP_Remarks.isSubmit)
                {
                    string[] ids = selectedIds.Split(',');
                    int iid = 0, intTemp = 0;
                    JArray jaID = new JArray();
                    JObject joData = new JObject();
                    foreach (string id in ids)
                    {
                        iid = Int32.TryParse(id, out intTemp) ? Convert.ToInt32(id) : intTemp;
                        jaID.Add(iid);
                    }
                    joData.Add("ids", jaID);
                    joData.Add("sap_number", SAP_Remarks.sap_number);
                    joData.Add("remarks", SAP_Remarks.rem);
                    string sResult = apic.loadData("/api/inv/trfr/update_sap", "", "application/json", joData.ToString(), RestSharp.Method.PUT, true);
                    if (!string.IsNullOrEmpty(sResult))
                    {
                        if (sResult.Substring(0, 1).Equals("{"))
                        {
                            JObject joResult = JObject.Parse(sResult);
                            apic.showCustomMsgBox("Message", (string)joResult["message"]);
                            isSubmit = true;
                            this.Hide();
                        }
                        else
                        {
                            apic.showCustomMsgBox("Validation", sResult);
                        }
                    }
                }
            }
            else
            {
                apic.showCustomMsgBox("Validation", "Access Denied");
            }

        }
    }
}
