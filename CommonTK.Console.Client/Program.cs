using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SAPTeam.Zily;

using Serilog;

namespace SAPTeam.CommonTK.Console.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "-p")
            {
                Log.Logger = new LoggerConfiguration()
#if DEBUG
                    .MinimumLevel.Debug()
#endif
                    .WriteTo.Console()
                    .CreateLogger();

                var pipe = new NamedPipeClientStream(args[1]);
                var client = new ZilyPipeClientStream(pipe);

                Thread.Sleep(1000);
                client.Connect();
                client.Listen();
            }
            else if (args[0] == "-s")
            {
                while (true)
                {
                    System.Console.ReadLine();
                }
            }
            else
            {
                System.Console.WriteLine("Invalid syntax: " + args.ToString());
                System.Console.WriteLine("valid arguments are: -p <pipe name> or -s");
                Environment.Exit(1);
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
