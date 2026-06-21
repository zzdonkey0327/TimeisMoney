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
        // 新增：用類別層級的變數儲存當月數值，避免從 Label 抓取字串解析
        private decimal monthIncome = 0;
        private decimal monthExpense = 0;
        private decimal monthTaskValue = 0;

        public DashboardPage()
        {
            InitializeComponent();
            this.VisibleChanged += DashboardPage_VisibleChanged;
        }

        private void DashboardPage_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // 必須先計算好卡片數值，再載入比較圖表
                LoadSummaryCards();
                LoadExpenseChart();
                LoadTaskChart();
                LoadTrendChart();
            }
        }

        private void DashboardPage_Load(object sender, EventArgs e)
        {
        }

        // 1. 載入上方總覽卡片
        private void LoadSummaryCards()
        {
            monthExpense = 0;
            monthIncome = 0;
            monthTaskValue = 0;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // 撈取本月支出
                string expenseQuery = @"
                    SELECT SUM(Amount) FROM ExpenseRecords 
                    WHERE Type = '支出' AND strftime('%Y-%m', RecordDate) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(expenseQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        monthExpense = Convert.ToDecimal(result);
                }

                // 撈取本月收入
                string incomeQuery = @"
                    SELECT SUM(Amount) FROM ExpenseRecords 
                    WHERE Type = '收入' AND strftime('%Y-%m', RecordDate) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(incomeQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        monthIncome = Convert.ToDecimal(result);
                }

                // 撈取本月任務價值
                string taskQuery = @"
                    SELECT SUM(Cost) FROM TaskRecords 
                    WHERE strftime('%Y-%m', StartTime) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(taskQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        monthTaskValue = Convert.ToDecimal(result);
                }
            }

            // 更新 UI 卡片數字
            lblTotalExpense.Text = $"${monthExpense:N0}";
            lblTotalIncome.Text = $"${monthIncome:N0}"; // 確保 Designer 中有建立此變數
            lblTotalTaskValue.Text = $"${monthTaskValue:N0}";

            // 結餘公式：總收入 + 任務價值 - 總支出
            decimal netBalance = monthIncome + monthTaskValue - monthExpense;
            lblNetBalance.Text = $"${netBalance:N0}";
            lblNetBalance.ForeColor = netBalance >= 0 ? Color.Green : Color.Red;
        }

        // 2. 載入收支比較圖表
        private void LoadTrendChart()
        {
            chartTrend.Series.Clear();
            chartTrend.Titles.Clear();
            chartTrend.Titles.Add("本月財務總覽比較");

            Series series = new Series("Amount")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };
            chartTrend.Series.Add(series);

            chartTrend.Legends.Clear();
            chartTrend.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chartTrend.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTrend.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartTrend.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // 直接使用安全乾淨的 decimal 變數繪圖，完全防呆
            int idxIncome = series.Points.AddXY("實際收入", monthIncome);
            series.Points[idxIncome].Color = Color.SeaGreen;
            series.Points[idxIncome].Label = $"${monthIncome:N0}";

            int idxExpense = series.Points.AddXY("實際支出", monthExpense);
            series.Points[idxExpense].Color = Color.IndianRed;
            series.Points[idxExpense].Label = $"${monthExpense:N0}";

            int idxTask = series.Points.AddXY("任務產生價值", monthTaskValue);
            series.Points[idxTask].Color = Color.SteelBlue;
            series.Points[idxTask].Label = $"${monthTaskValue:N0}";
        }

        // 3. 載入記帳圓餅圖
        private void LoadExpenseChart()
        {
            chartExpense.Series.Clear();
            chartExpense.Titles.Clear();
            chartExpense.Titles.Add("本月支出分類佔比");
            chartExpense.Legends.Clear();

            Series series = new Series("Expense")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
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
                        var pointIndex = series.Points.AddXY(category, amount);
                        series.Points[pointIndex].Label = $"{category}\n#PERCENT{{P0}}";
                    }
                }
            }
        }

        // 4. 載入任務長條圖
        private void LoadTaskChart()
        {
            chartTask.Series.Clear();
            chartTask.Titles.Clear();
            chartTask.Titles.Add("本月各專案產生價值");
            chartTask.Legends.Clear();

            Series series = new Series("TaskValue")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Color = Color.SteelBlue
            };
            chartTask.Series.Add(series);

            chartTask.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chartTask.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTask.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartTask.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

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
                        int pointIndex = series.Points.AddXY(category, cost);
                        series.Points[pointIndex].Label = $"${cost:N0}";
                    }
                }
            }
        }
    }
}