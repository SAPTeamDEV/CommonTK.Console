using System;
using System.Collections.Generic;
using System.Linq;

using SAPTeam.CommonTK.Console;

namespace SAPTeam.CommonTK.Contexts
{
    /// <summary>
    /// Represents the <see cref="Context"/> that used for writing temporary data to the console.
    /// </summary>
    public class DisposableWriter : Context
    {
        private readonly List<ConsoleCoords> coords = new List<ConsoleCoords>();
        private readonly List<int> lines = new List<int>();
        private bool lineClear;
        private ConsoleColor backColor;
        private ConsoleColor foreColor;

        /// <summary>
        /// Initializes a new instance of <see cref="DisposableWriter"/>.
        /// </summary>
        /// <param name="lineClear">
        /// Determines that when disposing the writer, the entire wrote lines will be cleared or just clears the wrote texts.
        /// </param>
        /// <param name="backgroundColor">
        /// Determines the temporary console background color.
        /// </param>
        /// <param name="foregroundColor">
        /// Determines the temporary console foreground color.
        /// </param>
        public DisposableWriter(bool lineClear = false, ConsoleColor backgroundColor = ConsoleColor.Black, ConsoleColor foregroundColor = ConsoleColor.Gray) : base(lineClear, backgroundColor, foregroundColor)
        {

        }

        /// <inheritdoc/>
        protected override void CreateContext()
        {
            ColorSet.Current = new ColorSet(backColor, foreColor);
        }

        /// <inheritdoc/>
        protected override void DisposeContext()
        {
            ColorSet.Current = ColorSet.Default;

            Clear();

            coords.Clear();
        }

        /// <summary>
        /// Clears all wrote texts from console.
        /// </summary>
        public void Clear()
        {
            foreach (var coord in coords)
            {
                System.Console.CursorTop = coord.X;
                if (lineClear)
                {
                    Utils.ClearLine(true, null);
                }
                else
                {
                    Utils.ClearLine(true, coord.Length);
                }
            }

            if (lines.Count > 0)
            {
                System.Console.CursorTop = lines.Min();
            }
        }

        /// <summary>
        /// Registers a new line to the <see cref="DisposableWriter"/>.
        /// </summary>
        /// <param name="y">
        /// The vertical coordinate of the new line.
        /// </param>
        /// <param name="length">
        /// The length of texts on the line.
        /// </param>
        public void AddCoords(int y, int length)
        {
            if (!lines.Contains(y))
            {
                lines.Add(y);
                coords.Add(new ConsoleCoords() { Length = length, X = y });
            }
        }

        /// <inheritdoc/>
        protected override void ArgsHandler(dynamic[] args)
        {
            lineClear = args[0];
            backColor = args[1];
            foreColor = args[2];
        }
    }
}