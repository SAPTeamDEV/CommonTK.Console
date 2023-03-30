﻿using System;
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
        protected override void ArgsHandler(dynamic[] args)
        {
            throw new NotImplementedException();
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