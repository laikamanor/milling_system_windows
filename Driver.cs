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
using RestSharp;

namespace AB
{
    public partial class Driver : Form
    {
        public Driver()
        {
            InitializeComponent();
        }
        devexpress_class devc = new devexpress_class();
        api_class apic = new api_class();
        private void Driver_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
        }

        public void loadData()
        {
            string sParams = "";
            string sResult = apic.loadData("/api/driver/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
            {
                JObject joResponse = JObject.Parse(sResult);
                JArray jaData = (JArray)joResponse["data"];
                DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                if (IsHandleCreated)
                {
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        dtData.Columns.Add("btn_edit");
                        gridControl1.DataSource = dtData;

                        //auto complete
                        string[] suggestions = { "fullname", "lic_no"};
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);

                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = fieldName.Equals("fullname") ? "full_name" : fieldName.Equals("lic_no") ? "license_#" : col.GetCaption();
                            string s = v.Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("btn_edit") ? repositoryItemButtonEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = "";
                            col.Visible = fieldName.Equals("fullname") || fieldName.Equals("lic_no") || fieldName.Equals("company") || fieldName.Equals("btn_edit");
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            AddDriver.isSubmit = false;
            AddDriver frm = new AddDriver();
            frm.ShowDialog();
            if (AddDriver.isSubmit)
            {
                bg();
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0;
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            string fullName = gridView1.GetFocusedRowCellValue("fullname").ToString();
            string licenseNumber = gridView1.GetFocusedRowCellValue("lic_no").ToString();
            string companyName = gridView1.GetFocusedRowCellValue("company").ToString();
            id = gridView1.GetFocusedRowCellValue("id") == null ? intTemp : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            EditDriver.isSubmit = false;
            EditDriver frm = new AB.EditDriver(id, companyName);
            frm.txtFullName.Text = fullName;
            frm.txtLicenseNumber.Text = licenseNumber;
            frm.ShowDialog();
            if (EditDriver.isSubmit)
            {
                bg();
            }
        }
    }
}
