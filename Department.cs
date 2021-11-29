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
using DevExpress.XtraGrid;
using System;
using System.ComponentModel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AB
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
            //gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
        }
        utility_class utilityc = new utility_class();
        private async void Branches2_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            refresh();
        }

        public async void refresh()
        {
            
            DataTable dt = await loadData();

            if (gridControl1.InvokeRequired)
            {
                gridControl1.Invoke(new MethodInvoker(delegate
                {
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = dt;
                    (gridControl1.MainView as GridView).Columns["ID"].OptionsColumn.ReadOnly = (gridControl1.MainView as GridView).Columns["Code"].OptionsColumn.ReadOnly = (gridControl1.MainView as GridView).Columns["Namee"].OptionsColumn.ReadOnly = true;

                    GridColumn myCol = new GridColumn() { Caption = "Action", Visible = true, FieldName = "Action" };
                    GridColumn myCol2 = new GridColumn() { Caption = "Action", Visible = true, FieldName = "Action2" };
                    gridView1.Columns.Add(myCol);
                    gridView1.Columns.Add(myCol2);
                    (gridControl1.MainView as GridView).Columns["Action"].ColumnEdit = repositoryItemButtonEdit1;
                    (gridControl1.MainView as GridView).Columns["Action2"].ColumnEdit = repositoryItemButtonEdit2;
                    gridView1.Columns["ID"].Visible = false;
                }));
            }
            else
            {
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                gridControl1.DataSource = dt;
                (gridControl1.MainView as GridView).Columns["ID"].OptionsColumn.ReadOnly = (gridControl1.MainView as GridView).Columns["Code"].OptionsColumn.ReadOnly = (gridControl1.MainView as GridView).Columns["Namee"].OptionsColumn.ReadOnly = true;

                GridColumn myCol = new GridColumn() { Caption = "Action", Visible = true, FieldName = "Action" };
                GridColumn myCol2 = new GridColumn() { Caption = "Action", Visible = true, FieldName = "Action2" };
                gridView1.Columns.Add(myCol);
                gridView1.Columns.Add(myCol2);
                (gridControl1.MainView as GridView).Columns["Action"].ColumnEdit = repositoryItemButtonEdit1;
                (gridControl1.MainView as GridView).Columns["Action2"].ColumnEdit = repositoryItemButtonEdit2;
                gridView1.Columns["ID"].Visible = false;
            }
        }

        public async Task <DataTable> loadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Code");
            dt.Columns.Add("Namee");
            if (Login.jsonResult != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
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
                    //string branch = "A1-S";
                    var request = new RestRequest("/api/branch/get_all");
                    request.AddHeader("Authorization", "Bearer " + token);
                    Task<IRestResponse> t = client.ExecuteAsync(request);
                    t.Wait();
                    var response = await t;
                    JObject jObject = new JObject();
                    jObject = JObject.Parse(response.Content.ToString());
                    bool isSuccess = false;
                    foreach (var x in jObject)
                    {
                        if (x.Key.Equals("success"))
                        {
                            isSuccess = Convert.ToBoolean(x.Value.ToString());
                        }
                    }
                    if (isSuccess)
                    {
                        foreach (var x in jObject)
                        {
                            if (x.Key.Equals("data"))
                            {
                                if (x.Value.ToString() != "[]")
                                {
                                    JArray jsonArray = JArray.Parse(x.Value.ToString());
                                    for (int i = 0; i < jsonArray.Count(); i++)
                                    {
                                        JObject data = JObject.Parse(jsonArray[i].ToString());
                                        int id = 0;
                                        string userName = "",
            fullName = "";
                                        foreach (var q in data)
                                        {
                                            if (q.Key.Equals("code"))
                                            {
                                                userName = q.Value.ToString();
                                                auto.Add(q.Value.ToString());
                                            }
                                            else if (q.Key.Equals("name"))
                                            {
                                                fullName = q.Value.ToString();
                                            }
                                            else if (q.Key.Equals("id"))
                                            {
                                                id = Convert.ToInt32(q.Value.ToString());
                                            }
                                        }

                                        dt.Rows.Add(id, userName, fullName);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string msg = "No message response found";
                        foreach (var x in jObject)
                        {
                            if (x.Key.Equals("message"))
                            {
                                msg = x.Value.ToString();
                            }
                        }
                        MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            return dt;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
       
        }

        private async void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            string code = gridView1.GetFocusedDataRow()["Code"].ToString(), name = gridView1.GetFocusedDataRow()["Namee"].ToString();
            int id = 0, intTemp = 0;
            id = Int32.TryParse(gridView1.GetFocusedDataRow()["ID"].ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedDataRow()["ID"].ToString()) : intTemp;
            EditBranch editBranch = new EditBranch();
            editBranch.selectedID = id;
            editBranch.selectedCode = code;
            editBranch.selectedName = name;
            editBranch.ShowDialog();
            if (EditBranch.isSubmit)
            {
                refresh();
            }
        }

        public async void deleteBranch(int id)
        {
            if (Login.jsonResult != null)
            {
                Cursor.Current = Cursors.WaitCursor;
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
                    var request = new RestRequest("/api/branch/delete/" + id);
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.DELETE;
                    Task<IRestResponse> t = client.ExecuteAsync(request);
                    t.Wait();
                    var response = await t;
                    JObject jObject = JObject.Parse(response.Content.ToString());
                    bool isSuccess = false;

                    string msg = "No message response found";
                    foreach (var x in jObject)
                    {
                        if (x.Key.Equals("message"))
                        {
                            msg = x.Value.ToString();
                        }
                    }

                    foreach (var x in jObject)
                    {
                        if (x.Key.Equals("success"))
                        {
                            isSuccess = Convert.ToBoolean(x.Value.ToString());
                            MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            refresh();
                        }
                    }

                    if (!isSuccess)
                    {
                        if (msg.Equals("Token is invalid"))
                        {
                            MessageBox.Show("Your login session is expired. Please login again", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            AddBranch frm = new AddBranch();
            frm.ShowDialog();
            if (AddBranch.isSubmit)
            {
                refresh();
            }
        }

        private async void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                int id = 0, intTemp = 0;
                id = Int32.TryParse(gridView1.GetFocusedDataRow()["ID"].ToString(), out intTemp) ? Convert.ToInt32(gridView1.GetFocusedDataRow()["ID"].ToString()) : intTemp;
                await Task.Run(() => deleteBranch(id));
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

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
