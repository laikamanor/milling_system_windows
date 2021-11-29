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
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class QA_Remarks : Form
    {
        public QA_Remarks(string url,string urlAddRemarks, string urlRemarksById)
        {
            gURL = url;
            gURLAddRemarks = urlAddRemarks;
            gURLRemarksById = urlRemarksById;
            InitializeComponent();
        }
        public int selectedID = 0;
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        string gURL = "", gURLAddRemarks = "", gURLRemarksById = "";
        public static bool isSubmit = false;
        private void QA_Remarks_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            btnAddRemarks.Visible = !string.IsNullOrEmpty(gURLAddRemarks.Trim());
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
            string sID = selectedID.ToString();
            string sParams = sID;
            string sResult = apic.loadData(gURL, sParams, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridView1.OptionsView.ColumnAutoWidth = false;

                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();
                        gridControl1.DataSource = dt;
                        //auto complete
                        string[] suggestions = { "username" };
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
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.Visible = fieldName.Equals("transdate") || fieldName.Equals("remarks") || fieldName.Equals("username") ? true : false;
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatString = fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;

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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void repositoryItemMemoEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (selectedColumnText.Equals("remarks") && !string.IsNullOrEmpty(gURLRemarksById.Trim()))
            {
                Remarkss frm = new Remarkss(gURLRemarksById);
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

        private void btnAddRemarks_Click(object sender, EventArgs e)
        {
            Remarks.isSubmit = false;
            Remarks.rem = "";
            Remarks frm = new Remarks();
            frm.ShowDialog();
            if (Remarks.isSubmit)
            {
                JObject joBody = new JObject();
                joBody.Add("remarks", Remarks.rem);
                string sResult = apic.loadData(gURLAddRemarks, selectedID.ToString(), "application/json", joBody.ToString(), RestSharp.Method.POST, true);
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        string msg = joResult["message"].ToString();
                        bool isSuccess = (bool)joResult["success"];
                        MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        if (isSuccess)
                        {
                            isSubmit = true;
                            bg();
                        }
                    }
                }
            }
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
