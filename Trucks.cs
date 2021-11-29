using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
namespace AB
{
    public partial class Trucks : Form
    {
        public Trucks()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTruck frm = new AddTruck();
            frm.ShowDialog();
            bg();
        }

        public void loadData()
        {
            UI_Class.api_class apic = new UI_Class.api_class();
            string sResult = apic.loadData("/api/trucks/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    Console.WriteLine(jaData.ToString());
                    DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();
                        gridControl1.DataSource = dtData;
                        gridView1.OptionsView.ColumnAutoWidth = false;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string v = col.FieldName;
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.Visible = v.Equals("plate_num") || v.Equals("truck_model") || v.Equals("truck_type") ? true : false;
                        }
                        gridView1.BestFitColumns();
                    }));

                }
            }
        }

        private void Trucks_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }
    }
}
