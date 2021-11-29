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
    public partial class manualReceive_Selected : Form
    {
        public manualReceive_Selected(JArray ja)
        {
            InitializeComponent();
            jaSelected = ja;
        }
        JArray jaSelected = new JArray();
        public static bool isSubmit = false;
        api_class apic = new api_class();
        private void manualReceive_Selected_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            manualReceive_selectedItems frm = new manualReceive_selectedItems(jaSelected);
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
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(lblSelectedFromWhse.Text.Trim()) || lblSelectedFromWhse.Text == "N/A")
                {
                    apic.showCustomMsgBox("Validation", "Please select From Warehouse!");
                }
                else if (string.IsNullOrEmpty(lblSelectedToWhse.Text.Trim()) || lblSelectedToWhse.Text == "N/A")
                {
                    apic.showCustomMsgBox("Validation", "Please select To Warehouse!");
                }
                else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    apic.showCustomMsgBox("Validation", "Remarks field is required!");
                }
                else if (jaSelected.Count <= 0)
                {
                    apic.showCustomMsgBox("Validation", "No selected item!");
                }
                else
                {
                    executeManualReceive(hshdID);
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

        public void executeManualReceive(string hashedID)
        {
            try
            {
                btnSubmit.Enabled = false;
                JObject joBody = new JObject();
                JObject joHeader = new JObject();
                joHeader.Add("transdate", DateTime.Now);
                joHeader.Add("transtype", "MNL");
                joHeader.Add("sap_number", null);
                joHeader.Add("remarks", txtRemarks.Text.Trim());
                joHeader.Add("reference2", null);
                joHeader.Add("supplier", null);
                joHeader.Add("type2", "N/A");
                if (lblSelectedTruck.Text.Trim() != "N/A")
                {
                    joHeader.Add("plate_num", lblSelectedTruck.Text);
                }
                if (lblSelectedVessel.Text.Trim() != "N/A")
                {
                    joHeader.Add("vessel", lblSelectedVessel.Text);
                }
                if (lblSelectedDriver.Text.Trim() != "N/A")
                {
                    joHeader.Add("driver", lblSelectedDriver.Text);
                }
                if (!string.IsNullOrEmpty(txtAGITS.Text.Trim()))
                {
                    joHeader.Add("agi_truck_scale", txtAGITS.Text);
                }
                if (!string.IsNullOrEmpty(txtCHTITS.Text.Trim()))
                {
                    joHeader.Add("chti_truck_scale", txtCHTITS.Text);
                }
                joHeader.Add("shift", Login.selectedShift);
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
                    joRows.Add("actualrec", joSelected["quantity"].IsNullOrEmpty() ? (double?)null : Convert.ToDouble(joSelected["quantity"].ToString()));
                    joRows.Add("from_whse", lblSelectedFromWhse.Text);
                    joRows.Add("to_whse", lblSelectedToWhse.Text);
                    jaRows.Add(joRows);
                }
                joBody.Add("details", jaRows);
                string sResult = apic.loadData("/api/inv/recv/new", "", "application/json", joBody.ToString(), Method.POST, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = joResult["success"] == null ? false : (bool)joResult["success"];
                    string msg = joResult["success"] == null ? "" : joResult["message"].ToString();
                    if (isSuccess)
                    {
                        apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                        manualReceive.jaSelected = new JArray();
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

        private void btnFromWhse_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/whse/get_all", sParams, "whsename", "whsecode", false, false);
                frm.ShowDialog();
                this.Focus();
                lblSelectedFromWhse.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnToWhse_Click(object sender, EventArgs e)
        {
            try
            {
                string currentBranch = Login.jsonResult["data"]["branch"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["branch"].ToString();
                string sParams = "?branch=" + currentBranch;
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/whse/get_all", sParams, "whsename", "whsecode", false, false);
                frm.ShowDialog();
                this.Focus();
                lblSelectedToWhse.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTruck_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/trucks/get_all", sParams, "plate_num", "plate_num", false, false);
                frm.ShowDialog();
                this.Focus();
                lblSelectedTruck.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVessel_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/vessel/get_all", sParams, "name", "name", false, false);
                frm.ShowDialog();
                this.Focus();
                lblSelectedVessel.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDriver_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/driver/get_all", sParams, "name", "name", false, false);
                frm.ShowDialog();
                this.Focus();
                lblSelectedDriver.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
