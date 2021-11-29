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
    public partial class IssueForProduction_Tab : Form
    {
        public IssueForProduction_Tab()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        utility_class utilityc = new utility_class();
        private void IssueForProduction_Load(object sender, EventArgs e)
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

        private void tcProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelCWOpen : tc.SelectedIndex == 1 ? panelCWClosed : panelCWCancelled;
            loadTabs( pn, docStatus, "CLEAN WHEAT");
        }


        private void tcDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = "O", tabName = "";
            if (tcDepts.SelectedIndex <= 0 && tcCWDoc.SelectedIndex <= 0)
            {
                loadTabs(panelCWOpen, "O", "CLEAN WHEAT");

            }
            else if (tcDepts.SelectedIndex == 1 && tcF.SelectedIndex <= 0)
            {
                loadTabs(panelFOpen, "O", "FEEDBACK");
            }
            //else if (tcDepts.SelectedIndex == 1 && tcFPBDoc.SelectedIndex <= 0)
            //{
            //    loadTabs(panelFBPOpen, "O", "FLOUR PACKING BINS");
            //}
            //else if (tcDepts.SelectedIndex == 2 && tcBPPBDoc.SelectedIndex <= 0)
            //{
            //    loadTabs(panelBPPBOpen, "O", "BRAN/POLLARD PACKING BINS");
            //}
            else if (tcDepts.SelectedIndex <= 0)
            {
                tcCWDoc.SelectedIndex = 0;
            }
            else if (tcDepts.SelectedIndex == 1)
            {
                tcF.SelectedIndex = 0;
            }
            //else if (tcDepts.SelectedIndex == 1)
            //{
            //    tcFPBDoc.SelectedIndex = 0;
            //}
            //else if (tcDepts.SelectedIndex == 2)
            //{
            //    tcBPPBDoc.SelectedIndex = 0;
            //}

        }

        //private void tcFPBDoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TabControl tc = (TabControl)sender;
        //    string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
        //    //string docStatus = tc.SelectedIndex <= 0 ? "C" : "N";
        //    Panel pn = tc.SelectedIndex <= 0 ? panelFBPOpen : tc.SelectedIndex == 1 ? panelFBPClosed : panelFBPCancelled;
        //    //Panel pn = tc.SelectedIndex <= 0 ? panelFBPClosed : panelFBPCancelled;
        //    loadTabs( pn, docStatus, "FLOUR PACKING BINS");
        //}

        //private void tcBPPBDoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TabControl tc = (TabControl)sender;
        //    string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
        //    string docStatus = tc.SelectedIndex <= 0 ? "C" : "N";
        //    Panel pn = tc.SelectedIndex <= 0 ? panelBPPBOpen : tc.SelectedIndex == 1 ? panelBPPBClosed : panelBPPBCancelled;
        //    loadTabs(pn, docStatus, "BRAN/POLLARD PACKING BINS");
        //}

        public void loadTabs(Panel pn, string docStatus, string tabName)
        {
            IssueForProduction frm = new IssueForProduction(docStatus, tabName);
            showForm(pn, frm);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bg();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            tpCleanWheat.Invoke(new Action(delegate ()
            {
                tpCleanWheat.Text = "Clean Wheat (" + countc.loadIssueCount("CLEAN WHEAT").ToString() + ")";
            }));
            tpCleanWheat.Invoke(new Action(delegate ()
            {
                tpFeedback.Text = "Feedback (" + countc.loadIssueCount("FEEDBACK").ToString() + ")";
            }));
            //tpFlourPackingBins.Invoke(new Action(delegate ()
            //{
            //    tpFlourPackingBins.Text = "Flour Packing Bins (" + countc.loadIssueCount("FLOUR PACKING BINS") + ")";
            //}));
            //tpBRANPOLLARDPACKINGBINS.Invoke(new Action(delegate ()
            //{
            //    tpBRANPOLLARDPACKINGBINS.Text = "Bran/Pollard Packing Bins (" + countc.loadIssueCount("BRAN/POLLARD PACKING BINS") + ")";
            //}));    
        }

        private void tcF_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelFOpen : tc.SelectedIndex == 1 ? panelFClosed : panelFCancelled;
            loadTabs(pn, docStatus, "FEEDBACK");
        }
    }
}
