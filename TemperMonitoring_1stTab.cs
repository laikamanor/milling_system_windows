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
    public partial class TemperMonitoring_1stTab : Form
    {
        public TemperMonitoring_1stTab(string mode)
        {
            InitializeComponent();
        }
        string gMode = "";
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMode = (tabControl1.SelectedIndex == 0 ? "for_dispositon" : "done");
            Panel pn = tabControl1.SelectedIndex == 0 ? panelForDisposition : panelDone;
            TemperMonitoring frm = new AB.TemperMonitoring(sMode);
            showForm(frm, pn);
        }

        public void showForm(Form form, Panel pn)
        {
            form.TopLevel = false;
            pn.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void TemperMonitoring_1stTab_Load(object sender, EventArgs e)
        {
            TemperMonitoring frm = new AB.TemperMonitoring(gMode);
            showForm(frm, panelForDisposition);
        }
    }
}
