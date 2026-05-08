using System;
using System.Windows.Forms;

namespace CV_app
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobalVar.frmMain = new Main();
            GlobalVar.frmMain.ShowDialog();
        }
    }
}
