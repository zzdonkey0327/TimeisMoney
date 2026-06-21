using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using TimeisMoney.Helpers;
using System.Media; // 用來播放提示音

namespace TimeisMoney
{
    public partial class TaskPage : UserControl
    {
        private int elapsedSeconds = 0;
        private DateTime? currentStartTime = null;

        // 新增：用來記錄番茄鐘的目標總秒數
        private int pomodoroTargetSeconds = 0;

        public TaskPage()
        {
            InitializeComponent();

            this.Load += TaskPage_Load;
            btnStart.Click += BtnStart_Click;
            btnPause.Click += BtnPause_Click;
            btnStop.Click += BtnStop_Click;
            taskTimer.Tick += TaskTimer_Tick;

            // 綁定模式切換事件
            rdoPomodoro.CheckedChanged += RdoMode_CheckedChanged;
            rdoStopwatch.CheckedChanged += RdoMode_CheckedChanged;
        }

        private void TaskPage_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.AddRange(new string[] { "專案開發", "學習", "會議", "行政", "其他" });
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;

            btnPause.Enabled = false;
            btnStop.Enabled = false;
            LoadData();
        }

        // 處理模式切換的 UI 變化
        private void RdoMode_CheckedChanged(object sender, EventArgs e)
        {
            numPomodoroMins.Enabled = rdoPomodoro.Checked;

            // 如果還沒開始計時，切換模式時預先改變畫面上的數字
            if (currentStartTime == null)
            {
                if (rdoPomodoro.Checked)
                {
                    lblTimer.Text = TimeSpan.FromMinutes((double)numPomodoroMins.Value).ToString(@"hh\:mm\:ss");
                }
                else
                {
                    lblTimer.Text = "00:00:00";
                }
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("請先輸入任務名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentStartTime == null)
            {
                currentStartTime = DateTime.Now;
                // 如果是番茄鐘模式，計算目標總秒數
                if (rdoPomodoro.Checked)
                {
                    pomodoroTargetSeconds = (int)(numPomodoroMins.Value * 60);
                }
            }

            taskTimer.Start();

            btnStart.Enabled = false;
            txtTaskName.Enabled = false;
            rdoStopwatch.Enabled = false; // 計時中不可切換模式
            rdoPomodoro.Enabled = false;
            numPomodoroMins.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            taskTimer.Stop();
            btnStart.Enabled = true;
            btnStart.Text = "▶ 繼續";
            btnPause.Enabled = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            FinishTask("手動結束");
        }

        // 修改計時器邏輯，分開處理正向與倒數
        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;

            if (rdoStopwatch.Checked)
            {
                // 碼表模式：正向顯示
                TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
                lblTimer.Text = time.ToString(@"hh\:mm\:ss");
            }
            else
            {
                // 番茄鐘模式：計算剩餘時間
                int remainingSeconds = pomodoroTargetSeconds - elapsedSeconds;

                if (remainingSeconds <= 0)
                {
                    // 時間到！自動結束任務
                    taskTimer.Stop();
                    lblTimer.Text = "00:00:00";
                    SystemSounds.Exclamation.Play(); // 播放 Windows 預設提示音
                    FinishTask("番茄鐘完成");
                }
                else
                {
                    TimeSpan time = TimeSpan.FromSeconds(remainingSeconds);
                    lblTimer.Text = time.ToString(@"hh\:mm\:ss");
                }
            }
        }

        // 把原本 BtnStop_Click 的邏輯獨立出來
        private void FinishTask(string finishReason)
        {
            taskTimer.Stop();
            DateTime endTime = DateTime.Now;

            int durationMinutes = elapsedSeconds / 60;
            if (durationMinutes == 0 && elapsedSeconds > 0) durationMinutes = 1;

            decimal hourlyRate = numHourlyRate.Value;
            decimal totalCost = (durationMinutes / 60m) * hourlyRate;

            // 狀態可以根據完成原因記錄
            string status = finishReason == "番茄鐘完成" ? "番茄鐘達成" : "手動結束";
            SaveTaskRecord(currentStartTime.Value, endTime, durationMinutes, hourlyRate, totalCost, status);

            MessageBox.Show($"任務結束 ({finishReason})！\n總耗時：{durationMinutes} 分鐘\n產生價值：${totalCost:F0}", "任務完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ResetTimer();
            LoadData();
        }

        // 記得修改 SaveTaskRecord 方法，接收 status 參數並寫入資料庫
        private void SaveTaskRecord(DateTime start, DateTime end, int mins, decimal rate, decimal cost, string status)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO TaskRecords (TaskName, Category, StartTime, EndTime, DurationMinutes, HourlyRate, Cost, Status)
                    VALUES (@TaskName, @Category, @StartTime, @EndTime, @DurationMinutes, @HourlyRate, @Cost, @Status)";

                using (var cmd = new SqliteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@StartTime", start.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@EndTime", end.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@DurationMinutes", mins);
                    cmd.Parameters.AddWithValue("@HourlyRate", rate);
                    cmd.Parameters.AddWithValue("@Cost", cost);
                    cmd.Parameters.AddWithValue("@Status", status); // 寫入狀態

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ResetTimer()
        {
            elapsedSeconds = 0;
            currentStartTime = null;
            pomodoroTargetSeconds = 0;

            if (rdoPomodoro.Checked)
                lblTimer.Text = TimeSpan.FromMinutes((double)numPomodoroMins.Value).ToString(@"hh\:mm\:ss");
            else
                lblTimer.Text = "00:00:00";

            txtTaskName.Enabled = true;
            txtTaskName.Clear();
            btnStart.Text = "▶ 開始";
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;

            // 恢復模式切換的控制權
            rdoStopwatch.Enabled = true;
            rdoPomodoro.Enabled = true;
            numPomodoroMins.Enabled = rdoPomodoro.Checked;
        }

        // 載入資料的方法 (LoadData) 維持原樣即可
        private void LoadData()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM TaskRecords ORDER BY StartTime DESC";
                using (var cmd = new SqliteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvTasks.DataSource = dt;
                    }
                }
            }
        }
    }
}