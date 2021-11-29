using RestSharp;
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
using System.Globalization;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AB
{
    public partial class ReceiveTransaction_SAP : Form
    {
        public ReceiveTransaction_SAP()
        {
            InitializeComponent();
        }
        api_class apic = new api_class();
        DataTable dtWarehouse = new DataTable();
        public bool hasSAP = false;
        private void ReceiveTransaction_SAP_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            btnViewItems.Text = !hasSAP ? "Consolidated for SAP #" : "View Items";
            btnViewItems.Size = !hasSAP ? new Size(199, 35) : new Size(122, 35);
            btnViewItems.Image =!hasSAP ? Properties.Resources.sap : Properties.Resources.view;
            checkFromDate.Checked = checkToDate.Checked = true;
            dtFromDate.EditValue = dtToDate.EditValue = DateTime.Now;
            cmbFromTime.SelectedIndex = 0;
            cmbToTime.SelectedIndex = cmbToTime.Items.Count - 1;
            cmbPerPage.SelectedIndex = 0;
            loadWarehouse();
            bg(backgroundWorker1);
        }

        public void loadWarehouse()
        {
            try
            {
                cmbWhse.Items.Clear();
                string sResult = "";
                sResult = apic.loadData("/api/whse/get_all","", "", "", Method.GET, true);
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    dtWarehouse = apic.getDtDownloadResources(sResult, "data");
                }
                if (dtWarehouse.Rows.Count > 1)
                {
                    cmbWhse.Invoke(new Action(delegate ()
                    {
                        cmbWhse.Items.Add("All");
                    }));
                }

                DataView dv = dtWarehouse.DefaultView;
                dv.Sort = "id ASC";
                dtWarehouse = dv.ToTable();
                foreach (DataRow row in dtWarehouse.Rows)
                {
                    cmbWhse.Invoke(new Action(delegate ()
                    {
                        cmbWhse.Items.Add(row["whsename"].ToString());
                    }));
                }
                cmbWhse.Invoke(new Action(delegate ()
                {
                    string whse = (string)Login.jsonResult["data"]["whse"];
                    cmbWhse.SelectedIndex = cmbWhse.Items.IndexOf(whse) <= 0 ? 0 : cmbWhse.Items.IndexOf(whse);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Visible = checkFromDate.Checked;
        }

        private void checkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Visible = checkToDate.Checked;
        }

        private void btnSearchQuery_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        public void loadData(string pageNumber)
        {
            bool isFromDate = false, isToDate = false;
            string sFromDate = "?from_date=", sToDate = "&to_date=", sWhseCode = "&whsecode=", sParams = "", sPerPage = "&per_page=", sHasSAP = "&has_sap_num=" + (hasSAP ? "1" : "");
            checkFromDate.Invoke(new Action(delegate ()
            {
                isFromDate = checkFromDate.Checked;
            }));
            checkToDate.Invoke(new Action(delegate ()
            {
                isToDate = checkToDate.Checked;
            }));
            dtFromDate.Invoke(new Action(delegate ()
            {
                sFromDate += !isFromDate ? "" : dtFromDate.Text;
            }));
            dtToDate.Invoke(new Action(delegate ()
            {
                sToDate += !isToDate ? "" : dtToDate.Text;
            }));
            cmbWhse.Invoke(new Action(delegate ()
            {
                sWhseCode += apic.findValueInDataTable(dtWarehouse, cmbWhse.Text, "whsename", "whsecode");
            }));
            cmbPerPage.Invoke(new Action(delegate ()
            {
                sPerPage += cmbPerPage.Text;
            }));
            gridControl1.Invoke(new Action(delegate ()
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
            }));
            sParams = sFromDate + sToDate + sWhseCode + sPerPage + sHasSAP;
            string sResult = apic.loadData("/api/inv/recv/for_sap/get_all", sParams, "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jTransfer = (JArray)joResult["data"]["transfer"];
                    JArray jPageList = (JArray)joResult["data"]["page_list"];

                    bool hasNext = false, hasPrev = false;
                    hasNext = (bool)joResult["data"]["has_next"];
                    hasPrev = (bool)joResult["data"]["has_prev"];
                    int currentPage = (int)joResult["data"]["current_page"];
                    loadPagination(jPageList, hasNext, hasPrev, currentPage);

                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jTransfer.ToString(), (typeof(DataTable)));
                    dt.SetColumnsOrder(new string[] { "id:", "transdate", "reference", "reference2","from_whse","to_whse", "transtype", "sap_number", "docstatus","plate_num", "shift", "agi_truck_scale", "chti_truck_scale", "vessel", "driver", "remarks", "type2","supplier", "created_by", "updated_by", "canceled_by", "date_created", "date_updated", "date_canceled" });
                    gridControl1.Invoke(new Action(delegate ()
                    {
                        gridControl1.DataSource = null;
                        gridControl1.DataSource = dt;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string v = col.FieldName;
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = v.Equals("transdate") || v.ToLower().Equals("date_created") || v.ToLower().Equals("date_updated") ? DevExpress.Utils.FormatType.DateTime : DevExpress.Utils.FormatType.None;
                            col.DisplayFormat.FormatString = v.Equals("transdate") || v.ToLower().Equals("date_created") || v.ToLower().Equals("date_updated") ? "yyyy-MM-dd HH:mm:ss" : "";
                            col.Visible = v.Equals("docstatus") || v.Equals("id") || v.Equals("reference2") || v.Equals("type2") || v.Equals("created_by") || v.Equals("updated_by") || v.Equals("canceled_by") ||v.Equals("supplier") ||v.Equals("date_created") || v.Equals("date_updated") || v.Equals("date_canceled") ? false : true;
                        }
                        if (dt.Rows.Count > 0)
                        {
                            gridView1.OptionsSelection.MultiSelect = true;
                            gridView1.Columns["reference"].Summary.Clear();
                            gridView1.Columns["reference"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "reference", "Count: {0:N0}");
                        }
                        gridView1.BestFitColumns();
                    }));
                }
            }
        }

          public void loadPagination(JArray lists,bool hasNext, bool hasPrev, int currentPage)
        {
            panelPagination.Invoke(new Action(delegate ()
            {
                panelPagination.Controls.Clear();
            }));
            FontFamily fontArial = new FontFamily("Arial");
            Button btnPrev = new Button();
            btnPrev.Size = new Size(70, 30);
            btnPrev.Font = new Font(fontArial, 9, FontStyle.Bold);
            btnPrev.Location = new Point(12, 7);
            btnPrev.Cursor = Cursors.Hand;
            btnPrev.Text = "< Prev";
            btnPrev.BackColor = Color.FromArgb(225, 227, 234);
            btnPrev.ForeColor = Color.FromArgb(76,76,76);
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(130,130,130);
            btnPrev.Enabled = hasPrev;
            btnPrev.Click += new EventHandler(btnPrev_click);

            panelPagination.Invoke(new Action(delegate ()
            {
                panelPagination.Controls.Add(btnPrev);
            }));

            Size btnSize = new Size(52, 30);
            int btnLocX = 87;
            foreach(string list in lists)
            {
                Button btnNumber = new Button();
                btnNumber.Size = btnSize;
                btnNumber.Cursor = Cursors.Hand;
                btnNumber.Location = new Point(btnLocX, 7);
                btnNumber.Text = list;
                btnNumber.Click += new EventHandler(btnNumber_click);

                double num = 0.00, doubleTemp = 0.00;
                num = double.TryParse(list, out doubleTemp) ? Convert.ToDouble(list) : doubleTemp;
                btnNumber.Enabled = num <= 0 ? false : true;
                btnNumber.FlatAppearance.BorderSize = num <= 0 ? 0 : 1;
                btnNumber.FlatAppearance.BorderColor = Color.FromArgb(130, 130, 130);
                btnNumber.Font = new Font(fontArial, 9, FontStyle.Bold);
                btnNumber.FlatStyle = FlatStyle.Flat;
                btnNumber.BackColor = num <=0 ? Color.White : Color.FromArgb(225, 227, 234);
                btnNumber.ForeColor = num <= 0 ? Color.White : Color.FromArgb(76, 76, 76);

                //first
                if (currentPage == num)
                {
                    btnNumber.BackColor = Color.FromArgb(178, 187, 217);
                    btnNumber.ForeColor = Color.FromArgb(76,76,76);
                }

                panelPagination.Invoke(new Action(delegate ()
                {
                    panelPagination.Controls.Add(btnNumber);
                    btnLocX += 54;
                }));
            }
            Button btnNext = new Button();
            btnNext.Size = new Size(70, 30);
            btnNext.Cursor = Cursors.Hand;
            btnNext.Text = "Next >";
            btnNext.Location = new Point(btnLocX, 7);
            btnNext.BackColor = Color.FromArgb(225, 227, 234);
            btnNext.ForeColor = Color.FromArgb(76, 76, 76);
            btnNext.Enabled = hasNext;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(130,130,130);
            btnNext.Font = new Font(fontArial, 9, FontStyle.Bold);
            btnNext.Click += new EventHandler(btnNext_click);
            panelPagination.Invoke(new Action(delegate ()
            {
                panelPagination.Controls.Add(btnNext);
            }));
        }

        private void btnNext_click(object sender, EventArgs e)
        {
            foreach (Control c in panelPagination.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    Button btnC = (c as Button);
                    if (btnC.BackColor == Color.FromArgb(178, 187, 217))
                    {
                        double num = 0.00, doubleTemp = 0.00;
                        num = double.TryParse(btnC.Text, out doubleTemp) ? Convert.ToDouble(btnC.Text) : doubleTemp;
                        num += 1;

                        BackgroundWorker bg1 = new BackgroundWorker();
                        bg1.WorkerReportsProgress = true;
                        bg1.DoWork += delegate
                        {
                            loadData(num.ToString());
                        };
                        bg1.RunWorkerCompleted += delegate
                        {
                            closeForm();
                        };
                        bg(bg1);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("not a button");
                }
            }
        }

        private void btnPrev_click(object sender, EventArgs e)
        {
            foreach (Control c in panelPagination.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    Button btnC = (c as Button);
                   if(btnC.BackColor== Color.FromArgb(178, 187, 217))
                    {
                        double num = 0.00, doubleTemp = 0.00;
                        num = double.TryParse(btnC.Text, out doubleTemp) ? Convert.ToDouble(btnC.Text) : doubleTemp;
                        num -= 1;
                        BackgroundWorker bg1 = new BackgroundWorker();
                        bg1.WorkerReportsProgress = true;
                        bg1.DoWork += delegate
                        {
                            loadData(num.ToString());
                        };
                        bg1.RunWorkerCompleted += delegate
                        {
                            closeForm();
                        };
                        bg(bg1);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("not a button");
                }
            }
        }

        private void btnNumber_click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            BackgroundWorker bg1 = new BackgroundWorker();
            bg1.WorkerReportsProgress = true;
            bg1.DoWork += delegate
            {
                loadData(button.Text);
            };
            bg1.RunWorkerCompleted += delegate
            {
                closeForm();
            };
            bg(bg1);
        }

        public void bg(BackgroundWorker bgw )
        {
            if (!bgw.IsBusy)
            {
                closeForm();
                Loading frm = new Loading();
                frm.Show();
                bgw.RunWorkerAsync();
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
            loadData("");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
        }

        private void btnSearchQuery2_Click(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void cmbWhse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            bg(backgroundWorker1);
        }

        private void btnViewItems_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            if (gridView1.RowCount > 0)
            {
                string ids = "";
                foreach (int row in selectedRows)
                {
                    int id = 0, intTemp = 0;
                    id = Int32.TryParse(gridView1.GetRowCellValue(row, "id").ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetRowCellValue(row, "id").ToString()) : intTemp;
                    ids += id + ",";
                }
                ids = ids.Length > 0 ? ids.Substring(0, ids.Length - 1) : ids;
                if (ids.Length > 0)
                {
                    Receive_ForSAPDetails frm = new Receive_ForSAPDetails(ids);
                    frm.hasSAP = hasSAP;
                    frm.ShowDialog();
                    if (Receive_ForSAPDetails.isSubmit)
                    {
                        Receive_ForSAPDetails.isSubmit = false;
                        bg(backgroundWorker1);
                    }
                }
                else
                {
                    apic.showCustomMsgBox("Validation", "No data selected");
                }
            }
            else
            {
                apic.showCustomMsgBox("Validation", "No data found");
            }
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            string selectedColumnText = gridView1.FocusedColumn.FieldName;
            int id = !Convert.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) ? Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id").ToString()) : 0;
            if (selectedColumnText.Equals("reference"))
            {
                if (id > 0)
                {
                    TransferItems frm = new TransferItems("Received Transactions For SAP");
                    frm.Text = "Received Items";
                    frm.selectedID = id;
                    frm.ShowDialog();
                    if (TransferItems.isSubmit)
                    {
                        bg(backgroundWorker1);
                        TransferItems.isSubmit = false;
                    }
                }
                else
                {
                    apic.showCustomMsgBox("Validation", "No id found!");
                }
            }
        }
    }


}
