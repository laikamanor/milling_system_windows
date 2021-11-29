using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AB.UI_Class;
namespace AB
{
    public partial class ViewAssignedDept : Form
    {
        public ViewAssignedDept(string data, string userName,bool isChangeDept)
        {
            InitializeComponent();
            gData = data;
            gUserName = userName;
            gIsChangeDept = isChangeDept;
        }
        string gData = "", gUserName = "";
        public static bool isSubmit = false;
        bool gIsChangeDept = false;
        public static string selectedDepartment= "";
        api_class apic = new api_class();
        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                string currentColumn = gridView1.FocusedColumn.FieldName;
                string currentDeptValue = gridView1.GetFocusedRowCellValue("department") == null ? "" : gridView1.GetFocusedRowCellValue("department").ToString();
                if (currentColumn.Equals("department"))
                {
                    if (gIsChangeDept)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to change department to " + currentDeptValue + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            isSubmit = true;
                            selectedDepartment = currentDeptValue;
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void ViewAssignedDept_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Properties.Resources.logo2;
                lblName.Text = gUserName;
                loadData();
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            
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
                string currentBranch = Login.jsonResult["data"]["branch"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["branch"].ToString();
                if (e.Column.FieldName.Equals("department"))
                {
                    if (e.CellValue.Equals(currentBranch))
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
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

        public void loadData()
        {
           try
            {
                JArray jaData = JArray.Parse(gData);
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dtData;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType =  DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = "";
                            col.Visible = fieldName.Equals("department");
                        }
                        gridView1.BestFitColumns();
                    }));
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }
    }
}
