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
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AB
{
    public partial class Production_ReceivedProduction : Form
    {
        public Production_ReceivedProduction(string type)
        {
            gType = type;
            InitializeComponent();
        }
        string gType = "";
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        branch_class branchc = new branch_class();
        warehouse_class warehousec = new warehouse_class();
        DataTable dtWarehouse = new DataTable(), dtBranches = new DataTable(), dtPlant = new DataTable();
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
                    if (e.ColumnIndex == 2)
                    {
                        Production_ReceivedProduction_Items items = new Production_ReceivedProduction_Items(gType);
                        items.selectedID = string.IsNullOrEmpty(dgv.CurrentRow.Cells["id"].Value.ToString()) ? 0 : Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value.ToString());
                        items.reference = dgv.CurrentRow.Cells["reference"].Value.ToString();
                        items.ShowDialog();
                        if (Production_ReceivedProduction_Items.isSubmit)
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

        private void Production_ReceivedProduction_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            checkToDate.Checked = true;
            //dgv.Columns["btnViewRemarks"].Visible = gType.Equals("Closed");
            dtFromDate.Visible = false;
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
            if (typee.Equals("Warehouse"))
            {
                foreach (DataRow row in dtWarehouse.Rows)
                {
                    if (row["whsename"].ToString() == value)
                    {
                        result = row["whsecode"].ToString();
                        break;
                    }
                }
            }
            else
            {
                foreach (DataRow row in dtBranches.Rows)
                {
                    if (row["name"].ToString() == value)
                    {
                        result = row["code"].ToString();
                        break;
                    }
                }
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches();
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
                    Console.WriteLine(gType);
                    string sDocStatus =  "?docstatus=" + (gType.Equals("Open") ? "O" : gType.Equals("Closed") ? "C" : gType.Equals("Cancelled") ? "N" : "");
                    string sBranch = "&branch=" + findCode(cmbBranch.Text, "Branch");
                    string sDate = !checkDate.Checked ? "&from_date=" : "&from_date=" + dtFromDate.Value.ToString("yyyy-MM-dd");
                    sDate += !checkToDate.Checked ? "&to_date=" : "&to_date=" + dtToDate.Value.ToString("yyyy-MM-dd");

                    //di pa nalalagay
                    string sFromTime = "&from_time=" + cmbFromTime.Text;
                    string sToTime = "&to_time=" + cmbToTime.Text;

                    var request = new RestRequest("/api/production/rec_from_prod/get_all" + sDocStatus + sBranch  + sDate + sFromTime + sToTime);
                    Console.WriteLine("/api/production/rec_from_prod/get_all" + sDocStatus + sBranch  + sDate + sFromTime + sToTime);
                    request.AddHeader("Authorization", "Bearer " + token);
                    var response = client.Execute(request);
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
                                                int id = 0;
                                                string referenceNumber = "", docStatus = "", sapNumber = "", remarkss = "";
                                                    //, sIsConfirmed = "", prodReference = "";
                                                //bool IsConfirmed = false;
                                                DateTime dtTransDate = new DateTime();
                                                foreach (var q in data)
                                                {
                                                    if (q.Key.Equals("id"))
                                                    {
                                                        id = Convert.ToInt32(q.Value.ToString());
                                                    }
                                                    else if (q.Key.Equals("transdate"))
                                                    {
                                                        string replaceT = q.Value.ToString().Replace("T", "");
                                                        dtTransDate = Convert.ToDateTime(replaceT);
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
                                                    //else if (q.Key.Equals("confirm"))
                                                    //{
                                                    //    IsConfirmed = (q.Value.ToString().Trim() == "" ? false : Convert.ToBoolean(q.Value.ToString()));
                                                    //    sIsConfirmed = (IsConfirmed ? "✔ " : "");
                                                    //}
                                                    //else if (q.Key.Equals("prod_order_ref"))
                                                    //{
                                                    //    prodReference = q.Value.ToString();
                                                    //}
                                                }
                                                if (!string.IsNullOrEmpty(txtSearch.Text.ToString().Trim()))
                                                {
                                                    
                                                    if (txtSearch.Text.ToString().Trim().ToLower().Contains(referenceNumber.ToLower()))
                                                    {
                                                        dgv.Rows.Add(id, dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), referenceNumber, docStatus, sapNumber, remarkss,docStatus.Equals("Open") ? "Close" : "");
                                                    }
                                                }
                                                else
                                                {
                                                    dgv.Rows.Add(id, dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), referenceNumber, docStatus, sapNumber, remarkss, docStatus.Equals("Open") ? "Close" : "");
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
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
