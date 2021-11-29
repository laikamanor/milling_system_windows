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
            checkFromDate.Checked = gMode.Equals("") ? false : true;
            dtFromDate.Visible = checkFromDate.Checked;

            //color
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
            dtColor2.Columns.Add("index", typeof(int));
            dtColor2.Columns.Add("color", typeof(string));
            loadPlant();
            bg(backgroundWorker1);
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

            string sPlant = "?plant=", sFromDate = "&from_date=", sToDate = "&to_date=", sFromTime = "&from_time=", sToTime = "&to_time=", sMode = "&mode=" + gMode;
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
            }));
            string sParams = sPlant + sFromDate + sToDate + sFromTime + sToTime + sMode;
            string sResult = apic.loadData("/api/inv/trfr/tempering_monitoring", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                //Console.WriteLine(jaData.ToString());
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                //dtData.Rows.Add(16, "RW-TRFR-100000005", DateTime.Now, DateTime.Now, 1, "item", 1, "from whse", "to whse", "done");
                if (gMode.Equals("") && dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("btn_good");
                    dtData.Columns.Add("btn_overdue");
                }
                Console.WriteLine("H1");
                Task t = Task.Run(() =>
                {
                    string[] list = { "reference", "issued_reference" };
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
                                "issued_date",
                                "issued_reference",
                                "issued_remarks",
                                "for_issue"};

                    dtData.SetColumnsOrder(columnOrder);
                });
                Task.WaitAll(t2);
                Console.WriteLine("H2");
                DataTable dtCloned = new DataTable();
                Task t3 = Task.Run(() =>
                {
                    dtCloned = dtData.Clone();
                    DateTime dtTemp = new DateTime();
                    if (dtCloned.Columns.Contains("issued_date"))
                    {
                        dtCloned.Columns["issued_date"].DataType = typeof(DateTime);

                        foreach (DataRow row in dtData.Rows)
                        {
                            row["issued_date"] = row["issued_date"] == null ? DBNull.Value : DateTime.TryParse(row["issued_date"].ToString(), out dtTemp) ? row["issued_date"] : (object)DBNull.Value;
                            dtCloned.ImportRow(row);
                        }
                    }
                });
                Task.WaitAll(t3);
                Console.WriteLine("H3");

                loadUI(dtCloned);
            }
        }

        async void loadUI(DataTable dtCloned)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });
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
                    col.Caption = fieldName.Equals("btn_overdue") ? "Over Due" : fieldName.Equals("btn_good") ? "Good" : col.Caption;

                    col.ColumnEdit = fieldName.Equals("btn_overdue") ? repositoryItemButtonEdit1 : fieldName.Equals("btn_good") ? repositoryItemButtonEdit2 : fieldName.Equals("remarks") || fieldName.Equals("issued_remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;
                    col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("tempering_time") || fieldName.Equals("overdue") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") || fieldName.Equals("due") || fieldName.Equals("duedate") || fieldName.Equals("start_transfer_date") || fieldName.Equals("end_transfer_date") || fieldName.Equals("due_start") || fieldName.Equals("due_end") || fieldName.Equals("issued_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                    col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("tempering_time") || fieldName.Equals("overdue") ? "{0:#,0.000}" : fieldName.Equals("transdate") || fieldName.Equals("due") || fieldName.Equals("duedate") || fieldName.Equals("issued_date") || fieldName.Equals("start_transfer_date") || fieldName.Equals("end_transfer_date") || fieldName.Equals("due_start") || fieldName.Equals("due_end") ? "yyyy-MM-dd HH:mm:ss" : "";

                    col.Visible = !(fieldName.Equals("id") || fieldName.Equals("is_overdue") || fieldName.Equals("issued_id") || fieldName.Equals("reference_color") || fieldName.Equals("issued_reference_color"));


                    //fonts
                    FontFamily fontArial = new FontFamily("Arial");
                    col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                    col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                    //fixed column
                    col.Fixed = fieldName.Equals("btn_good") || fieldName.Equals("btn_overdue") ? FixedStyle.Right : fieldName.Equals("reference") || fieldName.Equals("transdate") ? FixedStyle.Left : FixedStyle.None;
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
            Console.WriteLine("H4");
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

        public void isOverDue(bool isGood)
        {
            int id = 0, intTemp = 0;
            id = int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            Remarks.isSubmit = false;
            Remarks rem = new Remarks();
            rem.ShowDialog();
            if (Remarks.isSubmit)
            {
                JObject joBody = new JObject();
                joBody.Add("is_overdue", isGood);
                joBody.Add("remarks", Remarks.rem);
                string sResult = apic.loadData("/api/inv/trfr/tempering/isoverdue/", id.ToString(), "application/json", joBody.ToString(), RestSharp.Method.PUT, true);
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
                            apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                            bg(backgroundWorker1);
                        }
                    }
                }
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            isOverDue(true);   
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
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
                int issuedID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString() == "" ? 0 : !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) : 0;
                Production_IssueProduction_Items.isSubmit = false;
                Production_IssueProduction_Items frm = new Production_IssueProduction_Items("Closed");
                frm.selectedID = issuedID;
                frm.ShowDialog();
                if (Production_IssueProduction_Items.isSubmit)
                {
                    bg(backgroundWorker1);
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
            int overDue = 0, intTemp = 0;
            bool isOverDue = false, boolTemp = false;
            isOverDue = gridView1.GetRowCellValue(e.RowHandle, "is_overdue") == null ? boolTemp : bool.TryParse(gridView1.GetRowCellValue(e.RowHandle, "is_overdue").ToString(), out boolTemp) ? Convert.ToBoolean(gridView1.GetRowCellValue(e.RowHandle, "is_overdue").ToString()) : boolTemp;
            if (e.Column.FieldName == "duedate" && string.IsNullOrEmpty(gMode.Trim()))
            {
                overDue = Int32.TryParse(gridView1.GetRowCellValue(e.RowHandle, "overdue").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "overdue").ToString()) : intTemp;
                if (overDue > 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(251, 255, 191);
                }
            }
            else if (!string.IsNullOrEmpty(gMode.Trim()) && !isOverDue)
            {
                e.Appearance.BackColor = Color.FromArgb(207, 255, 191);
            }
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;

            if (e.Column.FieldName.Equals("reference"))
            {
                string referenceCol = gridView1.GetRowCellValue(e.RowHandle, "reference_color").ToString();
                Color col = ColorTranslator.FromHtml(referenceCol);
                e.Appearance.BackColor = col;
            }
            if (e.Column.FieldName.Equals("issued_reference"))
            {
                string referenceCol = gridView1.GetRowCellValue(e.RowHandle, "issued_reference_color").ToString();
                Color col = ColorTranslator.FromHtml(referenceCol);
                e.Appearance.BackColor = col;
            }
            if (e.Column.FieldName.Equals("issued_reference"))
            {
                foreach (DataRow row in dtColor.Rows)
                {
                    int index = 0;
                    if (!string.IsNullOrEmpty(row["color2"].ToString()))
                    {
                        index = int.TryParse(row["index2"].ToString(), out intTemp) ? Convert.ToInt32(row["index2"].ToString()) : intTemp;
                        if (index == e.RowHandle)
                        {
                            //Color color = new Color(), colorTemp = new Color();
                            //Console.WriteLine(row["color"].ToString());
                            //e.Appearance.BackColor = ColorTranslator.FromHtml(row["color"].ToString());
                            //Color.from

                            e.Appearance.BackColor = ColorTranslator.FromHtml("#" + row["color2"].ToString());
                        }
                    }
                }
            }
            else if (e.Column.FieldName.Equals("reference"))
            {
                string currentRef = gridView1.GetRowCellValue(e.RowHandle, "reference") == null ? "" : gridView1.GetRowCellValue(e.RowHandle, "reference").ToString();
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    bool isSameRef = (gridView1.GetRowCellValue(i, "reference").ToString() == currentRef) && (i != e.RowHandle);
                    e.Appearance.BackColor = isSameRef ? Color.Orange : e.Appearance.BackColor;

                }
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

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            isOverDue(false);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
         

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
