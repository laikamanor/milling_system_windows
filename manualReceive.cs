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
    public partial class manualReceive : Form
    {
        public manualReceive()
        {
            InitializeComponent();
        }
        public int selectedID = 0;
        api_class apic = new api_class();
        DataTable dtItem = new DataTable(), dtItemGroup = new DataTable();
        public static JArray jaSelected = new JArray();
        public static bool isSubmit = false;
        private void manualReceive_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            bg(bgItemGroup);
            bg(backgroundWorker1);
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
                sResult = apic.loadData("/api/item/getall", sParams, "", "", RestSharp.Method.GET, true);
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
                            string itemCode = row["item_code"] == null ? "" : row["item_code"].ToString();
                            string uom = row["uom"] == null ? "" : row["uom"].ToString();
                            auto.Add(itemCode);
                            if (!string.IsNullOrEmpty(sSearch.Trim()))
                            {
                                if (itemCode.ToLower().Trim().Contains(sSearch.ToLower().Trim()))
                                {
                                    loadUI(itemCode, uom);
                                }
                            }
                            else
                            {
                                loadUI(itemCode, uom);
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

        public void loadUI(string itemCode, string uom)
        {
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

            pn.Click += new EventHandler(panelClick);

            flowLayoutPanel1.Invoke(new Action(delegate ()
            {
                flowLayoutPanel1.Controls.Add(pn);
            }));
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
                            //else if (lblName.ToLower().Trim().Contains("lbl_item_quantity_"))
                            //{
                            //    string quantityTemp = ((Label)c).Name.ToString().Replace("lbl_item_quantity_", "").Trim();
                            //    quantity = double.TryParse(quantityTemp, out doubleTemp) ? Convert.ToDouble(quantityTemp) : doubleTemp;
                            //}
                        }
                    }
                    if (!string.IsNullOrEmpty(itemCode.Trim()))
                    {
                        transaction_class transc = new transaction_class();
                        if (transc.isItemExist(itemCode, jaSelected))
                        {
                            apic.showCustomMsgBox("Validation", "This item is selected!");
                        }else
                        {
                            try
                            {
                                manualReceive_Details.isSubmit = false;
                                manualReceive_Details frm = new manualReceive_Details(itemCode, uom, true);
                                frm.ShowDialog();
                                this.Focus();
                                if (manualReceive_Details.isSubmit)
                                {
                                    pnFinal.BackColor = Color.FromArgb(247, 92, 92);
                                    JObject joSelected = new JObject();
                                    joSelected.Add("item_code", itemCode);
                                    joSelected.Add("quantity", manualReceive_Details.quantity);
                                    joSelected.Add("uom", uom);
                                    jaSelected.Add(joSelected);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("You cannot click this because Panel Final is null", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadData();
        }

        private void bgItemGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            loadItemGroup();
        }

        private void bgItemGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            manualReceive_Selected.isSubmit = false;
            manualReceive_Selected frm = new manualReceive_Selected(jaSelected);
            frm.ShowDialog();
            isSubmit = true;
            //this.Hide();
            bg(backgroundWorker1);
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

    }
}
