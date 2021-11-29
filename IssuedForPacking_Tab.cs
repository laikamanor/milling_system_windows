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
    public partial class IssuedForPacking_Tab : Form
    {
        public IssuedForPacking_Tab()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        utility_class utilityc = new utility_class();
        private void IssuedForPacking_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
            //IssueForProduction frm = new IssueForProduction("O", "FLOUR PACKING BINS");
            //showForm(panelFBPOpen, frm);
            //MIssueForPacking frm = new MIssueForPacking("FLOUR PACKING BINS", "O");
            IssueForProduction frm = new IssueForProduction("O", "FLOUR PACKING BINS");
            showForm(panelFPBOpen, frm);
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

        private void tcDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = "O", tabName = "";
            if (tcDepts.SelectedIndex == 0 && tcFPBStatus.SelectedIndex <= 0)
            {
                loadTabs(panelFPBOpen, "O", "FLOUR PACKING BINS");
            }
            else if (tcDepts.SelectedIndex == 1 && tcBPPBStatus.SelectedIndex <= 0)
            {
                loadTabs(panelBPPBOpen, "O", "BRAN/POLLARD PACKING BINS");
            }
            else if (tcDepts.SelectedIndex == 0)
            {
                tcFPBStatus.SelectedIndex = 0;
            }
            else if (tcDepts.SelectedIndex == 1)
            {
                tcBPPBStatus.SelectedIndex = 0;
            }
        }

        public void loadTabs(Panel pn, string docStatus, string tabName)
        {
            IssueForProduction frm = new IssueForProduction(docStatus,tabName);
            showForm(pn, frm);
        }

        private void tcFPBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            //string docStatus = tc.SelectedIndex <= 0 ? "Open" : "Done";
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelFPBOpen : tc.SelectedIndex == 1 ? panelFPBClosed : panelFPBCancelled;
            //Panel pn = tc.SelectedIndex <= 0 ? panelFBPClosed : panelFBPCancelled;
            loadTabs(pn, docStatus, "FLOUR PACKING BINS");
        }

        private void tcBPPBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            //string docStatus = tc.SelectedIndex <= 0 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelBPPBOpen : tc.SelectedIndex == 1 ? panelBPPBClosed : panelBPPBCancelled;
            loadTabs(pn, docStatus, "BRAN/POLLARD PACKING BINS");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            //tpCleanWheat.Invoke(new Action(delegate ()
            //{
            //    tpCleanWheat.Text = "Clean Wheat (" + countc.loadIssueCount("CLEAN WHEAT") + ")";
            //}));
            tpFPB.Invoke(new Action(delegate ()
            {
                tpFPB.Text = "Flour Packing Bins (" + countc.loadIssueCount("FLOUR PACKING BINS") + ")";
            }));
            tpBPPB.Invoke(new Action(delegate ()
            {
                tpBPPB.Text = "Bran/Pollard Packing Bins (" + countc.loadIssueCount("BRAN/POLLARD PACKING BINS") + ")";
            }));
        }
    }
}
