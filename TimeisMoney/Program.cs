using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeisMoney.Helpers;

namespace TimeisMoney
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 在主畫面啟動前，確保資料庫與資料表已建立
            DatabaseHelper.InitializeDatabase();

            Application.Run(new MainForm());
        }
    }
}
