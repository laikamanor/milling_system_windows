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
    public partial class PullOut : Form
    {
        public PullOut()
        {
            InitializeComponent();
        }

        private void PullOut_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
        }
    }
}
