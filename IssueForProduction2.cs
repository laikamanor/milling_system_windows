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
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using AB.API_Class;

namespace AB
{
    public partial class IssueForProduction2 : Form
    {
        public IssueForProduction2(string docStatus, string tab)
        {
            InitializeComponent();
            gDocStatus = docStatus;
            gTab = tab;
        }
        string gDocStatus = "", gTab = "";
        api_class apic = new api_class();
        DataTable dtPlant = new DataTable(), dtAssignDept = new DataTable();
        int currentRowIndex = 0, currentColorIndex = 0;
        color_class colorc = new color_class();
        private void IssueForProduction2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtFromDate.Visible = false;
            checkToDate.Checked = true;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            loadPlant();
            bg();
        }


        public void loadPlant()
        {
            cmbPlant.Properties.Items.Clear();
            dtPlant = new DataTable();
            string sResult = apic.loadData("/api/plant/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                }
            }

            string[] lists = { "isAdmin", "isSuperAdmin" };
            if (dtPlant.Rows.Count > 0)
            {
                if (apic.haveAccess(lists))
                {
                    cmbPlant.Properties.Items.Add("All");
                    foreach (DataRow row in dtPlant.Rows)
                    {
                        cmbPlant.Properties.Items.Add(row["name"].ToString());
                    }
                    string plantCode = Login.jsonResult["data"]["plant"].ToString();
                    string plantName = apic.findValueInDataTable(dtPlant, plantCode, "code", "name");
                    cmbPlant.SelectedIndex = cmbPlant.Properties.Items.IndexOf(plantName) <= 0 ? 0 : cmbPlant.Properties.Items.IndexOf(plantName);
                }
                else
                {
                    string plantCode = Login.jsonResult["data"]["plant"].ToString();
                    string plantName = apic.findValueInDataTable(dtPlant, plantCode, "code", "name");
                    cmbPlant.Properties.Items.Add(plantName);
                    cmbPlant.SelectedIndex = cmbPlant.Properties.Items.Count > 0 ? 0 : -1;
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
            loadData();
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void grd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         try
            {
                var col = grd.Columns["reference"];
                var col2 = grd.Columns["view_remarks"];
                string docStatusDecode = gDocStatus.Equals("O") ? "Open" : gDocStatus.Equals("C") ? "Closed" : gDocStatus.Equals("N") ? "Cancelled" : "";
                int intTemp = 0;
                int id = grd.CurrentRow.Cells["id"].Value == null ? 0 : Int32.TryParse(grd.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
                if (col.Index == e.ColumnIndex)
                {
                    string reference = grd.CurrentRow.Cells["reference"].Value.ToString();
                    Production_IssueProduction_Items.isSubmit = false;
                    Production_IssueProduction_Items items = new Production_IssueProduction_Items(docStatusDecode);
                    items.selectedID = id;
                    items.reference = reference;
                    items.ShowDialog();
                    if (Production_IssueProduction_Items.isSubmit)
                    {
                        loadData();
                    }
                }
                else if (col2.Index == e.ColumnIndex)
                {
                    string remarksURL = "/api/production/issue_for_prod/remarks/";
                    string remarksByIdURL = "/api/production/issue_for_prod/remarks/get_by_id/";
                    QA_Remarks frm = new QA_Remarks(remarksURL, "", remarksByIdURL);
                    frm.selectedID = id;
                    frm.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                apic.showCustomMsgBox("Validation", ex.ToString());
            }
        }

        private void grd_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Close This Transaction",close_Click));
                int currentMouseOverColumn = grd.HitTest(e.X, e.Y).ColumnIndex;
                var col = grd.Columns["reference"];
                if(col.Index == currentMouseOverColumn && gDocStatus.Equals("O"))
                {
                    //int currentMouseOverRow = grd.HitTest(e.X, e.Y).RowIndex;
                    m.Show(grd, new Point(e.X, e.Y));
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            int intTemp = 0;
            int id = grd.CurrentRow.Cells["id"].Value == null ? 0 : Int32.TryParse(grd.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
            Remarks.isSubmit = false;
            Remarks frm = new Remarks();
            frm.Text = "Add Remarks (" + grd.CurrentRow.Cells["reference"].Value.ToString() + ")";
            frm.ShowDialog();
            if (Remarks.isSubmit)
            {
                JObject joBody = new JObject();
                joBody.Add("remarks", Remarks.rem);
                apiPUT(joBody, "/api/production/issue_for_prod/cancel/" + id.ToString());
            }
        }

        public void apiPUT(JObject body, string URL)
        {
            utility_class utilityc = new utility_class();
            if (Login.jsonResult != null)
            {
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
                    var request = new RestRequest(URL);
                    Console.WriteLine(URL);
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.PUT;
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    if (response.ErrorMessage == null)
                    {
                        bool boolTemp = false;
                        if (!string.IsNullOrEmpty(response.Content.Trim()))
                        {
                            if (response.Content.Substring(0, 1).Equals("{"))
                            {
                                JObject jObjectResponse = JObject.Parse(response.Content);
                                bool isSubmit = jObjectResponse["success"].IsNullOrEmpty() ? false : bool.TryParse(jObjectResponse["success"].ToString(), out boolTemp) ? Convert.ToBoolean(jObjectResponse["success"].ToString()) : boolTemp;

                                string msg = "No message response found";
                                foreach (var x in jObjectResponse)
                                {
                                    if (x.Key.Equals("message"))
                                    {
                                        msg = x.Value.ToString();
                                    }
                                }
                                MessageBox.Show(msg, "", MessageBoxButtons.OK, isSubmit ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                                if (isSubmit)
                                {
                                    bg();
                                }
                            }
                            else
                            {
                                MessageBox.Show(response.Content, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }
        int OldRow = 0;
        private void grd_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = grd.HitTest(e.X, e.Y);

            if (hti.RowIndex >= 0 && hti.RowIndex != OldRow)
            {
                grd.Rows[OldRow].Selected = false;
                grd.Rows[hti.RowIndex].Selected = true;
                OldRow = hti.RowIndex;
            }
        }

        public void loadData()
        {
            grd.Invoke(new Action(delegate ()
            {
                grd.Columns.Clear();
                grd.DataSource = null;
            }));

            string sPlant = "?plant=", sDate = "", sBranch = "", sFromTime = "", sToTime = "", sDocStatus = "&docstatus=" + gDocStatus, sTab = "&tab=" + gTab;
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));
            dtFromDate.Invoke(new Action(delegate ()
            {
                sDate = !checkDate.Checked ? "&from_date=" : "&from_date=" + dtFromDate.Value.ToString("yyyy-MM-dd");
            }));
            dtToDate.Invoke(new Action(delegate ()
            {
                sDate += !checkToDate.Checked ? "&to_date=" : "&to_date=" + dtToDate.Value.ToString("yyyy-MM-dd");
            }));
            cmbFromTime.Invoke(new Action(delegate ()
            {
                sFromTime = "&from_time=" + cmbFromTime.Text;
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime = "&to_time=" + cmbToTime.Text;
            }));
            string sParams = sPlant + sBranch + sDate + sFromTime + sToTime + sDocStatus + sTab;
            string sResult = apic.loadData("/api/production/issue_for_prod/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                //Console.WriteLine(jaData);
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));

                if (dtData.Rows.Count > 0)
                {
                    DataTable dtCloned = dtData.Clone();
                    dtCloned.Columns["receipt_quantity"].DataType = typeof(double);
                    dtCloned.Columns["fg_quantity"].DataType = typeof(double);
                    foreach (DataRow row in dtData.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }
                    dtData = dtCloned;
                }
                DateTime dtTemp = new DateTime();
                double doubleTemp = 0.00;
                int intTemp = 0;
                dtData.SetColumnsOrder("transdate", "reference", "mill", "gr_num", "remarks", "created_by","receipt_date", "receipt_ref", "receipt_item", "receipt_quantity","receipt_by", "fg_item", "fg_quantity", "fg_uom");
                if (IsHandleCreated)
                {
                    grd.Invoke(new Action(delegate ()
                    {
                        if (dtData.Rows.Count > 0)
                        {
                            grd.DataSource = dtData;
                            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                            btnCol.Name = "view_remarks";
                            btnCol.UseColumnTextForButtonValue = true;
                            btnCol.FlatStyle = FlatStyle.Flat;
                            btnCol.DefaultCellStyle.BackColor = Color.FromArgb(120, 199, 245);
                            btnCol.HeaderText = "View Remarks";
                            btnCol.Text = "View Remarks";
                            foreach (DataGridViewColumn col in grd.Columns)
                            {
                                string fieldName = col.Name;
                                string v = col.HeaderText;
                                string s = v.Replace("_", " ");
                                col.HeaderText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());

                                col.DefaultCellStyle.Format = fieldName.Equals("transdate") || fieldName.Equals("receipt_date") ? "yyyy-MM-dd HH:mm:ss" : fieldName.Equals("receipt_quantity") || fieldName.Equals("fg_quantity") ? "n3" : "";

                                if (gTab.Equals("CLEAN WHEAT") || gTab.Equals("FEEDBACK"))
                                {
                                    col.Visible = fieldName.Equals("transdate") || fieldName.Equals("reference") || fieldName.Equals("mill") || fieldName.Equals("remarks") || fieldName.Equals("receipt_date") || fieldName.Equals("receipt_ref") || fieldName.Equals("receipt_item") || fieldName.Equals("receipt_quantity") || fieldName.Equals("receipt_by") || fieldName.Equals("gr_num") || fieldName.Equals("created_by");
                                }
                                else
                                {
                                    col.Visible = fieldName.Equals("transdate") || fieldName.Equals("reference") || fieldName.Equals("mill") || fieldName.Equals("remarks") || fieldName.Equals("fg_item") || fieldName.Equals("fg_quantity") || fieldName.Equals("fg_uom") || fieldName.Equals("gr_num") || fieldName.Equals("created_by");
                                }
                       
                            }

                            if (dtData.Rows.Count > 0 && gDocStatus.Equals("C"))
                            {
                                grd.Columns.Add(btnCol);
                            }

                            foreach (DataGridViewColumn col in grd.Columns)
                            {
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                int colw = col.Width;

                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                col.Width = colw;
                            }
                            //grd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                        }
                        foreach (DataGridViewColumn col in grd.Columns)
                        {
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            int colw = col.Width;
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                            col.Width = colw;
                        }
                        currentRowIndex = 0;
                        for (int i = 0; i < grd.Rows.Count; i++)
                        {

                            string currentRef = grd.Rows[i].Cells["reference"].Value.ToString();
     
                            for (int j = 0; j < grd.Rows.Count; j++)
                            {
                                currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                                string currentRef1 = grd.Rows[j].Cells["reference"].Value.ToString();
                                //if (currentRef.Equals("FB-TRFR-100000422") && currentRef1.Equals("FB-TRFR-100000422"))
                                //{
                                //    Console.WriteLine(currentRef + "/" + currentRef1 + Environment.NewLine + currentItemCode + "/" + currentItemCode1 + Environment.NewLine + i + "/" + j);
                                //}
                                bool v = (currentRef == currentRef1)  && (i != j);
                                if (v)
                                {
                                    grd.Rows[i].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                    grd.Rows[j].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                }
                                else if (currentRef != currentRef1)
                                {
                                    currentColorIndex++;
                                }
                            }
                        }
                        var col2 = grd.Columns["remarks"];
                        if (col2 != null)
                        {
                            col2.Width = 200;
                        }
                    }));
                }
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
    }
}