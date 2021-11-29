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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;

namespace AB
{
    public partial class WheatUsageReport : Form
    {
        public WheatUsageReport()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtPlant = new DataTable(), dtMill = new DataTable();
        private void WheatUsageReport_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtRWBFromDate.EditValue = dtRWBToDate.EditValue = dtCWBFromDate.EditValue = dtCWBToDate.EditValue = dtIssuedFromDate.EditValue = dtIssuedToDate.EditValue = dtProdRecFromDate.EditValue = dtProdRecToDate.EditValue = DateTime.Now;
            cmbCWBFromTime.SelectedIndex = cmbRWBFromTime.SelectedIndex = cmbIssuedFromTime.SelectedIndex = cmbProdRecFromTime.SelectedIndex = 0;
            cmbRWBToTime.SelectedIndex = cmbCWBToTime.SelectedIndex = cmbIssuedToTime.SelectedIndex = cmbProdRecToTime.SelectedIndex = cmbRWBToTime.Properties.Items.Count - 1;
            loadPlant();
            loadMill();
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
        public void loadMill()
        {
            cmbMill.Properties.Items.Clear();
            cmbMill.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/mill/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtMill = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtMill.Rows)
                    {
                        cmbMill.Properties.Items.Add(row["name"].ToString());
                    }
                }
            }
            cmbMill.SelectedIndex = 0;
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
 
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

        public bool bDelegateControl(CheckBox c)
        {
            bool result = false;
            c.Invoke(new Action(delegate ()
            {
                result = c.Checked;
            }));
            return result;
        }

        public void loadData()
        {
            string sPlant = "?plant=", sMill = "&mill=", sRWBFromDate = "&rwb_from_date=", sRWBToDate = "&rwb_to_date=", sRWBFromTime = "&rwb_from_time=", sRWBToTime = "&rwb_to_time=", sCWBFromDate = "&cwb_from_date=", sCWBToDate = "&cwb_to_date=", sCWBFromTime = "&cwb_from_time=", sCWBToTime = "&cwb_to_time=", sIssuedFromDate = "&issued_from_date=", sIssuedToDate = "&issued_to_date=", sIssuedFromTime = "&issued_from_time=", sIssuedToTime = "&issued_to_time=", sProdRecFromDate = "&prod_rec_from_date=", sProdRecToDate = "&prod_rec_to_date=", sProdRecFromTime = "&prod_rec_from_time=", sProdRecToTime = "&prod_rec_to_time=";
            string plantCode = apic.findValueInDataTable(dtPlant, delegateControl(cmbPlant), "name", "code");

            bool cRWBFromDate = bDelegateControl(chckRWBFromDate), cRWBToDate = bDelegateControl(chckRWBToDate), cRWBFromTime = bDelegateControl(chckRWBFromTime), cRWBToTime = bDelegateControl(chckRWBToTime), cCWBFromDate = bDelegateControl(chckCWBFromDate), cCWBToDate = bDelegateControl(chckCWBToDate), cCWBFromTime = bDelegateControl(chckCWBFromTime), cCWBToTime = bDelegateControl(chckCWBToTime), cIssuedFromDate = bDelegateControl(chckIssuedFromDate), cIssuedToDate = bDelegateControl(chckIssuedToDate), cIssuedFromTime = bDelegateControl(chckIssuedFromTime), cIssuedToTime = bDelegateControl(chckIssuedToTime), cProdRecFromDate = bDelegateControl(chckProdRecFromDate), cProdRecToDate = bDelegateControl(chckProdRecToDate), cProdRecFromTime = bDelegateControl(chckProdRecFromTime), cProdRecToTime = bDelegateControl(chckProdRecToTime);

            //plant
            sPlant += plantCode;
            //mill
            sMill += apic.findValueInDataTable(dtMill, delegateControl(cmbMill), "name", "code");
            //rwb date
            sRWBFromDate += cRWBFromDate ? delegateControl(dtRWBFromDate) : "";
            sRWBToDate += cRWBToDate ? delegateControl(dtRWBToDate) : "";
            //rwb time
            sRWBFromTime += cRWBFromTime ? delegateControl(cmbRWBFromTime) : "";
            sRWBToTime += cRWBToTime ? delegateControl(cmbRWBToTime) : "";
            //cwb date
            sCWBFromDate += cCWBFromDate ? delegateControl(dtCWBFromDate) : "";
            sCWBToDate += cCWBToDate ? delegateControl(dtCWBToDate) : "";
            //cwb time
            sCWBFromTime += cCWBFromTime ? delegateControl(cmbCWBFromTime) : "";
            sCWBToTime += cCWBToTime ? delegateControl(cmbCWBToTime) : "";
            //issued date
            sIssuedFromDate += cIssuedFromDate ? delegateControl(dtIssuedFromDate) : "";
            sIssuedToDate += cIssuedToDate ? delegateControl(dtIssuedToDate) : "";
            //issued time
            sIssuedFromTime += cIssuedFromTime ? delegateControl(cmbIssuedFromTime) : "";
            sIssuedToTime += cIssuedToTime ? delegateControl(cmbIssuedToTime) : "";
            //prod rec date
            sProdRecFromDate += cProdRecFromDate ? delegateControl(dtProdRecFromDate) : "";
            sProdRecToDate += cProdRecToDate ? delegateControl(dtProdRecToDate) : "";
            //issued time
            sProdRecFromTime += cProdRecFromTime ? delegateControl(cmbProdRecFromTime) : "";
            sProdRecToTime += cProdRecToTime ? delegateControl(cmbProdRecToTime) : "";
            string sParams = sPlant + sMill + sRWBFromDate + sRWBToDate + sRWBFromTime + sRWBToTime + sCWBFromDate + sCWBToDate + sCWBFromTime + sCWBToTime + sIssuedFromDate + sIssuedToDate + sIssuedFromTime + sIssuedToTime + sProdRecFromDate + sProdRecToDate + sProdRecFromTime + sProdRecToTime;
            string sResult = apic.loadData("/api/report/wheat_usage", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JObject joData = (JObject)joResponse["data"];
                JArray jaWheatUsageReport = (JArray)joData["wheat_usage_report"];
                JArray jaReceiveFromProdReport = (JArray)joData["receive_from_prod_report"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaWheatUsageReport.ToString(), (typeof(DataTable)));
                DataTable dtData2 = (DataTable)JsonConvert.DeserializeObject(jaReceiveFromProdReport.ToString(), (typeof(DataTable)));
                loadUi(dtData, gridControl1, gridView1,repositoryItemTextEdit1);
                loadUi(dtData2, gridControl2, gridView2, repositoryItemTextEdit2);
            }
        }

        public void loadUi(DataTable dtData, GridControl grid, GridView view,RepositoryItem item)
        {
            if (IsHandleCreated)
            {
                grid.Invoke(new Action(delegate ()
                {
                    grid.DataSource = null;
                    grid.DataSource = dtData;
                    view.OptionsView.ColumnAutoWidth = false;
                    view.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                    foreach (GridColumn col in view.Columns)
                    {
                        string v = col.GetCaption();
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                        col.ColumnEdit = item;
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = "{0:#,0.000}";
                        //fonts
                        FontFamily fontArial = new FontFamily("Arial");
                        col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                        col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                    }
                    view.BestFitColumns();

                    //auto complete
                    string[] suggestions = { "item_code" };
                    string suggestConcat = string.Join(";", suggestions);
                    view.OptionsFind.FindFilterColumns = suggestConcat;
                    devc.loadSuggestion(view, grid, suggestions);
                    view.BestFitColumns();
                }));
            }
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
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
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            string sParams = "?from_date="  + "&to_date=" + "&plant=" + plantCode + "&mode=raw_wheat_in";
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            if (selectedColumnText.Equals("raw_wheat_in"))
            {
                RawWheatIn frm = new RawWheatIn(sParams);
                frm.ShowDialog();
            }
        }
        private int hotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle,
            hotTrackRow2 = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
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
        private int HotTrackRow2
        {
            get
            {
                return hotTrackRow2;
            }
            set
            {
                if (hotTrackRow2 != value)
                {
                    int prevHotTrackRow = hotTrackRow2;
                    hotTrackRow2 = value;
                    gridView2.RefreshRow(prevHotTrackRow);
                    gridView2.RefreshRow(hotTrackRow2);

                    if (hotTrackRow2 >= 0)
                        gridControl2.Cursor = Cursors.Hand;
                    else
                        gridControl2.Cursor = Cursors.Default;
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

        private void chckRWBFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtRWBFromDate.Visible = chckRWBFromDate.Checked;
        }

        private void chckRWBToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtRWBToDate.Visible = chckRWBToDate.Checked;
        }

        private void chckRWBFromTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbRWBFromTime.Visible = chckRWBFromTime.Checked;
        }

        private void chckRWBToTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbRWBToTime.Visible = chckRWBToTime.Checked;
        }

        private void chckCWBFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCWBFromDate.Visible = chckCWBFromDate.Checked;
        }

        private void chckCWBToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtCWBToDate.Visible = chckCWBToDate.Checked;
        }

        private void chckCWBFromTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbCWBFromTime.Visible = chckCWBFromTime.Checked;
        }

        private void chckCWBToTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbCWBToTime.Visible = chckCWBToTime.Checked;
        }

        private void chckIssuedFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtIssuedFromDate.Visible = chckIssuedFromDate.Checked;
        }

        private void chckIssuedToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtIssuedToDate.Visible = chckIssuedToDate.Checked;
        }

        private void chckIssuedFromTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbIssuedFromTime.Visible = chckIssuedFromTime.Checked;
        }

        private void chckIssuedToTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbIssuedToTime.Visible = chckIssuedToTime.Checked;
        }

        private void chckProdRecFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtProdRecFromDate.Visible = chckProdRecFromDate.Checked;
        }

        private void chckProdRecToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtProdRecToDate.Visible = chckProdRecToDate.Checked;
        }

        private void chckProdRecFromTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbProdRecFromTime.Visible = chckProdRecFromTime.Checked;
        }

        private void chckProdRecToTime_CheckedChanged(object sender, EventArgs e)
        {
            cmbProdRecToTime.Visible = chckProdRecToTime.Checked;
        }

        private void gridView2_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow2 = info.RowHandle;
            else
                HotTrackRow2 = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow2)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }
    }
}
