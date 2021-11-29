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
using AB.API_Class;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class PackingBinsHistory : Form
    {
        public PackingBinsHistory(int id)
        {
            InitializeComponent();
            selectedID = id;
        }
        int selectedID = 0;
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        color_class colorc = new color_class();
        int currentColorIndex = 0;
        DataTable dtColor = new DataTable();
        private void PackingBinsHistory_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtColor.Columns.Add("index", typeof(int));
            dtColor.Columns.Add("color", typeof(string));
            bg();
        }

        public void loadData()
        {
         try
            {
                string sParams = selectedID.ToString();
                string sResult = apic.loadData("/api/inv/packing_bins/history/", sParams, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResponse = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResponse["data"];
                    DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                    currentColorIndex = 0;
                    color_class colorc = new color_class();
                    dtColor.Rows.Clear();
                    foreach (DataRow row in dtData.Rows)
                    {
                        string currentRef = row["issued_reference"].ToString();
                        foreach (DataRow row2 in dtData.Rows)
                        {
                            currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                            string currentRef1 = row2["issued_reference"].ToString();
                            bool v =string.IsNullOrEmpty(currentRef.Trim()) || string.IsNullOrEmpty(currentRef1.Trim()) ? false : (currentRef == currentRef1) && (dtData.Rows.IndexOf(row) != dtData.Rows.IndexOf(row2));
                            if (v)
                            {
                                Color cc = colorc.c[currentColorIndex];
                                string hex = string.Format("{0:X2}{1:X2}{2:X2}", cc.R, cc.G, cc.B);
                                dtColor.Rows.Add(dtData.Rows.IndexOf(row), hex);
                                dtColor.Rows.Add(dtData.Rows.IndexOf(row2), hex);
                            }
                            else if (currentRef != currentRef1)
                            {
                                currentColorIndex++;
                            }
                        }
                    }
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
                                string s = v.Replace("_", " ");
                                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                                col.ColumnEdit = repositoryItemTextEdit1;
                                col.DisplayFormat.FormatType = fieldName.Equals("issued_qty") || fieldName.Equals("prod_receipt_quantity") ? DevExpress.Utils.FormatType.Numeric : fieldName.Equals("issued_date") || fieldName.Equals("prod_receipt_date") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                                col.DisplayFormat.FormatString = fieldName.Equals("issued_qty") || fieldName.Equals("prod_receipt_quantity") ? "{0:#,0.000}" : fieldName.Equals("issued_date") || fieldName.Equals("prod_receipt_date") ? "yyyy-MM-dd HH:mm:ss" : "";
                                col.Visible = !(fieldName.Equals("issued_id") || fieldName.Equals("prod_receipt_id"));
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
            if (e.Column.FieldName.Equals("issued_reference"))
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
    }
}
