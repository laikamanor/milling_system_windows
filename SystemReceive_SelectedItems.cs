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
    public partial class SystemReceive_SelectedItems : Form
    {
        public SystemReceive_SelectedItems(JArray jSelected)
        {
            InitializeComponent();
            jaSelected = jSelected;
        }
        JArray jaSelected = new JArray();
        public static bool isSubmit = false;
        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        public void loadData()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaSelected.ToString(), typeof(DataTable));
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("variance");
                dt.Columns.Add("edit");
                dt.Columns.Add("remove");

                double doubleTemp = 0.00;
                foreach(DataRow row in dt.Rows)
                {
                    double quantity = 0.00, actualQty = 0.00;
                    quantity = row.IsNull("quantity") ? 0.00 : double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;
                    actualQty = row.IsNull("actualrec") ? 0.00 : double.TryParse(row["actualrec"].ToString(), out doubleTemp) ? Convert.ToDouble(row["actualrec"].ToString()) : doubleTemp;
                    row["variance"] = actualQty - quantity;
                }

                gridControl1.DataSource = dt;
                foreach (GridColumn col in gridView1.Columns)
                {
                    string fieldName = col.FieldName;
                    string v = col.GetCaption();
                    string s = col.GetCaption().Replace("_", " ");
                    col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                    col.ColumnEdit = fieldName.Equals("remove") ? repositoryItemButtonEdit1 : fieldName.Equals("edit") ? repositoryItemButtonEdit2 : repositoryItemTextEdit1;
                    col.DisplayFormat.FormatType = fieldName.Equals("quantity") || fieldName.Equals("actualrec") || fieldName.Equals("variance")  ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                    col.DisplayFormat.FormatString = fieldName.Equals("quantity") || fieldName.Equals("actualrec") || fieldName.Equals("variance") ? "{0:#,0.000}" : "";
                    col.Visible = !(fieldName.Equals("from_whse") || fieldName.Equals("to_whse"));
                    col.Caption = fieldName.Equals("quantity") ? "Del Qty." : fieldName.Equals("actualrec") ? "Actual Receive" : col.GetCaption();
                }
                gridView1.BestFitColumns();
            }
        }

        private void SystemReceive_SelectedItems_Load(object sender, EventArgs e)
        {
            loadData();
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
            //string fromWhse = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "from_whse").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "from_whse").ToString() : "";
            //string toWhse = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "to_whse").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "to_whse").ToString() : "";
            double quantity = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "quantity").ToString()) ? Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "quantity").ToString()) : 0.00;
            double actualrec = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "actualrec").ToString()) ? Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "actualrec").ToString()) : 0.00;

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
                SystemReceive_Dialog.isSubmit = false;
                SystemReceive_Dialog.actualQty = 0.00;
                SystemReceive_Dialog frm = new SystemReceive_Dialog(itemCode, uom, quantity);
                frm.txtQuantity.Text = String.Format("{0:#,0.000}", actualrec);
                frm.ShowDialog();
                if (SystemReceive_Dialog.isSubmit)
                {
                    jaSelected[selectedIndex]["actualrec"] = SystemReceive_Dialog.actualQty;
                    loadData();
                }
            }
        }
    }
}
