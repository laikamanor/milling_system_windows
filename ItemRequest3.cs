using Newtonsoft.Json;
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
using AB.UI_Class;
using RestSharp;
using AB.API_Class;
namespace AB
{
    public partial class ItemRequest3 : Form
    {
        public ItemRequest3(string docStatus)
        {
            InitializeComponent();
            gDocStatus = docStatus;
        }
        DataTable dtPlant = new DataTable(), dtBranches = new DataTable();
        api_class apic = new api_class();
        color_class colorc = new color_class();
        string gDocStatus = "";
        int currentColorIndex = 0, intTemp = 0, currentRowIndex = 0;
        double doubleTemp = 0.00;
        private void ItemRequest3_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadPlant();
            bg(backgroundWorker1);
            grd.Columns["view_remarks"].Visible = !gDocStatus.Equals("O");
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

        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranches(cmbFromBranch);
            loadBranches(cmbToBranch);
        }

        public void loadData()
        {
            string sPlant = "&plant=";
            cmbPlant.Invoke(new Action(delegate ()
            {
                sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            }));

            string sDocStatus = "?docstatus=" + gDocStatus;
            //string sConfirmed = gForType.Equals("Cls")  ? "&confirm=" : "&confirm=1";
            //string sDueDate = (!checkDueDate.Checked ? "&duedate=" : "&duedate=" + dtDueDate.Value.ToString("yyyy-MM-dd"));
            string sTransDate = (!checkTransDate.Checked ? "&transdate=" : "&transdate=" + dtTransDate.Value.ToString("yyyy-MM-dd"));
            string sfromWarehouse = (cmbFromBranch.SelectedIndex.Equals(-1) ? "" : cmbFromBranch.SelectedIndex.Equals(0) || cmbFromBranch.Text.ToLower() == "all" ? "&from_branch=" : "&from_branch=" + apic.findValueInDataTable(dtBranches, cmbFromBranch.Text, "name", "code"));
            string stoWarehouse = (cmbToBranch.SelectedIndex.Equals(-1) ? "" : cmbToBranch.SelectedIndex.Equals(0) || cmbToBranch.Text.ToLower() == "all" ? "&to_branch=" : "&to_branch=" + apic.findValueInDataTable(dtBranches, cmbToBranch.Text, "name", "code"));
            string sParams = sDocStatus + sfromWarehouse + stoWarehouse + sTransDate + sPlant;
            string sResult = apic.loadData("/api/inv/item_request/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    Console.WriteLine(jaData);
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    grd.Invoke(new Action(delegate ()
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtTemp = new DateTime();
                            foreach (DataRow row in dt.Rows)
                            {
                                DateTime dtTransDate = row["transdate"] == null ? dtTemp : DateTime.TryParse(row["transdate"].ToString(), out dtTemp) ? Convert.ToDateTime(row["transdate"].ToString()) : dtTemp;
                                DateTime dtTransferDate = row["transfer_date"] == null ? dtTemp : DateTime.TryParse(row["transfer_date"].ToString(), out dtTemp) ? Convert.ToDateTime(row["transfer_date"].ToString()) : dtTemp;

                                //double quantity = row["quantity"] == null ? doubleTemp : double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;

                                int id = row["id"] == null ? intTemp : int.TryParse(row["id"].ToString(), out intTemp) ? Convert.ToInt32(row["id"].ToString()) : intTemp;
                                int transferID = row["transfer_id"] == null ? intTemp : int.TryParse(row["transfer_id"].ToString(), out intTemp) ? Convert.ToInt32(row["transfer_id"].ToString()) : intTemp;

                                grd.Rows.Add(dtTransDate == DateTime.MinValue ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), row["reference"].ToString(), row["from_branch"].ToString(), row["to_branch"].ToString(), row["remarks"].ToString(),"", row["transfer_ref"].ToString(), dtTransferDate == DateTime.MinValue ? "" : dtTransferDate.ToString("yyyy-MM-dd HH:mm:ss"), id, transferID);
                            }

                            foreach (DataGridViewColumn col in grd.Columns)
                            {
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                int colw = col.Width;
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                col.Width = colw;
                            }
                            currentRowIndex = 0;
                            for (int i = 0; i < grd.Rows.Count; i++)
                            {

                                string currentRef = grd.Rows[i].Cells["reference"].Value.ToString();
                                for (int j = 0; j < grd.Rows.Count; j++)
                                {
                                    currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                                    string currentRef1 = grd.Rows[j].Cells["reference"].Value.ToString();
                                    bool v = (currentRef == currentRef1) && (i != j);
                                    if (v)
                                    {
                                        grd.Rows[i].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                        grd.Rows[j].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                    }
                                    else if (currentRef != currentRef1)
                                    {
                                        currentColorIndex++;
                                    }
                                }
                            }
                            var col2 = grd.Columns["remarks"];
                            if (col2 != null)
                            {
                                col2.Width = 200;
                            }
                        }
                    }));
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public void bg(BackgroundWorker bgw)
        {
            if (!bgw.IsBusy)
            {
                this.Enabled = true;
                this.Cursor = Cursors.WaitCursor;
                bgw.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void grd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int viewRemarksIndex = grd.Columns["view_remarks"].Index;
            int reference = grd.Columns["reference"].Index;
            int transferRef = grd.Columns["transfer_reference"].Index;
            if(viewRemarksIndex== e.ColumnIndex)
            {
                int id = 0, intTemp = 0;
                id = grd.CurrentRow.Cells["id"].Value== null ? 0 : int.TryParse(grd.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
                string remarksURL = "/api/inv/item_request/remarks/";
                string remarksByIdURL = "/api/inv/item_request/remarks/get_by_id/";
                QA_Remarks frm = new QA_Remarks(remarksURL, "", remarksByIdURL);
                frm.selectedID = id;
                frm.ShowDialog();
            }
            else if(reference == e.ColumnIndex)
            {
                int id = 0, intTemp = 0;
                id = grd.CurrentRow.Cells["id"].Value == null ? 0 : int.TryParse(grd.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
                ItemRequest_Items2.isSubmit = false;
                string docStatusEncode = gDocStatus.Equals("O") ? "Open" : gDocStatus.Equals("C") ? "Closed" : gDocStatus.Equals("N") ? "Cancelled" : "";
                ItemRequest_Items2 frm = new ItemRequest_Items2(id, docStatusEncode);
                frm.ShowDialog();
                if (ItemRequest_Items2.isSubmit)
                {
                    loadData();
                }
            }
            else if(transferRef == e.ColumnIndex)
            {
                int transferID = 0, intTemp = 0;
                transferID = grd.CurrentRow.Cells["transfer_id"].Value == null ? 0 : int.TryParse(grd.CurrentRow.Cells["transfer_id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["transfer_id"].Value.ToString()) : intTemp;
                TransferItem_Details.isSubmit = false;
                TransferItem_Details frm = new TransferItem_Details(transferID);
                frm.ShowDialog();
                if (TransferItem_Details.isSubmit)
                {
                    loadData();
                }
            }
        }

        private void checkTransDate_CheckedChanged(object sender, EventArgs e)
        {
            dtTransDate.Visible = checkTransDate.Checked;
        }

        public void closeForm()
        {
            this.Enabled = true;
            this.Cursor = grd.Cursor  = Cursors.Default;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
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
                    foreach (DataRow row in dtBranches.Rows)
                    {
                        cmb.Properties.Items.Add(row["name"].ToString());
                    }
                    string currentBranch = (string)Login.jsonResult["data"]["branch"];
                    currentBranch = apic.findValueInDataTable(dtBranches, currentBranch, "name", "code");
                    cmb.SelectedIndex = cmb.Name.Equals("cmbToBranch") ? 0 : cmb.Properties.Items.IndexOf(currentBranch) > 0 ? cmb.Properties.Items.IndexOf(currentBranch) : 0;
                }
            }
        }
    }
}
