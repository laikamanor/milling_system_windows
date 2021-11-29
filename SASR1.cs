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
using System.Globalization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class SASR1 : Form
    {
        public SASR1(JArray ja, string arrayName, int daysDiff)
        {
            jaBSR = ja;
            gArrayName = arrayName;
            InitializeComponent(); 
        }
        JArray jaBSR = new JArray();
        string gArrayName = "";

        private void SASR1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblTitle.Text = gArrayName.Equals("customer_summary_report") ? "Customer Summary" : "Branch Summary";
            DataTable dt = populateData();
            gridControl1.DataSource = dt;


            foreach (GridColumn col in gridView1.Columns)
            {
                if (col.Caption != "branch")
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n2";
                }
                string s = col.GetCaption().Replace("_", " ");
                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                (gridControl1.MainView as GridView).Columns[col.AbsoluteIndex].ColumnEdit = repositoryItemTextEdit1;
            }
            //gridView1.Columns["branch"].GroupIndex = 1;

        }


        public DataTable populateData()
        {
            DataTable dt = new DataTable();
            if (gArrayName.Equals("customer_summary_report"))
            {
                dt.Columns.Add("cust_code", typeof(string));
                dt.Columns.Add("total_sales", typeof(double));
            }
            else
            {
                dt.Columns.Add("branch", typeof(string));
                dt.Columns.Add("total", typeof(double));
                dt.Columns.Add("cash_sales", typeof(double));
                dt.Columns.Add("ar_sales", typeof(double));
                dt.Columns.Add("agent_sales", typeof(double));
            }
            double stotal = 0.00, scashSales = 0.00, sarSales = 0.00, sagentSales = 0.00;
            for (int i = 0; i < jaBSR.Count(); i++)
            {
                JObject jo = JObject.Parse(jaBSR[i].ToString());
                string branch = "";
                double total = 0.00, doubleTemp = 0.00, cashSales = 0.00, arSales = 0.00, agentSales = 0.00;
                if (gArrayName.Equals("customer_summary_report"))
                {
                    foreach (var q in jo)
                    {
                        if (q.Key.Equals("cust_code"))
                        {
                            branch = q.Value.ToString();
                        }
                        else if (q.Key.Equals("total_sales"))
                        {
                            total = double.TryParse(q.Value.ToString(), out doubleTemp) ? Double.Parse(q.Value.ToString(), CultureInfo.CurrentCulture) : doubleTemp;
                            stotal += total;
                        }
                    }
                }
                else;
                {
                    foreach (var q in jo)
                    {
                        if (q.Key.Equals("branch"))
                        {
                            branch = q.Value.ToString();
                        }
                        else if (q.Key.Equals("total"))
                        {
                            total = double.TryParse(q.Value.ToString(), out doubleTemp) ? Double.Parse(q.Value.ToString(), CultureInfo.CurrentCulture) : doubleTemp;
                            stotal += total;
                        }
                        else if (q.Key.Equals("cash_sales"))
                        {
                            cashSales = double.TryParse(q.Value.ToString(), out doubleTemp) ? Double.Parse(q.Value.ToString(), CultureInfo.CurrentCulture) : doubleTemp;
                            scashSales += cashSales;
                        }
                        else if (q.Key.Equals("ar_sales"))
                        {
                            arSales = double.TryParse(q.Value.ToString(), out doubleTemp) ? Double.Parse(q.Value.ToString(), CultureInfo.CurrentCulture) : doubleTemp;
                            sarSales += arSales;
                        }
                        else if (q.Key.Equals("agent_sales"))
                        {
                            agentSales = double.TryParse(q.Value.ToString(), out doubleTemp) ? Double.Parse(q.Value.ToString(), CultureInfo.CurrentCulture) : doubleTemp;
                            sagentSales += agentSales;
                        }
                    }
                }
                if (gArrayName.Equals("customer_summary_report"))
                {
                    dt.Rows.Add(branch, total);
                }
                else
                {
                    dt.Rows.Add(branch, total, cashSales, arSales, agentSales);
                }
            }
            if(dt.Rows.Count > 0)
            {
                if (gArrayName.Equals("customer_summary_report"))
                {
                    dt.Rows.Add("Total", stotal);
                }
                else
                {
                    dt.Rows.Add("Total", stotal, scashSales, sarSales, sagentSales);
                }

            }
            return dt;
        }
    }
}
