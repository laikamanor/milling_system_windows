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

namespace AB
{
    public partial class SystemTransferItem : Form
    {
        public SystemTransferItem()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        DataTable dtItemGroup = new DataTable(), dtItem = new DataTable();
        string gResult = "";
        private EventHandler lblQuantityClick;
        string currentItemGroup = "";
        public static JArray jaSelected = new JArray();
        private void SystemReceiveItem_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadItemGroup();
            bg();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
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

        public void loadItemGroup()
        {
            cmbItemGroup.Properties.Items.Clear();
            cmbItemGroup.Properties.Items.Add("All");
            string sResult = apic.loadData("/api/item/item_grp/getall", "", "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtItemGroup = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtItemGroup.Rows)
                    {
                        cmbItemGroup.Properties.Items.Add(row["code"].ToString());
                    }
                }
            }
            cmbItemGroup.SelectedIndex = 0;
        }

        public void loadData()
        {
         try
            {
                flowLayoutPanel1.Invoke(new Action(delegate ()
                {
                    flowLayoutPanel1.Controls.Clear();
                }));
                string sItemGroup = "?item_group=", sParams = "", sSearch = "", sItemGroupValue = "";
                cmbItemGroup.Invoke(new Action(delegate ()
                {
                    sItemGroup += cmbItemGroup.SelectedIndex <= 0 || cmbItemGroup.Text.ToLower().Contains("all") ? "" : cmbItemGroup.Text;
                    sItemGroupValue = cmbItemGroup.Text;
                }));
                txtSearch.Invoke(new Action(delegate ()
                {
                    sSearch = txtSearch.Text;
                }));
                sParams = sItemGroup;
                string sResult = "";
                if (string.IsNullOrEmpty(gResult.Trim()))
                {
                    if (!gResult.StartsWith("{") || currentItemGroup != sItemGroupValue)
                    {
                        sResult = apic.loadData("/api/inv/whseinv/getall", sParams, "", "", RestSharp.Method.GET, true);
                        gResult = sResult;
                    }
                    else
                    {
                        sResult = gResult;
                    }
                }
                else
                {
                    if (currentItemGroup != sItemGroupValue)
                    {
                        sResult = apic.loadData("/api/inv/whseinv/getall", sParams, "", "", RestSharp.Method.GET, true);
                        gResult = sResult;
                    }
                    else
                    {
                        sResult = gResult;
                    }
                }
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        JArray jaData = (JArray)joResult["data"];
                        dtItem = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                        AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                        foreach (DataRow row in dtItem.Rows)
                        {
                            double quantity = 0.00, doubleTemp = 0.00;
                            quantity = double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;
                            string itemCode = row["item_code"] == null ? "" : row["item_code"].ToString();
                            string uom = row["uom"] == null ? "" : row["uom"].ToString();
                            auto.Add(itemCode);
                            if (!string.IsNullOrEmpty(sSearch.Trim()))
                            {
                                if (itemCode.ToLower().Trim().Contains(sSearch.ToLower().Trim()))
                                {
                                    loadUI(itemCode, quantity,uom);
                                }
                            }
                            else
                            {
                                loadUI(itemCode, quantity, uom);
                            }
                        }
                        txtSearch.Invoke(new Action(delegate ()
                        {
                            txtSearch.AutoCompleteCustomSource = auto;
                        }));
                    }
                }
                cmbItemGroup.Invoke(new Action(delegate ()
                {
                    currentItemGroup = cmbItemGroup.Text;
                }));
            }
            catch(Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void loadUI(string itemCode, double quantity,string uom)
        {
            Panel pn = new Panel();
            pn.Name = "pn_" + itemCode;
            pn.Tag = uom;
            pn.Cursor = Cursors.Hand;
            pn.AutoScroll = true;
            pn.Size = new Size(169, 181);
            pn.BorderStyle = BorderStyle.FixedSingle;
            DataTable dtt = (DataTable)JsonConvert.DeserializeObject(jaSelected.ToString(), typeof(DataTable));
            foreach(DataRow row in dtt.Rows)
            {
                if (itemCode.ToLower().Trim().Contains(row["item_code"].ToString().ToLower().Trim()))
                {
                    pn.BackColor = Color.FromArgb(247, 92, 92);
                }
            }

            Label lblItem = new Label();
            lblItem.Text = itemCode.Length >= 104 ? itemCode.Substring(0, 104) + "..." : itemCode;
            lblItem.Name = "lbl_item_code_" + itemCode;
            lblItem.Tag = "pn_" + itemCode;
            lblItem.Font = new Font("Arial", 12, FontStyle.Bold);
            lblItem.Location = new Point(4, 14);
            lblItem.AutoSize = false;
            lblItem.Size = new Size(160, 125);
            lblItem.Click += new EventHandler(lblItemClick);
            pn.Controls.Add(lblItem);

            Label lblQuantity = new Label();

            lblQuantity.Text = String.Format("{0:#,0.000}", quantity) + " quantity";
            lblQuantity.ForeColor = quantity == 0 ? Color.Red : quantity <= 5 ? Color.Orange : Color.Green;
            lblQuantity.Name = "lbl_item_quantity_" + quantity;
            lblQuantity.Font = new Font("Arial", 12, FontStyle.Bold);
            lblQuantity.Location = new Point(3, 142);
            lblQuantity.AutoSize = false;
            lblQuantity.Size = new Size(160, 28);
            lblQuantity.Tag = "pn_" + itemCode;
            lblQuantity.Click += new EventHandler(qwe);
            pn.Controls.Add(lblQuantity);

            pn.Click += new EventHandler(panelClick);

            flowLayoutPanel1.Invoke(new Action(delegate ()
            {
                flowLayoutPanel1.Controls.Add(pn);
            }));
        }

        private void qwe(object sender, EventArgs e)
        {
            Control con = (Control)sender;
            eventCLick(con);
        }

        private void lblItemClick(object sender, EventArgs e)
        {
            Control con = (Control)sender;
            eventCLick(con);
        }

        private void panelClick(object sender, EventArgs e)
        {
            Control con = (Control)sender;
            eventCLick(con);
        }

        public void eventCLick(Control con)
        {
            Panel pn = null;
            Label lbl = null;
            Panel pnFinal = null;
            if (con is Panel)
            {
                pn = ((Panel)con);
            }
            else if (con is Label)
            {
                lbl = ((Label)con);
            }

            string itemCode = "", uom = "";
            double quantity = 0.00, doubleTemp = .00;

            if (pn != null)
            {
                pnFinal = pn;
            }
            else if (lbl != null)
            {
                Control[] c = this.Controls.Find(lbl.Tag.ToString(), true);
                if (c[0] is Panel)
                {
                    pnFinal = (Panel)c[0];
                }
            }

            if (pnFinal != null)
            {
                uom = pnFinal.Tag.ToString();
                if(pnFinal.BackColor == Color.FromArgb(247, 92, 92))
                {
                    MessageBox.Show("This item is selected!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (pnFinal.Controls.Count > 0)
                    {
                        foreach (Control c in pnFinal.Controls)
                        {
                            if (c is Label)
                            {
                                string lblName = ((Label)c).Name;
                                if (lblName.ToLower().Trim().Contains("lbl_item_code_"))
                                {

                                    itemCode = ((Label)c).Name.ToString().Replace("lbl_item_code_", "").Trim();
                                }
                                else if (lblName.ToLower().Trim().Contains("lbl_item_quantity_"))
                                {
                                    string quantityTemp = ((Label)c).Name.ToString().Replace("lbl_item_quantity_", "").Trim();
                                    quantity = double.TryParse(quantityTemp, out doubleTemp) ? Convert.ToDouble(quantityTemp) : doubleTemp;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(itemCode.Trim()))
                        {
                            SystemTransferItem_Details.isSubmit = false;
                            showAvailableQtyPerWhse.selectedWhse = "";
                            showAvailableQtyPerWhse.isSubmit = false;
                            showAvailableQtyPerWhse frm = new showAvailableQtyPerWhse(itemCode, uom,"Add");
                            frm.hiddenTitle = "System Transfer Item";
                            Invoke((Action)(() => {
                                frm.ShowDialog();
                            }));
                            this.Focus();
                            if (SystemTransferItem_Details.isSubmit)
                            {
                                pnFinal.BackColor = Color.FromArgb(247, 92, 92);
                                JObject joSelected = new JObject();
                                joSelected.Add("item_code", itemCode);
                                joSelected.Add("quantity", SystemTransferItem_Details.quantity);
                                joSelected.Add("from_whse", SystemTransferItem_Details.fromWhse);
                                joSelected.Add("to_whse", SystemTransferItem_Details.toWhse);
                                joSelected.Add("uom", uom);
                                jaSelected.Add(joSelected);
                            }
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("null", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void cmbItemGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            SystemTransferItem_Dialog frm = new SystemTransferItem_Dialog(jaSelected);
            frm.ShowDialog();
            bg();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }
    }
}
