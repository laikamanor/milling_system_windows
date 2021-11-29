using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AB
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void Loading_Load(object sender, EventArgs e)
        {

        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Focus();
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
