using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CommandLine;

using SAPTeam.Zily;

using Serilog;
using Serilog.Core;

namespace SAPTeam.CommonTK.Console.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(options => Run(options));
        }

        public static void Run(CommandLineOptions options)
        {
            if (!options.Quiet)
            {
                var logSource = new LoggerConfiguration()
                            .WriteTo.Console();

                if (options.Verbose)
                {
                    logSource = logSource.MinimumLevel.Debug();
                }

                Log.Logger = logSource.CreateLogger(); 
            }

            if (options.PipeName != null)
            {
                if (options.Zily)
                {
                    var pipe = new NamedPipeClientStream(options.PipeName);
                    var side = new ZilyClientSide(pipe);

                    pipe.Connect();
                    side.Connect();
                    side.Listen(!options.Verbose);
                }
                else
                {
                    System.Console.Error.WriteLine("This feature is not implemented in this version, use --zily to use this protocol for communication.");
                    Environment.Exit(1);
                }
            }
            else if (options.Silent)
            {
                Log.Logger.Information("Application is ready");
                while (true)
                {
                    System.Console.Read();
                }
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
