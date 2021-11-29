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
    public partial class customMessageBox : Form
    {
        public customMessageBox()
        {
            InitializeComponent();
        }

        private void customMessageBox_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            this.Activate();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customMessageBox_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    this.Focus();
        //}
        //protected override void OnDeactivate(EventArgs e)
        //{
        //    base.OnDeactivate(e);
        //    this.Focus();
        //}
    }
}
