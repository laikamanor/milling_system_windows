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
    public partial class IssueForProdPacking_SelectedItems : Form
    {
        public IssueForProdPacking_SelectedItems(JArray jSelected, string type)
        {
            InitializeComponent();
            jaSelected = jSelected;
            gType = type;
        }
        JArray jaSelected = new JArray();
        string gType = "";
        public static bool isSubmit = false;
        public int selectedID = 0;
        private void SelectedItems_Load(object sender, EventArgs e)
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
                if(gType.Equals("Issue For Production"))
                {
                    dt.Columns.Add("edit");
                    dt.Columns.Add("remove");
                }
                gridControl1.DataSource = dt;
                //foreach(DataRow row in dt.Rows)
                //{
                //    row["actual_quantity"] = row["balance"];
                //    row["variance"] = 0.00;
                //}
                foreach (GridColumn col in gridView1.Columns)
                {
                    string fieldName = col.FieldName;
                    string v = col.GetCaption();
                    string s = col.GetCaption().Replace("_", " ");
                    col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                    col.ColumnEdit = fieldName.Equals("remove") ? repositoryItemButtonEdit1 : fieldName.Equals("edit") ? repositoryItemButtonEdit2 : fieldName.Equals("actual_quantity") ? repositoryItemTextEdit2 : repositoryItemTextEdit1;
                    col.DisplayFormat.FormatType = fieldName.Equals("balance") || fieldName.Equals("actual_quantity") || fieldName.Equals("variance") || fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                    col.DisplayFormat.FormatString = fieldName.Equals("balance") || fieldName.Equals("actual_quantity") || fieldName.Equals("variance") || fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                    col.Caption = fieldName.Equals("edit") ? "Edit Qty" : col.GetCaption();

                    col.Visible = !(fieldName.Equals("transrow_id"));
                }
                gridView1.BestFitColumns();
            }
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (jaSelected.Count <= 0)
            {
                MessageBox.Show("No Item Selected!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(gType.Equals("Issue For Production"))
                {
                    IssueForProd_Dialog.isSubmit = false;
                    IssueForProd_Dialog frm = new IssueForProd_Dialog(jaSelected, gType);
                    frm.selectedID = selectedID;
                    frm.ShowDialog();
                    if (IssueForProd_Dialog.isSubmit)
                    {
                        isSubmit = true;
                        this.Close();
                    }
                }
                else if(gType.Equals("Issue For Packing"))
                {
                    IssueForProdPacking_Dialog.isSubmit = false;
                    IssueForProdPacking_Dialog frm = new IssueForProdPacking_Dialog(jaSelected, gType,0);
                    frm.selectedID = selectedID;
                    frm.ShowDialog();
                    if (IssueForProdPacking_Dialog.isSubmit)
                    {
                        isSubmit = true;
                        this.Close();
                    }
                }
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
          try
            {
                string selectedColumnText = gridView1.FocusedColumn.FieldName;
                string itemCode = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "item_code").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "item_code").ToString() : "";
                string fromWhse = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "whsecode").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "whsecode").ToString() : "";
                string uom = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "uom").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "uom").ToString() : "";

                string quantityFieldName = gType.Equals("Issue For Production") ? "quantity" : "actual_quantity";

                double quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, quantityFieldName) == null ? 0.00 : gridView1.GetRowCellValue(gridView1.FocusedRowHandle, quantityFieldName).ToString() == "" ? 0.00 : Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, quantityFieldName).ToString());

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
                    IssueForProdPacking_Details frm = new IssueForProdPacking_Details(itemCode, uom, false);
                    IssueForProdPacking_Details.isSubmit = false;
                    IssueForProdPacking_Details.quantity = 0;
                    IssueForProdPacking_Details.balance = 0;
                    IssueForProdPacking_Details.itemCode = "";
                    IssueForProdPacking_Details.uom = "";
                    IssueForProdPacking_Details.fromWhse = "";
                    frm.lblItemCode.Text = "Item Code: " + itemCode;
                    frm.lblItemCode.Text = "UOM: " + uom;
                    frm.txtQuantity.Text = quantity.ToString("n3");
                    frm.lblFromWhse.Text = fromWhse;
                    frm.ShowDialog();
                    if (IssueForProdPacking_Details.isSubmit)
                    {
                        //jaSelected.RemoveAt(selectedIndex);
                        jaSelected[selectedIndex][quantityFieldName] = IssueForProdPacking_Details.quantity;
                        //JObject joSelected = new JObject();
                        //joSelected.Add("item_code", itemCode);
                        //joSelected.Add("actual_quantity", IssueForProdPacking_Details.balance);
                        //joSelected.Add("actual_quantity", );
                        //joSelected.Add("whsecode", IssueForProdPacking_Details.fromWhse);
                        //joSelected.Add("uom", uom);
                        //jaSelected.Add(joSelected);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
 
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          try
            {
                double doubleTemp = 0.00;
                double balance = Convert.ToDouble(gridView1.GetFocusedRowCellValue("balance").ToString());

                var varCol = gridView1.Columns["variance"];
                if (e.Value.ToString().Trim() == "")
                {
                    gridView1.SetRowCellValue(e.RowHandle, varCol, null);
                    if (e.RowHandle >= 0)
                    {
                        jaSelected[e.RowHandle]["actual_quantity"] = null;
                    }
                }
                else
                {
                    double actualCount = double.TryParse(e.Value.ToString(), out doubleTemp) ? Convert.ToDouble(e.Value.ToString()) : doubleTemp;

                    double variance = actualCount - balance;

                    if (e.RowHandle >= 0)
                    {
                        jaSelected[e.RowHandle]["actual_quantity"] = actualCount;
                    }
                    gridView1.SetRowCellValue(e.RowHandle, varCol, variance.ToString("n3"));
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName.Equals("variance"))
            {
                double doubleTemp = 0.00;
                double variance = double.TryParse(e.CellValue.ToString(), out doubleTemp) ? Convert.ToDouble(e.CellValue.ToString()) : doubleTemp;
                e.Appearance.ForeColor = variance == 0 ? Color.Black : variance < 0 ? Color.Red : Color.Blue;
            }
        }
    }
}
