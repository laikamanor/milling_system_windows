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
using RestSharp;
namespace AB
{
    public partial class EditDriver : Form
    {
        public EditDriver(int id, string companyName)
        {
            InitializeComponent();
            selectedID = id;
            currentCompanyName = companyName;
        }
        api_class apic = new api_class();
        DataTable dtCompany = new DataTable();
        public static bool isSubmit = false;
        int selectedID = 0;
        string currentCompanyName = "";
        private void EditDriver_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg(backgroundWorker1);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
                {
                    MessageBox.Show("Full Name field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                }
                else if (string.IsNullOrEmpty(txtLicenseNumber.Text.Trim()))
                {
                    MessageBox.Show("License # field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLicenseNumber.Focus();
                }
                else if (string.IsNullOrEmpty(cmbCompany.Text.Trim()))
                {
                    MessageBox.Show("Company field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCompany.Focus();
                }
                else
                {
                    bg(backgroundWorker2);
                }
            }
        }

        public void bg(BackgroundWorker bgw)
        {
            if (!bgw.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgw.RunWorkerAsync();
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

        public string delegateControl(Control c)
        {
            string result = "";
            c.Invoke(new Action(delegate ()
            {
                result = c.Text;
            }));
            return result;
        }

        public void loadCompanies()
        {
            cmbCompany.Invoke(new Action(delegate ()
            {
                cmbCompany.Items.Clear();
            }));
            string sResult = apic.loadData("/api/driver/company/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtCompany = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtCompany.Rows)
                    {
                        cmbCompany.Invoke(new Action(delegate ()
                        {
                            cmbCompany.Items.Add(row["name"].ToString());
                        }));
                    }
                }
            }
            cmbCompany.Invoke(new Action(delegate ()
            {
                cmbCompany.Text = currentCompanyName;
            }));
        }

        public void updateDriver(string fullName, string licenseNumber, string companyName)
        {
            JObject joBody = new JObject();
            joBody.Add("fullname", fullName.Trim());
            joBody.Add("lic_no", licenseNumber.Trim());
            joBody.Add("company", companyName.Trim());
            string sResult = apic.loadData("/api/driver/update/", selectedID.ToString(), "application/json", joBody.ToString(), Method.PUT, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResult = JObject.Parse(sResult.Trim());
                bool isSuccess = false, boolTemp = false;
                isSuccess = isSubmit = joResult["success"] == null ? boolTemp : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
                apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                if (isSubmit)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        this.Hide();
                    }));
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            string sFullName = delegateControl(txtFullName), sLicenseNumber = delegateControl(txtLicenseNumber), sCompanyName = delegateControl(cmbCompany);
            updateDriver(sFullName, sLicenseNumber, sCompanyName);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadCompanies();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
    }
}
