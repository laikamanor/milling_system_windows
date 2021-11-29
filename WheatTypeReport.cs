using Newtonsoft.Json;
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
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class WheatTypeReport : Form
    {
        public WheatTypeReport()
        {
            InitializeComponent();
        }
        DataTable dtPlant = new DataTable();
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        private void WheatTypeReport_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            loadPlant();
            bg();
        }

        public void loadData()
        {
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));

            string sPlant = "?plant=", sFromDate = "&from_date=", sToDate = "&to_date=", sFromTime = "&from_time=", sToTime = "&to_time=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                string plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                sPlant += plantCode;
            }));
            dtFromDate.Invoke(new Action(delegate ()
            {
                sFromDate += dtFromDate.Text;
            }));
            dtToDate.Invoke(new Action(delegate ()
            {
                sToDate += dtToDate.Text;
            }));
            cmbFromTime.Invoke(new Action(delegate ()
            {
                sFromTime += cmbFromTime.Text;
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime += cmbToTime.Text;
            }));
            string sParams = sPlant + sFromDate + sToDate + sFromTime + sToTime;
            string sResult = apic.loadData("/api/report/production/wheat_type/report", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "{0:#,0.000}" : "";

                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                        }
                        gridView1.BestFitColumns();
                        //auto complete
                        string[] suggestions = { "item_code" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                        gridView1.BestFitColumns();
                    }));
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            string itemCode = gridView1.GetFocusedRowCellValue("item_code").ToString();
            if (selectedColumnfieldName.Equals("quantity"))
            {
                string sPlant = "?plant=" + apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code"), sFromDate = "&from_date=" + dtFromDate.Text, sToDate = "&to_date=" + dtToDate.Text, sFromTime = "&from_time=" + cmbFromTime.Text, sToTime = "&to_time=" + cmbToTime.Text, sItemCode = "&item_code=" + itemCode;
                string sParams = sPlant + sFromDate + sToDate + sFromTime + sToTime + sItemCode;
                string sResult = apic.loadData("/api/report/production/wheat_type/details/report?", sParams, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"] == null ? new JArray() : (JArray)joResult["data"];
                    DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                    WheatTypeReportDetails frm = new WheatTypeReportDetails(dtData);
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
        }
    }
}
