using System;
using System.IO;

namespace SAPTeam.CommonTK.Contexts
{
    /// <summary>
    /// Represents the <see cref="Context"/> that used for hiding console outputs.
    /// </summary>
    public class RedirectConsole : Context
    {
        private TextWriter consoleOut;
        private StringWriter consoleOutVirtual;

        /// <summary>
        /// Gets or Sets the vertical coordinate.
        /// </summary>
        internal int Line { get; set; }

        /// <inheritdoc/>
        public override string[] Groups => Array.Empty<string>();

        /// <summary>
        /// Initializes a new instance of <see cref="RedirectConsole"/>.
        /// </summary>
        public RedirectConsole()
        {
            Initialize(true);
        }

        /// <inheritdoc/>
        protected override void CreateContext()
        {
            consoleOut = System.Console.Out;
            consoleOutVirtual = new StringWriter();
            System.Console.SetOut(consoleOutVirtual);
        }

        /// <inheritdoc/>
        protected override void DisposeContext()
        {
            System.Console.SetOut(consoleOut);
            consoleOut = null;
            consoleOutVirtual.Dispose();
        }
    }
}