using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;

namespace AB
{
    public partial class showWarehouse : Form
    {
        public showWarehouse(string itemCode, string URL, string sParams,string keyDisplayed, string keyValue, bool isView, bool isConfirm)
        {
            InitializeComponent();
            gItemCode = itemCode;
            gURL = URL;
            gParams = sParams;
            gKeyDisplayed = keyDisplayed;
            gKeyValue = keyValue;
            gIsView = isView;
            gIsConfirm = isConfirm;
        }
        bool gIsView = false, gIsConfirm = false;
        string gItemCode = "",gURL="", gParams = "", gKeyDisplayed = "", gKeyValue = "";
        public static string selectedWhse = "", selectedUom = "";
        public static bool isSubmit = false;
        api_class apic = new api_class();
        DataTable dtWarehouse = new DataTable(), dtAssignDept = new DataTable();
        private void showWarehouse_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Properties.Resources.logo2;
                btnView.Visible = gIsView;
                bg();
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle == HotTrackRow)
                    e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
                else
                    e.Appearance.BackColor = e.Appearance.BackColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

                if (info.InRowCell)
                    HotTrackRow = info.RowHandle;
                else
                    HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
           
            GridView view = sender as GridView;
            TextEdit edit = view.ActiveEditor as TextEdit;
            if (edit != null && view.IsFilterRow(view.FocusedRowHandle))
                edit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        }

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    this.Focus();
        //}
        //protected override void OnDeactivate(EventArgs e)
        //{
        //    base.OnDeactivate(e);
        //    this.Focus();
        //}

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    this.Focus();
        //}
        //protected override void OnDeactivate(EventArgs e)
        //{
        //    base.OnDeactivate(e);
        //    this.Focus();
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                loadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
                string keyDis = gridView1.GetFocusedRowCellValue(gKeyDisplayed).ToString();
                string uomm = gridView1.GetFocusedRowCellValue("uom") == null ? "" : gridView1.GetFocusedRowCellValue("uom").ToString();
                if (selectedColumnfieldName.Equals(gKeyDisplayed))
                {
                    if (gIsConfirm)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to select " + keyDis + "? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string findWhseCode = apic.findValueInDataTable(dtWarehouse, keyDis, gKeyDisplayed, gKeyValue);
                            selectedWhse = findWhseCode;
                            selectedUom = uomm;
                            isSubmit = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        string findWhseCode = apic.findValueInDataTable(dtWarehouse, keyDis, gKeyDisplayed, gKeyValue);
                        selectedWhse = findWhseCode;
                        selectedUom = uomm;
                        isSubmit = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //showAvailableQtyPerWhse frm = new showAvailableQtyPerWhse(gItemCode, "");
            //frm.ShowDialog();
        }

        public void loadData()
        {
            try
            {
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                    }));
                }
                string sResult = apic.loadData(gURL, gParams, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResponse = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResponse["data"];
                    dtWarehouse = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                    if (IsHandleCreated)
                    {
                        gridControl1.Invoke(new Action(delegate ()
                        {
                            gridControl1.DataSource = null;
                            gridControl1.DataSource = dtWarehouse;
                            string cap = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gKeyDisplayed.Trim().ToLower().Replace("_", ""));
                            foreach (GridColumn col in gridView1.Columns)
                            {
                                string fieldName = col.FieldName;

                                string v = col.GetCaption();
                                string s = col.GetCaption().Replace("_", " ");
                                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                                col.ColumnEdit = repositoryItemTextEdit1;
                                col.Visible = fieldName.Equals(gKeyDisplayed);
                                col.Caption = fieldName.Equals(gKeyDisplayed) ? cap : col.GetCaption();

                                //fonts
                                FontFamily fontArial = new FontFamily("Arial");
                                col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                                col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                            }
                            devexpress_class devc = new devexpress_class();
                            //auto complete
                            string[] suggestions = { gKeyDisplayed };
                            string suggestConcat = string.Join(";", suggestions);
                            Console.WriteLine(suggestConcat);
                            gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                            gridView1.OptionsFind.FindNullPrompt = "Search " + cap + "...";
                            devc.loadSuggestion(gridView1, gridControl1, suggestions);
                            gridView1.BestFitColumns();
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void bg()
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    closeForm();
                    Loading frm = new Loading();
                    frm.Show();
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void closeForm()
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "Loading")
                    {
                        frm.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                closeForm();
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }
    }
}
