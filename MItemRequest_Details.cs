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
    public partial class MItemRequest_Details : Form
    {
        public MItemRequest_Details(string itemCode, string uom, bool isAdd)
        {
            InitializeComponent();
            gItemCode = itemCode;
            gUom = uom;
            gIsAdd = isAdd;
        }
        double doubleTemp = 0.00;
        string gItemCode = "", gUom = "";
        bool gIsAdd = false;
        public static bool isSubmit = false;
        public static double quantity = 0.00;
        public static string itemCode = "", uom = "";

        private void btnPlus_Click(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            qty++;
            txtQuantity.Text = String.Format("{0:#,0.000}", qty);
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            if (qty <= 0)
            {
                MessageBox.Show("Please input atleast 1!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
            }
            else
            {
                quantity = qty;
                isSubmit = true;
                uom = lblUom.Text.Replace("UOM: ", "").Trim();
                itemCode = lblItemCode.Text.Replace("Item Code: ", "").Trim();
                this.Hide();
            }
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

        private void btnMinus_Click(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            if (qty >= 1)
            {
                qty--;

                txtQuantity.Text = String.Format("{0:#,0.000}", qty);
            }
            else
            {
                txtQuantity.Text = "0.00";
            }
        }

        private void MItemRequest_Details_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblItemCode.Text = "Item Code: " + gItemCode;
            lblUom.Text = "UOM: " + gUom;
        }
    }
}
