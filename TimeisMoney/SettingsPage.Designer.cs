namespace TimeisMoney
{
    partial class SettingsPage
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExportExpense = new System.Windows.Forms.Button();
            this.btnExportTask = new System.Windows.Forms.Button();
            this.btnBackupDb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExportExpense
            // 
            this.btnExportExpense.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExportExpense.Location = new System.Drawing.Point(257, 105);
            this.btnExportExpense.Name = "btnExportExpense";
            this.btnExportExpense.Size = new System.Drawing.Size(285, 70);
            this.btnExportExpense.TabIndex = 1;
            this.btnExportExpense.Text = "匯出記帳 (.csv)";
            this.btnExportExpense.UseVisualStyleBackColor = true;
            // 
            // btnExportTask
            // 
            this.btnExportTask.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExportTask.Location = new System.Drawing.Point(257, 192);
            this.btnExportTask.Name = "btnExportTask";
            this.btnExportTask.Size = new System.Drawing.Size(285, 70);
            this.btnExportTask.TabIndex = 2;
            this.btnExportTask.Text = "匯出任務 (.csv)";
            this.btnExportTask.UseVisualStyleBackColor = true;
            // 
            // btnBackupDb
            // 
            this.btnBackupDb.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBackupDb.Location = new System.Drawing.Point(257, 277);
            this.btnBackupDb.Name = "btnBackupDb";
            this.btnBackupDb.Size = new System.Drawing.Size(285, 70);
            this.btnBackupDb.TabIndex = 3;
            this.btnBackupDb.Text = "備份資料庫 (.db)";
            this.btnBackupDb.UseVisualStyleBackColor = true;
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBackupDb);
            this.Controls.Add(this.btnExportTask);
            this.Controls.Add(this.btnExportExpense);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExportExpense;
        private System.Windows.Forms.Button btnExportTask;
        private System.Windows.Forms.Button btnBackupDb;
    }
}
