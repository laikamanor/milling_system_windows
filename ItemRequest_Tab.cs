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

namespace AB
{
    public partial class ItemRequest_Tab : Form
    {
     
        public ItemRequest_Tab()
        {
            InitializeComponent();
        }

        private void ItemRequest_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.logo2;
            //ItemRequest3 itemRequest = new ItemRequest3("O");
            ItemRequest2 itemRequest = new ItemRequest2("O");
            showForm(panelConfirmation, itemRequest);

        }


        public void showForm(Panel panel, Form form)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex.Equals(0))
            {
                ItemRequest2 itemRequest = new ItemRequest2("O");
                showForm(panelConfirmation, itemRequest);
            }
            else if (tabControl1.SelectedIndex.Equals(1))
            {
                ItemRequest2 itemRequest = new ItemRequest2("C");
                showForm(panelLogs, itemRequest);
            }
            else if (tabControl1.SelectedIndex.Equals(2))
            {
                ItemRequest2 itemRequest = new ItemRequest2("N");
                showForm(panelProduction, itemRequest);
            }
        }
    }
}
