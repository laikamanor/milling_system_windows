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
    public partial class SystemReceive : Form
    {
        public SystemReceive()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        DataTable  dt = new DataTable();
        string gResult = "";
        private EventHandler lblQuantityClick;
        string currentItemGroup = "";
        public static JArray jaSelected = new JArray();
        private void SystemReceive_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
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
                sParams = "";
                string sResult = "";
                if (string.IsNullOrEmpty(gResult.Trim()))
                {
                    if (!gResult.StartsWith("{"))
                    {
                        sResult = apic.loadData("/api/inv/trfr/forrec?mode=For Sales Items", sParams, "", "", RestSharp.Method.GET, true);
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
                            int id = 0, intTemp = 0;
                            id = int.TryParse(row["id"].ToString(), out intTemp) ? Convert.ToInt32(row["id"].ToString()) : intTemp;
                            string reference = row["reference"] == null ? "" : row["reference"].ToString();
                            //string uom = row["uom"] == null ? "" : row["uom"].ToString();
                            auto.Add(reference);
                            if (!string.IsNullOrEmpty(sSearch.Trim()))
                            {
                                if (reference.ToLower().Trim().Contains(sSearch.ToLower().Trim()))
                                {
                                    loadUI(reference, id);
                                }
                            }
                            else
                            {
                                loadUI(reference, id);
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

        public void loadUI(string reference, int id)
        {
            Panel pn = new Panel();
            pn.Name = "pn_" + reference;
            pn.Tag = id;
            pn.Cursor = Cursors.Hand;
            pn.AutoScroll = true;
            pn.Size = new Size(169, 181);
            pn.BorderStyle = BorderStyle.FixedSingle;
            DataTable dtt = (DataTable)JsonConvert.DeserializeObject(jaSelected.ToString(), typeof(DataTable));
            foreach (DataRow row in dtt.Rows)
            {
                if (reference.ToLower().Trim().Contains(row["item_code"].ToString().ToLower().Trim()))
                {
                    pn.BackColor = Color.FromArgb(247, 92, 92);
                }
            }

            Label lblItem = new Label();
            lblItem.Text = reference.Length >= 104 ? reference.Substring(0, 104) + "..." : reference;
            lblItem.Name = "lbl_reference_" + reference;
            lblItem.Tag = "pn_" + reference;
            lblItem.Font = new Font("Arial", 12, FontStyle.Bold);
            lblItem.Location = new Point(4, 14);
            lblItem.AutoSize = false;
            lblItem.Size = new Size(160, 125);
            lblItem.Click += new EventHandler(lblItemClick);
            pn.Controls.Add(lblItem);

            //Label lblQuantity = new Label();

            //lblQuantity.Text = String.Format("{0:#,0.000}", quantity) + " quantity";
            //lblQuantity.ForeColor = quantity == 0 ? Color.Red : quantity <= 5 ? Color.Orange : Color.Green;
            //lblQuantity.Name = "lbl_item_quantity_" + quantity;
            //lblQuantity.Font = new Font("Arial", 12, FontStyle.Bold);
            //lblQuantity.Location = new Point(3, 142);
            //lblQuantity.AutoSize = false;
            //lblQuantity.Size = new Size(160, 28);
            //lblQuantity.Tag = "pn_" + itemCode;
            //lblQuantity.Click += new EventHandler(qwe);
            //pn.Controls.Add(lblQuantity);

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
                    foreach(Control c in pnFinal.Controls)
                    {
                        if(c is Label)
                        {
                            if (c.Name.ToLower().Trim().Contains("lbl_reference_"))
                            {
                                lblFinal =(Label) c;
                                break;
                            }
                        }
                    }
                }
                else if (lbl != null)
                {
                    Control[] c = this.Controls.Find(lbl.Tag.ToString(), true);
                    if (c.Count() > 0)
                    {
                        if (c[0] is Panel)
                        {
                            pnFinal = (Panel)c[0];
                            lblFinal = lbl;
                        }
                    }
                }
                if (pnFinal != null)
                {
                    int id = 0, intTemp = 0;
                    string selectedRef = lblFinal == null ? "" : lblFinal.Text;
                    if (!string.IsNullOrEmpty(selectedRef.Trim())){
                        id = int.TryParse(pnFinal.Tag.ToString(), out intTemp) ? Convert.ToInt32(pnFinal.Tag.ToString()) : intTemp;

                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to select " + selectedRef + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            this.Hide();
                            jaSelected = new JArray();
                            SystemReceive_SelectedReference.jaSelected = new JArray();
                            SystemReceive_SelectedReference frm = new SystemReceive_SelectedReference(id);
                            MainMenu.showForm(frm);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
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
            bg();
        }
    }
}
