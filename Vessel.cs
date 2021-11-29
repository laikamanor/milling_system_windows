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
    public partial class Vessel : Form
    {
        public Vessel()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        private void Vessel_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
        }

        public void loadData()
        {
            string sParams = "";
            string sResult = apic.loadData("/api/vessel/get_all", sParams, "", "", Method.GET, true);
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
                        string[] suggestions = { "name" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);

                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("btn_edit") ? repositoryItemButtonEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = "";
                            col.Visible = fieldName.Equals("name") || fieldName.Equals("btn_edit");
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

        private void btnAddVessel_Click(object sender, EventArgs e)
        {
            AddVessel.isSubmit = false;
            AddVessel frm = new AddVessel();
            frm.ShowDialog();
            if (AddVessel.isSubmit)
            {
                bg();
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            int id = 0, intTemp = 0;
            string selectedColumnfieldName = gridView1.FocusedColumn.FieldName;
            string companyName = gridView1.GetFocusedRowCellValue("name").ToString();
            id = gridView1.GetFocusedRowCellValue("id") == null ? intTemp : Int32.TryParse(gridView1.GetFocusedRowCellValue("id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString()) : intTemp;
            EditVessel.isSubmit = false;
            EditVessel frm = new AB.EditVessel(id);
            frm.txtName.Text = companyName;
            frm.ShowDialog();
            if (EditVessel.isSubmit)
            {
                bg();
            }
        }
    }
}
