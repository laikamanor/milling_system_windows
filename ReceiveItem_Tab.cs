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
    public partial class ReceiveItem_Tab : Form
    {
        public ReceiveItem_Tab()
        {
            InitializeComponent();
        }

        private void ReceiveItem_Tab_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            ReceiveItem frm = new ReceiveItem("C");
            showForm(frm, panelClosed);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex <= 0)
            {
                ReceiveItem frm = new ReceiveItem("C");
                showForm(frm, panelClosed);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                ReceiveItem frm = new ReceiveItem("N");
                showForm(frm, panelCancelled);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                //ReceiveItem frm = new ReceiveItem("O");
            }
        }

        public void showForm(Form form,Panel panel)
        {
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }
    }
}
