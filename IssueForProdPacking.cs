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
using AB.API_Class;

namespace AB
{
    public partial class IssueForProdPacking : Form
    {
        public IssueForProdPacking(string type, int transferID)
        {
            InitializeComponent();
            gType = type;
            gTransferID = transferID;
        }
        public int selectedID = 0, gTransferID = 0;
        api_class apic = new api_class();
        DataTable dtItem = new DataTable(), dtItemGroup = new DataTable();
        public static JArray jaSelected = new JArray();
        public static bool isSubmit = false;
        string gType = "";
        public Form frmm;
        private void ListItems_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            issue_class issuec = new issue_class();
            if (issuec.checkIssueDepartments(issuec.prodLists))
            {
                bg(bgItemGroup);
                bg(backgroundWorker1);
            }
            else
            {
                flowLayoutPanel1.Invoke(new Action(delegate ()
                {
                    flowLayoutPanel1.Controls.Clear();
                }));
                cmbItemGroup.Invoke(new Action(delegate ()
                {
                    cmbItemGroup.Properties.Items.Clear();
                    cmbItemGroup.Text = "";
                }));
                jaSelected = new JArray();
                apic.showCustomMsgBox("Validation", issuec.checkIssueDepartmentsMsgVal(issuec.prodLists,"Production"));
            }
            if (gTransferID > 0)
            {
                frmm.Hide();
                btnDone.PerformClick();
            }
        }

        public void loadItemGroup()
        {
            cmbItemGroup.Invoke(new Action(delegate ()
            {
                cmbItemGroup.Properties.Items.Clear();
                cmbItemGroup.Properties.Items.Add("All");
            }));

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
                        cmbItemGroup.Invoke(new Action(delegate ()
                        {
                            cmbItemGroup.Properties.Items.Add(row["code"].ToString());
                        }));
                    }
                }
            }
            cmbItemGroup.Invoke(new Action(delegate ()
            {
                cmbItemGroup.SelectedIndex = 0;
            }));

        }

        public void bg(BackgroundWorker bgg)
        {
            if (!bgg.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgg.RunWorkerAsync();
            }
        }

        public void loadData()
        {
            try
            {
                string sItemGroup = "?item_group=", sParams = "", sSearch = "", sItemGroupValue = "";
                flowLayoutPanel1.Invoke(new Action(delegate ()
                {
                    flowLayoutPanel1.Controls.Clear();
                }));
                cmbItemGroup.Invoke(new Action(delegate ()
                {
                    sItemGroup += cmbItemGroup.SelectedIndex <= 0 || cmbItemGroup.Text.ToLower().Contains("all") ? "" : cmbItemGroup.Text;
                    sItemGroupValue = cmbItemGroup.Text;
                }));
                txtSearch.Invoke(new Action(delegate ()
                {
                    sSearch = txtSearch.Text;
                }));

                string sResult = "";
                sParams = sItemGroup;
                sResult = apic.loadData("/api/inv/whseinv/getall", sParams, "", "", RestSharp.Method.GET, true);
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
                                if (itemCode.ToLower().Trim().Equals(sSearch.ToLower().Trim()))
                                {
                                    loadUI(itemCode, quantity, uom);
                                }
                            }
                            else
                            {

                                loadUI(itemCode, quantity, uom);
                            }
                        }
                        //txtSearch.Invoke(new Action(delegate ()
                        //{
                        //    txtSearch.AutoCompleteCustomSource = auto;
                        //}));
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void loadUI(string itemCode, double quantity, string uom)
        {
            //try
            //  {
            Panel pn = new Panel();
            pn.Name = "pn_" + itemCode;
            pn.Tag = uom;
            pn.Cursor = Cursors.Hand;
            pn.AutoScroll = true;
            pn.Size = new Size(169, 181);
            pn.BorderStyle = BorderStyle.FixedSingle;
            DataTable dtt = (DataTable)JsonConvert.DeserializeObject(jaSelected.ToString(), typeof(DataTable));
            foreach (DataRow row in dtt.Rows)
            {
                if (itemCode.ToLower().Trim().Equals(row["item_code"].ToString().ToLower().Trim()))
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
            lblQuantity.Click += new EventHandler(lblQuantityClick);
            pn.Controls.Add(lblQuantity);

            pn.Click += new EventHandler(panelClick);
            try
            {
                flowLayoutPanel1.Invoke(new Action(delegate ()
                {
                    flowLayoutPanel1.Controls.Add(pn);
                }));
            }
            catch(Exception ex)
            {

            }
        
            //}
            //catch(Exception ex)
            //{
            //    //Console.WriteLine(ex.ToString());
            //}
        }

        private void lblQuantityClick(object sender, EventArgs e)
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
                        try
                        {
                            IssueForProdPacking_Details.isSubmit = false;
                            string currentBranch = Login.jsonResult["data"]["branch"] == null ? "" : Login.jsonResult["data"]["branch"].ToString();
                            string sParams = "?branch=" + currentBranch;

                            showAvailableQtyPerWhse.selectedWhse = "";
                            showAvailableQtyPerWhse.isSubmit = false;
                            showAvailableQtyPerWhse frm = new showAvailableQtyPerWhse(itemCode, uom, "Add");
                            frm.hiddenTitle = gType;
                            Invoke((Action)(() => {
                                frm.ShowDialog();
                            }));
                            this.Focus();
                            if (IssueForProdPacking_Details.isSubmit)
                            {
                                pnFinal.BackColor = Color.FromArgb(247, 92, 92);

                                int cIsExist = isExist(itemCode, IssueForProdPacking_Details.fromWhse);
                                if (cIsExist <= -1)
                                {
                                    JObject joSelected = new JObject();
                                    joSelected.Add("item_code", itemCode);
                                    joSelected.Add("quantity", IssueForProdPacking_Details.quantity);
                                    joSelected.Add("whsecode", IssueForProdPacking_Details.fromWhse);
                                    joSelected.Add("uom", uom);
                                    jaSelected.Add(joSelected);
                                    loadData();
                                }
                                else
                                {
                                    string quantityFieldName = gType.Equals("Issue For Production") ? "quantity" : "actual_quantity";
                                    double newQuantity = jaSelected[cIsExist][quantityFieldName].IsNullOrEmpty() ? doubleTemp : double.TryParse(jaSelected[cIsExist][quantityFieldName].ToString(), out doubleTemp) ? Convert.ToDouble(jaSelected[cIsExist][quantityFieldName].ToString()) : doubleTemp;
                                    if (!jaSelected[cIsExist][quantityFieldName].IsNullOrEmpty())
                                    {
                                        jaSelected[cIsExist][quantityFieldName] = (IssueForProdPacking_Details.quantity + newQuantity);
                                        loadData();
                                    } 
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("You cannot click this because Panel Final is null", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public int isExist(string itemCode, string whseCode)
        {
            if (jaSelected != null)
            {
                for (int i = 0; i < jaSelected.Count; i++)
                {
                    JObject joSelected = JObject.Parse(jaSelected[i].ToString());
                    string joItemCode = joSelected["item_code"].IsNullOrEmpty() ? "" : joSelected["item_code"].ToString();
                    string joWhseCode = joSelected["whsecode"].IsNullOrEmpty() ? "" : joSelected["whsecode"].ToString();
                    if ((itemCode.Trim() == joItemCode.Trim()) && (whseCode.Trim() == joWhseCode.Trim()))
                    {
                        return i;
                    }
                }
            }
            else
            {
                return -1;
            }
            return -1;
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            issue_class issuec = new issue_class();
            if (issuec.checkIssueDepartments(issuec.prodLists))
            {
                bg(bgItemGroup);
                bg(backgroundWorker1);
            }
            else
            {
                flowLayoutPanel1.Invoke(new Action(delegate ()
                {
                    flowLayoutPanel1.Controls.Clear();
                }));
                cmbItemGroup.Invoke(new Action(delegate ()
                {
                    cmbItemGroup.Properties.Items.Clear();
                    cmbItemGroup.Text = "";
                }));
                jaSelected = new JArray();
                apic.showCustomMsgBox("Validation", issuec.checkIssueDepartmentsMsgVal(issuec.prodLists,"Production"));
            }

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            //IssueForProdPacking_SelectedItems.isSubmit = false;
            //IssueForProdPacking_SelectedItems frm = new IssueForProdPacking_SelectedItems(jaSelected, gType);
            //frm.selectedID = selectedID;
            //frm.ShowDialog();
            //if (IssueForProdPacking_SelectedItems.isSubmit)
            //{
            //    isSubmit = true;
            //    bg();
            //}
            //else
            //{
            //    bg();
            //}

            if (gType.Equals("Issue For Production"))
            {
                issue_class issuec = new issue_class();
                if (issuec.checkIssueDepartments(issuec.prodLists))
                {
                    IssueForProd_Dialog.isSubmit = false;
                    IssueForProd_Dialog frm = new IssueForProd_Dialog(jaSelected, gType);
                    frm.selectedID = selectedID;
                    frm.ShowDialog();
                    isSubmit = true;

                    if (cmbItemGroup.Properties.Items.Count <= 0)
                    {
                        bg(bgItemGroup);
                    }
                    bg(backgroundWorker1);
                }
                else
                {
                    flowLayoutPanel1.Invoke(new Action(delegate ()
                    {
                        flowLayoutPanel1.Controls.Clear();
                    }));
                    cmbItemGroup.Invoke(new Action(delegate ()
                    {
                        cmbItemGroup.Properties.Items.Clear();
                        cmbItemGroup.Text = "";
                    }));
                    jaSelected = new JArray();
                    apic.showCustomMsgBox("Validation", issuec.checkIssueDepartmentsMsgVal(issuec.prodLists, "Production"));
                }

            }
            else if (gType.Equals("Issue For Packing"))
            {
                issue_class issuec = new issue_class();
                if (issuec.checkIssueDepartments(issuec.prodLists))
                {
                    IssueForProdPacking_Dialog.isSubmit = false;
                    IssueForProdPacking_Dialog frm = new IssueForProdPacking_Dialog(jaSelected, gType, gTransferID);
                    frm.selectedID = selectedID;
                    frm.ShowDialog();

                    if(cmbItemGroup.Properties.Items.Count <= 0)
                    {
                        bg(bgItemGroup);
                    }
                    bg(backgroundWorker1);
                }
                else
                {
                    flowLayoutPanel1.Invoke(new Action(delegate ()
                    {
                        flowLayoutPanel1.Controls.Clear();
                    }));
                    cmbItemGroup.Invoke(new Action(delegate ()
                    {
                        cmbItemGroup.Properties.Items.Clear();
                        cmbItemGroup.Text = "";
                    }));
                    jaSelected = new JArray();
                    apic.showCustomMsgBox("Validation", issuec.checkIssueDepartmentsMsgVal(issuec.prodLists, "Production"));
                }

            }
        }

        private void bgItemGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            loadItemGroup();
        }

        private void bgItemGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void IssueForProdPacking_FormClosing(object sender, FormClosingEventArgs e)
        {
            //foreach(Control c in this.Controls)
            // {
            //     c.Dispose();
            // }
        }

        private void IssueForProdPacking_Shown(object sender, EventArgs e)
        {
          
        }

        private void cmbItemGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
