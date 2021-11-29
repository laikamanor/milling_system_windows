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
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;
namespace AB
{
    public partial class IssueForProd_Dialog : Form
    {
        public IssueForProd_Dialog(JArray jSelected, string type)
        {
            InitializeComponent();
            jaSelected = jSelected;
            gType = type;
        }
        api_class apic = new api_class();
        JArray jaSelected = new JArray();
        string gType = "";
        public int selectedID = 0;
        public static bool isSubmit = false;
        int intTemp = 0;
        BackgroundWorker bgSubmit = new BackgroundWorker();
        private void IssueForProd_Dialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            IssueForProdPacking_SelectedItems.isSubmit = false;
            IssueForProdPacking_SelectedItems frm = new IssueForProdPacking_SelectedItems(jaSelected, gType);
            frm.selectedID = selectedID;
            showForm(frm);
        }

        public void showForm(Form form)
        {
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        public void bg(BackgroundWorker bgg)
        {
            if (!bgg.IsBusy)
            {
                closeForm();
                this.Cursor = Cursors.WaitCursor;
                btnSubmit.Enabled = false;
                bgg.RunWorkerAsync();
            }
        }

        public void closeForm()
        {
            this.Cursor = Cursors.Default;
            btnSubmit.Enabled = true;
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

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789abcdefghijklmnñopqrstuvxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                double doubleTemp = 0.00;
                string hashedID = RandomString(20);
                if (string.IsNullOrEmpty(lblMill.Text.Trim()) || lblMill.Text == "N/A")
                {
                    MessageBox.Show("Mill field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblMill.Focus();
                }
                else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    MessageBox.Show("Remarks field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRemarks.Focus();
                }
                else
                {
                    bgSubmit = new BackgroundWorker();
                    bgSubmit.DoWork += delegate
                    {
                        executeFG(hashedID);
                    };
                    bgSubmit.RunWorkerCompleted += delegate
                    {
                        closeForm();
                    };
                    bg(bgSubmit);
                }
            }
        }

        public void executeFG(string hashedID)
        {
            try
            {
                string sRemarks = delegateControl(txtRemarks), sMill = delegateControl(lblMill);
                JObject joBody = new JObject();
                JObject joHeader = new JObject();
                joHeader.Add("transdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                joHeader.Add("remarks", sRemarks);
                if (!(sMill.Equals("N/A") || string.IsNullOrEmpty(sMill.Trim())))
                {
                    joHeader.Add("mill", sMill);
                }
                joBody.Add("header", joHeader);
                joHeader.Add("hashed_id", hashedID);
                joBody.Add("rows", jaSelected);
                Console.WriteLine(joBody);
                string sResult = apic.loadData("/api/production/issue_for_prod/new", "", "application/json", joBody.ToString(), Method.POST, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = joResult["success"] == null ? false : (bool)joResult["success"];
                    string msg = joResult["success"] == null ? "" : joResult["message"].ToString();
                    if (isSuccess)
                    {
                        apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                        IssueForProdPacking.jaSelected = new JArray();
                        jaSelected = new JArray();
                        isSubmit = true;
                        this.Invoke(new Action(delegate ()
                        {
                            this.Hide();
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnMill_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/mill/get_all", sParams, "name", "code", false, false);
                frm.ShowDialog();
                this.Focus();
                lblMill.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
