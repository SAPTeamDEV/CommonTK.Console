using System.Windows.Forms;

using SAPTeam.CommonTK.Console;
using SAPTeam.CommonTK.Contexts;
using SAPTeam.CommonTK.Console.SharedInteractiveTests;
using System.Threading;

namespace CommonTK.Console.UIInteractiveTests
{
    public partial class MainForm : Form
    {
        ConsoleWindow con;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            con = new ConsoleWindow(ConsoleLaunchMode.CreateClient, true);
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            var tests = new EntryPoint();
            new Thread(tests.BeginTests).Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConsoleManager.Pipe.Close();
        }
    }
}