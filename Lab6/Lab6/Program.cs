using Lab6;
using System;
using System.Windows.Forms;

namespace Lab6WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // .NET 8: стандартный вызов для WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
