using System.Windows.Forms;

using SAPTeam.CommonTK.Console;
using SAPTeam.CommonTK.Contexts;
using SAPTeam.CommonTK.Console.SharedInteractiveTests;

namespace CommonTK.Console.UIInteractiveTests
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            using (var con = new ConsoleWindow(ConsoleLaunchMode.CreateClient))
            {
                var tests = new EntryPoint();
                tests.BeginTests();
            }
        }
    }
}