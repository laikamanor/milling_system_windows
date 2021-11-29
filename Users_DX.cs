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
using AB.API_Class.Branch;
using RestSharp;
using AB.UI_Class;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using AB.UI_Class;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Users_DX : Form
    {
        public Users_DX()
        {
            InitializeComponent();
        }
        utility_class utilityc = new utility_class();
        branch_class branchc = new branch_class();
        DataTable dtBranch = new DataTable();
        DataTable dtPlant = new DataTable();
        api_class apic = new api_class();
        private void Users_DX_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadPlant();
            refresh();
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

        public void loadBranch()
        {
            string plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            cmbDepartment.Properties.Items.Clear();
            cmbDepartment.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtBranch = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtBranch.Rows)
                    {
                        cmbDepartment.Properties.Items.Add(row["name"].ToString());
                    }
                    string branchCode = Login.jsonResult["data"]["branch"].ToString();
                    string branchName = apic.findValueInDataTable(dtBranch, branchCode, "code", "name");
                    cmbDepartment.SelectedIndex = cmbDepartment.Properties.Items.IndexOf(branchName) <= 0 ? 0 : cmbDepartment.Properties.Items.IndexOf(branchName);
                }
            }
            else
            {
                cmbDepartment.SelectedIndex = 0;
            }
        }

        public DataTable loadData()
        {
            DataTable dtData = new DataTable();
            try
            {
                string sBranch = "?branch=" + apic.findValueInDataTable(dtBranch, cmbDepartment.Text, "name", "code"), sParams = sBranch;
                string sResult = apic.loadData("/api/auth/user/get_all", sParams, "", "", RestSharp.Method.GET, true);
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        JArray jaData = joResult["data"] == null ? new JArray() : (JArray)joResult["data"];

                        JArray jaNewData = new JArray();
                        for (int i = 0; i < jaData.Count; i++)
                        {
                            var x = JObject.Parse(jaData[i].ToString());
                            JObject joNewData = new JObject();
                            foreach (var y in x)
                            {
                                joNewData.Add(y.Key, (y.Key.Equals("assigned_dep") ? y.Value.ToString() : y.Value));
                            }
                            jaNewData.Add(joNewData);
                        }
                        dtData = (DataTable)JsonConvert.DeserializeObject(jaNewData.ToString(), typeof(DataTable));

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dtData;
        }

        public string findBranchCode(string value)
        {
            string result = "";
            foreach (DataRow row in dtBranch.Rows)
            {
                if (row["name"].ToString() == value)
                {
                    result = row["code"].ToString();
                    break;
                }
            }
            return result;
        }

        public void refresh()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
          
            DataTable dt =  loadData();
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("btn_view_assigned_department");
                dt.Columns.Add("btn_edit");
            }
            gridControl1.DataSource = dt;
            gridView1.OptionsView.ColumnAutoWidth = false;

            foreach (GridColumn col in gridView1.Columns)
            {
                string fieldName = col.FieldName;
                string v = col.GetCaption();
                string s = fieldName.Equals("branch") ? "department" : col.GetCaption().Replace("_", " ");
                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                col.ColumnEdit = fieldName.Equals("btn_edit") ? repositoryItemButtonEdit1 : fieldName.Equals("btn_view_assigned_department") ? repositoryItemButtonEdit3 : repositoryItemTextEdit1;
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = "n2";
                col.Visible = !(fieldName.Equals("id") || fieldName.Equals("assigned_dep"));
            }
            gridView1.BestFitColumns();
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Text = "Add User";
            addUser.ShowDialog();
            if (AddUser.isSubmit)
            {
                refresh();
            }
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranch();
        }

        private void repositoryItemButtonEdit1_Click_1(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0;
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            id = Int32.TryParse(gridView1.GetFocusedDataRow()["id"].ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedDataRow()["id"].ToString()) : intTemp;
            AddUser addUser = new AddUser();
            addUser.userID = id;
            addUser.Text = "Edit User";
            addUser.ShowDialog();
            if (AddUser.isSubmit)
            {
                refresh();
            }

        }

        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            string assignDep = "", userName = "";
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            assignDep = gridView1.GetFocusedDataRow()["assigned_dep"] == null ? "[]" : string.IsNullOrEmpty(gridView1.GetFocusedDataRow()["assigned_dep"].ToString()) ? "[]" : gridView1.GetFocusedDataRow()["assigned_dep"].ToString();
            userName = gridView1.GetFocusedDataRow()["fullname"] == null ? "NO_USER_FOUND" : gridView1.GetFocusedDataRow()["fullname"].ToString();
            if (selectedColumnfieldName.Equals("btn_view_assigned_department"))
            {
                ViewAssignedDept.isSubmit = false;
                ViewAssignedDept.selectedDepartment="";
                ViewAssignedDept frm = new ViewAssignedDept(assignDep,userName,false);
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
