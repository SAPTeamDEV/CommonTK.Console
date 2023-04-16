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

namespace SAPTeam.CommonTK.Console.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "-p")
            {
                var receiver = new NamedPipeClientStream(args[1]);
                receiver.Connect();

                var zs = new ZilyStream(receiver);
                int i = 0;

                while (true)
                {
                    receiver.WaitForPipeDrain();
                    System.Console.WriteLine(zs.ReadString());
                    if (i > 10)
                    {
                        zs.WriteString("END");
                        receiver.Close();
                        break;
                    }
                    else
                    {
                        zs.WriteString("OK");
                    }
                    i++;
                }
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
