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
    public partial class GR_Remarks : Form
    {
        public GR_Remarks()
        {
            InitializeComponent();
        }
        public static bool isSubmit = false;
        public static string grNumber = "", remarks = "";
        private void GR_Remarks_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
             if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                MessageBox.Show("Remarks field is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRemarks.Focus();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    isSubmit = true;
                    grNumber = txtSAP.Text.Trim();
                    remarks = txtRemarks.Text.Trim();
                    this.Dispose();
                }
            }
        }
    }
}
