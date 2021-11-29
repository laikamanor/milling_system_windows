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
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;

namespace AB
{
    public partial class IssueForProdPacking_Dialog : Form
    {
        public IssueForProdPacking_Dialog(JArray jSelected,string type, int transferID)
        {
            InitializeComponent();
            jaSelected = jSelected;
            gType = type;
            gTransferID = transferID;
        }
        api_class apic = new api_class();
        JArray jaSelected = new JArray();
        string gType = "";
        public int selectedID = 0;
        public static bool isSubmit = false;
        int intTemp = 0,gTransferID = 0;
        public Form frmm;
        BackgroundWorker bgSubmit = new BackgroundWorker();
        private void IssueForProdPackingFG_Dialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;

            IssueForProdPacking_SelectedItems.isSubmit = false;
            IssueForProdPacking_SelectedItems frm = new IssueForProdPacking_SelectedItems(jaSelected, gType);
            frm.selectedID = selectedID;
            showForm(frm);
            bgSubmit.WorkerReportsProgress = true;
            frmm.Hide();
            //this.TopMost = true;
            //this.Activate();
        }


        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    this.Focus();
        //}
        //protected override void OnDeactivate(EventArgs e)
        //{
        //    base.OnDeactivate(e);
        //    this.Focus();
        //}

        //protected override void OnActivated(EventArgs e)
        //{
        //    base.OnActivated(e);
        //    this.Focus();
        //}

        public void showForm(Form form)
        {
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        public void bg(BackgroundWorker bgw)
        {
            if (!bgw.IsBusy)
            {
                closeForm();
                this.Cursor = Cursors.WaitCursor;
                btnSubmit.Enabled = false;
                bgw.RunWorkerAsync();
            }
        }

        public void closeForm()
        {
            try
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        this.Cursor = Cursors.Default;
                    }));
                }
                btnSubmit.Invoke(new Action(delegate ()
                {
                    btnSubmit.Enabled = true;
                }));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public string delegateControl(Control c)
        {
            string result = "";
            c.Invoke(new Action(delegate ()
            {
                result = c.Text;
            }));
            return result;
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789abcdefghijklmnñopqrstuvxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool checkHaveBlankActualQuantity()
        {
            bool result = false;
            int result_int = 0;
            for(int i =0; i < jaSelected.Count; i++)
            {

                JObject joSelected = jaSelected[i].IsNullOrEmpty() ? new JObject() : jaSelected[i].Type == JTokenType.Object ? (JObject)jaSelected[i] : new JObject();
                string sActualQuantity = joSelected["actual_quantity"].IsNullOrEmpty() ? "" : joSelected["actual_quantity"].ToString();
                result_int += string.IsNullOrEmpty(sActualQuantity.Trim()) ? 1 : 0;
            }
            result = result_int > 0;
            return result;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string hashedID = RandomString(20);
            double doubleTemp = 0.00;
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(lblMill.Text.Trim()) || lblMill.Text == "N/A")
                {
                    MessageBox.Show("Mill field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblMill.Focus();
                }
                else if (string.IsNullOrEmpty(lblFGItem.Text.Trim()) || lblFGItem.Text.Trim().ToLower().Equals("n/a"))
                {
                    MessageBox.Show("FG Item field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    button1.Focus();
                }
                else if (string.IsNullOrEmpty(txtQuantity.Text.Trim()) || !double.TryParse(txtQuantity.Text.Trim(), out doubleTemp))
                {
                    MessageBox.Show("FG Quantity field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuantity.Focus();
                }
                else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    MessageBox.Show("Remarks field is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRemarks.Focus();
                }
                else if (checkHaveBlankActualQuantity() && gType.Equals("Issue For Packing"))
                {
                    MessageBox.Show("Please Review, You have unfilled actual quantity field!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bgSubmit = new BackgroundWorker();
                    bgSubmit.DoWork += delegate
                    {
                        executeFG(hashedID);
                    };
                    bgSubmit.RunWorkerCompleted += delegate
                    {
                        closeForm();
                    };
                    bg(bgSubmit);
                }
            }
        }
        public void executeFG(string hashedID)
        {
            try
            {
                string sRemarks = delegateControl(txtRemarks), sMill = delegateControl(lblMill), sFGItem = delegateControl(lblFGItem), sFGQuantity = delegateControl(txtQuantity), sFGUom = delegateControl(lblFGUom);
                double qty = 0.00, doubleTemp = 0.00;
                qty = double.TryParse(sFGQuantity, out doubleTemp) ? Convert.ToDouble(sFGQuantity) : doubleTemp;
                JObject joBody = new JObject();
                JObject joHeader = new JObject();
                joHeader.Add("transdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                joHeader.Add("remarks", sRemarks);
                if (!(sMill.Equals("N/A") || string.IsNullOrEmpty(sMill.Trim())))
                {
                    joHeader.Add("mill", sMill);
                }
                double targetQty = double.TryParse(lblTargetQuantity.Text, out doubleTemp) ? Convert.ToDouble(lblTargetQuantity.Text) : 0.00;
                joHeader.Add("fg_targeted_qty", targetQty);
                joHeader.Add("fg_item", sFGItem);
                joHeader.Add("fg_quantity", qty);
                joHeader.Add("fg_uom", sFGUom);
                joHeader.Add("hashed_id", hashedID);
                if (gTransferID > 0)
                {
                    joHeader.Add("transfer_id", gTransferID);
                }

                JArray jaFinal = new JArray();
                int intTemp = 0;
                for(int i = 0; i < jaSelected.Count; i++)
                {
                    JObject j = (JObject)jaSelected[i];
                    JObject joFinal = new JObject();
                    //joFinal.Add("item_code", j["item_code"].ToString());
                    Console.WriteLine("actual quantity: " + j["actual_quantity"].ToString());
                    double quantity = j["actual_quantity"].IsNullOrEmpty() ? 0.00 : double.TryParse(j["actual_quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(j["actual_quantity"].ToString()) : 0.00;

                    joFinal.Add("item_code", j["item_code"].IsNullOrEmpty() ? null : j["item_code"].ToString());
                    joFinal.Add("quantity", quantity);
                    joFinal.Add("uom", j["uom"].IsNullOrEmpty() ? null : j["uom"].ToString());
                    joFinal.Add("whsecode", j["whsecode"].IsNullOrEmpty() ? null : j["whsecode"].ToString());


                    joFinal.Add("transrow_id", j["transrow_id"].IsNullOrEmpty() ? (int?)null : int.TryParse(j["transrow_id"].ToString(), out intTemp) ? Convert.ToInt32(j["transrow_id"].ToString()) : intTemp);
                    jaFinal.Add(joFinal);
                }
                joBody.Add("header", joHeader);
                joBody.Add("rows", jaFinal);
                string sResult = apic.loadData("/api/production/issue_for_prod/new", "", "application/json", joBody.ToString(), Method.POST, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = joResult["success"] == null ? false : (bool)joResult["success"];
                    string msg = joResult["success"] == null ? "" : joResult["message"].ToString();
                    if (isSuccess)
                    {
                        IssueForProdPacking.jaSelected = new JArray();
                        jaSelected = new JArray();
                        isSubmit = true;
                        closeForm();
                        apic.showCustomMsgBox("Message", msg);
                        //MessageBox.Show(msg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Invoke(new Action(delegate ()
                        {
                            this.Hide();
                        }));
                    }
                    else
                    {
                        closeForm();
                        apic.showCustomMsgBox("Validation", msg);
                        //MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                closeForm();
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMill_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "";
                this.TopMost = false;
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/mill/get_all", sParams, "name", "code",false,false);
                frm.ShowDialog();
                
                lblMill.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public  JArray  loadIssueDetails()
        {
            JArray jaResult = new JArray();
            string sResult = apic.loadData("/api/production/issue_for_prod/details/", selectedID.ToString(), "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = (bool)joResult["success"];
                    if (isSuccess)
                    {
                        JArray jaData = (JArray)joResult["data"];
                        DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                        foreach(DataRow row in dt.Rows)
                        {
                            JObject jo = new JObject();
                            double qty = 0.00, doubleTemp = 0.00;
                            string itemCode = row["item_code"] == null ? "" : row["item_code"].ToString();
                            string uom = row["uom"] == null ? "" : row["uom"].ToString();
                            string whseCode = row["whsecode"] == null ? "" : row["whsecode"].ToString();
                            qty = row["quantity"] == null ? 0.00 : double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;
                            jo.Add("item_code", itemCode);
                            jo.Add("quantity", qty);
                            jo.Add("whsecode", whseCode);
                            jo.Add("uom", uom);
                            jaResult.Add(jo);
                        }
                    }
                }
            }
            return jaResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sParams = "?in_item_group=FG - Soft Flour&in_item_group=FG - Hard Flour&in_item_group=By-Products&in_item_group=Specialty Flour";
                showWarehouse.selectedWhse = showWarehouse.selectedUom = "";
                showWarehouse frm = new showWarehouse("", "/api/item/getall", sParams, "item_name", "item_code",false, false);
                frm.ShowDialog();
                this.Focus();
                lblFGItem.Text = string.IsNullOrEmpty(showWarehouse.selectedWhse.Trim()) ? "N/A" : showWarehouse.selectedWhse;
                lblFGUom.Text = showWarehouse.selectedUom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            double doubleTemp = 0.00;
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            qty++;
            txtQuantity.Text = String.Format("{0:#,0.000}", qty);

            double targetQty = double.TryParse(lblTargetQuantity.Text, out doubleTemp) ? Convert.ToDouble(lblTargetQuantity.Text) : doubleTemp;
            double variance = qty - targetQty;
            lblVariance.Text = string.IsNullOrEmpty( txtQuantity.Text.Trim()) ? "Variance: " : "Variance: " + variance.ToString("n3");
            lblVariance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
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
            double targetQty = double.TryParse(lblTargetQuantity.Text, out doubleTemp) ? Convert.ToDouble(lblTargetQuantity.Text) : doubleTemp;
            double variance = qty - targetQty;
            lblVariance.Text = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "Variance: " : "Variance: " + variance.ToString("n3");
            lblVariance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
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
            double doubleTemp = 0.00;
            double qty = double.TryParse(txtQuantity.Text, out doubleTemp) ? Convert.ToDouble(txtQuantity.Text) : doubleTemp;
            double targetQty = double.TryParse(lblTargetQuantity.Text, out doubleTemp) ? Convert.ToDouble(lblTargetQuantity.Text) : doubleTemp;
            double variance = qty - targetQty;
            lblVariance.Text = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "Variance: " : "Variance: " + variance.ToString("n3");
            lblVariance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
