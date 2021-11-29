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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AB.API_Class;

namespace AB
{
    public partial class IssueForPacking : Form
    {
        public IssueForPacking(string docStatus, string tab)
        {
            InitializeComponent();
            gDocStatus = docStatus;
            gTab = tab;
        }
        string gDocStatus = "", gTab = "";
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtPlant = new DataTable(), dtAssignDept = new DataTable();
        int currentColorIndex = 0;
        DataTable dtColor = new DataTable();
        private void IssueForPacking_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
            dtFromDate.Visible = false;
            checkDate.Checked = !gDocStatus.Equals("O");
            checkToDate.Checked = true;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            loadPlant();
            bg();
        }

        public void loadData()
        {
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
            string sResult = apic.loadData("/api/production/issue_for_packing/get_all", sParams, "", "", Method.GET, true);
            //Console.WriteLine(sResult);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                //Console.WriteLine(jaData);
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));

                if (dtData.Rows.Count > 0)
                {
                    DataTable dtCloned = dtData.Clone();
                    if (dtCloned.Columns.Contains("receipt_quantity"))
                    {
                        dtCloned.Columns["receipt_quantity"].DataType = typeof(double);
                    }
                    if (dtCloned.Columns.Contains("fg_quantity"))
                    {
                        dtCloned.Columns["fg_quantity"].DataType = typeof(double);
                    }
                    if (dtCloned.Columns.Contains("fg_targeted_qty"))
                    {
                        dtCloned.Columns["fg_targeted_qty"].DataType = typeof(double);
                    }
                    foreach (DataRow row in dtData.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }
                    dtData = dtCloned;
                }

                if (dtData.Rows.Count > 0 && gDocStatus.Equals("C"))
                {
                    dtData.Columns.Add("view_remarks");
                }
                if(dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("view_base_reference");
                }
                dtData.SetColumnsOrder("transdate", "reference", "item_code", "quantity", "whsecode", "mill", "gr_num", "remarks", "view_remarks", "fg_item", "fg_quantity", "fg_uom", "fg_targeted_qty", "created_by", "view_base_reference");
                currentColorIndex = 0;
                color_class colorc = new color_class();
                dtColor.Rows.Clear();
                foreach (DataRow row in dtData.Rows)
                {
                    string currentRef = row["reference"].ToString();
                    foreach (DataRow row2 in dtData.Rows)
                    {
                        currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                        string currentRef1 = row2["reference"].ToString();
                        bool v = (currentRef == currentRef1) && (dtData.Rows.IndexOf(row) != dtData.Rows.IndexOf(row2));
                        if (v)
                        {
                            Color cc = colorc.c[currentColorIndex];
                            string hex = string.Format("{0:X2}{1:X2}{2:X2}", cc.R, cc.G, cc.B);
                            dtColor.Rows.Add(dtData.Rows.IndexOf(row), hex);
                            dtColor.Rows.Add(dtData.Rows.IndexOf(row2), hex);
                        }
                        else if (currentRef != currentRef1)
                        {
                            currentColorIndex++;
                        }
                    }
                }

                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtData;

                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                        //auto complete
                        string[] suggestions = { "reference" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.AlwaysVisible = true;
                        gridView1.OptionsFind.FindNullPrompt = "Search " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(suggestConcat.Replace(";", ", ") + "...");
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);


                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = v.Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("view_remarks") ? repositoryItemButtonEdit1 : fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : fieldName.Equals("view_base_reference") ? repositoryItemButtonEdit2 : repositoryItemTextEdit1;

                            col.DisplayFormat.FormatType = fieldName.Equals("transdate") || fieldName.Equals("receipt_date") ? DevExpress.Utils.FormatType.DateTime : fieldName.Equals("receipt_quantity") || fieldName.Equals("fg_quantity")  || fieldName.Equals("fg_targeted_qty") || fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;

                            col.DisplayFormat.FormatString = fieldName.Equals("transdate") || fieldName.Equals("receipt_date") ? "yyyy-MM-dd HH:mm:ss" : fieldName.Equals("receipt_quantity") || fieldName.Equals("fg_quantity") || fieldName.Equals("fg_targeted_qty") || fieldName.Equals("quantity") ? "n3" : "";

                            col.Visible = fieldName.Equals("transdate") || fieldName.Equals("reference") || fieldName.Equals("mill") || fieldName.Equals("remarks") || fieldName.Equals("view_remarks") || fieldName.Equals("fg_item") || fieldName.Equals("fg_quantity") || fieldName.Equals("fg_uom") || fieldName.Equals("gr_num") || fieldName.Equals("created_by") || fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("fg_targeted_qty") || fieldName.Equals("view_base_reference");

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

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
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

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            int id = 0, intTemp = 0;
            id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
            if (selectedColumnfieldName.Equals("view_remarks"))
            {
                string remarksURL = "/api/production/issue_for_prod/remarks/";
                string remarksByIdURL = "/api/production/issue_for_prod/remarks/get_by_id/";
                QA_Remarks frm = new QA_Remarks(remarksURL, "", remarksByIdURL);
                frm.selectedID = id;
                frm.ShowDialog();
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
        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
            if (e.Column.FieldName.Equals("reference"))
            {
                foreach (DataRow row in dtColor.Rows)
                {
                    int index = 0, intTemp = 0;
                    index = int.TryParse(row["index"].ToString(), out intTemp) ? Convert.ToInt32(row["index"].ToString()) : intTemp;
                    if (index == e.RowHandle)
                    {
                        //Color color = new Color(), colorTemp = new Color();
                        //Console.WriteLine(row["color"].ToString());
                        //e.Appearance.BackColor = ColorTranslator.FromHtml(row["color"].ToString());
                        //Color.from
                        e.Appearance.BackColor = ColorTranslator.FromHtml("#" + row["color"].ToString());
                    }
                }
            }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            int transferID = 0, intTemp = 0;
            transferID = gridView1.GetFocusedRowCellValue("transfer_id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("transfer_id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("transfer_id").ToString()) : intTemp;
            if (selectedColumnfieldName.Equals("view_base_reference"))
            {
                if(transferID > 0)
                {
                    this.Focus();
                    MIssueForPacking_TransferItem.isSubmit = false;
                    MIssueForPacking_TransferItem frm = new MIssueForPacking_TransferItem(transferID);
                    frm.btnCreateIssuePacking.Visible = false;
                    frm.ShowDialog();
                }else
                {
                    MessageBox.Show("No Base reference found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void gridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row && gDocStatus.Equals("O") && e.HitInfo.Column.FieldName.Equals("reference"))
            {
                DXMenuItem item = new DXMenuItem("Cancel this Transaction");
                item.Click += (o, args) =>
                {
                    int intTemp = 0;
                    int id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
                    string reference = gridView1.GetFocusedRowCellValue("reference") == null ? "" : gridView1.GetFocusedRowCellValue("reference").ToString();
                    Remarks.isSubmit = false;
                    Remarks frm = new Remarks();
                    frm.Text = "Cancel - " + reference;
                    frm.ShowDialog();
                    if (Remarks.isSubmit)
                    {
                        JObject joBody = new JObject();
                        joBody.Add("remarks", Remarks.rem);
                        apiPUT(joBody, "/api/production/issue_for_prod/cancel/" + id.ToString());
                    }
                };
                DXMenuItem item2 = new DXMenuItem("Close this Transaction");
                item2.Click += (o, args) =>
                {
                    int intTemp = 0;
                    int id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
                    string reference = gridView1.GetFocusedRowCellValue("reference") == null ? "" : gridView1.GetFocusedRowCellValue("reference").ToString();
                    GR_Remarks.isSubmit = false;
                    GR_Remarks frm = new GR_Remarks();
                    frm.Text = "Close - " + reference;
                    frm.ShowDialog();
                    if (GR_Remarks.isSubmit)
                    {
                        JObject joBody = new JObject();
                        joBody.Add("gr_num", !string.IsNullOrEmpty(GR_Remarks.grNumber.Trim()) ? GR_Remarks.grNumber : (string)null);
                        joBody.Add("remarks", GR_Remarks.remarks);
                        apiPUT(joBody, "/api/production/issue_for_prod/manual_close/" + id);
                    }
                };
                e.Menu.Items.Add(item);
                e.Menu.Items.Add(item2);
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
    }
}
