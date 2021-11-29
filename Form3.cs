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
using AB.API_Class;
namespace AB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private static async Task<string> login()
        {
            api_class apic = new api_class();

            string output = "";
            await Task.Run(() =>
            {
                output = apic.loadData("/api/auth/login?username=gord&password=qwe123","","","", RestSharp.Method.GET, false);
            });
            return output;
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

        private  void button1_Click(object sender, EventArgs e)
        {
            bg();
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            button1.Invoke(new Action(delegate ()
            {
                button1.Enabled = false;
            }));

            Console.WriteLine("hello " + await login());
            Console.WriteLine("hello2 " + await login());
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            closeForm();
            button1.Invoke(new Action(delegate ()
            {
                button1.Enabled = true;
            }));
        }
    }
}
