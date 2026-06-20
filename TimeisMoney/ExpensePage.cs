using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using TimeisMoney.Helpers; // 確保有引入 DatabaseHelper

namespace TimeisMoney
{
    public partial class ExpensePage : UserControl
    {
        // 用來記錄目前準備要編輯或刪除的資料 ID，-1 代表沒選取
        private int currentSelectedId = -1;
        public ExpensePage()
        {
            InitializeComponent();

            // 綁定事件
            this.Load += ExpensePage_Load;
            btnAdd.Click += BtnAdd_Click;
        }

        // 當頁面載入時觸發
        private void ExpensePage_Load(object sender, EventArgs e)
        {
            // 初始化下拉選單選項
            cmbType.Items.AddRange(new string[] { "支出", "收入" });
            cmbType.SelectedIndex = 0; // 預設選取第一項

            cmbCategory.Items.AddRange(new string[] { "飲食", "交通", "購物", "娛樂", "薪資", "其他" });
            cmbCategory.SelectedIndex = 0;

            cmbPaymentMethod.Items.AddRange(new string[] { "現金", "信用卡", "行動支付", "轉帳" });
            cmbPaymentMethod.SelectedIndex = 0;

            // 載入資料庫資料到 DataGridView
            LoadData();
        }

        // 從 SQLite 讀取資料並綁定到 DataGridView
        private void LoadData()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                // 撈取所有紀錄，並依日期由新到舊排序
                string query = "SELECT * FROM ExpenseRecords ORDER BY RecordDate DESC";

                using (var cmd = new SqliteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader); // 將 SqliteDataReader 的資料轉入 DataTable
                        dgvExpenses.DataSource = dt; // 綁定到畫面的表格
                    }
                }
            }
        }

        // 新增按鈕點擊事件
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // 簡單防呆檢查
            if (numAmount.Value <= 0)
            {
                MessageBox.Show("請輸入大於 0 的金額！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // 使用參數化查詢 (@變數) 防止 SQL Injection，這是好習慣！
                string insertQuery = @"
                    INSERT INTO ExpenseRecords (Type, Category, Amount, RecordDate, Note, PaymentMethod)
                    VALUES (@Type, @Category, @Amount, @RecordDate, @Note, @PaymentMethod)";

                using (var cmd = new SqliteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Amount", numAmount.Value);
                    // 將日期格式化為字串儲存
                    cmd.Parameters.AddWithValue("@RecordDate", dtpRecordDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Note", txtNote.Text);
                    cmd.Parameters.AddWithValue("@PaymentMethod", cmbPaymentMethod.SelectedItem.ToString());

                    cmd.ExecuteNonQuery(); // 執行寫入
                }
            }

            MessageBox.Show("新增成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadData(); // 重新載入表格，讓新資料顯示出來
            ClearInput(); // 寫一個小方法把輸入框清空
        }



        // 清空輸入區的輔助方法
        private void ClearInput()
        {
            numAmount.Value = 0;
            txtNote.Clear();
            dtpRecordDate.Value = DateTime.Now;
        }

        private void dgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 確保點擊的是有效的資料列 (排除標題列)
            if (e.RowIndex >= 0)
            {
                // 取得目前點選的該筆資料列
                DataGridViewRow row = dgvExpenses.Rows[e.RowIndex];

                // 將隱藏在表格中的 ID 存入變數 (假設 ExpenseId 是第 0 欄)
                currentSelectedId = Convert.ToInt32(row.Cells["ExpenseId"].Value);

                // 將表格的資料倒灌回 UI 控制項
                cmbType.SelectedItem = row.Cells["Type"].Value.ToString();
                cmbCategory.SelectedItem = row.Cells["Category"].Value.ToString();
                numAmount.Value = Convert.ToDecimal(row.Cells["Amount"].Value);

                // 處理日期格式
                if (DateTime.TryParse(row.Cells["RecordDate"].Value.ToString(), out DateTime recordDate))
                {
                    dtpRecordDate.Value = recordDate;
                }

                cmbPaymentMethod.SelectedItem = row.Cells["PaymentMethod"].Value.ToString();
                txtNote.Text = row.Cells["Note"].Value.ToString();

                // 開啟編輯與刪除按鈕
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Enabled = false; // 編輯模式下先關閉新增按鈕
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (currentSelectedId == -1) return;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string updateQuery = @"
            UPDATE ExpenseRecords 
            SET Type = @Type, Category = @Category, Amount = @Amount, 
                RecordDate = @RecordDate, Note = @Note, PaymentMethod = @PaymentMethod
            WHERE ExpenseId = @ExpenseId";

                using (var cmd = new SqliteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Type", cmbType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Amount", numAmount.Value);
                    cmd.Parameters.AddWithValue("@RecordDate", dtpRecordDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Note", txtNote.Text);
                    cmd.Parameters.AddWithValue("@PaymentMethod", cmbPaymentMethod.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ExpenseId", currentSelectedId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("資料已更新！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetFormState();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentSelectedId == -1) return;

            // 給一個防呆確認視窗
            var confirmResult = MessageBox.Show("確定要刪除這筆紀錄嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM ExpenseRecords WHERE ExpenseId = @ExpenseId";

                    using (var cmd = new SqliteCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ExpenseId", currentSelectedId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("資料已刪除！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetFormState();
            }
        }

        // 寫一個小方法用來重置表單狀態
        private void ResetFormState()
        {
            LoadData(); // 重新整理表格
            ClearInput(); // 之前寫過的清空輸入框方法
            currentSelectedId = -1;

            // 按鈕狀態恢復預設
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}