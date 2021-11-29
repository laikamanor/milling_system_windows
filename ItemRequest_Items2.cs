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

namespace AB
{
    public partial class ItemRequest_Items2 : Form
    {
        public ItemRequest_Items2(int id,string docStatus)
        {
            InitializeComponent();
            selectedID = id;
            gDocStatus = docStatus;
        }
        int selectedID = 0;
        string gDocStatus = "", fromBranch = "", toBranch = "", remarks = "";
        DataTable dtRequestRows = new DataTable();
        devexpress_class devc = new devexpress_class();
        api_class apic = new api_class();
        public static bool isSubmit = false;
        private void ItemRequest_Items2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            btnCloseTransaction.Visible = gDocStatus.Equals("Open");
            btnPrint.Location = btnCloseTransaction.Visible ? btnPrint.Location : btnCloseTransaction.Location;
            bg();
        }
        public void loadData()
        {
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));
            string sID = selectedID.ToString();
            string sParams = sID;
            string sResult = apic.loadData("/api/inv/item_request/details/", sParams, "", "", Method.GET, true);
            Console.WriteLine(sResult);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                DateTime dtTemp = new DateTime();
                JObject joResponse = JObject.Parse(sResult);
                lblDueDate.Invoke(new Action(delegate ()
                {
                    lblDueDate.Text = joResponse["data"]["due_date"] == null ? "" : DateTime.TryParse(joResponse["data"]["due_date"].ToString(), out dtTemp) ? Convert.ToDateTime(joResponse["data"]["due_date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") : "";
                }));
                lblRequestDate.Invoke(new Action(delegate ()
                {
                    lblRequestDate.Text = joResponse["data"]["date_created"] == null ? "" : DateTime.TryParse(joResponse["data"]["date_created"].ToString(), out dtTemp) ? Convert.ToDateTime(joResponse["data"]["date_created"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") : "";
                }));
                lblReference.Invoke(new Action(delegate ()
                {
                    lblReference.Text = joResponse["data"]["date_created"] == null ? "" : joResponse["data"]["reference"].ToString();
                }));
                fromBranch = joResponse["data"]["from_branch"] == null ? "" : joResponse["data"]["from_branch"].ToString();
                toBranch = joResponse["data"]["to_branch"] == null ? "" : joResponse["data"]["to_branch"].ToString();
                remarks = joResponse["data"]["remarks"] == null ? "" : joResponse["data"]["remarks"].ToString();
                JObject joData = JObject.Parse(joResponse["data"].ToString());
                JArray jaData = (JArray)joData["request_rows"];
                dtRequestRows = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                dtRequestRows.SetColumnsOrder("item_code", "uom", "quantity", "deliverqty", "from_branch", "to_branch");
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = dtRequestRows;


                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                        //auto complete
                        string[] suggestions = { "item_code", };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);

                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = fieldName.Equals("quantity") ? "request_quantity" : fieldName.Equals("deliverqty") ? "transffered_quantity" : col.GetCaption();
                            string s = v.Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("deliverqty") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                            col.Visible = fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("deliverqty") || fieldName.Equals("uom") || fieldName.Equals("from_branch") || fieldName.Equals("to_branch");

                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                        }
                        gridView1.BestFitColumns();
                    }));
                }
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("from_branch", typeof(string));
            dt.Columns.Add("to_branch", typeof(string));
            dt.Columns.Add("item_code", typeof(string));
            dt.Columns.Add("quantity", typeof(double));
            dt.Columns.Add("deliverqty", typeof(double));
            dt.Columns.Add("reference", typeof(string));
            dt.Columns.Add("remarks", typeof(string));
            dt.Columns.Add("transdate", typeof(string));

            foreach(DataRow row in dtRequestRows.Rows)
            {
                dt.Rows.Add(row["from_branch"].ToString(), row["to_branch"].ToString(), row["item_code"].ToString(), Convert.ToDouble(row["quantity"].ToString()), Convert.ToDouble(row["deliverqty"].ToString()), lblReference.Text, remarks, lblRequestDate.Text);
            }
            crItemRequestt frm = new crItemRequestt(dt);
            frm.ShowDialog();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnCloseTransaction_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want close this transaction?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Remarks remarkss = new Remarks();
                remarkss.ShowDialog();
                if (Remarks.isSubmit)
                {
                    string remarks = Remarks.rem;
                    JObject jObjectBody = new JObject();
                    jObjectBody.Add("remarks", remarks);
                    string sResult = apic.loadData("/api/inv/item_request/manual_close/", selectedID.ToString(), "application/json", jObjectBody.ToString(), RestSharp.Method.PUT, true);
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
                                isSubmit = true;
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
