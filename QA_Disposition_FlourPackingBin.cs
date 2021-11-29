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
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using AB.API_Class;

namespace AB
{
    public partial class QA_Disposition_FlourPackingBin : Form
    {
        public QA_Disposition_FlourPackingBin(string type,string tabName)
        {
            gType = type;
            gTabName = tabName;
            InitializeComponent();
        }
        string gType = "", gTabName = "";
        public string docStatus = "";
        bool isSameRef = false;
        int currentRowIndex = 0;
        Random rndColor = new Random();
        Color randomColor;
        DataTable dtPlant = new DataTable(), dtWarehouse = new DataTable();
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        utility_class utilityc = new utility_class();
        color_class colorc = new color_class();
        int currentColorIndex = 0;
        string lastColumnName = "";
        private void QA_Disposition_FlourPackingBin_Load(object sender, EventArgs e)
        {
            grd.Columns["btn_approve"].Visible = grd.Columns["btn_reject"].Visible = gType.Equals("For Disposition");
            grd.Columns["btn_close"].Visible = gType.Equals("Rejected") && docStatus.Equals("O");
            checkDate.Checked = !(docStatus.Equals("O") || gType.Equals("For Disposition"));
            dtFromDate.EditValue = DateTime.Now;
            dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Properties.Items.Count - 1;
            loadPlant();
            bg();
        }

        public void loadData()
        {
          try
            {

                grd.Invoke(new Action(delegate ()
                {
                    grd.DataSource = null;
                    grd.Rows.Clear();
                }));
                bool cFromDate = false, cToDate = false;
                string sDtFromDate = "?from_date=", sDtToDate = "&to_date=", sFromTime = "&from_time=", sToTime = "&to_time=", sPlant = "&plant=", sTabName = "&tab=" + gTabName;
                checkDate.Invoke(new Action(delegate ()
                {
                    cFromDate = checkDate.Checked;
                }));
                checkToDate.Invoke(new Action(delegate ()
                {
                    cToDate = checkToDate.Checked;
                }));
                dtFromDate.Invoke(new Action(delegate ()
                {
                    sDtFromDate += cFromDate ? dtFromDate.Text : "";
                }));
                dtToDate.Invoke(new Action(delegate ()
                {
                    sDtToDate += cToDate ? dtToDate.Text : "";
                }));
                cmbFromTime.Invoke(new Action(delegate ()
                {
                    sFromTime += cmbFromTime.Text;
                }));
                cmbToTime.Invoke(new Action(delegate ()
                {
                    sToTime += cmbToTime.Text;
                }));
                cmbPlant.Invoke(new Action(delegate ()
                {
                    sPlant += apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
                }));

                string sDispoStatus = gType.Equals("For Disposition") ? "&dispo_status=O&is_rejected=" : gType.Equals("Rejected") && docStatus.Equals("O") ? "&dispo_status=O&is_rejected=1" : gType.Equals("Rejected") && docStatus.Equals("C") ? "&dispo_status=C&is_rejected=1" : gType.Equals("Approved") ? "&dispo_status=C&is_rejected=0" : "";
                string sParams = sDtFromDate + sDtToDate + sFromTime + sToTime + sPlant + sDispoStatus + sTabName;
                string sResult = apic.loadData("/api/inv/trfr/for_dispo", sParams, "", "", RestSharp.Method.GET, true);

                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.Trim().StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        JArray jaData = (JArray)joResult["data"] == null ? new JArray() : (JArray)joResult["data"];
                        DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                        DateTime dtTemp = new DateTime();
                        double doubleTemp = 0.00;
                        int intTemp = 0;
                        grd.Invoke(new Action(delegate ()
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    DateTime dtTransDate = row["transdate"] == null ? dtTemp : DateTime.TryParse(row["transdate"].ToString(), out dtTemp) ? Convert.ToDateTime(row["transdate"].ToString()) : dtTemp;
                                    DateTime dtFinalDispoDate = row["final_dispo_closed_date"] == null ? dtTemp : DateTime.TryParse(row["final_dispo_closed_date"].ToString(), out dtTemp) ? Convert.ToDateTime(row["final_dispo_closed_date"].ToString()) : dtTemp;

                                    double quantity = row["quantity"] == null ? doubleTemp : double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;

                                    int id = row["id"] == null ? intTemp : int.TryParse(row["id"].ToString(), out intTemp) ? Convert.ToInt32(row["id"].ToString()) : intTemp;
                                    int remarksCount = row["remarks_count"] == null ? intTemp : int.TryParse(row["remarks_count"].ToString(), out intTemp) ? Convert.ToInt32(row["remarks_count"].ToString()) : intTemp;

                                    grd.Rows.Add(dtTransDate == DateTime.MinValue ? "" : dtTransDate.ToString("yyyy-MM-dd HH:mm:ss"), row["reference"].ToString(), row["item_code"].ToString(), quantity, row["from_whse"].ToString(), row["to_whse"].ToString(), row["remarks"].ToString(), null, dtFinalDispoDate == DateTime.MinValue ? "" : dtFinalDispoDate.ToString("yyyy-MM-dd HH:mm:ss"), null, id, remarksCount);

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
                                    string currentItemCode = grd.Rows[i].Cells["item_code"].Value.ToString();
                                    for (int j = 0; j < grd.Rows.Count; j++)
                                    {
                                        currentColorIndex = currentColorIndex >= colorc.c.Count() ? 0 : currentColorIndex;
                                        string currentRef1 = grd.Rows[j].Cells["reference"].Value.ToString();
                                        string currentItemCode1 = grd.Rows[j].Cells["item_code"].Value.ToString();
                                        //if (currentRef.Equals("FB-TRFR-100000422") && currentRef1.Equals("FB-TRFR-100000422"))
                                        //{
                                        //    Console.WriteLine(currentRef + "/" + currentRef1 + Environment.NewLine + currentItemCode + "/" + currentItemCode1 + Environment.NewLine + i + "/" + j);
                                        //}
                                        bool v = (currentRef == currentRef1) && (currentItemCode != currentItemCode1) && (i != j);
                                        bool v2 = (currentRef == currentRef1) && (currentItemCode == currentItemCode1) && (i != j);
                                        if (currentRef == currentRef1)
                                        {
                                            if (v)
                                            {
                                                grd.Rows[i].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                                grd.Rows[j].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                            }else if(v2)
                                            {
                                                grd.Rows[i].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                                grd.Rows[j].Cells["reference"].Style.BackColor = colorc.c[currentColorIndex];
                                            }else
                                            {
                                                currentColorIndex++;
                                            }

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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        public void apiPut(string title, string url, int id)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + title + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string sResult = apic.loadData(url, id.ToString(), "", "", RestSharp.Method.PUT, true);
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        bool isSuccess = (bool)joResult["success"];
                        string msg = joResult["message"].ToString();
                        MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        bg();
                    }
                }
            }

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

        public void loadPlant()
        {
            cmbPlant.Properties.Items.Clear();
            cmbPlant.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/plant/get_all","","","", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                   
                    foreach(DataRow row in dtPlant.Rows)
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

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkDate.Checked;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void repositoryItemTextEdit4_Click(object sender, EventArgs e)
        {
            //string selectedColumnText = gridView1.FocusedColumn.FieldName;
            //int issuedID = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) : 0;
            //int prodID = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "prod_receipt_id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "prod_receipt_id").ToString()) : 0;
            //if (selectedColumnText.Equals("issued_reference"))
            //{
            //    Production_IssueProduction_Items frm = new Production_IssueProduction_Items("Closed");
            //    frm.selectedID = issuedID;
            //}
            //int remarksCount = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "remarks_count").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "remarks_count").ToString()) : 0;
            //if (id <= 0)
            //{
            //    MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    if (selectedColumnText.Equals("reference"))
            //    {
            //        string forTransferType = docStatus.Equals("O") && gType.Equals("Rejected") ? "Reject For Close" : "Closed";
            //        TransferItems frm = new TransferItems(forTransferType);
            //        frm.Text = "Transfer Items";
            //        frm.selectedID = id;
            //        frm.ShowDialog();
            //        bg();
            //    }
            //}
            
            //string selectedColumnText = gridView1.FocusedColumn.FieldName;
            //int intTemp = 0;
            //int issuedID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id") == null ? 0 : Int32.TryParse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "issued_id").ToString()) : 0;
            //int prodID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "prod_receipt_id") == null ? 0 : Int32.TryParse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "prod_receipt_id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "prod_receipt_id").ToString()) : 0;
            //if (selectedColumnText.Equals("issued_reference"))
            //{

            //    Production_IssueProduction_Items frm = new Production_IssueProduction_Items("Closed");
            //    frm.selectedID = issuedID;
            //    frm.ShowDialog();
            //}
            //else if (selectedColumnText.Equals("prod_receipt_reference"))
            //{
            //    Production_ReceivedProduction_Items frm = new Production_ReceivedProduction_Items("Closed");
            //    frm.selectedID = prodID;
            //    frm.ShowDialog();
            //}
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            //string selectedColumnText = gridView1.FocusedColumn.FieldName;
            //int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            //int remarksCount = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "remarks_count").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "remarks_count").ToString()) : 0;
            //if (id <= 0)
            //{
            //    MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    if (selectedColumnText.Equals("view_remarks"))
            //    {
            //        QA_Remarks.isSubmit = false;
            //        QA_Remarks frm = new QA_Remarks("/api/inv/trfr/remarks/get_all/", "/api/inv/trfr/remarks/", "/api/inv/trfr/remarks/get_by_id/");
            //        frm.selectedID = id;
            //        frm.ShowDialog();
            //        if (QA_Remarks.isSubmit)
            //        {
            //            bg();
            //        }
            //    }
            //}
        }

        public void btnPut( string title, string url)
        {
            int intTemp = 0;
            int id = grd.CurrentRow.Cells["id"].Value == null ? intTemp : int.TryParse(grd.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
            int remarksCount = grd.CurrentRow.Cells["remarks_count"].Value == null ? intTemp : int.TryParse(grd.CurrentRow.Cells["remarks_count"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["remarks_count"].Value.ToString()) : intTemp;
            if (id <= 0)
            {
                MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (remarksCount <= 0)
                {
                    MessageBox.Show("Please Add Remarks first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    apiPut(title, url, id);
                }
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
           
            //if (e.Column.FieldName.Equals("reference"))
            //{
            //    string currentRef = gridView1.GetRowCellValue(e.RowHandle, "reference") == null ? "" : gridView1.GetRowCellValue(e.RowHandle, "reference").ToString();
            //    string currentItemCode = gridView1.GetRowCellValue(e.RowHandle, "item_code") == null ? "" : gridView1.GetRowCellValue(e.RowHandle, "item_code").ToString();
            //    for (int i = 0; i < gridView1.DataRowCount; i++)
            //    {
            //        //bool isSameRef3 = (gridView1.GetRowCellValue(i, "reference").ToString() == currentRef) && (i != e.RowHandle);
            //        bool isSameReff = (gridView1.GetRowCellValue(i, "reference").ToString() == currentRef) && (gridView1.GetRowCellValue(i, "item_code").ToString() != currentItemCode) && (i != e.RowHandle);
            //        if (colorc.c.Count() <= currentColorIndex)
            //        {
            //            currentColorIndex = 0;
            //        }
            //        if (isSameReff)
            //        {
            //            randomColor = colorc.c[currentColorIndex];
            //            e.Appearance.BackColor = randomColor;
            //        }

            //        else if (gridView1.GetRowCellValue(i, "reference").ToString() != currentRef && (i != e.RowHandle))
            //        {
            //            currentColorIndex += 1;
            //        }
            //    }
            //}
            
        }

        private void repositoryItemButtonEdit5_Click(object sender, EventArgs e)
        {
            //string selectedColumnText = gridView1.FocusedColumn.FieldName;
            //int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            //if (id <= 0)
            //{
            //    MessageBox.Show("ID not found!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    if (selectedColumnText.Equals("btn_view_history"))
            //    {
            //        PackingBinsHistory frm = new AB.PackingBinsHistory(id);
            //        frm.ShowDialog();
            //    }
            //}
        }
        //string currentRef1 = "", currentItemCode1 = "";
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName.Equals("reference"))
            //{
            //    currentRef1 = e.DisplayText;
            //}else if (e.Column.FieldName.Equals("reference"))
            //{
            //    currentItemCode1 = e.DisplayText;
            //}
            //for (int j = 0; j < gridView1.DataRowCount; j++)
            //{
            //    string currentRef2 = gridView1.GetRowCellValue(j, "reference") == null ? "" : gridView1.GetRowCellValue(j, "reference").ToString();
            //    string currentItemCode2 = gridView1.GetRowCellValue(j, "item_code") == null ? "" : gridView1.GetRowCellValue(j, "item_code").ToString();
            //    bool b = (currentRef1 == currentRef2) && (currentItemCode1 != currentItemCode2);
            //    if (b)
            //    {
            //        e.Column.inde
            //        e.Column.AppearanceCell.BackColor = Color.LightBlue;
            //    }
            //}
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void gridView1_TopRowChanged(object sender, EventArgs e)
        {
        
            currentRowIndex = 0;
        }

        private void grd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var c = grd.Columns[e.ColumnIndex];
            int intTemp = 0;
            int id = grd.CurrentRow.Cells["id"].Value == null ? intTemp : int.TryParse(grd.CurrentRow.Cells["id"].Value.ToString(), out intTemp) ? Convert.ToInt32(grd.CurrentRow.Cells["id"].Value.ToString()) : intTemp;
            if (c.Name.Equals("view_remarks"))
            {

                QA_Remarks.isSubmit = false;
                QA_Remarks frm = new QA_Remarks("/api/inv/trfr/remarks/get_all/", "/api/inv/trfr/remarks/", "/api/inv/trfr/remarks/get_by_id/");
                frm.selectedID = id;
                frm.ShowDialog();
                if (QA_Remarks.isSubmit)
                {
                    bg();
                }
            }
            else if (c.Name.Equals("view_history"))
            {
                PackingBinsHistory frm = new AB.PackingBinsHistory(id);
                frm.ShowDialog();
            }
            else if (c.Name.Equals("btn_close"))
            {

                if (gType.Equals("Rejected") && docStatus.Equals("O"))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to close this transaction?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string sResult = apic.loadData("/api/inv/trfr/dispo/close/", id.ToString(), "", "", RestSharp.Method.PUT, true);
                        if (sResult != null)
                        {
                            if (!string.IsNullOrEmpty(sResult.Trim()))
                            {
                                if (sResult.StartsWith("{"))
                                {
                                    JObject joResult = JObject.Parse(sResult);
                                    bool isSuccess = false, boolTemp = false;
                                    isSuccess = (bool)joResult["success"];
                                    string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
                                    MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                                    bg();
                                }
                            }
                        }
                    }
                }
            }
            else if (c.Name.Equals("btn_approve"))
            {
                btnPut("approve", "/api/inv/trfr/dispo/approve/");
            }
            else if (c.Name.Equals("btn_reject"))
            {
                btnPut("reject", "/api/inv/trfr/dispo/reject/");
            }
        }
        int OldRow = 0;
        private void grd_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = grd.HitTest(e.X, e.Y);

            if (hti.RowIndex >= 0 && hti.RowIndex != OldRow)
            {
                grd.Rows[OldRow].Selected = false;
                grd.Rows[hti.RowIndex].Selected = true;
                OldRow = hti.RowIndex;
            }
        }

        //private void grd_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    if(e.RowIndex > -1)
        //    {
        //        if (grd.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.White)
        //        {
        //            grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(191, 236, 255);
        //            grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
        //        }
        //    }
        //}

        //private void grd_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex > -1)
        //    {
        //        if (grd.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.FromArgb(191, 236, 255))
        //        {
        //            grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        //            grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
        //        }

        //    }
        //}

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            //int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            //DialogResult dialogResult = MessageBox.Show("Are you sure you want to close this transaction?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    if (gType.Equals("Rejected") && docStatus.Equals("O"))
            //    {
            //        string sResult = apic.loadData("/api/inv/trfr/dispo/close/", id.ToString(), "", "", RestSharp.Method.PUT, true);
            //        if (sResult != null)
            //        {
            //            if (!string.IsNullOrEmpty(sResult.Trim()))
            //            {
            //                if (sResult.StartsWith("{"))
            //                {
            //                    JObject joResult = JObject.Parse(sResult);
            //                    bool isSuccess = false, boolTemp = false;
            //                    isSuccess = (bool)joResult["success"];
            //                    string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
            //                    MessageBox.Show(msg, isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            //                    bg();
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }
    }
}
