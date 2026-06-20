using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace TimeisMoney.Helpers
{
    public static class DatabaseHelper
    {
        // 資料庫檔案名稱
        private const string DbFileName = "TimeIsMoney.db";

        // 連線字串 (注意：Microsoft.Data.Sqlite 不需要加上 Version=3)
        public static readonly string ConnectionString = $"Data Source={DbFileName};";

        /// <summary>
        /// 初始化資料庫：檢查檔案是否存在，若無則建立檔案與資料表
        /// </summary>
        public static void InitializeDatabase()
        {
            // 如果資料庫檔案不存在，則建立新的
            if (!File.Exists(DbFileName))
            {
                // Microsoft.Data.Sqlite 沒有 CreateFile 方法，因此我們直接用 System.IO 建立空檔案
                File.Create(DbFileName).Dispose();

                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    // 1. 建立 ExpenseRecords (記帳紀錄) 表
                    string createExpenseTable = @"
                        CREATE TABLE ExpenseRecords (
                            ExpenseId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Type TEXT,
                            Category TEXT,
                            Amount REAL,
                            RecordDate TEXT,
                            Note TEXT,
                            PaymentMethod TEXT
                        );";
                    using (var cmd = new SqliteCommand(createExpenseTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 2. 建立 TaskRecords (任務紀錄) 表
                    string createTaskTable = @"
                        CREATE TABLE TaskRecords (
                            TaskId INTEGER PRIMARY KEY AUTOINCREMENT,
                            TaskName TEXT,
                            Category TEXT,
                            StartTime TEXT,
                            EndTime TEXT,
                            DurationMinutes INTEGER,
                            HourlyRate REAL,
                            Cost REAL,
                            Status TEXT,
                            Note TEXT
                        );";
                    using (var cmd = new SqliteCommand(createTaskTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// 取得一個新的資料庫連線實體
        /// </summary>
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}