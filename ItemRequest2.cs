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
using RestSharp;
using AB.API_Class.Warehouse;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AB.API_Class;

namespace AB
{
    public partial class ItemRequest2 : Form
    {
        public ItemRequest2(string docStatus)
        {
            InitializeComponent();
            gDocStatus = docStatus;
        }
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        warehouse_class wahousesc = new warehouse_class();
        devexpress_class devc = new devexpress_class();
        string gDocStatus = "";
        DataTable dtBranches = new DataTable(), dtPlant = new DataTable();
        int currentColorIndex = 0;
        DataTable dtColor = new DataTable();
        private void ItemRequest2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
            //dgv.Columns["btnViewRemarks"].Visible = gForType.Equals("Closed");
            dtTransDate.Value = DateTime.Now;
            loadPlant();
            loadData();
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

        public void loadBranches(DevExpress.XtraEditors.ComboBoxEdit cmb)
        {
            string sPlant = "?plant=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));
            string sResult = apic.loadData("/api/branch/get_all", sPlant, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtBranches = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    cmb.Properties.Items.Clear();
                    cmb.Properties.Items.Add("All");
                    foreach (DataRow row in dtBranches.Rows)
                    {
                        cmb.Properties.Items.Add(row["name"].ToString());
                    }
                    string currentBranch = (string)Login.jsonResult["data"]["branch"];
                    currentBranch = apic.findValueInDataTable(dtBranches, currentBranch, "name", "code");
                    cmb.SelectedIndex = cmb.Name.Equals("cmbToBranch") ? 0 : cmb.Properties.Items.IndexOf(currentBranch) > 0 ? cmb.Properties.Items.IndexOf(currentBranch) : 0;
                }
            }
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0, transferID = 0;
            id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
            transferID = gridView1.GetFocusedRowCellValue("transfer_id") == null ? 0 : int.TryParse(gridView1.GetFocusedRowCellValue("transfer_id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("transfer_id")) : intTemp;
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            if (selectedColumnText.Equals("reference"))
            {
                ItemRequest_Items2.isSubmit = false;
                string docStatusEncode = gDocStatus.Equals("O") ? "Open" : gDocStatus.Equals("C") ? "Closed" : gDocStatus.Equals("N") ? "Cancelled" : "";
                ItemRequest_Items2 frm = new ItemRequest_Items2(id, docStatusEncode);
                frm.ShowDialog();
                if (ItemRequest_Items2.isSubmit)
                {
                    loadData();
                }
            }
            else if (selectedColumnText.Equals("transfer_ref"))
            {
                TransferItem_Details.isSubmit = false;
                TransferItem_Details frm = new TransferItem_Details(transferID);
                frm.ShowDialog();
                if (TransferItem_Details.isSubmit)
                {
                    loadData();
                }
            }
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches(cmbFromBranch);
            loadBranches(cmbToBranch);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0;
            id = gridView1.GetFocusedRowCellValue("id") == null ? 0 : int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id")) : intTemp;
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            string remarksURL = "/api/inv/item_request/remarks/";
            string remarksByIdURL = "/api/inv/item_request/remarks/get_by_id/";
            if (selectedColumnText.Equals("view_remarks"))
            {
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        private void checkTransDate_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void loadData()
        {
            string sPlant = "&plant=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));

            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));

            string sDocStatus = "?docstatus=" + gDocStatus;
            //string sConfirmed = gForType.Equals("Cls")  ? "&confirm=" : "&confirm=1";
            //string sDueDate = (!checkDueDate.Checked ? "&duedate=" : "&duedate=" + dtDueDate.Value.ToString("yyyy-MM-dd"));
            string sTransDate = (!checkTransDate.Checked ? "&transdate=" : "&transdate=" + dtTransDate.Value.ToString("yyyy-MM-dd"));
            string sfromWarehouse = (cmbFromBranch.SelectedIndex.Equals(-1) ? "" : cmbFromBranch.SelectedIndex.Equals(0) || cmbFromBranch.Text.ToLower() == "all" ? "&from_branch=" : "&from_branch=" + apic.findValueInDataTable(dtBranches, cmbFromBranch.Text, "name", "code"));
            string stoWarehouse = (cmbToBranch.SelectedIndex.Equals(-1) ? "" : cmbToBranch.SelectedIndex.Equals(0) || cmbToBranch.Text.ToLower() == "all" ? "&to_branch=" : "&to_branch=" + apic.findValueInDataTable(dtBranches, cmbToBranch.Text, "name", "code"));
            string sParams = sDocStatus + sfromWarehouse + stoWarehouse + sTransDate + sPlant;
            string sResult = apic.loadData("/api/inv/item_request/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    DataTable dtNewData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    
                    currentColorIndex = 0;
                    color_class colorc = new color_class();
                    dtColor.Rows.Clear();
                    foreach (DataRow row in dtNewData.Rows)
                    {
                        string currentRef = row["reference"].ToString();
                        foreach (DataRow row2 in dtNewData.Rows)
                        {
                            currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                            string currentRef1 = row2["reference"].ToString();
                            bool v = (currentRef == currentRef1) && (dtNewData.Rows.IndexOf(row) != dtNewData.Rows.IndexOf(row2));
                            if (v)
                            {
                                Color cc = colorc.c[currentColorIndex];
                                string hex = string.Format("{0:X2}{1:X2}{2:X2}", cc.R, cc.G, cc.B);
                                dtColor.Rows.Add(dtNewData.Rows.IndexOf(row), hex);
                                dtColor.Rows.Add(dtNewData.Rows.IndexOf(row2), hex);
                            }
                            else if (currentRef != currentRef1)
                            {
                                currentColorIndex++;
                            }
                        }
                    }

                    if (dtNewData.Rows.Count > 0 && (gDocStatus.Equals("C") || gDocStatus.Equals("N")))
                    {
                        dtNewData.Columns.Add("view_remarks");
                    }
                    dtNewData.SetColumnsOrder("transdate", "reference", "from_branch", "to_branch", "remarks", "transfer_ref", "transfer_date","view_remarks");
                    gridControl1.DataSource = dtNewData;
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtNewData;

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
                            string v = fieldName.Equals("transfer_ref") ? "transfer_reference" : fieldName.Equals("from_branch") ? "from_department" : fieldName.Equals("to_branch") ? "to_department" : col.GetCaption();
                            string s = v.Replace("_", " ");
                            col.Caption =  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("view_remarks") ? repositoryItemButtonEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("transdate") || fieldName.Equals("transfer_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("transdate") || fieldName.Equals("transfer_date") ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.Visible = !(fieldName.Equals("transfer_id") || fieldName.Equals("id") || fieldName.Equals("docstatus"));

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

    }
}
