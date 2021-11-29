using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AB
{
    public partial class manualReceive_selectedItems : Form
    {
        public manualReceive_selectedItems(JArray ja)
        {
            InitializeComponent();
            jaSelected = ja;
            
        }
        JArray jaSelected = new JArray();
        private void manualReceive_selectedItems_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadData();
        }

        public void loadData()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaSelected.ToString(), typeof(DataTable));
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("edit");
                dt.Columns.Add("remove");
                gridControl1.DataSource = dt;
                foreach (GridColumn col in gridView1.Columns)
                {
                    string fieldName = col.FieldName;
                    string v = col.GetCaption();
                    string s = col.GetCaption().Replace("_", " ");
                    col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                    col.ColumnEdit = fieldName.Equals("remove") ? repositoryItemButtonEdit1 : fieldName.Equals("edit") ? repositoryItemButtonEdit2 : repositoryItemTextEdit1;
                    col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                    col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                    col.Caption = fieldName.Equals("edit") ? "Edit Qty" : col.GetCaption();
                }
                gridView1.BestFitColumns();
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            string itemCode = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "item_code").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "item_code").ToString() : "";
            if (selectedColumnText.Equals("remove"))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove " + itemCode + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int isDeletedInt = 0;
                    for (int i = 0; i < jaSelected.Count(); i++)
                    {
                        JObject data = JObject.Parse(jaSelected[i].ToString());
                        foreach (var q in data)
                        {
                            if (q.Key.Equals("item_code"))
                            {
                                if (q.Value.ToString().Trim().ToLower().Equals(itemCode.ToLower().Trim()))
                                {
                                    jaSelected.RemoveAt(i);
                                    isDeletedInt++;
                                    break;
                                }
                            }
                        }
                    }
                    if (isDeletedInt > 0)
                    {
                        loadData();
                    }
                }
            }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            string itemCode = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "item_code").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "item_code").ToString() : "";
            string uom = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "uom").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "uom").ToString() : "";
            double quantity = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "quantity").ToString()) ? Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "quantity").ToString()) : 0.00;

            int selectedIndex = 0;
            for (int i = 0; i < jaSelected.Count(); i++)
            {
                JObject data = JObject.Parse(jaSelected[i].ToString());
                foreach (var q in data)
                {
                    if (q.Key.Equals("item_code"))
                    {
                        if (q.Value.ToString().Trim().ToLower().Equals(itemCode.ToLower().Trim()))
                        {
                            selectedIndex = i;
                        }
                    }
                }
            }

            if (selectedColumnText.Equals("edit"))
            {
                manualReceive_Details frm = new manualReceive_Details(itemCode, uom, false);
                manualReceive_Details.isSubmit = false;
                manualReceive_Details.quantity = 0;
                manualReceive_Details.itemCode = "";
                manualReceive_Details.uom = "";
                frm.txtQuantity.Text = quantity.ToString("n3");
                frm.ShowDialog();
                if (manualReceive_Details.isSubmit)
                {
                    jaSelected.RemoveAt(selectedIndex);
                    JObject joSelected = new JObject();
                    joSelected.Add("item_code", itemCode);
                    joSelected.Add("quantity", manualReceive_Details.quantity);
                    joSelected.Add("uom", uom);
                    jaSelected.Add(joSelected);
                    loadData();
                }

                //IssueForProdPacking_Details.isSubmit = false;
                //string currentBranch = Login.jsonResult["data"]["branch"] == null ? "" : Login.jsonResult["data"]["branch"].ToString();
                //string sParams = "?branch=" + currentBranch;

                //showAvailableQtyPerWhse.selectedWhse = "";
                //showAvailableQtyPerWhse.isSubmit = false;
                //showAvailableQtyPerWhse frm = new showAvailableQtyPerWhse(itemCode, uom);
                //frm.ShowDialog();
                //this.Focus();
                //if (IssueForProdPacking_Details.isSubmit)
                //{
                //         jaSelected.RemoveAt(selectedIndex);
                //    JObject joSelected = new JObject();
                //    joSelected.Add("item_code", itemCode);
                //    joSelected.Add("quantity", IssueForProdPacking_Details.quantity);
                //    joSelected.Add("whsecode", IssueForProdPacking_Details.fromWhse);
                //    joSelected.Add("uom", uom);
                //    jaSelected.Add(joSelected);
                //}
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
