using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AB
{
    public partial class viewToSaveInventory : Form
    {
        public viewToSaveInventory(DataTable dt)
        {
            InitializeComponent();
            gDt = dt;
        }
        DataTable gDt = new DataTable();

        private void viewToSaveInventory_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl1.DataSource = gDt;
            foreach (GridColumn col in gridView1.Columns)
            {
                string fieldName = col.FieldName;
                string v = col.GetCaption();
                string s = col.GetCaption().Replace("_", " ");
                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                col.ColumnEdit = fieldName.Equals("actual_count") ? repositoryItemTextEdit2 : fieldName.Equals("comments") ? repositoryItemTextEdit3 : repositoryItemTextEdit1;
                col.DisplayFormat.FormatType = fieldName.Equals("available") || fieldName.Equals("variance") || fieldName.Equals("actual_count") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                col.DisplayFormat.FormatString = fieldName.Equals("available") || fieldName.Equals("variance") || fieldName.Equals("actual_count") ? "{0:#,0.000}" : "";
                col.Visible = fieldName.Equals("whsecode") || fieldName.Equals("item_code") || fieldName.Equals("available") || fieldName.Equals("variance") || fieldName.Equals("actual_count") || fieldName.Equals("comments");

                col.Caption = fieldName.Equals("actual_count") ? "Input Actual Soundings" : col.Caption;

                col.SortMode = fieldName.Equals("item_code") || fieldName.Equals("whsecode") ? ColumnSortMode.Custom : ColumnSortMode.Default;

                col.Fixed = fieldName.Equals("item_code") || fieldName.Equals("whsecode") || fieldName.Equals("available") ? FixedStyle.Left : FixedStyle.None;

                //fonts
                FontFamily fontArial = new FontFamily("Arial");
                col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);


            }
            gridView1.BestFitColumns();

            var colComments = gridView1.Columns["comments"];
            if (colComments != null)
            {
                colComments.Width = 200;
            }
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {

        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //e.Cancel = true;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemTextEdit2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void repositoryItemTextEdit2_EditValueChanged(object sender, EventArgs e)
        {


            //double actualCount = gridView1.GetFocusedRowCellValue("actual_count") == null ? double.TryParse(gridView1.GetFocusedRowCellValue("actual_count").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetFocusedRowCellValue("actual_count").ToString()) : doubleTemp : doubleTemp;

            //double variance = gridView1.GetFocusedRowCellValue("variance") == null ? double.TryParse(gridView1.GetFocusedRowCellValue("variance").ToString(), out doubleTemp) ? Convert.ToDouble(gridView1.GetFocusedRowCellValue("variance").ToString()) : doubleTemp : doubleTemp;

            //MessageBox.Show(actualCount + "/" + available);
            //variance = actualCount - available;
            //Console.WriteLine(variance);
            //var col = gridView1.Columns["variance"];
            //gridView1.SetFocusedRowCellValue(col, variance.ToString("n3"));

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                double doubleTemp = 0.00;
                double available = Convert.ToDouble(gridView1.GetFocusedRowCellValue("available").ToString());
                string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
                var varCol = gridView1.Columns["variance"];
                if (selectedColumnfieldName.Equals("actual_count"))
                {
                    if (e.Value.ToString().Trim() == "")
                    {
                        gridView1.SetRowCellValue(e.RowHandle, varCol, null);
                        if (e.RowHandle >= 0)
                        {
                            int rowIndex = 0, intTemp = 0;
                            rowIndex = int.TryParse(gridView1.GetFocusedRowCellValue("row_index").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("row_index").ToString()) : intTemp;
                            if (rowIndex >= 0)
                            {
                                DataRow row = gDt.Rows[rowIndex];
                                if (row != null)
                                {
                                    row["actual_count"] = DBNull.Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        double actualCount = double.TryParse(e.Value.ToString(), out doubleTemp) ? Convert.ToDouble(e.Value.ToString()) : doubleTemp;

                        double variance = actualCount - available;
                        gridView1.SetRowCellValue(e.RowHandle, varCol, variance.ToString("n3"));

                        int rowIndex = 0, intTemp = 0;
                        rowIndex = int.TryParse(gridView1.GetFocusedRowCellValue("row_index").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("row_index").ToString()) : intTemp;
                        if (rowIndex >= 0)
                        {
                            DataRow row = gDt.Rows[rowIndex];
                            if (row != null)
                            {
                                row["actual_count"] = actualCount;
                            }
                        }
                    }
                }
                else if (selectedColumnfieldName.Equals("comments"))
                {
                    if (e.Value.ToString().Trim() == "")
                    {
                        if (e.RowHandle >= 0)
                        {
                            int rowIndex = 0, intTemp = 0;
                            rowIndex = int.TryParse(gridView1.GetFocusedRowCellValue("row_index").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("row_index").ToString()) : intTemp;
                            if (rowIndex >= 0)
                            {
                                DataRow row = gDt.Rows[rowIndex];
                                if (row != null)
                                {
                                    row["comments"] = DBNull.Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        int rowIndex = 0, intTemp = 0;
                        rowIndex = int.TryParse(gridView1.GetFocusedRowCellValue("row_index").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("row_index").ToString()) : intTemp;
                        if (rowIndex >= 0)
                        {
                            DataRow row = gDt.Rows[rowIndex];
                            if (row != null)
                            {
                                row["comments"] = e.Value.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //gridView1.Appearance.BackColor = Color.Red;
        }
        bool needCalculate = true;

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
            //if (e.Column.FieldName.Equals("whsecode"))
            //{
            //    Console.WriteLine("haha" + e.Appearance.BackColor + "/" + e.CellValue);
            //    if (e.Appearance.BackColor ==  Color.White)
            //    {
            //        if (e.CellValue.ToString() == "Mill-B PB 3")
            //        {
            //            if(e.Appearance.BackColor != gridView1.PaintAppearance.SelectedRow.BackColor)
            //            {
            //                Random r = new Random();
            //                e.Appearance.BackColor = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            //                Console.WriteLine("heheheh: " + e.Appearance.BackColor);
            //            }
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = e.Appearance.BackColor;

            //        }
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = e.Appearance.BackColor;
            //    }
            //}
            //if (e.RowHandle == HotTrackRow)
            //    e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            //else
            //    e.Appearance.BackColor = e.Appearance.BackColor;
            if (e.Column.FieldName.Equals("variance"))
            {
                double variance = 0.00, doubleTemp = 0.00;
                variance = double.TryParse(e.CellValue.ToString(), out doubleTemp) ? Convert.ToDouble(e.CellValue.ToString()) : doubleTemp;
                if (variance < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
               
                else
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
            }
        }
    

        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {


        }

        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            //GridView view = sender as GridView;
            //GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            //if (info.InRowCell)
            //    HotTrackRow = info.RowHandle;
            //else
            //    HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void gridView1_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == "whsecode" || e.Column.FieldName.Equals("item_code"))
            {
                int Value1_NumberPart;
                string Value1_TextPart;

                string Value1_NumberString = Regex.Match(e.Value1.ToString(), @"\d+").Value;
                if (Value1_NumberString != "")
                {
                    Value1_NumberPart = Convert.ToInt32(Value1_NumberString);
                    Value1_TextPart = e.Value1.ToString().Replace(Value1_NumberString, "").ToLower();
                }
                else
                {
                    Value1_NumberPart = 0;
                    Value1_TextPart = e.Value1.ToString().ToLower();
                }


                int Value2_NumberPart;
                string Value2_TextPart;
                string Value2_NumberString = Regex.Match(e.Value2.ToString(), @"\d+").Value;
                if (Value2_NumberString != "")
                {
                    Value2_NumberPart = Convert.ToInt32(Value2_NumberString);
                    Value2_TextPart = e.Value2.ToString().Replace(Value2_NumberString, "").ToLower();
                }
                else
                {
                    Value2_NumberPart = 0;
                    Value2_TextPart = e.Value2.ToString().ToLower();
                }


                if (Value1_TextPart != Value2_TextPart)
                    e.Handled = false;
                else
                {
                    e.Handled = true;
                    e.Result = Value1_NumberPart > Value2_NumberPart ? 1 : -1;
                }
            }
        }
    }
}
