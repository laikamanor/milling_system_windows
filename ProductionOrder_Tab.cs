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
    public partial class ProductionOrder_Tab : Form
    {
        public ProductionOrder_Tab()
        {
            InitializeComponent();
        }

        private void ProductionOrder_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            Production_ProductionOrder frm = new Production_ProductionOrder("Open");
            showForm(panelOpen, frm);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex.Equals(0))
            {
                Production_ProductionOrder frm = new Production_ProductionOrder("Open");
                showForm(panelOpen, frm);
            }
            else if (tabControl1.SelectedIndex.Equals(1))
            {
                Production_ProductionOrder frm = new Production_ProductionOrder("Cancelled");
                showForm(panelCancelled, frm);
            }
            else if (tabControl1.SelectedIndex.Equals(2))
            {
                Production_ProductionOrder frm = new Production_ProductionOrder("Closed");
                showForm(panelClosed, frm);
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
    }
}
