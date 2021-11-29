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
using RestSharp;
using Newtonsoft.Json.Linq;
using AB.API_Class.Counts;
namespace AB
{
    public partial class IssuedForProduction_Tab : Form
    {
        public IssuedForProduction_Tab()
        {
            InitializeComponent();
        }

        private void IssuedForProduction_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
            IssueForProduction frm = new IssueForProduction("O", "CLEAN WHEAT");
            showForm(panelCWOpen, frm);
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }


        public void showForm(Panel panel, Form form)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        public void loadTabs(Panel pn, string docStatus, string tabName)
        {
            IssueForProduction frm = new IssueForProduction(docStatus, tabName);
            showForm(pn, frm);
        }

        private void tcDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = "O", tabName = "";
            if (tcDepts.SelectedIndex <= 0 && tcCWStatus.SelectedIndex <= 0)
            {
                loadTabs(panelCWOpen, "O", "CLEAN WHEAT");

            }
            else if (tcDepts.SelectedIndex == 1 && tcFBStatus.SelectedIndex <= 0)
            {
                loadTabs(panelFBOpen, "O", "FEEDBACK");
            }
            else if (tcDepts.SelectedIndex <= 0)
            {
                tcCWStatus.SelectedIndex = 0;
            }
            else if (tcDepts.SelectedIndex == 1)
            {
                tcFBStatus.SelectedIndex = 0;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            tpCW.Invoke(new Action(delegate ()
            {
                tpCW.Text = "Clean Wheat (" + countc.loadIssueCount("CLEAN WHEAT").ToString() + ")";
            }));
            tpFB.Invoke(new Action(delegate ()
            {
                tpFB.Text = "Feedback (" + countc.loadIssueCount("FEEDBACK").ToString() + ")";
            }));
        }

        private void tcCWStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelCWOpen : tc.SelectedIndex == 1 ? panelCWClosed : panelCWCancelled;
            loadTabs(pn, docStatus, "CLEAN WHEAT");
        }

        private void tcFBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelFBOpen : tc.SelectedIndex == 1 ? panelFBClosed : panelFBCancelled;
            loadTabs(pn, docStatus, "FEEDBACK");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bg();
        }
    }
}
