using SAPTeam.CommonTK.Console.SharedInteractiveTests;

namespace SAPTeam.CommonTK.Console.ConsoleInteractiveTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tests = new EntryPoint();
            tests.BeginTests();
        }
    }
}