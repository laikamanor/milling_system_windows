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
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using AB.API_Class;

namespace AB
{
    public partial class FlourBrandPollardBins : Form
    {
        public FlourBrandPollardBins(string specsType, string tabName)
        {
            gSpecsType = specsType;
            gTabName = tabName;
            InitializeComponent();
        }
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtPlant = new DataTable();
        public string docStatus = "";
        string gSpecsType = "", gTabName = "";
        private void QADisposition_Tab_Load(object sender, EventArgs e)
        {
            //label12.Visible= label2.Visible= cmbFromTime.Visible = cmbToTime.Visible = gType.Equals("Off") ? true : false;
            //this.WindowState = FormWindowState.Maximized;
            this.Icon = Properties.Resources.logo2;
            checkDate.Checked = !(gSpecsType.Equals("") || docStatus.Equals("O"));
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            loadPlant();
            bg();
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

        public void loadData()
        {
            bool cFromDate = false, cToDate = false;
            string sDtFromDate = "", sDtToDate = "", sFromTime = "", sToTime = "";

            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));

            checkDate.Invoke(new Action(delegate ()
            {
                cFromDate = checkDate.Checked;
            }));
            checkToDate.Invoke(new Action(delegate ()
            {
                cToDate = checkToDate.Checked;
            }));
            dtFromDate.Invoke(new Action(delegate ()
            {
                sDtFromDate = dtFromDate.Text;
            }));
            dtToDate.Invoke(new Action(delegate ()
            {
                sDtToDate = dtToDate.Text;
            }));
            cmbFromTime.Invoke(new Action(delegate ()
            {
                sFromTime = cmbFromTime.Text;
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime = cmbToTime.Text;
            }));


            string sSpecs= gSpecsType.Equals("Off") ? "?off_specs=1" : gSpecsType.Equals("On") ? "?on_specs=1" : "";
            string sDocStatus = "&docstatus=" + docStatus;
            string sFromDate = "";
            sFromDate = (string.IsNullOrEmpty(sSpecs.Trim()) ? "?from_date=" : "&from_date=") + (cFromDate ? sDtFromDate : "");
            string sTime = "&from_time=" + sFromTime + "&to_time=" + sToTime;
            string sToDate = (string.IsNullOrEmpty(sSpecs.Trim()) && string.IsNullOrEmpty(sFromDate.Trim()) ? "?to_date=" : "&to_date=") + (cToDate ?sDtToDate : "");
            string sPlant = "&plant=";
            string sTabName="&tab=" + gTabName;
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));

            string sParams = sSpecs + sFromDate + sToDate + sDocStatus + sTime + sPlant + sTabName;

            string sResult = apic.loadData("/api/production/rec_from_prod/for_qa/get_all", sParams, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    //Console.WriteLine(jaData);
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("qa_remarks");
                        dt.Columns.Add("off__specs");
                        dt.Columns.Add("on__specs");
                        dt.Columns.Add("close");
                        gridControl1.Invoke(new Action(delegate ()
                        {
                            dt.SetColumnsOrder("issued_date", "issued_reference", "transdate", "reference", "item_code", "quantity", "whsecode", "from_whse", "to_whse", "mill", "remarks", "qa_remarks", "created_by", "closed_by", "date_closed");
                            gridControl1.DataSource = dt;

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
                                string s = col.GetCaption().Replace("_", " ").Replace("__", "");
                                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                                col.ColumnEdit = fieldName.Equals("qa_remarks") ? repositoryItemButtonEdit2 : fieldName.Equals("off__specs") ? repositoryItemButtonEdit3 : fieldName.Equals("on__specs") ? repositoryItemButtonEdit4 : fieldName.Equals("close") ? repositoryItemButtonEdit5 : fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit4;

                                col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.Custom;
                                col.DisplayFormat.FormatString = fieldName.Equals("transdate") || fieldName.Equals("issued_date") || fieldName.Equals("date_closed") ? "yyyy-MM-dd HH:mm:ss" : fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                                //if (!fieldName.Equals("btn_off_specs") || !fieldName.Equals("btn_on_specs"))
                                //{
                             
                                //}
                                if (gSpecsType.Equals(""))
                                {
                                    col.Visible = fieldName.Equals("reference") || fieldName.Equals("transdate") || fieldName.Equals("remarks") || fieldName.Equals("qa_remarks") || fieldName.Equals("off__specs") || fieldName.Equals("on__specs") || fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("created_by") || fieldName.Equals("issued_reference") || fieldName.Equals("issued_date") || fieldName.Equals("mill");
                                    Console.WriteLine("oneee");
                                }
                                else if (gSpecsType.Equals("Off") && docStatus.Equals("O"))
                                {
                                    col.Visible = fieldName.Equals("reference") || fieldName.Equals("transdate") || fieldName.Equals("remarks") || fieldName.Equals("qa_remarks") || fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("close") || fieldName.Equals("created_by") || fieldName.Equals("issued_reference") || fieldName.Equals("issued_date") || fieldName.Equals("mill");
                                    Console.WriteLine("twooo");
                                }else if(gSpecsType.Equals("Off") && docStatus.Equals("C"))
                                {
                                    col.Visible = fieldName.Equals("reference") || fieldName.Equals("transdate") || fieldName.Equals("remarks") || fieldName.Equals("qa_remarks") || fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("created_by") || fieldName.Equals("issued_reference") || fieldName.Equals("issued_date") || fieldName.Equals("mill") || fieldName.Equals("closed_by") || fieldName.Equals("date_closed");
                                }
                                else
                                {
                                    col.Visible = fieldName.Equals("reference") || fieldName.Equals("transdate") || fieldName.Equals("remarks") || fieldName.Equals("qa_remarks") || fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("created_by") || fieldName.Equals("issued_reference") || fieldName.Equals("issued_date") || fieldName.Equals("mill");
                                }

                                //fonts
                                FontFamily fontArial = new FontFamily("Arial");
                                col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                                col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                            }
                            gridView1.BestFitColumns();
                            var col2 = gridView1.Columns["remarks"];
                            if(col2 != null)
                            {
                                col2.Width = 200;
                            }
                        }));
                    }
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

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            QA_Remarks.isSubmit = false;
            QA_Remarks frm = new QA_Remarks("/api/production/rec_from_prod/remarks/get_all/", "/api/production/rec_from_prod/remarks/", "/api/production/rec_from_prod/remarks/get_by_id/");
            frm.selectedID = id;
            frm.ShowDialog();
            if (QA_Remarks.isSubmit)
            {
                bg();
            }
        }

        private void repositoryItemTextEdit3_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int remarksCount = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "qa_remarks_count").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "qa_remarks_count").ToString()) : 0;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (gTabName.Equals("FLOUR BINS"))
            {
                if (selectedColumnText.Equals("off__specs"))
                {
                    if (remarksCount > 0)
                    {
                        toggleSpecs("off", id);
                    }
                    else
                    {
                        MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else if (selectedColumnText.Equals("on__specs"))
                {
                    if (remarksCount > 0)
                    {
                        toggleSpecs("on", id);
                    }
                    else
                    {
                        MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void toggleSpecs(string specs, int id)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to toggle " + specs + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sResult = apic.loadData("/api/production/rec_from_prod/toggle/" + specs + "/", id.ToString(), "", "", RestSharp.Method.PUT, true);
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        bool isSuccess = (bool)joResult["success"];
                        string msg = joResult["message"].ToString();
                        MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        if (isSuccess)
                        {
                            bg();
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int issuedID = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issue_id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issue_id").ToString()) : 0;
            if (selectedColumnText.Equals("issued_reference"))
            {
                if (issuedID > 0)
                {
                    Production_IssueProduction_Items frm = new Production_IssueProduction_Items("Closed");
                    frm.selectedID = issuedID;
                    frm.ShowDialog();
                }
            }
        }

        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int remarksCount = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "qa_remarks_count").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "qa_remarks_count").ToString()) : 0;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (gTabName.Equals("FLOUR BINS") || gTabName.Equals("BRAN/POLLARD BINS"))
            {
                if (selectedColumnText.Equals("off__specs"))
                {
                    if (remarksCount > 0)
                    {
                        toggleSpecs("off", id);
                    }
                    else
                    {
                        MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else if (selectedColumnText.Equals("on__specs"))
                {
                    if (remarksCount > 0)
                    {
                        toggleSpecs("off", id);
                    }
                    else
                    {
                        MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void repositoryItemButtonEdit4_Click_1(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int remarksCount = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "qa_remarks_count").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "qa_remarks_count").ToString()) : 0;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (gTabName.Equals("FLOUR BINS") || gTabName.Equals("BRAN/POLLARD BINS"))
            {
                if (selectedColumnText.Equals("off__specs"))
                {
                    if (remarksCount > 0)
                    {
                        toggleSpecs("off", id);
                    }
                    else
                    {
                        MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else if (selectedColumnText.Equals("on__specs"))
                {
                    if (remarksCount > 0)
                    {
                        toggleSpecs("on", id);
                    }
                    else
                    {
                        MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid title!" + Environment.NewLine + selectedColumnText + "/off__specs", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
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

        private void repositoryItemButtonEdit5_Click(object sender, EventArgs e)
        {
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to close this transaction?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (gSpecsType.Equals("Off") && docStatus.Equals("O"))
                {
                    string sResult = apic.loadData("/api/production/rec_from_prod/manual_close/", id.ToString(), "", "", RestSharp.Method.PUT, true);
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
                                MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                                bg();
                            }
                        }
                    }
                }
            }
        }
    }

    public static class DataTableExtensions
    {
        public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            DataColumnCollection columns = table.Columns;
            if (table.Columns.Count > 0)
            {
                foreach (var columnName in columnNames)
                {
                    if(columns.Contains(columnName))
                    {
                        table.Columns[columnName].SetOrdinal(columnIndex);
                        columnIndex++;
                    }
                }
            }
        }
        /// <summary>
        /// wala lang
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static DataTable addSameReference(this DataTable dt, string[] columnNames)
        {
            api_class apic = new api_class();
            color_class colorc = new color_class();
            try
            {
                
                DataTable dt2 = dt;
                //add if not exist
                if (dt2.Rows.Count > 0)
                {
                    if (columnNames != null && columnNames.Length > 0)
                    {
                        foreach (string columnName in columnNames)
                        {
                            if (dt2.Columns.Contains(columnName))
                            {
                                //await Task.Run(() =>
                                //{
                                //    if (!dt2.Columns.Contains(columnName + "_color"))
                                //    {
                                //        dt2.Columns.Add(columnName + "_color", typeof(string));
                                //    }
                                //});
                                if (!dt2.Columns.Contains(columnName + "_color"))
                                {
                                    dt2.Columns.Add(columnName + "_color", typeof(string));
                                }
                            }
                            else
                            {
                                apic.showCustomMsgBox("Validation", columnName + " not exist! Returned original data");
                                return dt;
                            }
                        }
                        int counter = 0;
                        foreach (string columnName in columnNames)
                        {

                            if (dt2.Columns.Contains(columnName))
                            {
                                foreach (DataRow row in dt2.Rows)
                                {
                                    string reference = row[columnName].ToString();
                                    DataRow[] rows = dt2.Select(columnName + "='" + reference + "'");
                                    if (rows.Length > 0)
                                    {
                                        foreach (DataRow row2 in rows)
                                        {
                                            if (dt2.Rows.IndexOf(row) != dt2.Rows.IndexOf(row2))
                                            {
                                                int rIndex = dt2.Rows.IndexOf(row);
                                                int rIndex2 = dt2.Rows.IndexOf(row2);

                                                counter = counter >= colorc.c.Count() ? 0 : counter;
                                                Color col = colorc.c[counter];

                                                dt2.Rows[rIndex][columnName + "_color"] = ColorTranslator.ToHtml(col);
                                                dt2.Rows[rIndex2][columnName + "_color"] = ColorTranslator.ToHtml(col);
                                            }
                                        }
                                        counter++;
                                    }
                                }
                            }
                        }
                        return dt2;
                    }
                    else
                    {
                        apic.showCustomMsgBox("Validation", "No column to add color! Returned original data");
                        return dt;
                    }
                }
                else
                {
                    return dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                apic.showCustomMsgBox("Validation", "Returned original data");
                return dt;
            }
        }
    }
}
