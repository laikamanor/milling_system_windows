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
    public partial class SystemReceive_Dialog : Form
    {
        public SystemReceive_Dialog(string itemCode, string uom,double quantity)
        {
            InitializeComponent();
            gItemCode = itemCode;
            gUom = uom;
            gQty = quantity;
        }
        string gItemCode = "", gUom = "";
        double gQty = 0.00, doubleTemp = 0.00;
        public static bool isSubmit = false;
        public static double actualQty = 0.00;
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            actualQty = double.TryParse(txtQuantity.Text.Trim().ToLower(), out doubleTemp) ? Convert.ToDouble(txtQuantity.Text.Trim().ToLower()) : doubleTemp;
            isSubmit = true;
            this.Hide();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            double variance = 0.00;
            if (qty >= 1)
            {
                qty--;

                txtQuantity.Text = String.Format("{0:#,0.000}", qty);
                variance = qty - gQty;
            }
            else
            {
                txtQuantity.Text = "0.00";
                variance = qty - gQty;
            }
            lblVariance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
            lblVariance.Text = "Variance: " + variance.ToString("n3");
            
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
&& !char.IsDigit(e.KeyChar)
&& e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            double variance = qty - gQty;
            lblVariance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
            lblVariance.Text = "Variance: " + variance.ToString("n3");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            qty++;
            txtQuantity.Text = String.Format("{0:#,0.000}", qty);
            double variance = qty - gQty;
            lblVariance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
            lblVariance.Text = "Variance: " + variance.ToString("n3");
        }

        private void SystemReceive_Dialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblItemCode.Text = gItemCode;
            //txtQuantity.Text = "0";
        }
    }
}
