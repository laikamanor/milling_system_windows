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
    public partial class crInventoryClick : Form
    {
        public crInventoryClick(DataTable dt, string invType)
        {
            InitializeComponent();
            gDt = dt;
            gInvType = invType;
        }
        DataTable gDt = new DataTable();
        string gInvType = "";
        private void crInventoryClick_Load(object sender, EventArgs e)
        {

            this.Icon = Properties.Resources.logo2;
            
            cInventoryClick report = new cInventoryClick();
            report.Database.Tables["data"].SetDataSource(gDt);
            report.SetParameterValue("inventory_type", gInvType);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = report;
          
        }
    }
}
