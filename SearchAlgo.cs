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
    public partial class SearchAlgo : Form
    {
        public SearchAlgo()
        {
            InitializeComponent();
        }
        string[] names = { "Gordon", "Axl", "Ambassador", "BIGDESAL", "BUNNYSAL","Additive -1 For Ambassador" };
        private void SearchAlgo_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(names);
            txtSearch.AutoCompleteCustomSource = auto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           foreach(string name in names)
            {
                if (name.Trim().ToLower().Contains(txtSearch.Text.Trim().ToLower())){
                    MessageBox.Show(name);
                }
            }
        }
    }
}
