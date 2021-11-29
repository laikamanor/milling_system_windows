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
using RestSharp;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Production_ReceivedProduction_Items : Form
    {
        public Production_ReceivedProduction_Items(string type)
        {
            gType = type;
            InitializeComponent();
        }
        string gType = "";
        public static bool isSubmit = false;
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        devexpress_class devc = new devexpress_class();
        public int selectedID = 0;
        public string reference = "";
        public bool isOffSpecsOpen = false;
        private void Production_ReceivedProduction_Items_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            //btnUpdateSAP.Visible = gType.Equals("For SAP") ? true : false;
            btnCloseTransaction.Visible = isOffSpecsOpen;
            lblReference.Text = reference;
            loadData();
        }

        public void loadData()
        {
            string sResult = apic.loadData("/api/production/rec_from_prod/details/", selectedID.ToString(), "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = (bool)joResult["success"];
                    if (isSuccess)
                    {
                        JArray jaData = (JArray)joResult["data"];
                        DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                        dt.SetColumnsOrder("item_code", "quantity", "uom", "whsecode");
                        gridControl1.DataSource = dt;

                        //auto complete
                        string[] suggestions = { "item_code", };
                        string suggestConcat = string.Join(";", suggestions);
                        gridView1.OptionsFind.FindFilterColumns = suggestConcat;
                        devc.loadSuggestion(gridView1, gridControl1, suggestions);

                        gridView1.OptionsView.ColumnAutoWidth = false;
                        gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                        foreach (GridColumn col in gridView1.Columns)
                        {
                            string fieldName = col.FieldName;
                            string v = col.GetCaption();
                            string s = col.GetCaption().Replace("_", " ");
                            col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.Custom;
                            col.DisplayFormat.FormatString = fieldName.Equals("date_created") || fieldName.Equals("date_updated") ? "yyyy-MM-dd HH:mm:ss" : fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                            col.Visible = fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("uom") ? true : false;

                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);
                        }
                        gridView1.BestFitColumns();
                    }
                    else
                    {
                        string msg = joResult["message"].ToString();
                        MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
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
                        if (response.Content.StartsWith("{"))
                        {
                            JObject jObjectResponse = JObject.Parse(response.Content);
                            foreach (var x in jObjectResponse)
                            {
                                if (x.Key.Equals("success"))
                                {
                                    isSubmit = string.IsNullOrEmpty(x.Value.ToString()) ? false : Convert.ToBoolean(x.Value.ToString());
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
                            MessageBox.Show(msg, "", MessageBoxButtons.OK, isSubmit ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                            if (isSubmit)
                            {
                                this.Dispose();
                            }
                        }
                        else
                        {
                            MessageBox.Show(response.Content, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        private void btnUpdateSAP_Click(object sender, EventArgs e)
        {
            //if (gType.Equals("For SAP"))
            //{
            //    SAP_Remarks.isSubmit = false;
            //    SAP_Remarks frm = new SAP_Remarks();
            //    frm.ShowDialog();
            //    if (SAP_Remarks.isSubmit)
            //    {
            //        JObject joBody = new JObject();
            //        joBody.Add("sap_number", SAP_Remarks.sap_number);
            //        joBody.Add("remarks", SAP_Remarks.rem);
            //        apiPUT(joBody, "/api/sap_num/receive_from_prod/update/" + selectedID);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            if (isOffSpecsOpen)
            {
                Remarks.isSubmit = false;
                Remarks frm = new Remarks();
                frm.ShowDialog();
                if (Remarks.isSubmit)
                {
                    JObject joBody = new JObject();
                    joBody.Add("remarks", Remarks.rem);
                    apiPUT(joBody, "/api/production/rec_from_prod/manual_close/" + selectedID);
                }
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
    }
}
