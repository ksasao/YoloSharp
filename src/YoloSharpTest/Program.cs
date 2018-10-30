using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloSharpTest
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // C:\opencv343\build\x64\vc15\bin から opencv_world343.dll または opencv_world343d.dll を
            // YoloSharpTest.exe と同じフォルダにコピーしてください
            Application.Run(new Form1());
        }
    }
}
