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
                LoadTrendChart();
            }
        }

        // 補上這個空的方法來滿足 Designer 的要求
        private void DashboardPage_Load(object sender, EventArgs e)
        {
            // 這裡目前不需要寫東西，因為我們已經改用 VisibleChanged 來載入資料了
        }

        // 1. 載入上方總覽卡片 (計算本月結餘)
        // 修改原本的 LoadSummaryCards 方法
        private void LoadSummaryCards()
        {
            decimal totalExpense = 0;
            decimal totalIncome = 0; // 新增：用來裝總收入
            decimal totalTaskValue = 0;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // 1. 撈取本月「支出」
                string expenseQuery = @"
            SELECT SUM(Amount) FROM ExpenseRecords 
            WHERE Type = '支出' AND strftime('%Y-%m', RecordDate) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(expenseQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        totalExpense = Convert.ToDecimal(result);
                }

                // 2. 撈取本月「收入」 (新增的邏輯)
                string incomeQuery = @"
            SELECT SUM(Amount) FROM ExpenseRecords 
            WHERE Type = '收入' AND strftime('%Y-%m', RecordDate) = strftime('%Y-%m', 'now', 'localtime')";
                using (var cmd = new SqliteCommand(incomeQuery, connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        totalIncome = Convert.ToDecimal(result);
                }

                // 3. 撈取本月「任務價值」
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

            // 更新 UI 卡片數字
            lblTotalExpense.Text = $"${totalExpense:N0}";
            lblTotalIncome.Text = $"${totalIncome:N0}"; // 顯示總收入
            lblTotalTaskValue.Text = $"${totalTaskValue:N0}";

            // 修正結餘公式：總收入 + 任務創造價值 - 總支出
            decimal netBalance = totalIncome + totalTaskValue - totalExpense;
            lblNetBalance.Text = $"${netBalance:N0}";

            // 結餘如果是正的顯示綠色，負的顯示紅色
            lblNetBalance.ForeColor = netBalance >= 0 ? Color.Green : Color.Red;
        }

        // ==========================================
        // 新增這個方法：載入收支比較圖表
        // ==========================================
        private void LoadTrendChart()
        {
            chartTrend.Series.Clear();
            chartTrend.Titles.Clear();
            chartTrend.Titles.Add("本月財務總覽比較");

            Series series = new Series("Amount")
            {
                ChartType = SeriesChartType.Column, // 垂直長條圖
                IsValueShownAsLabel = true          // 顯示數字
            };
            chartTrend.Series.Add(series);

            // 圖表外觀優化
            chartTrend.Legends.Clear();
            chartTrend.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chartTrend.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTrend.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartTrend.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            // 為了不重複連線資料庫，我們直接從畫面上已經算好的 Label 抓數字來畫圖
            // 注意：需要把字串裡的 '$' 和 ',' 去掉才能轉回數字
            decimal income = decimal.Parse(lblTotalIncome.Text.Replace("$", "").Replace(",", ""));
            decimal expense = decimal.Parse(lblTotalExpense.Text.Replace("$", "").Replace(",", ""));
            decimal taskValue = decimal.Parse(lblTotalTaskValue.Text.Replace("$", "").Replace(",", ""));

            // 將三個柱狀圖加入，並分別給予直覺的顏色
            int idxIncome = series.Points.AddXY("實際收入", income);
            series.Points[idxIncome].Color = Color.SeaGreen;

            int idxExpense = series.Points.AddXY("實際支出", expense);
            series.Points[idxExpense].Color = Color.IndianRed;

            int idxTask = series.Points.AddXY("任務產生價值", taskValue);
            series.Points[idxTask].Color = Color.SteelBlue;

            // 格式化柱子上的數字標籤
            series.Points[idxIncome].Label = $"${income:N0}";
            series.Points[idxExpense].Label = $"${expense:N0}";
            series.Points[idxTask].Label = $"${taskValue:N0}";
        }

        // 2. 載入記帳圓餅圖 (依分類統計)
        private void LoadExpenseChart()
        {
            chartExpense.Series.Clear();
            chartExpense.Titles.Clear();
            chartExpense.Titles.Add("本月支出分類佔比");

            // 新增：隱藏右側的圖例，因為標籤已經直接顯示在圓餅上了
            chartExpense.Legends.Clear();

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

            // --- 新增 UI 優化設定 ---
            // 1. 隱藏不必要的圖例 (Legend)
            chartTask.Legends.Clear();

            // 2. 格式化 Y 軸數字，去除小數點並加上千分位 (例如：120)
            chartTask.ChartAreas[0].AxisY.LabelStyle.Format = "N0";

            // 3. 隱藏 X 軸與 Y 軸的背景網格線，讓畫面更乾淨
            chartTask.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTask.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartTask.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            // -------------------------

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
                        //series.Points.AddXY(category, cost);
                        int pointIndex = series.Points.AddXY(category, cost);

                        // 新增：把柱狀圖上方的數字也格式化，去掉小數點
                        series.Points[pointIndex].Label = $"${cost:N0}";
                    }
                }
            }
        }
    }
}