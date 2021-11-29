using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.API_Class.Items;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using RestSharp;
using AB.UI_Class;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Items_DX : Form
    {
        public Items_DX()
        {
            InitializeComponent();
        }
        item_class itemc = new item_class();

        private void Items_DX_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadData();
        }

        public void loadData()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt = itemc.loadData();
            if(dt.Rows.Count > 0)
            {
                dt.Columns.Add("btn_edit");
            }
            gridControl1.DataSource = dt;
            gridView1.OptionsView.ColumnAutoWidth = false;
            foreach (GridColumn col in gridView1.Columns)
            {
                string fieldName = col.FieldName;
                string v = col.GetCaption();
                string s = col.GetCaption().Replace("_", " ");
                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                col.ColumnEdit = fieldName.Equals("btn_edit") ? repositoryItemButtonEdit1 : repositoryItemTextEdit1;
                col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "n2" : "";
                col.Visible = (fieldName.Equals("item_code") || fieldName.Equals("item_name") || fieldName.Equals("item_group") || fieldName.Equals("uom") || fieldName.Equals("btn_edit"));
            }
            gridView1.BestFitColumns();
        }


        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.ShowDialog();
            if (AddItem.isSubmit)
            {
                loadData();
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            int id = 0, intTemp = 0;
            id = int.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            if (selectedColumnfieldName.Equals("btn_edit"))
            {
                EditItem.isSubmit = false;
                EditItem frm = new EditItem(id);
                frm.ShowDialog();
                if (EditItem.isSubmit)
                {
                    loadData();
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

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
    }
}
