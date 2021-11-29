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
    public partial class SystemReceive_Selected : Form
    {
        public SystemReceive_Selected(JArray jSelected, DataTable dt)
        {
            InitializeComponent();
            jaSelected = jSelected;
            gDt = dt;
        }
        JArray jaSelected = new JArray();
        DataTable gDt = new DataTable();
        api_class apic = new api_class();
        int intTemp = 0;
        public static bool isSubmit = false;
        BackgroundWorker bgSubmit = new BackgroundWorker();
        private void SystemReceive_Selected_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            SystemReceive_SelectedItems.isSubmit = false;
            SystemReceive_SelectedItems frm = new SystemReceive_SelectedItems(jaSelected);
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
            string hashID = RandomString(20);
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txtRemarks.Text.Trim().ToLower()))
                {
                    apic.showCustomMsgBox("Validation", "Remarks field is required!");
                }
                else if (jaSelected.Count <= 0)
                {
                    apic.showCustomMsgBox("Validation", "No Item Selected!");
                }
                else
                {
                    bgSubmit = new BackgroundWorker();
                    bgSubmit.DoWork += delegate
                    {
                        executeFG(hashID);
                    };
                    bgSubmit.RunWorkerCompleted += delegate
                    {
                        closeForm();
                    };
                    bg(bgSubmit);
                }
            }
        }

        public void bg(BackgroundWorker bgw)
        {
            if (!bgw.IsBusy)
            {
                closeForm();
                this.Cursor = Cursors.WaitCursor;
                btnSubmit.Enabled = false;
                bgw.RunWorkerAsync();
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

        public void executeFG(string hashedID)
        {
            try
            {
                string sRemarks = delegateControl(txtRemarks);
                JObject joBody = new JObject();
                JObject joHeader = new JObject();
                joHeader.Add("transdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                joHeader.Add("remarks", sRemarks);

                if (gDt.Rows.Count > 0)
                {
                    DataRow row = gDt.Rows[0];
                    if (row != null)
                    {
                        if (!row.IsNull("vessel"))
                        {
                            joHeader.Add("vessel", row["vessel"].ToString());
                        }
                        if (!row.IsNull("plate_num"))
                        {
                            joHeader.Add("plate_num", row["plate_num"].ToString());
                        }
                        if (!row.IsNull("plate_num"))
                        {
                            joHeader.Add("plate_num", row["plate_num"].ToString());
                        }
                        if (!row.IsNull("driver"))
                        {
                            joHeader.Add("driver", row["driver"].ToString());
                        }
                        if (!row.IsNull("agi_truck_scale"))
                        {
                            joHeader.Add("agi_truck_scale", row["agi_truck_scale"].ToString());
                        }
                        if (!row.IsNull("chti_truck_scale"))
                        {
                            joHeader.Add("chti_truck_scale", row["chti_truck_scale"].ToString());
                        }
                        if (!row.IsNull("from_whse"))
                        {
                            joHeader.Add("supplier", row["from_whse"].ToString());
                        }
                        if (!string.IsNullOrEmpty(Login.selectedShift.Trim()))
                        {
                            joHeader.Add("shift", Login.selectedShift);
                        }
                        if (!row.IsNull("transfer_id"))
                        {
                            int transferID = int.TryParse(row["transfer_id"].ToString(), out intTemp) ? Convert.ToInt32(row["transfer_id"].ToString()) : intTemp;
                            joHeader.Add("base_id", transferID);
                        }
                        joHeader.Add("hashed_id", hashedID);
                        joHeader.Add("transtype", "TRFR");
                    }
                }
                joBody.Add("header", joHeader);
                joBody.Add("details", jaSelected);
                Console.WriteLine(joBody);
                string sResult = apic.loadData("/api/inv/recv/new", "", "application/json", joBody.ToString(), Method.POST, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = joResult["success"] == null ? false : (bool)joResult["success"];
                    string msg = joResult["success"] == null ? "" : joResult["message"].ToString();
                    if (isSuccess)
                    {
                        SystemReceive.jaSelected = SystemReceive_SelectedReference.jaSelected = new JArray();
                        jaSelected = new JArray();
                        this.Invoke(new Action(delegate ()
                        {
                            isSubmit = true;
                            this.Hide();
                        }));
                    }
                    apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
