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
namespace AB
{
    public partial class AddVessel : Form
    {
        public AddVessel()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        public static bool isSubmit = false;
        private void AddVessel_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    txtName.Focus();
                    MessageBox.Show("Name field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bg();
                }
            }
        }

        public void insertVessel(string vesselName)
        {
            JObject joBody = new JObject();
            joBody.Add("name", vesselName.Trim());
            string sResult = apic.loadData("/api/vessel/add", "", "application/json", joBody.ToString(), Method.POST, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResult = JObject.Parse(sResult.Trim());
                bool isSuccess = false, boolTemp = false;
                isSuccess = isSubmit = joResult["success"] == null ? boolTemp : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
                apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                if (isSubmit)
                {
                    this.Hide();
                }
            }
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                backgroundWorker1.RunWorkerAsync();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            txtName.Invoke(new Action(delegate ()
            {
                insertVessel(txtName.Text);
            }));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
    }
}
