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
    public partial class PaymentMethodList : Form
    {
        public static DataTable dtList;
        public static bool isSubmit = false;
        public PaymentMethodList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PaymentMethodForm paymentMethodForm = new PaymentMethodForm();
            paymentMethodForm.ShowDialog();
            if (PaymentMethodForm.isSubmit)
            {
                dgv.Rows.Clear();
                foreach (DataRow row in dtList.Rows)
                {   
                    dgv.Rows.Add(row["payment_type"].ToString(), row["amount"].ToString(), row["sapnum"].ToString(), row["reference2"].ToString(), row["type"].ToString());
                }
            }

            totalAmount();
        }

        public void totalAmount()
        {
            double total = 0.00;
            for(int i = 0; i < dgv.Rows.Count; i++)
            {
                total += (dgv.Rows[i].Cells["amount"].Value.ToString() == "" ? 0.00 : Convert.ToDouble(dgv.Rows[i].Cells["amount"].Value.ToString()));
            }
            lblSelectedAmount.Text = total.ToString("n2");
        }

        private void PaymentMethodList_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dtList = new DataTable();
            dtList.Columns.Clear();
            dtList.Columns.Add("id");
            dtList.Columns.Add("payment_type");
            dtList.Columns.Add("amount");
            dtList.Columns.Add("sapnum");
            dtList.Columns.Add("reference2");
            dtList.Columns.Add("type");
            loadData();
        }

        public void loadData()
        {
            if (PendingOrder2.dtSelectedDeposit.Rows.Count > 0)
            {
                int hasNoFDEPS = 0;
                foreach (DataRow row in PendingOrder2.dtSelectedDeposit.Rows)
                {
                    if (row["payment_type"].ToString() != "FDEPS")
                    {
                        hasNoFDEPS += 1;
                    }
                }
                if (hasNoFDEPS > 0)
                {
                    dtList = new DataTable();
                    dtList.Columns.Clear();
                    dtList.Columns.Add("id");
                    dtList.Columns.Add("payment_type");
                    dtList.Columns.Add("amount");
                    dtList.Columns.Add("sapnum");
                    dtList.Columns.Add("reference2");
                    dtList.Columns.Add("type");
                    dgv.Rows.Clear();
                    dtList.Rows.Clear();
                }
                foreach (DataRow row in PendingOrder2.dtSelectedDeposit.Rows)
                {
                    if (row["payment_type"].ToString() != "FDEPS")
                    {
                        dtList.Rows.Add(null,row["payment_type"], row["amount"], row["sapnum"], row["reference2"], row["type"]);
                        dgv.Rows.Add(row["payment_type"], row["amount"], row["sapnum"], row["reference2"], row["type"]);
                    }
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                if (e.ColumnIndex == 5 && e.RowIndex >= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (PendingOrder2.dtSelectedDeposit.Rows.Count > 0)
                        {
                            if (dgv.CurrentRow.Cells["type"].Value.ToString() != "POS")
                            {
                                dtList.Rows.RemoveAt(dgv.CurrentRow.Index);
                                PendingOrder2.dtSelectedDeposit.Rows.RemoveAt(dgv.CurrentRow.Index);
                                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                                totalAmount();
                            }
                            else
                            {
                                MessageBox.Show("You cant remove POS type", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 6 && e.RowIndex >= 0)
                {
                    dgv.CurrentCell = dgv[0, dgv.CurrentRow.Index];
                    dgv.ClearSelection();
                    dgv.CurrentRow.Cells["amount"].Selected = true;
                    EnterAmount frm = new EnterAmount();
                    EnterAmount.amount = 0.00;
                    EnterAmount.amount = Convert.ToDouble(dgv.CurrentRow.Cells["amount"].Value.ToString());
                    frm.reference = dgv.CurrentRow.Cells["payment_type"].Value.ToString();
                    frm.ShowDialog();
                    dgv.CurrentRow.Cells["amount"].Value = Convert.ToDecimal(string.Format("{0:0.00}", EnterAmount.amount));
                    PendingOrder2.dtSelectedDeposit.Rows.Clear();
                    dtList.Rows.Clear();
                    for(int i = 0; i < dgv.Rows.Count; i++)
                    {
                        string paymentType = dgv.Rows[i].Cells["payment_type"].Value.ToString(),
                            sapNumber = dgv.Rows[i].Cells["sapnum"].Value.ToString(),
                            reference2 = dgv.Rows[i].Cells["reference"].Value.ToString(),
                            type = dgv.Rows[i].Cells["type"].Value.ToString();
                        double amount = string.IsNullOrEmpty(dgv.Rows[i].Cells["amount"].Value.ToString()) ? 0.00 : Convert.ToDouble(dgv.Rows[i].Cells["amount"].Value.ToString());
                        dtList.Rows.Add(null, paymentType, Convert.ToDecimal(string.Format("{0:0.00}", amount)),sapNumber, reference2, type);
                    }
                    totalAmount();

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(dgv.Rows.Count > 0)
            {
                if(dtList.Rows.Count > 0)
                {
                    for (int i = 0; i < PendingOrder2.dtSelectedDeposit.Rows.Count; i++)
                    {
                        DataRow row = PendingOrder2.dtSelectedDeposit.Rows[i];
                        if (row["type"].ToString() == "Payment Method" || row["type"].ToString() == "POS")
                        {
                            PendingOrder2.dtSelectedDeposit.Rows.RemoveAt(i);
                        }
                    }
                    foreach (DataRow row in dtList.Rows)
                    {
                        //MessageBox.Show(row["payment_type"].ToString() + Environment.NewLine + row["amount"].ToString());
                        if (row["amount"].ToString() != "")
                        {
                            PendingOrder2.dtSelectedDeposit.Rows.Add(null, Convert.ToDouble(row["amount"].ToString()), row["payment_type"].ToString(), row["sapnum"].ToString(), row["reference2"].ToString(),row["type"].ToString());
                        }
                    }
                    //dtList.Rows.Clear();
                }
                isSubmit = true;
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No order selected. If you want to close the form click the close button", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
