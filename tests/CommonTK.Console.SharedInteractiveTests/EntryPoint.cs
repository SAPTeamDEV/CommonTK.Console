using System.Threading;

namespace SAPTeam.CommonTK.Console.SharedInteractiveTests
{
    public class EntryPoint
    {
        public void BeginTests()
        {
            int i = 0;
            while (true)
            {
                i += 1;
                System.Console.WriteLine($"test {i}");
                System.Console.Out.Flush();
                Thread.Sleep(1000);
            }
        }
    }
}