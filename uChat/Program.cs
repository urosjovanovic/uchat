using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace uChat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            Thread connectThread = new Thread(f.LoopConnect);
            connectThread.IsBackground = true;
            connectThread.Name = "connect";
            connectThread.Start();
            Application.Run(f);
        }
    }
}
