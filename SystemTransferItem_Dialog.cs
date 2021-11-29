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
using Newtonsoft.Json.Linq;

namespace AB
{
    public partial class SystemTransferItem_Dialog : Form
    {
        public SystemTransferItem_Dialog(JArray jSelected)
        {
            InitializeComponent();
            jaSelected = jSelected;
        }
        api_class apic = new api_class();
        JArray jaSelected = new JArray();
        BackgroundWorker bgSubmit = new BackgroundWorker();
        private void SystemTransferItem_Dialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            SystemTransferItem_Selected frm = new SystemTransferItem_Selected(jaSelected);
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
            Console.WriteLine(jaSelected);
            string hashedID = RandomString(20);
            if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                MessageBox.Show("Remarks field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRemarks.Focus();
            }
            else
            {
                bgSubmit.DoWork += delegate
                {
                    executeTransfer(hashedID);
                };
                bgSubmit.RunWorkerCompleted += delegate
                {
                    closeForm();
                };
                bg(bgSubmit);
            }
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789abcdefghijklmnñopqrstuvxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void bg(BackgroundWorker bgg)
        {
            if (!bgg.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgg.RunWorkerAsync();
            }
        }


        private void btnTruck_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/trucks/get_all", sParams, "plate_num", "plate_num",true, false);
                frm.ShowDialog();

                this.Focus();
                lblTruck.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string delegateControl(Control c)
        {
            string result = "";
            c.Invoke(new Action(delegate ()
            {
                result = c.Text;
            }));
            return result;
        }

        public void executeTransfer(string hashedID)
        {
            try
            {
                string sRemarks = delegateControl(txtRemarks), sPlateNum = delegateControl(lblTruck), sShift = Login.selectedShift, sDriver = delegateControl(lblDriver), agiTS = delegateControl(txtAGITS), chtiTS = delegateControl(txtCHTITS);

                JObject joBody = new JObject();
                JObject joHeader = new JObject();
                joHeader.Add("transdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                joHeader.Add("remarks", sRemarks);
                joHeader.Add("plate_num", sPlateNum.Equals("N/A") || string.IsNullOrEmpty(sPlateNum.Trim()) ? null : sPlateNum);
                joHeader.Add("shift", sShift);
                joHeader.Add("agi_truck_scale", agiTS);
                joHeader.Add("chti_truck_scale", chtiTS);
                joHeader.Add("hashed_id", hashedID);
                joBody.Add("header", joHeader);
                joBody.Add("details", jaSelected);
                string sResult = apic.loadData("/api/inv/trfr/new", "", "application/json", joBody.ToString(), Method.POST, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = joResult["success"] == null ? false : (bool)joResult["success"];
                    string msg = joResult["success"] == null ? "" : joResult["message"].ToString();
                    if (isSuccess)
                    {
                        MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        SystemTransferItem.jaSelected = new JArray();
                        jaSelected = new JArray();
                        bg(bgSubmit);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void closeForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Loading")
                {
                    frm.Hide();
                }
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
                lblDriver.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
