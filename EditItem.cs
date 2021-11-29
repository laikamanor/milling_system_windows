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
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AB
{
    public partial class EditItem : Form
    {
        public EditItem(int id)
        {
            InitializeComponent();
            selectedID = id;
        }
        public int selectedID = 0;
        api_class apic = new api_class();
        DataTable dtUomGroup = new DataTable(), dtUom = new DataTable();
        public static bool isSubmit = false;
        private void EditItem_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadUom();
            loadItemGroups();
            loadUomGroup();
            loadData();
        }

        public void loadItemGroups()
        {
            utility_class utilityc = new utility_class();
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
                    var request = new RestRequest("/api/item/item_grp/getall");
                    request.AddHeader("Authorization", "Bearer " + token);
                    var response = client.Execute(request);
                    JObject jObject = new JObject();
                    jObject = JObject.Parse(response.Content.ToString());
                    cmbItemGroup.Items.Clear();
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
                                        string code = "";
                                        foreach (var q in data)
                                        {
                                            if (q.Key.Equals("code"))
                                            {
                                                code = q.Value.ToString();
                                            }
                                        }
                                        cmbItemGroup.Items.Add(code);
                                    }
                                }
                            }
                        }
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
                Cursor.Current = Cursors.Default;
            }
        }

        public void loadUomGroup()
        {
            string sResult = apic.loadData("/api/uom_grp/get_all", "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                dtUomGroup = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                foreach (DataRow row in dtUomGroup.Rows)
                {
                    cmbUomGroup.Items.Add(row["name"].ToString());
                }
            }
        }

        public void loadUom()
        {
            string sResult = apic.loadData("/api/item/uom/getall", "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                dtUom = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                foreach (DataRow row in dtUom.Rows)
                {
                    cmbUom.Items.Add(row["description"].ToString());
                }
            }
        }

        public void loadData()
        {
            string sResult =  apic.loadData("/api/item/getdetail/", selectedID.ToString(), "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResult = JObject.Parse(sResult);
                JObject joData = joResult["data"] == null ? new JObject() : JObject.Parse(joResult["data"].ToString());
                txtItemCode.Text = joData["item_code"] == null ? "" : joData["item_code"].ToString();
                txtItemName.Text = joData["item_name"] == null ? "" : joData["item_name"].ToString();
                cmbUom.Text = joData["uom"] == null ? "" : apic.findValueInDataTable(dtUom, joData["uom"].ToString(), "description", "code");
                int uomGroupID = 0, intTemp = 0;
                uomGroupID = joData["uom_group"] == null ? 0 : int.TryParse(joData["uom_group"].ToString(), out intTemp) ? Convert.ToInt32(joData["uom_group"].ToString()) : intTemp;
                cmbUomGroup.Text = apic.findValueInDataTable(dtUomGroup, uomGroupID.ToString(), "id", "name");
                cmbItemGroup.Text = joData["item_group"] == null ? "" : joData["item_group"].ToString();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
             if (txtItemName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Item Name field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemName.Focus();
            }
            else if (cmbUom.SelectedIndex == -1)
            {
                MessageBox.Show("UOM field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUom.Focus();
            }
            else if (cmbUomGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Uom Group field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUomGroup.Focus();
            }
            else if (cmbItemGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Item Group field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbItemGroup.Focus();
            }

            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    JObject joBody = new JObject();
                    joBody.Add("item_code", "");
                    joBody.Add("item_name", txtItemName.Text);
                    joBody.Add("uom", apic.findValueInDataTable(dtUom, cmbUom.Text, "description", "code"));
                    joBody.Add("item_group", cmbItemGroup.Text);
                    joBody.Add("uom_group", apic.findValueInDataTable(dtUomGroup, cmbUomGroup.Text, "name", "id"));
                    string sResult = apic.loadData("/api/item/update/", selectedID.ToString(), "application/json", joBody.ToString(), RestSharp.Method.PUT, true);
                    if (!string.IsNullOrEmpty(sResult.Trim()))
                    {
                        if (sResult.StartsWith("{"))
                        {
                            JObject joResult = JObject.Parse(sResult);
                            bool isSuccess = isSubmit = (bool)joResult["success"];
                            string msg = joResult["message"].ToString();
                            MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                            if (isSuccess)
                            {
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
