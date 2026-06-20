using System;
using System.Windows.Forms;

namespace TimeisMoney
{
    public partial class MainForm : Form
    {
        // 預先建立各個子頁面的實體，這樣切換時資料或計時器才不會不見
        private DashboardPage dashboardPage = new DashboardPage();
        private ExpensePage expensePage = new ExpensePage();
        private TaskPage taskPage = new TaskPage();
        private SettingsPage settingsPage = new SettingsPage();

        public MainForm()
        {
            InitializeComponent();

            // 綁定按鈕的點擊事件
            btnDashboard.Click += BtnDashboard_Click;
            btnExpense.Click += BtnExpense_Click;
            btnTask.Click += BtnTask_Click;
            btnSettings.Click += BtnSettings_Click;

            // 程式啟動時，預設顯示總覽頁面
            ShowPage(dashboardPage);
        }

        /// <summary>
        /// 切換右側內容區的頁面
        /// </summary>
        private void ShowPage(UserControl page)
        {
            // 清除內容區目前所有的控制項
            panelMainContent.Controls.Clear();

            // 設定子頁面填滿容器
            page.Dock = DockStyle.Fill;

            // 將子頁面加入並顯示
            panelMainContent.Controls.Add(page);
        }

        // 按鈕點擊事件處理
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ShowPage(dashboardPage);
        }

        private void BtnExpense_Click(object sender, EventArgs e)
        {
            ShowPage(expensePage);
        }

        private void BtnTask_Click(object sender, EventArgs e)
        {
            ShowPage(taskPage);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ShowPage(settingsPage);
        }
    }
}