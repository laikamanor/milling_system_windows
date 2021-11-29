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
    public partial class Remarkss : Form
    {
        public Remarkss(string url)
        {
            gURL = url;
            InitializeComponent();
        }
        api_class apic = new api_class();
        public int selectedID = 0;
        string gURL = "";
        private void Remarkss_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
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
            string sResult = apic.loadData(gURL, selectedID.ToString(), "", "", RestSharp.Method.GET, true);
            if (!string.IsNullOrEmpty(sResult.Trim()))
            {
                if (sResult.StartsWith("{"))
                {
                    JObject joResult = JObject.Parse(sResult);
                    JObject joData = (JObject)joResult["data"];
                    txtRemarks.Invoke(new Action(delegate ()
                    {
                        txtRemarks.Text = joData["remarks"].ToString();
                        string replaceT = joData["transdate"] == null ? joData["date_created"].ToString().Replace("T","") : joData["transdate"].ToString().Replace("T", " ");
                        DateTime dtTemp = new DateTime();
                        DateTime dt = DateTime.TryParse(replaceT, out dtTemp) ? Convert.ToDateTime(replaceT) : dtTemp;
                        lblTransDate.Text = dt == DateTime.MinValue ? "Trans. Date: " : "Trans. Date: " + dt.ToString("yyyy-MM-dd HH:mm");
                    }));

                }
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
    }
}
