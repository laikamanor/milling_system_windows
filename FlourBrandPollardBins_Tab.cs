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
    public partial class FlourBrandPollardBins_Tab : Form
    {
        public FlourBrandPollardBins_Tab()
        {
            InitializeComponent();
        }

        private void QA_Disposition_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg();
            FlourBrandPollardBins frm = new FlourBrandPollardBins("", "FLOUR BINS");
            showForm(panelFB_ForDisposition, frm);
        }

        public void showForm(Panel panel, Form form)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }


        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex <= 0)
            {
                if(tcFlourBins.SelectedIndex <= 0)
                {
                    FlourBrandPollardBins frm = new FlourBrandPollardBins("", "FLOUR BINS");
                    showForm(panelFB_ForDisposition, frm);
                }
                else
                {
                    tcFlourBins.SelectedIndex = 0;
                }
            }
            else
            {
                if (tcBranPollardBins.SelectedIndex <= 0)
                {
                    FlourBrandPollardBins frm = new FlourBrandPollardBins("", "BRAN/POLLARD BINS");
                    showForm(panelBPB_ForDisposition, frm);
                }
                else
                {
                    tcBranPollardBins.SelectedIndex = 0;
                }
            }
        }

        private void tcFlourBins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcFlourBins.SelectedIndex <= 0)
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("", "FLOUR BINS");
                showForm(panelFB_ForDisposition, frm);
            }
            else if (tcFlourBins.SelectedIndex == 1)
            {
               if(tcBins_FB_Off.SelectedIndex <= 0)
                {
                    FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "FLOUR BINS");
                    frm.docStatus = "O";
                    showForm(panelFB_OffOpen, frm);
                }
                else
                {
                    tcBins_FB_Off.SelectedIndex = 0;
                }
            }
            else
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("On", "FLOUR BINS");
                showForm(panelFB_On, frm);
            }
        }

        private void tcBins_FB_Off_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcBins_FB_Off.SelectedIndex <= 0)
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "FLOUR BINS");
                frm.docStatus = "O";
                showForm(panelFB_OffOpen, frm);
            }
            else if (tcBins_FB_Off.SelectedIndex == 1)
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "FLOUR BINS");
                frm.docStatus = "C";
                showForm(panelFB_OffClosed, frm);
            }
            else
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "FLOUR BINS");
                frm.docStatus = "N";
                showForm(panelFB_OffCancelled, frm);
            }
        }

        private void tcBranPollardBins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcBranPollardBins.SelectedIndex <= 0)
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("", "BRAN/POLLARD BINS");
                showForm(panelBPB_ForDisposition, frm);
            }
            else if (tcBranPollardBins.SelectedIndex == 1)
            {
                if(tcBins_BPB_Off.SelectedIndex <= 0)
                {
                    FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "BRAN/POLLARD BINS");
                    frm.docStatus = "O";
                    showForm(panelBPB_OffOpen, frm);
                }
                else
                {
                    tcBins_BPB_Off.SelectedIndex = 0;
                }
            }
            else
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("On", "BRAN/POLLARD BINS");
                showForm(panelBPB_On, frm);
            }
        }

        private void tcBins_BPB_Off_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcBins_BPB_Off.SelectedIndex <= 0)
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "BRAN/POLLARD BINS");
                frm.docStatus = "O";
                showForm(panelBPB_OffOpen, frm);
            }
            else if (tcBins_BPB_Off.SelectedIndex == 1)
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "BRAN/POLLARD BINS");
                frm.docStatus = "C";
                showForm(panelBFB_OffClosed, frm);
            }
            else
            {
                FlourBrandPollardBins frm = new FlourBrandPollardBins("Off", "BRAN/POLLARD BINS");
                frm.docStatus = "N";
                showForm(panelBFB_OffCancelled, frm);
            }
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            count_class countc = new count_class();
            int FBForDispoCount = await countc.loadBinsCount("FLOUR BINS", "");
            int BPBForDispoCount = await countc.loadBinsCount("BRAN/POLLARD BINS", "");
            int FBOpenCount = await countc.loadBinsCount("FLOUR BINS", "&off_specs=1");
            int BPBOpenCount = await countc.loadBinsCount("BRAN/POLLARD BINS", "&off_specs=1");
            int FBTotalCount = FBForDispoCount + FBOpenCount;
            int BPBTotalCount = BPBForDispoCount + BPBOpenCount;

            tcMain.Invoke(new Action(delegate ()
            {
                tpFlourBins.Text = "Flour Bins (" + FBTotalCount.ToString("N0") + ")";
                tpBranPollardBins.Text = "Bran/Pollard Bins (" + BPBTotalCount.ToString("N0") + ")";
            }));
            tcFlourBins.Invoke(new Action(delegate ()
            {
                tpBins_FB_ForDisposition.Text = "For Disposition (" + FBForDispoCount.ToString("N0") + ")";
                tpBins_BPB_ForDisposition.Text = "For Disposition (" + BPBForDispoCount.ToString("N0") + ")";
                tpBins_FB_OffSpecs.Text = "Off Specs (" + FBOpenCount.ToString("N0") + ")";
                tpBins_BPB_OffSpecs.Text = "Off Specs (" + BPBOpenCount.ToString("N0") + ")";
            }));
            tcBins_FB_Off.Invoke(new Action(delegate ()
            {
                tpOpen.Text = "Open (" + FBOpenCount.ToString("N0") + ")";
                tpBins_BPB_OffOpen.Text = "Open (" + BPBOpenCount.ToString("N0") + ")";
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bg();
        }
    }
}
