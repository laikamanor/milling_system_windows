using Newtonsoft.Json.Linq;
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
    public partial class SystemTransferItem_Details : Form
    {
        public SystemTransferItem_Details(string itemCode, string uom, double availableQty, string mode)
        {
            InitializeComponent();
            gItemCode = itemCode;
            gAvailableQty = availableQty;
            gMode = mode;
            gUom = uom;
        }
        string gItemCode = "", gMode = "", gUom = "";
        double gAvailableQty = 0.00;
        public static double quantity = 0;
        public static bool isSubmit = false;
        public static string fromWhse = "", toWhse = "";
        int intTemp = 0;
        private void AddQuantity_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblItemCode.Text = gItemCode;
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            double doubleTemp = 0.00;
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            if(qty <= 0)
            {
                MessageBox.Show("Please input atleast 1!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
            }
            else if (lblFromWhse.Text.Equals("N/A"))
            {
                MessageBox.Show("Please select From Warehouse!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (lblToWhse.Text.Equals("N/A"))
            {
                MessageBox.Show("Please select To Warehouse!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                quantity = qty;
                isSubmit = true;
                fromWhse = lblFromWhse.Text;
                toWhse = lblToWhse.Text;
                this.Hide();
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            double doubleTemp = 0.00;
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            if (qty >= 1)
            {
                qty--;

                txtQuantity.Text = String.Format("{0:#,0.000}", qty);
            }
            else
            {
                txtQuantity.Text = "0";
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            double doubleTemp = 0.00;
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            qty++;
            txtQuantity.Text = String.Format("{0:#,0.000}", qty);
        }

        private void btnFromWhse_Click(object sender, EventArgs e)
        {
            if (gMode.Equals("Add"))
            {
                this.Hide();
            }
            else
            {
                SystemTransferItem_Details.isSubmit = false;
                showAvailableQtyPerWhse.selectedWhse = "";
                showAvailableQtyPerWhse.isSubmit = false;
                showAvailableQtyPerWhse frm = new showAvailableQtyPerWhse(gItemCode, gUom, gMode);
                frm.hiddenTitle = "System Transfer Item";
                Invoke((Action)(() => {
                    frm.ShowDialog();
                }));
                this.Focus();
                if (showAvailableQtyPerWhse.isSubmit)
                {
                    lblFromWhse.Text = showAvailableQtyPerWhse.selectedWhse;
                    txtQuantity.Text = showAvailableQtyPerWhse.quantity.ToString("n3");
                }
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

        private void btnToWhse_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "?branch=", currentBranch = "";
                JObject joLoginResult = Login.jsonResult;
                currentBranch = joLoginResult["data"]["branch"].ToString();
                sParams += currentBranch;
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse(gItemCode, "/api/whse/get_all", sParams, "whsename", "whsecode",false, false);
                frm.ShowDialog();
                this.Focus();
                lblToWhse.Text = string.IsNullOrEmpty( showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
