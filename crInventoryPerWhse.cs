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
    public partial class crInventoryPerWhse : Form
    {
        public crInventoryPerWhse(DataTable dt, string currentUser)
        {
            InitializeComponent();
            gDt = dt;
            gCurrentUser = currentUser;
            
        }
        DataTable gDt = new DataTable();
        string gCurrentUser = "";
        private void crInventoryPerWhse_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            cInventoryPerWhse invPerWhse = new cInventoryPerWhse();
            invPerWhse.Database.Tables["data"].SetDataSource(gDt);
            invPerWhse.SetParameterValue("printed_by", gCurrentUser);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = invPerWhse;
        }
    }
}
