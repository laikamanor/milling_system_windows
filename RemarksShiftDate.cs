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
    public partial class RemarksShiftDate : Form
    {
        public RemarksShiftDate(DataTable dt2,DataTable dt3)
        {
            InitializeComponent();
            gDt2 = dt2;
            gDt3 = dt3;
        }
        DataTable gDt2 = new DataTable(), gDt3 = new DataTable(), dtShift = new DataTable();
        api_class apic = new api_class();
        public static DateTime dtShiftDatee = new DateTime();
        public static string remarks = "";
        public static bool isSubmit = false;
        private void RemarksShiftDate_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtShiftDate.EditValue = DateTime.Now;
            viewToSaveInventory frm = new viewToSaveInventory(gDt3);
            showForm(frm);
            loadShifts();
        }

        public void loadShifts()
        {
            cmbShift.Properties.Items.Clear();
            string sResult = apic.loadData("/api/production/shift/get_all", "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                dtShift = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    foreach(DataRow row in dtShift.Rows)
                    {
                        cmbShift.Properties.Items.Add(row["code"].ToString());
                    }
                }
            }
            cmbShift.SelectedItem = -1;
        }

        public void showForm(Form form)
        {
            panel1.Controls.Clear();
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sRows2 = JsonConvert.SerializeObject(gDt3);
            JArray jaRows2 = JArray.Parse(sRows2);
            Console.WriteLine(jaRows2);
            if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                apic.showCustomMsgBox("Validation", "Remarks field is required!");
                txtRemarks.Focus();
            }
            else if (string.IsNullOrEmpty(cmbShift.Text.Trim()))
            {
                apic.showCustomMsgBox("Validation", "Shift field is required!");
                txtRemarks.Focus();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to save?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    btnSave.Enabled = false;
                    try
                    {
                        DateTime dtShiftt = new DateTime(), dateTemp = new DateTime();
                        dtShiftt = DateTime.TryParse(dtShiftDate.Text, out dateTemp) ? Convert.ToDateTime(dtShiftDate.Text) : new DateTime();
                        JObject joBody = new JObject();
                        joBody.Add("transdate", DateTime.Now);
                        joBody.Add("remarks", txtRemarks.Text.Trim());
                        joBody.Add("shift_date", dtShiftt);
                        joBody.Add("shift", cmbShift.Text);

                        if (gDt3.Columns.Contains("variance"))
                        {
                            var col = gDt3.Columns["variance"];
                            gDt3.Columns.Remove(col);
                        }
                        if (gDt3.Columns.Contains("row_index"))
                        {
                            var col = gDt3.Columns["row_index"];
                            gDt3.Columns.Remove(col);
                        }
                        string sRows = JsonConvert.SerializeObject(gDt3);
                        JArray jaRows = JArray.Parse(sRows);
                        joBody.Add("rows", jaRows);
                        string sResult = apic.loadData("/api/report/ending/inventory/shift/add", "", "application/json", joBody.ToString(), Method.POST, true);
                        if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                        {
                            JObject joResult = JObject.Parse(sResult);
                            Console.WriteLine("result: " + joResult);
                            bool isSuccess = false, boolTemp = false;
                            isSuccess = isSubmit = joResult["success"].IsNullOrEmpty() ? boolTemp : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                            string msg = joResult["message"].ToString();
                            if (isSuccess)
                            {
                                //foreach (DataColumn col in gDt2.Columns)
                                //{
                                //    Console.WriteLine(col.ColumnName);
                                //}
                                if (!gDt3.Columns.Contains("shift_date"))
                                {
                                    gDt3.Columns.Add("shift_date");
                                }
                                if (!gDt3.Columns.Contains("remarks"))
                                {
                                    gDt3.Columns.Add("remarks");
                                }
                                if (!gDt3.Columns.Contains("shift"))
                                {
                                    gDt3.Columns.Add("shift");
                                }
                                if (!gDt3.Columns.Contains("variance"))
                                {
                                    gDt3.Columns.Add("variance");
                                }
                                if (!gDt3.Columns.Contains("transact_by"))
                                {
                                    gDt3.Columns.Add("transact_by");
                                }
                                if (!gDt3.Columns.Contains("transdate"))
                                {
                                    gDt3.Columns.Add("transdate");
                                }

                                apic.showCustomMsgBox("Message", msg);
                                if (gDt2.Rows.Count > 0)
                                {
                                    DataRow row1 = gDt2.Rows[0];
                                    foreach (DataRow row in gDt3.Rows)
                                    {
                                        if (gDt3.Columns.Contains("transact_by"))
                                        {
                                            row["transact_by"] = row1["transact_by"];
                                        }
                                        if (gDt3.Columns.Contains("transdate"))
                                        {
                                            row["transdate"] = row1["transdate"];
                                        }
                                    }
                                }

                                foreach (DataRow row in gDt3.Rows)
                                {
                                    if (gDt3.Columns.Contains("shift_date"))
                                    {
                                        row["shift_date"] = dtShiftt.ToString("yyyy-MM-dd");
                                    }
                                    if (gDt3.Columns.Contains("remarks"))
                                    {
                                        row["remarks"] = txtRemarks.Text.Trim();
                                    }
                                    if (gDt3.Columns.Contains("shift"))
                                    {
                                        row["shift"] = cmbShift.Text;
                                    }
                                    if (gDt3.Columns.Contains("variance"))
                                    {
                                        double doubleTemp = 0.00;
                                        double endingBalance = row.IsNull("available") ? doubleTemp : double.TryParse(row["available"].ToString(), out doubleTemp) ? Convert.ToDouble(row["available"].ToString()) : doubleTemp;

                                        if (row.IsNull("actual_count"))
                                        {
                                            row["variance"] = null;
                                        }
                                        else
                                        {
                                            double actualCount = row.IsNull("actual_count") ? doubleTemp : double.TryParse(row["actual_count"].ToString(), out doubleTemp) ? Convert.ToDouble(row["actual_count"].ToString()) : doubleTemp;
                                            row["variance"] = actualCount - endingBalance;
                                        }
                                    }
                                }
                                this.Cursor = Cursors.Default;
                                btnSave.Enabled = true;
                                string currentUser = Login.jsonResult["data"]["username"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["username"].ToString();
                                crInventoryPerWhse frm = new crInventoryPerWhse(gDt3, currentUser);
                                frm.ShowDialog();
                                this.Hide();
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                btnSave.Enabled = true;
                                apic.showCustomMsgBox("Validation", msg);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;
                        btnSave.Enabled = true;
                        apic.showCustomMsgBox(ex.Message, ex.ToString());
                    }
                }
            }
        }
    }
}
