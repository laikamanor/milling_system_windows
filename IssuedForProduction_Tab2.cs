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
    public partial class IssuedForProduction_Tab2 : Form
    {
        public IssuedForProduction_Tab2()
        {
            InitializeComponent();
        }

        private void IssuedForProduction_Tab2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
            //IssueForProduction frm = new IssueForProduction("O", "CLEAN WHEAT");
            //showForm(panelCWOpen, frm);
            IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
            showForm(panelForIssue, frm);
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
            if (tcDepts.SelectedIndex == 0)
            {
                IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
                showForm(panelForIssue, frm);
            }
            else if (tcDepts.SelectedIndex == 1)
            {
                if (tcCWStatus.SelectedIndex > 0)
                {
                    tcCWStatus.SelectedIndex = 0;
                }
                else
                {
                    loadTabs(panelCWOpen, "O", "CLEAN WHEAT");
                }
            }
            else if (tcDepts.SelectedIndex == 2)
            {
                if (tcFBStatus.SelectedIndex > 0)
                {
                    tcFBStatus.SelectedIndex = 0;
                }
                else
                {
                    //IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
                    //showForm(panelFBForIssue, frm);
                    loadTabs(panelFBOpen, "O", "FEEDBACK");
                }
            }
            else if (tcDepts.SelectedIndex == 3)
            {
                if (tcFlourBinsStatus.SelectedIndex > 0)
                {
                    tcFlourBinsStatus.SelectedIndex = 0;
                }
                else
                {
                    loadTabs(panelFlourBinsOpen, "O", "FLOUR BINS");
                }
            }
            //if (tcDepts.SelectedIndex <= 0 && tcCWStatus.SelectedIndex <= 0)
            //{
            //    loadTabs(panelCWOpen, "O", "CLEAN WHEAT");

            //}
            //else if (tcDepts.SelectedIndex == 1 && tcFBStatus.SelectedIndex <= 0)
            //{
            //    loadTabs(panelFBOpen, "O", "FEEDBACK");
            //}
            //else if (tcDepts.SelectedIndex <= 0)
            //{
            //    tcCWStatus.SelectedIndex = 0;
            //}
            //else if (tcDepts.SelectedIndex == 1)
            //{
            //    tcFBStatus.SelectedIndex = 0;
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bg();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            tpCW.Invoke(new Action(delegate ()
            {
                int count = countc.loadIssueCount("CLEAN WHEAT");
                tpCW.Text = "Clean Wheat (" +count.ToString("N0") + ")";
                //tpCWIssued.Text = "Issued (" + count.ToString("N0") + ")";
                tpCWOpen.Text = "Open (" + count.ToString("N0") + ")";
            }));
            tpFB.Invoke(new Action(delegate ()
            {
                int count = countc.loadIssueCount("FEEDBACK");
                tpFB.Text = "Feedback (" +count.ToString() + ")";
                //tpFBIssued.Text = "Issued (" + count.ToString() + ")";
                tpFBOpen.Text = "Open (" + count.ToString() + ")";
            }));
            tpFlourBins.Invoke(new Action(delegate ()
            {
                int count = countc.loadIssueCount("FLOUR BINS");
                tpFlourBins.Text = "Flour Bins (" + count.ToString() + ")";
                //tpFlourBinsIssued.Text = "Issued (" + count.ToString() + ")";
                tpFlourBinsOpen.Text = "Open (" + count.ToString() + ")";
            }));
        }

        //private void tcFB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tcFB.SelectedIndex == 0)
        //    {
        //        IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
        //        showForm(panelFBForIssue, frm);
        //    }
        //    else if (tcFB.SelectedIndex == 1)
        //    {
        //        if (tcFBStatus.SelectedIndex > 0)
        //        {
        //            tcFBStatus.SelectedIndex = 0;
        //        }
        //        else
        //        {
        //            loadTabs(panelFBOpen, "O", "FEEDBACK");
        //        }
        //    }
        //}

        //private void tcCW_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tcCW.SelectedIndex == 0)
        //    {
        //        IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
        //        showForm(panelCWForIssue, frm);
        //    }
        //    else if (tcCW.SelectedIndex == 1)
        //    {
        //        if (tcCWStatus.SelectedIndex > 0)
        //        {
        //            tcCWStatus.SelectedIndex = 0;
        //        }
        //        else
        //        {
        //            loadTabs(panelCWOpen, "O", "CLEAN WHEAT");
        //        }
        //    }
        //}
        //private void tcFlourBins_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tcFlourBins.SelectedIndex == 0)
        //    {
        //        IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
        //        showForm(panelFlourBinsForIssue, frm);
        //    }
        //    else if (tcFlourBins.SelectedIndex == 1)
        //    {
        //        if (tcFlourBinsStatus.SelectedIndex > 0)
        //        {
        //            tcFlourBinsStatus.SelectedIndex = 0;
        //        }
        //        else
        //        {
        //            loadTabs(panelFlourBinsOpen, "O", "FLOUR BINS");
        //        }
        //    }
        //}


        private void tcFlourBinsStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = tc.SelectedIndex <= 0 ? "O" : tc.SelectedIndex == 1 ? "C" : "N";
            Panel pn = tc.SelectedIndex <= 0 ? panelFlourBinsOpen : tc.SelectedIndex == 1 ? panelFlourBinsClosed : panelFlourBinsCancelled;
            loadTabs(pn, docStatus, "FLOUR BINS");
        }
    }
}
