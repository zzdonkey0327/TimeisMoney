using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Data.Sqlite;
using TimeisMoney.Helpers;

namespace TimeisMoney
{
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();

            // 綁定 VisibleChanged 事件，確保每次切換到這頁都會重新撈取資料
            this.VisibleChanged += DashboardPage_VisibleChanged;
        }

        private void DashboardPage_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadSummaryCards();
                LoadExpenseChart();
                LoadTaskChart();
            }
        }

        // 補上這個空的方法來滿足 Designer 的要求
        private void DashboardPage_Load(object sender, EventArgs e)
        {
            // 這裡目前不需要寫東西，因為我們已經改用 VisibleChanged 來載入資料了
        }

        // 1. 載入上方總覽卡片 (計算本月結餘)
        private void LoadSummaryCards()
        {
            decimal totalExpense = 0;
            decimal totalTaskValue = 0;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // 撈取本月總支出 (使用 SQLite 的 strftime 函數比對年月)
                string expenseQuery = @"
                    SELECT SUM(Amount) FROM ExpenseRecords 
                    WHERE Type = '支出' AND strftime('%Y-%m', RecordDate) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(expenseQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        totalExpense = Convert.ToDecimal(result);
                }

                // 撈取本月任務創造的總價值
                string taskQuery = @"
                    SELECT SUM(Cost) FROM TaskRecords 
                    WHERE strftime('%Y-%m', StartTime) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(taskQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        totalTaskValue = Convert.ToDecimal(result);
                }
            }

            // 更新 UI
            lblTotalExpense.Text = $"${totalExpense:N0}";
            lblTotalTaskValue.Text = $"${totalTaskValue:N0}";

            decimal netBalance = totalTaskValue - totalExpense;
            lblNetBalance.Text = $"${netBalance:N0}";

            // 結餘如果是正的顯示綠色，負的顯示紅色
            lblNetBalance.ForeColor = netBalance >= 0 ? Color.Green : Color.Red;
        }

        // 2. 載入記帳圓餅圖 (依分類統計)
        private void LoadExpenseChart()
        {
            chartExpense.Series.Clear();
            chartExpense.Titles.Clear();
            chartExpense.Titles.Add("本月支出分類佔比");

            Series series = new Series("Expense")
            {
                ChartType = SeriesChartType.Pie, // 設定為圓餅圖
                IsValueShownAsLabel = true       // 在圖表上顯示數值
            };
            chartExpense.Series.Add(series);

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Category, SUM(Amount) as TotalAmount 
                    FROM ExpenseRecords 
                    WHERE Type = '支出' AND strftime('%Y-%m', RecordDate) = strftime('%Y-%m', 'now', 'localtime')
                    GROUP BY Category";

                using (var cmd = new SqliteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader["Category"].ToString();
                        double amount = Convert.ToDouble(reader["TotalAmount"]);

                        // 將資料點加入圖表
                        var pointIndex = series.Points.AddXY(category, amount);
                        // 將標籤改為顯示百分比與名稱
                        series.Points[pointIndex].Label = $"{category}\n#PERCENT{{P0}}";
                    }
                }
            }
        }

        // 3. 載入任務長條圖 (依分類統計時間成本)
        private void LoadTaskChart()
        {
            chartTask.Series.Clear();
            chartTask.Titles.Clear();
            chartTask.Titles.Add("本月各專案產生價值");

            Series series = new Series("TaskValue")
            {
                ChartType = SeriesChartType.Column, // 設定為垂直長條圖
                IsValueShownAsLabel = true,
                Color = Color.SteelBlue
            };
            chartTask.Series.Add(series);

            // 隱藏 X 軸的網格線讓畫面乾淨點
            chartTask.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Category, SUM(Cost) as TotalCost 
                    FROM TaskRecords 
                    WHERE strftime('%Y-%m', StartTime) = strftime('%Y-%m', 'now', 'localtime')
                    GROUP BY Category";

                using (var cmd = new SqliteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader["Category"].ToString();
                        double cost = Convert.ToDouble(reader["TotalCost"]);
                        series.Points.AddXY(category, cost);
                    }
                }
            }
        }
    }
}