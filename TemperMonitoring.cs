using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.UI_Class;
using Newtonsoft.Json;
using RestSharp;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AB.API_Class;
using System.Text.RegularExpressions;
using System.Threading;

namespace AB
{
    public partial class TemperMonitoring : Form
    {
        public TemperMonitoring(string mode)
        {
            InitializeComponent();
            gMode = mode;
        }
        string gMode = "";
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtPlant = new DataTable();
        color_class colorc = new color_class();
        DataTable dtColor = new DataTable(), dtColor2 = new DataTable();
        BackgroundWorker bgSubmit = new BackgroundWorker();
        private void TemperMonitoring_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            checkFromDate.Checked = label8.Visible = cmbDispoStatus.Visible = panel1.Visible = label1.Visible = panel2.Visible = label4.Visible = gMode.Equals("for_dispo") ? false : true;
            dtFromDate.Visible = checkFromDate.Checked;


            //color
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
            dtColor2.Columns.Add("index", typeof(int));
            dtColor2.Columns.Add("color", typeof(string));
            loadDispoStatus();
            loadPlant();
            bg(backgroundWorker1);
        }

        public void loadDispoStatus()
        {
            cmbDispoStatus.Properties.Items.Clear();
            string[] list = { "All", "w/ Dispo", "w/o Dispo" };
           foreach(string a in list)
            {
                cmbDispoStatus.Properties.Items.Add(a);
            }
            cmbDispoStatus.SelectedIndex = 1;
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



        public void loadData()
        {
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));
            bool cFromDate = false, cToDate = false;

            string sPlant = "?plant=", sFromDate = "&from_date=", sToDate = "&to_date=", sFromTime = "&from_time=", sToTime = "&to_time=", sMode = "&mode=" + gMode, sDispoStatus = "&dispo_status=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                string plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                sPlant += plantCode;
            }));
            checkFromDate.Invoke(new Action(delegate ()
            {
                cFromDate = checkFromDate.Checked;
            }));
            checkToDate.Invoke(new Action(delegate ()
            {
                cToDate = checkToDate.Checked;
            }));
            dtFromDate.Invoke(new Action(delegate ()
            {
                sFromDate += cFromDate ? dtFromDate.Text : "";
            }));
            dtToDate.Invoke(new Action(delegate ()
            {
                sToDate += cToDate ? dtToDate.Text : "";
            }));
            cmbFromTime.Invoke(new Action(delegate ()
            {
                sFromTime += cFromDate ? cmbFromTime.Text : "";
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime += cmbToTime.Text;
            }));cmbDispoStatus.Invoke(new Action(delegate ()
            {
                string dispoText = cmbDispoStatus.Text;
                sDispoStatus += dispoText.Equals("All") ? 2 : dispoText.Equals("w/ Dispo") ? 1 : dispoText.Equals("w/o Dispo") ? 0 : -1;
            }));
            string sParams = sPlant + sFromDate + sToDate + sFromTime + sToTime + (gMode.Equals("for_dispo") ? sMode : sDispoStatus);
            string sResult = apic.loadData("/api/inv/trfr/tempering_monitoring", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                //dtData.Rows.Add(16, "RW-TRFR-100000005", DateTime.Now, DateTime.Now, 1, "item", 1, "from whse", "to whse", "done");
                if (gMode.Equals("for_dispo") && dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("btn_approve");
                }
                else if(gMode.Equals("1") && dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("btn_view_remarks");
                }
                Task t = Task.Run(() =>
                {
                    string[] list = { "reference" };
                    dtData = dtData.addSameReference(list);
                });
                Task.WaitAll(t);
                Task t2 = Task.Run(() =>
                {
                    string[] columnOrder = new string[]
                    {
                               "transdate",
                               "reference",
                               "start_transfer_date",
                               "end_transfer_date",
                               "tempering_time",
                                "due_start",
                               "due_end",
                               "overdue",
                                "item_code",
                                "quantity",
                                "from_whse",
                                "to_whse",
                                "duedate",
                                "due",
                                "remarks",
                                "mill",
                                "vessel",
                                "created_by",
                                "for_issue",
                                "cw_dispo_date",
                                "issued_date",
                                "issued_reference",
                                "btn_view_remarks"};

                    dtData.SetColumnsOrder(columnOrder);
                });
                Task.WaitAll(t2);
                DataTable dtCloned = new DataTable();
                //Task t3 = Task.Run(() =>
                //{
                //    dtCloned = dtData.Clone();
                //    DateTime dtTemp = new DateTime();
                //    if (dtCloned.Columns.Contains("issued_date"))
                //    {
                //        dtCloned.Columns["issued_date"].DataType = typeof(DateTime);

                //        foreach (DataRow row in dtData.Rows)
                //        {
                //            row["issued_date"] = row["issued_date"] == null ? DBNull.Value : DateTime.TryParse(row["issued_date"].ToString(), out dtTemp) ? row["issued_date"] : (object)DBNull.Value;
                //            dtCloned.ImportRow(row);
                //        }
                //    }
                //});
                //Task.WaitAll(t3);

                loadUI(dtData);
            }
        }

         void loadUI(DataTable dtCloned)
        {
            Task t = Task.Run(() =>
            {
                gridControl1.Invoke(new Action(delegate ()
                {
                    gridControl1.DataSource = dtCloned;
                    gridView1.OptionsView.ColumnAutoWidth = false;
                    gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
                        string v = col.GetCaption();
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());

                        //caption btn remove
                        col.Caption = fieldName.Equals("btn_approve") ? "Approve" : fieldName.Equals("btn_view_remarks") ? "View Remarks" : fieldName.Equals("cw_dispo_date") ? "Dispo Date" : col.Caption;

                        col.ColumnEdit = fieldName.Equals("btn_approve") ? repositoryItemButtonEdit1 : fieldName.Equals("btn_view_remarks") ? repositoryItemButtonEdit2 : fieldName.Equals("remarks") || fieldName.Equals("issued_remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;
                        col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("tempering_time") || fieldName.Equals("overdue") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") || fieldName.Equals("due") || fieldName.Equals("duedate") || fieldName.Equals("start_transfer_date") || fieldName.Equals("end_transfer_date") || fieldName.Equals("due_start") || fieldName.Equals("due_end") || fieldName.Equals("issued_date") || fieldName.Equals("cw_dispo_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("tempering_time") || fieldName.Equals("overdue") ? "{0:#,0.000}" : fieldName.Equals("transdate") || fieldName.Equals("due") || fieldName.Equals("duedate") || fieldName.Equals("issued_date") || fieldName.Equals("start_transfer_date") || fieldName.Equals("end_transfer_date") || fieldName.Equals("due_start") || fieldName.Equals("due_end") || fieldName.Equals("cw_dispo_date") ? "yyyy-MM-dd HH:mm:ss" : "";

                        col.Visible = !(fieldName.Equals("id") || fieldName.Equals("is_overdue") || fieldName.Equals("issued_id") || fieldName.Equals("reference_color") || fieldName.Equals("issued_reference_color"));


                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                        //fixed column
                        col.Fixed =   fieldName.Equals("btn_approve") || fieldName.Equals("btn_view_remarks") ? FixedStyle.Right : fieldName.Equals("reference") || fieldName.Equals("transdate") ? FixedStyle.Left : FixedStyle.None;
                    }
                    gridView1.BestFitColumns();
                    //auto complete
                    string[] suggestions = { "reference" };
                    string suggestConcat = string.Join(";", suggestions);
                    gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(gridView1, gridControl1, suggestions);
                    gridView1.BestFitColumns();
                    var col2 = gridView1.Columns["remarks"];
                    var col3 = gridView1.Columns["issued_remarks"];
                    if (col2 != null)
                    {
                        col2.Width = 200;
                    }
                    if (col3 != null)
                    {
                        col3.Width = 200;
                    }
                }));
            });
            t.Wait();
        }

        public void bg(BackgroundWorker bgww)
        {
            if (!bgww.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgww.RunWorkerAsync();
            }
        }

        public void closeForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Loading")
                {
                    frm.Invoke(new Action(delegate ()
                    {
                        frm.Hide();
                    }));
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void checkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkFromDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        public void funcApprove()
        {
            try
            {
                int id = 0, intTemp = 0;
                id = int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
                string currentRef = gridView1.GetFocusedRowCellValue("reference").ToString();
                Remarks.isSubmit = false;
                Remarks rem = new Remarks();
                rem.Text = "Approve - " + currentRef;
            rem.ShowDialog();
                if (Remarks.isSubmit)
                {
                    JObject joBody = new JObject();
                    joBody.Add("remarks", Remarks.rem);
                    string sResult = apic.loadData("/api/inv/trfr/tempering/dispo/", id.ToString(), "application/json", joBody.ToString(), RestSharp.Method.PUT, true);
                    if (sResult != null)
                    {
                        if (!string.IsNullOrEmpty(sResult.Trim()))
                        {
                            if (sResult.StartsWith("{"))
                            {
                                JObject joResult = JObject.Parse(sResult);
                                bool isSuccess = false;
                                isSuccess = (bool)joResult["success"];
                                string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
                                apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                                bg(backgroundWorker1);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            funcApprove();
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedColumnText = gridView1.FocusedColumn.FieldName;

                if (selectedColumnText.Equals("reference"))
                {
                    int id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString() == "" ? 0 : !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
                    TransferItem_Details.isSubmit = false;
                    TransferItem_Details frm = new TransferItem_Details(id);
                    frm.ShowDialog();
                    if (TransferItem_Details.isSubmit)
                    {
                        bg(backgroundWorker1);
                    }
                }
                else if (selectedColumnText.Equals("issued_reference"))
                {
                    string issuedReference = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_reference").ToString();

                    if (!string.IsNullOrEmpty(issuedReference.Trim()))
                    {
                        int issuedID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString() == "" ? 0 : !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) : 0;

                        Production_IssueProduction_Items.isSubmit = false;
                        Production_IssueProduction_Items frm = new Production_IssueProduction_Items("Closed");
                        frm.reference = issuedReference;
                        frm.selectedID = issuedID;
                        frm.ShowDialog();
                        if (Production_IssueProduction_Items.isSubmit)
                        {
                            bg(backgroundWorker1);
                        }
                    }else
                    {
                        apic.showCustomMsgBox("Validation", "No Issue For Production found!");
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
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

        public bool isValidHexaCode(string str)
        {
            // Define a regular expression for repeated words.

            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            // Return true if the hexadecimal color code
            // matched the ReGex
            if (Regex.IsMatch(str, @"\b(?<word>\w+)\s+(\k<word>)\b"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle == HotTrackRow)
                    e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
                else
                    e.Appearance.BackColor = e.Appearance.BackColor;

                if (e.Column.FieldName.Equals("reference"))
                {
                    string referenceCol = gridView1.GetRowCellValue(e.RowHandle, "reference_color") == null ? "" : gridView1.GetRowCellValue(e.RowHandle, "reference_color").ToString();
                    Color col = ColorTranslator.FromHtml(referenceCol);
                    e.Appearance.BackColor = col;
                }
                if (e.Column.FieldName.Equals("overdue") && gMode.Equals("1"))
                {
                    int intTemp = 0;
                    int overDue = Int32.TryParse(gridView1.GetRowCellValue(e.RowHandle, "overdue").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "overdue").ToString()) : intTemp;
                    if (overDue > 3)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 151, 94);
                    }
                    else if (overDue < 0)
                    {
                        e.Appearance.BackColor = Color.FromArgb(250, 102, 82);
                    }
                }
                //if (e.Column.FieldName.Equals("issued_reference"))
                //{
                //    string referenceCol = gridView1.GetRowCellValue(e.RowHandle, "issued_reference_color").ToString();
                //    Color col = ColorTranslator.FromHtml(referenceCol);
                //    e.Appearance.BackColor = col;
                //}
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    int overDue = 0, intTemp = 0;
            //    overDue = Int32.TryParse(gridView1.GetRowCellValue(e.RowHandle, "overdue").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "overdue").ToString()) : intTemp;
            //    if (overDue > 0)
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //    }
            //}
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
         

        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
         try
            {
                string selectedColumnText = gridView1.FocusedColumn.FieldName;

                if (selectedColumnText.Equals("btn_view_remarks"))
                {
                    int id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString() == "" ? 0 : !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;

                    string reference = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_reference").ToString();

                    Transfer_Remarks frm = new Transfer_Remarks(id, reference);
                    frm.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
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
    }
}
