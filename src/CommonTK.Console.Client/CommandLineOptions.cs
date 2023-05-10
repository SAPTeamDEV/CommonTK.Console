using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace SAPTeam.CommonTK.Console.Client
{
    internal class CommandLineOptions
    {
        // Pipe options
        [Option('p', "pipe", SetName = "pipe", Required = true, HelpText = "Uses pipe connection")]
        public string PipeName { get; set; }

        [Option('z', "zily", SetName = "pipe", HelpText = "Uses Zily protocol for establishing connection")]
        public bool Zily { get; set; }

        // Silent Options
        [Option('s', "silent", SetName = "silent", Required = true, HelpText = "Starts a blank session")]
        public bool Silent { get; set; }

        // General Options
        [Option('q', "quiet", HelpText = "Be more quiet, disables logging")]
        public bool Quiet { get; set; }

        [Option('v', "verbose", HelpText = "Be more verbose")]
        public bool Verbose { get; set; }
    }
}
