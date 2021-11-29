using DevExpress.XtraGrid.Columns;
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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class WheatTypeReportDetails : Form
    {
        public WheatTypeReportDetails(DataTable dt)
        {
            InitializeComponent();
            gDt = dt;
        }
        DataTable gDt = new DataTable();
        devexpress_class devc = new devexpress_class();
        private void WheatTypeReportDetails_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadData();
        }

        public void loadData()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            gridControl1.DataSource = null;
            gridControl1.DataSource = gDt;
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
                col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "{0:#,0.000}" : fieldName.Equals("transdate") ? "yyyy-MM-dd HH:mm:ss" : "";
                col.Visible = !fieldName.Equals("id");


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

            //auto complete
            string[] suggestions = { "reference" };
            string suggestConcat = string.Join(";", suggestions);
            gridView1.OptionsFind.FindFilterColumns = suggestConcat;
            devc.loadSuggestion(gridView1, gridControl1, suggestions);
            gridView1.BestFitColumns();
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }
    }
}
