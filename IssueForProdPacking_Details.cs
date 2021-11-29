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
    public partial class IssueForProdPacking_Details : Form
    {
        public IssueForProdPacking_Details(string itemCode, string uom,bool isAdd)
        {
            InitializeComponent();
            gItemCode = itemCode;
            gUom = uom;
            gIsAdd = isAdd;
        }
        double doubleTemp = 0;
        string gItemCode = "", gUom = "";
        bool gIsAdd = false;
        public static double quantity = 0.00, balance = 0.00;
        public static string itemCode = "", uom = "", fromWhse = "";
        public static bool isSubmit = false;
        private void ItemDetails_Load(object sender, EventArgs e)
        {
            btnFromWhse.Visible = gIsAdd;
            this.Icon = Properties.Resources.logo2;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            qty++;
            txtQuantity.Text = String.Format("{0:#,0.000}", qty);
        }

        private void btnFromWhse_Click(object sender, EventArgs e)
        {
            this.Hide();
            //if (!gIsAdd)
            //{
            //    try
            //    {
            //        string currentBranch = Login.jsonResult["data"]["branch"] == null ? "" : Login.jsonResult["data"]["branch"].ToString();
            //        string sParams = "?branch=" + currentBranch;

            //        showWarehouse.selectedWhse = "";
            //        showWarehouse frm = new showWarehouse(gItemCode, "/api/whse/get_all", sParams, "whsename", "whsecode", true);
            //        frm.ShowDialog();
            //        this.Focus();
            //        lblFromWhse.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}

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

        private void IssueForProdPacking_Details_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            isSubmit = false;
            this.Hide();
            string currentBranch = Login.jsonResult["data"]["branch"] == null ? "" : Login.jsonResult["data"]["branch"].ToString();
            string sParams = "?branch=" + currentBranch;

            showAvailableQtyPerWhse.selectedWhse = "";
            showAvailableQtyPerWhse.isSubmit = false;
            showAvailableQtyPerWhse frm = new showAvailableQtyPerWhse(itemCode,gUom,"Add");
            frm.ShowDialog();
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

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            if (qty <= 0)
            {
                MessageBox.Show("Please input atleast 1!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
            }else if(lblFromWhse.Text=="N/A" || string.IsNullOrEmpty(lblFromWhse.Text.Trim()))
            {
                MessageBox.Show("Please select From Warehouse!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnFromWhse.Focus();
            }
            else
            {
                quantity = qty;
                isSubmit = true;
                uom = lblUom.Text.Replace("UOM: ", "").Trim();
                fromWhse = lblFromWhse.Text;
                itemCode = lblItemCode.Text.Replace("Item Code: ", "").Trim();
                this.Hide();
            }
        }
    }
}
