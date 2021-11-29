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
namespace AB
{
    public partial class SalesPerCustomer_PaidDetails : Form
    {
        public SalesPerCustomer_PaidDetails()
        {
            InitializeComponent();
        }
        public int selectedID = 0;
        public string selectedCustCode = "", selectedReference = "";
        utility_class utilityc = new utility_class();
        private void SalesPerCustomer_PaidDetails_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dgv.Columns["doc_total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["balance_due"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["total_payment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            lblCustomerCode.Text = selectedCustCode;
            lblReference.Text = selectedReference;
            loadData();
        }

        public void loadData()
        {
            dgv.Rows.Clear();
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
                    bool isSuccess = false;
                    var client = new RestClient(utilityc.URL);
                    client.Timeout = -1;
                    var request = new RestRequest("/api/payment/paidsales/" + selectedID);
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.GET;
                    var response = client.Execute(request);
                    if (response.ErrorMessage == null)
                    {
                        if (response.Content.ToString().Substring(0, 1).Equals("{"))
                        {
                            JObject jObject = JObject.Parse(response.Content.ToString());
                            foreach (var x in jObject)
                            {
                                if (x.Key.Equals("success"))
                                {
                                    if (Convert.ToBoolean(x.Value.ToString()))
                                    {
                                        isSuccess = true;
                                        break;
                                    }
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
                                                JObject jObjectData = JObject.Parse(jsonArray[i].ToString());
                                                string reference = "", custCode = "";
                                                DateTime dtTransDate = new DateTime();
                                                double docTotal = 0.00, balanceDue = 0.00, totalPayment = 0.00;
                                                foreach (var y in jObjectData)
                                                {
                                                    if (y.Key.Equals("reference"))
                                                    {
                                                        reference = y.Value.ToString();
                                                    }
                                                    else if (y.Key.Equals("transdate"))
                                                    {
                                                        dtTransDate = Convert.ToDateTime(y.Value.ToString());
                                                    }
                                                    else if (y.Key.Equals("cust_code"))
                                                    {
                                                        custCode = y.Value.ToString();
                                                    }
                                                    else if (y.Key.Equals("doctotal"))
                                                    {
                                                        docTotal = Convert.ToDouble(y.Value.ToString());
                                                    }
                                                    else if (y.Key.Equals("balance_due"))
                                                    {
                                                        balanceDue = Convert.ToDouble(y.Value.ToString());
                                                    }
                                                    else if (y.Key.Equals("total_payment"))
                                                    {
                                                        totalPayment = Convert.ToDouble(y.Value.ToString());
                                                    }
                                                }
                                                dgv.Rows.Add(reference, dtTransDate.ToString("yyyy-MM-dd"), custCode, Convert.ToDecimal(string.Format("{0:0.00}", docTotal)), Convert.ToDecimal(string.Format("{0:0.00}", balanceDue)), Convert.ToDecimal(string.Format("{0:0.00}", totalPayment)));
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show(response.Content.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            JObject jObject = JObject.Parse(response.Content.ToString());
                            string msg = "";
                            foreach (var x in jObject)
                            {
                                if (x.Key.Equals("message"))
                                {
                                    msg = x.Value.ToString();
                                }
                            }
                            if (msg.Equals("Token is invalid"))
                            {
                                MessageBox.Show("Your login session is expired. Please login again", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }
    }
}
