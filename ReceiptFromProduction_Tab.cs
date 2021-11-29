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
    public partial class ReceiptFromProduction_Tab : Form
    {
        public ReceiptFromProduction_Tab()
        {
            InitializeComponent();
        }

        private void ReceiptFromProduction_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            ReceiptFromProduction frm = new ReceiptFromProduction("O");
            showForm(panelIssueProdOrder, frm);
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
            if (tcProd.SelectedIndex.Equals(0))
            {
                ReceiptFromProduction frm = new ReceiptFromProduction("O");
                showForm(panelIssueProdOrder, frm);
            }
            else if (tcProd.SelectedIndex.Equals(1))
            {
                ReceiptFromProduction frm = new ReceiptFromProduction("C");
                showForm(panelForSAP, frm);
            }
            else if (tcProd.SelectedIndex.Equals(2))
            {
                ReceiptFromProduction frm = new ReceiptFromProduction("N");
                showForm(panelCancelled, frm);
            }
        }
    }
}
