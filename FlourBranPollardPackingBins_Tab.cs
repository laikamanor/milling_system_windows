using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.API_Class.Counts;
namespace AB
{
    public partial class FlourBranPollardPackingBins_Tab : Form
    {
        public FlourBranPollardPackingBins_Tab()
        {
            InitializeComponent();
        }

        public void showForm(Panel panel, Form form)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void FlourBranPollardPackingBins_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
            //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("For Disposition", "FLOUR PACKING BINS");
            QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("For Disposition", "FLOUR PACKING BINS");
            showForm(panelFPB_ForDisposition, frm);
        }

        private void tcMain2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain2.SelectedIndex <= 0)
            {
                if (tcFlourPackingBins.SelectedIndex <= 0)
                {
                    //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("For Disposition", "FLOUR PACKING BINS");
                    QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("For Disposition", "FLOUR PACKING BINS");
                    showForm(panelFPB_ForDisposition, frm);
                }
                else
                {
                    tcFlourPackingBins.SelectedIndex = 0;
                }
            }
            else
            {
                if (tcBranPollardPackingBins.SelectedIndex <= 0)
                {
                    //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("For Disposition", "BRAN/POLLARD PACKING BINS");
                    QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("For Disposition", "BRAN/POLLARD PACKING BINS");
                    showForm(panelBPPB_ForDisposition, frm);
                }
                else
                {
                    tcBranPollardPackingBins.SelectedIndex = 0;
                }
            }
        }

        private void tcFlourPackingBins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcFlourPackingBins.SelectedIndex <= 0)
            {
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("For Disposition", "FLOUR PACKING BINS");
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("For Disposition", "FLOUR PACKING BINS");
                showForm(panelFPB_ForDisposition, frm);
            }
            else if (tcFlourPackingBins.SelectedIndex == 1)
            {
                if (tcFlourPackingBinsOff.SelectedIndex <= 0)
                {
                    QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "FLOUR PACKING BINS");
                    //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "FLOUR PACKING BINS");
                    frm.docStatus = "O";
                    showForm(panelFPB_RejectedOpen, frm);
                }
                else
                {
                    tcFlourPackingBinsOff.SelectedIndex = 0;
                }
            }
            else
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Approved", "FLOUR PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Approved", "FLOUR PACKING BINS");
                showForm(panelFPB_Approved, frm);
            }
        }

        private void tcFlourPackingBinsOff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcFlourPackingBinsOff.SelectedIndex <= 0)
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "FLOUR PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "FLOUR PACKING BINS");
                frm.docStatus = "O";
                showForm(panelFPB_RejectedOpen, frm);
            }
            else if (tcFlourPackingBinsOff.SelectedIndex == 1)
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "FLOUR PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "FLOUR PACKING BINS");
                frm.docStatus = "C";
                showForm(panelFPB_RejectedClosed, frm);
            }
            else
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "FLOUR PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "FLOUR PACKING BINS");
                frm.docStatus = "N";
                showForm(panelFPB_RejectedCancelled, frm);
            }
        }

        private void tcBranPollardPackingBins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcBranPollardPackingBins.SelectedIndex <= 0)
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("For Disposition", "BRAN/POLLARD PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("For Disposition", "BRAN/POLLARD PACKING BINS");
                showForm(panelBPPB_ForDisposition, frm);
            }
            else if (tcBranPollardPackingBins.SelectedIndex == 1)
            {
                if (tcBranPollardPackingOff.SelectedIndex <= 0)
                {
                    QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "BRAN/POLLARD PACKING BINS");
                    //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "BRAN/POLLARD PACKING BINS");
                    frm.docStatus = "O";
                    showForm(panelBPPB_RejectedOpen, frm);
                }
                else
                {
                    tcBranPollardPackingOff.SelectedIndex = 0;
                }
            }
            else
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Approved", "BRAN/POLLARD PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Approved", "BRAN/POLLARD PACKING BINS");
                showForm(panelBPPB_Approved, frm);
            }
        }

        private void tcBranPollardPackingOff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcBranPollardPackingOff.SelectedIndex <= 0)
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "BRAN/POLLARD PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "BRAN/POLLARD PACKING BINS");
                frm.docStatus = "O";
                showForm(panelBPPB_RejectedOpen, frm);
            }
            else if (tcBranPollardPackingOff.SelectedIndex == 1)
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "BRAN/POLLARD PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "BRAN/POLLARD PACKING BINS");
                frm.docStatus = "C";
                showForm(panelBPPB_RejectedClosed, frm);
            }
            else
            {
                QA_Disposition_FlourPackingBin2 frm = new QA_Disposition_FlourPackingBin2("Rejected", "BRAN/POLLARD PACKING BINS");
                //QA_Disposition_FlourPackingBin frm = new QA_Disposition_FlourPackingBin("Rejected", "BRAN/POLLARD PACKING BINS");
                frm.docStatus = "N";
                showForm(panelBPPB_RejectedCancelled, frm);
            }
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            int FBForDispoCount =  countc.loadPackingBinsCount("FLOUR PACKING BINS", "");
            int BPBForDispoCount =  countc.loadPackingBinsCount("BRAN/POLLARD PACKING BINS", "");
            int FBOpenCount =  countc.loadPackingBinsCount("FLOUR PACKING BINS", "&is_rejected=1&docstatus=O");
            int BPBOpenCount =  countc.loadPackingBinsCount("BRAN/POLLARD PACKING BINS", "&is_rejected=1&docstatus=O");
            int FBTotalCount = FBForDispoCount + FBOpenCount;
            int BPBTotalCount = BPBForDispoCount + BPBOpenCount;

            tcMain2.Invoke(new Action(delegate ()
            {
                tpFlourPackingBins.Text = "Flour Packing Bins (" + FBTotalCount.ToString("N0") + ")";
                tpBranPackingBins.Text = "Bran/Pollard Packing Bins (" + BPBTotalCount.ToString("N0") + ")";
            }));
            tcFlourPackingBins.Invoke(new Action(delegate ()
            {
                tpPacking_FB_ForDisposition.Text = "For Disposition (" + FBForDispoCount.ToString("N0") + ")";
                tpPacking_BPB_ForDisposition.Text = "For Disposition (" + BPBForDispoCount.ToString("N0") + ")";
                tpPacking_FB_Rejected.Text = "Rejected (" + FBOpenCount.ToString("N0") + ")";
                tpPacking_BPB_Rejected.Text = "Rejected (" + BPBOpenCount.ToString("N0") + ")";
            }));
            tcFlourPackingBinsOff.Invoke(new Action(delegate ()
            {
                tpPacking_FB_RejectedOpen.Text = "Open (" + FBOpenCount.ToString("N0") + ")";
                tpPacking_BPB_RejectedOpen.Text = "Open (" + BPBOpenCount.ToString("N0") + ")";
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
    }
}
