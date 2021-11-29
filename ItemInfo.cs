using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.UI_Class;
namespace AB
{
    public partial class ItemInfo : Form
    {
        api_class apic = new api_class();
        public string itemCode = "", uom = "";
        public static bool isSubmit = false;
        DataTable dtBranches = new DataTable(), dtWarehouse = new DataTable();
        public ItemInfo()
        {
            InitializeComponent();
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            bool isNotExist = false;
            foreach (DataRow row in AddAdjustmentIn.dtSelectedItems.Rows)
            {
                if (row["item_code"].ToString() == itemCode)
                {
                    isNotExist = true;
                    break;
                }
            }
            string whseCode = apic.findValueInDataTable(dtWarehouse, cmbWhse.Text, "whsename", "whsecode");
            if (AddAdjustmentIn.dtSelectedItems.Rows.Count <= 0)
            {
                AddAdjustmentIn.dtSelectedItems.Rows.Add(itemCode, txtQuantity.Text, uom, whseCode);
                isSubmit = true;
                this.Hide();
            }
            else if (!isNotExist)
            {
                AddAdjustmentIn.dtSelectedItems.Rows.Add(itemCode, txtQuantity.Text, uom, whseCode);
                isSubmit = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show(itemCode + " is already selected", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQuantity.Text))
            {
                txtQuantity.Text = "0.000";
            }
            else if (Convert.ToDouble(txtQuantity.Text) <= 0.000)
            {
                txtQuantity.Text = "0.000";
            }
            else
            {
                double qty = Convert.ToDouble(txtQuantity.Text);
                qty -= 1.000;
                txtQuantity.Text = String.Format("{0:#,0.000}", qty);
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQuantity.Text))
            {
                txtQuantity.Text = "0.000";
            }
            else
            {
                double qty = Convert.ToDouble(txtQuantity.Text);
                qty += 1.000;
                txtQuantity.Text = String.Format("{0:#,0.000}", qty);
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

        private void ItemInfo_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            isSubmit = false;
            lblItem.Text = itemCode;
            loadBranches();
            loadWarehouse(apic.findValueInDataTable(dtBranches, cmbBranch.Text, "name", "code"));
            txtQuantity.Text = "0";
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadWarehouse(apic.findValueInDataTable(dtBranches, cmbBranch.Text, "name", "code"));
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        public void loadBranches()
        {
            try
            {
                cmbBranch.Invoke(new Action(delegate ()
                {
                    cmbBranch.Items.Clear();
                }));
                string sResult = "";
                sResult = apic.loadData("/api/branch/get_all", "", "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    dtBranches = apic.getDtDownloadResources(sResult, "data");

                    foreach (DataRow row in dtBranches.Rows)
                    {
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                cmbBranch.Items.Add(row["name"].ToString());
                            }));
                        }

                    }
                    if (IsHandleCreated)
                    {
                        cmbBranch.Invoke(new Action(delegate ()
                        {
                            string branch = (string)Login.jsonResult["data"]["branch"];
                            string s = apic.findValueInDataTable(dtBranches, branch, "code", "name");
                            cmbBranch.SelectedIndex = cmbBranch.Items.IndexOf(s);
                        }));
                    }
                }
                else
                {
                    apic.showCustomMsgBox("Validation", sResult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadWarehouse(string branch)
        {
            try
            {
                cmbWhse.Invoke(new Action(delegate ()
                {
                    cmbWhse.Items.Clear();
                }));
                string sBranchCode = apic.findValueInDataTable(dtBranches, branch, "name", "code");
                string sResult = "";
                sResult = apic.loadData("/api/whse/get_all", "?branch=" + branch, "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    dtWarehouse = apic.getDtDownloadResources(sResult, "data");
                }
                foreach (DataRow row in dtWarehouse.Rows)
                {
                    cmbWhse.Invoke(new Action(delegate ()
                    {
                        cmbWhse.Items.Add(row["whsename"].ToString());
                    }));
                }
                cmbWhse.Invoke(new Action(delegate ()
                {
                    string whse = (string)Login.jsonResult["data"]["whse"];
                    string s = apic.findValueInDataTable(dtWarehouse, whse, "whsecode", "whsename");
                    int currentWhse = cmbWhse.Items.IndexOf(s);
                    cmbWhse.SelectedIndex = currentWhse <= 0 ? 0 : currentWhse;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
