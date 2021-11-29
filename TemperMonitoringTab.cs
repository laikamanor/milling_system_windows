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
            TemperMonitoring frm = new AB.TemperMonitoring(gMode);
            tcTemperMonitoring.SelectedIndex = gSelectedTabIndex;
            Panel pn = GetPanelByName(gPanelName);
            showForm(frm, pn);
        }

        public Panel GetPanelByName(string pName)
        {
            foreach (TabPage tp in tcTemperMonitoring.TabPages)
            {
                foreach(Control c in tp.Controls)
                {
                    if (c.Name == pName)
                    {
                        if(c is Panel)
                        {
                            return (Panel)c;
                        }
                    }
                }
            }
            return null;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMode = (tcTemperMonitoring.SelectedIndex == 0 ? "" : "1");
            Panel pn = tcTemperMonitoring.SelectedIndex == 0 ? panelForDisposition : panelDone;
            TemperMonitoring frm = new AB.TemperMonitoring(sMode);
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
