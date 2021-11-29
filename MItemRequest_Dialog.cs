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
using RestSharp;
namespace AB
{
    public partial class MItemRequest_Dialog : Form
    {
        public MItemRequest_Dialog(JArray ja)
        {
            InitializeComponent();
            jaSelected = ja;
        }
        JArray jaSelected = new JArray();
        public static bool isSubmit = false;
        api_class apic = new api_class();
        private void MItemRequest_Dialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            MItemRequest_SelectedItems frm = new MItemRequest_SelectedItems(jaSelected);
            dtDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            showForm(frm);
        }

        public void showForm(Form form)
        {
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string hshdID = RandomString(20);
            if (string.IsNullOrEmpty(lblSelectedFromDept.Text.Trim()) || lblSelectedFromDept.Text == "N/A")
            {
                apic.showCustomMsgBox("Validation", "Please select From Department!");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    executeItemRequest(hshdID);
                }
            }
        }


        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789abcdefghijklmnñopqrstuvxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void executeItemRequest(string hashedID)
        {
            try
            {
                btnSubmit.Enabled = false;
                JObject joBody = new JObject();
                JObject joHeader = new JObject();
                joHeader.Add("transdate", DateTime.Now);
                joHeader.Add("remarks", txtRemarks.Text.Trim());
                joHeader.Add("duedate", dtDueDate.Text);
                //joHeader.Add("shift", Login.selectedShift);
                joHeader.Add("hashed_id", hashedID);
                joBody.Add("header", joHeader);
                JArray jaRows = new JArray();
                for (int i = 0; i < jaSelected.Count; i++)
                {
                    JObject joSelected = JObject.Parse(jaSelected[i].ToString());
                    JObject joRows = new JObject();
                    joRows.Add("item_code", joSelected["item_code"].IsNullOrEmpty() ? null : joSelected["item_code"].ToString());
                    joRows.Add("uom", joSelected["uom"].IsNullOrEmpty() ? null : joSelected["uom"].ToString());
                    joRows.Add("quantity", joSelected["quantity"].IsNullOrEmpty() ? (double?)null : Convert.ToDouble(joSelected["quantity"].ToString()));
                    joRows.Add("from_branch", lblSelectedFromDept.Text);
                    jaRows.Add(joRows);
                }
                joBody.Add("rows", jaRows);
                string sResult = apic.loadData("/api/inv/item_request/new", "", "application/json", joBody.ToString(), Method.POST, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = joResult["success"] == null ? false : (bool)joResult["success"];
                    string msg = joResult["success"] == null ? "" : joResult["message"].ToString();
                    if (isSuccess)
                    {
                        apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                        MItemRequest.jaSelected = new JArray();
                        jaSelected = new JArray();
                        isSubmit = true;
                        this.Invoke(new Action(delegate ()
                        {
                            btnSubmit.Enabled = true;
                            this.Hide();
                        }));
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                btnSubmit.Enabled = true;
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnFromDept_Click(object sender, EventArgs e)
        {
            try
            {
                string sPlant = "?plant=" + Login.jsonResult["data"]["plant"].ToString();
                string sParams = sPlant;
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/branch/get_all", sParams, "name", "code", false, false);
                frm.ShowDialog();
                this.Focus();
                lblSelectedFromDept.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
