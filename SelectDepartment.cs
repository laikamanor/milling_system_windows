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
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using System.Globalization;

namespace AB
{
    public partial class SelectDepartment : Form
    {
        public SelectDepartment(List<string> listSelected)
        {
            InitializeComponent();
            gListSelected = listSelected;
        }
        api_class apic = new api_class();
        List<string> gListSelected = new List<string>();
        DataTable dtBranches = new DataTable();
        public static DataTable dtSelected = new DataTable();
        private void SelectDepartment_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadBranches();
        }

        public void loadBranches()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            api_class apic = new api_class();
            string sResult = apic.loadData("/api/branch/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtBranches = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    gridControl1.DataSource = dtBranches;
                    gridView1.OptionsView.ColumnAutoWidth = false;
                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
                        string v = col.GetCaption();
                        string s = fieldName.Equals("name") ? "Department" : col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                        col.ColumnEdit = repositoryItemTextEdit1;
                        col.Visible = fieldName.Equals("name");
                    }
                    gridView1.BestFitColumns();
                    if(gListSelected.Count > 0)
                    {
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            if(gridView1.GetRowCellValue(i, "name") != null)
                            {
                                string description = gridView1.GetRowCellValue(i, "name").ToString();
                                foreach (string s in gListSelected)
                                {
                                    if (description.Trim().Equals(s.Trim()))
                                    {
                                        gridView1.SelectRow(i);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            dtSelected.Columns.Clear();
            dtSelected.Columns.Add("department");
            Int32[] selectedRowHandles = gridView1.GetSelectedRows();
            foreach(int row in selectedRowHandles)
            {
                string description = apic.findValueInDataTable(dtBranches, gridView1.GetRowCellValue(row, "name").ToString(), "name", "code");
                dtSelected.Rows.Add(description);
            }
            dtSelected.Columns.Add("btn_remove");
            this.Close();
        }
    }
}
