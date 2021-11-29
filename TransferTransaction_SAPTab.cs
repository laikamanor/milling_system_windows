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
    public partial class TransferTransaction_SAPTab : Form
    {
        public TransferTransaction_SAPTab()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string docStatus = tabControl1.SelectedIndex <= 0 ? "Open" : "Close";
            TransferTransaction_SAP frm = new TransferTransaction_SAP(docStatus);
            frm.Text = this.Text;
            showForm(tabControl1.SelectedIndex <=0 ? panelOpen : panelClose, frm);
        }
        public void showForm(Panel panel, Form form)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void TransferTransaction_SAPTab_Load(object sender, EventArgs e)
        {
            string docStatus = tabControl1.SelectedIndex <= 0 ? "Open" : "Close";
            TransferTransaction_SAP frm = new TransferTransaction_SAP(docStatus);
            frm.Text = this.Text;
            showForm(panelOpen, frm);
        }
    }
}
