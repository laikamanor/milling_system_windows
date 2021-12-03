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
using Newtonsoft.Json.Linq;
using AB.API_Class.Warehouse;
using Newtonsoft.Json;
using System.Globalization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class AdjustmentIn3 : Form
    {
        public AdjustmentIn3(string adjType, string tabName)
        {
            InitializeComponent();
            gAdjType = adjType;
            gTabName = tabName;
        }
        DataTable dtWarehouse, dtPlant = new DataTable(), dtBranch = new DataTable();
        string gAdjType = "", gTabName = "";
        utility_class utilityc = new utility_class();
        devexpress_class devc = new devexpress_class();
        api_class apic = new api_class();
        private void AdjustmentIn3_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtFromDate.EditValue = dtToDate.EditValue = DateTime.Now;
            checkDate.Checked = dtFromDate.Visible = true;
            this.Text = gAdjType.Equals("in") ? "Adjusment In" : "Adjustment Out";
            btnAdd.Visible = gTabName.Equals("Done") ? false : true;
            loadWarehouse();
            bg();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddAdjustmentIn addAdjustmentIn = new AddAdjustmentIn(gAdjType);
            addAdjustmentIn.ShowDialog();
            loadData();
        }

        public void loadWarehouse()
        {
            try
            {
                cmbWarehouse.Invoke(new Action(delegate ()
                {
                    cmbWarehouse.Properties.Items.Clear();
                }));
                string[] listAccess = { "isAdmin", "isSuperAdmin", "isManager" };
                string sResult = "";
                string sParams = "";
                sResult = apic.loadData("/api/whse/get_all", sParams, "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    dtWarehouse = apic.getDtDownloadResources(sResult, "data");
                }
                if (dtWarehouse.Rows.Count > 1)
                {
                    cmbWarehouse.Invoke(new Action(delegate ()
                    {
                        cmbWarehouse.Properties.Items.Add("All");
                    }));
                }
                foreach (DataRow row in dtWarehouse.Rows)
                {
                    cmbWarehouse.Invoke(new Action(delegate ()
                    {
                        cmbWarehouse.Properties.Items.Add(row["whsename"].ToString());
                    }));
                }
                cmbWarehouse.Invoke(new Action(delegate ()
                {
                    string whse = (string)Login.jsonResult["data"]["whse"];
                    string s = apic.findValueInDataTable(dtWarehouse, whse, "whsecode", "whsename");
                    cmbWarehouse.SelectedIndex = 0;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public string delegateControl(Control c)
        {
            string result = "";
            c.Invoke(new Action(delegate ()
            {
                result = c.Text;
            }));
            return result;
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            int intTemp = 0;
            string currentRef = gridView1.GetFocusedRowCellValue("reference") == null ? "" : gridView1.GetFocusedRowCellValue("reference").ToString();
            string currentRemarks = gridView1.GetFocusedRowCellValue("remarks") == null ? "" : gridView1.GetFocusedRowCellValue("remarks").ToString();
            int id = gridView1.GetFocusedRowCellValue("id") == null ? intTemp : int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            if (selectedColumnfieldName.Equals("reference"))
            {
                AdjustmentIn_Details.isSubmit = false;
                AdjustmentIn_Details ajdDetails = new AdjustmentIn_Details(gTabName, gAdjType,currentRemarks);
                ajdDetails.Text = this.Text + " Details";
                ajdDetails.selectedID = id;
                if(this.Text== "Adjustment Out")
                {
                    ajdDetails.lblReference.Text = currentRef;
                }
                ajdDetails.ShowDialog();
                if (AdjustmentIn_Details.isSubmit)
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

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        public void loadData()
        {
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));
            bool cFromDate = false, cToDate = false;
            checkDate.Invoke(new Action(delegate ()
            {
                cFromDate = checkDate.Checked;
            }));
            checkToDate.Invoke(new Action(delegate ()
            {
                cToDate = checkToDate.Checked;
            }));

            string selectedWhse = delegateControl(cmbWarehouse);
            string sFromDate = "?from_date=" + (cFromDate ? delegateControl(dtFromDate) : ""), sToDate = "&to_date=" + (cToDate ? delegateControl(dtToDate) : ""), sWarehouse = "&whsecode=" + apic.findValueInDataTable(dtWarehouse, selectedWhse, "whsename", "whsecode"), sSAP = gTabName.Equals("Done") ? "&sap_number=1" : "&sap_number=", sParams = "";
            sParams = sFromDate + sToDate + sWarehouse + sSAP;
            string sResult = apic.loadData("/api/inv_adj/" + gAdjType + "/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        //auto complete
                        string[] suggestions = { "reference" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                        gridView1.BestFitColumns();

                        //gridView1.OptionsView.ColumnAutoWidth = true;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("transdate") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "{0:#,0.000}" : fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.Visible = fieldName.Equals("transdate") || fieldName.Equals("reference") || fieldName.Equals("sap_number") || fieldName.Equals("remarks");
                  
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
                        //gridView1.OptionsView.RowAutoHeight = true;
                    }));
                }
            }
        }
    }
}
