using Newtonsoft.Json.Linq;
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
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Production_IssueProduction_Items : Form
    {
        public Production_IssueProduction_Items(string type)
        {
            gType = type;
            InitializeComponent();
        }
        string gType = "";
        utility_class utilityc = new utility_class();
        devexpress_class devc = new devexpress_class();
        api_class apic = new api_class();
        public int selectedID = 0;
        public string reference = "";
        public static bool isSubmit = false;
        private void Production_IssueProduction_Items_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            lblReference.Text = reference;
            btnCloseTransaction.Visible = gType.Equals("Open");
            loadData();
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
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    if (response.ErrorMessage == null)
                    {
                        if (!string.IsNullOrEmpty(response.Content.Trim()))
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
                            }else
                            {
                                MessageBox.Show(response.Content, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        public void loadData() {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            string sResult = apic.loadData("/api/production/issue_for_prod/details/", selectedID.ToString(), "", "", Method.GET, true);
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
                            col.Width = 100;
                            col.ColumnEdit = repositoryItemTextEdit1;
                            col.DisplayFormat.FormatType = fieldName.Equals("quantity") ? DevExpress.Utils.FormatType.Numeric : DevExpress.Utils.FormatType.Custom;
                            col.DisplayFormat.FormatString = fieldName.Equals("date_created") || fieldName.Equals("date_updated") ? "yyyy-MM-dd HH:mm:ss" : fieldName.Equals("quantity") ? "{0:#,0.000}" : "";
                            col.Visible = fieldName.Equals("item_code") || fieldName.Equals("quantity") || fieldName.Equals("whsecode") || fieldName.Equals("uom") ? true : false;


                            //fonts
                            FontFamily fontArial = new FontFamily("Arial");
                            col.AppearanceHeader.Font = new Font(fontArial, 11, FontStyle.Regular);
                            col.AppearanceCell.Font = new Font(fontArial, 10, FontStyle.Regular);

                            //fixed column
                            col.Fixed = fieldName.Equals("item_code") || fieldName.Equals("quantity") ? FixedStyle.Left : FixedStyle.None;
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

        private void btnCloseTransaction_Click(object sender, EventArgs e)
        {
            GR_Remarks.isSubmit = false;
            GR_Remarks frm = new GR_Remarks();
            frm.ShowDialog();
            if (GR_Remarks.isSubmit)
            {
                JObject joBody = new JObject();
                joBody.Add("gr_num", !string.IsNullOrEmpty(GR_Remarks.grNumber.Trim()) ? GR_Remarks.grNumber : (string)null);
                joBody.Add("remarks", GR_Remarks.remarks);
                apiPUT(joBody, "/api/production/issue_for_prod/manual_close/" + selectedID);
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
