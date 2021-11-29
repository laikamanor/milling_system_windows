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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace AB
{
    public partial class AdjustmentIn_Details : Form
    {
        utility_class utilityc = new utility_class();
        public int selectedID = 0;
        public static bool isSubmit=false;
        string gAdjTrans = "", gAdjType = "", gRemarks = "";
        public AdjustmentIn_Details(string adjTrans, string adjType, string remarks)
        {
            gAdjType = adjType;
            gAdjTrans = adjTrans;
            gRemarks = remarks;
            InitializeComponent();
        }
        private void AdjustmentIn_Details_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            btnUpdateSAP.Visible = gAdjTrans.Equals("For SAP") ? true : false;
            loadData();
        }

        public void loadData()
        {
            try
            {
                api_class apic = new api_class();
                string sResult = apic.loadData("/api/inv_adj/" + gAdjType + "/details/", selectedID.ToString(), "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult) && sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResponse = JObject.Parse(sResult);
                    Console.WriteLine(joResponse);
                    JArray jaFinal = new JArray();

                    if (this.Text == "Adjustment Out Details")
                    {
                        jaFinal = (JArray)joResponse["data"];
                    }
                    else
                    {
                        JObject joData = (JObject)joResponse["data"];
                        jaFinal = (JArray)joData["rows"];
                    }

                    DataTable dtData = (DataTable)JsonConvert.DeserializeObject(jaFinal.ToString(), (typeof(DataTable)));

                    if (this.Text == "Adjusment In Details")
                    {
                        lblRemarks.Text = joResponse["data"]["remarks"].IsNullOrEmpty() ? "" : joResponse["data"]["remarks"].ToString();

                        lblReference.Text = joResponse["data"]["reference"].ToString();
                    }
                    else
                    {
                        lblRemarks.Text = gRemarks;
  
                    }


                    dtData.SetColumnsOrder("item_code", "quantity", "uom", "whsecode");
                    if (IsHandleCreated)
                    {
                        gridControl1.Invoke(new Action(delegate ()
                        {
                            gridControl1.DataSource = null;
                            gridControl1.DataSource = dtData;
                            gridView1.OptionsView.ColumnAutoWidth = false;
                            gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                            foreach (GridColumn col in gridView1.Columns)
                            {
                                string fieldName = col.FieldName;
                                string v = col.GetCaption();
                                string s = col.GetCaption().Replace("_", " ");
                                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                                col.ColumnEdit = repositoryItemTextEdit1;
                                col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.None;
                                col.DisplayFormat.FormatString = fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                                col.Visible = fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("uom") || fieldName.Equals("whsecode");
                                //fonts
                                FontFamily fontArial = new FontFamily("Arial");
                                col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                                col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                            }
                            gridView1.BestFitColumns();
                            //auto complete
                            devexpress_class devc = new devexpress_class();
                            string[] suggestions = { "item_code" };
                            string suggestConcat = string.Join(";", suggestions);
                            gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                            devc.loadSuggestion(gridView1, gridControl1, suggestions);
                            gridView1.BestFitColumns();
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private int hotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        private int HotTrackRow
        {
            get
            {
                return hotTrackRow;
            }
            set
            {
                if (hotTrackRow != value)
                {
                    int prevHotTrackRow = hotTrackRow;
                    hotTrackRow = value;
                    gridView1.RefreshRow(prevHotTrackRow);
                    gridView1.RefreshRow(hotTrackRow);

                    if (hotTrackRow >= 0)
                        gridControl1.Cursor = Cursors.Hand;
                    else
                        gridControl1.Cursor = Cursors.Default;
                }
            }
        }
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = gridView1.PaintAppearance.SelectedRow.BackColor;
            else
                e.Appearance.BackColor = e.Appearance.BackColor;
        }

        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));

            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void btnUpdateSAP_Click(object sender, EventArgs e)
        {
            if (this.Text == "Adjusment In Details" || this.Text== "Adjustment Out Details")
            {
                if (gAdjTrans.Equals("For SAP"))
                {
                    SAP_Remarks sapRemarks = new SAP_Remarks();
                    sapRemarks.isOptional = false;
                    sapRemarks.ShowDialog();
                    int sapNumber = SAP_Remarks.sap_number;
                    string remarks = SAP_Remarks.rem;
                    if (SAP_Remarks.isSubmit)
                    {
                        JObject jObjectBody = new JObject();
                        JArray jArrayID = new JArray();
                        jArrayID.Add(selectedID);
                        jObjectBody.Add("ids", jArrayID);
                        if(sapNumber <= 0)
                        {
                            jObjectBody.Add("sap_number", null);
                        }
                        else
                        {
                            jObjectBody.Add("sap_number", sapNumber);
                        }
                        if (string.IsNullOrEmpty(remarks.Trim()))
                        {
                            jObjectBody.Add("remarks", null);
                        }
                        else
                        {
                            jObjectBody.Add("remarks", remarks);
                        }
                        Console.WriteLine(jObjectBody);
                        
                        apiPUT(jObjectBody, "/api/sap_num/adj_" + gAdjType +  "/update");
                        if (isSubmit)
                        {
                            this.Dispose();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void apiPUT(JObject body, string URL)
        {
            if (Login.jsonResult != null)
            {
                string token = "";
                foreach (var x in Login.jsonResult)
                {
                    if (x.Key.Equals("token"))
                    {
                        token = x.Value.ToString();
                    }
                }
                if (!token.Equals(""))
                {
                    var client = new RestClient(utilityc.URL);
                    client.Timeout = -1;
                    var request = new RestRequest(URL);
                    Console.WriteLine(URL);
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.PUT;

                    Console.WriteLine(body);
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    if (response.ErrorMessage == null)
                    {
                        if (response.Content.ToString().Substring(0, 1).Equals("{"))
                        {
                            JObject jObjectResponse = JObject.Parse(response.Content);

                            foreach (var x in jObjectResponse)
                            {
                                if (x.Key.Equals("success"))
                                {
                                    isSubmit = true;
                                    break;
                                }
                            }

                            string msg = "No message response found";
                            foreach (var x in jObjectResponse)
                            {
                                if (x.Key.Equals("message"))
                                {
                                    msg = x.Value.ToString();
                                }
                            }
                            MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(response.Content.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }


    }
}
