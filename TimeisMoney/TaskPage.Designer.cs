namespace TimeisMoney
{
    partial class TaskPage
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numHourlyRate = new System.Windows.Forms.NumericUpDown();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.taskTimer = new System.Windows.Forms.Timer(this.components);
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.rdoStopwatch = new System.Windows.Forms.RadioButton();
            this.rdoPomodoro = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.numPomodoroMins = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numHourlyRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPomodoroMins)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "任務名稱:";
            // 
            // txtTaskName
            // 
            this.txtTaskName.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtTaskName.Location = new System.Drawing.Point(135, 15);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(143, 34);
            this.txtTaskName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label2.Location = new System.Drawing.Point(62, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "分類:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(135, 63);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(143, 33);
            this.cmbCategory.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label3.Location = new System.Drawing.Point(26, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "時薪 ($):";
            // 
            // numHourlyRate
            // 
            this.numHourlyRate.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numHourlyRate.Location = new System.Drawing.Point(135, 110);
            this.numHourlyRate.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numHourlyRate.Name = "numHourlyRate";
            this.numHourlyRate.Size = new System.Drawing.Size(143, 34);
            this.numHourlyRate.TabIndex = 5;
            // 
            // lblTimer
            // 
            this.lblTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTimer.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblTimer.Location = new System.Drawing.Point(31, 160);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(400, 102);
            this.lblTimer.TabIndex = 6;
            this.lblTimer.Text = "00:00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.btnStart.Location = new System.Drawing.Point(457, 115);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(119, 49);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "開始";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPause.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.btnPause.Location = new System.Drawing.Point(457, 170);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(119, 49);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "暫停";
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStop.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.btnStop.Location = new System.Drawing.Point(457, 222);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(119, 49);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "結束";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // taskTimer
            // 
            this.taskTimer.Interval = 1000;
            // 
            // dgvTasks
            // 
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTasks.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTasks.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTasks.GridColor = System.Drawing.Color.LimeGreen;
            this.dgvTasks.Location = new System.Drawing.Point(4, 277);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.RowHeadersWidth = 62;
            this.dgvTasks.RowTemplate.Height = 31;
            this.dgvTasks.Size = new System.Drawing.Size(793, 320);
            this.dgvTasks.TabIndex = 10;
            // 
            // rdoStopwatch
            // 
            this.rdoStopwatch.AutoSize = true;
            this.rdoStopwatch.Checked = true;
            this.rdoStopwatch.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdoStopwatch.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.rdoStopwatch.Location = new System.Drawing.Point(342, 17);
            this.rdoStopwatch.Name = "rdoStopwatch";
            this.rdoStopwatch.Size = new System.Drawing.Size(86, 34);
            this.rdoStopwatch.TabIndex = 11;
            this.rdoStopwatch.TabStop = true;
            this.rdoStopwatch.Text = "碼表";
            this.rdoStopwatch.UseVisualStyleBackColor = true;
            // 
            // rdoPomodoro
            // 
            this.rdoPomodoro.AutoSize = true;
            this.rdoPomodoro.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdoPomodoro.ForeColor = System.Drawing.Color.Firebrick;
            this.rdoPomodoro.Location = new System.Drawing.Point(457, 17);
            this.rdoPomodoro.Name = "rdoPomodoro";
            this.rdoPomodoro.Size = new System.Drawing.Size(110, 34);
            this.rdoPomodoro.TabIndex = 12;
            this.rdoPomodoro.Text = "番茄鐘";
            this.rdoPomodoro.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label4.Location = new System.Drawing.Point(337, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "設定分鐘：";
            // 
            // numPomodoroMins
            // 
            this.numPomodoroMins.Enabled = false;
            this.numPomodoroMins.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numPomodoroMins.Location = new System.Drawing.Point(457, 62);
            this.numPomodoroMins.Name = "numPomodoroMins";
            this.numPomodoroMins.Size = new System.Drawing.Size(119, 34);
            this.numPomodoroMins.TabIndex = 14;
            this.numPomodoroMins.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // TaskPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.numPomodoroMins);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rdoPomodoro);
            this.Controls.Add(this.rdoStopwatch);
            this.Controls.Add(this.dgvTasks);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.numHourlyRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.LimeGreen;
            this.Name = "TaskPage";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.TaskPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numHourlyRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPomodoroMins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numHourlyRate;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer taskTimer;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.RadioButton rdoStopwatch;
        private System.Windows.Forms.RadioButton rdoPomodoro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numPomodoroMins;
    }
}
