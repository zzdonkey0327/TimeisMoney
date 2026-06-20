using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using TimeisMoney.Helpers;

namespace TimeisMoney
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();

            // 綁定按鈕點擊事件
            btnExportExpense.Click += BtnExportExpense_Click;
            btnExportTask.Click += BtnExportTask_Click;
            btnBackupDb.Click += BtnBackupDb_Click;
        }

        // 1. 匯出記帳紀錄為 CSV
        private void BtnExportExpense_Click(object sender, EventArgs e)
        {
            ExportToCSV("ExpenseRecords", "記帳紀錄備份");
        }

        // 2. 匯出任務紀錄為 CSV
        private void BtnExportTask_Click(object sender, EventArgs e)
        {
            ExportToCSV("TaskRecords", "任務時間紀錄備份");
        }

        // 核心通用的 CSV 匯出邏輯
        private void ExportToCSV(string tableName, string defaultFileName)
        {
            // 建立 WinForms 內建的存檔視窗
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV 檔案 (*.csv)|*.csv";
                sfd.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd}";
                sfd.Title = $"請選擇【{defaultFileName}】的儲存位置";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var connection = DatabaseHelper.GetConnection())
                        {
                            connection.Open();
                            string query = $"SELECT * FROM {tableName}";

                            using (var cmd = new SqliteCommand(query, connection))
                            using (var reader = cmd.ExecuteReader())
                            {
                                // 使用 Encoding.UTF8 (true) 加上 BOM 標頭，防止 Excel 開啟時亂碼
                                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                                {
                                    // 寫入欄位標題 (Header)
                                    int fieldCount = reader.FieldCount;
                                    for (int i = 0; i < fieldCount; i++)
                                    {
                                        sw.Write(reader.GetName(i));
                                        if (i < fieldCount - 1) sw.Write(",");
                                    }
                                    sw.WriteLine();

                                    // 寫入資料內容 (Rows)
                                    while (reader.Read())
                                    {
                                        for (int i = 0; i < fieldCount; i++)
                                        {
                                            // 處理資料內包含逗號的情況，用雙引號包起來避免 CSV 跑格
                                            string value = reader[i].ToString().Replace("\"", "\"\"");
                                            sw.Write($"\"{value}\"");

                                            if (i < fieldCount - 1) sw.Write(",");
                                        }
                                        sw.WriteLine();
                                    }
                                }
                            }
                        }
                        MessageBox.Show("CSV 檔案匯出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"匯出失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 3. 備份整個 SQLite 資料庫檔案 (.db)
        private void BtnBackupDb_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "SQLite 資料庫 (*.db)|*.db";
                sfd.FileName = $"TimeIsMoney_Backup_{DateTime.Now:yyyyMMdd}.db";
                sfd.Title = "請選擇資料庫備份檔的儲存位置";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 源頭資料庫路徑 (目前執行目錄下的 TimeIsMoney.db)
                        string sourceDbPath = "TimeIsMoney.db";

                        // 使用者選擇的備份目標路徑
                        string targetPath = sfd.FileName;

                        // 直接利用 C# System.IO 複製檔案，簡單暴力且最安全
                        File.Copy(sourceDbPath, targetPath, true);

                        MessageBox.Show("資料庫備份成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"備份失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}