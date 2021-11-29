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
using Newtonsoft.Json;

namespace AB
{
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }
        utility_class utilityc = new utility_class();
        public static bool isSubmit = false;
        DataTable dtUomGroup = new DataTable(), dtUom = new DataTable();
        api_class apic = new api_class();
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtItemCode.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Item Code field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemCode.Focus();
            }
            else if (txtItemName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Item Name field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemName.Focus();
            }
            else if (cmbItemGroup.SelectedIndex == -1)
            {
                MessageBox.Show("UOM field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUom.Focus();
            }
            else if (cmbItemGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Item Group field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbItemGroup.Focus();
            }
            else if (cmbUomGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Uom Group field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUomGroup.Focus();
            }
            else
            {
                insertItem();
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

        public void insertItem()
        {
            api_class apic = new api_class();
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
                    //string branch = (cmbBranch.Text.Equals("") || cmbBranch.Text == "All" ? "" : cmbBranch.Text);
                    var request = new RestRequest("/api/item/new");
                    Console.WriteLine("/api/item/new");
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.POST;

                    JArray jArray = new JArray();
                    JObject jObject = new JObject();
                    jObject.Add("item_code", (txtItemCode.Text == String.Empty ? null : txtItemCode.Text));
                    jObject.Add("item_name", (txtItemName.Text == String.Empty ? null : txtItemName.Text));
                    jObject.Add("uom", apic.findValueInDataTable(dtUom, cmbUom.Text, "description", "code"));
                    jObject.Add("item_group", (cmbItemGroup.Text == String.Empty ? null : cmbItemGroup.Text));
                    jObject.Add("uom_group", apic.findValueInDataTable(dtUomGroup, cmbUomGroup.Text, "name", "id"));
                    jArray.Add(jObject);
                    Console.WriteLine("inserted: " + jArray);
                    request.AddParameter("application/json", jArray, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    Console.WriteLine("result: " + response.Content);
                    jObject = JObject.Parse(response.Content.ToString());
                    bool isSuccess = false;

                    string msg = "No message response found";
                    foreach (var x in jObject)
                    {
                        if (x.Key.Equals("message"))
                        {
                            msg = x.Value.ToString();
                        }
                    }

                    foreach (var x in jObject)
                    {
                        if (x.Key.Equals("success"))
                        {
                            isSuccess = Convert.ToBoolean(x.Value.ToString());
                            txtItemCode.Clear();
                            txtItemName.Clear();
                            cmbItemGroup.SelectedIndex = cmbUom.SelectedIndex = -1;
                            isSubmit = true;
                            MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                    }

                    if (!isSuccess)
                    {
                        if (msg.Equals("Token is invalid"))
                        {
                            MessageBox.Show("Your login session is expired. Please login again", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
&& !char.IsDigit(e.KeyChar)
&& e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadUom();
            loadUomGroup();
            loadItemGroups();
        }

        public void loadItemGroups()
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
            api_class apic = new api_class();
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
    }
}
