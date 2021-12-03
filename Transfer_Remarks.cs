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
using Newtonsoft.Json;
using RestSharp;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AB.API_Class;
using System.Text.RegularExpressions;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace AB
{
    public partial class Transfer_Remarks : Form
    {
        public Transfer_Remarks(int id, string reference)
        {
            InitializeComponent();
            this.id = id;
            this.reference = reference;
        }
        int id = 0;
        api_class apic = new api_class();
        string reference = "";
        private void Transfer_Remarks_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblReference.Text = reference;
            bg(backgroundWorker1);
        }

        public void loadData()
        {
            try
            {
                gridControl1.Invoke(new Action(delegate ()
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                }));

                string sParams = id.ToString();
                string sResult = apic.loadData("/api/inv/trfr/remarks/get_all/", sParams, "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResponse = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResponse["data"];
                    DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), (typeof(DataTable)));
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = dtData;

                        //gridView1.OptionsView.ColumnAutoWidth = false;
                        //gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = fieldName.Equals("remarks") ? repositoryItemMemoEdit1 : repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType =  fieldName.Equals("date_created") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = fieldName.Equals("date_created")? "yyyy-MM-dd HH:mm:ss" : "";

                            col.Visible = !(fieldName.Equals("id"));


                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                           

                            //fixed column
                            //col.Fixed = fieldName.Equals("btn_approve") || fieldName.Equals("btn_view_remarks") ? FixedStyle.Right : fieldName.Equals("reference") || fieldName.Equals("transdate") ? FixedStyle.Left : FixedStyle.None;
                        }
                        devexpress_class devc = new devexpress_class();
                        gridView1.BestFitColumns();
                        //auto complete
                        string[] suggestions = { "username" };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);
                        var col2 = gridView1.Columns["remarks"];
                        var col3 = gridView1.Columns["username"];
                        if (col2 != null)
                        {
                            col2.Width = 200;
                        }
                        if (col3 != null)
                        {
                            col3.Width +=50;
                        }
                    }));
                }
            }
            catch(Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void bg(BackgroundWorker bgww)
        {
            if (!bgww.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgww.RunWorkerAsync();
            }
        }

        public void closeForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Loading")
                {
                    frm.Invoke(new Action(delegate ()
                    {
                        frm.Hide();
                    }));
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
    }
}
