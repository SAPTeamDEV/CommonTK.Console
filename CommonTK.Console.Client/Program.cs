using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPTeam.CommonTK.Console.Client
{
    internal class Program
    {
        public static void Main()
        {
            var receiver = new NamedPipeClientStream("console.pipe");

            receiver.Connect();
            int len;
            len = receiver.ReadByte() * 256;
            len += receiver.ReadByte();

            var data = new byte[len];
            receiver.Read(data, 0, len);
            var text = Encoding.Unicode.GetString(data);
            System.Console.Write(text);

        }
    }
}
