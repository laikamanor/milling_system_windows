using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
namespace AB
{
    public partial class bgw : Form
    {
        public bgw()
        {
            InitializeComponent();
        }
        int counter = 1;
        Form fff;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("username", typeof(string));
            dt.Columns.Add("password", typeof(string));
            SqlConnection con = new SqlConnection("Data Source=localhost;Network Library=DBMSSOCN;Initial Catalog=vehicledb;User ID=admin;Password=admin;");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT username,password FROM tblusers WHERE status=1", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dt.Rows.Add(rdr["username"].ToString(), rdr["password"].ToString());
            }
            con.Close();
            Thread.Sleep(4000);
            dataGridView1.Invoke(new Action(delegate ()
            {
                dataGridView1.DataSource = dt;
            }));
        }

        private void bgw_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //progressBar1.Value = 0;
            dataGridView1.Invoke(new Action(delegate ()
            {
                dataGridView1.SuspendLayout();

            }));
            panel1.Visible = false;
            panel1.Dock = DockStyle.None;
            //fff.Close();
        }


        public void showForm(Form form)
        {
            form.TopLevel = false;
            panel1.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                //showForm();
                panel1.Visible = true;  
                panel1.Dock = DockStyle.Fill;
                LoadingForm frm = new LoadingForm();
                showForm(frm);
                counter = 0;
                dataGridView1.DataSource = null;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void bgw_SizeChanged(object sender, EventArgs e)
        {
            //this.Refresh();
        }

        private void bgw_Resize(object sender, EventArgs e)
        {
            this.Refresh();
            this.Invalidate();
            //this.Refresh();
        }
    }
}
