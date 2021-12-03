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
    public partial class TemperMonitoringTab : Form
    {
        public TemperMonitoringTab(int selectedTabIndex, string panelName,string mode)
        {
            InitializeComponent();
            gPanelName = panelName;
            gSelectedTabIndex = selectedTabIndex;
            gMode = mode;
        }
        string gMode = "", gPanelName = "";
        int gSelectedTabIndex = 0;
        private void TemperMonitoring_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            TemperMonitoring frm = new AB.TemperMonitoring("1");
            Panel pn = panelDone;
            showForm(frm, pn);
        }

        public void showForm(Form form,Panel pn)
        {
            form.TopLevel = false;
            pn.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }
    }
}
