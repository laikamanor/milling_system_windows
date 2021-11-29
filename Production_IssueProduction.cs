using Newtonsoft.Json.Linq;
using RestSharp;
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
using AB.API_Class.Warehouse;
using AB.API_Class.Branch;
using Newtonsoft.Json;

namespace AB
{
    public partial class Production_IssueProduction : Form
    {
        public Production_IssueProduction(string type)
        {
            gType = type;
            InitializeComponent();
        }
        string gType = "";
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        branch_class branchc = new branch_class();
        warehouse_class warehousec = new warehouse_class();
        DataTable dtBranches = new DataTable(), dtPlant = new DataTable();
        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv.Rows.Count > 0)
            {
                if(e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 8)
                    {
                        Production_ReceivedProduction_Items items = new Production_ReceivedProduction_Items(gType);
                        items.selectedID = string.IsNullOrEmpty(dgv.CurrentRow.Cells["receipt_id"].Value.ToString()) ? 0 : Convert.ToInt32(dgv.CurrentRow.Cells["receipt_id"].Value.ToString());
                        items.reference = dgv.CurrentRow.Cells["reference"].Value.ToString();
                        items.ShowDialog();
                    }
                    else if(e.ColumnIndex==2)
                    {
                        Production_IssueProduction_Items items = new Production_IssueProduction_Items(gType);
                        items.selectedID = string.IsNullOrEmpty(dgv.CurrentRow.Cells["id"].Value.ToString()) ? 0 : Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value.ToString());
                        items.reference = dgv.CurrentRow.Cells["reference"].Value.ToString();
                        items.ShowDialog();
                        if (Production_IssueProduction_Items.isSubmit)
                        {
                            loadData();
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void Production_IssueProduction_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dgv.Columns["btnViewRemarks"].Visible = gType.Equals("Closed");
            dtFromDate.Visible = false;
            checkToDate.Checked = true;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            loadPlant();
            loadData();
        }

        public void loadPlant()
        {
            cmbPlant.Properties.Items.Clear();
            cmbPlant.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/plant/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtPlant.Rows)
                    {
                        cmbPlant.Properties.Items.Add(row["name"].ToString());
                    }
                    string plantCode = Login.jsonResult["data"]["plant"].ToString();
                    string plantName = apic.findValueInDataTable(dtPlant, plantCode, "code", "name");
                    cmbPlant.SelectedIndex = cmbPlant.Properties.Items.IndexOf(plantName) <= 0 ? 0 : cmbPlant.Properties.Items.IndexOf(plantName);
                }
            }
            else
            {
                cmbPlant.SelectedIndex = 0;
            }
        }

        public void loadBranches()
        {
            try
            {
                string plantCode = "";
                cmbPlant.Invoke(new Action(delegate ()
                {
                    plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                }));
                cmbBranch.Invoke(new Action(delegate ()
                {
                    cmbBranch.Properties.Items.Clear();
                }));
                string[] lists = { "isAdmin", "isSuperAdmin" };
                if (apic.haveAccess(lists))
                {
                    string sResult = "";
                    sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", Method.GET, true);
                    if (sResult.Substring(0, 1).Equals("{"))
                    {
                        //DataTable dtData = apic.getDtDownloadResources(sResult, "data");
                        //string sBranch = apic.getFirstRowDownloadResources(dtData, "data");

                        dtBranches = apic.getDtDownloadResources(sResult, "data");
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                cmbBranch.Properties.Items.Add("All");
                            }));
                        }
                        foreach (DataRow row in dtBranches.Rows)
                        {
                            if (IsHandleCreated)
                            {
                                cmbBranch.Invoke(new Action(delegate ()
                                {
                                    cmbBranch.Properties.Items.Add(row["name"].ToString());
                                }));
                            }

                        }
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                string branch = (string)Login.jsonResult["data"]["branch"];
                                string s = apic.findValueInDataTable(dtBranches, branch, "code", "name");
                                cmbBranch.SelectedIndex = cmbBranch.Properties.Items.IndexOf(s) <= 0 ? 0 : cmbBranch.Properties.Items.IndexOf(s);

                            }));
                        }
                    }
                    else
                    {
                        apic.showCustomMsgBox("Validation", sResult);
                    }
                }
                else
                {
                    if (IsHandleCreated)
                    {
                        cmbBranch.Invoke(new Action(delegate ()
                        {
                            cmbBranch.Properties.Items.Add(Login.jsonResult["data"]["branch"]);
                            cmbBranch.SelectedIndex = 0;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string findCode(string value, string typee)
        {
            string result = "";
            foreach (DataRow row in dtBranches.Rows)
            {
                if (row["name"].ToString() == value)
                {
                    result = row["code"].ToString();
                    break;
                }
            }
                return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {
            if (Login.jsonResult != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                string token = "";
                foreach (var x in Login.jsonResult)
                {
                    if (x.Key.Equals("token"))
                    {
                        token = x.Value.ToString();
                    }
                }
                if (!token.Equals(""))
                {
                    var client = new RestClient(utilityc.URL);
                    client.Timeout = -1;
                    //string branch = "A1-S";
                    string sDocStatus = "?docstatus=" + (gType.Equals("Open") ? "O" : gType.Equals("Closed") ? "C" : gType.Equals("Cancelled") ? "N" : "");
                    string sBranch = "&branch=" + findCode(cmbBranch.Text, "Branch");
                    //string sWarehouse = "&whsecode=" + findCode(cmbWarehouse.Text, "Warehouse");
                    string sDate = !checkDate.Checked ? "&from_date=" : "&from_date=" + dtFromDate.Value.ToString("yyyy-MM-dd");
                    sDate += !checkToDate.Checked ? "&to_date=" : "&to_date=" + dtToDate.Value.ToString("yyyy-MM-dd");
                    //di pa nalalagay
                    string sFromTime = "&from_time=" + cmbFromTime.Text;
                    string sToTime = "&to_time=" + cmbToTime.Text;

                    var request = new RestRequest("/api/production/issue_for_prod/get_all" + sDocStatus + sBranch + sDate + sFromTime + sToTime);
                    Console.WriteLine("/api/production/issue_for_prod/get_all" + sDocStatus + sBranch + sDate  + sFromTime + sToTime);
                    request.AddHeader("Authorization", "Bearer " + token);
                    var response = client.Execute(request);
                    Console.WriteLine(response.Content);
                    if (response.ErrorMessage == null)
                    {
                        if (response.Content.ToString().Substring(0, 1).Equals("{"))
                        {
                            JObject jObject = new JObject();
                            jObject = JObject.Parse(response.Content.ToString());
                            dgv.Rows.Clear();
                            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                            bool isSuccess = false;
                            foreach (var x in jObject)
                            {
                                if (x.Key.Equals("success"))
                                {
                                    isSuccess = Convert.ToBoolean(x.Value.ToString());
                                }
                            }
                            if (isSuccess)
                            {
                                foreach (var x in jObject)
                                {
                                    if (x.Key.Equals("data"))
                                    {
                                        if (x.Value.ToString() != "[]")
                                        {
                                            JArray jsonArray = JArray.Parse(x.Value.ToString());
                                            for (int i = 0; i < jsonArray.Count(); i++)
                                            {
                                                JObject data = JObject.Parse(jsonArray[i].ToString());
                                                int id = 0, receiptID = 0, intTemp = 0;
                                                string referenceNumber = "", docStatus = "", sapNumber = "", remarkss = "", receiptReference = "", finishWhse = "";
                                                bool IsConfirmed = false;
                                                DateTime dtTransDate = new DateTime(), dtReceiptDate = new DateTime(), dtTemp = new DateTime();
                                                foreach (var q in data)
                                                {
                                                    if (q.Key.Equals("id"))
                                                    {
                                                        id = Int32.TryParse(q.Value.ToString(), out intTemp) ? Convert.ToInt32(q.Value.ToString()) : intTemp;
                                                    }
                                                    else if (q.Key.Equals("transdate"))
                                                    {
                                                        string replaceT = q.Value.ToString().Replace("T", "");
                                                        dtTransDate = DateTime.TryParse(replaceT, out dtTemp) ? Convert.ToDateTime(replaceT) : dtTemp;
                                                    }
                                                    else if (q.Key.Equals("reference"))
                                                    {
                                                        referenceNumber = q.Value.ToString();
                                                        auto.Add(referenceNumber);
                                                    }
                                                    else if (q.Key.Equals("docstatus"))
                                                    {
                                                        docStatus = q.Value.ToString() == "O" ? "Open" : q.Value.ToString() == "N" ? "Cancelled" : q.Value.ToString() == "C" ? "Closed" : "";
                                                    }
                                                    else if (q.Key.Equals("sap_number"))
                                                    {
                                                        sapNumber = q.Value.ToString();
                                                    }
                                                    else if (q.Key.Equals("remarks"))
                                                    {
                                                        remarkss = q.Value.ToString();
                                                    }
                                                    if (q.Key.Equals("receipt_id"))
                                                    {
                                                        receiptID = Int32.TryParse(q.Value.ToString(), out intTemp) ? Convert.ToInt32(q.Value.ToString()) : intTemp;
                                                    }
                                                    else if (q.Key.Equals("receipt_ref"))
                                                    {
                                                        receiptReference = q.Value.ToString();
                                                    }
                                                    else if (q.Key.Equals("receipt_date"))
                                                    {
                                                        string replaceT = q.Value.ToString().Replace("T", "");
                                                        dtReceiptDate = DateTime.TryParse(replaceT, out dtTemp) ? Convert.ToDateTime(replaceT) : dtTemp;
                                                    }
                                                    else if (q.Key.Equals("mill"))
                                                    {
                                                        finishWhse = q.Value.ToString();
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(txtSearch.Text.ToString().Trim()))
                                                {
                                                    if (txtSearch.Text.ToString().Trim().ToLower().Contains(referenceNumber.ToLower()))
                                                    {
                                                        dgv.Rows.Add(id, dtTransDate.Equals(DateTime.MinValue) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), referenceNumber, docStatus, sapNumber, finishWhse, remarkss,receiptID, receiptReference, dtReceiptDate.Equals(DateTime.MinValue) ? "" : dtReceiptDate.ToString("yyyy-MM-dd HH:mm:ss"), docStatus.Equals("Open") ? "Close" : "");
                                                    }
                                                }
                                                else
                                                {
                                                    dgv.Rows.Add(id, dtTransDate.Equals(DateTime.MinValue) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), referenceNumber, docStatus, sapNumber, finishWhse, remarkss, receiptID, receiptReference, dtReceiptDate.Equals(DateTime.MinValue) ? "" : dtReceiptDate.ToString("yyyy-MM-dd HH:mm:ss"), docStatus.Equals("Open") ? "Close" : "");
                                                }
                                            }
                                        }
                                    }
                                }
                                txtSearch.AutoCompleteCustomSource = auto;
                            }
                            else
                            {
                                string msg = "No message response found";
                                foreach (var x in jObject)
                                {
                                    if (x.Key.Equals("message"))
                                    {
                                        msg = x.Value.ToString();
                                    }
                                }
                                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(response.Content.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //for(int i = 0; i < dgv.Rows.Count; i++)
                //{
                //    if(dgv.Rows[i].Cells["docstatus"].Value.ToString() == "Closed")
                //    {
                //        dgv.Rows[i].Cells["btnClosed"].Style.BackColor = Color.Firebrick;
                //        dgv.Rows[i].Cells["btnClosed"].Style.ForeColor = Color.White;
                //    }
                //}
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 11)
                    {
                        int id = 0, intTemp = 0;
                        id = int.TryParse(dgv.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
                        string remarksURL = "/api/production/issue_for_prod/remarks/";
                        string remarksByIdURL = "/api/production/issue_for_prod/remarks/get_by_id/";
                        if (id > 0)
                        {
                            QA_Remarks frm = new QA_Remarks(remarksURL, "", remarksByIdURL);
                            frm.selectedID = id;
                            frm.ShowDialog();
                        }
                        else
                        {
                            apic.showCustomMsgBox("Validation", "No ID Found!");
                        }
                    }
                }
            }
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches();
        }
    }
}
