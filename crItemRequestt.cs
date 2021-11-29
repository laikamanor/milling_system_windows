using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AB
{
    public partial class crItemRequestt : Form
    {
        public crItemRequestt(DataTable dt)
        {
            InitializeComponent();
            gDt = dt;
        }
        DataTable gDt = new DataTable();
        private void crItemRequestt_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;

            cItemRequestt finalCount = new cItemRequestt();
            finalCount.Database.Tables["rows"].SetDataSource(gDt);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = finalCount;
        }
    }
}
