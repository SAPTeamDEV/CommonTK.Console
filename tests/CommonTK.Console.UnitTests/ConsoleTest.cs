using SAPTeam.CommonTK.Console;

namespace SAPTeam.CommonTK.Console.UnitTests
{
    public class ConsoleTest
    {
        public static void Run()
        {
            ConsoleManager.ShowConsole(ConsoleLaunchMode.CreateClient);
        }
    }
}