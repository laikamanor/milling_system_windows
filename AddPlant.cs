using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AB
{
    public partial class AddPlant : Form
    {
        public AddPlant()
        {
            InitializeComponent();
        }

        private void AddPlant_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            TextBox[] txts = { txtCode, txtName };
            JObject joResult = validateTextboxes(txts);
            bool isSuccess = false, boolTemp = false;
            isSuccess = bool.TryParse((string)joResult["success"], out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
            if (isSuccess)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    insert(txts);
                }
            }
            else
            {
                MessageBox.Show(joResult["message"].ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void insert(TextBox[] txts)
        {
            JObject joBody = new JObject();
            foreach (TextBox txt in txts)
            {
                string key = txt.Name.ToString().Replace("txt", "").ToLower();
                joBody.Add(key, txt.Text);
            }
            UI_Class.api_class apic = new UI_Class.api_class();
            string sResult = apic.loadData("/api/plant/new", "", "application/json", joBody.ToString(), RestSharp.Method.POST, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.Substring(0, 1).Equals("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    bool isSuccess = false, boolTemp = false;
                    isSuccess = bool.TryParse((string)joResult["success"], out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                    MessageBox.Show(joResult["message"].ToString(), isSuccess ? "Message" : "Validation", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    if (isSuccess)
                    {
                        this.Hide();
                    }
                }
            }
        }

        public JObject validateTextboxes(TextBox[] txts)
        {
            JObject joResult = new JObject();
            try
            {
                int count = 0;
                string msg = "";
                foreach (TextBox txt in txts)
                {
                    if (string.IsNullOrEmpty(txt.Text.Trim()))
                    {
                        count += 1;
                        msg = txt.Name.ToString().Replace("txt", "").Replace("_", " ") + " field is required!";
                        break;
                    }
                }
                joResult.Add("success", count <= 0 ? true : false);
                joResult.Add("message", count > 0 ? msg : "");
            }
            catch (Exception ex)
            {
                joResult.Add("success", false);
                joResult.Add("message", ex.ToString());
            }
            return joResult;
        }
    }
}
