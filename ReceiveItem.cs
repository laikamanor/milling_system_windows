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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using RestSharp;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class ReceiveItem : Form
    {
        public ReceiveItem(string docStatus)
        {
            InitializeComponent();
            gDocStatus = docStatus;
        }
        public string gDocStatus = "";
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        DataTable dtPlant = new DataTable(), dtDepartment = new DataTable(), dtWarehouse = new DataTable();
        private void ReceiveItem_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;

            dtFromDate.Visible = checkFromDate.Checked = gDocStatus.Equals("O") ? false : true;
            checkToDate.Checked = true;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;

            loadPlant();
            loadWarehouse(cmbToWhse);
            bg();
        }

        public void loadData()
        {
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));
            bool cFromDate = false, cToDate = false;
            string sBranch = "?branch=", sFromDate = "&from_date=", sToDate = "&to_date=", sFromTime = "&from_time=", sToTime = "&to_time=", sFromWhse = "&from_whse=", sToWhse = "&to_whse=", sDocStatus = "&docstatus=" + gDocStatus;
            checkFromDate.Invoke(new Action(delegate ()
            {
                cFromDate = checkFromDate.Checked;
            }));
            checkToDate.Invoke(new Action(delegate ()
            {
                cToDate = checkToDate.Checked;
            }));
            cmbPlant.Invoke(new Action(delegate ()
            {
                string branchCode = apic.findValueInDataTable(dtDepartment, cmbDepartment.Text, "name", "code");
                sBranch += branchCode;
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
                sFromTime += cmbFromTime.Text;
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime += cmbToTime.Text;
            }));
            cmbFromWhse.Invoke(new Action(delegate ()
            {
                sFromWhse += apic.findValueInDataTable(dtWarehouse, cmbFromWhse.Text, "whsename", "whsecode");
            }));
            cmbToWhse.Invoke(new Action(delegate ()
            {
                sToWhse += apic.findValueInDataTable(dtWarehouse, cmbToWhse.Text, "whsename", "whsecode");
            }));
            string sParams = sBranch + sFromDate + sToDate + sFromTime + sToTime + sFromWhse + sToWhse + sDocStatus;
            string sResult = apic.loadData("/api/inv/recv/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                AutoCompleteStringCollection auto = new AutoCompleteStringCollection();

                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        string[] columnVisible = new string[]
                        {
                            "transdate", "reference", "from_whse", "to_whse", "transtype", "platenum", "shift","driver", "vessel", "agi_truck_scale", "chti_truck_scale", "remarks","base_ref"
                        };
                        dtData.SetColumnsOrder(columnVisible);
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
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "n2" : fieldName.Equals("transdate")  ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.Visible = fieldName.Equals("transdate") || fieldName.Equals("reference") || fieldName.Equals("from_whse") || fieldName.Equals("to_whse") || fieldName.Equals("base_ref") || fieldName.Equals("platenum") || fieldName.Equals("shift") || fieldName.Equals("agi_truck_scale") || fieldName.Equals("chti_truck_scale") || fieldName.Equals("vessel") || fieldName.Equals("driver") || fieldName.Equals("remarks") || fieldName.Equals("base_ref");

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
                        //auto complete
                        string[] suggestions = { "reference" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                    }));
                }
            }
        }

        public void loadWarehouse(DevExpress.XtraEditors.ComboBoxEdit cmb)
        {
            string branchCode = cmb.Name.Equals("cmbToWhse") ? "" : apic.findValueInDataTable(dtDepartment, cmbDepartment.Text, "name", "code");
            cmb.Properties.Items.Clear();
            cmb.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/whse/get_all", "?branch=" + branchCode, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    if (cmb.Name.Equals("cmbToWhse"))
                    {
                        dtWarehouse = dt;
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        cmb.Properties.Items.Add(row["whsename"].ToString());
                    }
                    string plantCode = Login.jsonResult["data"]["whse"].ToString();
                    string plantName = cmb.Name.Equals("cmbToWhse") ? "" : apic.findValueInDataTable(dt, plantCode, "whsecode", "whsename");
                    cmb.SelectedIndex = cmb.Properties.Items.IndexOf(plantName) <= 0 ? 0 : cmb.Properties.Items.IndexOf(plantName);
                }
            }
            else
            {
                cmb.SelectedIndex = 0;
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

        public void loadDepartment()
        {
            string plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            cmbDepartment.Properties.Items.Clear();
            cmbDepartment.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtDepartment = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtDepartment.Rows)
                    {
                        cmbDepartment.Properties.Items.Add(row["name"].ToString());
                    }
                    string currentBranchCode = Login.jsonResult["data"]["branch"].ToString();
                    string currentBranchName = apic.findValueInDataTable(dtDepartment, currentBranchCode, "code", "name");
                    cmbDepartment.SelectedIndex = cmbDepartment.Properties.Items.IndexOf(currentBranchName) <= 0 ? 0 : cmbDepartment.Properties.Items.IndexOf(currentBranchName);
                }
            }
            else
            {
                cmbDepartment.SelectedIndex = 0;
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDepartment();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void checkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkFromDate.Checked;
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadWarehouse(cmbFromWhse);
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            int id = 0, baseID = 0, intTemp = 0;
            id = int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            baseID = int.TryParse(gridView1.GetFocusedRowCellValue("base_id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("base_id").ToString()) : intTemp;
            if (selectedColumnfieldName.Equals("reference"))
            {
                Receiveitem_Details.isSubmit = false;
                Receiveitem_Details frm = new Receiveitem_Details(id);
                frm.ShowDialog();
                if (Receiveitem_Details.isSubmit)
                {
                    bg();
                }
            }
            else if (selectedColumnfieldName.Equals("base_ref"))
            {
                Receiveitem_Details.isSubmit = false;
                Receiveitem_Details frm = new Receiveitem_Details(baseID);
                frm.ShowDialog();
                if (Receiveitem_Details.isSubmit)
                {
                    bg();
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

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }
    }
}
