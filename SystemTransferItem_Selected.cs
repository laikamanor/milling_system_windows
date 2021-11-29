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
    public partial class SystemTransferItem_Selected : Form
    {
        public SystemTransferItem_Selected(JArray jSelected)
        {
            InitializeComponent();
            jaSelected = jSelected;
        }
        JArray jaSelected = new JArray();
        public static bool isSubmit = false;
        private void SystemTransferItem_Selected_Load(object sender, EventArgs e)
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
                }
                gridView1.BestFitColumns();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(jaSelected.Count <= 0)
            {
                MessageBox.Show("No Item Selected!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                isSubmit = true;
                SystemTransferItem_Dialog frm = new SystemTransferItem_Dialog(jaSelected);
                frm.ShowDialog();
                this.Hide();
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
            string fromWhse = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "from_whse").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "from_whse").ToString() : "";
            string toWhse = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "to_whse").ToString()) ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "to_whse").ToString() : "";
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
                SystemTransferItem_Details frm = new SystemTransferItem_Details(itemCode,uom, quantity,"Edit");
                SystemTransferItem_Details.fromWhse = "";
                SystemTransferItem_Details.toWhse = "";
                SystemTransferItem_Details.isSubmit = false;
                SystemTransferItem_Details.quantity = 0;
                frm.txtQuantity.Text = String.Format("{0:#,0.000}", quantity);
                frm.lblFromWhse.Text = fromWhse;
                frm.lblToWhse.Text = toWhse;
                frm.ShowDialog();
                if (SystemTransferItem_Details.isSubmit)
                {
                    jaSelected.RemoveAt(selectedIndex);
                    JObject joSelected = new JObject();
                    joSelected.Add("item_code", itemCode);
                    joSelected.Add("quantity", SystemTransferItem_Details.quantity);
                    joSelected.Add("from_whse", SystemTransferItem_Details.fromWhse);
                    joSelected.Add("to_whse", SystemTransferItem_Details.toWhse);
                    joSelected.Add("uom", uom);
                    jaSelected.Add(joSelected);
                    loadData();
                }
            }
        }

        private void SystemTransferItem_Selected_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemTransferItem.jaSelected = jaSelected;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
