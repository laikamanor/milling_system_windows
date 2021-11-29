using AB.API_Class.Counts;
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
    public partial class IssuedForPacking_Tab2 : Form
    {
        public IssuedForPacking_Tab2()
        {
            InitializeComponent();
        }

        private void IssuedForPacking_Tab2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            //IssueForProduction frm = new IssueForProduction("O", "FLOUR PACKING BINS");
            //showForm(panelFPBOpen, frm);
            bg();
            MIssueForPacking frm = new MIssueForPacking("", "Open");
            showForm(panelForIssue, frm);
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
            IssueForPacking frm = new IssueForPacking(docStatus, tabName);
            showForm(pn, frm);
        }

        private void tcTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string docStatus = "O", tabName = "";
            if (tcTab.SelectedIndex == 0)
            {
                MIssueForPacking frm = new MIssueForPacking("", "Open");
                showForm(panelForIssue, frm);
            }
            else if (tcTab.SelectedIndex == 1)
            {
                if (tcFPBStatus.SelectedIndex > 0)
                {
                    tcFPBStatus.SelectedIndex = 0;
                }
                else
                {
                    loadTabs(panelFPBOpen, docStatus, "FLOUR PACKING BINS");
                }
            }
            else if (tcTab.SelectedIndex == 2)
            {
                if (tcBPPBStatus.SelectedIndex > 0)
                {
                    tcBPPBStatus.SelectedIndex = 0;
                }
                else
                {
                    //MIssueForPacking frm = new MIssueForPacking("POLLARD PACKING BINS", "Open");
                    //showForm(panelBPPBForInssuance, frm);
                    loadTabs(panelBPPBOpen, docStatus, "BRAN/POLLARD PACKING BINS");
                }
            }
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

        //private void tcFPB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ////if (tcFPB.SelectedIndex == 0)
        //    ////{
        //    ////    MIssueForPacking frm = new MIssueForPacking("FLOUR PACKING BINS", "Open");
        //    ////    showForm(panelFPBForInssuance, frm);
        //    ////}
        //    ////else if (tcFPB.SelectedIndex == 1)
        //    ////{
        //    ////    if (tcFPBStatus.SelectedIndex > 0)
        //    ////    {
        //    ////        tcFPBStatus.SelectedIndex = 0;
        //    ////    }
        //    ////    else
        //    ////    {
        //    ////        loadTabs(panelFPBOpen, "O", "FLOUR PACKING BINS");
        //    ////    }
        //    ////}
        //}

        //private void tcBPPB_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tcBPPB.SelectedIndex == 0)
        //    {
        //        MIssueForPacking frm = new MIssueForPacking("POLLARD PACKING BINS", "Open");
        //        showForm(panelBPPBForInssuance, frm);
        //    }
        //    else if (tcBPPB.SelectedIndex == 1)
        //    {
        //        if (tcBPPBStatus.SelectedIndex > 0)
        //        {
        //            tcBPPBStatus.SelectedIndex = 0;
        //        }
        //        else
        //        {
        //            loadTabs(panelBPPBOpen, "O", "POLLARD PACKING BINS");
        //        }
        //    }
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            tpFlourPackingBins.Invoke(new Action(delegate ()
            {
                int count = countc.loadIssueCount("FLOUR PACKING BINS");
                tpFlourPackingBins.Text = "Flour Packing Bins (" + count.ToString("N0") + ")";
                //tpFPBIssued.Text = "Issued (" + count.ToString("N0") + ")";
                tpFPBOpen.Text = "Open (" + count.ToString("N0") + ")";
            }));
            tpBranPollardPackingBins.Invoke(new Action(delegate ()
            {
                int count = countc.loadIssueCount("BRAN/POLLARD PACKING BINS");
                tpBranPollardPackingBins.Text = "Bran/Pollard Packing Bins (" + count.ToString() + ")";
                //tpBPPBIssued.Text = "Issued (" + count.ToString("N0") + ")";
                tpBPPBOpen.Text = "Open (" + count.ToString("N0") + ")";
            }));
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bg();
        }

        private void IssuedForPacking_Tab2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //foreach(Control c in this.Controls)
            //{
            //    c.Dispose();
            //}
        }
    }
}
