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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using AB.API_Class;
namespace AB
{
    public partial class QA_Disposition_FlourPackingBin2 : Form
    {
        public QA_Disposition_FlourPackingBin2(string type, string tabName)
        {
            InitializeComponent();
            gType = type;
            gTabName = tabName;
        }
        string gType = "", gTabName = "";
        public string docStatus = "";
        DataTable dtPlant = new DataTable(), dtWarehouse = new DataTable();
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        utility_class utilityc = new utility_class();
        color_class colorc = new color_class();
        int currentColorIndex = 0;
        DataTable dtColor = new DataTable();
        private void QA_Disposition_FlourPackingBin2_Load(object sender, EventArgs e)
        {
            checkDate.Checked = !(docStatus.Equals("O") || gType.Equals("For Disposition"));
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
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

        public void loadData()
        {
            try
            {
                gridControl1.Invoke(new Action(delegate ()
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                }));
                bool cFromDate = false, cToDate = false;
                string sDtFromDate = "?from_date=", sDtToDate = "&to_date=", sFromTime = "&from_time=", sToTime = "&to_time=", sPlant = "&plant=", sTabName = "&tab=" + gTabName;
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
                    sDtFromDate += cFromDate ? dtFromDate.Text : "";
                }));
                dtToDate.Invoke(new Action(delegate ()
                {
                    sDtToDate += cToDate ? dtToDate.Text : "";
                }));
                cmbFromTime.Invoke(new Action(delegate ()
                {
                    sFromTime += cmbFromTime.Text;
                }));
                cmbToTime.Invoke(new Action(delegate ()
                {
                    sToTime += cmbToTime.Text;
                }));
                cmbPlant.Invoke(new Action(delegate ()
                {
                    sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                }));
                string sDispoStatus = gType.Equals("For Disposition") ? "&dispo_status=O&is_rejected=" : gType.Equals("Rejected") && docStatus.Equals("O") ? "&dispo_status=O&is_rejected=1" : gType.Equals("Rejected") && docStatus.Equals("C") ? "&dispo_status=C&is_rejected=1" : gType.Equals("Approved") ? "&dispo_status=C&is_rejected=0" : "";
                string sParams = sDtFromDate + sDtToDate + sFromTime + sToTime + sPlant + sDispoStatus + sTabName;
                string sResult = apic.loadData("/api/inv/trfr/for_dispo", sParams, "", "", RestSharp.Method.GET, true);

                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.Trim().StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        JArray jaData = (JArray)joResult["data"] == null ? new JArray() : (JArray)joResult["data"];
                        DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                        currentColorIndex = 0;
                        color_class colorc = new color_class();
                        dtColor.Rows.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            string currentRef = row["reference"].ToString();
                            foreach (DataRow row2 in dt.Rows)
                            {
                                currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                                string currentRef1 = row2["reference"].ToString();
                                bool v = (currentRef == currentRef1) && (dt.Rows.IndexOf(row) != dt.Rows.IndexOf(row2));
                                if (v)
                                {
                                    Color cc = colorc.c[currentColorIndex];
                                    string hex = string.Format("{0:X2}{1:X2}{2:X2}", cc.R, cc.G, cc.B);
                                    dtColor.Rows.Add(dt.Rows.IndexOf(row), hex);
                                    dtColor.Rows.Add(dt.Rows.IndexOf(row2), hex);
                                }
                                else if (currentRef != currentRef1)
                                {
                                    currentColorIndex++;
                                }
                            }
                        }

                        //if (dt.Rows.Count <= 0)
                        //{
                        //    dt.Columns.Add("transdate", typeof(DateTime));
                        //    dt.Columns.Add("item_code", typeof(string));
                        //    dt.Columns.Add("from_whse", typeof(string));
                        //    dt.Columns.Add("to_whse", typeof(string));
                        //    dt.Columns.Add("quantity", typeof(double));
                        //    dt.Columns.Add("remarks_count", typeof(int));
                        //    dt.Columns.Add("id", typeof(int));
                        //    dt.Columns.Add("reference", typeof(string));
                        //    dt.Columns.Add("remarks", typeof(string));
                        //    dt.Columns.Add("final_dispo_closed_date", typeof(DateTime));
                        //    dt.Rows.Add("2021-07-21T11:58:32", "I-Makiling", "Mill-B FB 613", "Mill-B PB 3", 3.2, 1, 49, "FB-TRFR-100000000", "pinoypan", "2021-07-21T14:11:00.385691");
                        //}
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Add("view_remarks");
                            dt.Columns.Add("view_history");
                            if (gType.Equals("For Disposition"))
                            {
                                dt.Columns.Add("approve");
                                dt.Columns.Add("reject");
                            }
                            if(gType.Equals("Rejected") && docStatus.Equals("O"))
                            {
                                dt.Columns.Add("close");
                            }
                        }
                        dt.SetColumnsOrder("transdate", "reference", "item_code", "quantity", "from_whse", "to_whse", "remarks", "view_remarks", "final_dispo_closed_date", "view_history", "reject", "approve","close");

                        DateTime dtTemp = new DateTime();
                        double doubleTemp = 0.00;
                        int intTemp = 0;
                        gridControl1.Invoke(new Action(delegate ()
                        {
                            gridControl1.DataSource = null;
                            gridControl1.DataSource = dt;

                            gridView1.OptionsView.ColumnAutoWidth = false;
                            gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                            //auto complete
                            string[] suggestions = { "reference", };
                            string suggestConcat = string.Join(";", suggestions);
                            gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                            devc.loadSuggestion(gridView1, gridControl1, suggestions);
                            foreach (GridColumn col in gridView1.Columns)
                            {
                                string fieldName = col.FieldName;
                                string v = col.GetCaption();
                                string s = v.Replace("_", " ");
                                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                                col.ColumnEdit = fieldName.Equals("view_remarks") ? repositoryItemButtonEdit1 : fieldName.Equals("approve") ? repositoryItemButtonEdit2 : fieldName.Equals("reject") ? repositoryItemButtonEdit3 : fieldName.Equals("close") ? repositoryItemButtonEdit4 : fieldName.Equals("view_history") ? repositoryItemButtonEdit5 : repositoryItemTextEdit1;
                                col.DisplayFormat.FormatType = fieldName.Equals("transdate") || fieldName.Equals("final_dispo_closed_date") ? DevExpress.Utils.FormatType.DateTime : fieldName.Equals("quantity")? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                                col.DisplayFormat.FormatString = fieldName.Equals("transdate") || fieldName.Equals("final_dispo_closed_date") ? "yyyy-MM-dd HH:mm:ss" : fieldName.Equals("quantity") ? "n3" : "";
                                col.Visible = !(fieldName.Equals("id") || fieldName.Equals("remarks_count"));

                                //fix column
                                //col.Fixed = fieldName.Equals("reference") || fieldName.Equals("transdate") || fieldName.Equals("item_code") || fieldName.Equals("quantity") ? FixedStyle.Left : FixedStyle.None;

                                //fonts
                                FontFamily fontArial = new FontFamily("Arial");
                                col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                                col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                            }
                            gridView1.BestFitColumns();
                        }));
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
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
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (id <= 0)
            {
                MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (selectedColumnText.Equals("view_remarks"))
                {
                    QA_Remarks.isSubmit = false;
                    QA_Remarks frm = new QA_Remarks("/api/inv/trfr/remarks/get_all/", "/api/inv/trfr/remarks/", "/api/inv/trfr/remarks/get_by_id/");
                    frm.selectedID = id;
                    frm.ShowDialog();
                    if (QA_Remarks.isSubmit)
                    {
                        bg();
                    }
                }
            }
        }

        public void btnPut(string title, string url)
        {
            int intTemp = 0;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            int remarksCount = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "remarks_count").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "remarks_count").ToString()) : 0;
            string reference = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "reference") == null ? "" : gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "reference").ToString();
            if (id <= 0)
            {
                MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (remarksCount <= 0)
                {
                    MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    apiPut(title, url, id, reference);
                }
            }
        }

        public void apiPut(string title, string url, int id, string reference)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + title + " " + reference + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sResult = apic.loadData(url, id.ToString(), "", "", RestSharp.Method.PUT, true);
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        bool isSuccess = (bool)joResult["success"];
                        string msg = joResult["message"].ToString();
                        //MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        if (isSuccess)
                        {
                            bg();
                        }
                    }
                }
            }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            if (selectedColumnText.Equals("approve"))
            {
                btnPut("approve", "/api/inv/trfr/dispo/approve/");
            }
        }

        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            if (selectedColumnText.Equals("reject"))
            {
                btnPut("reject", "/api/inv/trfr/dispo/reject/");
            }
        }

        private void repositoryItemButtonEdit4_Click(object sender, EventArgs e)
        {
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            string reference = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "reference") == null ? "" : gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "reference").ToString();
            if (selectedColumnText.Equals("close"))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to close " + reference + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (gType.Equals("Rejected") && docStatus.Equals("O"))
                    {
                        string sResult = apic.loadData("/api/inv/trfr/dispo/close/", id.ToString(), "", "", RestSharp.Method.PUT, true);
                        if (sResult != null)
                        {
                            if (!string.IsNullOrEmpty(sResult.Trim()))
                            {
                                if (sResult.StartsWith("{"))
                                {
                                    JObject joResult = JObject.Parse(sResult);
                                    bool isSuccess = false, boolTemp = false;
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

        private void repositoryItemButtonEdit5_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (id <= 0)
            {
                MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (selectedColumnText.Equals("view_history"))
                {
                    PackingBinsHistory frm = new AB.PackingBinsHistory(id);
                    frm.ShowDialog();
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

        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
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
    }
}
