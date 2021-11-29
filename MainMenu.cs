using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AB.UI_Class;
using Newtonsoft.Json.Linq;
using Tulpep.NotificationWindow;
using System.Data.SqlClient;
using AB.API_Class.Notification;
using System.Threading.Tasks;
using RestSharp;
using System.Threading;
using System.Media;
using System.IO;
using AB.API_Class.Counts;
using System.Xml;
using System.Runtime.InteropServices;
using DevExpress.Internal.WinApi;
using DevExpress.XtraBars.ToastNotifications;
using AB.API_Class;
using System.Text.RegularExpressions;

namespace AB
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        bool isProductionAddress = false;
        PopupNotifier gPop;
        bool draggable = false;
        int mouseX = 0, mouseY = 0;
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;
        static Panel pnn = new Panel();
        private void MainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                int width = Screen.PrimaryScreen.Bounds.Width;

                this.Width = width / 2;
                this.WindowState = FormWindowState.Maximized;

                this.Icon = Properties.Resources.logo2;
                pnn = panelchildform;
                displayTitle();

                isProductionAddress = utilityc.URL.Contains(utilityc.getTextfromGithub(utilityc.abWindowsProdURLFile));
                bg(backgroundWorker1);
                bg(backgroundWorker2);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
            //await NotifcationAsync();
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == 0x84)
                {  // Trap WM_NCHITTEST
                    Point pos = new Point(m.LParam.ToInt32());
                    pos = this.PointToClient(pos);
                    if (pos.Y < cCaption)
                    {
                        m.Result = (IntPtr)2;  // HTCAPTION
                        return;
                    }
                    if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                    {
                        m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                        return;
                    }
                }
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void displayTitle()
        {
            try
            {
                JObject joLogin = Login.jsonResult;
                string currentBranch = joLogin["data"]["branch"] == null ? "NO_BRANCH_FOUND" : joLogin["data"]["branch"].ToString();
                this.Text = utilityc.appName + " App, You logged in as " + Login.fullName + ", Version " + utilityc.versionName + ", Connected to " + utilityc.URL.Replace("http://", "");
                label4.Text = currentBranch + " - " + Login.selectedShift;
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void bg(BackgroundWorker bgw)
        {
            try
            {
                if (!bgw.IsBusy)
                {
                    bgw.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public bool isAdmin()
        {

            bool result = true;
            //if (Login.jsonResult != null)
            //{
            //    foreach (var x in Login.jsonResult)
            //    {
            //        if (x.Key.Equals("data"))
            //        {
            //            JObject jObjectData = JObject.Parse(x.Value.ToString());
            //            foreach (var y in jObjectData)
            //            {
            //                if (y.Key.Equals("isAdmin"))
            //                {
            //                    if (y.Value.ToString().ToLower() == "true")
            //                    {
            //                        result = true;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return result;
        }

        public static void showForm(Form form)
        {
            form.TopLevel = false;
            pnn.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void pendingOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PendingOrder pendingOrder = new PendingOrder();
                showForm(pendingOrder);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Login.jsonResult = null;
                    this.Dispose();
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    Users_DX users = new Users_DX();
                    showForm(users);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (gPop != null)
                    {
                        gPop.Hide();
                    }
                    Login.jsonResult = null;
                    this.Dispose();
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void advancePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdvancePayment advancePayment = new AdvancePayment();
                showForm(advancePayment);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void inventoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Inventory2 inventory = new Inventory2();
                showForm(inventory);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void itemRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemRequest_Tab itemRequest = new ItemRequest_Tab();
                showForm(itemRequest);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void transferTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Transfer transfer = new Transfer();
                //transfer.Text = "Transfer Transactions";
                //showForm(transfer);
                TransferItem_Tab frm = new TransferItem_Tab();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void cashTransactionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashTransactionReport cashTransactionReport = new CashTransactionReport();
            showForm(cashTransactionReport);
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesReport salesReport = new SalesReport();
            showForm(salesReport);
        }

        private void receivedTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiveItem_Tab frm = new ReceiveItem_Tab();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void branchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    Department branch = new Department();
                    showForm(branch);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    Customers customers = new Customers();
                    showForm(customers);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void inventoryCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                printableReports("Final Count Report");
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public void printableReports(string reportType)
        {
            try
            {
                EnterDate enterDate = new EnterDate(reportType);
                enterDate.ShowDialog();
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void inventorySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                printableReports("Final Report");
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void salesTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesTransactions salesTransactions = new SalesTransactions();
            showForm(salesTransactions);
        }

        private void pulloutTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transfer transfer = new Transfer();
            transfer.Text = "Pullout Transactions";
            showForm(transfer);
        }

        private void adjustmentInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AdjustmentIn adjustmentIn = new AdjustmentIn("in");
                showForm(adjustmentIn);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void adjustmentOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdjustmentIn adjustmentIn = new AdjustmentIn("out");
                showForm(adjustmentIn);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void objectTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    ObjectType adjustmentIn = new ObjectType();
                    showForm(adjustmentIn);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void warehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    Warehouse warehouse = new Warehouse();
                    showForm(warehouse);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    Items_DX items = new Items_DX();
                    showForm(items);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void cashVarianceTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashVariance items = new CashVariance();
            showForm(items);
        }

        private void itemSalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemSalesReport items = new ItemSalesReport();
            showForm(items);
        }

        private void seriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    Series items = new Series();
                    showForm(items);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void priceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    PriceList items = new PriceList();
                    showForm(items);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void salesReportsWoInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printableReports("Final Sales Report");
        }

        private void notifications0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NotificationBar items = new NotificationBar();
            //items.Location = new Point(Cursor.Position.X - 50, Cursor.Position.Y + 20);
            //items.ShowDialog();
            //int notifBarSelectedID = NotificationBar.selectedID;
            //if (notifBarSelectedID > 0)
            //{
            //    Notification nf = new Notification();
            //    nf.selectedID = notifBarSelectedID;
            //    showForm(nf);
            //}
            NotificationTab nf = new NotificationTab();
            showForm(nf);
        }

        private void addActualCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddActualCash add = new AddActualCash();
            add.ShowDialog();
        }

        private void pOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isProductionAddress)
            {
                POS items = new POS();
                showForm(items);
            }
            else
            {
                MessageBox.Show("Under Development", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void uomGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    UOMGroup items = new UOMGroup();
                    showForm(items);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void uomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    UOM items = new UOM();
                    showForm(items);
                }
                else
                {
                    MessageBox.Show("Access Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void productionOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (isProductionAddress)
            //{
            //    Production_ProductionOrder items = new Production_ProductionOrder();
            //    showForm(items);
            //}
            //else
            //{
            //    MessageBox.Show("Under Development", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            try
            {
                ProductionOrder_Tab items = new ProductionOrder_Tab();
                showForm(items);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void receiptFromProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (isProductionAddress)
            //{
            //    ReceiptFromProduction items = new ReceiptFromProduction();
            //    showForm(items);
            //}
            //else
            //{
            //    MessageBox.Show("Under Development", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            try
            {
                ReceiptFromProduction_Tab items = new ReceiptFromProduction_Tab();
                showForm(items);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void gLAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdmin())
                {
                    GLAccounts items = new GLAccounts();
                    showForm(items);
                }
                else
                {
                    MessageBox.Show("Accedd Denied", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        public async Task NotifcationAsync()
        {
            //JObject joLogin = Login.jsonResult;
            //string selectedBranch = "", selectedWarehouse = "";
            //if (joLogin != null)
            //{
            //    foreach (var q in joLogin)
            //    {
            //        if (q.Key.Equals("data"))
            //        {
            //            JObject joData = JObject.Parse(q.Value.ToString());
            //            foreach (var x in joData)
            //            {
            //                if (x.Key.Equals("branch"))
            //                {
            //                    selectedBranch = x.Value.ToString();
            //                }
            //                else if (x.Key.Equals("whse"))
            //                {
            //                    selectedWarehouse = x.Value.ToString();
            //                }
            //            }
            //        }
            //    }
            //}
            try
            {
                notification_class notifc = new notification_class();
                //string sFromDate = "&from_date=",
                //    sToDate = "&to_date=" + DateTime.Now.ToString("yyyy-MM-dd"),
                //    sWarehouse = "&whsecode=" + selectedWarehouse;
                //Console.WriteLine(sFromDate + Environment.NewLine + sToDate + Environment.NewLine + sWarehouse + selectedBranch);
                DataTable dt = await Task.Run(() => notifc.getUnreadNotif("", "", "", "", 0));
                loadNotification(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async void loadNotification(DataTable dtUnread)
        {
            try
            {
                notification_class notifc = new notification_class();
                //dtUnread = await notifc.getUnreadNotif();
                int count = 0;
                if (dtUnread.Rows.Count > 0)
                {
                    DataRow row = dtUnread.Rows[0];
                    count = string.IsNullOrEmpty(row["count"].ToString()) ? 0 : Convert.ToInt32(row["count"].ToString());
                }
                notifications0ToolStripMenuItem.Text = "Notification (" + count.ToString("N0") + ")";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void salesAmountSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SASR0 frm = new SASR0();
            showForm(frm);
        }

        private void trucksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Trucks frm = new Trucks();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }


        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                changePassword frm = new changePassword();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void plantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Plant frm = new Plant();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void wheatUsageReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WheatUsageReport frm = new WheatUsageReport();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void wheatTypeReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WheatTypeReport frm = new WheatTypeReport();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await TemperCount();
        }

        public async Task TemperCount()
        {
            try
            {
                var client = new RestClient(utilityc.URL);
                client.Timeout = utilityc.apiTimeOut;
                var request = new RestRequest("/api/inv/trfr/tempering_due/count");
                //Console.WriteLine("/api/notification/get_all_unread?branch=" + selectedBranch + selectedFromDate + selectedToDate + selectedWarehouse);
                request.AddHeader("Authorization", "Bearer " + apic.loadToken());
                var t = client.ExecuteTaskAsync(request).ConfigureAwait(false);
                var response = await t;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (response.Content.StartsWith("{"))
                        {
                            bool boolTemp = false;
                            int intTemp = 0, dataCount = 0;
                            JObject joResult = JObject.Parse(response.Content);
                            bool isSuccess = joResult["success"] == null ? false : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                            string msg = joResult["message"].ToString();
                            if (isSuccess)
                            {
                                dataCount = joResult["data"] == null ? 0 : int.TryParse(joResult["data"].ToString(), out intTemp) ? Convert.ToInt32(joResult["data"].ToString()) : intTemp;
                                if (dataCount > 0)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (gPop != null)
                                        {
                                            gPop.Hide();
                                            gPop = null;
                                        }
                                        gPop = new PopupNotifier();
                                        gPop.TitleText = "Temper Monitoring Notification";
                                        gPop.TitleFont = new Font("Arial", 13, FontStyle.Bold);
                                        gPop.ContentFont = new Font("Arial", 11, FontStyle.Regular);
                                        //pop.HeaderColor = Color.Red;
                                        //pop.HeaderHeight = 200;
                                        gPop.ContentText = "You have " + dataCount.ToString("N0") + " Over Due transaction!";
                                        gPop.Click += new EventHandler(pop_Click);
                                        gPop.Delay = 86400000;
                                        gPop.Popup();
                                    });
                                }
                            }
                            else
                            {
                                Console.WriteLine("error2: " + msg);
                            }
                        }
                        else
                        {
                            Console.WriteLine("error2: " + response.Content);
                        }
                    }
                }
                else
                {
                    if (response.ErrorMessage == null)
                    {
                        if (!string.IsNullOrEmpty(response.Content))
                        {
                            if (response.Content.StartsWith("{"))
                            {
                                JObject joResult = JObject.Parse(response.Content);
                                string msg = joResult["message"].ToString();
                            }
                            else
                            {
                                Console.WriteLine("error3: " + response.Content);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("error4: " + response.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void pop_Click(object sender, EventArgs e)
        {
            try
            {
                PopupNotifier pop = (PopupNotifier)sender;
                pop.Hide();
                gPop = null;
                TemperMonitoringTab frm = new TemperMonitoringTab(0, "panelForDisposition", "");
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void temperMonitoringToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                TemperMonitoringTab frm = new TemperMonitoringTab(0, "panelForDisposition", "");
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void MainMenu_Shown(object sender, EventArgs e)
        {

        }



        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Company frm = new Company();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void driverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Driver frm = new Driver();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void vesselToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Vessel frm = new Vessel();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                bg(backgroundWorker1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                count_class countc = new count_class();
                menuStrip1.Invoke(new Action(async delegate ()
                {
                    int CWcount = countc.loadIssueCount("CLEAN WHEAT");
                    int feedBCount = countc.loadIssueCount("FEEDBACK");
                    int FBCount = countc.loadIssueCount("FLOUR BINS");
                    int FPBcount = countc.loadIssueCount("FLOUR PACKING BINS");
                    int BPBcount = countc.loadIssueCount("BRAN/POLLARD PACKING BINS");
                    int total = CWcount + FPBcount + BPBcount + feedBCount + FBCount;
                    int packingBinCount = FPBcount + BPBcount;
                    Console.WriteLine(CWcount + "/" + FPBcount + "/" + BPBcount);
                    int binCount = CWcount + feedBCount + FBCount;
                    asdToolStripMenuItem.Image = binCount <= 0 ? null : Properties.Resources.warning;
                    issueForPackingToolStripMenuItem.Image = packingBinCount <= 0 ? null : Properties.Resources.warning;
                    productionToolStriapMenuItem1.Image = total <= 0 ? null : Properties.Resources.warning;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                bg(backgroundWorker2);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private async void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                count_class countc = new count_class();
                menuStrip1.Invoke(new Action(async delegate ()
                {
                    int FBcount = await countc.loadBinsCount("", "");
                    int FPBcount = countc.loadPackingBinsCount("", "");
                    int totalCount = FBcount + FPBcount;
                    flourBranBinsToolStripMenuItem.Image = FBcount <= 0 ? null : Properties.Resources.warning;
                    flourBranPackingBinsToolStripMenuItem.Image = FPBcount <= 0 ? null : Properties.Resources.warning;
                    qADispositionToolStripMenuItem.Image = totalCount <= 0 ? null : Properties.Resources.warning;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void flourBranBinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FlourBrandPollardBins_Tab items = new FlourBrandPollardBins_Tab();
                showForm(items);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void flourBranPackingBinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FlourBranPollardPackingBins_Tab frm = new AB.FlourBranPollardPackingBins_Tab();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void toastNotificationsManager1_Activated(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {

        }



        private void toastNotificationsManager1_UpdateToastContent(object sender, DevExpress.XtraBars.ToastNotifications.UpdateToastContentEventArgs e)
        {
            //XmlDocument content = e.ToastContent;
            //XmlElement toastElement = content.GetElementsByTagName("toast").OfType<XmlElement>().FirstOrDefault();
            //XmlElement actions = content.CreateElement("actions");
            //toastElement.AppendChild(actions);
            //XmlElement action = content.CreateElement("action");
            //actions.AppendChild(action);
            //action.SetAttribute("content", "Details");
            //action.SetAttribute("arguments", "details");
            //action = content.CreateElement("action");
            //actions.AppendChild(action);
            //action.SetAttribute("content", "Remind");
            //action.SetAttribute("arguments", "remind");
        }

        private void toastNotificationsManager1_TimedOut(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {

        }

        private void systemReceiveItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void systemTransferItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                SystemTransferItem.jaSelected = new JArray();
                SystemTransferItem frm = new SystemTransferItem();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void changeDepartmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string assignDep = "", userName = "";
                userName = Login.jsonResult["data"]["username"] == null ? "" : Login.jsonResult["data"]["username"].ToString();
                JArray assignDept = Login.jsonResult["data"]["assigned_dep"] == null ? new JArray() : Login.jsonResult["data"]["assigned_dep"].ToString().StartsWith("[") ? (JArray)Login.jsonResult["data"]["assigned_dep"] : new JArray();
                userName = userName;
                ViewAssignedDept.isSubmit = false;
                ViewAssignedDept.selectedDepartment = "";
                ViewAssignedDept frm = new ViewAssignedDept(assignDept.ToString(), userName, true);
                frm.ShowDialog();
                if (ViewAssignedDept.isSubmit)
                {
                    JObject joBody = new JObject();
                    joBody.Add("department", ViewAssignedDept.selectedDepartment);
                    string sResult = apic.loadData("/api/auth/curr_user/update/department", "", "application/json", joBody.ToString(), Method.PUT, true);
                    if (!string.IsNullOrEmpty(sResult.Trim()))
                    {
                        if (sResult.StartsWith("{"))
                        {
                            JObject joResult = JObject.Parse(sResult);
                            bool isSuccess = false, boolTemp = false;
                            isSuccess = joResult["success"] == null ? boolTemp : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                            string msg = joResult["message"] == null ? "" : joResult["message"].ToString();
                            if (isSuccess)
                            {
                                string newDepartment = joResult["data"]["branch"] == null ? "" : joResult["data"]["branch"].ToString();
                                Login.jsonResult["data"]["branch"] = newDepartment;
                                displayTitle();
                                apic.showCustomMsgBox(isSuccess ? "Message" : "Validation", msg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        //private void issueForProductionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IssueForProduction_Tab frm = new IssueForProduction_Tab();
        //    showForm(frm);
        //}

        //private void issueForPackingToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IssueForPacking_Tab frm = new IssueForPacking_Tab();
        //    showForm(frm);
        //}

        private void issueForPackingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                issue_class issuec = new issue_class();
                if (issuec.checkIssueDepartments(issuec.packingLists))
                {
                    //IssueForProdPacking.jaSelected = new JArray();
                    //IssueForProdPacking frm = new IssueForProdPacking("Issue For Packing",0);
                    //frm.Text = "Issue For Packing";
                    //showForm(frm);
                    ForIssueForPacking_Tab frm = new ForIssueForPacking_Tab();
                    showForm(frm);
                }
                else
                {
                    apic.showCustomMsgBox("Validation", issuec.checkIssueDepartmentsMsgVal(issuec.packingLists, "Production"));
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                draggable = true;
                mouseX = Cursor.Position.X - this.Left;
                mouseY = Cursor.Position.Y - this.Top;
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (draggable)
                {
                    this.Top = Cursor.Position.Y - mouseY;
                    this.Left = Cursor.Position.X - mouseX;
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                draggable = false;
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Login.jsonResult = null;
                    this.Dispose();
                    Login login = new Login();
                    login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void panelchildform_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void label2_MouseEnter(object sender, EventArgs e)
        //{
        //    label2.BackColor = Color.Red;
        //    label2.ForeColor = Color.White;
        //}

        //private void label2_MouseLeave(object sender, EventArgs e)
        //{
        //    label2.BackColor = Color.White;
        //    label2.ForeColor = Color.Black;
        //}

        //private void label3_MouseEnter(object sender, EventArgs e)
        //{
        //    label3.BackColor = Color.FromArgb(237, 237, 237);
        //}

        //private void label3_MouseLeave(object sender, EventArgs e)
        //{
        //    label3.BackColor = Color.White;
        //}

        private void panelMaximize_Click(object sender, EventArgs e)
        {
            try
            {
                panelchildform.Invalidate();
                panelchildform.Refresh();
                int width = Screen.PrimaryScreen.Bounds.Width;

                this.Width = width - 400;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                this.StartPosition = FormStartPosition.CenterScreen;
                foreach (Control c in panelchildform.Controls)
                {
                    if (c is Form)
                    {
                        Form f = (Form)c;
                        f.Invalidate();
                        f.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        //private void panelMaximize_MouseEnter(object sender, EventArgs e)
        //{
        //    panelMaximize.BackColor = Color.FromArgb(237, 237, 237);
        //}

        //private void panelMaximize_MouseLeave(object sender, EventArgs e)
        //{
        //    panelMaximize.BackColor = Color.White;
        //}

        private void panelchildform_Resize(object sender, EventArgs e)
        {
        }

        private void panelMaximize_Paint(object sender, PaintEventArgs e)
        {

        }

        private void productionToolStriapMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void inventoryPerWhseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryPerWhse frm = new AB.InventoryPerWhse();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void manualReceiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                manualReceive.jaSelected = new JArray();
                manualReceive.isSubmit = false;
                manualReceive frm = new AB.manualReceive();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void changeShiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                showWarehouse.isSubmit = false;
                showWarehouse.selectedWhse = "";
                showWarehouse frm = new showWarehouse("", "/api/production/shift/get_all", "", "description", "code", false, true);
                frm.Text = "Select Shift";
                frm.ShowDialog();
                if (showWarehouse.isSubmit)
                {
                    Login.selectedShift = showWarehouse.selectedWhse;
                    displayTitle();
                    apic.showCustomMsgBox("Message", "Successfully update");
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void MainMenu_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void summaryOfAvailableBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SummaryAvailableBalances frm = new SummaryAvailableBalances();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void systemReceiveItemToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SystemReceive.jaSelected = new JArray();
                SystemReceive frm = new SystemReceive();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void mobileAppTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemRequestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                MItemRequest.isSubmit = false;
                MItemRequest.jaSelected = new JArray();
                MItemRequest frm = new MItemRequest();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //forProd frm = new forProd();
                //showForm(frm);
                IssuedForProduction_Tab2 frm = new IssuedForProduction_Tab2();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        //private void issuedForPackingToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IssuedForPacking_Tab frm = new IssuedForPacking_Tab();
        //    showForm(frm);
        //}

        private void issueForProductionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IssuedForProduction_Tab2 frm = new IssuedForProduction_Tab2();
                showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void issueForPackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IssuedForPacking_Tab2 frm = new AB.IssuedForPacking_Tab2();
                showForm(frm);
                //IssueForPacking frm = new IssueForPacking("O","FLOUR PACKING BINS");
                //showForm(frm);
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }

        private void productionOrderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void weatBinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemperMonitoring frm = new TemperMonitoring("");
            showForm(frm);
        }


        //private void issueForPackingToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IssueForPacking_Tab frm = new IssueForPacking_Tab();
        //    showForm(frm);
        //}

        //private void issueForProductionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    IssueForProduction_Tab frm = new IssueForProduction_Tab();
        //    showForm(frm);
        //}

        private void issueForProductionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                issue_class issuec = new issue_class();
                if (issuec.checkIssueDepartments(issuec.prodLists))
                {
                    IssueForProdPacking.jaSelected = new JArray();
                    IssueForProdPacking frm = new IssueForProdPacking("Issue For Production", 0);
                    frm.Text = "Issue For Production";
                    showForm(frm);
                }
                else
                {
                    apic.showCustomMsgBox("Validation", issuec.checkIssueDepartmentsMsgVal(issuec.prodLists, "Production"));
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
        }
    }


    public static class JsonExtensions
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }
    }

    static class ExtensionMethod
    {
        public static DataTable SortAlphaNumeric(this DataTable datatable, string columnName)
        {
            return datatable.AsEnumerable()
                      .OrderBy(r => r.Field<String>(columnName), new CustomComparer())
                      .CopyToDataTable();
        }
    }



    public class CustomComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int intTemp = 0;
            string sX = Regex.Match(x, @"\d+").Value;
            string sY = Regex.Match(y, @"\d+").Value;
            var numberX = int.TryParse(sX, out intTemp) ? int.Parse(sX) : intTemp;
            var numberY = int.TryParse(sY, out intTemp) ? int.Parse(sY) : intTemp;


            var alphaX = Regex.Match(x, @"[^a-z]").Value;
            var alphaY = Regex.Match(y, @"[^a-z]").Value;

            if (alphaX.CompareTo(alphaY) == 0)
                return numberX.CompareTo(numberY);
            else if (alphaX.CompareTo(alphaY) < 0)
                return -1;
            return 1;
        }
    }
}
