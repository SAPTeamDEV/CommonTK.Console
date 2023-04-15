using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

                var reader = new StreamReader(receiver);
                var writer = new StreamWriter(receiver);

                System.Console.SetIn(reader);
                System.Console.SetOut(writer);

                System.Console.WriteLine("Answer");

                while (true)
                {
                    System.Console.ReadLine();
                    Thread.Sleep(100);
                }

                return;
                int len;
                len = receiver.ReadByte() * 256;
                len += receiver.ReadByte();

                var data = new byte[len];
                receiver.Read(data, 0, len);
                var text = Encoding.Unicode.GetString(data);
                text.Trim();
                System.Console.Write(text);
                System.Console.ReadKey();
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
        }
    }
}
