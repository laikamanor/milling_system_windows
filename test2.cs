using DevExpress.XtraGrid;
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
    public partial class test2 : Form
    {
        public test2()
        {
            InitializeComponent();
        }

        private void test2_Load(object sender, EventArgs e)
        {
            //GridFormatRule s = new GridFormatRule();
            //s.Rule.Assign.
            //gridView1.FormatRules.Add()
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = Application.StartupPath;
            MessageBox.Show(s);
        }
    }
}
