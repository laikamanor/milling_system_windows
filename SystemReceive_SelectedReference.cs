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
    public partial class SystemReceive_SelectedReference : Form
    {
        public SystemReceive_SelectedReference(int id)
        {
            InitializeComponent();
            selectedID = id;
        }
        int selectedID = 0;
        api_class apic = new api_class();
        DataTable dt = new DataTable();
        string gResult = "";
        string currentItemGroup = "";
        public static JArray jaSelected = new JArray();
        private void SystemReceive_SelectedReference_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblInformation.Text = "Reference #: " + Environment.NewLine + "From Whse: " + Environment.NewLine + "To Whse: " + Environment.NewLine + "Remarks: " + Environment.NewLine + "Transdate: ";
            bg();
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

        public void loadData()
        {
            try
            {
                flowLayoutPanel1.Invoke(new Action(delegate ()
                {
                    flowLayoutPanel1.Controls.Clear();
                }));
                string sParams = "", sSearch = "";
                txtSearch.Invoke(new Action(delegate ()
                {
                    sSearch = txtSearch.Text;
                }));
                sParams = selectedID.ToString();
                string sResult = "";
                if (string.IsNullOrEmpty(gResult.Trim()))
                {
                    if (!gResult.StartsWith("{"))
                    {
                        sResult = apic.loadData("/api/inv/trfr/forrec/getdetails/", sParams, "", "", RestSharp.Method.GET, true);
                        gResult = sResult;
                    }
                    else
                    {
                        sResult = gResult;
                    }
                }
                else
                {
                    sResult = gResult;
                }
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        JArray jaData = (JArray)joResult["data"];
                        dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                        AutoCompleteStringCollection auto = new AutoCompleteStringCollection();

                        foreach (DataRow row in dt.Rows)
                        {
                            //info
                            lblInformation.Invoke(new Action(delegate ()
                            {
                                lblInformation.Text = "Reference #: " + row["reference"].ToString() + Environment.NewLine + "From Whse: " + row["from_whse"].ToString() + Environment.NewLine + "To Whse: " + row["to_whse"].ToString() + Environment.NewLine + "Remarks: " + row["remarks"].ToString() + Environment.NewLine + row["transdate"].ToString();
                            }));


                            double quantity = 0, doubleTemp = 0;
                            quantity = double.TryParse(row["quantity"].ToString(), out doubleTemp) ? Convert.ToDouble(row["quantity"].ToString()) : doubleTemp;
                            string itemCode = row["item_code"] == null ? "" : row["item_code"].ToString();
                            string uom = row["uom"] == null ? "" : row["uom"].ToString();
                            auto.Add(itemCode);
                            if (!string.IsNullOrEmpty(sSearch.Trim()))
                            {
                                if (itemCode.ToLower().Trim().Contains(sSearch.ToLower().Trim()))
                                {
                                    loadUI(itemCode, uom, quantity);
                                }
                            }
                            else
                            {
                                loadUI(itemCode, uom, quantity);
                            }
                        }
                        txtSearch.Invoke(new Action(delegate ()
                        {
                            txtSearch.AutoCompleteCustomSource = auto;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void loadUI(string itemCode,string uom, double quantity)
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
            lblItem.Name = "lbl_reference_" + itemCode;
            lblItem.Tag = "pn_" + itemCode;
            lblItem.Font = new Font("Arial", 12, FontStyle.Bold);
            lblItem.Location = new Point(4, 14);
            lblItem.AutoSize = false;
            lblItem.Size = new Size(160, 125);
            lblItem.Click += new EventHandler(lblItemClick);
            pn.Controls.Add(lblItem);

            Label lblQuantity = new Label();

            lblQuantity.Text = "Del Qty.: " + String.Format("{0:#,0.000}", quantity);
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
            eventClick(con);
        }

        private void lblItemClick(object sender, EventArgs e)
        {
            Control con = (Control)sender;
            eventClick(con);
        }

        private void panelClick(object sender, EventArgs e)
        {
            Control con = (Control)sender;
            eventClick(con);
        }


        public void eventClick(Control con)
        {
            try
            {
                Panel pn = null;
                Label lbl = null, lblFinal = null;
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
                        uom = pnFinal.Tag.ToString();
                        foreach (Control c in pnFinal.Controls)
                        {
                            if (c is Label)
                            {
                                if (c.Name.ToLower().Trim().Contains("lbl_reference_"))
                                {
                                    lblFinal = (Label)c;
                                }
                                else if (c.Name.ToString().Trim().Contains("lbl_item_quantity_"))
                                {
                                    string r = c.Name.ToString().Replace("lbl_item_quantity_", "").Trim();
                                    quantity = double.TryParse(r, out doubleTemp) ? Convert.ToDouble(r) : doubleTemp;
                                }
                        }
                    }
                }
                else if (lbl != null)
                {
                    Control[] cc = this.Controls.Find(lbl.Tag.ToString(), true);
                    if(cc.Count() > 0)
                    {
                        pnFinal = (Panel)cc[0];
                    }
                    if (pnFinal != null)
                    {
                        uom = pnFinal.Tag.ToString();
                        foreach (Control c in pnFinal.Controls)
                        {
                            if (c is Label)
                            {
                                if (c.Name.ToLower().Trim().Contains("lbl_reference_"))
                                {
                                    lblFinal = (Label)c;
                                }
                                else if (c.Name.ToString().Trim().Contains("lbl_item_quantity_"))
                                {
                                    string r = c.Name.ToString().Replace("lbl_item_quantity_", "").Trim();
                                    quantity = double.TryParse(r, out doubleTemp) ? Convert.ToDouble(r) : doubleTemp;
                                }
                            }
                        }
                    }
                }
                if (pnFinal != null)
                {
                    if (pnFinal.BackColor == Color.FromArgb(247, 92, 92))
                    {
                        apic.showCustomMsgBox("Validation", "This item is selected!");
                    }else
                    {
                        int id = 0, intTemp = 0;
                        string selectedRef = lblFinal == null ? "" : lblFinal.Text;
                        if (!string.IsNullOrEmpty(selectedRef.Trim()))
                        {
                            SystemReceive_Dialog.isSubmit = false;
                            SystemReceive_Dialog.actualQty = 0.00;
                            SystemReceive_Dialog frm = new SystemReceive_Dialog(selectedRef, uom, quantity);
                            frm.ShowDialog();
                            if (SystemReceive_Dialog.isSubmit)
                            {
                                string fromWhse = "", toWhse = "";
                               if(dt.Rows.Count > 0)
                                {
                                    DataRow row = dt.Rows[0];
                                    fromWhse = row["from_whse"].ToString();
                                    toWhse = row["to_whse"].ToString();

                                    JObject joDetails = new JObject();
                                    joDetails.Add("item_code", selectedRef);
                                    joDetails.Add("uom", uom);
                                    joDetails.Add("quantity", quantity);
                                    joDetails.Add("actualrec", SystemReceive_Dialog.actualQty);
                                    joDetails.Add("from_whse", fromWhse);
                                    joDetails.Add("to_whse",toWhse);
                                    jaSelected.Add(joDetails);
                                    pnFinal.BackColor = Color.FromArgb(247, 92, 92);
                                    Console.WriteLine(jaSelected);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            jaSelected = new JArray();
            SystemReceive.jaSelected = new JArray();
            SystemReceive frm = new SystemReceive();
            MainMenu.showForm(frm);
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bg();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            SystemReceive_Selected.isSubmit = false;
            SystemReceive_Selected frm = new SystemReceive_Selected(jaSelected,dt);
            frm.ShowDialog();
            loadData();
            if (SystemReceive_Selected.isSubmit)
            {
                btnBack.PerformClick();
            }
        }
    }
}
