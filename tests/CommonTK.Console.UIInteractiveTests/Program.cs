using System;
using System.Windows.Forms;

using CommonTK.Console.UIInteractiveTests;

namespace SAPTeam.CommonTK.Console.UIInteractiveTests
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            Application.Run(new MainForm());
        }
    }
}