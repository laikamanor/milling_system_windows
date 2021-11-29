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
using AB.API_Class.Warehouse;
using Newtonsoft.Json;

namespace AB
{
    public partial class ItemRequest : Form
    {
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        warehouse_class wahousesc = new warehouse_class();
        string gForType = "";
        DataTable dtBranches = new DataTable(), dtPlant = new DataTable();
        public ItemRequest(string forType)
        {
            gForType = forType;
            InitializeComponent();
        }


        private async void ItemRequest2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            dgv.Columns["btnViewRemarks"].Visible = gForType.Equals("Closed");
            //dtDueDate.Visible = checkDueDate.Checked;
            //dtDueDate.Value = DateTime.Now;
            dtTransDate.Value = DateTime.Now;
            loadPlant();
            loadData();
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


        public void loadBranches(DevExpress.XtraEditors.ComboBoxEdit cmb)
        {
            string sPlant = "?plant=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));
            string sResult = apic.loadData("/api/branch/get_all", sPlant, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtBranches = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    cmb.Properties.Items.Clear();
                    cmb.Properties.Items.Add("All");
                    foreach(DataRow row in dtBranches.Rows)
                    {
                        cmb.Properties.Items.Add(row["name"].ToString());
                    }
                    string currentBranch = (string)Login.jsonResult["data"]["branch"];
                    currentBranch = findBranchValue("code", currentBranch, "name");
                    cmb.SelectedIndex = cmb.Name.Equals("cmbToBranch") ? 0 : cmb.Properties.Items.IndexOf(currentBranch) > 0 ? cmb.Properties.Items.IndexOf(currentBranch) : 0;
                }
            }
        }

        public void loadData()
        {
            string sPlant = "&plant=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));
            Cursor.Current = Cursors.WaitCursor;
            string sDocStatus = "?docstatus=" + (gForType.Equals("Open") ? "O" : gForType.Equals("Cancelled") ? "N" : gForType.Equals("Closed") ? "C" : "");
            //string sConfirmed = gForType.Equals("Cls")  ? "&confirm=" : "&confirm=1";
            //string sDueDate = (!checkDueDate.Checked ? "&duedate=" : "&duedate=" + dtDueDate.Value.ToString("yyyy-MM-dd"));
            string sTransDate = (!checkTransDate.Checked ? "&transdate=" : "&transdate=" + dtTransDate.Value.ToString("yyyy-MM-dd"));
            string sfromWarehouse = (cmbFromBranch.SelectedIndex.Equals(-1) ? "" : cmbFromBranch.SelectedIndex.Equals(0) || cmbFromBranch.Text.ToLower() == "all" ? "&from_branch=" : "&from_branch=" + findBranchValue("name", cmbFromBranch.Text,"code"));
            string stoWarehouse = (cmbToBranch.SelectedIndex.Equals(-1) ? "" : cmbToBranch.SelectedIndex.Equals(0) || cmbToBranch.Text.ToLower() == "all" ? "&to_branch=" : "&to_branch=" + findBranchValue("name",cmbToBranch.Text,"code"));
            string sParams = sDocStatus + sfromWarehouse + stoWarehouse + sTransDate + sPlant;
            string sResult = apic.loadData("/api/inv/item_request/get_all", sParams, "", "", Method.GET, true);
            dgv.Rows.Clear();
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject jObjectResponse = JObject.Parse(sResult);
                    Console.WriteLine("hehe " + sResult);
                    bool isSuccess = false;
                    dgv.Rows.Clear();
                    foreach (var x in jObjectResponse)
                    {
                        if (x.Key.Equals("success"))
                        {
                            isSuccess = Convert.ToBoolean(x.Value.ToString());
                        }
                    }
                    if (isSuccess)
                    {
                        foreach (var z in jObjectResponse)
                        {
                            if (z.Key.Equals("data"))
                            {
                                if (z.Value.ToString() != "[]")
                                {
                                    int id = 0, transferID = 0, intTemp = 0;
                                    string referenceNumber = "", toBranch = "", fromBranch = "", docStatus = "", remarks = "", transferRef = "";
                                    DateTime dtTransDate = new DateTime(), dtDueDate = new DateTime(), dtTransferDate = new DateTime(), dtTemp = new DateTime();
                                    JArray jsonArray = JArray.Parse(z.Value.ToString());
                                    for (int i = 0; i < jsonArray.Count(); i++)
                                    {
                                        JObject jObjectData = JObject.Parse(jsonArray[i].ToString());
                                        foreach (var y in jObjectData)
                                        {
                                            if (y.Key.Equals("from_branch"))
                                            {
                                                fromBranch = y.Value.ToString();
                                            }
                                            else if (y.Key.Equals("to_branch"))
                                            {
                                                toBranch = y.Value.ToString();
                                            }
                                            else if (y.Key.Equals("id"))
                                            {
                                                id = Int32.TryParse(y.Value.ToString(), out intTemp) ? Convert.ToInt32(y.Value.ToString()) : intTemp;
                                            }
                                            else if (y.Key.Equals("reference"))
                                            {
                                                referenceNumber = y.Value.ToString();
                                            }
                                            else if (y.Key.Equals("transdate"))
                                            {
                                                string replaceT = y.Value.ToString().Replace("T", "");
                                                dtTransDate = DateTime.TryParse(replaceT, out dtTemp) ? Convert.ToDateTime(replaceT) : dtTemp;
                                            }
                                            else if (y.Key.Equals("duedate"))
                                            {
                                                string replaceT = y.Value.ToString().Replace("T", "");
                                                dtDueDate = DateTime.TryParse(replaceT, out dtTemp) ? Convert.ToDateTime(replaceT) : dtTemp;
                                            }
                                            else if (y.Key.Equals("docstatus"))
                                            {
                                                docStatus = (y.Value.ToString() == "O" ? "Open" : (y.Value.ToString() == "C" ? "Closed" : "Cancelled"));
                                            }
                                            else if (y.Key.ToString() == "remarks")
                                            {
                                                remarks = y.Value.ToString();
                                            }
                                            else if (y.Key.Equals("transfer_id"))
                                            {
                                                transferID = Int32.TryParse(y.Value.ToString(), out intTemp) ? Convert.ToInt32(y.Value.ToString()) : intTemp;
                                            }
                                            else if (y.Key.ToString() == "transfer_ref")
                                            {
                                                transferRef = y.Value.ToString();
                                            }
                                            else if (y.Key.Equals("transfer_date"))
                                            {
                                                string replaceT = y.Value.ToString().Replace("T", "");
                                                dtTransferDate = DateTime.TryParse(replaceT, out dtTemp) ? Convert.ToDateTime(replaceT) : dtTemp;
                                            }
                                        }
                                        dgv.Rows.Add(id, dtTransDate.Equals(DateTime.MinValue) ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), referenceNumber, fromBranch, toBranch, dtDueDate.Equals(DateTime.MinValue) ? "" : dtDueDate.ToString("yyyy-MM-dd"), docStatus, remarks, transferID, transferRef, dtTransferDate.Equals(DateTime.MinValue) ? "" : dtTransferDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            lblNoDataFound.Visible = (dgv.Rows.Count > 0 ? false : true);
            Cursor.Current = Cursors.Default;
        }


        public string findBranchValue(string valueName, string value,string valueFind)
        {
            string result = "";
            foreach (DataRow row in dtBranches.Rows)
            {
                if (row["name"].ToString() == value)
                {
                    result = row["code"].ToString();
                    break;
                }
            }
            return result;
        }


        private void checkDueDate_CheckedChanged(object sender, EventArgs e)
        {
            dtDueDate.Visible = checkDueDate.Checked;
        }

        private void checkTransDate_CheckedChanged(object sender, EventArgs e)
        {
            dtTransDate.Visible = checkTransDate.Checked;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 11)
                    {
                        int id = 0, intTemp = 0;
                        id = int.TryParse(dgv.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
                        string remarksURL = "/api/inv/item_request/remarks/";
                        string remarksByIdURL = "/api/inv/item_request/remarks/get_by_id/";
                        if(id > 0)
                        {
                            QA_Remarks frm = new QA_Remarks(remarksURL, "", remarksByIdURL);
                            frm.selectedID = id;
                            frm.ShowDialog();
                        }
                        else
                        {
                            apic.showCustomMsgBox("Validation", "No ID Found!");
                        }
                    }
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                if (e.RowIndex >= 0)
                {
                    int id = 0,transferID, intTemp = 0;
                    id = int.TryParse(dgv.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
                    transferID = int.TryParse(dgv.CurrentRow.Cells["transfer_id"].Value.ToString(), out intTemp) ? Convert.ToInt32(dgv.CurrentRow.Cells["transfer_id"].Value.ToString()) : intTemp;
                    if (e.ColumnIndex == 2)
                    {
                        ItemRequest_Items2 itemRequestItems = new ItemRequest_Items2(id, gForType);
                        itemRequestItems.ShowDialog();
                        loadData();
                    }
                    else if (e.ColumnIndex == 9)
                    {
                        if(transferID > 0)
                        {
                            TransferItems frm = new TransferItems("Closed");
                            frm.selectedID = transferID;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No Transfer found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches(cmbFromBranch);
            loadBranches(cmbToBranch);
        }
    }
}
