using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.API_Class.Branch;
using AB.API_Class.Warehouse;
using AB.API_Class.Transfer;
using AB.API_Class.User;
using Newtonsoft.Json.Linq;
using AB.UI_Class;
using RestSharp;
using Newtonsoft.Json;

namespace AB
{
    public partial class Transfer2 : Form
    {

        DataTable dtBranch = new DataTable(), dtWarehouse = new DataTable(), dtPlant = new DataTable();
        branch_class branchc = new branch_class();
        warehouse_class warehousec = new warehouse_class();
        api_class apic = new api_class();
        transfer_class transferc = new transfer_class();
        user_clas userc = new user_clas();
        string gForType = "";
        public Transfer2(string forType)
        {
            gForType = forType;
            InitializeComponent();
        }

        private async void Transfer2_Load(object sender, EventArgs e)
        {
            loadWarehouse(cmbWhse, this.Text.Equals("Received Transactions") ? true : false);
            loadWarehouse(cmbToWhse, this.Text.Equals("Received Transactions") ? false : true);
            
            dtFromDate.Visible = checkFromDate.Checked = gForType.Equals("Open") ? false : true;
            checkToDate.Checked = true;
            checkBranchToBranch.Visible = checkBranchToBranch.Checked = this.Text == "Transfer Transactions" ? true : false;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            label2.Visible = label12.Visible = cmbFromTime.Visible = cmbToTime.Visible = this.Text.Equals("Pullout Transactions") ? false : true;
            dtFromDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now;
            loadPlant();
            bg();
            dgvTransactions.Columns["rec_reference"].Visible = dgvTransactions.Columns["rec_trandate"].Visible = this.Text.Equals("Transfer Transactions") && (gForType.Equals("Closed") || gForType.Equals("Cancelled")) ? true : false;
            //dgvTransactions.Columns["from_whse"].Visible = dgvTransactions.Columns["to_whse"].Visible = this.Text.Equals("Transfer Transactions") || this.Text.Equals("Pullout Transactions") ? true : false;
            dgvTransactions.Columns["date_confirmed"].Visible = gForType.Equals("Closed") && this.Text.Equals("Pullout Transactions") ? true : false;
            //dgvTransactions.Columns["sap_number"].Visible = dgvTransactions.Columns["transtype"].Visible = dgvTransactions.Columns["plate_num"].Visible = dgvTransactions.Columns["shift"].Visible = dgvTransactions.Columns["agi_truck_scale"].Visible = dgvTransactions.Columns["chti_truck_scale"].Visible = dgvTransactions.Columns["vessel"].Visible = dgvTransactions.Columns["driver"].Visible = this.Text.Equals("Received Transactions");
        }

        public void checkVariance()
        {
            for (int i = 0; i < dgvTransactions.Rows.Count; i++)
            {
                bool isBranchBranch = false, boolTemp = false, interWhse = false;
                isBranchBranch = bool.TryParse(dgvTransactions.Rows[i].Cells["is_branch_to_branch"].Value.ToString(), out boolTemp) ? Convert.ToBoolean(dgvTransactions.Rows[i].Cells["is_branch_to_branch"].Value.ToString()) : false;
                interWhse = bool.TryParse(dgvTransactions.Rows[i].Cells["inter_whse"].Value.ToString(), out boolTemp) ? Convert.ToBoolean(dgvTransactions.Rows[i].Cells["inter_whse"].Value.ToString()) : false;
                if (Convert.ToDouble(dgvTransactions.Rows[i].Cells["variance_count"].Value.ToString()) != 0)
                {
                    dgvTransactions.Rows[i].Cells["reference"].Style.BackColor = Color.FromArgb(255, 110, 110);
                }
                else if (isBranchBranch)
                {
                    dgvTransactions.Rows[i].Cells["from_whse"].Style.BackColor = Color.FromArgb(115, 255, 110);
                    dgvTransactions.Rows[i].Cells["to_whse"].Style.BackColor = Color.FromArgb(115, 255, 110);
                }
                else if (!interWhse && !isBranchBranch)
                {
                    dgvTransactions.Rows[i].Cells["from_whse"].Style.BackColor = Color.FromArgb(255, 173, 110);
                    dgvTransactions.Rows[i].Cells["to_whse"].Style.BackColor = Color.FromArgb(255, 173, 110);
                }
            }
        }

        public void loadPlant()
        {
            cmbPlant.Properties.Items.Clear();
            cmbPlant.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/plant/get_all", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtPlant.Rows)
                    {
                        cmbPlant.Properties.Items.Add(row["name"].ToString());
                    }
                    string plantCode = Login.jsonResult["data"]["plant"].ToString();
                    string plantName = apic.findValueInDataTable(dtPlant, plantCode, "code", "name");
                    cmbPlant.SelectedIndex = cmbPlant.Properties.Items.IndexOf(plantName) <= 0 ? 0 : cmbPlant.Properties.Items.IndexOf(plantName);
                }
            }
            else
            {
                cmbPlant.SelectedIndex = 0;
            }
        }

        public void loadBranches()
        {
            try
            {
                string plantCode = "";
                cmbPlant.Invoke(new Action(delegate ()
                {
                    plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                }));
                cmbBranch.Invoke(new Action(delegate ()
                {
                    cmbBranch.Properties.Items.Clear();
                }));
                string[] lists = { "isAdmin", "isSuperAdmin","isManager" };
                if (apic.haveAccess(lists))
                {
                    string sResult = "";
                    sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", Method.GET, true);
                    if (sResult.Substring(0, 1).Equals("{"))
                    {
                        //DataTable dtData = apic.getDtDownloadResources(sResult, "data");
                        //string sBranch = apic.getFirstRowDownloadResources(dtData, "data");

                        dtBranch = apic.getDtDownloadResources(sResult, "data");
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                cmbBranch.Properties.Items.Add("All");
                            }));
                        }
                        foreach (DataRow row in dtBranch.Rows)
                        {
                            if (IsHandleCreated)
                            {
                                cmbBranch.Invoke(new Action(delegate ()
                                {
                                    cmbBranch.Properties.Items.Add(row["name"].ToString());
                                }));
                            }

                        }
                        if (IsHandleCreated)
                        {
                            cmbBranch.Invoke(new Action(delegate ()
                            {
                                string branch = (string)Login.jsonResult["data"]["branch"];
                                string s = apic.findValueInDataTable(dtBranch, branch, "code", "name");
                                cmbBranch.SelectedIndex = cmbBranch.Properties.Items.IndexOf(s) <= 0 ? 0 : cmbBranch.Properties.Items.IndexOf(s);

                            }));
                        }
                    }
                    else
                    {
                        apic.showCustomMsgBox("Validation", sResult);
                    }
                }
                else
                {
                    if (IsHandleCreated)
                    {
                        cmbBranch.Invoke(new Action(delegate ()
                        {
                            cmbBranch.Properties.Items.Add(Login.jsonResult["data"]["branch"]);
                            cmbBranch.SelectedIndex = 0;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

   
        public void loadWarehouse(ComboBox cmb,bool isTo)
        {
            try
            {
                cmb.Items.Clear();
                string sBranchCode = apic.findValueInDataTable(dtBranch, cmbBranch.Text, "name", "code");
                string sResult = "";
                sResult = apic.loadData("/api/whse/get_all", isTo ? "?branch=" + sBranchCode : "", "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    dtWarehouse = apic.getDtDownloadResources(sResult, "data");
                }
                if (dtWarehouse.Rows.Count > 1)
                {
                    cmb.Invoke(new Action(delegate ()
                    {
                        cmb.Items.Add("All");
                    }));
                }

                DataView dv = dtWarehouse.DefaultView;
                dv.Sort = "id ASC";
                dtWarehouse = dv.ToTable();
                foreach (DataRow row in dtWarehouse.Rows)
                {
                    cmb.Invoke(new Action(delegate ()
                    {
                        cmb.Items.Add(row["whsename"].ToString());
                    }));
                }
                cmb.Invoke(new Action(delegate ()
                {
                    string whse = (string)Login.jsonResult["data"]["whse"];
                    string s = apic.findValueInDataTable(dtWarehouse, whse, "whsecode", "whsename");
                    cmb.SelectedIndex = cmbBranch.Text.Equals("All") || string.IsNullOrEmpty(s.Trim()) ? 0 : isTo ? 0 : cmb.Items.IndexOf(s) <=0  ? 0 : cmb.Items.IndexOf(s);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadData()
        {
            dgvTransactions.Invoke(new Action(delegate ()
            {
                dgvTransactions.Rows.Clear();
            }));
            string statusCode = gForType.Equals("Open") || gForType.Equals("Closed") ? gForType.Substring(0, 1) : gForType.Equals("Cancelled") ? "N" : "";
            DataTable dtTransfers = new DataTable();

            string url = "trfr";
            if (this.Text == "Transfer Transactions")
            {
                url = "trfr";
            }
            else if (this.Text == "Pullout Transactions")
            {
                url = "pullout";
            }
            else
            {
                url = "recv";
            }
            //MessageBox.Show("TRANSFER 2: "  +url);

            string warehouseCode = "", branchCode = "", toWarehouseCode = "";
            //WAREHOUSE
            string fromWhse = "", toWhse = "", branch = "";
            cmbWhse.Invoke(new Action(delegate ()
            {
                fromWhse = cmbWhse.Text;
            }));
            foreach (DataRow row in dtWarehouse.Rows)
            {
                if (fromWhse.Equals(row["whsename"].ToString()))
                {
                    warehouseCode = row["whsecode"].ToString();
                    break;
                }
            }
            //TO WAREHOUSE
            cmbToWhse.Invoke(new Action(delegate ()
            {
                toWhse = cmbToWhse.Text;
            }));
            foreach (DataRow row in dtWarehouse.Rows)
            {
                if (toWhse.Equals(row["whsename"].ToString()))
                {
                    toWarehouseCode = row["whsecode"].ToString();
                    break;
                }
            }
            //BRANCH
            cmbBranch.Invoke(new Action(delegate ()
            {
                branch = cmbBranch.Text;
            }));
            foreach (DataRow row in dtBranch.Rows)
            {
                if (branch.Equals(row["name"].ToString()))
                {
                    branchCode = row["code"].ToString();
                    break;
                }
            }

            string sWarehouse = string.IsNullOrEmpty(warehouseCode) ? "" : "&from_whse=" + warehouseCode;
            string sToWarehouse = string.IsNullOrEmpty(toWarehouseCode) ? "" : "&to_whse=" + toWarehouseCode;
            string sBranch = string.IsNullOrEmpty(branchCode) ? "" : "&branch=" + branchCode;
            string sUnderScore = "", sURL = "";
            if (url.Equals("recv") || url.Equals("pullout"))
            {
                sUnderScore = "_";
            }
            if (url.Equals("pullout"))
            {
                sURL = "/api/";
            }
            else
            {
                sURL = "/api/inv/";
            }
            //MessageBox.Show("class: " + URL);

            string sFromDate = checkFromDate.Checked ? dtFromDate.Value.ToString("yyyy-MM-dd") : "",
                sToDate = checkToDate.Checked ? dtToDate.Value.ToString("yyyy-MM-dd") : "", sPlant = "";

            string isBranchToBranch = "";
            checkBranchToBranch.Invoke(new Action(delegate ()
            {
                isBranchToBranch = checkBranchToBranch.Checked ? "1" : "";
            }));
            string sFromTime = "", sToTime = "";
           cmbFromTime.Invoke(new Action(delegate ()
            {
                sFromTime = "&from_time=" + cmbFromTime.Text;
            }));
            cmbToTime.Invoke(new Action(delegate ()
            {
                sToTime = "&to_time=" + cmbToTime.Text;
            }));
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant = "&plant=" + cmbPlant.Text;
            }));

            dtTransfers = transferc.loadData(sURL + url + "/get" + sUnderScore + "all", statusCode, txtsearchTransactions.Text.Trim(), sToDate, gForType, sBranch, sWarehouse, sToWarehouse, sFromDate, isBranchToBranch, this.Text.Equals("Pullout Transactions") ? "" : sFromTime + sToTime + sPlant);
            if (dtTransfers.Rows.Count > 0)
            {
                if(dtTransfers.Rows.Count > 0)
                {
                    DataRow dtRow1 = dtTransfers.Rows[0];
                    if (Convert.ToBoolean(dtRow1[0].ToString()))
                    {
                        AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                        DateTime dtTemp = new DateTime();
                        foreach (DataRow row in dtTransfers.Rows)
                        {
                            string decodeDocStatus = row["docstatus"].ToString() == "O" ? "Open" : row["docstatus"].ToString() == "C" ? "Closed" : "Cancelled";
                            auto.Add(row["reference"].ToString());

                            DateTime dtTransDate = DateTime.TryParse(row["transdate"].ToString(), out dtTemp) ? Convert.ToDateTime(row["transdate"].ToString()) : new DateTime();
                            DateTime dtRecTransDate = Convert.ToDateTime(row["rec_transdate"].ToString());
                            DateTime dtDateConfirmed = Convert.ToDateTime(row["date_confirmed"].ToString());
                            DateTime dtDateClose = Convert.ToDateTime(row["date_close"].ToString());
                            if (!txtsearchTransactions.Text.ToLower().Equals("search reference..."))
                            {
                                if (txtsearchTransactions.Text.ToLower().Equals(row["reference"].ToString().ToLower()))
                                {
                                    dgvTransactions.Invoke(new Action(delegate ()
                                    {
                                        dgvTransactions.Rows.Add(row["id"], row["transnumber"], dtTransDate.Equals(default(DateTime)) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), row["reference"], row["from_whse"], row["to_whse"], row["transtype"], row["sap_number"], decodeDocStatus, row["variance_count"].ToString(), row["rec_reference"], dtRecTransDate.Equals(default(DateTime)) ? "" : dtRecTransDate.ToString("yyyy-MM-dd HH:mm:ss"), row["is_branch_to_branch"], dtDateConfirmed.ToString("yyyy-MM-dd HH:mm:ss"), row["inter_whse"],row["plate_num"],row["shift"],row["agi_truck_scale"],row["chti_truck_scale"],row["vessel"],row["driver"], row["remarks"], dtDateClose == DateTime.MinValue ? "" : dtDateClose.ToString("yyyy-MM-dd HH:mm:ss"));
                                    }));
                                }

                            }
                            else
                            {
                                dgvTransactions.Invoke(new Action(delegate ()
                                {
                                    dgvTransactions.Rows.Add(row["id"], row["transnumber"], dtTransDate.Equals(default(DateTime)) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), row["reference"], row["from_whse"], row["to_whse"], row["transtype"], row["sap_number"], decodeDocStatus, row["variance_count"].ToString(), row["rec_reference"], dtRecTransDate.Equals(default(DateTime)) ? "" : dtRecTransDate.ToString("yyyy-MM-dd HH:mm:ss"), row["is_branch_to_branch"], dtDateConfirmed.ToString("yyyy-MM-dd HH:mm:ss"), row["inter_whse"], row["plate_num"], row["shift"], row["agi_truck_scale"], row["chti_truck_scale"], row["vessel"], row["driver"], row["remarks"], dtDateClose == DateTime.MinValue ? "" : dtDateClose.ToString("yyyy-MM-dd HH:mm:ss"));
                                }));
                            }
                        }
                        txtsearchTransactions.Invoke(new Action(delegate ()
                        {
                            txtsearchTransactions.AutoCompleteCustomSource = auto;
                        }));
                    }
                    else
                    {
                        MessageBox.Show(dtRow1[1].ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            lblNoDataFound.Invoke(new Action(delegate ()
            {
                lblNoDataFound.Visible = (dgvTransactions.Rows.Count <= 0 ? true : false);
            }));

            if (this.Text == "Transfer Transactions")
            {
                checkVariance();
            }
        }


        private void checkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkFromDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        public void bg()
        {
            if (!backgroundWorker1.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void txtsearchTransactions_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsearchTransactions.Text.Trim()))
            {
                txtsearchTransactions.Text = "Search Reference...";
                txtsearchTransactions.ForeColor = Color.DimGray;
            }
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnSearchQuery2_Click(object sender, EventArgs e)
        {
            bg();
        }

        public void closeForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Loading")
                {
                    frm.Hide();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }


        private void txtsearchTransactions_Enter(object sender, EventArgs e)
        {
            if (txtsearchTransactions.Text.ToLower().Equals("search reference..."))
            {
                txtsearchTransactions.Text = string.Empty;
                txtsearchTransactions.ForeColor = Color.Black;
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void checkBranchToBranch_CheckedChanged(object sender, EventArgs e)
        {
            if(this.Text== "Transfer Transactions")
            {
                cmbWhse.Items.Clear();
                cmbToWhse.Items.Clear();
                loadWarehouse(cmbWhse, this.Text.Equals("Received Transactions") ? true : false);
                loadWarehouse(cmbToWhse, this.Text.Equals("Received Transactions") ? false : true);
            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbWhse.Items.Clear();
            cmbToWhse.Items.Clear();
            loadWarehouse(cmbWhse, this.Text.Equals("Received Transactions") ? true : false);
            loadWarehouse(cmbToWhse, this.Text.Equals("Received Transactions") ? false : true);
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches();
        }

        private void dgvTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTransactions.Rows.Count > 0)
            {
                var referenceColumn = dgvTransactions.Columns[e.ColumnIndex];
                if (referenceColumn != null && referenceColumn.Name.Equals("reference"))
                {
                    string sText = "";
                    if (this.Text.Equals("Transfer Transactions"))
                    {
                        sText = "Transfer Items";
                    }
                    else if (this.Text.Equals("Received Transactions"))
                    {
                        sText = "Received Items";
                    }
                    else
                    {
                        sText = "Pullout Items";

                    }
                    TransferItems transferItems = new TransferItems(gForType);
                    transferItems.selectedID = Convert.ToInt32(dgvTransactions.CurrentRow.Cells["id"].Value.ToString());

                    transferItems.Text = sText;
                    transferItems.ShowDialog();
                    if (TransferItems.isSubmit)
                    {
                        bg();
                    }
                }
            }
        }
    }
}
