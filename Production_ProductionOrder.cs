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
    public partial class Production_ProductionOrder : Form
    {
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        warehouse_class warehousec = new warehouse_class();
        branch_class branchc = new branch_class();
        DataTable dtStatus = new DataTable();
        DataTable dtWarehouse = new DataTable();
        DataTable dtBranches = new DataTable();
        string gType = "";
        public Production_ProductionOrder(string type)
        {
            gType = type;
            InitializeComponent();
        }

        private void Production_ProductionOrder_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            checkToDate.Checked = true;
            dtFromDate.Value = dtToDate.Value = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            dtStatus.Columns.Add("data");
            loadBranches();
            loadWarehouse();
            loadData();
            loadSearch();
        }

        public async void loadBranches()
        {
            int isAdmin = 0;
            string branch = "";
            dtBranches = await branchc.returnBranches();
            cmbBranches.Items.Clear();
            if (Login.jsonResult != null)
            {
                foreach (var x in Login.jsonResult)
                {
                    if (x.Key.Equals("data"))
                    {
                        JObject jObjectData = JObject.Parse(x.Value.ToString());
                        foreach (var y in jObjectData)
                        {
                            if (y.Key.Equals("branch"))
                            {
                                branch = y.Value.ToString();
                            }
                            else if (y.Key.Equals("isAdmin"))
                            {

                                if (y.Value.ToString().ToLower() == "false" || y.Value.ToString() == "")
                                {
                                    foreach (DataRow row in dtBranches.Rows)
                                    {
                                        if (row["code"].ToString() == branch)
                                        {
                                            cmbBranches.Items.Add(row["name"].ToString());
                                            if (cmbBranches.Items.Count > 0)
                                            {
                                                cmbBranches.SelectedIndex = 0;
                                            }
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    isAdmin += 1;
                                    break;
                                }
                            }
                            else if (y.Key.Equals("isAccounting"))
                            {
                                if (y.Value.ToString().ToLower() == "false" || y.Value.ToString() == "")
                                {
                                    foreach (DataRow row in dtBranches.Rows)
                                    {
                                        if (row["code"].ToString() == branch && isAdmin <= 0)
                                        {
                                            cmbBranches.Items.Add(row["name"].ToString());
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (cmbBranches.Items.Count <= 0)
                {
                    cmbBranches.Items.Add("All");
                    foreach (DataRow row in dtBranches.Rows)
                    {
                        cmbBranches.Items.Add(row["name"]);
                    }
                }
            }
            if (cmbBranches.Items.Count > 0)
            {
                string branchName = "";
                foreach (DataRow row in dtBranches.Rows)
                {
                    if (row["code"].ToString() == branch)
                    {
                        branchName = row["name"].ToString();
                        break;
                    }
                }
                cmbBranches.SelectedIndex = cmbBranches.Items.IndexOf(branchName);
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

        public async void loadWarehouse()
        {
            string branchCode = "";
            string warehouse = "";
            cmbWarehouse.Items.Clear();
            cmbWarehouse.Items.Add("All-Good");
            foreach (DataRow row in dtBranches.Rows)
            {
                if (cmbBranches.Text.Equals(row["name"].ToString()))
                {
                    branchCode = row["code"].ToString();
                    break;
                }
            }
            dtWarehouse = await warehousec.returnWarehouse(branchCode, string.IsNullOrEmpty(branchCode.Trim()) ? "?" : "&" + "is_production=1" + "is_production=1");
            foreach (DataRow row in dtWarehouse.Rows)
            {
                cmbWarehouse.Items.Add(row["whsename"]);
            }
            if (cmbWarehouse.Items.Count > 0)
            {
                string whseName = "";
                foreach (DataRow row in dtWarehouse.Rows)
                {
                    if (row["whsecode"].ToString() == warehouse)
                    {
                        whseName = row["whsename"].ToString();
                        break;
                    }
                }
                cmbWarehouse.SelectedIndex = cmbWarehouse.Items.IndexOf(whseName);
                if (cmbWarehouse.Text == "")
                {
                    cmbWarehouse.SelectedIndex = 0;
                }
            }
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }


        public void apiPUT(JObject body, string URL)
        {
            if (Login.jsonResult != null)
            {
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
                    var request = new RestRequest(URL);
                    Console.WriteLine(URL);
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.PUT;

                    Console.WriteLine(body);
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    if (response.ErrorMessage == null)
                    {
                        if (response.Content.Substring(0, 1).Equals("{"))
                        {
                            JObject jObjectResponse = JObject.Parse(response.Content);
                            bool isSubmit = false;
                            foreach (var x in jObjectResponse)
                            {
                                if (x.Key.Equals("success"))
                                {
                                    isSubmit = string.IsNullOrEmpty(x.Value.ToString()) ? false : Convert.ToBoolean(x.Value.ToString());
                                    break;
                                }
                            }

                            string msg = "No message response found";
                            foreach (var x in jObjectResponse)
                            {
                                if (x.Key.Equals("message"))
                                {
                                    msg = x.Value.ToString();
                                }
                            }
                            MessageBox.Show(msg, "", MessageBoxButtons.OK, isSubmit ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(response.Content.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        private async void cmbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadWarehouse();
        }


        public void loadSearch()
        {
            cmbStatus.Items.Clear();
            if (dtStatus.Rows.Count > 0)
            {
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("All");
                DataView view = new DataView(dtStatus);
                DataTable distinctValues = view.ToTable(true, "data");
                foreach (DataRow row in distinctValues.Rows)
                {
                    cmbStatus.Items.Add(row["data"].ToString());
                }
                cmbStatus.SelectedIndex = 0;
            }
        }
        public void loadData()
        {
            string sBranch = "?branch=" + findCode(cmbBranches.Text, "Branch");
            string sWarehouse = "&whsecode=" + findCode(cmbWarehouse.Text, "Warehouse");
            string sDate = !checkDate.Checked ? "&from_date=" : "&from_date=" + dtFromDate.Value.ToString("yyyy-MM-dd");
            sDate += !checkToDate.Checked ? "&to_date=" : "&to_date=" + dtToDate.Value.ToString("yyyy-MM-dd");

            //di pa nalalagay
            string sFromTime = "&from_time=" + cmbFromTime.Text;
            string sToTime = "&to_time=" + cmbToTime.Text;
            string sParams = sBranch + sWarehouse + sDate + sFromTime + sToTime;
            string sResult = apic.loadData("/api/production/order/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = (bool)joResult["success"];
                    string msg = joResult["message"].ToString();
                    if (isSuccess)
                    {
                        JArray jaData = (JArray)joResult["data"];
                        DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                        gridControl1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
