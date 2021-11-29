using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.API_Class.Transfer;
using Newtonsoft.Json.Linq;
using AB.API_Class.User;
namespace AB
{
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            Transfer2 transfer2 = new Transfer2(this.Text.Equals("Received Transactions") ? "Closed" : "Open");
            transfer2.Text = this.Text;
            showForm(this.Text.Equals("Received Transactions") ? panelClosed : panelOpen, transfer2);
            if (this.Text.Equals("Received Transactions"))
            {
                tabControl1.TabPages.Remove(tabPage1);
            }
            else
            {
                tabControl1.TabPages.Remove(tpSAP);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex.Equals(0))
            {
                Transfer2 transfer2 = new Transfer2(this.Text.Equals("Received Transactions") ? "Closed" : "Open");
                transfer2.Text = this.Text;
                showForm(this.Text.Equals("Received Transactions") ? panelClosed : panelOpen, transfer2);
            }
            else if (tabControl1.SelectedIndex.Equals(1))
            {
                Transfer2 transfer2 = new Transfer2(this.Text.Equals("Received Transactions") ? "Cancelled" : "Closed");
                transfer2.Text = this.Text;
                showForm(this.Text.Equals("Received Transactions") ? panelCancelled : panelClosed, transfer2);
            }
            else if (tabControl1.SelectedIndex.Equals(2))
            {
                if(this.Text.Equals("Received Transactions"))
                {
                    ReceiveTransaction_SAP frm = new ReceiveTransaction_SAP();
                    frm.Text = this.Text;
                    showForm(panelForSAP, frm);
                }
                else
                {
                    Transfer2 transfer2 = new Transfer2(this.Text.Equals("Received Transactions") ? "SAP" : "Cancelled");
                    transfer2.Text = this.Text;
                    showForm(panelCancelled, transfer2);
                }
            }
            //else if (tabControl1.SelectedIndex.Equals(3))
            //{
            //    TransferTransaction_SAPTab frm = new TransferTransaction_SAPTab();
            //    frm.Text = this.Text;
            //    showForm(panelSAPIT, frm);
            //}
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex.Equals(0))
            {
                ReceiveTransaction_SAP frm = new ReceiveTransaction_SAP();
                frm.Text = this.Text;
                showForm(panelForSAP, frm);
            }
            else
            {
                ReceiveTransaction_SAP frm = new ReceiveTransaction_SAP();
                frm.hasSAP = true;
                frm.Text = this.Text;
                showForm( panelDone, frm);
            }
        }
    }
}
