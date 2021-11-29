using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AB.API_Class.Branch;
using AB.API_Class.Warehouse;
using AB.UI_Class;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using System.Collections;

namespace AB
{
    public partial class AddUser : Form
    {
        public static bool isSubmit = false;
        branch_class branchc = new branch_class();
        warehouse_class warehousec = new warehouse_class();
        utility_class utilityc = new utility_class();
        DataTable dtBranches = new DataTable();
        DataTable dtWarehouse = new DataTable();
        DataTable dtPlant = new DataTable();
        public int userID = 0;
        string cUsername = "", cFullName = "", cBranch = "", cWarehouse = "", cPlant = "", cAssignedDept = "";
        int iBranch = 1, iWarehouse = 1;
        public AddUser()
        {
            InitializeComponent();
        }
        

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Username is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            else if (txtFullName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Full Name is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
            }
            else if (txtPassword.Text.ToString().Trim() == "" && this.Text == "Add User")
            {
                MessageBox.Show("Password is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }
            else if (txtPassword.Text.ToString().Trim() != txtConfirmPassword.Text.ToString().Trim() && this.Text == "Add User")
            {
                MessageBox.Show("Password did not match", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
            }
            else if (!string.IsNullOrEmpty(txtPassword.Text.Trim()) && txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim() && this.Text == "Edit User")
            {
                MessageBox.Show("Password did not match", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.Text == "Add User")
                    {
                        
                        if ( insertUser())
                        {
                            isSubmit = true;
                            MessageBox.Show("User added", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //clearFields();
                            this.Dispose();
                        }
                        else
                        {
                            isSubmit = false;
                            MessageBox.Show("User not " + (this.Text == "Add User" ? "Added" : "Edited"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //clearFields();
                        }

                    }
                    else if (this.Text == "Edit User")
                    {
                        DataTable dtGroupA = returnDTGroupA();
                        List<string> groupA = returnGroupA();
                        List<string> groupB = returnGroupB();
                        var filteredDepts = groupA.Except(groupB);
                        JArray jaUnAssignedDeptResult = new JArray();
                        foreach (string dept in filteredDepts)
                        {
                           foreach(DataRow row in dtGroupA.Rows)
                            {
                                int id = 0, intTemp = 0;
                                id = row["id"] == null ? intTemp : Int32.TryParse(row["id"].ToString(), out intTemp) ? Convert.ToInt32(row["id"].ToString()) : intTemp;
                                string dep = row["department"] == null ? "" : row["department"].ToString();
                                if (dept.Trim().Equals(dep.Trim()))
                                {
                                    JObject joUnAssignedDeptResult = unAssignedDepts(id);
                                    jaUnAssignedDeptResult.Add(joUnAssignedDeptResult);
                                }
                            }
                        }
                        int isSuccessCount = 0;
                        string msg = "";
                        for(int i = 0; i < jaUnAssignedDeptResult.Count; i++)
                        {
                            JObject joUnAssignedDeptResult = JObject.Parse(jaUnAssignedDeptResult[i].ToString());
                            bool isSuccess = false, boolTemp = false;
                            isSuccess = joUnAssignedDeptResult["success"] == null ? boolTemp : bool.TryParse(joUnAssignedDeptResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joUnAssignedDeptResult["success"].ToString()) : boolTemp;
                            isSuccessCount++;
                            msg += joUnAssignedDeptResult["message"] == null ? "" : joUnAssignedDeptResult["message"].ToString() + ",";
                        }

                        if(isSuccessCount != filteredDepts.Count())
                        {
                            MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (updateUser())
                        {
                            isSubmit = true;
                            MessageBox.Show("User edited", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //clearFields();
                            this.Dispose();
                        }
                        else
                        {
                            isSubmit = false;
                            MessageBox.Show("User not " + (this.Text == "Add User" ? "Added" : "Edited"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //clearFields();
                        }
                    }
                    else
                    {
                        isSubmit = false;
                        MessageBox.Show("User not " + (this.Text == "Add User" ? "Added" : "Edited"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearFields();
                    }
                }
            }
        }

        public List<string> returnGroupA()
        {
            JArray jaDepts = new JArray();
            JArray jaCurrentDepts = new JArray();
            List<string> groupA = new List<string>();
            if (!string.IsNullOrEmpty(cAssignedDept.Trim()))
            {
                if (cAssignedDept.StartsWith("["))
                {
                    jaCurrentDepts = JArray.Parse(cAssignedDept);
                    for (int i = 0; i < jaCurrentDepts.Count; i++)
                    {
                        JObject joCurrentDepts = JObject.Parse(jaCurrentDepts[i].ToString());
                        string sCurrentDepts = joCurrentDepts["department"] == null ? "" : joCurrentDepts["department"].ToString();
                        groupA.Add(sCurrentDepts);
                    }
                }
            }
            return groupA;
        }

        public DataTable returnDTGroupA()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("id", typeof(int));
            dtResult.Columns.Add("department", typeof(string));
            JArray jaDepts = new JArray();
            JArray jaCurrentDepts = new JArray();
            if (!string.IsNullOrEmpty(cAssignedDept.Trim()))
            {
                if (cAssignedDept.StartsWith("["))
                {
                    jaCurrentDepts = JArray.Parse(cAssignedDept);
                    for (int i = 0; i < jaCurrentDepts.Count; i++)
                    {
                        JObject joCurrentDepts = JObject.Parse(jaCurrentDepts[i].ToString());
                        int id = 0, intTemp = 0;
                        id = joCurrentDepts["id"] == null ? intTemp : Int32.TryParse(joCurrentDepts["id"].ToString(), out intTemp) ? Convert.ToInt32(joCurrentDepts["id"].ToString()) : intTemp;
                        string sCurrentDepts = joCurrentDepts["department"] == null ? "" : joCurrentDepts["department"].ToString();
                        dtResult.Rows.Add(id, sCurrentDepts);
                    }
                }
            }
            return dtResult;
        }

        public List<string> returnGroupB()
        {
            List<string> groupB = new List<string>();
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {
                string dept = gridView2.GetRowCellValue(i, "department").ToString();
                groupB.Add(dept);
            }
            return groupB;
        }

        public void clearFields()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            //cmbBranch.SelectedIndex = -1;
            //cmbWarehouse.SelectedIndex = -1;
            //cmbAdmin.Checked = false;
            //cmbSales.Checked = false;
            //cmbAddSAP.Checked = false;
            //cmbReceive.Checked = false;
            //cmbCashier.Checked = false;
            //cmbManager.Checked = false;
            //cmbTransfer.Checked = false;
            //cmbVoid.Checked = false;
            //cmbARSales.Checked = false;
            //cmbAgentSales.Checked = false;
            //cmbCashSales.Checked = false;
            //cmbDiscount.Checked = false;
            //cmbChecker.Checked = false;
            //cmbAuditor.Checked = false;
            //cmbEndbal.Checked = false;
            //cmbPullOut.Checked = false;
        }

        public  JObject unAssignedDepts(int depID)
        {
            JObject joReturn = new JObject();
            api_class apic = new api_class();
            string sResult = apic.loadData("/api/user/unassign/department/", depID.ToString(), "", "", RestSharp.Method.DELETE, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = false, boolTemp = false;
                    isSuccess = joResult["success"] == null ? boolTemp : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                    string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
                    joReturn.Add("success", isSuccess);
                    joReturn.Add("message", msg);
                }
            }
            return joReturn;
        }

        public void getUserDetails()
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
                    //string branch = "A1-S";
                    var request = new RestRequest("/api/auth/user/details/" + userID);
                    request.AddHeader("Authorization", "Bearer " + token);
                    var response = client.Execute(request);
                    JObject jObject = new JObject();
                    jObject = JObject.Parse(response.Content.ToString());
                    //clearFields();
                    string userName = "",
fullName = "", branch = "", warehouse = "", plant = "";
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
                        int isSelectCount = 0;
                        foreach (var x in jObject)
                        {
                            if (x.Key.Equals("data"))
                            {
                                if (x.Value.ToString() != "{}")
                                {
                                    JObject jObjectData = JObject.Parse(x.Value.ToString());
                                    foreach (var y in jObjectData)
                                    {
                                        if (y.Key.Equals("username"))
                                        {
                                            userName = y.Value.ToString();
                                        }
                                        else if (y.Key.Equals("fullname"))
                                        {
                                            fullName = y.Value.ToString();
                                        }
                                        else if (y.Key.Equals("branch"))
                                        {
                                            branch = y.Value.ToString();
                                        }
                                        else if (y.Key.Equals("whse"))
                                        {
                                            warehouse = y.Value.ToString();
                                        }
                                        else if (y.Key.Equals("plant"))
                                        {
                                            plant = y.Value.ToString();
                                        }

                                        else if (y.Key.ToLower().Trim().Contains("is"))
                                        {
                                            bool isSelect = false, boolTemp = false;
                                            //if(bool.TryParse(y.Value.ToString(), out boolTemp))
                                            //{
                                            //    isSelect = Convert.ToBoolean(y.Value.ToString());
                                            //    isSelectCount += 1;
                                            //}
                                            //dgv.Rows.Add(isSelect, y.Key);
                                            isSelect = bool.TryParse(y.Value.ToString(), out boolTemp) ? Convert.ToBoolean(y.Value.ToString()) : boolTemp;
                                            for (int i = 0; i < gridView1.DataRowCount; i++)
                                            {
                                                if (gridView1.GetRowCellValue(i, "description").ToString() == y.Key && isSelect)
                                                {
                                                    gridView1.SelectRow(i);
                                                }
                                            }
                                        }
                                        else if (y.Key.Equals("assigned_dep"))
                                        {
                                            cAssignedDept = y.Value.ToString();
                                            if (y.Value.ToString().StartsWith("["))
                                            {
                                                DataTable dtAD = new DataTable();
                                                dtAD.Columns.Add("department");
                                                JArray jaAD = JArray.Parse(y.Value.ToString());
                                                for (int i = 0; i < jaAD.Count; i++)
                                                {
                                                    JObject joDept = JObject.Parse(jaAD[i].ToString());
                                                    string department = joDept["department"] == null ? "" : joDept["department"].ToString();
                                                    if (!string.IsNullOrEmpty(department.Trim()))
                                                    {
                                                        dtAD.Rows.Add(department);
                                                    }
                                                }
                                                dtAD.Columns.Add("btn_remove");
                                                loadAssignedDepartment(dtAD);
                                            }
                                        }
                                    }
                                }
           
                                txtUsername.Text = cUsername = userName;
                                txtFullName.Text = cFullName = fullName;
                                cBranch = branch;
                                 cWarehouse = warehouse;
                                cPlant = plant;
                                string selectedBranch = branch, selectedWarehouse = warehouse;

                                cmbBranch.Text = selectedBranch;
                                cmbWarehouse.Text = selectedWarehouse;
                                cmbPlant.Text = plant;
                              
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

        public bool updateUser()
        {
            bool result = false;

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
                    //string branch = (cmbBranch.Text.Equals("") || cmbBranch.Text == "All" ? "" : cmbBranch.Text);
                    var request = new RestRequest("/api/auth/user/update/" + userID);
                    request.AddHeader("Authorization", "Bearer " + token);
                    request.Method = Method.PUT;

                    JObject jObject = new JObject();

                    if (txtUsername.Text != cUsername && !string.IsNullOrEmpty(txtUsername.Text.Trim()))
                    {
                        jObject.Add("username", txtUsername.Text.Trim());
                    }
                    if (txtFullName.Text != cFullName && !string.IsNullOrEmpty(txtFullName.Text.Trim()))
                    {
                        jObject.Add("fullname", txtFullName.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                    {
                        jObject.Add("password", txtConfirmPassword.Text.Trim());
                    }
                    if (cmbBranch.Text != cBranch && cmbBranch.SelectedIndex != -1)
                    {
                        jObject.Add("branch", cmbBranch.Text);
                    }
                    if (cmbWarehouse.Text != cWarehouse && cmbWarehouse.SelectedIndex != -1)
                    {
                        jObject.Add("whse", cmbWarehouse.Text);
                    }
                    if (cmbPlant.Text != cPlant && cmbPlant.SelectedIndex != -1)
                    {
                        jObject.Add("plant", cmbPlant.Text);
                    }
                    //for (int i = 0; i < dgv.Rows.Count; i++)
                    //{
                    //    bool val = Convert.ToBoolean(dgv.Rows[i].Cells["selectt"].Value.ToString());
                    //    string description = dgv.Rows[i].Cells["description"].Value.ToString();
                    //    if (!val)
                    //    {
                    //        jObject.Add(description, null);
                    //    }
                    //    else
                    //    {
                    //        jObject.Add(description, val);
                    //    }
                    //}

                    Int32[] selectedRowHandles = gridView1.GetSelectedRows();
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        string description = gridView1.GetRowCellValue(i, "description").ToString();
                        bool isSelect = gridView1.IsRowSelected(i);
                        jObject.Add(description, isSelect);
                    }
                    JArray jaDepts = new JArray();
                    List<string> groupA = returnGroupA();
                    List<string> groupB = returnGroupB();
                    var filteredDepts = groupB.Except(groupA);
                    foreach(string dept in filteredDepts)
                    {
                        jaDepts.Add(dept);
                    }
                    jObject.Add("departments", jaDepts);
                    request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    jObject = JObject.Parse(response.Content.ToString());
                    foreach (var x in jObject)
                    {
                        if (x.Key.Equals("success"))
                        {
                            if (Convert.ToBoolean(x.Value.ToString()))
                            {
                                result = true;
                            }
                        }
                    }
                    if (!result)
                    {
                        {
                            string msg = "No message response found";
                            foreach (var x in jObject)
                            {
                                if (x.Key.Equals("message"))
                                {
                                    msg = x.Value.ToString();
                                }
                            }
                            if (msg.Equals("Token is invalid"))
                            {
                                MessageBox.Show("Your login session is expired. Please login again", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    Cursor.Current = Cursors.Default;
                }
            }

            return result;
        }

        public  bool insertUser()
        {
            api_class apic = new api_class();
            bool isSuccess = false, boolTemp = false;
            string username = txtUsername.Text.ToString().Trim(),
                fullname = txtFullName.Text.ToString().Trim(),
                password = txtConfirmPassword.Text.Trim(),
                branch = cmbBranch.Text.ToString(),
                warehouse = cmbWarehouse.Text.ToString(),
                plant = cmbPlant.Text.ToString();

            JObject jObject = new JObject();
            jObject.Add("username", (username == String.Empty ? null : username));
            jObject.Add("fullname", (fullname == String.Empty ? null : fullname));
            jObject.Add("password", (password == String.Empty ? null : password));
            if (!string.IsNullOrEmpty(branch.Trim()))
            {
                jObject.Add("branch",  branch);
            }
            if (!string.IsNullOrEmpty(warehouse.Trim()))
            {
                jObject.Add("whse", warehouse);
            }
            if (!string.IsNullOrEmpty(plant.Trim()))
            {
                jObject.Add("plant", plant);
            }

            //for (int i = 0; i < dgv.Rows.Count; i++)
            //{
            //    bool val = Convert.ToBoolean(dgv.Rows[i].Cells["selectt"].Value.ToString());
            //    string description = dgv.Rows[i].Cells["description"].Value.ToString();
            //    if (!val)
            //    {
            //        jObject.Add(description, null);
            //    }
            //    else
            //    {
            //        jObject.Add(description, val);
            //    }
            //}
            Int32[] selectedRowHandles = gridView1.GetSelectedRows();
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                string description = gridView1.GetRowCellValue(i, "description").ToString();
                bool isSelect = gridView1.IsRowSelected(i);
          
                jObject.Add(description, isSelect);
            }

            JArray jaDepts = new JArray();
            Int32[] selectedRowHandles2 = gridView2.GetSelectedRows();
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {
                string dept = gridView2.GetRowCellValue(i, "department").ToString();
                jaDepts.Add(dept);
            }
            jObject.Add("departments", jaDepts);
            Console.WriteLine(jObject);
            string sResult = apic.loadData("/api/auth/user/new", "", "application/json", jObject.ToString(), Method.POST, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    isSuccess = joResult["success"] == null ? false : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                }
            }
            return isSuccess;
        }

        private async void AddUser_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            loadPlant();
            loadBranches();
            loadWarehouse();
            loadCol();
            if (this.Text == "Edit User")
            {
                getUserDetails();
            }
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iBranch <= 0)
            {
                loadBranches();

            }else
            {
                iBranch=0;
            }
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            SelectDepartment.dtSelected = new DataTable();
            List<string> listSelected = new List<string>();
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {
                string description = gridView2.GetRowCellValue(i, "department").ToString();
                listSelected.Add(description);
            }
            SelectDepartment frm = new SelectDepartment(listSelected);
            frm.ShowDialog();
            frm.Focus();
            if (SelectDepartment.dtSelected.Rows.Count > 0)
            {
                loadAssignedDepartment(SelectDepartment.dtSelected);
            }
        }

        public void loadAssignedDepartment(DataTable dt)
        {
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = dt;
            gridView2.OptionsView.ColumnAutoWidth = false;
            foreach (GridColumn col in gridView2.Columns)
            {
                string fieldName = col.FieldName;

                string v = col.GetCaption();
                string s = col.GetCaption().Replace("_", " ");
                col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                col.ColumnEdit = fieldName.Equals("btn_remove") ? repositoryItemButtonEdit1 : repositoryItemTextEdit2;
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                col.DisplayFormat.FormatString = "";
            }
            gridView2.BestFitColumns();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    int currentRow = gridView2.FocusedRowHandle;
                    gridView2.DeleteRow(currentRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void loadWarehouse()
        {
            api_class apic = new api_class();
            string branchCode =  apic.findValueInDataTable(dtBranches, cmbBranch.Text, "name", "code");
            cmbWarehouse.Items.Clear();
            //cmbWarehouse.Items.Add("All");
            string sResult = apic.loadData("/api/whse/get_all", "?branch=" + branchCode, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    if (cmbWarehouse.Name.Equals("cmbFromWhse"))
                    {
                        dtWarehouse = dt;
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbWarehouse.Items.Add(row["whsename"].ToString());
                    }
                    //string plantCode = Login.jsonResult["data"]["whse"].ToString();
                    //string plantName = cmbWarehouse.Name.Equals("cmbFromWhse") ? "" : apic.findValueInDataTable(dt, plantCode, "whsecode", "whsename");
                    //cmbWarehouse.SelectedIndex = cmbWarehouse.Items.IndexOf(plantName) <= 0 ? 0 : cmbWarehouse.Items.IndexOf(plantName);
                }
            }
            else
            {
                //cmbWarehouse.SelectedIndex = 0;
            }
        }

        public void loadCol()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            api_class apic = new api_class();
            string sResult = apic.loadData("/api/auth/users/col", "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaCols = joResult["data"] == null ? new JArray() : (JArray)joResult["data"]["cols"];
                    DataTable dtData = new DataTable();
                    dtData.Columns.Add("description");
                    for(int i =0; i < jaCols.Count; i++)
                    {
                        string a = jaCols[i].ToString();
                        dtData.Rows.Add(a);
                    }
                    gridControl1.DataSource = dtData;
                    foreach (GridColumn col in gridView1.Columns)
                    {
                        string fieldName = col.FieldName;
         
                        string v = col.GetCaption();
                        string s = col.GetCaption().Replace("_", " ");
                        col.Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                        col.ColumnEdit = repositoryItemTextEdit1;
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                        col.DisplayFormat.FormatString = "";
                    }
                    gridView1.BestFitColumns();
                }
            }
        }

        public void loadBranches()
        {
            api_class apic = new api_class();
            string plantCode = apic.findValueInDataTable(dtPlant, cmbPlant.Text, "name", "code");
            cmbBranch.Items.Clear();
            //cmbBranch.Items.Add("All");
            string sResult = apic.loadData("/api/branch/get_all", "?plant=" + plantCode, "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtBranches = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));

                    foreach (DataRow row in dtBranches.Rows)
                    {
                        cmbBranch.Items.Add(row["name"].ToString());
                    }
                    //string currentBranchCode = Login.jsonResult["data"]["branch"].ToString();
                    //string currentBranchName = apic.findValueInDataTable(dtBranches, currentBranchCode, "code", "name");
                    //cmbBranch.SelectedIndex = cmbBranch.Items.IndexOf(currentBranchName) <= 0 ? 0 : cmbBranch.Items.IndexOf(currentBranchName);
                }
            }
            else
            {
                //cmbBranch.SelectedIndex = 0;
            }
        }

        public void loadPlant()
        {
            cmbPlant.Items.Clear();
            api_class apic = new api_class();
            string sResult = apic.loadData("/api/plant/get_all", "", "", "", Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JArray jaData = (JArray)joResult["data"];
                    dtPlant = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                    
                    foreach(DataRow row in dtPlant.Rows)
                    {
                        cmbPlant.Items.Add(row["code"].ToString());
                    }
                }
            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadWarehouse();
        }
    }
}
