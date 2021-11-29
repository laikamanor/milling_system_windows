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
namespace AB
{
    public partial class ForIssueForPacking_Tab : Form
    {
        public ForIssueForPacking_Tab()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        utility_class utilityc = new utility_class();
        private void ForIssueForPacking_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;

            MIssueForPacking frm = new MIssueForPacking("FLOUR PACKING BINS", "Open");
            showForm(panelFPBOpen, frm);
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
            MIssueForPacking frm = new MIssueForPacking(tabName, docStatus);
            showForm(pn, frm);
        }

        private void tcDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = "Open", tabName = "";
            if (tcDepts.SelectedIndex == 0 && tcFPBStatus.SelectedIndex <= 0)
            {
                loadTabs(panelFPBOpen, "Open", "FLOUR PACKING BINS");
            }
            else if (tcDepts.SelectedIndex == 1 && tcBPPBStatus.SelectedIndex <= 0)
            {
                loadTabs(panelBPPBOpen, "Open", "BRAN/POLLARD BRAN BINS");
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

        private void tcFPBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "Open" : "Done";
            //string docStatus = tc.SelectedIndex <= 0 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelFPBOpen : panelFPBDone;
            //Panel pn = tc.SelectedIndex <= 0 ? panelFBPClosed : panelFBPCancelled;
            loadTabs(pn, docStatus, "FLOUR PACKING BINS");
        }

        private void tcBPPBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "Open" : "Done";
            //string docStatus = tc.SelectedIndex <= 0 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelBPPBOpen : panelBPPBDone;
            loadTabs(pn, docStatus, "BRAN/POLLARD BRAN BINS");
        }
    }
}
