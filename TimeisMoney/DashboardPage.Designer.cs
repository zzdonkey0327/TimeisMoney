namespace TimeisMoney
{
    partial class DashboardPage
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTotalExpense = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalTaskValue = new System.Windows.Forms.Label();
            this.lblNetBalance = new System.Windows.Forms.Label();
            this.chartExpense = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTask = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartExpense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTask)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalExpense
            // 
            this.lblTotalExpense.AutoSize = true;
            this.lblTotalExpense.Font = new System.Drawing.Font("微軟正黑體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTotalExpense.Location = new System.Drawing.Point(40, 46);
            this.lblTotalExpense.Name = "lblTotalExpense";
            this.lblTotalExpense.Size = new System.Drawing.Size(55, 40);
            this.lblTotalExpense.TabIndex = 0;
            this.lblTotalExpense.Text = "$0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNetBalance);
            this.panel1.Controls.Add(this.lblTotalTaskValue);
            this.panel1.Controls.Add(this.lblTotalExpense);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 122);
            this.panel1.TabIndex = 1;
            // 
            // lblTotalTaskValue
            // 
            this.lblTotalTaskValue.AutoSize = true;
            this.lblTotalTaskValue.Font = new System.Drawing.Font("微軟正黑體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTotalTaskValue.Location = new System.Drawing.Point(256, 46);
            this.lblTotalTaskValue.Name = "lblTotalTaskValue";
            this.lblTotalTaskValue.Size = new System.Drawing.Size(55, 40);
            this.lblTotalTaskValue.TabIndex = 1;
            this.lblTotalTaskValue.Text = "$0";
            // 
            // lblNetBalance
            // 
            this.lblNetBalance.AutoSize = true;
            this.lblNetBalance.Font = new System.Drawing.Font("微軟正黑體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNetBalance.Location = new System.Drawing.Point(418, 46);
            this.lblNetBalance.Name = "lblNetBalance";
            this.lblNetBalance.Size = new System.Drawing.Size(55, 40);
            this.lblNetBalance.TabIndex = 2;
            this.lblNetBalance.Text = "$0";
            // 
            // chartExpense
            // 
            chartArea1.Name = "ChartArea1";
            this.chartExpense.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartExpense.Legends.Add(legend1);
            this.chartExpense.Location = new System.Drawing.Point(3, 131);
            this.chartExpense.Name = "chartExpense";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartExpense.Series.Add(series1);
            this.chartExpense.Size = new System.Drawing.Size(300, 300);
            this.chartExpense.TabIndex = 2;
            this.chartExpense.Text = "chart1";
            // 
            // chartTask
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTask.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartTask.Legends.Add(legend2);
            this.chartTask.Location = new System.Drawing.Point(309, 131);
            this.chartTask.Name = "chartTask";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartTask.Series.Add(series2);
            this.chartTask.Size = new System.Drawing.Size(300, 300);
            this.chartTask.TabIndex = 3;
            this.chartTask.Text = "chart2";
            // 
            // DashboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartTask);
            this.Controls.Add(this.chartExpense);
            this.Controls.Add(this.panel1);
            this.Name = "DashboardPage";
            this.Size = new System.Drawing.Size(616, 437);
            this.Load += new System.EventHandler(this.DashboardPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartExpense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTotalExpense;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotalTaskValue;
        private System.Windows.Forms.Label lblNetBalance;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartExpense;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTask;
    }
}
