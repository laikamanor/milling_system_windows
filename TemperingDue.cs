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
    public partial class TemperingDue : Form
    {
        public TemperingDue()
        {
            InitializeComponent();
        }

        private void TemperingDue_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;

            TemperMonitoring frm = new TemperMonitoring("for_dispo");
            showForm(frm);
        }


        public void showForm(Form form)
        {
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }
    }
}
