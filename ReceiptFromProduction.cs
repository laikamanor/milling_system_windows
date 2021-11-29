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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using AB.API_Class;

namespace AB
{
    public partial class ReceiptFromProduction : Form
    {
        public ReceiptFromProduction(string docStatus)
        {
            InitializeComponent();
            gDocStatus = docStatus;
        }
        string gDocStatus = "";
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtBranches = new DataTable(), dtPlant = new DataTable();
        DataTable dtColor = new DataTable();
        int currentColorIndex = 0;
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void ReceiptFromProduction_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtFromDate.Visible = false;
            checkToDate.Checked = true;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            loadPlant();
            bg();
        }

        public void loadData()
        {
            string sPlant = "?plant=", sDate = "", sBranch = "", sFromTime = "", sToTime = "", sDocStatus = "&docstatus=" + gDocStatus;
            cmbBranch.Invoke(new Action(delegate ()
            {
                sBranch = "&branch=" + apic.findValueInDataTable(dtBranches, cmbBranch.Text, "name", "code");
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
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "code", "name");
            }));
            string sParams = sPlant + sBranch + sDate + sFromTime + sToTime + sDocStatus;
            string sResult = apic.loadData("/api/production/rec_from_prod/get_all", sParams, "", "", Method.GET, true);
            //Console.WriteLine(sResult);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];


                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if(dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("view_remarks");
                }

                //for (int i = 0; i < grd.Rows.Count; i++)
                //{

                //    string currentRef = grd.Rows[i].Cells["reference"].Value.ToString();
                //    for (int j = 0; j < grd.Rows.Count; j++)
                //    {
                //        currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                //        string currentRef1 = grd.Rows[j].Cells["reference"].Value.ToString();
                //        bool v = (currentRef == currentRef1) && (i != j);
                //        if (v)
                //        {
                //            grd.Rows[i].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                //            grd.Rows[j].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                //        }
                //        else if (currentRef != currentRef1)
                //        {
                //            currentColorIndex++;
                //        }
                //    }
                //}



                dtData.SetColumnsOrder("transdate", "reference", "remarks","view_remarks", "issued_transdate", "issued_reference");
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtData;

                        //auto complete
                        string[] suggestions = { "reference" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.AlwaysVisible = true;
                        gridView1.OptionsFind.FindNullPrompt = "Search " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(suggestConcat.Replace(";", ", ") + "...");
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);

                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = v.Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 :  fieldName.Equals("view_remarks") ? repositoryItemButtonEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("transdate") || fieldName.Equals("issued_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("transdate") || fieldName.Equals("issued_date") ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.Visible = fieldName.Equals("transdate") || fieldName.Equals("reference") || fieldName.Equals("remarks") || fieldName.Equals("view_remarks") || fieldName.Equals("issued_date") || fieldName.Equals("issued_reference");

                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                        }
                        gridView1.BestFitColumns();
                        var col2 = gridView1.Columns["remarks"];
                        if (col2 != null)
                        {
                            col2.Width = 200;
                        }
                    }));
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

        public void loadPlant()
        {
            cmbPlant.Properties.Items.Clear();
            cmbPlant.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/plant/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtPlant.Rows)
                    {
                        cmbPlant.Properties.Items.Add(row["name"].ToString());
                    }
                    string plantCode = Login.jsonResult["data"]["plant"].ToString();
                    string plantName = apic.findValueInDataTable(dtPlant, plantCode, "code", "name");
                    cmbPlant.SelectedIndex = cmbPlant.Properties.Items.IndexOf(plantName) <= 0 ? 0 : cmbPlant.Properties.Items.IndexOf(plantName);
                }
            }
            else
            {
                cmbPlant.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches();
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                string docStatusDecode = gDocStatus.Equals("O") ? "Open" : gDocStatus.Equals("C") ? "Closed" : gDocStatus.Equals("N") ? "Cancelled" : "";
                string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
                string reference = gridView1.GetFocusedRowCellValue("reference").ToString();
                int id = 0,issuedID=0, intTemp = 0;
                id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
                issuedID = gridView1.GetFocusedRowCellValue("issued_id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("issued_id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("issued_id")) : intTemp;
                if (selectedColumnfieldName.Equals("reference"))
                {
                    Production_ReceivedProduction_Items.isSubmit = false;
                    Production_ReceivedProduction_Items items = new Production_ReceivedProduction_Items(docStatusDecode);
                    items.selectedID = id;
                    items.reference = reference;
                    items.ShowDialog();
                    if (Production_ReceivedProduction_Items.isSubmit)
                    {
                        loadData();
                    }
                }
                else if (selectedColumnfieldName.Equals("issued_reference"))
                {
                    Production_IssueProduction_Items frm = new Production_IssueProduction_Items("Closed");
                    frm.selectedID = issuedID;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row && (gDocStatus.Equals("O") || gDocStatus.Equals("C")))
            {
                DXMenuItem item = new DXMenuItem("Cancel this Transaction");
                item.Click += (o, args) => {
                    int intTemp = 0;
                    int id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
                    Remarks.isSubmit = false;
                    Remarks frm = new Remarks();
                    frm.ShowDialog();
                    if (Remarks.isSubmit)
                    {
                        JObject joBody = new JObject();
                        joBody.Add("remarks", Remarks.rem);
                        apiPUT(joBody, "/api/production/rec_from_prod/cancel/" + id.ToString());
                    }
                };
                e.Menu.Items.Add(item);
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
                        MessageBox.Show(response.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }
        private int hotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        private int HotTrackRow
        {
            get
            {
                return hotTrackRow;
            }
            set
            {
                if (hotTrackRow != value)
                {
                    int prevHotTrackRow = hotTrackRow;
                    hotTrackRow = value;
                    gridView1.RefreshRow(prevHotTrackRow);
                    gridView1.RefreshRow(hotTrackRow);

                    if (hotTrackRow >= 0)
                        gridControl1.Cursor = Cursors.Hand;
                    else
                        gridControl1.Cursor = Cursors.Default;
                }
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }

        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            int id = 0, intTemp = 0;
            id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
            if (selectedColumnfieldName.Equals("view_remarks"))
            {
                QA_Remarks.isSubmit = false;
                string sAddRemarksURL = gDocStatus.Equals("O") ? "/api/production/rec_from_prod/remarks/" : "";
                QA_Remarks frm = new QA_Remarks("/api/production/rec_from_prod/remarks/get_all/", sAddRemarksURL, "/api/production/rec_from_prod/remarks/get_by_id/");
                frm.selectedID = id;
                frm.ShowDialog();
            }
        }

        public void loadBranches()
        {
            try
            {
                string plantCode = "";
                cmbPlant.Invoke(new Action(delegate ()
                {
                    plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                }));
                cmbBranch.Invoke(new Action(delegate ()
                {
                    cmbBranch.Properties.Items.Clear();
                }));
                string[] lists = { "isAdmin", "isSuperAdmin" };
                if (apic.haveAccess(lists))
                {
                    string sResult = "";
                    sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", Method.GET, true);
                    if (sResult.Substring(0, 1).Equals("{"))
                    {
                        //DataTable dtData = apic.getDtDownloadResources(sResult, "data");
                        //string sBranch = apic.getFirstRowDownloadResources(dtData, "data");

                        dtBranches = apic.getDtDownloadResources(sResult, "data");
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                cmbBranch.Properties.Items.Add("All");
                            }));
                        }
                        foreach (DataRow row in dtBranches.Rows)
                        {
                            if (IsHandleCreated)
                            {
                                cmbBranch.Invoke(new Action(delegate ()
                                {
                                    cmbBranch.Properties.Items.Add(row["name"].ToString());
                                }));
                            }

                        }
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                string branch = (string)Login.jsonResult["data"]["branch"];
                                string s = apic.findValueInDataTable(dtBranches, branch, "code", "name");
                                cmbBranch.SelectedIndex = cmbBranch.Properties.Items.IndexOf(s) <= 0 ? 0 : cmbBranch.Properties.Items.IndexOf(s);

                            }));
                        }
                    }
                    else
                    {
                        apic.showCustomMsgBox("Validation", sResult);
                    }
                }
                else
                {
                    if (IsHandleCreated)
                    {
                        cmbBranch.Invoke(new Action(delegate ()
                        {
                            cmbBranch.Properties.Items.Add(Login.jsonResult["data"]["branch"]);
                            cmbBranch.SelectedIndex = 0;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
